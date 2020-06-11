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
                
            
            // vers Entity
            CreateMap<ContactWithAdresseDTO, ContactEntity>()
                .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.Nom))
                .ForMember(dest => dest.Prenom, opt => opt.MapFrom(src => src.Prenom))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;
        }
    }
}