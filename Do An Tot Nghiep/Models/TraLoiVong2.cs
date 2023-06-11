using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Models
{
    public class TraLoiVong2
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Answer { get; set; }
        public int Result { get; set; }
        public int PhongDauId { get; set; }
    }
}
