using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using AppStock.Models;
using System.Threading;

namespace AppStock.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ArticleEntity> ArticleEntities { get; set; }     
        public DbSet<ArticleFamilleEntity> ArticleFamilleEntities { get; set; }   
        public DbSet<StockEntity> StockEntities { get; set; }   
        public DbSet<NomTypeTVAEntity> NomTypeTVAEntities { get; set; }   
        public DbSet<AdresseEntity> AdresseEntities { get; set; }   
        public DbSet<ContactEntity> ContactEntities { get; set; }   
        public DbSet<CommandeEntity> CommandeEntities { get; set; }  
        public DbSet<NomCommandeStatutEntity> NomCommandeStatutEntities { get; set; } 
        public DbSet<NomCommandeTypeEntity> NomCommandeTypeEntities { get; set; }   
        public DbSet<CommandeLigneEntity> CommandeClientLigneEntities { get; set; }  
        public DbSet<InventaireEntity> InventaireEntities { get; set; }  
        public DbSet<NomInventaireStatutEntity> NomInventaireStatutEntities { get; set; } 
        public DbSet<InventaireLigneEntity> InventaireLigneEntities { get; set; }  

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);

            builder.QueryFilters();
        }

        public override int SaveChanges()
        {
            SetTimedObjects();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTimedObjects();
            return await base.SaveChangesAsync();
        }
        private void SetTimedObjects()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is ITimedEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((ITimedEntity)entity.Entity).CreatedAt = now;
                }
                ((ITimedEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}
