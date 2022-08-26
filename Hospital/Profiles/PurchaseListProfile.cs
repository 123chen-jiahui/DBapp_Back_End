using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;

namespace Hospital.Profiles
{
    public class PurchaseListProfile : Profile
    {
        public PurchaseListProfile()
        {
            CreateMap<PurchaseList, PurchaseListDto>();

            CreateMap<PurchaseListForCreationDto, PurchaseList>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                );
        }
    }
}
