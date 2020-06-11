namespace AppStock.Models.DTO
{
    public class ContactWithAdresseDTOAndId
    {
        public int Id { get;set; }
        public int AdresseId { get;set; }
        public string UserId { get;set; }
        
        public string Nom { get;set; }
        public string Prenom { get;set; }
        public string Telephone { get;set; }
        public AdresseDTOWithId Adresse {get;set;}
    }
}