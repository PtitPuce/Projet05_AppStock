using System.ComponentModel;
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
        [DisplayName("Prix unitaire")]
        public decimal PrixUnitaire { get; set; }
        [Column("article_famille_uid")]
        [DisplayName("Famille")]
        public int? ArticleFamilleId { get; set; }
        [Column("article_fournisseur_uid")]
        [DisplayName("Fournisseur")]
        public int FournisseurId { get; set; }
        [Column("article_tva_uid")]
        [Required(ErrorMessage="Dis donc !!!")]
        [DisplayName("Taux de TVA")]
        public int NomTypeTVAId { get; set; }
        [Column("article_threshold")]
        [DisplayName("Seuil stock critique")]
        public int Threshold { get; set; }
        
        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get;set; } = false;

        [DisplayName("Famille")]
        public ArticleFamilleEntity ArticleFamille { get; set; }
        [DisplayName("Fournisseur")]
        public FournisseurEntity Fournisseur { get; set; }
        [DisplayName("TVA")]
        public NomTypeTVAEntity NomTypeTVA { get; set; }
        public StockEntity Stock { get; set; }
    }
}