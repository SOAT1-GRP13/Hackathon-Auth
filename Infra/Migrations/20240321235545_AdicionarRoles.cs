using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class AdicionarRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "acesso_usuario",
                keyColumn: "Id",
                keyValue: new Guid("3a0577fa-07f7-4dc3-8adc-5a006d0794cc"));

            migrationBuilder.DeleteData(
                table: "acesso_usuario",
                keyColumn: "Id",
                keyValue: new Guid("8522d738-7150-475c-9aa5-ea1dfd4505a5"));

            migrationBuilder.DeleteData(
                table: "acesso_usuario",
                keyColumn: "Id",
                keyValue: new Guid("c3443f23-021f-4b03-b20f-bde2313edbc4"));

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "acesso_usuario",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "acesso_usuario",
                columns: new[] { "Id", "email", "matricula", "nome", "Role", "senha" },
                values: new object[,]
                {
                    { new Guid("305af885-4794-4011-9e42-db3d23c7d8dd"), "jose.silva@gmail.com", "2200101", "José Silva", 20, "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C" },
                    { new Guid("65f3621e-f99c-475e-9430-00c11d91ec50"), "felipe.okagawa@gmail.com", "2400103", "Felipe Okagawa", 20, "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C" },
                    { new Guid("cc9eb831-4bc6-4f87-9342-2fcce1f6c29c"), "renata.barros@gmail.com", "2300102", "Renata Barros", 20, "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "acesso_usuario",
                keyColumn: "Id",
                keyValue: new Guid("305af885-4794-4011-9e42-db3d23c7d8dd"));

            migrationBuilder.DeleteData(
                table: "acesso_usuario",
                keyColumn: "Id",
                keyValue: new Guid("65f3621e-f99c-475e-9430-00c11d91ec50"));

            migrationBuilder.DeleteData(
                table: "acesso_usuario",
                keyColumn: "Id",
                keyValue: new Guid("cc9eb831-4bc6-4f87-9342-2fcce1f6c29c"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "acesso_usuario");

            migrationBuilder.InsertData(
                table: "acesso_usuario",
                columns: new[] { "Id", "email", "matricula", "nome", "senha" },
                values: new object[,]
                {
                    { new Guid("3a0577fa-07f7-4dc3-8adc-5a006d0794cc"), "renata.barros@gmail.com", "2300102", "Renata Barros", "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C" },
                    { new Guid("8522d738-7150-475c-9aa5-ea1dfd4505a5"), "jose.silva@gmail.com", "2200101", "José Silva", "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C" },
                    { new Guid("c3443f23-021f-4b03-b20f-bde2313edbc4"), "felipe.okagawa@gmail.com", "2400103", "Felipe Okagawa", "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C" }
                });
        }
    }
}
