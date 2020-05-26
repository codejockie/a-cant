using System;
using AutoMapper;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Entities;

namespace Hahn.ApplicatonProcess.May2020.Data.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<DTOMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class DTOMapper : Profile
    {
        public DTOMapper()
        {
            CreateMap<Applicant, ApplicantModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();
        }
    }
}
