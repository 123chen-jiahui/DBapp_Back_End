using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Profiles
{
    public class ScheduleProfile : Profile
    {
        public string day2string(int day)
        {
            string[] stringDay = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return stringDay[day];
        }
        public ScheduleProfile()
        {
            CreateMap<ScheduleDto, Schedule>();
            CreateMap<Schedule, ScheduleDto>()
                .ForMember(
                    dest => dest.Day,
                    opt => opt.MapFrom(src => day2string(src.Day))
            );
            CreateMap<ScheduleOfOneDayForCreationDto, Schedule>();
            CreateMap<ScheduleOfOneDayForCreationDto, ScheduleDto>();
            CreateMap<ScheduleForUpdationDto, Schedule>();
        }
    }
}
