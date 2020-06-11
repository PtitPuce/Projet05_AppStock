using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.Adresse
{
    public interface IAdresseService
    {
        #pragma warning disable 1591
        Task<AdresseEntity> GetOneById(int id);
        Task<AdresseEntity> Add(AdresseEntity item);
        Task<AdresseEntity> Update(AdresseEntity item);
        Task DeleteById(int id);
        bool Exist(int id);
        

        #pragma warning restore 1591         
    }
}