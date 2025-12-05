using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessManager.Migrations
{
    /// <inheritdoc />
    public partial class ModificoMetas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorAtual",
                table: "Metas");

            migrationBuilder.DropColumn(
                name: "ValorInicial",
                table: "Metas");

            migrationBuilder.DropColumn(
                name: "ValorMeta",
                table: "Metas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorAtual",
                table: "Metas",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorInicial",
                table: "Metas",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMeta",
                table: "Metas",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
