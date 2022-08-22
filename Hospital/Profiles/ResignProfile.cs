using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Hospital.Profiles
{

    public class ResignProfile : Profile
    {
        public string GetState(ResignState state)
        {
            if (state == ResignState.waitForApproval)
            {
                return "待审批";
            }
            else if (state == ResignState.agreed)
            {
                return "审批通过";
            }
            else
            {
                return "被拒绝";
            }
        }

        public ResignProfile()
        {
            CreateMap<Resign, ResignDto>()
             .ForMember(
                 dest => dest.State,
                 opt => opt.MapFrom(src => GetState(src.State))
            );
        }
    }
}
