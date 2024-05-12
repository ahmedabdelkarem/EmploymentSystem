using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employment.Domain.Migrations
{
    /// <inheritdoc />
    public partial class IntiateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    PasswordHash = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SecurityStamp = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: true),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VacancyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MaxNumberOfApplications = table.Column<int>(type: "int", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: true),
                    IsPosted = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CurrentNumberOfApplication = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies_ARC",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    VacancyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumberOfApplications = table.Column<int>(type: "int", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: true),
                    IsPosted = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies_ARC", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClaimValue = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClaimValue = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
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
                    LoginProvider = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    UserId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
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
                    UserId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    RoleId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
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
                    UserId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
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
                name: "VacanciesApplications_ARC",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Fk_VacancyId = table.Column<int>(type: "int", nullable: true),
                    Fk_ApplicantId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacanciesApplications_ARC", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VacanciesApplications_AspNetUsers_ARC",
                        column: x => x.Fk_ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacanciesApplications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_VacancyId = table.Column<int>(type: "int", nullable: false),
                    Fk_ApplicantId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacanciesApplications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VacanciesApplications_AspNetUsers",
                        column: x => x.Fk_ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacanciesApplications_Vacancies",
                        column: x => x.Fk_VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

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
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_VacanciesApplications_Fk_ApplicantId",
                table: "VacanciesApplications",
                column: "Fk_ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_VacanciesApplications_Fk_VacancyId",
                table: "VacanciesApplications",
                column: "Fk_VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_VacanciesApplications_ARC_Fk_ApplicantId",
                table: "VacanciesApplications_ARC",
                column: "Fk_ApplicantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Vacancies_ARC");

            migrationBuilder.DropTable(
                name: "VacanciesApplications");

            migrationBuilder.DropTable(
                name: "VacanciesApplications_ARC");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
