using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_commande_ligne")]
    public class CommandeLigneEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("commande_ligne_uid")]
        public int Id { get; set; }
        [Column("commande_ligne_quantite")]
        public int Quantite { get; set; }

        [Column("commande_ligne_commande_uid")]
        public int CommandeId { get; set; }
        [Column("commande_ligne_article_uid")]
        public int ArticleId { get; set; }

        public CommandeEntity Commande { get; set; }
        public ArticleEntity Article { get; set; }
    }
}