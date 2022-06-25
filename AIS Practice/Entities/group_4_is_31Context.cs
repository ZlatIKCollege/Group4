using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AIS_Practice.Entities
{
    public partial class group_4_is_31Context : DbContext
    {
        public group_4_is_31Context()
        {
        }

        public group_4_is_31Context(DbContextOptions<group_4_is_31Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Katalog> Katalogs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=192.168.0.94;Port=3306;user=group_4_is_31;password=ei5S8v;database=group_4_is_31");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Katalog>(entity =>
            {
                entity.ToTable("Katalog");

                entity.HasIndex(e => e.Price, "FK_Sales");

                entity.HasIndex(e => e.Type, "FK_Types");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("company");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReleaseDate).HasColumnName("releaseDate");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.PriceNavigation)
                    .WithMany(p => p.Katalogs)
                    .HasForeignKey(d => d.Price)
                    .HasConstraintName("FK_Sales");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Katalogs)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Types");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.HasIndex(e => e.Product, "FK_Product");

                entity.HasIndex(e => e.Staff, "FK_Staffs");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product");

                entity.HasOne(d => d.StaffNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Staff)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staffs");
            });

            modelBuilder.Entity<Supply>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Contact)
                    .HasMaxLength(16)
                    .HasColumnName("contact");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Role, "FK_Role");

                entity.HasIndex(e => e.Staff, "FK_Staff");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("address")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("login")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Staff).HasColumnName("staff");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role");

                entity.HasOne(d => d.StaffNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Staff)
                    .HasConstraintName("FK_Staff");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .HasColumnName("contact");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Passport).HasColumnName("passport");

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("surname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
