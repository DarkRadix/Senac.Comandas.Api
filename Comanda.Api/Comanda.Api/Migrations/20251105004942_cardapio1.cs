using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Api.Migrations
{
    /// <inheritdoc />
    public partial class cardapio1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "PossuiPreparo",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "PossuiPreparo",
                value: false);
        }
    }
}
