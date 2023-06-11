using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class PhongCho
    {
        public PhongCho()
        {
            ChiTietPhongChos = new HashSet<ChiTietPhongCho>();
        }

        public int PhongChoId { get; set; }
        public int? NguoiDungId { get; set; }
        public int? SoLuongNguoi { get; set; }
        public DateTime? ThoiGianLap { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public virtual ICollection<ChiTietPhongCho> ChiTietPhongChos { get; set; }
    }
}
