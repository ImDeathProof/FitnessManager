using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessManager.Migrations
{
    /// <inheritdoc />
    public partial class modifiedDRExIdNULL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleRutinas_Exercises_ExerciseId",
                table: "DetalleRutinas");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseId",
                table: "DetalleRutinas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleRutinas_Exercises_ExerciseId",
                table: "DetalleRutinas",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleRutinas_Exercises_ExerciseId",
                table: "DetalleRutinas");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseId",
                table: "DetalleRutinas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleRutinas_Exercises_ExerciseId",
                table: "DetalleRutinas",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
