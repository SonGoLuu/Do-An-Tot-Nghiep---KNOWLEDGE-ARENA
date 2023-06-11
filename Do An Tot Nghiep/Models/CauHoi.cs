using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class CauHoi
    {
        public int CauHoiId { get; set; }
        public int? VongCauHoi { get; set; }
        public string NoiDung { get; set; }
        public int? LinhVucId { get; set; }
        public string DapAn { get; set; }
        public int? SoKyTu { get; set; }
        public int? ChuongNgaiVatId { get; set; }
        public int? MucDo { get; set; }
        public byte[] Anh { get; set; }
        public int? GoiCauHoiId { get; set; }

        public virtual ChuongNgaiVat ChuongNgaiVat { get; set; }
        public virtual GoiCauHoi GoiCauHoi { get; set; }
        public virtual LinhVucCauHoi LinhVuc { get; set; }
    }
}
