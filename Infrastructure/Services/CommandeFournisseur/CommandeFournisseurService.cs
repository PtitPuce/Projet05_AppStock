using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.CommandeFournisseur;
using AppStock.Infrastructure.Services.CommandeFournisseurLigne;
using AppStock.Infrastructure.Services.Fournisseur;
using AppStock.Infrastructure.Services.Stock;
using AppStock.Infrastructure.Services.StockProjection;

namespace AppStock.Infrastructure.Services.CommandeFournisseur
{
    public class CommandeFournisseurService : ICommandeFournisseurService
    {
        private readonly ICommandeFournisseurRepository _repository;
        private readonly ICommandeFournisseurLigneService _service_ligne;
        private readonly IFournisseurService _service_fournisseur;
        private readonly IStockService _service_stock;
        private readonly Lazy<IStockProjectionService> _service_stock_projection;
        public CommandeFournisseurService(ICommandeFournisseurRepository repository,
                                          ICommandeFournisseurLigneService service_ligne,
                                          IStockService service_stock,
                                          IFournisseurService service_fournisseur,
                                          Lazy<IStockProjectionService> service_stock_projection
                                          )
        {
            _service_ligne = service_ligne;
            _service_stock = service_stock;
            _service_stock_projection = service_stock_projection;
            _service_fournisseur = service_fournisseur;
            _repository = repository ?? throw new ArgumentNullException(nameof(ICommandeFournisseurRepository));
        }

        public async Task<CommandeFournisseurEntity> Add(CommandeFournisseurEntity item)
        {
            return await _repository.AddAsync(item);
        }

        public async Task DeleteById(int id)
        {
            var item = await _repository.GetOneByIdAsync(id);

            if(item is null){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            
            await _repository.DeleteAsync(item);
        }

        public async Task<IEnumerable<CommandeFournisseurEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CommandeFournisseurEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<CommandeFournisseurEntity> Update(CommandeFournisseurEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            return await _repository.UpdateAsync(item);
        }

        public async Task<CommandeFournisseurEntity> Validate(CommandeFournisseurEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            return await _repository.ValidateAsync(item);
        }

        public bool Exist(int id){
            return _repository.Exist(id);
        }
        public async Task<CommandeFournisseurEntity> UploadStock(CommandeFournisseurEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            
            // Mise à jour de la quantité en stock pour chaque article
            foreach (CommandeFournisseurLigneEntity l in item.CommandeFournisseurLignes)
            {
                StockEntity s = await _service_stock.GetOneById(l.ArticleId);
                s.Quantite = s.Quantite + l.Quantite;
                await _service_stock.Update(s);
            }
            
            return item;
        }
        // ARTICLES
        public async Task<CommandeFournisseurLigneEntity> AddArticle(CommandeFournisseurEntity commande_fournisseur, ArticleEntity article, int article_quantite)
        {
            /*
                delegation service CommandeFournisseurLigne
            */
            return await _service_ligne.AddArticle(commande_fournisseur, article, article_quantite);

        }

        public async Task<CommandeFournisseurEntity> FournisseurChange(CommandeFournisseurEntity commande_fournisseur, int id_fournisseur)
        {
            if(!_repository.Exist(commande_fournisseur.Id))
            {
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }

            // Suppression des lignes de la commande
            // foreach (CommandeFournisseurLigneEntity l in commande_fournisseur.CommandeFournisseurLignes)
            // {
            //     await _service_ligne.DeleteById(l.Id);
            // }
            commande_fournisseur.CommandeFournisseurLignes = null;
            await Update(commande_fournisseur);

            // Modification du fournisseur
            commande_fournisseur.FournisseurId = id_fournisseur;
            return await _repository.UpdateAsync(commande_fournisseur);
        }

        // Quantite totale d'article en tension (representent une charge pour le stock)
        public int getTotalPendingArticles(int id_article)
        {   
            int total = _repository.getTotalPendingArticles(id_article);
            return total;
        }

        public int calculateArticleAdvisedQuantity(ArticleEntity article, int projection)
        {
            var i = 0;
            var proj_anticipee = 0;
            var quantite = 0;
            
            var proj_actuelle = projection;

            do
            {
                i++;
                quantite = article.Threshold * i;
                proj_anticipee =  proj_actuelle + quantite; // await _service_stock_projection.Projection(  article.Id )
            }
            while( proj_anticipee < article.Threshold ); 

            return quantite;
        }

        // Algorithme de determination de creation et envoi de CommandeFournisseur automatisees
        public async Task<List<CommandeFournisseurEntity>> getCommandesFournisseurAuto(CommandeEntity commande)
        {
            if(commande == null)
            {
                throw new NullReferenceException(nameof(commande));
            }
            
            // liste des references de lignes necessitant un restock
            List<CommandeLigneEntity> lignes_pour_restock = new List<CommandeLigneEntity>();
            // liste des references des Fournisseur impliqués
            List<FournisseurEntity> liste_fournisseurs = new List<FournisseurEntity>();
            // liste finale des CommandeFournisseur automatiques, à enregistrer puis envoyer
            List<CommandeFournisseurEntity> liste_commandes_auto = new List<CommandeFournisseurEntity>();
            

            foreach (var ligne in commande.CommandeLignes)
            {
                var proj = _service_stock_projection.Value.Projection(ligne.ArticleId).Result;
                Console.WriteLine(proj);
                // si la projection MOINS la quantite souhaitee met en peril le stock (inferieur au seuil de stock minimal parametre : aka. Article.Threshold)
                if( (proj - ligne.Quantite) <= ligne.Article.Threshold )
                {
                    // on reference la ligne en question
                    lignes_pour_restock.Add(ligne);
                    // on reference le Fournisseur isolement, en verifiant qu'il ne soit pas deja reference
                    FournisseurEntity fournisseur = await _service_fournisseur.GetOneById(ligne.Article.FournisseurId);
                    if(!liste_fournisseurs.Contains(fournisseur))
                    {
                        liste_fournisseurs.Add(fournisseur);
                    }
                }
            }

            // si effectivement des lignes de la Commande Client ont ete referencees...
            if( lignes_pour_restock.Count > 0 )
            {
                // ... on parcours chaque Fournisseur pour lui attribuer une CommandeFournisseur
                foreach (var fournisseur in liste_fournisseurs)
                {
                    var commande_auto = new CommandeFournisseurEntity();
                    commande_auto.CommandeFournisseurLignes = new List<CommandeFournisseurLigneEntity>();
                    commande_auto.FournisseurId = fournisseur.Id; // on lui associe le bon fournisseur, notamment pour l'adresse ; et en general car l'information semble pertinente

                    // on isole les lignes de commande pour ce Fournisseur uniquement, grace a List<T>.FindAll(...conditions...)
                    List<CommandeLigneEntity> lignes_a_traiter = lignes_pour_restock.FindAll(o => o.Article.FournisseurId == fournisseur.Id);

                    foreach (var ligne in lignes_a_traiter)
                    {
                        CommandeFournisseurLigneEntity new_ligne = new CommandeFournisseurLigneEntity();
                        new_ligne.ArticleId = ligne.Article.Id;
                        new_ligne.Quantite =  ligne.Article.Threshold * 2  ;//  2 x le seuil // anicennement :: calculateArticleAdvisedQuantity(ligne.Article, projectionPourCalcul);

                        commande_auto.CommandeFournisseurLignes.Add(new_ligne);

                        // enregistrement
                        liste_commandes_auto.Add(commande_auto);
                    }
                }
            }

            return liste_commandes_auto;
        }

    }
}