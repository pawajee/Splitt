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

    public virtual DbSet<Country> Country { get; set; }

    public virtual DbSet<DocumentCategory> DocumentCategory { get; set; }

    public virtual DbSet<DocumentConfiguration> DocumentConfiguration { get; set; }

    public virtual DbSet<DocumentLibrary> DocumentLibrary { get; set; }

    public virtual DbSet<Gender> Gender { get; set; }

    public virtual DbSet<Language> Language { get; set; }

    public virtual DbSet<Location> Location { get; set; }

    public virtual DbSet<MerchantAnnualSale> MerchantAnnualSale { get; set; }

    public virtual DbSet<MerchantAverageOrder> MerchantAverageOrder { get; set; }

    public virtual DbSet<MerchantBusinessType> MerchantBusinessType { get; set; }

    public virtual DbSet<MerchantCategory> MerchantCategory { get; set; }

    public virtual DbSet<MerchantRequest> MerchantRequest { get; set; }

    public virtual DbSet<MerchantRequestAttachment> MerchantRequestAttachment { get; set; }

    public virtual DbSet<MerchantRequestHistory> MerchantRequestHistory { get; set; }

    public virtual DbSet<MerchantRequestStatus> MerchantRequestStatus { get; set; }

    public virtual DbSet<MerchantUser> MerchantUser { get; set; }

    public virtual DbSet<Nationality> Nationality { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserType> UserType { get; set; }

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
            entity.Property(e => e.Id).ValueGeneratedNever();
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

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<DocumentCategory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BaseUrl).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<DocumentConfiguration>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.DescriptionArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.DescriptionEnglish).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.SupportedMineType).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.DocumentCategory).WithMany(p => p.DocumentConfiguration)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentConfiguration_DocumentCategory");
        });

        modelBuilder.Entity<DocumentLibrary>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FileName).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.MineType).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.DocumentCategory).WithMany(p => p.DocumentLibrary).HasConstraintName("FK_DocumentLibrary_DocumentCategory");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<MerchantAnnualSale>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<MerchantAverageOrder>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<MerchantBusinessType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<MerchantCategory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<MerchantRequest>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BusinessNameArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.BusinessNameEnglish).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.RequestNo).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.Country).WithMany(p => p.MerchantRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequest_Country");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.MerchantRequestCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequest_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MerchantRequestCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequest_User");

            entity.HasOne(d => d.MerchantAnnualSales).WithMany(p => p.MerchantRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequest_MerchantAnnualSale");

            entity.HasOne(d => d.MerchantAverageOrder).WithMany(p => p.MerchantRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequest_MerchantAverageOrder");

            entity.HasOne(d => d.MerchantBusinessType).WithMany(p => p.MerchantRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequest_MerchantBusinessType");

            entity.HasOne(d => d.MerchantCategory).WithMany(p => p.MerchantRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequest_MerchantCategory");

            entity.HasOne(d => d.MerchantRequestStatus).WithMany(p => p.MerchantRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequest_MerchantRequestStatus");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.MerchantRequestModifiedAtNavigation).HasConstraintName("FK_MerchantRequest_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MerchantRequestModifiedByNavigation).HasConstraintName("FK_MerchantRequest_User1");
        });

        modelBuilder.Entity<MerchantRequestAttachment>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.MerchantRequestAttachmentCreatedAtNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequestAttachment_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MerchantRequestAttachmentCreatedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequestAttachment_User");

            entity.HasOne(d => d.DocumentConfiguration).WithMany(p => p.MerchantRequestAttachment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequestAttachment_DocumentConfiguration");

            entity.HasOne(d => d.DocumentLibrary).WithMany(p => p.MerchantRequestAttachment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequestAttachment_DocumentLibrary");

            entity.HasOne(d => d.MerchantRequest).WithMany(p => p.MerchantRequestAttachment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequestAttachment_MerchantRequest");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.MerchantRequestAttachmentModifiedAtNavigation).HasConstraintName("FK_MerchantRequestAttachment_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MerchantRequestAttachmentModifiedByNavigation).HasConstraintName("FK_MerchantRequestAttachment_User1");
        });

        modelBuilder.Entity<MerchantRequestHistory>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Comment).UseCollation("Latin1_General_CI_AS");

            entity.HasOne(d => d.CreatedAtNavigation).WithMany(p => p.MerchantRequestHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequestHistory_Location");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MerchantRequestHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequestHistory_User");

            entity.HasOne(d => d.MerchantRequest).WithMany(p => p.MerchantRequestHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequestHistory_MerchantRequest");

            entity.HasOne(d => d.MerchantRequestStatus).WithMany(p => p.MerchantRequestHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantRequestHistory_MerchantRequestStatus");
        });

        modelBuilder.Entity<MerchantRequestStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
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
                .HasConstraintName("FK_MerchantUser_MerchantContactRequest");

            entity.HasOne(d => d.ModifiedAtNavigation).WithMany(p => p.MerchantUserModifiedAtNavigation).HasConstraintName("FK_MerchantUser_Location1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MerchantUserModifiedByNavigation).HasConstraintName("FK_MerchantUser_User1");

            entity.HasOne(d => d.User).WithMany(p => p.MerchantUserUser).HasConstraintName("FK_MerchantUser_User2");
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IsActive).HasDefaultValue(false);
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

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleArabic).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.TitleEnglish).UseCollation("Latin1_General_CI_AS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
