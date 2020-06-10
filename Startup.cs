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
using AppStock.Infrastructure.Repositories.Article;
using AppStock.Infrastructure.Services.ArticleFamille;
using AppStock.Infrastructure.Repositories.ArticleFamille;
using AppStock.Infrastructure.Services.NomTypeTVA;
using AppStock.Infrastructure.Repositories.NomTypeTVA;

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

            // S E R V I C E S //
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArticleFamilleService, ArticleFamilleService>();
            services.AddTransient<INomTypeTVAService, NomTypeTVAService>();

            // R E P O S I T O R I E S //
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleFamilleRepository, ArticleFamilleRepository>();
            services.AddTransient<INomTypeTVARepository, NomTypeTVARepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
