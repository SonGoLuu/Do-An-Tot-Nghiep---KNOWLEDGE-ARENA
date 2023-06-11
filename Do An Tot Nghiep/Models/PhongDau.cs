using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class PhongDau
    {
        public PhongDau()
        {
            ChiTietPhongDaus = new HashSet<ChiTietPhongDau>();
            ChiTietTungVongDaus = new HashSet<ChiTietTungVongDau>();
        }

        public int PhongDauId { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public int? NguoiDungId { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public virtual ICollection<ChiTietPhongDau> ChiTietPhongDaus { get; set; }
        public virtual ICollection<ChiTietTungVongDau> ChiTietTungVongDaus { get; set; }
    }
}
