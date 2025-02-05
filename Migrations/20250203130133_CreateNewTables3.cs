using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuTemplateForINL1.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewTables3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardPaymentInfo_Customers_CustomerId",
                table: "CardPaymentInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_PreviousOrders_Customers_CustomerId",
                table: "PreviousOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreviousOrders",
                table: "PreviousOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardPaymentInfo",
                table: "CardPaymentInfo");

            migrationBuilder.RenameTable(
                name: "PreviousOrders",
                newName: "INL1PreviousOrders");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "INL1Items");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "INL1Customers");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "INL1Categories");

            migrationBuilder.RenameTable(
                name: "CardPaymentInfo",
                newName: "INL1CardPaymentInfo");

            migrationBuilder.RenameIndex(
                name: "IX_PreviousOrders_CustomerId",
                table: "INL1PreviousOrders",
                newName: "IX_INL1PreviousOrders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CategoryId",
                table: "INL1Items",
                newName: "IX_INL1Items_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CardPaymentInfo_CustomerId",
                table: "INL1CardPaymentInfo",
                newName: "IX_INL1CardPaymentInfo_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_INL1PreviousOrders",
                table: "INL1PreviousOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_INL1Items",
                table: "INL1Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_INL1Customers",
                table: "INL1Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_INL1Categories",
                table: "INL1Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_INL1CardPaymentInfo",
                table: "INL1CardPaymentInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_INL1CardPaymentInfo_INL1Customers_CustomerId",
                table: "INL1CardPaymentInfo",
                column: "CustomerId",
                principalTable: "INL1Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_INL1Items_INL1Categories_CategoryId",
                table: "INL1Items",
                column: "CategoryId",
                principalTable: "INL1Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_INL1PreviousOrders_INL1Customers_CustomerId",
                table: "INL1PreviousOrders",
                column: "CustomerId",
                principalTable: "INL1Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INL1CardPaymentInfo_INL1Customers_CustomerId",
                table: "INL1CardPaymentInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_INL1Items_INL1Categories_CategoryId",
                table: "INL1Items");

            migrationBuilder.DropForeignKey(
                name: "FK_INL1PreviousOrders_INL1Customers_CustomerId",
                table: "INL1PreviousOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_INL1PreviousOrders",
                table: "INL1PreviousOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_INL1Items",
                table: "INL1Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_INL1Customers",
                table: "INL1Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_INL1Categories",
                table: "INL1Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_INL1CardPaymentInfo",
                table: "INL1CardPaymentInfo");

            migrationBuilder.RenameTable(
                name: "INL1PreviousOrders",
                newName: "PreviousOrders");

            migrationBuilder.RenameTable(
                name: "INL1Items",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "INL1Customers",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "INL1Categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "INL1CardPaymentInfo",
                newName: "CardPaymentInfo");

            migrationBuilder.RenameIndex(
                name: "IX_INL1PreviousOrders_CustomerId",
                table: "PreviousOrders",
                newName: "IX_PreviousOrders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_INL1Items_CategoryId",
                table: "Items",
                newName: "IX_Items_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_INL1CardPaymentInfo_CustomerId",
                table: "CardPaymentInfo",
                newName: "IX_CardPaymentInfo_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreviousOrders",
                table: "PreviousOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardPaymentInfo",
                table: "CardPaymentInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardPaymentInfo_Customers_CustomerId",
                table: "CardPaymentInfo",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreviousOrders_Customers_CustomerId",
                table: "PreviousOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
