using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("MEDICALRECORD")]
    public class MedicalRecord
    {
        // [Key]
        [Column("ID")]
        public Guid Id { get; set; }
        // [Key]
        [Column("PATIENT_ID")]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } // 并不会到数据库中，而是由EF保存，表示外键关系
        // [Key]
        [Column("STAFF_ID")]
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; } // 并不会到数据库中，而是由EF保存，表示外键关系

        [Column("DIAGNOSISTIME")]     //诊断时间
        public DateTime DiagnosisTime { get; set; }

        [Column("DIAGNOSTICRESULT")]  //诊断结果
        public string DiagnosticResult { get; set; }
      
    }

}
