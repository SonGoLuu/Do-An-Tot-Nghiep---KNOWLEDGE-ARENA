using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class ChiTietTungVongDau
    {
        public int VongDauId { get; set; }
        public int? PhongDauId { get; set; }
        public int? VongChoi { get; set; }
        public int? NguoiDungId { get; set; }
        public int? Diem { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public virtual PhongDau PhongDau { get; set; }
    }
}
