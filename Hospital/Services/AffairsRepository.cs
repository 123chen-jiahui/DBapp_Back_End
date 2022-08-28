using Hospital.Database;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class AffairsRepository : IAffairsRepository
    {
        private readonly AppDbContext _context;

        public AffairsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TimeSlot> GetTimeSlotAsync(int timeSlotId)
        {
            return await _context.TimeSlots.Where(ts => ts.Id == timeSlotId).FirstOrDefaultAsync();
        }
        public void AddTimeSlot(TimeSlot timeSlot)
        {
            _context.TimeSlots.Add(timeSlot);
        }

        public async Task<IEnumerable<TimeSlot>> GetTimeSlotsAsync()
        {
            return await _context.TimeSlots.ToListAsync();
        }

        public async Task<bool> TimeSlotExistsAsync(TimeSlot timeSlot)
        {
            return await _context.TimeSlots.AnyAsync(ts => ts.StartTime == timeSlot.StartTime && ts.EndTime == timeSlot.EndTime);
        }
        public async Task<bool> ScheduleExistsAsync(int staffId)
        {
            return await _context.Schedules.AnyAsync(s_ts => s_ts.StaffId == staffId);
        }

        public async Task<IEnumerable<Schedule>> GetScheduleAsync(int staffId)
        {
            return await _context.Schedules.Include(s => s.TimeSlot).Where(s_ts => s_ts.StaffId == staffId).ToListAsync();
        }

        public async Task<Schedule> GetScheduleOfOneDay(int staffId, int day)
        {
            return await _context.Schedules.Where(s_ts => s_ts.StaffId == staffId && s_ts.Day == day).FirstOrDefaultAsync();
        }
        public void AddSchedule(Schedule staff_TimeSlot)
        {
            _context.Schedules.Add(staff_TimeSlot);
        }

        // 挂号
        public async Task AddRegistrationAsync(Registration registration)
        {
            await _context.Registrations.AddAsync(registration);
        }
        public async Task AddBreakAsync(Break breakk)
        {
            await _context.Breaks.AddAsync(breakk);
        }
        public async Task AddResignAsync(Resign resign)
        {
            await _context.Resigns.AddAsync(resign);
        }
        public async Task<Registration> GetRegistrationByRegistrationId(Guid registrationId)
        {
            return await _context.Registrations.Where(r => r.Id == registrationId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Registration>> GetRegistrationsAsync(int patientId)
        {
            return await _context.Registrations.Where(r => r.PatientId == patientId).ToListAsync();
        }

        // waitline
        public async Task AddWaitLineAsync(WaitLine waitLine)
        {
            await _context.WaitLines.AddAsync(waitLine);
        }

        public async Task<IEnumerable<WaitLine>> GetWaitLinesAsync(int staffId, int day)
        {
            IQueryable<WaitLine> res = _context.WaitLines.Where(wl => wl.StaffId == staffId && wl.Day == day);
            // var res = await _context.WaitLines.Where(wl => wl.StaffId == staffId && wl.Day == day).ToListAsync();
            res = res.OrderBy(r => r.Order);
            return await res.ToListAsync();
            // return await _context.WaitLines.Where(wl => wl.StaffId == staffId && wl.Day == day).ToListAsync();
        }
        public async Task<IEnumerable<Break>> GetBreaksByStaffIdAsync(int staffId)
        {
            IQueryable<Break> res = _context.Breaks.Where(wl => wl.StaffId == staffId);
            res = res.OrderBy(r => r.FromTime);
            return await res.ToListAsync();
        }
        public async Task<IEnumerable<Break>> GetUnapprovedBreaksAsync()
        {
            IQueryable<Break> res = _context.Breaks.Where(wl => wl.State==BreakState.waitForApproval);
            res = res.OrderBy(r => r.FromTime);
            return await res.ToListAsync();
        }

        public async Task<Break>GetBreakByIdAsync(string Id)
        {
            return await _context.Breaks.Where(wl => wl.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Resign>> GetResignsByStaffIdAsync(int staffId)
        {
            IQueryable<Resign> res = _context.Resigns.Where(wl => wl.StaffId == staffId);
            res = res.OrderBy(r => r.Time);
            return await res.ToListAsync();
        }

        public async Task<IEnumerable<Resign>> GetUnapprovedResignsAsync()
        {
            IQueryable<Resign> res = _context.Resigns.Where(wl => wl.State ==ResignState.waitForApproval);
            res = res.OrderBy(r => r.Time);
            return await res.ToListAsync();
        }
        public async Task<Resign> GetResignByIdAsync(string Id)
        {
            return await _context.Resigns.Where(wl => wl.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }


    }
}
