using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessManager.Migrations
{
    /// <inheritdoc />
    public partial class modifiedRutinaRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ejercicios_Rutinas_RutinaId",
                table: "Ejercicios");

            migrationBuilder.DropIndex(
                name: "IX_Ejercicios_RutinaId",
                table: "Ejercicios");

            migrationBuilder.DropColumn(
                name: "RutinaId",
                table: "Ejercicios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RutinaId",
                table: "Ejercicios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicios_RutinaId",
                table: "Ejercicios",
                column: "RutinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ejercicios_Rutinas_RutinaId",
                table: "Ejercicios",
                column: "RutinaId",
                principalTable: "Rutinas",
                principalColumn: "Id");
        }
    }
}
