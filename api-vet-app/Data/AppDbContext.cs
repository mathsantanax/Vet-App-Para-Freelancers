using api_vet_app.Models.Persona;
using api_vet_app.Models.Pet;
using api_vet_app.Models.Service;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace api_vet_app.Data
{
    public class AppDbContext : IdentityDbContext<User>
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        // Persona
        public DbSet<Client> Clients { get; set; }

        // Pet
        public DbSet<ClientPet> ClientsPet { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Raca> Racas { get; set; }

        // Service
        public DbSet<Attending> Attendings { get; set; }
        public DbSet<Lab> Lab { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Vaccination> Vaccination { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    #region Relacionamento Client 
        //    modelBuilder.Entity<Client>()
        //        .HasOne(c => c.VetId)
        //        .WithMany() // Um Vet pode ter vários clientes
        //        .HasForeignKey(c => c.VetId) 
        //        .OnDelete(DeleteBehavior.Cascade); // exclusão em cascata
        //    #endregion

        //    #region Relacionamentos ClientPet
        //    modelBuilder.Entity<ClientPet>()
        //    .HasOne(p => p.VetId)
        //        .WithMany() // Um Vet pode ter vários pet cadastrados
        //        .HasForeignKey(p => p.VetId)
        //        .OnDelete(DeleteBehavior.Cascade); // exclusão casacata
            
        //    modelBuilder.Entity<ClientPet>()
        //        .HasOne(p => p.IdClient)
        //        .WithMany() // Um cliete pode ter vários pets
        //        .HasForeignKey(p => p.IdClient)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<ClientPet>()
        //        .HasOne(p => p.IdEspecie)
        //        .WithMany() // Um especie pode ter vários pets
        //        .HasForeignKey(p => p.IdEspecie )
        //        .OnDelete(DeleteBehavior.SetNull);
            
        //    modelBuilder.Entity<ClientPet>()
        //        .HasOne(p => p.IdRaca)
        //        .WithMany() // Um Raca pode ter vários pets
        //        .HasForeignKey(p => p.IdRaca )
        //        .OnDelete(DeleteBehavior.SetNull);
        //    #endregion

        //    #region Relacionamentos Attending
        //    modelBuilder.Entity<Attending>()
        //        .HasOne(a => a.VetId)
        //        .WithMany() // um vet pode ter varios attendin
        //        .HasForeignKey(a => a.VetId)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<Attending>()
        //        .HasOne(a => a.IdClient)
        //        .WithMany() // um vet pode ter varios attendin
        //        .HasForeignKey(a => a.IdClient)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<Attending>()
        //        .HasOne(a => a.IdClientPet)
        //        .WithMany() // um vet pode ter varios attendin
        //        .HasForeignKey(a => a.IdClientPet)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<Attending>()
        //        .HasOne(a => a.IdPayment)
        //        .WithMany() // um vet pode ter varios attendin
        //        .HasForeignKey(a => a.IdPayment)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    #endregion

        //    #region Relacionamentos Job
        //    modelBuilder.Entity<Job>()
        //        .HasOne(j => j.VetId)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.VetId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    #endregion

        //    #region Relacionamentos Lab
        //    modelBuilder.Entity<Lab>()
        //        .HasOne(j => j.VetId)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.VetId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    #endregion

        //    #region Relacionamentos Product
        //    modelBuilder.Entity<Product>()
        //        .HasOne(j => j.VetId)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.VetId)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<Product>()
        //        .HasOne(j => j.IdLab)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.IdLab)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<Product>()
        //        .HasOne(j => j.IdLab)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.IdLab)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    #endregion

        //    #region Relacionamentos ProductItem
        //    modelBuilder.Entity<ProductItem>()
        //        .HasOne(j => j.VetId)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.VetId)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<ProductItem>()
        //        .HasOne(j => j.IdAttending)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.IdAttending)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<ProductItem>()
        //        .HasOne(j => j.IdProduct)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.IdProduct)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    #endregion

        //    #region Relacionamentos ServiceItem

        //    modelBuilder.Entity<ServiceItem>()
        //        .HasOne(j => j.VetId)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.VetId)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<ServiceItem>()
        //        .HasOne(j => j.IdAttending)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.IdAttending)
        //        .OnDelete(DeleteBehavior.Cascade);
            
        //    modelBuilder.Entity<ServiceItem>()
        //        .HasOne(j => j.IdJob)
        //        .WithMany() // um vet pode ter varios jobs
        //        .HasForeignKey(a => a.IdJob)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    #endregion

        //    #region Relacionamentos Vaccination
        //    modelBuilder.Entity<Vaccination>()
        //       .HasOne(j => j.VetId)
        //       .WithMany() // um vet pode ter varios jobs
        //       .HasForeignKey(a => a.VetId) // definindo foreign key
        //       .OnDelete(DeleteBehavior.Cascade); // Exclusão cascata

        //    modelBuilder.Entity<Vaccination>()
        //       .HasOne(j => j.IdClient)
        //       .WithMany() // um vet pode ter varios jobs
        //       .HasForeignKey(a => a.IdClient) // definindo foreign key
        //       .OnDelete(DeleteBehavior.Cascade); // Exclusão cascata

        //    modelBuilder.Entity<Vaccination>()
        //       .HasOne(j => j.IdAttending)
        //       .WithMany() // um vet pode ter varios jobs
        //       .HasForeignKey(a => a.IdAttending) // definindo foreign key
        //       .OnDelete(DeleteBehavior.Cascade); // Exclusão cascata

        //    modelBuilder.Entity<Vaccination>()
        //       .HasOne(j => j.IdClientPet)
        //       .WithMany() // um vet pode ter varios jobs
        //       .HasForeignKey(a => a.IdClientPet) // definindo foreign key
        //       .OnDelete(DeleteBehavior.Cascade); // Exclusão cascata
        //    #endregion
        //}

    }
}
