using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiweb.churras.show.Migrations
{
    /// <inheritdoc />
    public partial class BDv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeDeslike");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikeDeslike",
                columns: table => new
                {
                    IdLikeDeslike = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdComentario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Positivo = table.Column<bool>(type: "BIT", nullable: true)
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
                name: "IX_LikeDeslike_IdComentario",
                table: "LikeDeslike",
                column: "IdComentario");
        }
    }
}
