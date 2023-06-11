using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class XepHang
    {
        public int XepHangId { get; set; }
        public int? NguoiDungId { get; set; }
        public int? BacXepHangId { get; set; }
        public int? DiemNangLuc { get; set; }

        public virtual BacXepHang BacXepHang { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
