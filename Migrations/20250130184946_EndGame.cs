﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuTemplateForINL1.Migrations
{
    /// <inheritdoc />
    public partial class EndGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPreviousOrder");

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

            migrationBuilder.CreateTable(
                name: "ItemPreviousOrder",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "int", nullable: false),
                    PreviousOrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPreviousOrder", x => new { x.ItemsId, x.PreviousOrdersId });
                    table.ForeignKey(
                        name: "FK_ItemPreviousOrder_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPreviousOrder_PreviousOrders_PreviousOrdersId",
                        column: x => x.PreviousOrdersId,
                        principalTable: "PreviousOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPreviousOrder_PreviousOrdersId",
                table: "ItemPreviousOrder",
                column: "PreviousOrdersId");
        }
    }
}
