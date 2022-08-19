using System;
using System.Collections.Generic;
using Hospital.Models;

namespace Hospital.Services
{
    public interface IPurchaseListRepository
    {
        // 获取所有的采购清单
        IEnumerable<PurchaseList> GetPurchaseLists(int staffid);

        // 根据Id查找到一条采购记录
        PurchaseList GetPurchaseListById(Guid id);

        // 查找是否存在该id的purchaseList
        bool PurchaseListExists(Guid id);

        // 获取详细的采购清单
        IEnumerable<PurchaseListItem> GetPurchaseListItemsById(Guid id);

        // 添加采购清单
        void AddPurchaseList(PurchaseList purchaseList);

        // 添加采购清单的一项
        void AddPurchaseListItem(Guid purchaseListId,PurchaseListItem purchaseListItem);

        bool Save();
    }
}
