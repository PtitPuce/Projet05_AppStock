using AppStock.Models;
using Microsoft.EntityFrameworkCore;
namespace AppStock.Data
{
    public static class ApplicationDbContextQueryFilters
    {
        public static void QueryFilters(this ModelBuilder builder)
        {
            builder.Entity<ArticleEntity>().HasQueryFilter(e=>!e.IsDeleted);
            builder.Entity<ArticleFamilleEntity>().HasQueryFilter(e=>!e.IsDeleted);
            builder.Entity<NomTypeTVAEntity>().HasQueryFilter(e=>!e.IsDeleted);
            //builder.Entity<StockEntity>().HasQueryFilter(e=>!e.IsDeleted);
        }
    }
}