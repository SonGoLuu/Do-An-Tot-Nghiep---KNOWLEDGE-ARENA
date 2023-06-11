using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class TaiKhoan
    {
        public int TaiKhoanId { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? PhanQuyenId { get; set; }
        public int? NguoiDungId { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public virtual PhanQuyen PhanQuyen { get; set; }
    }
}
