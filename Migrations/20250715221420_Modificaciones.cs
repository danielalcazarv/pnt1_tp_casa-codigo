using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace casa_codigo_cursos.Migrations
{
    /// <inheritdoc />
    public partial class Modificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "imagen",
                table: "Curso",
                newName: "ImagenUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagenUrl",
                table: "Curso",
                newName: "imagen");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
