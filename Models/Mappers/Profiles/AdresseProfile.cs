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
            CreateMap<AdresseEntity, AdresseDTOWithId>();

            // vers Entity
            CreateMap<AdresseDTO, AdresseEntity>()
                .ForMember(dest => dest.Champ1, opt => opt.MapFrom(src => src.Champ1))
                .ForMember(dest => dest.Champ2, opt => opt.MapFrom(src => src.Champ2))
                .ForMember(dest => dest.CodePostal, opt => opt.MapFrom(src => src.CodePostal))
                .ForMember(dest => dest.Ville, opt => opt.MapFrom(src => src.Ville))
                .ForMember(dest => dest.Pays, opt => opt.MapFrom(src => src.Pays))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;
            CreateMap<AdresseDTOWithId, AdresseEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Champ1, opt => opt.MapFrom(src => src.Champ1))
                .ForMember(dest => dest.Champ2, opt => opt.MapFrom(src => src.Champ2))
                .ForMember(dest => dest.CodePostal, opt => opt.MapFrom(src => src.CodePostal))
                .ForMember(dest => dest.Ville, opt => opt.MapFrom(src => src.Ville))
                .ForMember(dest => dest.Pays, opt => opt.MapFrom(src => src.Pays))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;
        }
    }
}