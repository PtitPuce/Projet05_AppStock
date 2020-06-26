using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_inventaire_ligne")]
    public class InventaireLigneEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("inventaire_ligne_uid")]
        public int Id { get; set; }
        [Column("inventaire_ligne_inventaire_uid")]
        public int InventaireId { get; set; }
        [Column("inventaire_ligne_article_uid")]
        public int ArticleId { get; set; }
        [Column("inventaire_ligne_quantite_theorique")]
        public int QuantiteTheorique { get; set; }
        [Column("inventaire_ligne_quantite_comptee")]
        public int QuantiteComptee { get; set; }

        public InventaireEntity Inventaire { get; set; }
        public ArticleEntity Article{ get; set; }
    }
}