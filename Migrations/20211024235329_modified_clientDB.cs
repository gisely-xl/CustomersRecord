using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomersRec.APIrest.Migrations
{
    public partial class modified_clientDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cep_C",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cep_C",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
