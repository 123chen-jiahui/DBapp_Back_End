using System;
using System.Collections.Generic;
using Hospital.Models;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IPurchaseListRepository
    {
        // 获取所有的采购清单
        Task<IEnumerable<PurchaseList>> GetPurchaseListsAsync(int staffid);


        // 根据Id查找到一条采购记录
        Task<PurchaseList> GetPurchaseListByIdAsync(Guid id);

        // 查找是否存在该id的purchaseList
        Task<bool> PurchaseListExistsAsync(Guid id);

        // 获取详细的采购清单
        Task<IEnumerable<PurchaseListItem>> GetPurchaseListItemsByIdAsync(Guid id);

        // 添加采购清单
        void AddPurchaseList(PurchaseList purchaseList);

        // 添加采购清单的一项
        void AddPurchaseListItem(Guid purchaseListId,PurchaseListItem purchaseListItem);

        Task<bool> SaveAsync();
    }
}
