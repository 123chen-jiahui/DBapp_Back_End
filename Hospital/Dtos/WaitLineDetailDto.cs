using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class WaitLineDetailDto
    {
        public int PatientId { get; set; }
        public PatientDetailDto Patient { get; set; }
    }
}
