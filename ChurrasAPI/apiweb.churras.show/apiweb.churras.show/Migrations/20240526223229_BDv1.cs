using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiweb.churras.show.Migrations
{
    /// <inheritdoc />
    public partial class BDv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    IdEndereco = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Cidade = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    UF = table.Column<string>(type: "VARCHAR(2)", nullable: false),
                    CEP = table.Column<int>(type: "INT", nullable: false),
                    Numero = table.Column<int>(type: "INT", nullable: false),
                    Bairro = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Complemento = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "Pacotes",
                columns: table => new
                {
                    IdPacotes = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomePacote = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    DescricaoPacote = table.Column<string>(type: "TEXT", nullable: true),
                    ValorPorPessoa = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacotes", x => x.IdPacotes);
                });

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
                name: "TiposUsuario",
                columns: table => new
                {
                    IdTipoUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TituloTipoUsuario = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposUsuario", x => x.IdTipoUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    RG = table.Column<string>(type: "VARCHAR(9)", nullable: false),
                    CPF = table.Column<string>(type: "VARCHAR(11)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Foto = table.Column<string>(type: "TEXT", nullable: true),
                    CodRecupSenha = table.Column<int>(type: "INT", nullable: true),
                    IdTipoUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdEndereco = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Endereco_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_TiposUsuario_IdTipoUsuario",
                        column: x => x.IdTipoUsuario,
                        principalTable: "TiposUsuario",
                        principalColumn: "IdTipoUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    IdEvento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHoraEvento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    QuantidadePessoasEvento = table.Column<int>(type: "INT", nullable: false),
                    DuracaoEvento = table.Column<int>(type: "INT", nullable: false),
                    Descartaveis = table.Column<bool>(type: "BIT", nullable: true),
                    Acompanhamentos = table.Column<bool>(type: "BIT", nullable: true),
                    Garconete = table.Column<bool>(type: "BIT", nullable: true),
                    Confirmado = table.Column<bool>(type: "BIT", nullable: true),
                    IdPacotes = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdEndereco = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.IdEvento);
                    table.ForeignKey(
                        name: "FK_Evento_Endereco_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Evento_Pacotes_IdPacotes",
                        column: x => x.IdPacotes,
                        principalTable: "Pacotes",
                        principalColumn: "IdPacotes",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Evento_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    IdComentario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescricaoComentario = table.Column<string>(type: "TEXT", nullable: true),
                    Pontuacao = table.Column<int>(type: "INT", nullable: true),
                    Exibe = table.Column<bool>(type: "BIT", nullable: false),
                    IdEvento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.IdComentario);
                    table.ForeignKey(
                        name: "FK_Comentarios_Evento_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "Evento",
                        principalColumn: "IdEvento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orcamento",
                columns: table => new
                {
                    IdOrcamento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    PDF = table.Column<string>(type: "TEXT", nullable: true),
                    CriacaoOrcamento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IdEvento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdStatusOrcamento = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "LikeDeslike",
                columns: table => new
                {
                    IdLikeDeslike = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Positivo = table.Column<bool>(type: "BIT", nullable: true),
                    IdComentario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeDeslike", x => x.IdLikeDeslike);
                    table.ForeignKey(
                        name: "FK_LikeDeslike_Comentarios_IdComentario",
                        column: x => x.IdComentario,
                        principalTable: "Comentarios",
                        principalColumn: "IdComentario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdEvento",
                table: "Comentarios",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdUsuario",
                table: "Comentarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdEndereco",
                table: "Evento",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdPacotes",
                table: "Evento",
                column: "IdPacotes");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdUsuario",
                table: "Evento",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDeslike_IdComentario",
                table: "LikeDeslike",
                column: "IdComentario");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_IdEvento",
                table: "Orcamento",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_IdStatusOrcamento",
                table: "Orcamento",
                column: "IdStatusOrcamento");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CPF",
                table: "Usuario",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEndereco",
                table: "Usuario",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdTipoUsuario",
                table: "Usuario",
                column: "IdTipoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RG",
                table: "Usuario",
                column: "RG",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeDeslike");

            migrationBuilder.DropTable(
                name: "Orcamento");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "StatusOrcamento");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Pacotes");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "TiposUsuario");
        }
    }
}
