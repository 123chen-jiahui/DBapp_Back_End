using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;

namespace Hospital.Profiles
{
    public class PurchaseListItemProfile : Profile
    {
        public PurchaseListItemProfile()
        {
            CreateMap<PurchaseListItem, PurchaseListItemDto>();
            CreateMap<PurchaseListItemForCreationDto, PurchaseListItem>();
        }
    }
}
