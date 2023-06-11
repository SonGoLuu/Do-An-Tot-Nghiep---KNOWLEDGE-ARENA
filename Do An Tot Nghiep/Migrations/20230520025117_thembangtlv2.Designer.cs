﻿// <auto-generated />
using System;
using Do_An_Tot_Nghiep.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Do_An_Tot_Nghiep.Migrations
{
    [DbContext(typeof(dbKA))]
    [Migration("20230520025117_thembangtlv2")]
    partial class thembangtlv2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.BacXepHang", b =>
                {
                    b.Property<int>("BacXepHangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BacXepHangID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BacHang")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BacXepHangId");

                    b.ToTable("BacXepHang");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.CauHoi", b =>
                {
                    b.Property<int>("CauHoiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CauHoiID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Anh")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("ChuongNgaiVatId")
                        .HasColumnType("int")
                        .HasColumnName("ChuongNgaiVatID");

                    b.Property<string>("DapAn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GoiCauHoiId")
                        .HasColumnType("int")
                        .HasColumnName("GoiCauHoiID");

                    b.Property<int?>("LinhVucId")
                        .HasColumnType("int")
                        .HasColumnName("LinhVucID");

                    b.Property<int?>("MucDo")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SoKyTu")
                        .HasColumnType("int");

                    b.Property<int?>("VongCauHoi")
                        .HasColumnType("int");

                    b.HasKey("CauHoiId");

                    b.HasIndex("ChuongNgaiVatId");

                    b.HasIndex("GoiCauHoiId");

                    b.HasIndex("LinhVucId");

                    b.ToTable("CauHoi");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.ChiTietPhongCho", b =>
                {
                    b.Property<int>("ChiTietPhongChoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChiTietPhongChoID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NguoiDungId")
                        .HasColumnType("int")
                        .HasColumnName("NguoiDungID");

                    b.Property<int?>("PhongChoId")
                        .HasColumnType("int")
                        .HasColumnName("PhongChoID");

                    b.Property<DateTime?>("ThoiGianVao")
                        .HasColumnType("datetime");

                    b.HasKey("ChiTietPhongChoId");

                    b.HasIndex("NguoiDungId");

                    b.HasIndex("PhongChoId");

                    b.ToTable("ChiTietPhongCho");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.ChiTietPhongDau", b =>
                {
                    b.Property<int>("ChiTietPhongDauId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChiTietPhongDauID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NguoiDungId")
                        .HasColumnType("int")
                        .HasColumnName("NguoiDungID");

                    b.Property<int?>("PhongDauId")
                        .HasColumnType("int")
                        .HasColumnName("PhongDauID");

                    b.Property<int?>("ThuHang")
                        .HasColumnType("int");

                    b.Property<int?>("TongDiem")
                        .HasColumnType("int");

                    b.HasKey("ChiTietPhongDauId");

                    b.HasIndex("NguoiDungId");

                    b.HasIndex("PhongDauId");

                    b.ToTable("ChiTietPhongDau");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.ChiTietTungVongDau", b =>
                {
                    b.Property<int>("VongDauId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("VongDauID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Diem")
                        .HasColumnType("int");

                    b.Property<int?>("NguoiDungId")
                        .HasColumnType("int")
                        .HasColumnName("NguoiDungID");

                    b.Property<int?>("PhongDauId")
                        .HasColumnType("int")
                        .HasColumnName("PhongDauID");

                    b.Property<int?>("VongChoi")
                        .HasColumnType("int");

                    b.HasKey("VongDauId")
                        .HasName("PK__ChiTietT__26497C5E4CAA914F");

                    b.HasIndex("NguoiDungId");

                    b.HasIndex("PhongDauId");

                    b.ToTable("ChiTietTungVongDau");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.ChuongNgaiVat", b =>
                {
                    b.Property<int>("ChuongNgaiVatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChuongNgaiVatID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Anh")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("DapAn")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("SoKyTu")
                        .HasColumnType("int");

                    b.HasKey("ChuongNgaiVatId");

                    b.ToTable("ChuongNgaiVat");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.GoiCauHoi", b =>
                {
                    b.Property<int>("GoiCauHoiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("GoiCauHoiID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Diem")
                        .HasColumnType("int");

                    b.HasKey("GoiCauHoiId");

                    b.ToTable("GoiCauHoi");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.LinhVucCauHoi", b =>
                {
                    b.Property<int>("LinhVucId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LinhVucID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TenLinhVuc")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LinhVucId")
                        .HasName("PK__LinhVucC__F06AD8A61037382B");

                    b.ToTable("LinhVucCauHoi");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.NguoiDung", b =>
                {
                    b.Property<int>("NguoiDungId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NguoiDungID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("GioiTinh")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("HoVaTen")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime");

                    b.Property<string>("QueQuan")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TruongHoc")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("NguoiDungId");

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.PhanQuyen", b =>
                {
                    b.Property<int>("PhanQuyenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PhanQuyenID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChiTietPhanQuyen")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TenQuyen")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PhanQuyenId");

                    b.ToTable("PhanQuyen");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.PhongCho", b =>
                {
                    b.Property<int>("PhongChoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PhongChoID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NguoiDungId")
                        .HasColumnType("int")
                        .HasColumnName("NguoiDungID");

                    b.Property<int?>("SoLuongNguoi")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ThoiGianLap")
                        .HasColumnType("datetime");

                    b.HasKey("PhongChoId");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("PhongCho");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.PhongDau", b =>
                {
                    b.Property<int>("PhongDauId")
                        .HasColumnType("int")
                        .HasColumnName("PhongDauID");

                    b.Property<int?>("NguoiDungId")
                        .HasColumnType("int")
                        .HasColumnName("NguoiDungID");

                    b.Property<DateTime?>("ThoiGianBatDau")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ThoiGianKetThuc")
                        .HasColumnType("datetime");

                    b.HasKey("PhongDauId");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("PhongDau");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.TaiKhoan", b =>
                {
                    b.Property<int>("TaiKhoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TaiKhoanID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MatKhau")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("NguoiDungId")
                        .HasColumnType("int")
                        .HasColumnName("NguoiDungID");

                    b.Property<int?>("PhanQuyenId")
                        .HasColumnType("int")
                        .HasColumnName("PhanQuyenID");

                    b.Property<string>("TenDangNhap")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TaiKhoanId");

                    b.HasIndex("NguoiDungId");

                    b.HasIndex("PhanQuyenId");

                    b.ToTable("TaiKhoan");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.TraLoiVong2", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlayerID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Answer");

                    b.Property<bool>("Result")
                        .HasColumnType("bit")
                        .HasColumnName("Result");

                    b.HasKey("PlayerId");

                    b.ToTable("TraLoiVong2");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.XepHang", b =>
                {
                    b.Property<int>("XepHangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("XepHangID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BacXepHangId")
                        .HasColumnType("int")
                        .HasColumnName("BacXepHangID");

                    b.Property<int?>("DiemNangLuc")
                        .HasColumnType("int");

                    b.Property<int?>("NguoiDungId")
                        .HasColumnType("int")
                        .HasColumnName("NguoiDungID");

                    b.HasKey("XepHangId");

                    b.HasIndex("BacXepHangId");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("XepHang");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.CauHoi", b =>
                {
                    b.HasOne("Do_An_Tot_Nghiep.Models.ChuongNgaiVat", "ChuongNgaiVat")
                        .WithMany()
                        .HasForeignKey("ChuongNgaiVatId")
                        .HasConstraintName("FK__CauHoi__ChuongNg__32E0915F");

                    b.HasOne("Do_An_Tot_Nghiep.Models.GoiCauHoi", "GoiCauHoi")
                        .WithMany("CauHois")
                        .HasForeignKey("GoiCauHoiId")
                        .HasConstraintName("FK__CauHoi__GoiCauHo__33D4B598");

                    b.HasOne("Do_An_Tot_Nghiep.Models.LinhVucCauHoi", "LinhVuc")
                        .WithMany()
                        .HasForeignKey("LinhVucId")
                        .HasConstraintName("FK__CauHoi__LinhVucI__31EC6D26");

                    b.Navigation("ChuongNgaiVat");

                    b.Navigation("GoiCauHoi");

                    b.Navigation("LinhVuc");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.ChiTietPhongCho", b =>
                {
                    b.HasOne("Do_An_Tot_Nghiep.Models.NguoiDung", "NguoiDung")
                        .WithMany("ChiTietPhongChos")
                        .HasForeignKey("NguoiDungId")
                        .HasConstraintName("FK__ChiTietPh__Nguoi__3A81B327");

                    b.HasOne("Do_An_Tot_Nghiep.Models.PhongCho", "PhongCho")
                        .WithMany("ChiTietPhongChos")
                        .HasForeignKey("PhongChoId")
                        .HasConstraintName("FK__ChiTietPh__Phong__398D8EEE");

                    b.Navigation("NguoiDung");

                    b.Navigation("PhongCho");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.ChiTietPhongDau", b =>
                {
                    b.HasOne("Do_An_Tot_Nghiep.Models.NguoiDung", "NguoiDung")
                        .WithMany()
                        .HasForeignKey("NguoiDungId")
                        .HasConstraintName("FK__ChiTietPh__Nguoi__412EB0B6");

                    b.HasOne("Do_An_Tot_Nghiep.Models.PhongDau", "PhongDau")
                        .WithMany("ChiTietPhongDaus")
                        .HasForeignKey("PhongDauId")
                        .HasConstraintName("FK__ChiTietPh__Phong__403A8C7D");

                    b.Navigation("NguoiDung");

                    b.Navigation("PhongDau");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.ChiTietTungVongDau", b =>
                {
                    b.HasOne("Do_An_Tot_Nghiep.Models.NguoiDung", "NguoiDung")
                        .WithMany("ChiTietTungVongDaus")
                        .HasForeignKey("NguoiDungId")
                        .HasConstraintName("FK__ChiTietTu__Nguoi__44FF419A");

                    b.HasOne("Do_An_Tot_Nghiep.Models.PhongDau", "PhongDau")
                        .WithMany("ChiTietTungVongDaus")
                        .HasForeignKey("PhongDauId")
                        .HasConstraintName("FK__ChiTietTu__Phong__440B1D61");

                    b.Navigation("NguoiDung");

                    b.Navigation("PhongDau");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.PhongCho", b =>
                {
                    b.HasOne("Do_An_Tot_Nghiep.Models.NguoiDung", "NguoiDung")
                        .WithMany("PhongChos")
                        .HasForeignKey("NguoiDungId")
                        .HasConstraintName("FK__PhongCho__NguoiD__36B12243");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.PhongDau", b =>
                {
                    b.HasOne("Do_An_Tot_Nghiep.Models.NguoiDung", "NguoiDung")
                        .WithMany("PhongDaus")
                        .HasForeignKey("NguoiDungId")
                        .HasConstraintName("FK__PhongDau__NguoiD__3D5E1FD2");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.TaiKhoan", b =>
                {
                    b.HasOne("Do_An_Tot_Nghiep.Models.NguoiDung", "NguoiDung")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("NguoiDungId")
                        .HasConstraintName("FK__TaiKhoan__NguoiD__29572725");

                    b.HasOne("Do_An_Tot_Nghiep.Models.PhanQuyen", "PhanQuyen")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("PhanQuyenId")
                        .HasConstraintName("FK__TaiKhoan__PhanQu__286302EC");

                    b.Navigation("NguoiDung");

                    b.Navigation("PhanQuyen");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.XepHang", b =>
                {
                    b.HasOne("Do_An_Tot_Nghiep.Models.BacXepHang", "BacXepHang")
                        .WithMany("XepHangs")
                        .HasForeignKey("BacXepHangId")
                        .HasConstraintName("FK__XepHang__BacXepH__4AB81AF0");

                    b.HasOne("Do_An_Tot_Nghiep.Models.NguoiDung", "NguoiDung")
                        .WithMany("XepHangs")
                        .HasForeignKey("NguoiDungId")
                        .HasConstraintName("FK__XepHang__NguoiDu__49C3F6B7");

                    b.Navigation("BacXepHang");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.BacXepHang", b =>
                {
                    b.Navigation("XepHangs");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.GoiCauHoi", b =>
                {
                    b.Navigation("CauHois");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.NguoiDung", b =>
                {
                    b.Navigation("ChiTietPhongChos");

                    b.Navigation("ChiTietTungVongDaus");

                    b.Navigation("PhongChos");

                    b.Navigation("PhongDaus");

                    b.Navigation("TaiKhoans");

                    b.Navigation("XepHangs");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.PhanQuyen", b =>
                {
                    b.Navigation("TaiKhoans");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.PhongCho", b =>
                {
                    b.Navigation("ChiTietPhongChos");
                });

            modelBuilder.Entity("Do_An_Tot_Nghiep.Models.PhongDau", b =>
                {
                    b.Navigation("ChiTietPhongDaus");

                    b.Navigation("ChiTietTungVongDaus");
                });
#pragma warning restore 612, 618
        }
    }
}
