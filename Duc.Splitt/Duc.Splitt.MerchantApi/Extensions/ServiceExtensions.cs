﻿using Duc.Splitt.MerchantApi.ActionFilters;
using Duc.Splitt.MerchantApi.Helper;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Logger;
using Duc.Splitt.Repository;
using Duc.Splitt.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Identity;
using Duc.Splitt.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Duc.Splitt.MerchantApi.Extensions
{
    public static class ServiceExtensions
    {

        public static void ApplyServiceExtensions(this WebApplicationBuilder builder)
        {

            builder.ApplyServiceConfiguration();
            builder.ApplyAppConfiguration();
        }
        private static void ApplyServiceConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(ObjectMapperAr));
            builder.Services.AddAutoMapper(typeof(ObjectMapperEn));
            // builder.Services.AddControllersWithViews();
            // builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.ConfigureIdentityDbContext();
            builder.Services.AddScoped<ValidateSecureClientAttribute>();
            builder.Services.AddScoped<ValidateAnonymousClientAttribute>();
            builder.ConfigureSwagger();
            builder.ConfigureCors();
            builder.ConfigureSqlDbContext();
            builder.ConfigureLoggerService();
            builder.ConfigureUnitOfWork();
            builder.ConfigureBusinessLogicServices();
            builder.ConfigureLocalization();
            builder.ConfigureAppSettingsValue();
            builder.ConfigureAuthentication();
            //builder.ConfigureSignalR();
            builder.ConfigureRateLimiter();
            builder.Services.Configure<HostOptions>(hostOptions =>
           {
               hostOptions.BackgroundServiceExceptionBehavior =
               BackgroundServiceExceptionBehavior.Ignore;
           });
        }

        private static void ApplyAppConfiguration(this WebApplicationBuilder builder)
        {
            var app = builder.Build();
            app.UseRateLimiter();
            app.UseStaticFiles();
            var allowSwagger = builder.Configuration.GetValue<bool>("AllowSwagger");
            if (allowSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<LoggingMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.MapRazorPages();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=Index}/{id?}");

            var supportedCultures = new[]
             {
                    new CultureInfo( Constant.LanguageEnText),
                    new CultureInfo(Constant.LanguageArText)
                };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(Constant.LanguageEnText, Constant.LanguageArText),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            //  app.UseRouting();
            app.Run();

        }
        #region Services 



        public static void ConfigureIdentityDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<SplittIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionStringSplitt"),
            b => b.MigrationsAssembly("Duc.Splitt.Identity")));

            builder.Services.AddIdentity<SplittIdentityUser, SplittIdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

            })
                .AddEntityFrameworkStores<SplittIdentityDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddDataProtection()
                        .PersistKeysToDbContext<SplittIdentityDbContext>()
                        .SetApplicationName("SplittApplication");


        }
        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;

                //options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        public static void ConfigureSqlDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<SplittAppContext>(
                options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionStringSplitt")),
                ServiceLifetime.Transient);
        }

        private static void ConfigureCors(this WebApplicationBuilder builder)
        {
            ConfigurationManager configuration = builder.Configuration;
            var allowAnyOrigin = configuration.GetValue<bool>("AllowAnyOrigin");
            var getStr = configuration.GetSection("AllowedOriginUrls").Get<string[]>();
            if (allowAnyOrigin)
            {
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

                });
            }
            else if (getStr != null)
            {
                builder.Services.AddCors(options =>
                {

                    options.AddDefaultPolicy(builder =>
                    {
                        builder.WithOrigins(getStr)
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .SetIsOriginAllowed((x) => true)
                           .AllowCredentials();
                    });

                });
            }

        }
        static string GetUserEndPoint(HttpContext context) =>
   $"User {context.User.Identity?.Name ?? "Anonymous"} endpoint:{context.Request.Path}"
   + $" {context.Connection.RemoteIpAddress}";
        private static void ConfigureRateLimiter(this WebApplicationBuilder builder)
        {
            ConfigurationManager configuration = builder.Configuration;
            var Secure_PermitLimit = configuration.GetValue<int>("RateLimiterOptions:Secure_PermitLimit");
            var Secure_Window = configuration.GetValue<int>("RateLimiterOptions:Secure_Window_InSec");
            var Public_PermitLimit = configuration.GetValue<int>("RateLimiterOptions:Public_PermitLimit");
            var Public_Window = configuration.GetValue<int>("RateLimiterOptions:Public_Window_InSec");

            builder.Services.AddRateLimiter(options =>
            {
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    string rejectMessage = "";
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))// Noncompliant
                    {
                        rejectMessage = "Too many requests. Please try again later. ";
                        await context.HttpContext.Response.WriteAsync(rejectMessage, cancellationToken: token);
                    }
                    else
                    {
                        rejectMessage = "Too many requests. Please try again later. ";
                        await context.HttpContext.Response.WriteAsync(rejectMessage, cancellationToken: token);
                    }
                    context.HttpContext.RequestServices.GetService<ILoggerFactory>()?
                        .CreateLogger("Microsoft.AspNetCore.RateLimitingMiddleware").LogWarning(rejectMessage);
                };
                options.AddFixedWindowLimiter("Secure", options =>
                {
                    options.AutoReplenishment = true;
                    options.PermitLimit = Secure_PermitLimit;
                    ;

                    options.Window = TimeSpan.FromSeconds(Secure_Window);
                });
                options.AddFixedWindowLimiter("Public", options =>
                {
                    options.AutoReplenishment = true;
                    options.PermitLimit = Public_PermitLimit;

                    options.Window = TimeSpan.FromSeconds(Public_Window);
                });
            });
        }
        private static void ConfigureSwagger(this WebApplicationBuilder builder)
        {
            ConfigurationManager configuration = builder.Configuration;
            var allowSwagger = configuration.GetValue<bool>("AllowSwagger");
            if (allowSwagger)
            {
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(s =>
                {
                    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Splitt Merchant API", Version = "v1", });
                    s.OperationFilter<CustomHeaderSwaggerAttribute>();
                    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme."
                    });

                    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {    new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                    });

                });
            }
        }
        public static void ConfigureLoggerService(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerService, LoggerService>();
        }
        public static void ConfigureUnitOfWork(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IDapperDBConnection, DapperDBConnection>();
        }
        public static void ConfigureBusinessLogicServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddTransient<ILocalizationService, LocalizationService>();
            builder.Services.AddTransient<IUtilsService, UtilsService>();
            builder.Services.AddTransient<ILookupService, LookupService>();
            builder.Services.AddTransient<IMerchantService, MerchantService>();
            builder.Services.AddTransient<IAuthMerchantService, AuthMerchantService>();
            builder.Services.AddTransient<IAuthCustomerService, AuthCustomerService>();
            builder.Services.AddTransient<IUtilitiesService, UtilitiesService>();
            builder.Services.AddTransient<IMIDServiceAuthenticationService, MIDServiceAuthenticationService>();

        }
        public static void ConfigureLocalization(this WebApplicationBuilder builder)
        {
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.AddLocalization();
        }
        public static void ConfigureAppSettingsValue(this WebApplicationBuilder builder)
        {

            builder.WebHost.UseSetting(WebHostDefaults.ContentRootKey, Directory.GetCurrentDirectory());
            ConfigurationManager config = builder.Configuration;
            var contentRootTemplate = $"{builder.Environment.WebRootPath}\\Template";
            Utilities.AnonymousUserID = builder.Configuration.GetValue<Guid>("AnonymousUserID");

        }
        public static void ConfigureSignalR(this WebApplicationBuilder builder)
        {
            //builder.Services.AddSignalR();
        }
        #endregion
    }
}
