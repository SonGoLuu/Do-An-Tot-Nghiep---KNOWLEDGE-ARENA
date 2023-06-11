using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class NguoiDung
    {
        public NguoiDung()
        {
            ChiTietPhongChos = new HashSet<ChiTietPhongCho>();
            //ChiTietPhongDaus = new HashSet<ChiTietPhongDau>();
            ChiTietTungVongDaus = new HashSet<ChiTietTungVongDau>();
            PhongChos = new HashSet<PhongCho>();
            PhongDaus = new HashSet<PhongDau>();
            TaiKhoans = new HashSet<TaiKhoan>();
            XepHangs = new HashSet<XepHang>();
        }

        public int NguoiDungId { get; set; }
        public string HoVaTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string QueQuan { get; set; }
        public string TruongHoc { get; set; }
        public byte[] Avatar { get; set; }

        public virtual ICollection<ChiTietPhongCho> ChiTietPhongChos { get; set; }
        //public virtual ICollection<ChiTietPhongDau> ChiTietPhongDaus { get; set; }
        public virtual ICollection<ChiTietTungVongDau> ChiTietTungVongDaus { get; set; }
        public virtual ICollection<PhongCho> PhongChos { get; set; }
        public virtual ICollection<PhongDau> PhongDaus { get; set; }
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
        public virtual ICollection<XepHang> XepHangs { get; set; }
    }
}
