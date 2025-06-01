using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomFlowApi.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TAB_Curso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Periodo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Curso", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TAB_Disciplina",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Disciplina", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TAB_Sala",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NumeroSala = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusSala = table.Column<int>(type: "int", nullable: false),
                    TipoSala = table.Column<int>(type: "int", nullable: false),
                    FlagExibirNumeroSala = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Sala", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TAB_Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Login = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChaveResetSenha = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Perfil = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Usuario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TAB_Turma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CursoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TAB_Turma_TAB_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "TAB_Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TAB_Aula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Bloco = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SalaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TurmaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProfessorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CursoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Aula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TAB_Aula_TAB_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "TAB_Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAB_Aula_TAB_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "TAB_Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAB_Aula_TAB_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "TAB_Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAB_Aula_TAB_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "TAB_Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAB_Aula_TAB_Usuario_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "TAB_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Aula_CursoId",
                table: "TAB_Aula",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Aula_DisciplinaId",
                table: "TAB_Aula",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Aula_ProfessorId",
                table: "TAB_Aula",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Aula_SalaId",
                table: "TAB_Aula",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Aula_TurmaId",
                table: "TAB_Aula",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Turma_CursoId",
                table: "TAB_Turma",
                column: "CursoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAB_Aula");

            migrationBuilder.DropTable(
                name: "TAB_Disciplina");

            migrationBuilder.DropTable(
                name: "TAB_Sala");

            migrationBuilder.DropTable(
                name: "TAB_Turma");

            migrationBuilder.DropTable(
                name: "TAB_Usuario");

            migrationBuilder.DropTable(
                name: "TAB_Curso");
        }
    }
}
