using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class ChiTietPhongDau
    {
        public int ChiTietPhongDauId { get; set; }
        public int? PhongDauId { get; set; }
        public int? NguoiDungId { get; set; }
        public int? TongDiem { get; set; }
        public int? ThuHang { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public virtual PhongDau PhongDau { get; set; }
    }
}
