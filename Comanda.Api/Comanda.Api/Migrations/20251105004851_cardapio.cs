using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Api.Migrations
{
    /// <inheritdoc />
    public partial class cardapio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CardapioItems",
                columns: new[] { "Id", "Descricao", "PossuiPreparo", "Preco", "Titulo" },
                values: new object[] { 1, "Hambúrguer clássico com queijo, alface, tomate e molho especial.", false, 20m, "Hambúrguer Clássico" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
