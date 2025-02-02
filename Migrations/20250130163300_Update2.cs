using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuTemplateForINL1.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPreviousOrder_Items_FinalItemsId",
                table: "ItemPreviousOrder");

            migrationBuilder.RenameColumn(
                name: "FinalItemsId",
                table: "ItemPreviousOrder",
                newName: "ItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPreviousOrder_Items_ItemsId",
                table: "ItemPreviousOrder",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPreviousOrder_Items_ItemsId",
                table: "ItemPreviousOrder");

            migrationBuilder.RenameColumn(
                name: "ItemsId",
                table: "ItemPreviousOrder",
                newName: "FinalItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPreviousOrder_Items_FinalItemsId",
                table: "ItemPreviousOrder",
                column: "FinalItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
