using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuTemplateForINL1.Migrations
{
    /// <inheritdoc />
    public partial class RecreateINL1Items : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INL1Items_INL1Categories_CategoryId",
                table: "INL1Items");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "INL1Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_INL1Items_INL1Categories_CategoryId",
                table: "INL1Items",
                column: "CategoryId",
                principalTable: "INL1Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INL1Items_INL1Categories_CategoryId",
                table: "INL1Items");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "INL1Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_INL1Items_INL1Categories_CategoryId",
                table: "INL1Items",
                column: "CategoryId",
                principalTable: "INL1Categories",
                principalColumn: "Id");
        }
    }
}
