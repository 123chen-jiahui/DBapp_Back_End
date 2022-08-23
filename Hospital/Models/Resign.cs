using Stateless;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    [Table("RESIGNS")]
    public class Resign
    {
        [Key]
        [Required]
        [Column("ID")]
        public string Id { get; set; }

        [Required]
        [Column("TIME")]
        public DateTime Time { get; set; }

        [Required]
        [Column("STAFF_ID")]
        public int StaffId { get; set; }
        // [ForeignKey("StaffId")]
        public Staff Staff { get; set; }

        [Column("ADMIN_ID")]
        public int AdminId { get; set; }
        // [ForeignKey("StaffId")]
        public Staff Admin { get; set; }

        [Required]
        [Column("STATE")]
        public ResignState State { get; set; }

        [Required]
        [Column("REASON")]
        public string Reason { get; set; }
    }
}
