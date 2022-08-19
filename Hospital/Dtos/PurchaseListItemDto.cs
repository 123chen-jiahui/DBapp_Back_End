using Hospital.Models;
using System;

namespace Hospital.Dtos
{
    public class PurchaseListItemDto
    {
        public string Itemid { get; set; }
        public PurchaseListItemType PurchaseListItemType { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint ItemCount { get; set; }
        public string Producer { get; set; }
        public string description { get; set; }
        public Guid PurchaseListId { get; set; }
    }
}
