﻿using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Profiles
{
    public class UserProfile : Profile
    {
        public int GetAge(DateTime birthday)
        {
            DateTime n = DateTime.Now; // To avoid a race condition around midnight
            int age = n.Year - birthday.Year;

            if (n.Month < birthday.Month || (n.Month == birthday.Month && n.Day < birthday.Day))
                age--;

            return age;
        }

        public string GetGender(Gender gender)
        {
            if (gender == Gender.female)
            {
                return "女";
            }
            else
            {
                return "男";
            }
        }
        public string GetRole(Role role)
        {
            if (role == Role.Admin)
            {
                return "管理员";
            }
            else if(role == Role.Doctor)
            {
                return "医生";
            }
            else 
            {
                return "订单前台";
            }
        }

        public string GetPosition(Position position)
        {
            if (position == Position.fuzhuren)
            {
                return "副主任";
            }
            else if (position == Position.shixi)
            {
                return "实习医生";
            }
            else if (position == Position.zhuren)
            {
                return "主任";
            }
            else
            {
                return "主治医生";
            }
        }
        public UserProfile()
        {
            CreateMap<RegisterForPatientDto, Patient>();
            CreateMap<RegisterForStaffDto, Staff>();
            CreateMap<Staff, StaffDto>();
            CreateMap<Staff, StaffDto>()
                .ForMember(
                    dest => dest.age,
                    opt => opt.MapFrom(src => GetAge(src.Birthday))
                    )
                .ForMember(
                    dest => dest.Gender,
                    opt => opt.MapFrom(src => GetGender(src.Gender))
                    )
                 .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => GetRole(src.Role))
                    )
                 .ForMember(
                    dest => dest.Position,
                    opt => opt.MapFrom(src => GetPosition(src.Position))

            );
            CreateMap<Staff, StaffWithScheduleDto>()
                .ForMember(
                    dest => dest.age,
                    opt => opt.MapFrom(src => GetAge(src.Birthday))
                    )
                .ForMember(
                    dest => dest.Gender,
                    opt => opt.MapFrom(src => GetGender(src.Gender))
                    )
                 .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => GetRole(src.Role))
                    )
                 .ForMember(
                    dest => dest.Position,
                    opt => opt.MapFrom(src => GetPosition(src.Position))

            );
            CreateMap<GuahaoDto, Registration>();

            CreateMap<Patient, PatientDto>()
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => GetAge(src.Birthday))
                )
                .ForMember(
                    dest => dest.Gender,
                    opt => opt.MapFrom(src => GetGender(src.Gender))
            );
            CreateMap<Patient, PatientDetailDto>()
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => GetAge(src.Birthday))
                )
                .ForMember(
                    dest => dest.Gender,
                    opt => opt.MapFrom(src => GetGender(src.Gender))
                );
        }
    }
}
