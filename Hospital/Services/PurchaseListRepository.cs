using Hospital.Database;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class PurchaseListRepository : IPurchaseListRepository
    {
        private  AppDbContext _context;

        public PurchaseListRepository (AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<PurchaseList> GetPurchaseListByIdAsync(Guid id)
        {
            return await _context.PurchaseLists.Include(t => t.PurchaseListItems).FirstOrDefaultAsync(pl => pl.Id == id);
        }


        public async Task<IEnumerable<PurchaseList>> GetPurchaseListsAsync(int staffid)
        {
            IQueryable<PurchaseList> res = _context.PurchaseLists.Include(t => t.PurchaseListItems);
            if(staffid>= 2000000L)
            {
                res=res.Where(t => t.StaffId==staffid);
            }
           
            return await res.ToListAsync();
        }

       
        public async Task<IEnumerable<PurchaseListItem>> GetPurchaseListItemsByIdAsync(Guid id)
        {
            return await _context.PurchaseListItems.Where(pli => pli.PurchaseListId == id).ToListAsync();
        }

        public async Task<bool> PurchaseListExistsAsync(Guid id)
        {
            return await _context.PurchaseLists.AnyAsync(pl => pl.Id == id);
        }

        public void AddPurchaseList(PurchaseList purchaseList)
        {
            if(purchaseList==null)
            {
                throw new ArgumentNullException(nameof(purchaseList));
            }
            _context.PurchaseLists.Add(purchaseList);
            //_context.SaveChanges();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void AddPurchaseListItem(Guid purchaseListId, PurchaseListItem purchaseListItem)
        {
            if (purchaseListId == Guid.Empty)
                throw new ArgumentNullException(nameof(purchaseListId));
            if(purchaseListItem==null)
                throw new ArgumentNullException(nameof(purchaseListItem));

            if (purchaseListItem.PurchaseListItemType == PurchaseListItemType.Medicine.ToString("G"))
            {
                var medicineitem = _context.Medicine.Where(t => t.Id == purchaseListItem.ItemId).FirstOrDefault();
                if(medicineitem == null)
                {
                    _context.Medicine.Add(new Medicine
                    {
                        Id = purchaseListItem.ItemId,
                        Name = purchaseListItem.Name,
                        Price = purchaseListItem.Price,
                        Inventory = (int)purchaseListItem.ItemCount,
                        Indications = purchaseListItem.description,
                        Manufacturer = purchaseListItem.Producer
                    });
                }
                else
                {
                    medicineitem.Inventory += purchaseListItem.ItemCount;
                }
            }
            else if(purchaseListItem.PurchaseListItemType == PurchaseListItemType.MedicialEquipment.ToString("G"))
            {
                var medicineequip = _context.MedicalEquipments.Where(me => me.Id == purchaseListItem.ItemId).FirstOrDefault();
                if(medicineequip == null)
                {
                    _context.MedicalEquipments.Add(new MedicalEquipment
                    {
                        Id = purchaseListItem.ItemId,
                        Name = purchaseListItem.Name,
                        Producer = purchaseListItem.Producer,
                        RoomId = "库房"
                    });
                }
                else
                {
                    // 按照目前的数据库结构，医疗器械不应重名
                    throw new Exception(nameof(purchaseListItem));
                }
            }
            purchaseListItem.PurchaseListId=purchaseListId;
            _context.PurchaseListItems.Add(purchaseListItem);
        }

       
    }
}
