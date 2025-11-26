using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessManager.Migrations
{
    /// <inheritdoc />
    public partial class fixedDetalleRutina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleRutinas_Ejercicios_EjercicioId",
                table: "DetalleRutinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Rutinas_AspNetUsers_UsuarioId",
                table: "Rutinas");

            migrationBuilder.RenameColumn(
                name: "EjercicioId",
                table: "DetalleRutinas",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleRutinas_EjercicioId",
                table: "DetalleRutinas",
                newName: "IX_DetalleRutinas_ExerciseId");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Rutinas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleRutinas_Exercises_ExerciseId",
                table: "DetalleRutinas",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rutinas_AspNetUsers_UsuarioId",
                table: "Rutinas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleRutinas_Exercises_ExerciseId",
                table: "DetalleRutinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Rutinas_AspNetUsers_UsuarioId",
                table: "Rutinas");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "DetalleRutinas",
                newName: "EjercicioId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleRutinas_ExerciseId",
                table: "DetalleRutinas",
                newName: "IX_DetalleRutinas_EjercicioId");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Rutinas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleRutinas_Ejercicios_EjercicioId",
                table: "DetalleRutinas",
                column: "EjercicioId",
                principalTable: "Ejercicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rutinas_AspNetUsers_UsuarioId",
                table: "Rutinas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
