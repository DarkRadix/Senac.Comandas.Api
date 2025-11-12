using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Comanda.Api.Migrations
{
    /// <inheritdoc />
    public partial class banco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CardapioItems",
                columns: new[] { "Id", "Descricao", "PossuiPreparo", "Preco", "Titulo" },
                values: new object[,]
                {
                    { 2, "Refrigerante gelado de 350ml para acompanhar sua refeição.", false, 5m, "Refrigerante 350ml" },
                    { 3, "Porção de batatas fritas crocantes e douradas.", true, 10m, "Batatas Fritas" }
                });

            migrationBuilder.InsertData(
                table: "Mesas",
                columns: new[] { "Id", "NumeroMesa", "SituacaoMesa" },
                values: new object[,]
                {
                    { 2, 2, 0 },
                    { 3, 3, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
