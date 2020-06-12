using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_nom_commande_statut")]
    public class NomCommandeStatutEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("commande_statut_uid")]
        public int Id { get; set; }
        [Column("commande_statut_code")]
        [Required]
        public string Code { get; set; }
        [Column("commande_statut_libelle")]
        [Required]
        public string Libelle { get; set; }
    }
}