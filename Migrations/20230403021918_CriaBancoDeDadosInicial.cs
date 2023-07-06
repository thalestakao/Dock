using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dock.Migrations
{
    /// <inheritdoc />
    public partial class CriaBancoDeDadosInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR(180)", maxLength: 180, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Agencia = table.Column<int>(type: "int", nullable: false),
                    Conta = table.Column<string>(type: "NVARCHAR(15)", maxLength: 15, nullable: false),
                    Saldo_Moeda = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    Saldo_Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsBloqueada = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsAtiva = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PortadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contas_Portadores_PortadorId",
                        column: x => x.PortadorId,
                        principalTable: "Portadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoTransacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor_Moeda = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Valor_Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RealizadaEm = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ContaDigitalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_Transacao_ContaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaDigital_TransacaoId",
                        column: x => x.FK_Transacao_ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contas_PortadorId",
                table: "Contas",
                column: "PortadorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portador_Cpf",
                table: "Portadores",
                column: "Cpf",
                unique: true,
                filter: "[Cpf] is not null");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_FK_Transacao_ContaId",
                table: "Transacoes",
                column: "FK_Transacao_ContaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropTable(
                name: "Portadores");
        }
    }
}
