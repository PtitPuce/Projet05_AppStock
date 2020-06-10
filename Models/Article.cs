using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{

    [Table("app_article")]
    public class Article
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
        public int? ArticleFamilleID { get; set; }
        [Column("article_tva_uid")]
        [Required(ErrorMessage="Dis donc !!!")]
        public int NomTypeTVAID { get; set; }
        
        public ArticleFamille ArticleFamille { get; set; }
        public NomTypeTVA NomTypeTVA { get; set; }
        public Stock Stock { get; set; }
    }
}