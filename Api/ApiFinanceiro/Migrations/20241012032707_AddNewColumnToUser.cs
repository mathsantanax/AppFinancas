using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFinanceiro.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Usuario",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ValoresSaida_IdUser",
                table: "ValoresSaida",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ValoresEntrada_IdUser",
                table: "ValoresEntrada",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ValoresEntrada_Usuario_IdUser",
                table: "ValoresEntrada",
                column: "IdUser",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValoresSaida_Usuario_IdUser",
                table: "ValoresSaida",
                column: "IdUser",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValoresEntrada_Usuario_IdUser",
                table: "ValoresEntrada");

            migrationBuilder.DropForeignKey(
                name: "FK_ValoresSaida_Usuario_IdUser",
                table: "ValoresSaida");

            migrationBuilder.DropIndex(
                name: "IX_ValoresSaida_IdUser",
                table: "ValoresSaida");

            migrationBuilder.DropIndex(
                name: "IX_ValoresEntrada_IdUser",
                table: "ValoresEntrada");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Usuario");
        }
    }
}
