using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class ScheduleDto
    {
        public int StaffId { get; set; }
        // public int Day { get; set; }
        public string Day { get; set; }
        public int TimeSlotId { get; set; }
        public string RoomId { get; set; }
        public TimeSlotDto TimeSlot { get; set; }
        public int Capacity { get; set; }
    }
}
