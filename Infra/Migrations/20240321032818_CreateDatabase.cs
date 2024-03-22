using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "acesso_usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    matricula = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acesso_usuario", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acesso_usuario");
        }
    }
}
