using AutoMapper;
using AppStock.Models.Mappers.Profiles;

namespace AppStock.Models.Mappers
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg => 
                {
                    cfg.AddProfile( new AdresseProfile() );
                    cfg.AddProfile( new ContactProfile() );
                    cfg.AddProfile( new CommandeProfile() );
                    //cfg.AddProfile( new CommandeLigneProfile() );
                }
            );
        }
    }
}