using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class InitV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_adresse",
                columns: table => new
                {
                    adresse_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    adresse_champ_1 = table.Column<string>(nullable: true),
                    adresse_champ_2 = table.Column<string>(nullable: true),
                    adresse_code_postal = table.Column<string>(nullable: true),
                    adresse_ville = table.Column<string>(nullable: true),
                    adresse_pays = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_adresse", x => x.adresse_uid);
                });

            migrationBuilder.CreateTable(
                name: "app_article_famille",
                columns: table => new
                {
                    article_famille_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    article_famille_code = table.Column<string>(nullable: false),
                    article_famille_libelle = table.Column<string>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_article_famille", x => x.article_famille_uid);
                });

            migrationBuilder.CreateTable(
                name: "app_nom_commande_statut",
                columns: table => new
                {
                    commande_statut_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commande_statut_code = table.Column<string>(nullable: false),
                    commande_statut_libelle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_nom_commande_statut", x => x.commande_statut_uid);
                });

            migrationBuilder.CreateTable(
                name: "app_nom_commande_type",
                columns: table => new
                {
                    commande_type_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commande_type_code = table.Column<string>(nullable: false),
                    commande_type_libelle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_nom_commande_type", x => x.commande_type_uid);
                });

            migrationBuilder.CreateTable(
                name: "app_nom_type_tva",
                columns: table => new
                {
                    tva_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tva_code = table.Column<string>(nullable: false),
                    tva_libelle = table.Column<string>(nullable: false),
                    tva_taux = table.Column<decimal>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_nom_type_tva", x => x.tva_uid);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "app_article",
                columns: table => new
                {
                    article_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    article_code = table.Column<string>(nullable: false),
                    article_libelle = table.Column<string>(nullable: false),
                    article_pu = table.Column<decimal>(nullable: false),
                    article_famille_uid = table.Column<int>(nullable: true),
                    article_tva_uid = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_article", x => x.article_uid);
                    table.ForeignKey(
                        name: "FK_app_article_app_article_famille_article_famille_uid",
                        column: x => x.article_famille_uid,
                        principalTable: "app_article_famille",
                        principalColumn: "article_famille_uid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_app_article_app_nom_type_tva_article_tva_uid",
                        column: x => x.article_tva_uid,
                        principalTable: "app_nom_type_tva",
                        principalColumn: "tva_uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_contact",
                columns: table => new
                {
                    contact_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    contact_nom = table.Column<string>(nullable: true),
                    contact_prenom = table.Column<string>(nullable: true),
                    contact_telephone = table.Column<string>(nullable: true),
                    contact_adresse_uid = table.Column<int>(nullable: true),
                    contact_user_identity_uid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_contact", x => x.contact_uid);
                    table.ForeignKey(
                        name: "FK_app_contact_app_adresse_contact_adresse_uid",
                        column: x => x.contact_adresse_uid,
                        principalTable: "app_adresse",
                        principalColumn: "adresse_uid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_app_contact_AspNetUsers_contact_user_identity_uid",
                        column: x => x.contact_user_identity_uid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_stock",
                columns: table => new
                {
                    article_uid = table.Column<int>(nullable: false),
                    stock_quantite = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_stock", x => x.article_uid);
                    table.ForeignKey(
                        name: "FK_app_stock_app_article_article_uid",
                        column: x => x.article_uid,
                        principalTable: "app_article",
                        principalColumn: "article_uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_commande",
                columns: table => new
                {
                    commande_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commande_numero = table.Column<string>(nullable: true),
                    commande_commentaire = table.Column<string>(nullable: true),
                    commande_contact_uid = table.Column<int>(nullable: false),
                    commande_statut_uid = table.Column<int>(nullable: false),
                    commande_type_uid = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_commande", x => x.commande_uid);
                    table.ForeignKey(
                        name: "FK_app_commande_app_contact_commande_contact_uid",
                        column: x => x.commande_contact_uid,
                        principalTable: "app_contact",
                        principalColumn: "contact_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_commande_app_nom_commande_statut_commande_statut_uid",
                        column: x => x.commande_statut_uid,
                        principalTable: "app_nom_commande_statut",
                        principalColumn: "commande_statut_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_commande_app_nom_commande_type_commande_type_uid",
                        column: x => x.commande_type_uid,
                        principalTable: "app_nom_commande_type",
                        principalColumn: "commande_type_uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_commande_ligne",
                columns: table => new
                {
                    commande_ligne_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commande_ligne_quantite = table.Column<int>(nullable: false),
                    commande_ligne_commande_uid = table.Column<int>(nullable: false),
                    commande_ligne_article_uid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_commande_ligne", x => x.commande_ligne_uid);
                    table.ForeignKey(
                        name: "FK_app_commande_ligne_app_article_commande_ligne_article_uid",
                        column: x => x.commande_ligne_article_uid,
                        principalTable: "app_article",
                        principalColumn: "article_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_commande_ligne_app_commande_commande_ligne_commande_uid",
                        column: x => x.commande_ligne_commande_uid,
                        principalTable: "app_commande",
                        principalColumn: "commande_uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_article_article_famille_uid",
                table: "app_article",
                column: "article_famille_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_article_article_tva_uid",
                table: "app_article",
                column: "article_tva_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_commande_contact_uid",
                table: "app_commande",
                column: "commande_contact_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_commande_statut_uid",
                table: "app_commande",
                column: "commande_statut_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_commande_type_uid",
                table: "app_commande",
                column: "commande_type_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_ligne_commande_ligne_article_uid",
                table: "app_commande_ligne",
                column: "commande_ligne_article_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_ligne_commande_ligne_commande_uid",
                table: "app_commande_ligne",
                column: "commande_ligne_commande_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_contact_contact_adresse_uid",
                table: "app_contact",
                column: "contact_adresse_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_contact_contact_user_identity_uid",
                table: "app_contact",
                column: "contact_user_identity_uid");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_commande_ligne");

            migrationBuilder.DropTable(
                name: "app_stock");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "app_commande");

            migrationBuilder.DropTable(
                name: "app_article");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "app_contact");

            migrationBuilder.DropTable(
                name: "app_nom_commande_statut");

            migrationBuilder.DropTable(
                name: "app_nom_commande_type");

            migrationBuilder.DropTable(
                name: "app_article_famille");

            migrationBuilder.DropTable(
                name: "app_nom_type_tva");

            migrationBuilder.DropTable(
                name: "app_adresse");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
