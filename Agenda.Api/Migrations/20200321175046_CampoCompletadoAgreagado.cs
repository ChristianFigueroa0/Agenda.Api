using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Api.Migrations
{
    public partial class CampoCompletadoAgreagado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completado",
                table: "Agendas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completado",
                table: "Agendas");
        }
    }
}
