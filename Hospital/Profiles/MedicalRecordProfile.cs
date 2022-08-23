using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Profiles
{
    public class MedicalRecordProfile : Profile
    {
        public MedicalRecordProfile()
        {
            CreateMap<MedicalRecord, MedicalRecordDto>();
            CreateMap<MedicalRecordForCreationDto,MedicalRecord >()
                .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                dest => dest.DiagnosisTime,
                opt => opt.MapFrom(src => DateTime.UtcNow)
                );
        }
    }
}
