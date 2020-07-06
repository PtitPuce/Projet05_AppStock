using AutoMapper;
using AppStock.Models;
using AppStock.Models.DTO;

namespace AppStock.Models.Mappers.Profiles
{
    public class CommandeProfile : Profile
    {
        public CommandeProfile()
        {
            // vers DTO
            CreateMap<CommandeEntity, CommandeDTO>();
                
            // vers Entity
            CreateMap<CommandeDTO, CommandeEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.ContactId))
                .ForMember(dest => dest.NomCommandeStatutId, opt => opt.MapFrom(src => src.NomCommandeStatutId))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Commentaire, opt => opt.MapFrom(src => src.Commentaire))
                .ForMember(dest => dest.CommandeLignes, opt => opt.MapFrom(src => src.CommandeLignes))

                .ForAllOtherMembers(opt => opt.Ignore())
                ;
        }
    }
}