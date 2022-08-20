using Hospital.Models;
using System;

namespace Hospital.Dtos
{
    public class PurchaseListItemForCreationDto
    {
        public string ItemId { get; set; }
        public string PurchaseListItemType { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint ItemCount { get; set; }
        public string Producer { get; set; }
        public string description { get; set; }
        public Guid PurchaseListId { get; set; }
    }
}
