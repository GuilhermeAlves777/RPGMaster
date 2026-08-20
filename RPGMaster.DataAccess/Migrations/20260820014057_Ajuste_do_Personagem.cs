using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGMaster.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste_do_Personagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PERSONAGEM_USUARIO_ID_JOGADOR",
                table: "PERSONAGEM");

            migrationBuilder.AlterColumn<long>(
                name: "ID_JOGADOR",
                table: "PERSONAGEM",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<bool>(
                name: "EhNpc",
                table: "PERSONAGEM",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ManaAtual",
                table: "PERSONAGEM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManaMaxima",
                table: "PERSONAGEM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VidaAtual",
                table: "PERSONAGEM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VidaMaxima",
                table: "PERSONAGEM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PERSONAGEM_USUARIO_ID_JOGADOR",
                table: "PERSONAGEM",
                column: "ID_JOGADOR",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PERSONAGEM_USUARIO_ID_JOGADOR",
                table: "PERSONAGEM");

            migrationBuilder.DropColumn(
                name: "EhNpc",
                table: "PERSONAGEM");

            migrationBuilder.DropColumn(
                name: "ManaAtual",
                table: "PERSONAGEM");

            migrationBuilder.DropColumn(
                name: "ManaMaxima",
                table: "PERSONAGEM");

            migrationBuilder.DropColumn(
                name: "VidaAtual",
                table: "PERSONAGEM");

            migrationBuilder.DropColumn(
                name: "VidaMaxima",
                table: "PERSONAGEM");

            migrationBuilder.AlterColumn<long>(
                name: "ID_JOGADOR",
                table: "PERSONAGEM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PERSONAGEM_USUARIO_ID_JOGADOR",
                table: "PERSONAGEM",
                column: "ID_JOGADOR",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
