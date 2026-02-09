using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kembyela.Migrations
{
    /// <inheritdoc />
    public partial class aps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstPayee",
                table: "Traites",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstPayee",
                table: "Traites");
        }
    }
}
