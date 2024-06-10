using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiweb.churras.show.Migrations
{
    /// <inheritdoc />
    public partial class BDv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Usuario",
                type: "rowversion",
                rowVersion: true,
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Usuario");
        }
    }
}
