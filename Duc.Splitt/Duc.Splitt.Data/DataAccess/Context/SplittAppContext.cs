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

    public virtual DbSet<BackOfficeUser> BackOfficeUser { get; set; }

    public virtual DbSet<ConsumerOtpRequest> ConsumerOtpRequest { get; set; }

    public virtual DbSet<ConsumerUser> ConsumerUser { get; set; }

    public virtual DbSet<DocumentLibrary> DocumentLibrary { get; set; }

    public virtual DbSet<LkCountry> LkCountry { get; set; }

    public virtual DbSet<LkDocumentCategory> LkDocumentCategory { get; set; }

    public virtual DbSet<LkDocumentConfiguration> LkDocumentConfiguration { get; set; }

    public virtual DbSet<LkGender> LkGender { get; set; }

    public virtual DbSet<LkLanguage> LkLanguage { get; set; }

    public virtual DbSet<LkLocation> LkLocation { get; set; }

    public virtual DbSet<LkMerchantAnnualSale> LkMerchantAnnualSale { get; set; }

    public virtual DbSet<LkMerchantAverageOrder> LkMerchantAverageOrder { get; set; }

    public virtual DbSet<LkMerchantBusinessType> LkMerchantBusinessType { get; set; }

    public virtual DbSet<LkMerchantCategory> LkMerchantCategory { get; set; }

    public virtual DbSet<LkMerchantStatus> LkMerchantStatus { get; set; }

    public virtual DbSet<LkNationality> LkNationality { get; set; }

    public virtual DbSet<LkNotificationCategory> LkNotificationCategory { get; set; }

    public virtual DbSet<LkNotificationChannel> LkNotificationChannel { get; set; }

    public virtual DbSet<LkNotificationStatus> LkNotificationStatus { get; set; }

    public virtual DbSet<LkNotificationTemplate> LkNotificationTemplate { get; set; }

    public virtual DbSet<LkNotificationType> LkNotificationType { get; set; }

    public virtual DbSet<LkRole> LkRole { get; set; }

    public virtual DbSet<Merchant> Merchant { get; set; }

    public virtual DbSet<MerchantAttachment> MerchantAttachment { get; set; }

    public virtual DbSet<MerchantHistory> MerchantHistory { get; set; }

    public virtual DbSet<MerchantUser> MerchantUser { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BackOfficeUser>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.BackOfficeUserCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackOfficeUser_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BackOfficeUserCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackOfficeUser_User1");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.BackOfficeUserModifiedAtNavigation).HasConstraintName("FK_BackOfficeUser_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BackOfficeUserModifiedByNavigation).HasConstraintName("FK_BackOfficeUser_User2");

            entity.HasOne(d => d.User).WithMany(p => p.BackOfficeUserUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BackOfficeUser_User");
        });

        modelBuilder.Entity<ConsumerOtpRequest>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ExpiredOn).HasDefaultValueSql("(dateadd(minute,(5),getdate()))");
        });

        modelBuilder.Entity<ConsumerUser>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.MobileNo).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.NameArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.NameEnglish).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.ConsumerUserCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumerUser_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ConsumerUserCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumerUser_User1");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.ConsumerUserModifiedAtNavigation).HasConstraintName("FK_ConsumerUser_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ConsumerUserModifiedByNavigation).HasConstraintName("FK_ConsumerUser_User2");

            entity.HasOne(d => d.User).WithMany(p => p.ConsumerUserUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumerUser_User");
        });

        modelBuilder.Entity<DocumentLibrary>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FileName).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.MineType).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.DocumentCategory).WithMany(p => p.DocumentLibrary).HasConstraintName("FK_DocumentLibrary_DocumentCategory");
        });

        modelBuilder.Entity<LkCountry>(entity =>
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

        modelBuilder.Entity<LkGender>(entity =>
        {
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkLanguage>(entity =>
        {
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkLocation>(entity =>
        {
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

        modelBuilder.Entity<LkNationality>(entity =>
        {
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<LkNotificationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NotificationStatus");
        });

        modelBuilder.Entity<LkNotificationTemplate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.LkNotificationTemplateCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NLkotificationTemplate_LkLocation");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.LkNotificationTemplateModifiedAtNavigation).HasConstraintName("FK_LkNotificationTemplate_LkLocation1");
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
            entity.Property(e => e.BusinessNameArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.BusinessNameEnglish).UseCollation("Latin1_General_CI_AS");
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

            entity.HasOne(d => d.DocumentLibrary).WithMany(p => p.MerchantAttachment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantAttachment_DocumentLibrary");

            entity.HasOne(d => d.MerchantRequest).WithMany(p => p.MerchantAttachment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantAttachment_Merchant");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.MerchantAttachmentModifiedAtNavigation).HasConstraintName("FK_MerchantAttachment_LkLocation1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MerchantAttachmentModifiedByNavigation).HasConstraintName("FK_MerchantAttachment_User1");
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

        modelBuilder.Entity<MerchantUser>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BusinessEmail).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.MobileNo).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.NameArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.NameEnglish).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.MerchantUserCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantUser_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MerchantUserCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantUser_User");

            entity.HasOne(d => d.MerchantRequest).WithMany(p => p.MerchantUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantUser_Merchant");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.MerchantUserModifiedAtNavigation).HasConstraintName("FK_MerchantUser_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MerchantUserModifiedByNavigation).HasConstraintName("FK_MerchantUser_User1");

            entity.HasOne(d => d.User).WithMany(p => p.MerchantUserUser).HasConstraintName("FK_MerchantUser_User2");
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
