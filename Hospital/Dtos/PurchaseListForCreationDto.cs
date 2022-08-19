using System;
using System.Collections.Generic;

namespace Hospital.Dtos
{
    public class PurchaseListForCreationDto
    {
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public int StaffId { get; set; }
        public string Comment { get; set; }
        //public ICollection<PurchaseListItemDto> PurchaseListItems { get; set; }
        public ICollection<PurchaseListItemForCreationDto> PurchaseListItems { get; set; }
            = new List<PurchaseListItemForCreationDto>();
    }
}
