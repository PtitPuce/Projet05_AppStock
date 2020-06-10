using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_stock")]
    public class Stock
    {
        [Key]
        [Column("article_uid")]
        public int ArticleID { get; set; }
        [Column("stock_quantite")]
        public int Quantite { get; set; }
        
        public Article Article { get; set; }
    }
}