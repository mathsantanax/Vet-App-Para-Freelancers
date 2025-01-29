using api_vet_app.Models.Persona;
using api_vet_app.Models.Pet;
using api_vet_app.Models.Service;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_vet_app.Data
{
    public class DbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    { 

        // Persona
        public DbSet<Client> Clients { get; set; }

        // Pet
        public DbSet<ClientPet> ClientsPet { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Raca> Racas { get; set; }

        // Service
        public DbSet<Attending> Attendings { get; set; }
        public DbSet<job> Job { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Vaccination> Vaccination { get; set; }
    }
}
