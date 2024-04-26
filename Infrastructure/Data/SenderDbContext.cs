using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class SenderDbContext : DbContext
{
    public SenderDbContext()
    {
    }

    public SenderDbContext(DbContextOptions<SenderDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyRecoveryToken> CompanyRecoveryTokens { get; set; }

    public virtual DbSet<CompanyRefreshToken> CompanyRefreshTokens { get; set; }

    public virtual DbSet<CompanyRole> CompanyRoles { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sending> Sendings { get; set; }

    public virtual DbSet<SendingCategory> SendingCategories { get; set; }

    public virtual DbSet<SendingContent> SendingContents { get; set; }

    public virtual DbSet<SendingReview> SendingReviews { get; set; }

    public virtual DbSet<SendingScore> SendingScores { get; set; }

    public virtual DbSet<SendingTime> SendingTimes { get; set; }

    public virtual DbSet<SendingType> SendingTypes { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserRecoveryToken> UserRecoveryTokens { get; set; }

    public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserSending> UserSendings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SenderDb;Username=postgres;Password=G525mc23!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("company_pkey");

            entity.ToTable("company");

            entity.HasIndex(e => e.Email, "company_email_key").IsUnique();

            entity.HasIndex(e => e.Name, "company_name_key").IsUnique();

            entity.HasIndex(e => e.Salt, "company_salt_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .HasColumnName("hashed_password");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Salt)
                .HasMaxLength(1024)
                .HasColumnName("salt");
        });

        modelBuilder.Entity<CompanyRecoveryToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("company_recovery_token_pkey");

            entity.ToTable("company_recovery_token");

            entity.HasIndex(e => e.Token, "company_recovery_token_token_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .HasColumnName("ip_address");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(150)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyRecoveryTokens)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("company_recovery_token_company_id_fkey");
        });

        modelBuilder.Entity<CompanyRefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("company_refresh_token_pkey");

            entity.ToTable("company_refresh_token");

            entity.HasIndex(e => e.Token, "company_refresh_token_token_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyRefreshTokens)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("company_refresh_token_company_id_fkey");
        });

        modelBuilder.Entity<CompanyRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("company_role_pkey");

            entity.ToTable("company_role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyRoles)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("company_role_company_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.CompanyRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("company_role_role_id_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("review_pkey");

            entity.ToTable("review");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasMaxLength(1024)
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("review_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "role_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Sending>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sending_pkey");

            entity.ToTable("sending");

            entity.HasIndex(e => e.Name, "sending_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.SendingCategoryId).HasColumnName("sending_category_id");
            entity.Property(e => e.SendingTimeId).HasColumnName("sending_time_id");
            entity.Property(e => e.SendingTypeId).HasColumnName("sending_type_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Sendings)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sending_company_id_fkey");

            entity.HasOne(d => d.SendingCategory).WithMany(p => p.Sendings)
                .HasForeignKey(d => d.SendingCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sending_sending_category_id_fkey");

            entity.HasOne(d => d.SendingTime).WithMany(p => p.Sendings)
                .HasForeignKey(d => d.SendingTimeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sending_sending_time_id_fkey");

            entity.HasOne(d => d.SendingType).WithMany(p => p.Sendings)
                .HasForeignKey(d => d.SendingTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sending_sending_type_id_fkey");
        });

        modelBuilder.Entity<SendingCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sending_category_pkey");

            entity.ToTable("sending_category");

            entity.HasIndex(e => e.Name, "sending_category_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SendingContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sending_content_pkey");

            entity.ToTable("sending_content");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.IsApproved).HasColumnName("is_approved");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.SendingId).HasColumnName("sending_id");
            entity.Property(e => e.SengingDate).HasColumnName("senging_date");

            entity.HasOne(d => d.Sending).WithMany(p => p.SendingContents)
                .HasForeignKey(d => d.SendingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sending_content_sending_id_fkey");
        });

        modelBuilder.Entity<SendingReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sending_review_pkey");

            entity.ToTable("sending_review");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.SendingId).HasColumnName("sending_id");

            entity.HasOne(d => d.Review).WithMany(p => p.SendingReviews)
                .HasForeignKey(d => d.ReviewId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sending_review_review_id_fkey");

            entity.HasOne(d => d.Sending).WithMany(p => p.SendingReviews)
                .HasForeignKey(d => d.SendingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sending_review_sending_id_fkey");
        });

        modelBuilder.Entity<SendingScore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sending_score_pkey");

            entity.ToTable("sending_score");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrentSubscriber).HasColumnName("current_subscriber");
            entity.Property(e => e.ReviewScore)
                .HasPrecision(3, 2)
                .HasColumnName("review_score");
            entity.Property(e => e.SendingId).HasColumnName("sending_id");

            entity.HasOne(d => d.Sending).WithMany(p => p.SendingScores)
                .HasForeignKey(d => d.SendingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sending_score_sending_id_fkey");
        });

        modelBuilder.Entity<SendingTime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sending_time_pkey");

            entity.ToTable("sending_time");

            entity.HasIndex(e => e.SendTime, "sending_time_send_time_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SendTime).HasColumnName("send_time");
        });

        modelBuilder.Entity<SendingType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sending_type_pkey");

            entity.ToTable("sending_type");

            entity.HasIndex(e => e.Name, "sending_type_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_info_pkey");

            entity.ToTable("user_info");

            entity.HasIndex(e => e.Email, "user_info_email_key").IsUnique();

            entity.HasIndex(e => e.Salt, "user_info_salt_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .HasColumnName("hashed_password");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Salt)
                .HasMaxLength(1024)
                .HasColumnName("salt");
        });

        modelBuilder.Entity<UserRecoveryToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_recovery_token_pkey");

            entity.ToTable("user_recovery_token");

            entity.HasIndex(e => e.Token, "user_recovery_token_token_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .HasColumnName("ip_address");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(150)
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserRecoveryTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_recovery_token_user_id_fkey");
        });

        modelBuilder.Entity<UserRefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_refresh_token_pkey");

            entity.ToTable("user_refresh_token");

            entity.HasIndex(e => e.Token, "user_refresh_token_token_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserRefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_refresh_token_user_id_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_role_pkey");

            entity.ToTable("user_role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_role_role_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_role_user_id_fkey");
        });

        modelBuilder.Entity<UserSending>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_sending_pkey");

            entity.ToTable("user_sending");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SendingId).HasColumnName("sending_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Sending).WithMany(p => p.UserSendings)
                .HasForeignKey(d => d.SendingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_sending_sending_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserSendings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_sending_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
