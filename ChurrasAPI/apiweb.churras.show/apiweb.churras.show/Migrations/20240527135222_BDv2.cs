using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiweb.churras.show.Migrations
{
    /// <inheritdoc />
    public partial class BDv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orcamento");

            migrationBuilder.DropTable(
                name: "StatusOrcamento");

            migrationBuilder.AlterColumn<int>(
                name: "Garconete",
                table: "Evento",
                type: "INT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "BIT",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeCriacao",
                table: "Evento",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdStatusEvento",
                table: "Evento",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Evento",
                type: "DECIMAL(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UF",
                table: "Endereco",
                type: "VARCHAR(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2)");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "Endereco",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Endereco",
                type: "VARCHAR(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Endereco",
                type: "VARCHAR(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

            migrationBuilder.AlterColumn<int>(
                name: "CEP",
                table: "Endereco",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Endereco",
                type: "VARCHAR(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");

            migrationBuilder.CreateTable(
                name: "StatusEvento",
                columns: table => new
                {
                    IdStatusEvento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusEvento", x => x.IdStatusEvento);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdStatusEvento",
                table: "Evento",
                column: "IdStatusEvento");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_StatusEvento_IdStatusEvento",
                table: "Evento",
                column: "IdStatusEvento",
                principalTable: "StatusEvento",
                principalColumn: "IdStatusEvento",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_StatusEvento_IdStatusEvento",
                table: "Evento");

            migrationBuilder.DropTable(
                name: "StatusEvento");

            migrationBuilder.DropIndex(
                name: "IX_Evento_IdStatusEvento",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "DataDeCriacao",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "IdStatusEvento",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Evento");

            migrationBuilder.AlterColumn<bool>(
                name: "Garconete",
                table: "Evento",
                type: "BIT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UF",
                table: "Endereco",
                type: "VARCHAR(2)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "Endereco",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Endereco",
                type: "VARCHAR(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Endereco",
                type: "VARCHAR(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CEP",
                table: "Endereco",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Endereco",
                type: "VARCHAR(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StatusOrcamento",
                columns: table => new
                {
                    IdStatusOrcamento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOrcamento", x => x.IdStatusOrcamento);
                });

            migrationBuilder.CreateTable(
                name: "Orcamento",
                columns: table => new
                {
                    IdOrcamento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdEvento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdStatusOrcamento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriacaoOrcamento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PDF = table.Column<string>(type: "TEXT", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamento", x => x.IdOrcamento);
                    table.ForeignKey(
                        name: "FK_Orcamento_Evento_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "Evento",
                        principalColumn: "IdEvento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orcamento_StatusOrcamento_IdStatusOrcamento",
                        column: x => x.IdStatusOrcamento,
                        principalTable: "StatusOrcamento",
                        principalColumn: "IdStatusOrcamento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_IdEvento",
                table: "Orcamento",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_IdStatusOrcamento",
                table: "Orcamento",
                column: "IdStatusOrcamento");
        }
    }
}
