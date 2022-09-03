using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IAffairsRepository
    {
        Task<TimeSlot> GetTimeSlotAsync(int timeSlotId);
        Task<IEnumerable<TimeSlot>> GetTimeSlotsAsync();
        void AddTimeSlot(TimeSlot timeSlot);
        Task<bool> TimeSlotExistsAsync(TimeSlot timeSlot);
        Task<bool> ScheduleExistsAsync(int staffId);

        Task<IEnumerable<Schedule>> GetScheduleAsync(int staffId);
        Task<Schedule> GetScheduleOfOneDay(int staffId, int day);
        void AddSchedule(Schedule staff_TimeSlot);

        // 挂号
        Task AddRegistrationAsync(Registration registration);
      
        Task<Registration> GetRegistrationByRegistrationId(Guid registrationId);
        Task<IEnumerable<Registration>> GetRegistrationsAsync(int patientId); // 查看某个病人的历史挂号信息
        // Task<Registration> GetRegistrationByRegistrationId(Guid registrationId);

        // waitline
        Task AddWaitLineAsync(WaitLine waitLine);
        Task<IEnumerable<WaitLine>> GetWaitLinesAsync(int staffId, int day);
        Task<IEnumerable<WaitLine>> GetWaitLinesDetailAsync(int staffId, int day);


        //请假
        Task AddBreakAsync(Break breakk);
        Task<IEnumerable<Break>> GetBreaksByStaffIdAsync(int staffId);
        Task<IEnumerable<Break>> GetUnapprovedBreaksAsync();
        Task<Break> GetBreakByIdAsync(string Id);

        //辞职
        Task AddResignAsync(Resign resign);
        Task<IEnumerable<Resign>> GetResignsByStaffIdAsync(int staffId);
        Task<IEnumerable<Resign>> GetUnapprovedResignsAsync();
        Task<Resign> GetResignByIdAsync(string Id);
        Task<bool> SaveAsync();
    }
}
