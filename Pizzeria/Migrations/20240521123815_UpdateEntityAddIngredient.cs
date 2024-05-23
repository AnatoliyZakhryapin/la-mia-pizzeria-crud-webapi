using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Migrations
{
    public partial class UpdateEntityAddIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Ingredients_ingredientsIngredientId",
                table: "IngredientPizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_Name",
                table: "ingredients",
                newName: "IX_ingredients_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_ingredients_ingredientsIngredientId",
                table: "IngredientPizza",
                column: "ingredientsIngredientId",
                principalTable: "ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_ingredients_ingredientsIngredientId",
                table: "IngredientPizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients");

            migrationBuilder.RenameTable(
                name: "ingredients",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_ingredients_Name",
                table: "Ingredients",
                newName: "IX_Ingredients_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Ingredients_ingredientsIngredientId",
                table: "IngredientPizza",
                column: "ingredientsIngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
