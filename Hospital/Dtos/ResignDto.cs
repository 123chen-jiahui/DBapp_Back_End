using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class ResignDto
    {
            public int staffId { get; set; }   //可能需要前端转化成name
            public DateTime Time { get; set; }
            public String Reason { get; set; }
            public String State { get; set; }
    }
}
