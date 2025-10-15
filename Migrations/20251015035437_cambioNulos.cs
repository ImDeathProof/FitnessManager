using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessManager.Migrations
{
    /// <inheritdoc />
    public partial class cambioNulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ejercicios_Musculos_MusculoId",
                table: "Ejercicios");

            migrationBuilder.AlterColumn<int>(
                name: "MusculoId",
                table: "Ejercicios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ejercicios_Musculos_MusculoId",
                table: "Ejercicios",
                column: "MusculoId",
                principalTable: "Musculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ejercicios_Musculos_MusculoId",
                table: "Ejercicios");

            migrationBuilder.AlterColumn<int>(
                name: "MusculoId",
                table: "Ejercicios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ejercicios_Musculos_MusculoId",
                table: "Ejercicios",
                column: "MusculoId",
                principalTable: "Musculos",
                principalColumn: "Id");
        }
    }
}
