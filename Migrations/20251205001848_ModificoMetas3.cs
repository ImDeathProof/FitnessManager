using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessManager.Migrations
{
    /// <inheritdoc />
    public partial class ModificoMetas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetaType",
                table: "Metas",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorActual",
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
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaType",
                table: "Metas");

            migrationBuilder.DropColumn(
                name: "ValorActual",
                table: "Metas");

            migrationBuilder.DropColumn(
                name: "ValorInicial",
                table: "Metas");

            migrationBuilder.DropColumn(
                name: "ValorMeta",
                table: "Metas");
        }
    }
}
