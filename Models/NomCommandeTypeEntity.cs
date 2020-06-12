using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_nom_commande_type")]
    public class NomCommandeTypeEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("commande_type_uid")]
        public int Id { get; set; }
        [Column("commande_type_code")]
        [Required]
        public string Code { get; set; }
        [Column("commande_type_libelle")]
        [Required]
        public string Libelle { get; set; }
        
    }
}