using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppStock.Models;
using AppStock.Data;

namespace AppStock.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Seed statuts de commande
            if (context.NomCommandeStatutEntities.Any())
            {
                return;
            }

            var commande_status = new NomCommandeStatutEntity[]
            {
                new NomCommandeStatutEntity { Code = "P",   Libelle = "Panier" },
                new NomCommandeStatutEntity { Code = "V",   Libelle = "Validée" },
                new NomCommandeStatutEntity { Code = "A",   Libelle = "En attente" }
            };

            foreach (NomCommandeStatutEntity s in commande_status)
            {
                context.NomCommandeStatutEntities.Add(s);
            }
            context.SaveChanges();

            // Seed types de commande
            if (context.NomCommandeTypeEntities.Any())
            {
                return;
            }

            var commande_type = new NomCommandeTypeEntity[]
            {
                new NomCommandeTypeEntity { Code = "C",   Libelle = "Client" },
                new NomCommandeTypeEntity { Code = "F",   Libelle = "Fournisseur" }
            };

            foreach (NomCommandeTypeEntity s in commande_type)
            {
                context.NomCommandeTypeEntities.Add(s);
            }
            context.SaveChanges();

        }
    }
}