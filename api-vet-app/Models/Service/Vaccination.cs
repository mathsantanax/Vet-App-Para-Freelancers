
namespace api_vet_app.Models.Service
{
    public class Vaccination
    {
        public int Id { get; set; }
        public DateTime? NextVaccination { get; set; }
        public int IdClientPet { get; set; }
        public int IdClient {  get; set; }
        public int IdAttending { get; set; }
    }
}
