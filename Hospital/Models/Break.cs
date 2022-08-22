using Stateless;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace Hospital.Models
{
    [Table("BREAKS")]
    public class Break
    {
        [Key]
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }

        [Required]
        [Column("FROM_TIME")]
        public DateTime FromTime { get; set; }

        [Required]
        [Column("TO_TIME")]
        public DateTime ToTime { get; set; }

        [Required]
        [Column("STAFF_ID")]
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }

        [Column("ADMIN_ID")]
        public int AdminId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Admin { get; set; }

        [Required]
        [Column("STATE")]
        public BreakState State { get; set; }

        [Required]
        [Column("Reason")]
        public string Reason { get; set; }
    }
}
