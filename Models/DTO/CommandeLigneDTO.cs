using AppStock.Models;
namespace AppStock.Models.DTO
{
    public class CommandeLigneDTOWithId
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int Quantite { get; set; }
        public ArticleEntity Article {get;set;}

        // virtuels
        public double TotalTVA {get;set;}
        public double TotalHT {get;set;}
        public double TotalTTC {get;set;}

    }
}