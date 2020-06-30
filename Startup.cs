using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

using AppStock.Infrastructure.Services.Article;
using AppStock.Infrastructure.Services.ArticleFamille;
using AppStock.Infrastructure.Services.NomTypeTVA;
using AppStock.Infrastructure.Services.Stock;
using AppStock.Infrastructure.Services.Adresse;
using AppStock.Infrastructure.Services.Contact;
using AppStock.Infrastructure.Services.Commande;
using AppStock.Infrastructure.Services.CommandeLigne;
using AppStock.Infrastructure.Services.Inventaire;
using AppStock.Infrastructure.Services.InventaireLigne;

using AppStock.Infrastructure.Repositories.Article;
using AppStock.Infrastructure.Repositories.ArticleFamille;
using AppStock.Infrastructure.Repositories.NomTypeTVA;
using AppStock.Infrastructure.Repositories.Stock;
using AppStock.Infrastructure.Repositories.Adresse;
using AppStock.Infrastructure.Repositories.Contact;
using AppStock.Infrastructure.Repositories.Commande;
using AppStock.Infrastructure.Repositories.CommandeLigne;
using AppStock.Infrastructure.Repositories.Inventaire;
using AppStock.Infrastructure.Repositories.InventaireLigne;

using AutoMapper;


namespace AppStock
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(
                o => o.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    mysqlo => {
                        mysqlo.EnableRetryOnFailure(
                            maxRetryCount: 15,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        );
                    }
                )
            );
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews(config =>
                                                    {
                                                        // using Microsoft.AspNetCore.Mvc.Authorization;
                                                        // using Microsoft.AspNetCore.Authorization;
                                                        var policy = new AuthorizationPolicyBuilder()
                                                                        .RequireAuthenticatedUser()
                                                                        .Build();
                                                        config.Filters.Add(new AuthorizeFilter(policy));
                                                    });
            //services.AddRazorPages();
            services.AddMvc();

            services.AddAutoMapper(typeof(Startup).Assembly);

            // S E R V I C E S //
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArticleFamilleService, ArticleFamilleService>();
            services.AddTransient<INomTypeTVAService, NomTypeTVAService>();
            services.AddTransient<IStockService, StockService>();
            services.AddTransient<IAdresseService, AdresseService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ICommandeService, CommandeService>();
            services.AddTransient<ICommandeLigneService, CommandeLigneService>();
            services.AddTransient<IInventaireService, InventaireService>();
            services.AddTransient<IInventaireLigneService, InventaireLigneService>();

            // R E P O S I T O R I E S //
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleFamilleRepository, ArticleFamilleRepository>();
            services.AddTransient<INomTypeTVARepository, NomTypeTVARepository>();
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddTransient<IAdresseRepository, AdresseRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<ICommandeRepository, CommandeRepository>();
            services.AddTransient<ICommandeLigneRepository, CommandeLigneRepository>();
            services.AddTransient<IInventaireRepository, InventaireRepository>();
            services.AddTransient<IInventaireLigneRepository, InventaireLigneRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AutoMapper.IConfigurationProvider autoMapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //autoMapper.AssertConfigurationIsValid();
            
            app.UseRequestLocalization("en-US");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
