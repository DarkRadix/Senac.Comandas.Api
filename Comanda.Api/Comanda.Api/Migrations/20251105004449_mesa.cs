using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Api.Migrations
{
    /// <inheritdoc />
    public partial class mesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Mesas",
                columns: new[] { "Id", "NumeroMesa", "SituacaoMesa" },
                values: new object[] { 1, 1, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
