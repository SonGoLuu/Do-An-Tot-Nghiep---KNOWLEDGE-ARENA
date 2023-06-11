using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class BacXepHang
    {
        public BacXepHang()
        {
            XepHangs = new HashSet<XepHang>();
        }

        public int BacXepHangId { get; set; }
        public string BacHang { get; set; }

        public virtual ICollection<XepHang> XepHangs { get; set; }
    }
}
