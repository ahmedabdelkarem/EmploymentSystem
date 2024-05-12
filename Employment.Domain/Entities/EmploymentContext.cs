using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Employment.Domain.Entities;

public partial class EmploymentContext : DbContext
{
    public EmploymentContext()
    {
    }

    public EmploymentContext(DbContextOptions<EmploymentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<VacanciesApplication> VacanciesApplications { get; set; }

    public virtual DbSet<VacanciesApplicationsArc> VacanciesApplicationsArcs { get; set; }

    public virtual DbSet<VacanciesArc> VacanciesArcs { get; set; }

    public virtual DbSet<Vacancy> Vacancies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;Database=Employment");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ConcurrencyStamp)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedName)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.Property(e => e.ClaimType)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClaimValue)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ConcurrencyStamp)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedEmail)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedUserName)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SecurityStamp)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        j.IndexerProperty<string>("UserId")
                            .HasMaxLength(100)
                            .IsUnicode(false);
                        j.IndexerProperty<string>("RoleId")
                            .HasMaxLength(100)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.Property(e => e.ClaimType)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClaimValue)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.ProviderKey)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.ProviderDisplayName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LoginProvider)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<VacanciesApplication>(entity =>
        {
            entity.HasIndex(e => e.FkApplicantId, "IX_VacanciesApplications_Fk_ApplicantId");

            entity.HasIndex(e => e.FkVacancyId, "IX_VacanciesApplications_Fk_VacancyId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.FkApplicantId)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Fk_ApplicantId");
            entity.Property(e => e.FkVacancyId).HasColumnName("Fk_VacancyId");

            entity.HasOne(d => d.FkApplicant).WithMany(p => p.VacanciesApplications)
                .HasForeignKey(d => d.FkApplicantId)
                .HasConstraintName("FK_VacanciesApplications_AspNetUsers");

            entity.HasOne(d => d.FkVacancy).WithMany(p => p.VacanciesApplications)
                .HasForeignKey(d => d.FkVacancyId)
                .HasConstraintName("FK_VacanciesApplications_Vacancies");
        });

        modelBuilder.Entity<VacanciesApplicationsArc>(entity =>
        {
            entity.ToTable("VacanciesApplications_ARC");

            entity.HasIndex(e => e.FkApplicantId, "IX_VacanciesApplications_ARC_Fk_ApplicantId");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.FkApplicantId)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Fk_ApplicantId");
            entity.Property(e => e.FkVacancyId).HasColumnName("Fk_VacancyId");

            entity.HasOne(d => d.FkApplicant).WithMany(p => p.VacanciesApplicationsArcs)
                .HasForeignKey(d => d.FkApplicantId)
                .HasConstraintName("FK_VacanciesApplications_AspNetUsers_ARC");
        });

        modelBuilder.Entity<VacanciesArc>(entity =>
        {
            entity.ToTable("Vacancies_ARC");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.VacancyName)
                .IsRequired()
                .HasMaxLength(200);
        });

        modelBuilder.Entity<Vacancy>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CurrentNumberOfApplication).HasDefaultValueSql("((0))");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.VacancyName)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Vacancies_AspNetUsers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
