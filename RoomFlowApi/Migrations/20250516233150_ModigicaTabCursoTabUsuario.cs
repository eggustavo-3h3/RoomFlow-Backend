using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomFlowApi.Migrations
{
    /// <inheritdoc />
    public partial class ModigicaTabCursoTabUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TAB_Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TAB_Usuario");
        }
    }
}
