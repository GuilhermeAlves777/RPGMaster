using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGMaster.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    SOBRENOME = table.Column<string>(type: "TEXT", nullable: false),
                    EMAIL = table.Column<string>(type: "TEXT", nullable: false),
                    USER = table.Column<string>(type: "TEXT", nullable: false),
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    DATA_NASC = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SENHA = table.Column<string>(type: "TEXT", nullable: false),
                    IMAGEM = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "CAMPANHA",
                columns: table => new
                {
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    ID_CRIADOR = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAMPANHA", x => x.ID_CAMPANHA);
                    table.ForeignKey(
                        name: "FK_CAMPANHA_USUARIO_ID_CRIADOR",
                        column: x => x.ID_CRIADOR,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ATRIBUTO",
                columns: table => new
                {
                    ID_ATRIBUTO = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    VALOR_PADRAO = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATRIBUTO", x => x.ID_ATRIBUTO);
                    table.ForeignKey(
                        name: "FK_ATRIBUTO_CAMPANHA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHA",
                        principalColumn: "ID_CAMPANHA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAMPANHA_JOGADOR",
                columns: table => new
                {
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_USUARIO = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAMPANHA_JOGADOR", x => new { x.ID_CAMPANHA, x.ID_USUARIO });
                    table.ForeignKey(
                        name: "FK_CAMPANHA_JOGADOR_CAMPANHA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHA",
                        principalColumn: "ID_CAMPANHA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAMPANHA_JOGADOR_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CLASSE",
                columns: table => new
                {
                    ID_CLASSE = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    DESCRICAO = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLASSE", x => x.ID_CLASSE);
                    table.ForeignKey(
                        name: "FK_CLASSE_CAMPANHA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHA",
                        principalColumn: "ID_CAMPANHA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITEM",
                columns: table => new
                {
                    ID_ITEM = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    TIPO = table.Column<int>(type: "INTEGER", nullable: false),
                    DESCRICAO = table.Column<string>(type: "TEXT", nullable: true),
                    IMAGEM = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEM", x => x.ID_ITEM);
                    table.ForeignKey(
                        name: "FK_ITEM_CAMPANHA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHA",
                        principalColumn: "ID_CAMPANHA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MAGIA",
                columns: table => new
                {
                    ID_MAGIA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    DADOS = table.Column<string>(type: "TEXT", nullable: false),
                    Imagem = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAGIA", x => x.ID_MAGIA);
                    table.ForeignKey(
                        name: "FK_MAGIA_CAMPANHA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHA",
                        principalColumn: "ID_CAMPANHA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERICIA",
                columns: table => new
                {
                    ID_PERICIA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    VALOR_PADRAO = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERICIA", x => x.ID_PERICIA);
                    table.ForeignKey(
                        name: "FK_PERICIA_CAMPANHA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHA",
                        principalColumn: "ID_CAMPANHA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RACA",
                columns: table => new
                {
                    ID_RACA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    DESCRICAO = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RACA", x => x.ID_RACA);
                    table.ForeignKey(
                        name: "FK_RACA_CAMPANHA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHA",
                        principalColumn: "ID_CAMPANHA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLASSE_ATRIBUTO",
                columns: table => new
                {
                    ID_CLASSE = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_ATRIBUTO = table.Column<long>(type: "INTEGER", nullable: false),
                    MODIFICADOR = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLASSE_ATRIBUTO", x => new { x.ID_CLASSE, x.ID_ATRIBUTO });
                    table.ForeignKey(
                        name: "FK_CLASSE_ATRIBUTO_ATRIBUTO_ID_ATRIBUTO",
                        column: x => x.ID_ATRIBUTO,
                        principalTable: "ATRIBUTO",
                        principalColumn: "ID_ATRIBUTO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CLASSE_ATRIBUTO_CLASSE_ID_CLASSE",
                        column: x => x.ID_CLASSE,
                        principalTable: "CLASSE",
                        principalColumn: "ID_CLASSE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ARMA_DETALHE",
                columns: table => new
                {
                    ID_ITEM = table.Column<long>(type: "INTEGER", nullable: false),
                    DANO = table.Column<string>(type: "TEXT", nullable: false),
                    TIPO_DANO = table.Column<int>(type: "INTEGER", nullable: false),
                    ALCANCE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARMA_DETALHE", x => x.ID_ITEM);
                    table.ForeignKey(
                        name: "FK_ARMA_DETALHE_ITEM_ID_ITEM",
                        column: x => x.ID_ITEM,
                        principalTable: "ITEM",
                        principalColumn: "ID_ITEM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ARMADURA_DETALHE",
                columns: table => new
                {
                    ID_ITEM = table.Column<long>(type: "INTEGER", nullable: false),
                    DEFESA = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARMADURA_DETALHE", x => x.ID_ITEM);
                    table.ForeignKey(
                        name: "FK_ARMADURA_DETALHE_ITEM_ID_ITEM",
                        column: x => x.ID_ITEM,
                        principalTable: "ITEM",
                        principalColumn: "ID_ITEM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITEM_ATRIBUTO",
                columns: table => new
                {
                    ID_ITEM = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_ATRIBUTO = table.Column<long>(type: "INTEGER", nullable: false),
                    MODIFICADOR = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEM_ATRIBUTO", x => new { x.ID_ITEM, x.ID_ATRIBUTO });
                    table.ForeignKey(
                        name: "FK_ITEM_ATRIBUTO_ATRIBUTO_ID_ATRIBUTO",
                        column: x => x.ID_ATRIBUTO,
                        principalTable: "ATRIBUTO",
                        principalColumn: "ID_ATRIBUTO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ITEM_ATRIBUTO_ITEM_ID_ITEM",
                        column: x => x.ID_ITEM,
                        principalTable: "ITEM",
                        principalColumn: "ID_ITEM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSONAGEM",
                columns: table => new
                {
                    ID_PERSONAGEM = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    Imagem = table.Column<string>(type: "TEXT", nullable: true),
                    ID_CAMPANHA = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_JOGADOR = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_CLASSE = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_RACA = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSONAGEM", x => x.ID_PERSONAGEM);
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_CAMPANHA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHA",
                        principalColumn: "ID_CAMPANHA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_CLASSE_ID_CLASSE",
                        column: x => x.ID_CLASSE,
                        principalTable: "CLASSE",
                        principalColumn: "ID_CLASSE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_RACA_ID_RACA",
                        column: x => x.ID_RACA,
                        principalTable: "RACA",
                        principalColumn: "ID_RACA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_USUARIO_ID_JOGADOR",
                        column: x => x.ID_JOGADOR,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RACA_ATRIBUTO",
                columns: table => new
                {
                    ID_RACA = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_ATRIBUTO = table.Column<long>(type: "INTEGER", nullable: false),
                    MODIFICADOR = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RACA_ATRIBUTO", x => new { x.ID_RACA, x.ID_ATRIBUTO });
                    table.ForeignKey(
                        name: "FK_RACA_ATRIBUTO_ATRIBUTO_ID_ATRIBUTO",
                        column: x => x.ID_ATRIBUTO,
                        principalTable: "ATRIBUTO",
                        principalColumn: "ID_ATRIBUTO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RACA_ATRIBUTO_RACA_ID_RACA",
                        column: x => x.ID_RACA,
                        principalTable: "RACA",
                        principalColumn: "ID_RACA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSONAGEM_ATRIBUTO",
                columns: table => new
                {
                    ID_PERSONAGEM = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_ATRIBUTO = table.Column<long>(type: "INTEGER", nullable: false),
                    VALOR = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSONAGEM_ATRIBUTO", x => new { x.ID_PERSONAGEM, x.ID_ATRIBUTO });
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_ATRIBUTO_ATRIBUTO_ID_ATRIBUTO",
                        column: x => x.ID_ATRIBUTO,
                        principalTable: "ATRIBUTO",
                        principalColumn: "ID_ATRIBUTO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_ATRIBUTO_PERSONAGEM_ID_PERSONAGEM",
                        column: x => x.ID_PERSONAGEM,
                        principalTable: "PERSONAGEM",
                        principalColumn: "ID_PERSONAGEM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSONAGEM_ITEM",
                columns: table => new
                {
                    ID_PERSONAGEM = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_ITEM = table.Column<long>(type: "INTEGER", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSONAGEM_ITEM", x => new { x.ID_PERSONAGEM, x.ID_ITEM });
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_ITEM_ITEM_ID_ITEM",
                        column: x => x.ID_ITEM,
                        principalTable: "ITEM",
                        principalColumn: "ID_ITEM",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_ITEM_PERSONAGEM_ID_PERSONAGEM",
                        column: x => x.ID_PERSONAGEM,
                        principalTable: "PERSONAGEM",
                        principalColumn: "ID_PERSONAGEM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSONAGEM_MAGIA",
                columns: table => new
                {
                    ID_PERSONAGEM = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_MAGIA = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSONAGEM_MAGIA", x => new { x.ID_PERSONAGEM, x.ID_MAGIA });
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_MAGIA_MAGIA_ID_MAGIA",
                        column: x => x.ID_MAGIA,
                        principalTable: "MAGIA",
                        principalColumn: "ID_MAGIA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_MAGIA_PERSONAGEM_ID_PERSONAGEM",
                        column: x => x.ID_PERSONAGEM,
                        principalTable: "PERSONAGEM",
                        principalColumn: "ID_PERSONAGEM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSONAGEM_PERICIA",
                columns: table => new
                {
                    ID_PERSONAGEM = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_PERICIA = table.Column<long>(type: "INTEGER", nullable: false),
                    VALOR = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSONAGEM_PERICIA", x => new { x.ID_PERSONAGEM, x.ID_PERICIA });
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_PERICIA_PERICIA_ID_PERICIA",
                        column: x => x.ID_PERICIA,
                        principalTable: "PERICIA",
                        principalColumn: "ID_PERICIA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSONAGEM_PERICIA_PERSONAGEM_ID_PERSONAGEM",
                        column: x => x.ID_PERSONAGEM,
                        principalTable: "PERSONAGEM",
                        principalColumn: "ID_PERSONAGEM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATRIBUTO_ID_CAMPANHA",
                table: "ATRIBUTO",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "IX_CAMPANHA_ID_CRIADOR",
                table: "CAMPANHA",
                column: "ID_CRIADOR");

            migrationBuilder.CreateIndex(
                name: "IX_CAMPANHA_JOGADOR_ID_USUARIO",
                table: "CAMPANHA_JOGADOR",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_CLASSE_ID_CAMPANHA",
                table: "CLASSE",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "IX_CLASSE_ATRIBUTO_ID_ATRIBUTO",
                table: "CLASSE_ATRIBUTO",
                column: "ID_ATRIBUTO");

            migrationBuilder.CreateIndex(
                name: "IX_ITEM_ID_CAMPANHA",
                table: "ITEM",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "IX_ITEM_ATRIBUTO_ID_ATRIBUTO",
                table: "ITEM_ATRIBUTO",
                column: "ID_ATRIBUTO");

            migrationBuilder.CreateIndex(
                name: "IX_MAGIA_ID_CAMPANHA",
                table: "MAGIA",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "IX_PERICIA_ID_CAMPANHA",
                table: "PERICIA",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONAGEM_ID_CAMPANHA",
                table: "PERSONAGEM",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONAGEM_ID_CLASSE",
                table: "PERSONAGEM",
                column: "ID_CLASSE");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONAGEM_ID_JOGADOR",
                table: "PERSONAGEM",
                column: "ID_JOGADOR");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONAGEM_ID_RACA",
                table: "PERSONAGEM",
                column: "ID_RACA");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONAGEM_ATRIBUTO_ID_ATRIBUTO",
                table: "PERSONAGEM_ATRIBUTO",
                column: "ID_ATRIBUTO");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONAGEM_ITEM_ID_ITEM",
                table: "PERSONAGEM_ITEM",
                column: "ID_ITEM");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONAGEM_MAGIA_ID_MAGIA",
                table: "PERSONAGEM_MAGIA",
                column: "ID_MAGIA");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONAGEM_PERICIA_ID_PERICIA",
                table: "PERSONAGEM_PERICIA",
                column: "ID_PERICIA");

            migrationBuilder.CreateIndex(
                name: "IX_RACA_ID_CAMPANHA",
                table: "RACA",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "IX_RACA_ATRIBUTO_ID_ATRIBUTO",
                table: "RACA_ATRIBUTO",
                column: "ID_ATRIBUTO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ARMA_DETALHE");

            migrationBuilder.DropTable(
                name: "ARMADURA_DETALHE");

            migrationBuilder.DropTable(
                name: "CAMPANHA_JOGADOR");

            migrationBuilder.DropTable(
                name: "CLASSE_ATRIBUTO");

            migrationBuilder.DropTable(
                name: "ITEM_ATRIBUTO");

            migrationBuilder.DropTable(
                name: "PERSONAGEM_ATRIBUTO");

            migrationBuilder.DropTable(
                name: "PERSONAGEM_ITEM");

            migrationBuilder.DropTable(
                name: "PERSONAGEM_MAGIA");

            migrationBuilder.DropTable(
                name: "PERSONAGEM_PERICIA");

            migrationBuilder.DropTable(
                name: "RACA_ATRIBUTO");

            migrationBuilder.DropTable(
                name: "ITEM");

            migrationBuilder.DropTable(
                name: "MAGIA");

            migrationBuilder.DropTable(
                name: "PERICIA");

            migrationBuilder.DropTable(
                name: "PERSONAGEM");

            migrationBuilder.DropTable(
                name: "ATRIBUTO");

            migrationBuilder.DropTable(
                name: "CLASSE");

            migrationBuilder.DropTable(
                name: "RACA");

            migrationBuilder.DropTable(
                name: "CAMPANHA");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
