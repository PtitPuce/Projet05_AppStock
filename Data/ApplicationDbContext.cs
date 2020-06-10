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

        public DbSet<Article> Articles { get; set; }     
        public DbSet<ArticleFamille> ArticleFamilles { get; set; }   
        public DbSet<Stock> Stocks { get; set; }   
        public DbSet<NomTypeTVA> NomTypeTVA { get; set; }   

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
        }
    }
}
