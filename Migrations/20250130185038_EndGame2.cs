using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuTemplateForINL1.Migrations
{
    /// <inheritdoc />
    public partial class EndGame2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreviousOrders_Items_ItemId",
                table: "PreviousOrders");

            migrationBuilder.DropIndex(
                name: "IX_PreviousOrders_ItemId",
                table: "PreviousOrders");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "PreviousOrders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "PreviousOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreviousOrders_ItemId",
                table: "PreviousOrders",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreviousOrders_Items_ItemId",
                table: "PreviousOrders",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
