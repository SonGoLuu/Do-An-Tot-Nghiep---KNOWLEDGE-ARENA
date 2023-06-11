using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public partial class dbKA: IdentityDbContext
    {
        public dbKA()
        {
        }
        public dbKA(DbContextOptions<dbKA> options) : base(options) { }

        public virtual DbSet<BacXepHang> BacXepHangs { get; set; }
        public virtual DbSet<CauHoi> CauHois { get; set; }
        public virtual DbSet<ChiTietPhongCho> ChiTietPhongChos { get; set; }
        public virtual DbSet<ChiTietPhongDau> ChiTietPhongDaus { get; set; }
        public virtual DbSet<ChiTietTungVongDau> ChiTietTungVongDaus { get; set; }
        public virtual DbSet<ChuongNgaiVat> ChuongNgaiVats { get; set; }
        public virtual DbSet<GoiCauHoi> GoiCauHois { get; set; }
        public virtual DbSet<LinhVucCauHoi> LinhVucCauHois { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<PhongCho> PhongChos { get; set; }
        public virtual DbSet<PhongDau> PhongDaus { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TraLoiCNV> TraLoiCNVs { get; set; }
        public virtual DbSet<CauDaHoi> CauDaHois { get; set; }
        public virtual DbSet<TraLoiVong2> TraLoiVong2s { get; set; }
        public virtual DbSet<XepHang> XepHangs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-5LK6RF58;Database=dbDoAn;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BacXepHang>(entity =>
            {
                entity.ToTable("BacXepHang");

                entity.Property(e => e.BacXepHangId).HasColumnName("BacXepHangID");

                entity.Property(e => e.BacHang).HasMaxLength(100);
            });

            modelBuilder.Entity<CauHoi>(entity =>
            {
                entity.ToTable("CauHoi");

                entity.Property(e => e.CauHoiId).HasColumnName("CauHoiID");

                entity.Property(e => e.ChuongNgaiVatId).HasColumnName("ChuongNgaiVatID");

                entity.Property(e => e.GoiCauHoiId).HasColumnName("GoiCauHoiID");

                entity.Property(e => e.LinhVucId).HasColumnName("LinhVucID");

                entity.HasOne(d => d.ChuongNgaiVat)
                    .WithMany()
                    .HasForeignKey(d => d.ChuongNgaiVatId)
                    .HasConstraintName("FK__CauHoi__ChuongNg__32E0915F");

                entity.HasOne(d => d.GoiCauHoi)
                    .WithMany(p => p.CauHois)
                    .HasForeignKey(d => d.GoiCauHoiId)
                    .HasConstraintName("FK__CauHoi__GoiCauHo__33D4B598");

                entity.HasOne(d => d.LinhVuc)
                    .WithMany(/*p => p.CauHois*/)
                    .HasForeignKey(d => d.LinhVucId)
                    .HasConstraintName("FK__CauHoi__LinhVucI__31EC6D26");
            });

            modelBuilder.Entity<ChiTietPhongCho>(entity =>
            {
                entity.ToTable("ChiTietPhongCho");

                entity.Property(e => e.ChiTietPhongChoId).HasColumnName("ChiTietPhongChoID");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.PhongChoId).HasColumnName("PhongChoID");

                entity.Property(e => e.ThoiGianVao).HasColumnType("datetime");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.ChiTietPhongChos)
                    .HasForeignKey(d => d.NguoiDungId)
                    .HasConstraintName("FK__ChiTietPh__Nguoi__3A81B327");

                entity.HasOne(d => d.PhongCho)
                    .WithMany(p => p.ChiTietPhongChos)
                    .HasForeignKey(d => d.PhongChoId)
                    .HasConstraintName("FK__ChiTietPh__Phong__398D8EEE");
            });

            modelBuilder.Entity<ChiTietPhongDau>(entity =>
            {
                entity.ToTable("ChiTietPhongDau");

                entity.Property(e => e.ChiTietPhongDauId).HasColumnName("ChiTietPhongDauID");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.PhongDauId).HasColumnName("PhongDauID");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(/*p => p.ChiTietPhongDaus*/)
                    .HasForeignKey(d => d.NguoiDungId)
                    .HasConstraintName("FK__ChiTietPh__Nguoi__412EB0B6");

                entity.HasOne(d => d.PhongDau)
                    .WithMany(p => p.ChiTietPhongDaus)
                    .HasForeignKey(d => d.PhongDauId)
                    .HasConstraintName("FK__ChiTietPh__Phong__403A8C7D");
            });

            modelBuilder.Entity<ChiTietTungVongDau>(entity =>
            {
                entity.HasKey(e => e.VongDauId)
                    .HasName("PK__ChiTietT__26497C5E4CAA914F");

                entity.ToTable("ChiTietTungVongDau");

                entity.Property(e => e.VongDauId).HasColumnName("VongDauID");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.PhongDauId).HasColumnName("PhongDauID");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.ChiTietTungVongDaus)
                    .HasForeignKey(d => d.NguoiDungId)
                    .HasConstraintName("FK__ChiTietTu__Nguoi__44FF419A");

                entity.HasOne(d => d.PhongDau)
                    .WithMany(p => p.ChiTietTungVongDaus)
                    .HasForeignKey(d => d.PhongDauId)
                    .HasConstraintName("FK__ChiTietTu__Phong__440B1D61");
            });

            modelBuilder.Entity<ChuongNgaiVat>(entity =>
            {
                entity.ToTable("ChuongNgaiVat");

                entity.Property(e => e.ChuongNgaiVatId).HasColumnName("ChuongNgaiVatID");

                entity.Property(e => e.DapAn).HasMaxLength(500);
            });

            modelBuilder.Entity<GoiCauHoi>(entity =>
            {
                entity.ToTable("GoiCauHoi");

                entity.Property(e => e.GoiCauHoiId).HasColumnName("GoiCauHoiID");
            });

            modelBuilder.Entity<LinhVucCauHoi>(entity =>
            {
                entity.HasKey(e => e.LinhVucId)
                    .HasName("PK__LinhVucC__F06AD8A61037382B");

                entity.ToTable("LinhVucCauHoi");

                entity.Property(e => e.LinhVucId).HasColumnName("LinhVucID");

                entity.Property(e => e.TenLinhVuc).HasMaxLength(100);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.ToTable("NguoiDung");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.GioiTinh).HasMaxLength(20);

                entity.Property(e => e.HoVaTen).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.QueQuan).HasMaxLength(500);

                entity.Property(e => e.TruongHoc).HasMaxLength(100);
            });

            modelBuilder.Entity<PhanQuyen>(entity =>
            {
                entity.ToTable("PhanQuyen");

                entity.Property(e => e.PhanQuyenId).HasColumnName("PhanQuyenID");

                entity.Property(e => e.ChiTietPhanQuyen).HasMaxLength(500);

                entity.Property(e => e.TenQuyen).HasMaxLength(100);
            });

            modelBuilder.Entity<PhongCho>(entity =>
            {
                entity.ToTable("PhongCho");

                entity.Property(e => e.PhongChoId).HasColumnName("PhongChoID"); ;

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.ThoiGianLap).HasColumnType("datetime");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.PhongChos)
                    .HasForeignKey(d => d.NguoiDungId)
                    .HasConstraintName("FK__PhongCho__NguoiD__36B12243");
            });

            modelBuilder.Entity<PhongDau>(entity =>
            {
                entity.ToTable("PhongDau");

                entity.Property(e => e.PhongDauId).HasColumnName("PhongDauID").ValueGeneratedNever(); ;

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.PhongDaus)
                    .HasForeignKey(d => d.NguoiDungId)
                    .HasConstraintName("FK__PhongDau__NguoiD__3D5E1FD2");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.ToTable("TaiKhoan");

                entity.Property(e => e.TaiKhoanId).HasColumnName("TaiKhoanID");

                entity.Property(e => e.MatKhau).HasMaxLength(20);

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.PhanQuyenId).HasColumnName("PhanQuyenID");

                entity.Property(e => e.TenDangNhap).HasMaxLength(50);

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.NguoiDungId)
                    .HasConstraintName("FK__TaiKhoan__NguoiD__29572725");

                entity.HasOne(d => d.PhanQuyen)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.PhanQuyenId)
                    .HasConstraintName("FK__TaiKhoan__PhanQu__286302EC");
            });

            modelBuilder.Entity<TraLoiCNV>(entity =>
            {
                entity.ToTable("TraLoiCNV");

                entity.Property(e => e.TraLoiId).HasColumnName("TraLoiID");

                entity.Property(e => e.PhongDauId).HasColumnName("PhongDauID");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.Property(e => e.ChuongNgaiVatId).HasColumnName("ChuongNgaiVatID");

                entity.Property(e => e.Answer).HasColumnName("Answer");

                entity.Property(e => e.ThoiGian).HasColumnName("ThoiGian");

                entity.Property(e => e.TrangThaiNguoiChoi).HasColumnName("TrangThaiNguoiChoi");

                entity.Property(e => e.HoVaTen).HasColumnName("HoVaTen");

                entity.HasKey(e => e.TraLoiId);

            });


            modelBuilder.Entity<TraLoiVong2>(entity =>
            {
                entity.ToTable("TraLoiVong2");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.Answer).HasColumnName("Answer");

                entity.Property(e => e.Result).HasColumnName("Result");

                entity.HasKey(e => e.Id);

            });

            modelBuilder.Entity<CauDaHoi>(entity =>
            {
                entity.ToTable("CauDaHoi");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PhongDauId).HasColumnName("PhongDauID");

                entity.Property(e => e.CauHoiId).HasColumnName("CauHoiID");

                entity.HasKey(e => e.Id);

            });


            modelBuilder.Entity<XepHang>(entity =>
            {
                entity.ToTable("XepHang");

                entity.Property(e => e.XepHangId).HasColumnName("XepHangID");

                entity.Property(e => e.BacXepHangId).HasColumnName("BacXepHangID");

                entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

                entity.HasOne(d => d.BacXepHang)
                    .WithMany(p => p.XepHangs)
                    .HasForeignKey(d => d.BacXepHangId)
                    .HasConstraintName("FK__XepHang__BacXepH__4AB81AF0");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.XepHangs)
                    .HasForeignKey(d => d.NguoiDungId)
                    .HasConstraintName("FK__XepHang__NguoiDu__49C3F6B7");
            });


            //modelBuilder.Entity<IdentityUser>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //});

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
