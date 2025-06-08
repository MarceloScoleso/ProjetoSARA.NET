using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoSARA.NET.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SEQ_ALERTA");

            migrationBuilder.CreateSequence(
                name: "SEQ_LEITURA_SENSOR");

            migrationBuilder.CreateSequence(
                name: "SEQ_LOCALIZACAO");

            migrationBuilder.CreateSequence(
                name: "SEQ_NIVEL_ALERTA");

            migrationBuilder.CreateSequence(
                name: "SEQ_NOTIFICACAO");

            migrationBuilder.CreateSequence(
                name: "SEQ_SENSOR");

            migrationBuilder.CreateSequence(
                name: "SEQ_STATUS_NOTIFICACAO");

            migrationBuilder.CreateSequence(
                name: "SEQ_TIPO_SENSOR");

            migrationBuilder.CreateSequence(
                name: "SEQ_TIPO_USUARIO");

            migrationBuilder.CreateSequence(
                name: "SEQ_USUARIO");

            migrationBuilder.CreateTable(
                name: "LOCALIZACAO",
                columns: table => new
                {
                    ID_LOCALIZACAO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CIDADE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ESTADO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOCALIZACAO", x => x.ID_LOCALIZACAO);
                });

            migrationBuilder.CreateTable(
                name: "NIVELALERTA",
                columns: table => new
                {
                    ID_NIVEL_ALERTA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CODIGO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NIVELALERTA", x => x.ID_NIVEL_ALERTA);
                });

            migrationBuilder.CreateTable(
                name: "STATUSNOTIFICACAO",
                columns: table => new
                {
                    ID_STATUS_NOTIFICACAO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CODIGO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATUSNOTIFICACAO", x => x.ID_STATUS_NOTIFICACAO);
                });

            migrationBuilder.CreateTable(
                name: "TIPOSENSOR",
                columns: table => new
                {
                    ID_TIPO_SENSOR = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CODIGO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOSENSOR", x => x.ID_TIPO_SENSOR);
                });

            migrationBuilder.CreateTable(
                name: "TIPOUSUARIO",
                columns: table => new
                {
                    ID_TIPO_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CODIGO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOUSUARIO", x => x.ID_TIPO_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "SENSOR",
                columns: table => new
                {
                    ID_SENSOR = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TIPO_SENSOR_ID = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    LOCALIZACAO_ID = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SENSOR", x => x.ID_SENSOR);
                    table.ForeignKey(
                        name: "FK_SENSOR_LOCALIZACAO_LOCALIZACAO_ID",
                        column: x => x.LOCALIZACAO_ID,
                        principalTable: "LOCALIZACAO",
                        principalColumn: "ID_LOCALIZACAO");
                    table.ForeignKey(
                        name: "FK_SENSOR_TIPOSENSOR_TIPO_SENSOR_ID",
                        column: x => x.TIPO_SENSOR_ID,
                        principalTable: "TIPOSENSOR",
                        principalColumn: "ID_TIPO_SENSOR");
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CPF = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TIPO_USUARIO_ID = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID_USUARIO);
                    table.ForeignKey(
                        name: "FK_USUARIO_TIPOUSUARIO_TIPO_USUARIO_ID",
                        column: x => x.TIPO_USUARIO_ID,
                        principalTable: "TIPOUSUARIO",
                        principalColumn: "ID_TIPO_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "LEITURASENSOR",
                columns: table => new
                {
                    ID_LEITURA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SENSOR_ID = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    VALOR = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    UNIDADE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DATA_HORA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEITURASENSOR", x => x.ID_LEITURA);
                    table.ForeignKey(
                        name: "FK_LEITURASENSOR_SENSOR_SENSOR_ID",
                        column: x => x.SENSOR_ID,
                        principalTable: "SENSOR",
                        principalColumn: "ID_SENSOR");
                });

            migrationBuilder.CreateTable(
                name: "ALERTA",
                columns: table => new
                {
                    ID_ALERTA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MENSAGEM = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DATA_HORA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    USUARIO_ID = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    NIVEL_ALERTA_ID = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    LOCALIZACAO_ID = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALERTA", x => x.ID_ALERTA);
                    table.ForeignKey(
                        name: "FK_ALERTA_LOCALIZACAO_LOCALIZACAO_ID",
                        column: x => x.LOCALIZACAO_ID,
                        principalTable: "LOCALIZACAO",
                        principalColumn: "ID_LOCALIZACAO");
                    table.ForeignKey(
                        name: "FK_ALERTA_NIVELALERTA_NIVEL_ALERTA_ID",
                        column: x => x.NIVEL_ALERTA_ID,
                        principalTable: "NIVELALERTA",
                        principalColumn: "ID_NIVEL_ALERTA");
                    table.ForeignKey(
                        name: "FK_ALERTA_USUARIO_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICACAO",
                columns: table => new
                {
                    ID_NOTIFICACAO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USUARIO_ID = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    ALERTA_ID = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    STATUS_ID = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    DATA_ENVIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICACAO", x => x.ID_NOTIFICACAO);
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_ALERTA_ALERTA_ID",
                        column: x => x.ALERTA_ID,
                        principalTable: "ALERTA",
                        principalColumn: "ID_ALERTA");
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_STATUSNOTIFICACAO_STATUS_ID",
                        column: x => x.STATUS_ID,
                        principalTable: "STATUSNOTIFICACAO",
                        principalColumn: "ID_STATUS_NOTIFICACAO");
                    table.ForeignKey(
                        name: "FK_NOTIFICACAO_USUARIO_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALERTA_LOCALIZACAO_ID",
                table: "ALERTA",
                column: "LOCALIZACAO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ALERTA_NIVEL_ALERTA_ID",
                table: "ALERTA",
                column: "NIVEL_ALERTA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ALERTA_USUARIO_ID",
                table: "ALERTA",
                column: "USUARIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LEITURASENSOR_SENSOR_ID",
                table: "LEITURASENSOR",
                column: "SENSOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_ALERTA_ID",
                table: "NOTIFICACAO",
                column: "ALERTA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_STATUS_ID",
                table: "NOTIFICACAO",
                column: "STATUS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACAO_USUARIO_ID",
                table: "NOTIFICACAO",
                column: "USUARIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SENSOR_LOCALIZACAO_ID",
                table: "SENSOR",
                column: "LOCALIZACAO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SENSOR_TIPO_SENSOR_ID",
                table: "SENSOR",
                column: "TIPO_SENSOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_TIPO_USUARIO_ID",
                table: "USUARIO",
                column: "TIPO_USUARIO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LEITURASENSOR");

            migrationBuilder.DropTable(
                name: "NOTIFICACAO");

            migrationBuilder.DropTable(
                name: "SENSOR");

            migrationBuilder.DropTable(
                name: "ALERTA");

            migrationBuilder.DropTable(
                name: "STATUSNOTIFICACAO");

            migrationBuilder.DropTable(
                name: "TIPOSENSOR");

            migrationBuilder.DropTable(
                name: "LOCALIZACAO");

            migrationBuilder.DropTable(
                name: "NIVELALERTA");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "TIPOUSUARIO");

            migrationBuilder.DropSequence(
                name: "SEQ_ALERTA");

            migrationBuilder.DropSequence(
                name: "SEQ_LEITURA_SENSOR");

            migrationBuilder.DropSequence(
                name: "SEQ_LOCALIZACAO");

            migrationBuilder.DropSequence(
                name: "SEQ_NIVEL_ALERTA");

            migrationBuilder.DropSequence(
                name: "SEQ_NOTIFICACAO");

            migrationBuilder.DropSequence(
                name: "SEQ_SENSOR");

            migrationBuilder.DropSequence(
                name: "SEQ_STATUS_NOTIFICACAO");

            migrationBuilder.DropSequence(
                name: "SEQ_TIPO_SENSOR");

            migrationBuilder.DropSequence(
                name: "SEQ_TIPO_USUARIO");

            migrationBuilder.DropSequence(
                name: "SEQ_USUARIO");
        }
    }
}
