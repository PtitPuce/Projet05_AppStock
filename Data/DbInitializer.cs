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

            // Seed statuts des inventaires
            if (!context.NomInventaireStatutEntities.Any())
            {
                var inventaire_status = new NomInventaireStatutEntity[]
                {
                    new NomInventaireStatutEntity { Code = "E",   Libelle = "En cours" },
                    new NomInventaireStatutEntity { Code = "T",   Libelle = "Terminé" }
                };

                foreach (NomInventaireStatutEntity s in inventaire_status)
                {
                    context.NomInventaireStatutEntities.Add(s);
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
                var type_tvas = new NomTypeTVAEntity[]
                {
                    new NomTypeTVAEntity { Code = "DOUZE",   Libelle = "Douze", Taux = 12M },
                    new NomTypeTVAEntity { Code = "VINGT",   Libelle = "Vingt", Taux = 20M },
                    new NomTypeTVAEntity { Code = "ZERO",   Libelle = "Zero", Taux = 0M }
                };

                foreach (NomTypeTVAEntity s in type_tvas)
                {
                    context.NomTypeTVAEntities.Add(s);
                }
                context.SaveChanges();
            }

            // ADRESSES (pour fournisseur, notamment)
            if (!context.AdresseEntities.Any())
            {
                var adresses = new AdresseEntity[]
                {
                    /*
                    Champ1 = "", Champ2 = "", CodePostal = "", Ville = "", Pays = ""
                    */
                    new AdresseEntity { Champ1 = "1 bld de Strasbourg", Champ2 = "", CodePostal = "44000", Ville = "NANTES", Pays = "FRANCE" },
                    new AdresseEntity { Champ1 = "12 rue de la Foret", Champ2 = "", CodePostal = "49500", Ville = "CRESSON", Pays = "FRANCE" }
                };

                foreach (AdresseEntity s in adresses)
                {
                    context.AdresseEntities.Add(s);
                }
                context.SaveChanges();
            }
            
            // FOURNISSEUR
            if (!context.FournisseurEntities.Any())
            {
                var id_adresse_A = context.AdresseEntities.Where(f => f.Champ1 == "1 bld de Strasbourg").First().Id;
                var id_adresse_B = context.AdresseEntities.Where(f => f.Champ1 == "12 rue de la Foret").First().Id;

                var fournisseurs = new FournisseurEntity[]
                {
                    new FournisseurEntity { Raison="Meubles SARL",  
                                            Telephone="+33622657991", 
                                            Email="contact@meubles.fr",
                                            AdresseId=  id_adresse_A
                                        },
                    new FournisseurEntity { Raison="Boiserie and Co.",  
                                            Telephone="+33801223040", 
                                            Email="contact@boiseries-co.fr",
                                            AdresseId=  id_adresse_B
                                        },
                    
                };

                foreach (FournisseurEntity s in fournisseurs)
                {
                    context.FournisseurEntities.Add(s);
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