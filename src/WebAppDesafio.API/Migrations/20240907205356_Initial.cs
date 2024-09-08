using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppDesafio.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    DepartamentoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.DepartamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Chamados",
                columns: table => new
                {
                    ChamadoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Assunto = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Solicitante = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    DepartamentoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamados", x => x.ChamadoId);
                    table.ForeignKey(
                        name: "FK_Chamados_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_DepartamentoId",
                table: "Chamados",
                column: "DepartamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamados");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
