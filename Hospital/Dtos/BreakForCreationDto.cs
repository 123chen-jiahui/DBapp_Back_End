using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class BreakForCreationDto
    {
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string Reason { get; set; }
    }
}
