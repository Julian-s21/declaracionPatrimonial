using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace declaracionPatrimonial.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    IDBanco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_Banco = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banco", x => x.IDBanco);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipoCuenta",
                columns: table => new
                {
                    IDtipoCuenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_tipoCuenta = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoCuenta", x => x.IDtipoCuenta);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipoInmueble",
                columns: table => new
                {
                    IDtipoInmueble = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_tipoInmueble = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoInmueble", x => x.IDtipoInmueble);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipoPropiedad",
                columns: table => new
                {
                    IDtipoPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_tipoPropiedad = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoPropiedad", x => x.IDtipoPropiedad);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IDUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_Usuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido_Usuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Correo_Usuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contrasena_Usuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rol_Usuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IDUsuario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bienes",
                columns: table => new
                {
                    IDBienes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_Bienes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_adquisicionBien = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descripcion_Bien = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Forma_adquisicionBien = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Precio_Bien = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IDUsuario = table.Column<int>(type: "int", nullable: false),
                    IDtipoInmueble = table.Column<int>(type: "int", nullable: true),
                    IDtipoPropiedad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bienes", x => x.IDBienes);
                    table.ForeignKey(
                        name: "FK_Bienes_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bienes_tipoInmueble_IDtipoInmueble",
                        column: x => x.IDtipoInmueble,
                        principalTable: "tipoInmueble",
                        principalColumn: "IDtipoInmueble",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bienes_tipoPropiedad_IDtipoPropiedad",
                        column: x => x.IDtipoPropiedad,
                        principalTable: "tipoPropiedad",
                        principalColumn: "IDtipoPropiedad",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cuentaBancaria",
                columns: table => new
                {
                    IDCuentaBancaria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numero_cuentaBancaria = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Saldo_cuentaBancaria = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    IDUsuario = table.Column<int>(type: "int", nullable: false),
                    IDBanco = table.Column<int>(type: "int", nullable: false),
                    IDtipoCuenta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuentaBancaria", x => x.IDCuentaBancaria);
                    table.ForeignKey(
                        name: "FK_cuentaBancaria_Banco_IDBanco",
                        column: x => x.IDBanco,
                        principalTable: "Banco",
                        principalColumn: "IDBanco",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cuentaBancaria_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cuentaBancaria_tipoCuenta_IDtipoCuenta",
                        column: x => x.IDtipoCuenta,
                        principalTable: "tipoCuenta",
                        principalColumn: "IDtipoCuenta",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "creditoBancario",
                columns: table => new
                {
                    IDCreditoBancario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cantidad_AprobadaCredito = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Motivo_creditoBancario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IDUsuario = table.Column<int>(type: "int", nullable: false),
                    IDBanco = table.Column<int>(type: "int", nullable: false),
                    IDtipoCuenta = table.Column<int>(type: "int", nullable: false),
                    IDCuentaBancaria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creditoBancario", x => x.IDCreditoBancario);
                    table.ForeignKey(
                        name: "FK_creditoBancario_Banco_IDBanco",
                        column: x => x.IDBanco,
                        principalTable: "Banco",
                        principalColumn: "IDBanco",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_creditoBancario_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_creditoBancario_cuentaBancaria_IDCuentaBancaria",
                        column: x => x.IDCuentaBancaria,
                        principalTable: "cuentaBancaria",
                        principalColumn: "IDCuentaBancaria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_creditoBancario_tipoCuenta_IDtipoCuenta",
                        column: x => x.IDtipoCuenta,
                        principalTable: "tipoCuenta",
                        principalColumn: "IDtipoCuenta",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "otroPasivo",
                columns: table => new
                {
                    IDPasivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cantidad_aprobadaPasivo = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Motivo_Pasivo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IDUsuario = table.Column<int>(type: "int", nullable: false),
                    IDCuentaBancaria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_otroPasivo", x => x.IDPasivo);
                    table.ForeignKey(
                        name: "FK_otroPasivo_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_otroPasivo_cuentaBancaria_IDCuentaBancaria",
                        column: x => x.IDCuentaBancaria,
                        principalTable: "cuentaBancaria",
                        principalColumn: "IDCuentaBancaria",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bienes_IDtipoInmueble",
                table: "Bienes",
                column: "IDtipoInmueble");

            migrationBuilder.CreateIndex(
                name: "IX_Bienes_IDtipoPropiedad",
                table: "Bienes",
                column: "IDtipoPropiedad");

            migrationBuilder.CreateIndex(
                name: "IX_Bienes_IDUsuario",
                table: "Bienes",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_creditoBancario_IDBanco",
                table: "creditoBancario",
                column: "IDBanco");

            migrationBuilder.CreateIndex(
                name: "IX_creditoBancario_IDCuentaBancaria",
                table: "creditoBancario",
                column: "IDCuentaBancaria");

            migrationBuilder.CreateIndex(
                name: "IX_creditoBancario_IDtipoCuenta",
                table: "creditoBancario",
                column: "IDtipoCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_creditoBancario_IDUsuario",
                table: "creditoBancario",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_cuentaBancaria_IDBanco",
                table: "cuentaBancaria",
                column: "IDBanco");

            migrationBuilder.CreateIndex(
                name: "IX_cuentaBancaria_IDtipoCuenta",
                table: "cuentaBancaria",
                column: "IDtipoCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_cuentaBancaria_IDUsuario",
                table: "cuentaBancaria",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_otroPasivo_IDCuentaBancaria",
                table: "otroPasivo",
                column: "IDCuentaBancaria");

            migrationBuilder.CreateIndex(
                name: "IX_otroPasivo_IDUsuario",
                table: "otroPasivo",
                column: "IDUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bienes");

            migrationBuilder.DropTable(
                name: "creditoBancario");

            migrationBuilder.DropTable(
                name: "otroPasivo");

            migrationBuilder.DropTable(
                name: "tipoInmueble");

            migrationBuilder.DropTable(
                name: "tipoPropiedad");

            migrationBuilder.DropTable(
                name: "cuentaBancaria");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "tipoCuenta");
        }
    }
}
