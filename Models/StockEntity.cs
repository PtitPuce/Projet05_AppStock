using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_stock")]
    public class StockEntity
    {
        [Key]
        [Column("article_uid")]
        public int ArticleID { get; set; }
        [Column("stock_quantite")]
        public int Quantite { get; set; }

        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get;set; } = false;
        
        public ArticleEntity Article { get; set; }
    }
}