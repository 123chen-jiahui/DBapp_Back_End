using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class MedicalRecordForCreationDto
    {
        public int PatientId { get; set; }
        public int StaffId { get; set; }
        public string DiagnosticResult { get; set; }   //诊断结果
    }
}
