using AutoMapper;
using AppStock.Models;
using AppStock.Models.DTO;

namespace AppStock.Models.Mappers.Profiles
{
    public class AdresseProfile : Profile
    {
        public AdresseProfile()
        {
            // vers DTO
            CreateMap<AdresseEntity, AdresseDTO>();

            // vers Entity
            CreateMap<AdresseDTO, AdresseEntity>()
                .ForMember(dest => dest.Champ1, opt => opt.MapFrom(src => src.Champ1))
                .ForMember(dest => dest.Champ2, opt => opt.MapFrom(src => src.Champ2))
                .ForMember(dest => dest.CodePostal, opt => opt.MapFrom(src => src.CodePostal))
                .ForMember(dest => dest.Ville, opt => opt.MapFrom(src => src.Ville))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;
        }
    }
}