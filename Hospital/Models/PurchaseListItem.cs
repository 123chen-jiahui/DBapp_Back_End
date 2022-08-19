﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    [Table("PURCHASE_LIST_ITEMS")]
    public class PurchaseListItem
    {
        // 产品编号，药品可对应于国药准字，医疗器械可对应产品批次号等
        //[Key]
        [Required]
        [MaxLength(20)]
        [Column("ITEM_ID")]
        public string ItemId { get; set; }

        // 采购物品的类型
        //[Key]
        [Required]
        [Column("PURCHASE_LIST_ITEM_TYPE")]
        public PurchaseListItemType PurchaseListItemType { get; set; }

        // 采购的物品的名字
        [MaxLength(100)]
        [Column("NAME")]
        public string Name { get; set; }

        // 采购物品单价
        [Column("PRICE",TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // 采购数量
        [Column("ITEM_COUNT")]
        public uint ItemCount { get; set; }

        // 生产商
        [MaxLength(50)]
        [Column("PRODUCER")]
        public string Producer { get; set; }

        // 物品描述，药品可对应于适应症等，医疗器械可对应于使用说明等
        [MaxLength]
        [Column("DESCRIPTION")]
        public string description { get; set; }

        // 引用PurchaseList的外码
        [Column("PURCHASE_LIST_ID")]
        public Guid PurchaseListId { get; set; }
        [ForeignKey("PurchaseListId")]
        public PurchaseList PurchaseList { get; set; }
    }
}
