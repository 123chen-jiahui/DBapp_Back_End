using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class MedicalRecordForCreationDto
    {
        public int PatientId { get; set; }
        public string Record { get; set; }
    }
}
