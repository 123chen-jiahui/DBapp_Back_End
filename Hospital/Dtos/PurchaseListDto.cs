using System;
using System.Collections.Generic;

namespace Hospital.Dtos
{
    public class PurchaseListDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public int StaffId { get; set; }
        public string Comment { get; set; }
        public ICollection<PurchaseListItemDto> PurchaseListItems { get; set; }
    }
}
