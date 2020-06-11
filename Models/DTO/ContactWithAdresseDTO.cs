namespace AppStock.Models.DTO
{
    public class ContactWithAdresseDTO
    {
        public string Nom { get;set; }
        public string Prenom { get;set; }
        public string Telephone { get;set; }

        public AdresseDTO Adresse {get;set;}
    }
}