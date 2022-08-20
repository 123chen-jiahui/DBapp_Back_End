using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Profiles
{
    public class BreakProfile: Profile
    {
        public string GetState(BreakState state)
        {
            if (state == BreakState.waitForApproval)
            {
                return "待审批";
            }
            else if (state == BreakState.agreed)
            {
                return "审批通过";
            }
            else 
            {
                return "被拒绝";
            }
        }

        public BreakProfile()
        {
            CreateMap<Break, BreakDto>()
             .ForMember(
                 dest => dest.State,
                 opt => opt.MapFrom(src => GetState(src.State))
            );
        }

    }
}
