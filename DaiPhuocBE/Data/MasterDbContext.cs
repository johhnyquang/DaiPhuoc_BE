using System;
using System.Collections.Generic;
using DaiPhuocBE.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace DaiPhuocBE.Data;

public partial class MasterDbContext : DbContext
{
    public readonly string TransactionSchema;
    private readonly string MasterSchema;

    public MasterDbContext(DbContextOptions<MasterDbContext> options, IConfiguration configuration)
        : base(options)
    {
        TransactionSchema = configuration.GetSection("SchemaName").Value ?? string.Empty;
        MasterSchema = configuration.GetSection("SchemaName").Value ?? string.Empty;
    }

    public virtual DbSet<Btddt> Btddts { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Dmquocgium> Dmquocgia { get; set; }

    public virtual DbSet<Phuongxa> Phuongxas { get; set; }

    public virtual DbSet<Tinhthanh> Tinhthanhs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("HSOFTDAIPHUOC");

        modelBuilder.Entity<Btddt>(entity =>
        {
            entity.HasKey(e => e.Madantoc);

            entity.ToTable("BTDDT");

            entity.Property(e => e.Madantoc)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("MADANTOC");
            entity.Property(e => e.Dantoc)
                .HasMaxLength(254)
                .HasColumnName("DANTOC");
            entity.Property(e => e.Hide)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("HIDE");
            entity.Property(e => e.IdSytdantoc)
                .HasPrecision(4)
                .HasDefaultValueSql("0")
                .HasColumnName("ID_SYTDANTOC");
            entity.Property(e => e.Mabh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MABH");
            entity.Property(e => e.Ngayud)
                .HasDefaultValueSql("sysdate")
                .HasColumnType("DATE")
                .HasColumnName("NGAYUD");
        });

        modelBuilder.Entity<Dmquocgium>(entity =>
        {
            entity.HasKey(e => e.Ma);

            entity.ToTable("DMQUOCGIA");

            entity.Property(e => e.Ma)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("MA");
            entity.Property(e => e.IdSytquocgia)
                .HasPrecision(4)
                .HasDefaultValueSql("0")
                .HasColumnName("ID_SYTQUOCGIA");
            entity.Property(e => e.Mabh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MABH");
            entity.Property(e => e.Ngayud)
                .HasDefaultValueSql("sysdate")
                .HasColumnType("DATE")
                .HasColumnName("NGAYUD");
            entity.Property(e => e.Ten)
                .HasMaxLength(254)
                .HasColumnName("TEN");
            entity.Property(e => e.Valuea)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("VALUEA");
            entity.Property(e => e.Vietnamese)
                .HasMaxLength(254)
                .HasColumnName("VIETNAMESE");
        });

        modelBuilder.Entity<Phuongxa>(entity =>
        {
            entity.HasKey(e => e.Maphuongxa);

            entity.ToTable("PHUONGXAS");

            entity.Property(e => e.Maphuongxa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MAPHUONGXA");
            entity.Property(e => e.Hide)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("HIDE");
            entity.Property(e => e.Tenphuongxa)
                .HasMaxLength(250)
                .HasColumnName("TENPHUONGXA");
            entity.Property(e => e.Viettat)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("VIETTAT");
        });

        modelBuilder.Entity<Tinhthanh>(entity =>
        {
            entity.HasKey(e => e.Matinhthanh);

            entity.ToTable("TINHTHANHS");

            entity.Property(e => e.Matinhthanh)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("MATINHTHANH");
            entity.Property(e => e.Hide)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("HIDE");
            entity.Property(e => e.Tentinhthanh)
                .HasMaxLength(250)
                .HasColumnName("TENTINHTHANH");
            entity.Property(e => e.Viettat)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("VIETTAT");
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USERS");

            entity.HasIndex(e => e.Sdt, "UQ_USERS_SDT").IsUnique();

            entity.HasIndex(e => e.Socmnd, "UQ_USERS_SOCMND").IsUnique();

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Namsinh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("NAMSINH");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("DATE")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD_HASH");
            entity.Property(e => e.Phai)
                .HasPrecision(1)
                .HasColumnName("PHAI");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Socmnd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SOCMND");
            entity.Property(e => e.quoctich)
                  .HasMaxLength(250)
                  .IsUnicode(false)
                  .HasColumnName("QUOCTICH");
            entity.Property(e => e.dantoc)
                  .HasMaxLength(250)
                  .IsUnicode(false)
                  .HasColumnName("DANTOC");
            entity.Property(e => e.matinh)
                  .HasMaxLength(2)
                  .IsUnicode(false)
                  .HasColumnName("MATINH");
            entity.Property(e => e.maphuongxa)
                  .HasMaxLength(7)
                  .IsUnicode(false)
                  .HasColumnName("MAPHUONGXA");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
