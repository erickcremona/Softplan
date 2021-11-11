using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CadastroProdutos.Infra.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    PrecoCusto = table.Column<float>(type: "real", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    PrecoVenda = table.Column<double>(type: "float", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
