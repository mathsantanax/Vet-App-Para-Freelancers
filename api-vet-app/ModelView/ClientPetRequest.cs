namespace api_vet_app.ModelView
{
    public record ClientPetRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Microchip { get; set; }
        public int Idade { get; set; }
        public float Peso { get; set; }
        public string? Sexo { get; set; }
        public int IdRaca { get; set; }
        public int IdEspecie { get; set; }
        public int IdClient { get; set; }
        public string? VetId { get; set; }
    }
}
