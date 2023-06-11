using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class PhanQuyen
    {
        public PhanQuyen()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public int PhanQuyenId { get; set; }
        public string TenQuyen { get; set; }
        public string ChiTietPhanQuyen { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
