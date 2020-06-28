using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.InventaireLigne
{
    public interface IInventaireLigneService
    {
        #pragma warning disable 1591
        Task<IEnumerable<InventaireLigneEntity>> GetAll();
        Task<InventaireLigneEntity> GetOneById(int id);
        Task<InventaireLigneEntity> AddArticle(InventaireEntity Inventaire, int id_article);
        Task<InventaireLigneEntity> Update(InventaireLigneEntity item);
        Task DeleteById(int id);
        bool Exist(int id);
        
        #pragma warning restore 1591         
    }
}