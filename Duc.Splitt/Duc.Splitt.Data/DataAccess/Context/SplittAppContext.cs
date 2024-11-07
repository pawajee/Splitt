using System;
using System.Collections.Generic;
using Duc.Splitt.Data.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Context;

public partial class SplittAppContext : DbContext
{
    public SplittAppContext(DbContextOptions<SplittAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }

    public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

    public virtual DbSet<BackOfficeUser> BackOfficeUser { get; set; }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<CustomerRegistrationRequest> CustomerRegistrationRequest { get; set; }

    public virtual DbSet<DataProtectionKeys> DataProtectionKeys { get; set; }

    public virtual DbSet<DocumentLibrary> DocumentLibrary { get; set; }

    public virtual DbSet<EmailNotification> EmailNotification { get; set; }

    public virtual DbSet<LkCountry> LkCountry { get; set; }

    public virtual DbSet<LkCurrency> LkCurrency { get; set; }

    public virtual DbSet<LkCustomerRegistrationStatus> LkCustomerRegistrationStatus { get; set; }

    public virtual DbSet<LkCustomerStatus> LkCustomerStatus { get; set; }

    public virtual DbSet<LkDocumentCategory> LkDocumentCategory { get; set; }

    public virtual DbSet<LkDocumentConfiguration> LkDocumentConfiguration { get; set; }

    public virtual DbSet<LkEducationalLevel> LkEducationalLevel { get; set; }

    public virtual DbSet<LkEmploymentSector> LkEmploymentSector { get; set; }

    public virtual DbSet<LkEmploymentStatus> LkEmploymentStatus { get; set; }

    public virtual DbSet<LkGender> LkGender { get; set; }

    public virtual DbSet<LkInstallmentType> LkInstallmentType { get; set; }

    public virtual DbSet<LkLanguage> LkLanguage { get; set; }

    public virtual DbSet<LkLocation> LkLocation { get; set; }

    public virtual DbSet<LkMartialStatus> LkMartialStatus { get; set; }

    public virtual DbSet<LkMerchantAnnualSale> LkMerchantAnnualSale { get; set; }

    public virtual DbSet<LkMerchantAverageOrder> LkMerchantAverageOrder { get; set; }

    public virtual DbSet<LkMerchantBusinessType> LkMerchantBusinessType { get; set; }

    public virtual DbSet<LkMerchantCategory> LkMerchantCategory { get; set; }

    public virtual DbSet<LkMerchantStatus> LkMerchantStatus { get; set; }

    public virtual DbSet<LkMidRequestStatus> LkMidRequestStatus { get; set; }

    public virtual DbSet<LkMidRequestType> LkMidRequestType { get; set; }

    public virtual DbSet<LkNationality> LkNationality { get; set; }

    public virtual DbSet<LkNotificationCategory> LkNotificationCategory { get; set; }

    public virtual DbSet<LkNotificationPriority> LkNotificationPriority { get; set; }

    public virtual DbSet<LkNotificationStatus> LkNotificationStatus { get; set; }

    public virtual DbSet<LkNotificationTemplate> LkNotificationTemplate { get; set; }

    public virtual DbSet<LkNotificationType> LkNotificationType { get; set; }

    public virtual DbSet<LkOrderStatus> LkOrderStatus { get; set; }

    public virtual DbSet<LkOtpPurpose> LkOtpPurpose { get; set; }

    public virtual DbSet<LkPaymentBrandType> LkPaymentBrandType { get; set; }

    public virtual DbSet<LkPaymentOption> LkPaymentOption { get; set; }

    public virtual DbSet<LkPaymentRequestType> LkPaymentRequestType { get; set; }

    public virtual DbSet<LkPaymentStatus> LkPaymentStatus { get; set; }

    public virtual DbSet<LkRole> LkRole { get; set; }

    public virtual DbSet<Merchant> Merchant { get; set; }

    public virtual DbSet<MerchantAttachment> MerchantAttachment { get; set; }

    public virtual DbSet<MerchantContact> MerchantContact { get; set; }

    public virtual DbSet<MerchantHistory> MerchantHistory { get; set; }

