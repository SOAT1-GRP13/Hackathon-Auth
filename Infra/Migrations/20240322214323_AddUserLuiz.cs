using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "acesso_usuario",
                columns: new[] { "Id", "email", "matricula", "nome", "Role", "senha" },
                values: new object[,]
                {
                    { new Guid("dd1edfcd-1425-4cd8-a8a4-421967940eb5"), "luizsohpro@gmail.com", "2300105", "Luiz Soh", 20, "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "acesso_usuario",
                keyColumn: "Id",
                keyValue: new Guid("dd1edfcd-1425-4cd8-a8a4-421967940eb5"));
        }
    }
}
