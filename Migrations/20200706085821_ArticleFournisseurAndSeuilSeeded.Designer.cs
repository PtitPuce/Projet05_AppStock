﻿// <auto-generated />
using System;
using AppStock.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppStock.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200706085821_ArticleFournisseurAndSeuilSeeded")]
    partial class ArticleFournisseurAndSeuilSeeded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AppStock.Models.AdresseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("adresse_uid")
                        .HasColumnType("int");

                    b.Property<string>("Champ1")
                        .HasColumnName("adresse_champ_1")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Champ2")
                        .HasColumnName("adresse_champ_2")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CodePostal")
                        .HasColumnName("adresse_code_postal")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Pays")
                        .HasColumnName("adresse_pays")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Ville")
                        .HasColumnName("adresse_ville")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("app_adresse");
                });

            modelBuilder.Entity("AppStock.Models.ArticleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("article_uid")
                        .HasColumnType("int");

                    b.Property<int?>("ArticleFamilleId")
                        .HasColumnName("article_famille_uid")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("article_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FournisseurId")
                        .HasColumnName("article_fournisseur_uid")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnName("article_libelle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("NomTypeTVAId")
                        .HasColumnName("article_tva_uid")
                        .HasColumnType("int");

                    b.Property<decimal>("PrixUnitaire")
                        .HasColumnName("article_pu")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Threshold")
                        .HasColumnName("article_threshold")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleFamilleId");

                    b.HasIndex("FournisseurId");

                    b.HasIndex("NomTypeTVAId");

                    b.ToTable("app_article");
                });

            modelBuilder.Entity("AppStock.Models.ArticleFamilleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("article_famille_uid")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("article_famille_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnName("article_famille_libelle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("app_article_famille");
                });

            modelBuilder.Entity("AppStock.Models.CommandeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("commande_uid")
                        .HasColumnType("int");

                    b.Property<int?>("AdresseId")
                        .HasColumnName("commande_adresse_uid")
                        .HasColumnType("int");

                    b.Property<string>("Commentaire")
                        .HasColumnName("commande_commentaire")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ContactId")
                        .HasColumnName("commande_contact_uid")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("NomCommandeStatutId")
                        .HasColumnName("commande_statut_uid")
                        .HasColumnType("int");

                    b.Property<int>("NomCommandeTypeId")
                        .HasColumnName("commande_type_uid")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .HasColumnName("commande_numero")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AdresseId");

                    b.HasIndex("ContactId");

                    b.HasIndex("NomCommandeStatutId");

                    b.HasIndex("NomCommandeTypeId");

                    b.ToTable("app_commande");
                });

            modelBuilder.Entity("AppStock.Models.CommandeLigneEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("commande_ligne_uid")
                        .HasColumnType("int");

                    b.Property<int>("ArticleId")
                        .HasColumnName("commande_ligne_article_uid")
                        .HasColumnType("int");

                    b.Property<int>("CommandeId")
                        .HasColumnName("commande_ligne_commande_uid")
                        .HasColumnType("int");

                    b.Property<int>("Quantite")
                        .HasColumnName("commande_ligne_quantite")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("CommandeId");

                    b.ToTable("app_commande_ligne");
                });

            modelBuilder.Entity("AppStock.Models.ContactEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("contact_uid")
                        .HasColumnType("int");

                    b.Property<int?>("AdresseId")
                        .HasColumnName("contact_adresse_uid")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .HasColumnName("contact_nom")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Prenom")
                        .HasColumnName("contact_prenom")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Telephone")
                        .HasColumnName("contact_telephone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .HasColumnName("contact_user_identity_uid")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("AdresseId");

                    b.HasIndex("UserId");

                    b.ToTable("app_contact");
                });

            modelBuilder.Entity("AppStock.Models.FournisseurEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("fournisseur_uid")
                        .HasColumnType("int");

                    b.Property<int?>("AdresseId")
                        .HasColumnName("fournisseur_adresse_uid")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnName("fournisseur_email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Raison")
                        .HasColumnName("fournisseur_raison")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Telephone")
                        .HasColumnName("fournisseur_telephone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("AdresseId");

                    b.ToTable("app_fournisseur");
                });

            modelBuilder.Entity("AppStock.Models.InventaireEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("inventaire_uid")
                        .HasColumnType("int");

                    b.Property<int>("ArticleFamilleId")
                        .HasColumnName("article_famille_uid")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateCloture")
                        .HasColumnName("inventaire_date_cloture")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("NomInventaireStatutId")
                        .HasColumnName("inventaire_statut_uid")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .HasColumnName("inventaire_user_uid")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ArticleFamilleId");

                    b.HasIndex("NomInventaireStatutId");

                    b.HasIndex("UserId");

                    b.ToTable("app_inventaire");
                });

            modelBuilder.Entity("AppStock.Models.InventaireLigneEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("inventaire_ligne_uid")
                        .HasColumnType("int");

                    b.Property<int>("ArticleId")
                        .HasColumnName("inventaire_ligne_article_uid")
                        .HasColumnType("int");

                    b.Property<int>("InventaireId")
                        .HasColumnName("inventaire_ligne_inventaire_uid")
                        .HasColumnType("int");

                    b.Property<int>("QuantiteComptee")
                        .HasColumnName("inventaire_ligne_quantite_comptee")
                        .HasColumnType("int");

                    b.Property<int>("QuantiteTheorique")
                        .HasColumnName("inventaire_ligne_quantite_theorique")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("InventaireId");

                    b.ToTable("app_inventaire_ligne");
                });

            modelBuilder.Entity("AppStock.Models.NomCommandeStatutEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("commande_statut_uid")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("commande_statut_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnName("commande_statut_libelle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("app_nom_commande_statut");
                });

            modelBuilder.Entity("AppStock.Models.NomCommandeTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("commande_type_uid")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("commande_type_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnName("commande_type_libelle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("app_nom_commande_type");
                });

            modelBuilder.Entity("AppStock.Models.NomInventaireStatutEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("inventaire_statut_uid")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("inventaire_statut_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnName("inventaire_statut_libelle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("app_nom_inventaire_statut");
                });

            modelBuilder.Entity("AppStock.Models.NomTypeTVAEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tva_uid")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("tva_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnName("tva_libelle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Taux")
                        .HasColumnName("tva_taux")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("app_nom_type_tva");
                });

            modelBuilder.Entity("AppStock.Models.StockEntity", b =>
                {
                    b.Property<int>("ArticleID")
                        .HasColumnName("article_uid")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Quantite")
                        .HasColumnName("stock_quantite")
                        .HasColumnType("int");

                    b.HasKey("ArticleID");

                    b.ToTable("app_stock");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AppStock.Models.ArticleEntity", b =>
                {
                    b.HasOne("AppStock.Models.ArticleFamilleEntity", "ArticleFamille")
                        .WithMany("Articles")
                        .HasForeignKey("ArticleFamilleId");

                    b.HasOne("AppStock.Models.FournisseurEntity", "Fournisseur")
                        .WithMany()
                        .HasForeignKey("FournisseurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppStock.Models.NomTypeTVAEntity", "NomTypeTVA")
                        .WithMany()
                        .HasForeignKey("NomTypeTVAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppStock.Models.CommandeEntity", b =>
                {
                    b.HasOne("AppStock.Models.AdresseEntity", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId");

                    b.HasOne("AppStock.Models.ContactEntity", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppStock.Models.NomCommandeStatutEntity", "NomCommandeStatut")
                        .WithMany()
                        .HasForeignKey("NomCommandeStatutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppStock.Models.NomCommandeTypeEntity", "NomCommandeType")
                        .WithMany()
                        .HasForeignKey("NomCommandeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppStock.Models.CommandeLigneEntity", b =>
                {
                    b.HasOne("AppStock.Models.ArticleEntity", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppStock.Models.CommandeEntity", "Commande")
                        .WithMany("CommandeLignes")
                        .HasForeignKey("CommandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppStock.Models.ContactEntity", b =>
                {
                    b.HasOne("AppStock.Models.AdresseEntity", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AppStock.Models.FournisseurEntity", b =>
                {
                    b.HasOne("AppStock.Models.AdresseEntity", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId");
                });

            modelBuilder.Entity("AppStock.Models.InventaireEntity", b =>
                {
                    b.HasOne("AppStock.Models.ArticleFamilleEntity", "ArticleFamille")
                        .WithMany()
                        .HasForeignKey("ArticleFamilleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppStock.Models.NomInventaireStatutEntity", "NomInventaireStatut")
                        .WithMany()
                        .HasForeignKey("NomInventaireStatutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AppStock.Models.InventaireLigneEntity", b =>
                {
                    b.HasOne("AppStock.Models.ArticleEntity", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppStock.Models.InventaireEntity", "Inventaire")
                        .WithMany("InventaireLignes")
                        .HasForeignKey("InventaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppStock.Models.StockEntity", b =>
                {
                    b.HasOne("AppStock.Models.ArticleEntity", "Article")
                        .WithOne("Stock")
                        .HasForeignKey("AppStock.Models.StockEntity", "ArticleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
