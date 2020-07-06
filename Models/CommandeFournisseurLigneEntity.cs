using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_commande_fournisseur_ligne")]
    public class CommandeFournisseurLigneEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("commande_fournisseur_ligne_uid")]
        public int Id { get; set; }
        [Column("commande_fournisseur_ligne_quantite")]
        public int Quantite { get; set; } = 1; // defaut 1

        [Column("commande_fournisseur_ligne_commande_fournisseur_uid")]
        public int CommandeFournisseurId { get; set; }
        [Column("commande_fournisseur_ligne_article_uid")]
        public int ArticleId { get; set; }

        public CommandeFournisseurEntity CommandeFournisseur { get; set; }
        public ArticleEntity Article { get; set; }
    }
}