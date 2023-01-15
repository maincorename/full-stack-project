using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class RemovedFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Coffees_FK_Purchases_Coffees",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Coffees_FK_Statistics_Coffees",
                table: "Statistics");

            migrationBuilder.RenameColumn(
                name: "FK_Statistics_Coffees",
                table: "Statistics",
                newName: "CoffeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Statistics_FK_Statistics_Coffees",
                table: "Statistics",
                newName: "IX_Statistics_CoffeeId");

            migrationBuilder.RenameColumn(
                name: "FK_Purchases_Coffees",
                table: "Purchases",
                newName: "CoffeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_FK_Purchases_Coffees",
                table: "Purchases",
                newName: "IX_Purchases_CoffeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Coffees_CoffeeId",
                table: "Purchases",
                column: "CoffeeId",
                principalTable: "Coffees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Coffees_CoffeeId",
                table: "Statistics",
                column: "CoffeeId",
                principalTable: "Coffees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Coffees_CoffeeId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Coffees_CoffeeId",
                table: "Statistics");

            migrationBuilder.RenameColumn(
                name: "CoffeeId",
                table: "Statistics",
                newName: "FK_Statistics_Coffees");

            migrationBuilder.RenameIndex(
                name: "IX_Statistics_CoffeeId",
                table: "Statistics",
                newName: "IX_Statistics_FK_Statistics_Coffees");

            migrationBuilder.RenameColumn(
                name: "CoffeeId",
                table: "Purchases",
                newName: "FK_Purchases_Coffees");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_CoffeeId",
                table: "Purchases",
                newName: "IX_Purchases_FK_Purchases_Coffees");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Coffees_FK_Purchases_Coffees",
                table: "Purchases",
                column: "FK_Purchases_Coffees",
                principalTable: "Coffees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Coffees_FK_Statistics_Coffees",
                table: "Statistics",
                column: "FK_Statistics_Coffees",
                principalTable: "Coffees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
