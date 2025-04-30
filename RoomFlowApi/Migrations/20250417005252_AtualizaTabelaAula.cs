using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomFlowApi.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaTabelaAula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "professorId",
                table: "TAB_Aula",
                newName: "ProfessorId");

            migrationBuilder.AddColumn<Guid>(
                name: "CursoId",
                table: "TAB_Aula",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "TAB_Aula");

            migrationBuilder.RenameColumn(
                name: "ProfessorId",
                table: "TAB_Aula",
                newName: "professorId");
        }
    }
}
