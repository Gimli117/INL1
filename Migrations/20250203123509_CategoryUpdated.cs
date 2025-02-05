using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuTemplateForINL1.Migrations
{
    /// <inheritdoc />
    public partial class CategoryUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Categories",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Categories");
        }
    }
}
