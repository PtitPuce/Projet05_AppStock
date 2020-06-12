using AutoMapper;
using AppStock.Models;
using AppStock.Models.DTO;

namespace AppStock.Models.Mappers.Profiles
{
    public class CommandeLigneProfile : Profile
    {
        public CommandeLigneProfile()
        {
            // vers DTO
            CreateMap<CommandeLigneEntity, CommandeLigneDTOWithId>();
                
            // vers Entity
            CreateMap<CommandeLigneDTOWithId, CommandeLigneEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(src => src.Article.Id))
                .ForMember(dest => dest.Quantite, opt => opt.MapFrom(src => src.Quantite))
                .ForAllOtherMembers(opt => opt.Ignore())
                ;
        }
    }
}