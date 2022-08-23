using Hospital.Models;
using System;

namespace Hospital.Dtos
{
    public class MedicalRecordDto
    {
        public Guid Id { get; set; }
        public int PatientId { get; set; }
        public int StaffId { get; set; }
        public DateTime DiagnosisTime { get; set; }     //诊断时间
        public string DiagnosticResult { get; set; }   //诊断结果
    }
}
