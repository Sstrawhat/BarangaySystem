using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.Models
{
    public partial class TEST_DBContext : DbContext
    {
        public virtual DbSet<EducationalBackground> EducationalBackgrounds { get; set; }
        public virtual DbSet<EmploymentInformation> EmploymentInformations { get; set; }
        public virtual DbSet<EnumValue> EnumValues { get; set; }
        public virtual DbSet<ResidentProfile> ResidentProfiles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<TemplateTable> TemplateTables { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        public TEST_DBContext()
        {
        }

        public TEST_DBContext(DbContextOptions<TEST_DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationalBackground>(entity =>
            {
                entity.ToTable("EducationalBackground", "residence");

                entity.Property(e => e.Course).HasMaxLength(150);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.SchoolName).HasMaxLength(150);

                entity.Property(e => e.YearCompleted).HasMaxLength(50);
            });

            modelBuilder.Entity<EmploymentInformation>(entity =>
            {
                entity.ToTable("EmploymentInformation", "residence");

                entity.Property(e => e.CompanyAddress).HasMaxLength(150);

                entity.Property(e => e.CompanyName).HasMaxLength(150);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Duration).HasMaxLength(200);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Occupation).HasMaxLength(150);
            });

            modelBuilder.Entity<EnumValue>(entity =>
            {
                entity.ToTable("EnumValues", "maintenance");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).HasMaxLength(150);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Source).HasMaxLength(150);
            });

            modelBuilder.Entity<ResidentProfile>(entity =>
            {
                entity.ToTable("ResidentProfile", "residence");

                entity.Property(e => e.BirthPlace).HasMaxLength(200);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.Firstname).HasMaxLength(150);

                entity.Property(e => e.Height).HasMaxLength(20);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Lastname).HasMaxLength(150);

                entity.Property(e => e.Middlename).HasMaxLength(150);

                entity.Property(e => e.PrimaryAddress).HasMaxLength(200);

                entity.Property(e => e.PrimaryContactNo).HasMaxLength(50);

                entity.Property(e => e.ProfileImageName).HasMaxLength(200);

                entity.Property(e => e.ProfileImageTag).HasMaxLength(50);

                entity.Property(e => e.ResidentNumber).HasMaxLength(50);

                entity.Property(e => e.SecondaryAddress).HasMaxLength(200);

                entity.Property(e => e.SecondaryContactNo).HasMaxLength(50);

                entity.Property(e => e.Weight).HasMaxLength(20);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status", "maintenance");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Value).HasMaxLength(150);
            });

            modelBuilder.Entity<TemplateTable>(entity =>
            {
                entity.ToTable("TemplateTable");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount", "security");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.Username).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
