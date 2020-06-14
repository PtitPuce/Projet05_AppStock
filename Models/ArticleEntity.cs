using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{

    [Table("app_article")]
    public class ArticleEntity
    {
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column("article_uid")]
        public int Id { get; set; }
        [Column("article_code")]
        [Required]
        public string Code { get; set; }
        [Column("article_libelle")]
        [Required]
        public string Libelle { get; set; }
        [Column("article_pu")]
        public decimal PrixUnitaire { get; set; }
        [Column("article_famille_uid")]
        public int? ArticleFamilleId { get; set; }
        [Column("article_tva_uid")]
        [Required(ErrorMessage="Dis donc !!!")]
        public int NomTypeTVAId { get; set; }
        
        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get;set; } = false;

        public ArticleFamilleEntity ArticleFamille { get; set; }
        public NomTypeTVAEntity NomTypeTVA { get; set; }
        public StockEntity Stock { get; set; }
    }
}