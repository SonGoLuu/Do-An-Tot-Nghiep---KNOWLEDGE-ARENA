using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class GoiCauHoi
    {
        public GoiCauHoi()
        {
            CauHois = new HashSet<CauHoi>();
        }

        public int GoiCauHoiId { get; set; }
        public int? Diem { get; set; }

        public virtual ICollection<CauHoi> CauHois { get; set; }
    }
}
