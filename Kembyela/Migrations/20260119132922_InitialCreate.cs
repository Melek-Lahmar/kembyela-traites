using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kembyela.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Traites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DateEcheance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateEdition = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    RIB = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Monnaie = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "DT"),
                    OrdreDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Payer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aval = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Banque = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Protestable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    MontantEnLettres = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Traites_CreatedAt",
                table: "Traites",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Traites_DateEcheance",
                table: "Traites",
                column: "DateEcheance");

            migrationBuilder.CreateIndex(
                name: "IX_Traites_RIB",
                table: "Traites",
                column: "RIB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Traites");
        }
    }
}
