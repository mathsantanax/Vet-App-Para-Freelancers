namespace api_vet_app.Models.Service
{
    public class ServiceItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal Amount { get; set; }
        public int IdJob {  get; set; }
        public int IdAttending { get; set; }
    }
}
