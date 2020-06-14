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
            if (!context.NomCommandeStatutEntities.Any())
            {
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
            }

            // Seed types de commande
            if (!context.NomCommandeTypeEntities.Any())
            {
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

            // ARTICLE FAMILLE
            if (!context.ArticleFamilleEntities.Any())
            {
                var article_familles = new ArticleFamilleEntity[]
                {
                    new ArticleFamilleEntity { Code = "MOB",   Libelle = "Mobilier" },
                    new ArticleFamilleEntity { Code = "DECO",   Libelle = "Décoration" },
                    new ArticleFamilleEntity { Code = "ELEC",   Libelle = "Electronique" }
                };

                foreach (ArticleFamilleEntity s in article_familles)
                {
                    context.ArticleFamilleEntities.Add(s);
                }
                context.SaveChanges();
            }

            // TVA
            if (!context.NomTypeTVAEntities.Any())
            {
                var article_familles = new NomTypeTVAEntity[]
                {
                    new NomTypeTVAEntity { Code = "DOUZE",   Libelle = "Douze", Taux = 12M },
                    new NomTypeTVAEntity { Code = "VINGT",   Libelle = "Vingt", Taux = 20M },
                    new NomTypeTVAEntity { Code = "ZERO",   Libelle = "Zero", Taux = 0M }
                };

                foreach (NomTypeTVAEntity s in article_familles)
                {
                    context.NomTypeTVAEntities.Add(s);
                }
                context.SaveChanges();
            }

            // ARTICLES
            if (!context.ArticleEntities.Any())
            {
                var articles = new ArticleEntity[]
                {
                    new ArticleEntity { Code = "CHA",
                                        Libelle = "Chaise",
                                        PrixUnitaire = 20,
                                        ArticleFamilleId = context.ArticleFamilleEntities.Where(f => f.Code == "MOB").First().Id,
                                        NomTypeTVAId = context.NomTypeTVAEntities.Where(f => f.Code == "VINGT").First().Id
                                        },
                    new ArticleEntity { Code = "TAB",
                                        Libelle = "Table",
                                        PrixUnitaire = 150,
                                        ArticleFamilleId = context.ArticleFamilleEntities.Where(f => f.Code == "MOB").First().Id,
                                        NomTypeTVAId = context.NomTypeTVAEntities.Where(f => f.Code == "DOUZE").First().Id
                                        },
                    new ArticleEntity { Code = "LUX",
                                        Libelle = "Lampe",
                                        PrixUnitaire = 12.5M,
                                        ArticleFamilleId = context.ArticleFamilleEntities.Where(f => f.Code == "DECO").First().Id,
                                        NomTypeTVAId = context.NomTypeTVAEntities.Where(f => f.Code == "ZERO").First().Id
                                        },
                    
                };

                foreach (ArticleEntity s in articles)
                {
                    context.ArticleEntities.Add(s);
                }
                context.SaveChanges();
            }

        }
    }
}