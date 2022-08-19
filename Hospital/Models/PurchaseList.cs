using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    [Table("PURCHASE_LISTS")]
    public class PurchaseList
    {
        // 主键 采购清单ID
        [Key]
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }

        // 清单创建日期
        [Required]
        [Column("DATE")]
        public DateTime Date { get; set; }

        // 此次采购总的花费
        [Required]
        [Column("COST",TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        // 引用Staff的外码
        [Required]
        [Column("STAFF_ID")]
        public int StaffId { get; set; }
        
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }

        // 备注
        [MaxLength(50)]
        [Column("COMMENT")]
        public string Comment { get; set; }

        // 采购内容
        public ICollection<PurchaseListItem> PurchaseListItems { get; set; }
    }
}
