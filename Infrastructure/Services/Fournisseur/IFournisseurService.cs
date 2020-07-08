using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.Fournisseur
{
    public interface IFournisseurService
    {
        #pragma warning disable 1591
        
        Task<FournisseurEntity> GetOneById(int id);

        #pragma warning restore 1591         
    }
}