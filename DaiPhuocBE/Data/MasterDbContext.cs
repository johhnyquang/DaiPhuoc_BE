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

    public virtual DbSet<Btdbn> Btdbns { get; set; }

    public virtual DbSet<Btddt> Btddts { get; set; }

    public virtual DbSet<Dmquocgium> Dmquocgia { get; set; }

    public virtual DbSet<Phuongxa> Phuongxas { get; set; }

    public virtual DbSet<Tinhthanh> Tinhthanhs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("HSOFTDAIPHUOC");

        modelBuilder.Entity<Btdbn>(entity =>
        {
            entity.HasKey(e => e.Mabn);

            entity.ToTable("BTDBN");

            entity.Property(e => e.Mabn)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("MABN");
            entity.Property(e => e.Benhmantinh)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("BENHMANTINH");
            entity.Property(e => e.Boquathemmoi)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("BOQUATHEMMOI");
            entity.Property(e => e.Cholam)
                .HasMaxLength(254)
                .HasColumnName("CHOLAM");
            entity.Property(e => e.Diachitt)
                .HasMaxLength(1000)
                .HasColumnName("DIACHITT");
            entity.Property(e => e.Docthan)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("DOCTHAN");
            entity.Property(e => e.Donvi)
                .HasMaxLength(10)
                .HasColumnName("DONVI");
            entity.Property(e => e.Hide)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("HIDE");
            entity.Property(e => e.Hinh)
                .HasColumnType("CLOB")
                .HasColumnName("HINH");
            entity.Property(e => e.Hoten)
                .HasMaxLength(254)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Hotenkdau)
                .HasMaxLength(254)
                .HasColumnName("HOTENKDAU");
            entity.Property(e => e.Idbv)
                .HasPrecision(8)
                .HasDefaultValueSql("0")
                .HasColumnName("IDBV");
            entity.Property(e => e.Idhl7)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IDHL7");
            entity.Property(e => e.Macho)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("MACHO");
            entity.Property(e => e.Madantoc)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("MADANTOC");
            entity.Property(e => e.Mann)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("MANN");
            entity.Property(e => e.Mannsk)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MANNSK");
            entity.Property(e => e.Manv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MANV");
            entity.Property(e => e.Maphuongxa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAPHUONGXA");
            entity.Property(e => e.MaphuongxaBak)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAPHUONGXA_BAK");
            entity.Property(e => e.Maqh)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("MAQH");
            entity.Property(e => e.Maqu)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("MAQU");
            entity.Property(e => e.Masothue)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MASOTHUE");
            entity.Property(e => e.Matc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MATC");
            entity.Property(e => e.Mathon)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MATHON");
            entity.Property(e => e.Mato)
                .HasMaxLength(10)
                .HasColumnName("MATO");
            entity.Property(e => e.Matongiao)
                .HasPrecision(2)
                .HasDefaultValueSql("0")
                .HasColumnName("MATONGIAO");
            entity.Property(e => e.Matt)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("MATT");
            entity.Property(e => e.MattBak)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("MATT_BAK");
            entity.Property(e => e.Nam).HasColumnName("NAM");
            entity.Property(e => e.Nams).HasColumnName("NAMS");
            entity.Property(e => e.Namsinh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("NAMSINH");
            entity.Property(e => e.Ngaycap)
                .HasColumnType("DATE")
                .HasColumnName("NGAYCAP");
            entity.Property(e => e.Ngaycapnhat)
                .HasColumnType("DATE")
                .HasColumnName("NGAYCAPNHAT");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("DATE")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Ngayud)
                .HasDefaultValueSql("sysdate")
                .HasColumnType("DATE")
                .HasColumnName("NGAYUD");
            entity.Property(e => e.Nhommau)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("NHOMMAU");
            entity.Property(e => e.Noicap)
                .HasMaxLength(254)
                .HasColumnName("NOICAP");
            entity.Property(e => e.Password)
                .HasMaxLength(254)
                .IsUnicode(false)
                .HasColumnName("PASSWORD_");
            entity.Property(e => e.Phai)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("PHAI");
            entity.Property(e => e.Sobaotu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SOBAOTU");
            entity.Property(e => e.Sobenhan)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("SOBENHAN");
            entity.Property(e => e.Sobhxh)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SOBHXH");
            entity.Property(e => e.Socmnd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SOCMND");
            entity.Property(e => e.Sonha)
                .HasMaxLength(600)
                .HasColumnName("SONHA");
            entity.Property(e => e.Sothe)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SOTHE");
            entity.Property(e => e.Taohoso)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("TAOHOSO");
            entity.Property(e => e.Tenfile)
                .HasMaxLength(254)
                .IsUnicode(false)
                .HasColumnName("TENFILE");
            entity.Property(e => e.Thon)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("THON");
            entity.Property(e => e.Tuvong)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("TUVONG");
            entity.Property(e => e.Userid)
                .HasPrecision(7)
                .HasDefaultValueSql("0")
                .HasColumnName("USERID");
        });

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
            entity.Property(e => e.Dantoc)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DANTOC");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hoten)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Maphuongxa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MAPHUONGXA");
            entity.Property(e => e.Matinh)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("MATINH");
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
            entity.Property(e => e.Quoctich)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("QUOCTICH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Socmnd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SOCMND");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
