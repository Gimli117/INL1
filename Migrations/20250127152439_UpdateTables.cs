using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuTemplateForINL1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_PreviousOrders_PreviousOrderId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_PreviousOrderId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PreviousOrderId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "PreviousOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSelectedByAdmin",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<bool>(
                name: "IsSelectedByAdmin",
                table: "Items",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "PreviousOrderId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_PreviousOrderId",
                table: "Items",
                column: "PreviousOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_PreviousOrders_PreviousOrderId",
                table: "Items",
                column: "PreviousOrderId",
                principalTable: "PreviousOrders",
                principalColumn: "Id");
        }
    }
}
