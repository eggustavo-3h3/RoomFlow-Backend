using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomFlowApi.Migrations
{
    /// <inheritdoc />
    public partial class addNumeroSala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroSala",
                table: "TAB_Sala",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroSala",
                table: "TAB_Sala");
        }
    }
}
