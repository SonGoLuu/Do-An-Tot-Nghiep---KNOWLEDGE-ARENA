using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class ChiTietPhongCho
    {
        public int ChiTietPhongChoId { get; set; }
        public int? PhongChoId { get; set; }
        public int? NguoiDungId { get; set; }
        public DateTime? ThoiGianVao { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public virtual PhongCho PhongCho { get; set; }
    }
}
