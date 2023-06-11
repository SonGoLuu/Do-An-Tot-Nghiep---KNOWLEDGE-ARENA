using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Do_An_Tot_Nghiep.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BacXepHang",
                columns: table => new
                {
                    BacXepHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BacHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BacXepHang", x => x.BacXepHangID);
                });

            migrationBuilder.CreateTable(
                name: "ChuongNgaiVat",
                columns: table => new
                {
                    ChuongNgaiVatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoKyTu = table.Column<int>(type: "int", nullable: true),
                    DapAn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Anh = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuongNgaiVat", x => x.ChuongNgaiVatID);
                });

            migrationBuilder.CreateTable(
                name: "GoiCauHoi",
                columns: table => new
                {
                    GoiCauHoiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diem = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoiCauHoi", x => x.GoiCauHoiID);
                });

            migrationBuilder.CreateTable(
                name: "LinhVucCauHoi",
                columns: table => new
                {
                    LinhVucID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLinhVuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LinhVucC__F06AD8A61037382B", x => x.LinhVucID);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    NguoiDungID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoVaTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    QueQuan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TruongHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.NguoiDungID);
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyen",
                columns: table => new
                {
                    PhanQuyenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChiTietPhanQuyen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyen", x => x.PhanQuyenID);
                });

            migrationBuilder.CreateTable(
                name: "CauHoi",
                columns: table => new
                {
                    CauHoiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VongCauHoi = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinhVucID = table.Column<int>(type: "int", nullable: true),
                    DapAn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoKyTu = table.Column<int>(type: "int", nullable: true),
                    ChuongNgaiVatID = table.Column<int>(type: "int", nullable: true),
                    MucDo = table.Column<int>(type: "int", nullable: true),
                    Anh = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    GoiCauHoiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoi", x => x.CauHoiID);
                    table.ForeignKey(
                        name: "FK__CauHoi__ChuongNg__32E0915F",
                        column: x => x.ChuongNgaiVatID,
                        principalTable: "ChuongNgaiVat",
                        principalColumn: "ChuongNgaiVatID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CauHoi__GoiCauHo__33D4B598",
                        column: x => x.GoiCauHoiID,
                        principalTable: "GoiCauHoi",
                        principalColumn: "GoiCauHoiID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CauHoi__LinhVucI__31EC6D26",
                        column: x => x.LinhVucID,
                        principalTable: "LinhVucCauHoi",
                        principalColumn: "LinhVucID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhongCho",
                columns: table => new
                {
                    PhongChoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: true),
                    SoLuongNguoi = table.Column<int>(type: "int", nullable: true),
                    ThoiGianLap = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongCho", x => x.PhongChoID);
                    table.ForeignKey(
                        name: "FK__PhongCho__NguoiD__36B12243",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhongDau",
                columns: table => new
                {
                    PhongDauID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime", nullable: true),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiDungID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongDau", x => x.PhongDauID);
                    table.ForeignKey(
                        name: "FK__PhongDau__NguoiD__3D5E1FD2",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "XepHang",
                columns: table => new
                {
                    XepHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: true),
                    BacXepHangID = table.Column<int>(type: "int", nullable: true),
                    DiemNangLuc = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XepHang", x => x.XepHangID);
                    table.ForeignKey(
                        name: "FK__XepHang__BacXepH__4AB81AF0",
                        column: x => x.BacXepHangID,
                        principalTable: "BacXepHang",
                        principalColumn: "BacXepHangID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__XepHang__NguoiDu__49C3F6B7",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    TaiKhoanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhanQuyenID = table.Column<int>(type: "int", nullable: true),
                    NguoiDungID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.TaiKhoanID);
                    table.ForeignKey(
                        name: "FK__TaiKhoan__NguoiD__29572725",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__TaiKhoan__PhanQu__286302EC",
                        column: x => x.PhanQuyenID,
                        principalTable: "PhanQuyen",
                        principalColumn: "PhanQuyenID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhongCho",
                columns: table => new
                {
                    ChiTietPhongChoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhongChoID = table.Column<int>(type: "int", nullable: true),
                    NguoiDungID = table.Column<int>(type: "int", nullable: true),
                    ThoiGianVao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhongCho", x => x.ChiTietPhongChoID);
                    table.ForeignKey(
                        name: "FK__ChiTietPh__Nguoi__3A81B327",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTietPh__Phong__398D8EEE",
                        column: x => x.PhongChoID,
                        principalTable: "PhongCho",
                        principalColumn: "PhongChoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhongDau",
                columns: table => new
                {
                    ChiTietPhongDauID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhongDauID = table.Column<int>(type: "int", nullable: true),
                    NguoiDungID = table.Column<int>(type: "int", nullable: true),
                    TongDiem = table.Column<int>(type: "int", nullable: true),
                    ThuHang = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhongDau", x => x.ChiTietPhongDauID);
                    table.ForeignKey(
                        name: "FK__ChiTietPh__Nguoi__412EB0B6",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTietPh__Phong__403A8C7D",
                        column: x => x.PhongDauID,
                        principalTable: "PhongDau",
                        principalColumn: "PhongDauID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietTungVongDau",
                columns: table => new
                {
                    VongDauID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhongDauID = table.Column<int>(type: "int", nullable: true),
                    VongChoi = table.Column<int>(type: "int", nullable: true),
                    NguoiDungID = table.Column<int>(type: "int", nullable: true),
                    Diem = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietT__26497C5E4CAA914F", x => x.VongDauID);
                    table.ForeignKey(
                        name: "FK__ChiTietTu__Nguoi__44FF419A",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTietTu__Phong__440B1D61",
                        column: x => x.PhongDauID,
                        principalTable: "PhongDau",
                        principalColumn: "PhongDauID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CauHoi_ChuongNgaiVatID",
                table: "CauHoi",
                column: "ChuongNgaiVatID");

            migrationBuilder.CreateIndex(
                name: "IX_CauHoi_GoiCauHoiID",
                table: "CauHoi",
                column: "GoiCauHoiID");

            migrationBuilder.CreateIndex(
                name: "IX_CauHoi_LinhVucID",
                table: "CauHoi",
                column: "LinhVucID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhongCho_NguoiDungID",
                table: "ChiTietPhongCho",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhongCho_PhongChoID",
                table: "ChiTietPhongCho",
                column: "PhongChoID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhongDau_NguoiDungID",
                table: "ChiTietPhongDau",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhongDau_PhongDauID",
                table: "ChiTietPhongDau",
                column: "PhongDauID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietTungVongDau_NguoiDungID",
                table: "ChiTietTungVongDau",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietTungVongDau_PhongDauID",
                table: "ChiTietTungVongDau",
                column: "PhongDauID");

            migrationBuilder.CreateIndex(
                name: "IX_PhongCho_NguoiDungID",
                table: "PhongCho",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_PhongDau_NguoiDungID",
                table: "PhongDau",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_NguoiDungID",
                table: "TaiKhoan",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_PhanQuyenID",
                table: "TaiKhoan",
                column: "PhanQuyenID");

            migrationBuilder.CreateIndex(
                name: "IX_XepHang_BacXepHangID",
                table: "XepHang",
                column: "BacXepHangID");

            migrationBuilder.CreateIndex(
                name: "IX_XepHang_NguoiDungID",
                table: "XepHang",
                column: "NguoiDungID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHoi");

            migrationBuilder.DropTable(
                name: "ChiTietPhongCho");

            migrationBuilder.DropTable(
                name: "ChiTietPhongDau");

            migrationBuilder.DropTable(
                name: "ChiTietTungVongDau");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "XepHang");

            migrationBuilder.DropTable(
                name: "ChuongNgaiVat");

            migrationBuilder.DropTable(
                name: "GoiCauHoi");

            migrationBuilder.DropTable(
                name: "LinhVucCauHoi");

            migrationBuilder.DropTable(
                name: "PhongCho");

            migrationBuilder.DropTable(
                name: "PhongDau");

            migrationBuilder.DropTable(
                name: "PhanQuyen");

            migrationBuilder.DropTable(
                name: "BacXepHang");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
