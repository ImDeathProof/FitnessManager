using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessManager.Migrations
{
    /// <inheritdoc />
    public partial class useridnulldieta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dietas_AspNetUsers_UsuarioId",
                table: "Dietas");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Dietas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Dietas_AspNetUsers_UsuarioId",
                table: "Dietas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dietas_AspNetUsers_UsuarioId",
                table: "Dietas");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Dietas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dietas_AspNetUsers_UsuarioId",
                table: "Dietas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
