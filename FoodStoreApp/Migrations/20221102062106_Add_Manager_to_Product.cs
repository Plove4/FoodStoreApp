using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStoreApp.Migrations
{
    public partial class Add_Manager_to_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Managerid",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Managerid",
                table: "Product",
                column: "Managerid");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Manager_Managerid",
                table: "Product",
                column: "Managerid",
                principalTable: "Manager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Manager_Managerid",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_Managerid",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Managerid",
                table: "Product");
        }
    }
}
