using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using AppStock.Models;

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

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);

            builder.QueryFilters();
        }
    }
}
