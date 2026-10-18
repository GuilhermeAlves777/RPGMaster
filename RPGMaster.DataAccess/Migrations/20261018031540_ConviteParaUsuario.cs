using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGMaster.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ConviteParaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CONVITE",
                columns: table => new
                {
                    ID_CONVITE = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false),
                    CODIGO = table.Column<string>(type: "TEXT", maxLength: 12, nullable: false),
                    ATIVO = table.Column<bool>(type: "INTEGER", nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONVITE", x => x.ID_CONVITE);
                    table.ForeignKey(
                        name: "FK_CONVITE_CAMPANHA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHA",
                        principalColumn: "ID_CAMPANHA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONVITE_CODIGO",
                table: "CONVITE",
                column: "CODIGO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CONVITE_ID_CAMPANHA",
                table: "CONVITE",
                column: "ID_CAMPANHA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONVITE");
        }
    }
}
