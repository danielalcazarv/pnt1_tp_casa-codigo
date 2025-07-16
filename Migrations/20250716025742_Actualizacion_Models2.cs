using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace casa_codigo_cursos.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacion_Models2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duracion",
                table: "Curso",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "Curso");
        }
    }
}
