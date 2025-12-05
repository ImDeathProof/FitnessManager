using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessManager.Migrations
{
    /// <inheritdoc />
    public partial class Metas : Migration
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
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TipoMetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TipoMetaId = table.Column<int>(type: "int", nullable: false),
                    ValorMeta = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FechaLimite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorInicial = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ValorAtual = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Cumplida = table.Column<bool>(type: "bit", nullable: false),
                    FechaCumplida = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metas_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metas_TipoMetas_TipoMetaId",
                        column: x => x.TipoMetaId,
                        principalTable: "TipoMetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metas_TipoMetaId",
                table: "Metas",
                column: "TipoMetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Metas_UsuarioId",
                table: "Metas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleRutinas_Exercises_ExerciseId",
                table: "DetalleRutinas",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleRutinas_Exercises_ExerciseId",
                table: "DetalleRutinas");

            migrationBuilder.DropTable(
                name: "Metas");

            migrationBuilder.DropTable(
                name: "TipoMetas");

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
    }
}
