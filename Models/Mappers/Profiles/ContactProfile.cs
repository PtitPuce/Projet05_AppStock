using AutoMapper;
using AppStock.Models;
using AppStock.Models.DTO;

namespace AppStock.Models.Mappers.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            // vers DTO
            CreateMap<ContactEntity, ContactWithAdresseDTO>();
            CreateMap<ContactEntity, ContactWithAdresseDTOAndId>();
                
            // vers Entity
            CreateMap<ContactWithAdresseDTO, ContactEntity>()
                .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.Nom))
                .ForMember(dest => dest.Prenom, opt => opt.MapFrom(src => src.Prenom))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;
            CreateMap<ContactWithAdresseDTOAndId, ContactEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AdresseId, opt => opt.MapFrom(src => src.Adresse.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.Nom))
                .ForMember(dest => dest.Prenom, opt => opt.MapFrom(src => src.Prenom))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;
        }
    }
}