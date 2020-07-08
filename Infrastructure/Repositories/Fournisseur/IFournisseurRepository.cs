using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;


namespace AppStock.Infrastructure.Repositories.Fournisseur
{
    public interface IFournisseurRepository
    {
        #pragma warning disable 1591

         Task<FournisseurEntity> GetOneByIdAsync(int id);
         
        #pragma warning restore 1591

    }
}