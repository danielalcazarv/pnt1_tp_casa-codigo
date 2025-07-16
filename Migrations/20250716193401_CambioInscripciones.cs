using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace casa_codigo_cursos.Migrations
{
    /// <inheritdoc />
    public partial class CambioInscripciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoLetra",
                table: "Inscripcion");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInscripcion",
                table: "Inscripcion",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaInscripcion",
                table: "Inscripcion");

            migrationBuilder.AddColumn<int>(
                name: "CodigoLetra",
                table: "Inscripcion",
                type: "int",
                nullable: true);
        }
    }
}
