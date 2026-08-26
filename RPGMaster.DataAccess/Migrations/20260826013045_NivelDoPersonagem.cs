using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGMaster.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NivelDoPersonagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NIVEL",
                table: "PERSONAGEM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NIVEL",
                table: "PERSONAGEM");
        }
    }
}
