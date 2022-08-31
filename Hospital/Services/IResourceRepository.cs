using Hospital.Helper;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IResourceRepository
    {
        Task<int> CountMedinesAsync(string keyWord);
        Task<Medicine> GetMedicineAsync(string medicineId);
        Task<PaginationList<Medicine>> GetMedicinesAsync(string keyWord, int pageNumber, int pageSize);
        void AddShoppingCartItem(LineItem lineItem);
        Task<LineItem> GetShoppingCartItemByItemIdAsync(int lineItemId);
        void DeleteShoppingCartItem(LineItem lineItem);
        Task<IEnumerable<LineItem>> GetShoppingCartItemsByItemIdListAsync(IEnumerable<int> lineItemIds);
        void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems);
        Task<ICollection<LineItem>> GetShoppingCartItemsByShoppingCartIdAsync(Guid shoppingCartId);
        Task<Order> GetOrderByOrderIdAsync(Guid orderId);

        // 科室资源
        Task<IEnumerable<Department>> GetDepartments();
        Task<IEnumerable<Department>> GetDepartmentsDetail();

        Task<bool> SaveAsync();
        Task<MedicalRecord> GetMedicalRecordByMedicalRecordId(Guid medicalRecordId);
    }
}
