using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoad.Infrastructure.Persistencia.Migracoes
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "perfis",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    avatar_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    idioma = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_perfis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "areas_de_trabalho",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    eh_pessoal = table.Column<bool>(type: "boolean", nullable: false),
                    dono_id = table.Column<Guid>(type: "uuid", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_areas_de_trabalho", x => x.id);
                    table.ForeignKey(
                        name: "fk_areas_de_trabalho_perfis_dono_id",
                        column: x => x.dono_id,
                        principalTable: "perfis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "membros_area_de_trabalho",
                columns: table => new
                {
                    area_de_trabalho_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    papel = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    entrou_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membros_area_de_trabalho", x => new { x.area_de_trabalho_id, x.usuario_id });
                    table.ForeignKey(
                        name: "fk_membros_area_de_trabalho_areas_de_trabalho_area_de_trabalho~",
                        column: x => x.area_de_trabalho_id,
                        principalTable: "areas_de_trabalho",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_membros_area_de_trabalho_perfis_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "perfis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quadros",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    area_de_trabalho_id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cor_de_fundo = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    arquivado = table.Column<bool>(type: "boolean", nullable: false),
                    criado_por_id = table.Column<Guid>(type: "uuid", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quadros", x => x.id);
                    table.ForeignKey(
                        name: "fk_quadros_areas_de_trabalho_area_de_trabalho_id",
                        column: x => x.area_de_trabalho_id,
                        principalTable: "areas_de_trabalho",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quadros_perfis_criado_por_id",
                        column: x => x.criado_por_id,
                        principalTable: "perfis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "etiquetas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quadro_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cor = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_etiquetas", x => x.id);
                    table.ForeignKey(
                        name: "fk_etiquetas_quadros_quadro_id",
                        column: x => x.quadro_id,
                        principalTable: "quadros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "listas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quadro_id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    posicao = table.Column<double>(type: "double precision", nullable: false),
                    arquivada = table.Column<bool>(type: "boolean", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listas", x => x.id);
                    table.ForeignKey(
                        name: "fk_listas_quadros_quadro_id",
                        column: x => x.quadro_id,
                        principalTable: "quadros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cartoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    lista_id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descricao = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    posicao = table.Column<double>(type: "double precision", nullable: false),
                    prazo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    concluido_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cor_da_capa = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    arquivado = table.Column<bool>(type: "boolean", nullable: false),
                    criado_por_id = table.Column<Guid>(type: "uuid", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cartoes", x => x.id);
                    table.ForeignKey(
                        name: "fk_cartoes_listas_lista_id",
                        column: x => x.lista_id,
                        principalTable: "listas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cartoes_perfis_criado_por_id",
                        column: x => x.criado_por_id,
                        principalTable: "perfis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "atividades",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quadro_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cartao_id = table.Column<Guid>(type: "uuid", nullable: true),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    dados = table.Column<string>(type: "jsonb", nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_atividades", x => x.id);
                    table.ForeignKey(
                        name: "fk_atividades_cartoes_cartao_id",
                        column: x => x.cartao_id,
                        principalTable: "cartoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_atividades_perfis_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "perfis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_atividades_quadros_quadro_id",
                        column: x => x.quadro_id,
                        principalTable: "quadros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cartao_etiquetas",
                columns: table => new
                {
                    cartao_id = table.Column<Guid>(type: "uuid", nullable: false),
                    etiqueta_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cartao_etiquetas", x => new { x.cartao_id, x.etiqueta_id });
                    table.ForeignKey(
                        name: "fk_cartao_etiquetas_cartoes_cartao_id",
                        column: x => x.cartao_id,
                        principalTable: "cartoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cartao_etiquetas_etiquetas_etiqueta_id",
                        column: x => x.etiqueta_id,
                        principalTable: "etiquetas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cartao_responsaveis",
                columns: table => new
                {
                    cartao_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cartao_responsaveis", x => new { x.cartao_id, x.usuario_id });
                    table.ForeignKey(
                        name: "fk_cartao_responsaveis_cartoes_cartao_id",
                        column: x => x.cartao_id,
                        principalTable: "cartoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cartao_responsaveis_perfis_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "perfis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checklists",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cartao_id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    posicao = table.Column<double>(type: "double precision", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_checklists", x => x.id);
                    table.ForeignKey(
                        name: "fk_checklists_cartoes_cartao_id",
                        column: x => x.cartao_id,
                        principalTable: "cartoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cartao_id = table.Column<Guid>(type: "uuid", nullable: false),
                    autor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    texto = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    editado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_comentarios_cartoes_cartao_id",
                        column: x => x.cartao_id,
                        principalTable: "cartoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comentarios_perfis_autor_id",
                        column: x => x.autor_id,
                        principalTable: "perfis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "itens_checklist",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    checklist_id = table.Column<Guid>(type: "uuid", nullable: false),
                    texto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    concluido = table.Column<bool>(type: "boolean", nullable: false),
                    posicao = table.Column<double>(type: "double precision", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_itens_checklist", x => x.id);
                    table.ForeignKey(
                        name: "fk_itens_checklist_checklists_checklist_id",
                        column: x => x.checklist_id,
                        principalTable: "checklists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_areas_de_trabalho_dono_id",
                table: "areas_de_trabalho",
                column: "dono_id");

            migrationBuilder.CreateIndex(
                name: "ix_atividades_cartao_id",
                table: "atividades",
                column: "cartao_id");

            migrationBuilder.CreateIndex(
                name: "ix_atividades_quadro_id_criado_em",
                table: "atividades",
                columns: new[] { "quadro_id", "criado_em" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "ix_atividades_usuario_id",
                table: "atividades",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_cartao_etiquetas_etiqueta_id",
                table: "cartao_etiquetas",
                column: "etiqueta_id");

            migrationBuilder.CreateIndex(
                name: "ix_cartao_responsaveis_usuario_id",
                table: "cartao_responsaveis",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_cartoes_criado_por_id",
                table: "cartoes",
                column: "criado_por_id");

            migrationBuilder.CreateIndex(
                name: "ix_cartoes_lista_id_posicao",
                table: "cartoes",
                columns: new[] { "lista_id", "posicao" });

            migrationBuilder.CreateIndex(
                name: "ix_checklists_cartao_id",
                table: "checklists",
                column: "cartao_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_autor_id",
                table: "comentarios",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_cartao_id_criado_em",
                table: "comentarios",
                columns: new[] { "cartao_id", "criado_em" });

            migrationBuilder.CreateIndex(
                name: "ix_etiquetas_quadro_id",
                table: "etiquetas",
                column: "quadro_id");

            migrationBuilder.CreateIndex(
                name: "ix_itens_checklist_checklist_id",
                table: "itens_checklist",
                column: "checklist_id");

            migrationBuilder.CreateIndex(
                name: "ix_listas_quadro_id_posicao",
                table: "listas",
                columns: new[] { "quadro_id", "posicao" });

            migrationBuilder.CreateIndex(
                name: "ix_membros_area_de_trabalho_usuario_id",
                table: "membros_area_de_trabalho",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_perfis_email",
                table: "perfis",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_quadros_area_de_trabalho_id",
                table: "quadros",
                column: "area_de_trabalho_id");

            migrationBuilder.CreateIndex(
                name: "ix_quadros_criado_por_id",
                table: "quadros",
                column: "criado_por_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atividades");

            migrationBuilder.DropTable(
                name: "cartao_etiquetas");

            migrationBuilder.DropTable(
                name: "cartao_responsaveis");

            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "itens_checklist");

            migrationBuilder.DropTable(
                name: "membros_area_de_trabalho");

            migrationBuilder.DropTable(
                name: "etiquetas");

            migrationBuilder.DropTable(
                name: "checklists");

            migrationBuilder.DropTable(
                name: "cartoes");

            migrationBuilder.DropTable(
                name: "listas");

            migrationBuilder.DropTable(
                name: "quadros");

            migrationBuilder.DropTable(
                name: "areas_de_trabalho");

            migrationBuilder.DropTable(
                name: "perfis");
        }
    }
}
