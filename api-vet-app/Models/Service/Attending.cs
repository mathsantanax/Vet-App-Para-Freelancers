namespace api_vet_app.Models.Service
{
    public class Attending
    {
        public int Id { get; set; }
        public bool IsVaccina { get; set; }
        public DateTime? CreatedAt { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }

        public int IdClient { get; set; }
        public int IdClientPet { get; set; }
        public int IdPayment { get; set; }
    }
}
