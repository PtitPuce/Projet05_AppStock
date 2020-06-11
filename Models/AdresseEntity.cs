using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_adresse")]
    public class AdresseEntity
    {
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column("adresse_uid")]
        public int Id { get; set; }

        [Column("adresse_champ_1")]
        public string Champ1 { get;set; }
        [Column("adresse_champ_2")]
        public string Champ2 { get;set; }
        [Column("adresse_code_postal")]
        public string CodePostal { get;set; }
        [Column("adresse_ville")]
        public string Ville { get;set; }
        [Column("adresse_pays")]
        public string Pays { get;set; }
    }
}