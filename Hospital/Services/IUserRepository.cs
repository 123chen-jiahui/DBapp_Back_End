﻿using Hospital.Helper;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IUserRepository
    {
        void DeleteStaff(Staff staff);
        Task<bool> PatientExistsByGlobalIdAsync(string patientGlobalId); // 根据身份证号查找病人是否存在
        Task<bool> PatientExistsByPatientIdAsync(int patientId);
        Task<bool> StaffExistsByGlobalIdAsync(string staffGlobalId);
        // bool StaffExistsByStaffId(string staffId);
        void AddPatient(Patient patient);
        void AddStaff(Staff staff);
        void AddRegistration(Registration reg);
        Task<Patient> GetPatientByPatientIdAsync(int patientId); // 根据病人Id获取病人model
        Task<Patient> GetPatientDetailByPatientIdAsync(int patientId); // 获取详细信息（诊疗记录）
        Task<IEnumerable<Patient>> GetPatientsByNameAsync(string keyword);
        Task<Staff> GetStaffByStaffIdAsync(int staffId);
        Task<PaginationList<Staff>> GetStaffsAsync(int departmentId, int pageNumber, int pageSize);
        Task<ShoppingCart> GetShoppingCartByPatientIdAsync(int patientId);
        void CreateShoppingCart(ShoppingCart shoppingCart);
        Task AddOrderAsync(Order order);
        Task<Staff> GetAdminByAsync();
        Task<bool> SaveAsync();
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordByMedicalRecordIdAsync(int patientId);
        void AddMedicalRecord(MedicalRecord medicalRecord);
        Task<PaginationList<Order>> GetOrdersByPatientIdAsync(int patientId, int pageNumber, int pageSize);
        Task<int> CountOrdersAsync(int patientId);
        Task<int> CountStaffAsync(int departmentId);
    }
}
