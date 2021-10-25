using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomersRec.APIrest.Migrations
{
    public partial class new_tables_Clients_and_Adresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Cep = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GIA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SIAFI = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Cep);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<int>(type: "int", nullable: false),
                    Cep_C = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressCep = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Adresses_AddressCep",
                        column: x => x.AddressCep,
                        principalTable: "Adresses",
                        principalColumn: "Cep",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AddressCep",
                table: "Clients",
                column: "AddressCep");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Adresses");
        }
    }
}
