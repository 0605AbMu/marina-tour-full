using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.Models.Common;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableTripAddColumnDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<MultiLanguageField>(
                name: "description",
                table: "trips",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "trips");
        }
    }
}
