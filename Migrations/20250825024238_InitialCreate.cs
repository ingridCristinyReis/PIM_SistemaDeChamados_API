using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIM_SistemaDeChamados_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tecnicos",
                table: "Tecnicos");

            migrationBuilder.AddColumn<int>(
                name: "IdTecnico",
                table: "Tecnicos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tecnicos",
                table: "Tecnicos",
                column: "IdTecnico");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_IdFunc",
                table: "Tecnicos",
                column: "IdFunc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tecnicos",
                table: "Tecnicos");

            migrationBuilder.DropIndex(
                name: "IX_Tecnicos_IdFunc",
                table: "Tecnicos");

            migrationBuilder.DropColumn(
                name: "IdTecnico",
                table: "Tecnicos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tecnicos",
                table: "Tecnicos",
                column: "IdFunc");
        }
    }
}