    public virtual DbSet<MidRequestLog> MidRequestLog { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderItem> OrderItem { get; set; }

    public virtual DbSet<OtpRequest> OtpRequest { get; set; }

    public virtual DbSet<Payment> Payment { get; set; }

    public virtual DbSet<PaymentInstallment> PaymentInstallment { get; set; }

    public virtual DbSet<PrePayment> PrePayment { get; set; }

    public virtual DbSet<PushNotification> PushNotification { get; set; }

    public virtual DbSet<SmsNotification> SmsNotification { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRoles>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<AspNetUsers>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Role).WithMany(p => p.User)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRoles",
                    r => r.HasOne<AspNetRoles>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUsers>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<BackOfficeUser>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.BackOfficeUserCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackOfficeUser_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BackOfficeUserCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackOfficeUser_User1");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.BackOfficeUserModifiedAtNavigation).HasConstraintName("FK_BackOfficeUser_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BackOfficeUserModifiedByNavigation).HasConstraintName("FK_BackOfficeUser_User2");

            entity.HasOne(d => d.User).WithOne(p => p.BackOfficeUserUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackOfficeUser_User");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.MobileNo).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.PaciNameArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.PaciNameEnglish).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.CustomerCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_User1");

            entity.HasOne(d => d.CustomerRegistrationRequest).WithMany(p => p.Customer).HasConstraintName("FK_Customer_CustomerRegistrationRequest");

            entity.HasOne(d => d.CustomerStatus).WithMany(p => p.Customer).HasConstraintName("FK_Customer_LkCustomerStatus");

            entity.HasOne(d => d.EducationalLevel).WithMany(p => p.Customer).HasConstraintName("FK_Customer_LkEducationalLevel");

            entity.HasOne(d => d.EmploymentSector).WithMany(p => p.Customer).HasConstraintName("FK_Customer_LkEmploymentSector");

            entity.HasOne(d => d.EmploymentStatus).WithMany(p => p.Customer).HasConstraintName("FK_Customer_LkEmploymentStatus");

            entity.HasOne(d => d.Gender).WithMany(p => p.Customer).HasConstraintName("FK_Customer_LkGender");

