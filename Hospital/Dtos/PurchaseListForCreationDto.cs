using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Dtos
{
    public class PurchaseListForCreationDto : IValidatableObject
    {
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public int StaffId { get; set; }
        public string Comment { get; set; }
        //public ICollection<PurchaseListItemDto> PurchaseListItems { get; set; }
        public ICollection<PurchaseListItemForCreationDto> PurchaseListItems { get; set; }
            = new List<PurchaseListItemForCreationDto>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            decimal comp = 0;
            if(validationContext != null)
            {
                foreach (PurchaseListItemForCreationDto item in PurchaseListItems){
                    comp += item.ItemCount * item.Price;
                }
            }
            if (PurchaseListItems == null && Cost != 0 || comp != Cost)
            {
                yield return new ValidationResult(
                    "采购物品总量与总金额不符",
                    new[] { "PurchaseListForCreationDto" }
                    );
            }
        }
    }
}
