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
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(MasterSchema);

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

        PhuongXaBuilder(modelBuilder);
        TinhThanhBuilder(modelBuilder);
        DanTocBuilder(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    #region PHƯỜNG XÃ
    private void PhuongXaBuilder (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhuongXa>(entity =>
        {
            entity.ToTable("PHUONGXAS",MasterSchema);
            entity.HasKey(e => e.MaPhuongXa).HasName("PK_PHUONGXAS");

            entity.Property(e => e.MaPhuongXa)
                  .HasColumnName("MAPHUONGXA")
                  .HasColumnType("VARCHAR2(7)")
                  .IsRequired();

            entity.Property(e => e.TenPhuongXa)
                  .HasColumnName("TENPHUONGXA")
                  .HasColumnType("VARCHAR2(250)")
                  .IsRequired();

            entity.Property(e => e.VietTat)
                  .HasColumnName("VIETTAT")
                  .HasColumnType("VARCHAR2(5)");

            entity.Property(e => e.Hide)
                  .HasColumnName("HIDE")
                  .HasColumnType("NUMBER(1)")
                  .HasDefaultValue(0);
        });
    }
    #endregion

    #region TỈNH THÀNH
    private void TinhThanhBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TinhThanh>(entity =>
        {
            entity.ToTable("TINHTHANHS",MasterSchema);
            entity.HasKey(e => e.MaTinhThanh).HasName("PK_TINHTHANHS");

            entity.Property(e => e.TenTinhThanh)
                  .HasColumnName("TENTINHTHANH")
                  .HasColumnType("VARCHAR2(2)")
                  .IsRequired();

            entity.Property(e => e.TenTinhThanh)
                  .HasColumnName("TENTINHTHANH")
                  .HasColumnType("VARCHAR2(250)")
                  .IsRequired();

            entity.Property(e => e.VietTat)
                  .HasColumnName("VIETTAT")
                  .HasColumnType("VARCHAR2(5)");

            entity.Property(e => e.Hide)
                  .HasColumnName("HIDE")
                  .HasColumnType("NUMBER(1)")
                  .HasDefaultValue(0);
        });
    }
    #endregion

    #region DÂN TỘC
    private void DanTocBuilder (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DanToc>(entity =>
        {
            entity.ToTable("BTDDT", MasterSchema);
            entity.HasKey(e => e.MaDanToc).HasName("PK_BTDDT");

            entity.Property(e => e.TenDanToc)
                  .HasColumnName("DANTOC")
                  .HasColumnType("NVARCHAR2(254)");

            entity.Property (e => e.Hide)
                  .HasColumnName("HIDE")
                  .HasColumnType("NUMBER(1)")
                  .HasDefaultValue (0);
        });
    }
    #endregion

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