            entity.HasOne(d => d.MartialStatus).WithMany(p => p.Customer).HasConstraintName("FK_Customer_LkMartialStatus");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.CustomerModifiedAtNavigation).HasConstraintName("FK_Customer_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.CustomerModifiedByNavigation).HasConstraintName("FK_Customer_User2");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Customer).HasConstraintName("FK_Customer_LkNationality");

            entity.HasOne(d => d.User).WithOne(p => p.CustomerUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_User");
        });

        modelBuilder.Entity<CustomerRegistrationRequest>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.CustomerRegistrationRequestCreatedAtNavigation).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerRegistrationRequestCreatedByNavigation).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CustomerRegistrationStatus).WithMany(p => p.CustomerRegistrationRequest).HasConstraintName("FK_CustomerRegistrationRequest_LkCustomerRegistrationStatus");

            entity.HasOne(d => d.OtpRequest).WithMany(p => p.CustomerRegistrationRequest).HasConstraintName("FK_CustomerRegistrationRequest_CustomerRegistrationRequest");
        });

        modelBuilder.Entity<DocumentLibrary>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FileName).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.MineType).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.DocumentCategory).WithMany(p => p.DocumentLibrary).HasConstraintName("FK_DocumentLibrary_DocumentCategory");
        });

        modelBuilder.Entity<EmailNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmailNotification_1");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.EmailNotificationCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailNotification_LkLocation");

            entity.HasOne(d => d.Language).WithMany(p => p.EmailNotification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailNotification_LkLanguage");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.EmailNotificationModifiedAtNavigation).HasConstraintName("FK_EmailNotification_LkLocation1");

            entity.HasOne(d => d.NotificationStatus).WithMany(p => p.EmailNotification).HasConstraintName("FK_EmailNotification_LkNotificationStatus");

            entity.HasOne(d => d.NotificationTemplate).WithMany(p => p.EmailNotification).HasConstraintName("FK_EmailNotification_LkNotificationTemplate");

            entity.HasOne(d => d.Priority).WithMany(p => p.EmailNotification).HasConstraintName("FK_EmailNotification_LkPriority");
        });

        modelBuilder.Entity<LkCountry>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkCurrency>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkCustomerRegistrationStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkCustomerStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkDocumentCategory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BaseUrl).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkDocumentConfiguration>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.DescriptionArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.DescriptionEnglish).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.SupportedMineType).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.DocumentCategory).WithMany(p => p.LkDocumentConfiguration)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LkDocumentConfiguration_LkDocumentCategory");
        });

        modelBuilder.Entity<LkEducationalLevel>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkEmploymentSector>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkEmploymentStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LkEmploymentStatus_1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkGender>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkInstallmentType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkLanguage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkLocation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkMartialStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkMerchantAnnualSale>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkMerchantAverageOrder>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkMerchantBusinessType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkMerchantCategory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkMerchantStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkMidRequestStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkMidRequestType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkNationality>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkNotificationCategory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LkNotificationPriority>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LkNotificationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NotificationStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LkNotificationTemplate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.NotificationCategory).WithMany(p => p.LkNotificationTemplate).HasConstraintName("FK_LkNotificationTemplate_LkNotificationCategory");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.LkNotificationTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LkNotificationTemplate_LkNotificationType");
        });

        modelBuilder.Entity<LkNotificationType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LkOrderStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkOtpPurpose>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkPaymentBrandType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkPaymentOption>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkPaymentRequestType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkPaymentStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkRole>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BusinessEmail).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.BusinessNameArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.BusinessNameEnglish).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.MobileNo).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.RequestNo).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.Country).WithMany(p => p.Merchant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merchant_LkCountry");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.MerchantCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merchant_LkLocation");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MerchantCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merchant_User");

            entity.HasOne(d => d.MerchantAnnualSales).WithMany(p => p.Merchant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merchant_LKMerchantAnnualSale");

            entity.HasOne(d => d.MerchantAverageOrder).WithMany(p => p.Merchant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merchant_LKMerchantAverageOrder");

            entity.HasOne(d => d.MerchantBusinessType).WithMany(p => p.Merchant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merchant_LKMerchantBusinessType");

            entity.HasOne(d => d.MerchantCategory).WithMany(p => p.Merchant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merchant_LKMerchantCategory");

            entity.HasOne(d => d.MerchantStatus).WithMany(p => p.Merchant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merchant_LkMerchantStatus");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.MerchantModifiedAtNavigation).HasConstraintName("FK_Merchant_LkLocation1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MerchantModifiedByNavigation).HasConstraintName("FK_Merchant_User1");
        });

        modelBuilder.Entity<MerchantAttachment>(entity =>
        {
            entity.Property(e => e.DocumentLibraryId).ValueGeneratedNever();
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.MerchantAttachmentCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantAttachment_LkLocation");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MerchantAttachmentCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantAttachment_User");

            entity.HasOne(d => d.DocumentConfiguration).WithMany(p => p.MerchantAttachment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantAttachment_LkDocumentConfiguration");

            entity.HasOne(d => d.DocumentLibrary).WithOne(p => p.MerchantAttachment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantAttachment_DocumentLibrary");

            entity.HasOne(d => d.MerchantRequest).WithMany(p => p.MerchantAttachment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantAttachment_Merchant");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.MerchantAttachmentModifiedAtNavigation).HasConstraintName("FK_MerchantAttachment_LkLocation1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MerchantAttachmentModifiedByNavigation).HasConstraintName("FK_MerchantAttachment_User1");
        });

        modelBuilder.Entity<MerchantContact>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BusinessEmail).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.MobileNo).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.NameArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.NameEnglish).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.MerchantContactCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantContact_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MerchantContactCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantContact_User");

            entity.HasOne(d => d.MerchantRequest).WithMany(p => p.MerchantContact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantContact_Merchant");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.MerchantContactModifiedAtNavigation).HasConstraintName("FK_MerchantContact_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MerchantContactModifiedByNavigation).HasConstraintName("FK_MerchantContact_User1");

            entity.HasOne(d => d.User).WithOne(p => p.MerchantContactUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantContact_User2");
        });

        modelBuilder.Entity<MerchantHistory>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Comment).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.MerchantHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantHistory_LkLocation");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MerchantHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantHistory_User");

            entity.HasOne(d => d.MerchantRequest).WithMany(p => p.MerchantHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantHistory_Merchant");

            entity.HasOne(d => d.MerchantRequestStatus).WithMany(p => p.MerchantHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantHistory_LkMerchantStatus");
        });

        modelBuilder.Entity<MidRequestLog>(entity =>
        {
            entity.HasOne(d => d.CustomerRegistrationRequest).WithMany(p => p.MidRequestLog).HasConstraintName("FK_MidRequestLog_CustomerRegistrationRequest");

            entity.HasOne(d => d.MidRequestStatus).WithMany(p => p.MidRequestLog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MidRequestLog_LkMidRequestStatus");

            entity.HasOne(d => d.MidRequestType).WithMany(p => p.MidRequestLog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MidRequestLog_LkMidRequestType");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CurrencyId).HasDefaultValue(1);
            entity.Property(e => e.OrderStatusId).HasDefaultValue(1);

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.OrderCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_LkLocation");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OrderCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");

            entity.HasOne(d => d.Currency).WithMany(p => p.Order).HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.Merchant).WithMany(p => p.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Merchant");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.OrderModifiedAtNavigation).HasConstraintName("FK_Order_LkLocation1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.OrderModifiedByNavigation).HasConstraintName("FK_Order_User1");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Order).HasConstraintName("FK_Order_OLkOrderStatus");

            entity.HasOne(d => d.PaymentOption).WithMany(p => p.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_LkPaymentOption");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItem).HasConstraintName("FK_OrderItem_Order");
        });

        modelBuilder.Entity<OtpRequest>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ExpiredOn).HasDefaultValueSql("(dateadd(minute,(5),getdate()))");

            entity.HasOne(d => d.OtpPurpose).WithMany(p => p.OtpRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OtpRequest_LkOtpPurpose");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.PaymentRecepitDocumentLibrary).WithMany(p => p.Payment).HasConstraintName("FK_Payment_DocumentLibrary");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.Payment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_LkPaymentStatus");

            entity.HasOne(d => d.PrePayment).WithMany(p => p.Payment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_PrePayment");

            entity.HasOne(d => d.User).WithMany(p => p.Payment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_User");
        });

        modelBuilder.Entity<PaymentInstallment>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PaymentInstallmentCreatedByNavigation).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.InstallmentType).WithMany(p => p.PaymentInstallment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentInstallment_InstallmentType");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PaymentInstallmentModifiedByNavigation).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Order).WithMany(p => p.PaymentInstallment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentInstallment_Order1");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.PaymentInstallment).HasConstraintName("FK_PaymentInstallment_LkPaymentStatus");
        });

        modelBuilder.Entity<PrePayment>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PrePaymentCreatedByNavigation).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PrePaymentModifiedByNavigation).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PaymentBrand).WithMany(p => p.PrePayment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrePayment_LkPaymentBrandType");

            entity.HasOne(d => d.PaymentRequestType).WithMany(p => p.PrePayment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrePayment_LkPaymentRequestType");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.PrePayment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrePayment_LkPaymentStatus");

            entity.HasOne(d => d.User).WithMany(p => p.PrePaymentUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrePayment_User");
        });

        modelBuilder.Entity<PushNotification>(entity =>
        {
            entity.HasOne(d => d.NotificationStatus).WithMany(p => p.PushNotification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PushNotification_LkNotificationStatus");

            entity.HasOne(d => d.NotificationTemplate).WithMany(p => p.PushNotification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PushNotification_LkNotificationTemplate");

            entity.HasOne(d => d.Priority).WithMany(p => p.PushNotification).HasConstraintName("FK_PushNotification_LkNotificationPriority");
        });

        modelBuilder.Entity<SmsNotification>(entity =>
        {
            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.SmsNotificationCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SmsNotification_LkLocation");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SmsNotificationCreatedByNavigation).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Language).WithMany(p => p.SmsNotification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SmsNotification_LkLanguage");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.SmsNotificationModifiedAtNavigation).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SmsNotificationModifiedByNavigation).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.NotificationStatus).WithMany(p => p.SmsNotification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SmsNotification_LkNotificationStatus");

            entity.HasOne(d => d.NotificationTemplate).WithMany(p => p.SmsNotification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SmsNotification_LkNotificationTemplate");

            entity.HasOne(d => d.Priority).WithMany(p => p.SmsNotification).HasConstraintName("FK_SmsNotification_LkPriority");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.IsSystemAccount).HasDefaultValue(false);
            entity.Property(e => e.LoginId).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.UserTypeId).HasDefaultValue(5);

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.UserCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_User");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.UserModifiedAtNavigation).HasConstraintName("FK_User_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InverseModifiedByNavigation).HasConstraintName("FK_User_User1");

            entity.HasOne(d => d.UserType).WithMany(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
