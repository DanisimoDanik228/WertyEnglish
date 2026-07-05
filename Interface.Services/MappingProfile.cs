using AutoMapper;
using Application.Dto;
using Domain.Models;
using Models;

namespace Interface.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PairWord, PairWordDto>().ReverseMap();
            CreateMap<Dictionary, DictionaryDto>()
                .ForMember(dest => dest.CountWord, opt => opt.MapFrom(src => src.PairWords != null ? src.PairWords.Count : 0))
                .ReverseMap();
        }
    }
}
