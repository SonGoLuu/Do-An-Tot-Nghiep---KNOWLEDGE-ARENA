using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class TraLoiCNV
    {
        public int TraLoiId { get; set; }
        public int PhongDauId { get; set; }
        public int ChuongNgaiVatId { get; set; }
        public int NguoiDungId { get; set; }
        public string Answer { get; set; }
        public DateTime ThoiGian { get; set; }
        public string TrangThaiNguoiChoi { get; set; }
        public string HoVaTen { get; set; }
    }
}
