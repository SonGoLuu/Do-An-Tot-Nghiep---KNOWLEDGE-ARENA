using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class ChuongNgaiVat
    {
        
        public int ChuongNgaiVatId { get; set; }
        public int? SoKyTu { get; set; }
        public string DapAn { get; set; }
        public byte[] Anh { get; set; }

       
    }
}
