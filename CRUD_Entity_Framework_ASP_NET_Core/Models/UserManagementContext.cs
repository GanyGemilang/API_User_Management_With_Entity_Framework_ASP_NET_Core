using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CRUD_Entity_Framework_ASP_NET_Core.Models
{
    public partial class UserManagementContext : DbContext
    {
        public UserManagementContext()
        {
        }

        public UserManagementContext(DbContextOptions<UserManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblUser> TblUsers { get; set; }

        public IConfiguration Configuration { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblUser>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("tbl_user");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasComment("");

                entity.Property(e => e.ExpiredToken)
                    .HasColumnType("datetime")
                    .HasComment("");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Online)
                    .HasColumnName("online")
                    .HasComment("");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("role")
                    .HasComment("");

                entity.Property(e => e.Token)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
