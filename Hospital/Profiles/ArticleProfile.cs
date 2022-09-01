using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;

namespace Hospital.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleInfo, ArticleInfoDto>();
            CreateMap<ArticleForCreationDto,ArticleInfo>().
                ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                );
        }
    }
}
