using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace casa_codigo_cursos.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionInscripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscripcion_Usuario_UsuarioId",
                table: "Inscripcion");

            migrationBuilder.DropColumn(
                name: "AlumnoID",
                table: "Inscripcion");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Inscripcion",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Inscripcion_UsuarioId",
                table: "Inscripcion",
                newName: "IX_Inscripcion_UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripcion_Usuario_UsuarioID",
                table: "Inscripcion",
                column: "UsuarioID",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscripcion_Usuario_UsuarioID",
                table: "Inscripcion");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Inscripcion",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Inscripcion_UsuarioID",
                table: "Inscripcion",
                newName: "IX_Inscripcion_UsuarioId");

            migrationBuilder.AddColumn<int>(
                name: "AlumnoID",
                table: "Inscripcion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripcion_Usuario_UsuarioId",
                table: "Inscripcion",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
