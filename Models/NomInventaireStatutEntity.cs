using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_nom_inventaire_statut")]
    public class NomInventaireStatutEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("inventaire_statut_uid")]
        public int Id { get; set; }
        [Column("inventaire_statut_code")]
        [Required]
        public string Code { get; set; }
        [Column("inventaire_statut_libelle")]
        [Required]
        public string Libelle { get; set; }
    }
}