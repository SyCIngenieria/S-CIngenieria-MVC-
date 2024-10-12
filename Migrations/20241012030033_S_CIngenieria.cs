using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace S_CIngenieria.Migrations
{
    /// <inheritdoc />
    public partial class S_CIngenieria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NIT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreModulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProyecto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescripcionProyecto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Troncales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTroncal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Troncales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroContrato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Objeto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmpresasId = table.Column<int>(type: "int", nullable: false),
                    NombrePersonaContrato = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroDocumentoPersonaContrato = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailPersonaContrato = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TelefonoPersonaContrato = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValorContrato = table.Column<decimal>(type: "decimal(18,2)", maxLength: 50, nullable: false),
                    DuracionDiascontrato = table.Column<int>(type: "int", nullable: false),
                    Fecha_Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Fin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratos_Empresas_EmpresasId",
                        column: x => x.EmpresasId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelacionalModulosRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkModulo = table.Column<int>(type: "int", nullable: false),
                    FkRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelacionalModulosRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelacionalModulosRoles_Modulos_FkModulo",
                        column: x => x.FkModulo,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelacionalModulosRoles_Roles_FkRol",
                        column: x => x.FkRol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fotoPerfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaExpira = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FkRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_FkRol",
                        column: x => x.FkRol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AmpliacionContratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    FechaInicioAmpliacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinAmpliacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorAmpliacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroDiasAmpliacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmpliacionContratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmpliacionContratos_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkRelacionalModulosRoles = table.Column<int>(type: "int", nullable: false),
                    Lectura = table.Column<bool>(type: "bit", nullable: false),
                    Editar = table.Column<bool>(type: "bit", nullable: false),
                    Actualizar = table.Column<bool>(type: "bit", nullable: false),
                    Insertar = table.Column<bool>(type: "bit", nullable: false),
                    Eliminar = table.Column<bool>(type: "bit", nullable: false),
                    Exportar = table.Column<bool>(type: "bit", nullable: false),
                    Importar = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permisos_RelacionalModulosRoles_FkRelacionalModulosRoles",
                        column: x => x.FkRelacionalModulosRoles,
                        principalTable: "RelacionalModulosRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "oDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratosId = table.Column<int>(type: "int", nullable: false),
                    AmpliacionContratoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ValorInicalODS = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    DiasEjecuCionODS = table.Column<int>(type: "int", nullable: false),
                    FechaInicioODS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinODS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuspensionFechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspensionFechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorActual = table.Column<decimal>(type: "decimal(18,2)", maxLength: 50, nullable: false),
                    EstadoODS = table.Column<int>(type: "int", nullable: false),
                    Supervisor = table.Column<int>(type: "int", nullable: false),
                    SolicitanteCliente = table.Column<int>(type: "int", nullable: false),
                    RecursoODS = table.Column<int>(type: "int", nullable: false),
                    PlantaSistema = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConexoObra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProyectosId = table.Column<int>(type: "int", nullable: false),
                    TroncalesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_oDs_AmpliacionContratos_AmpliacionContratoId",
                        column: x => x.AmpliacionContratoId,
                        principalTable: "AmpliacionContratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_oDs_Contratos_ContratosId",
                        column: x => x.ContratosId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_oDs_Proyectos_ProyectosId",
                        column: x => x.ProyectosId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_oDs_Troncales_TroncalesId",
                        column: x => x.TroncalesId,
                        principalTable: "Troncales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_oDs_Usuarios_SolicitanteCliente",
                        column: x => x.SolicitanteCliente,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_oDs_Usuarios_Supervisor",
                        column: x => x.Supervisor,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Actas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ODSId = table.Column<int>(type: "int", nullable: false),
                    NombreActa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DocumentoAdjunto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actas_oDs_ODSId",
                        column: x => x.ODSId,
                        principalTable: "oDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ordenCambios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ODSId = table.Column<int>(type: "int", nullable: false),
                    FechaInicioAmpliacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinAmpliacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorAmpliacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroDiasAmpliacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordenCambios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ordenCambios_oDs_ODSId",
                        column: x => x.ODSId,
                        principalTable: "oDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actas_ODSId",
                table: "Actas",
                column: "ODSId");

            migrationBuilder.CreateIndex(
                name: "IX_AmpliacionContratos_ContratoId",
                table: "AmpliacionContratos",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EmpresasId",
                table: "Contratos",
                column: "EmpresasId");

            migrationBuilder.CreateIndex(
                name: "IX_oDs_AmpliacionContratoId",
                table: "oDs",
                column: "AmpliacionContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_oDs_ContratosId",
                table: "oDs",
                column: "ContratosId");

            migrationBuilder.CreateIndex(
                name: "IX_oDs_ProyectosId",
                table: "oDs",
                column: "ProyectosId");

            migrationBuilder.CreateIndex(
                name: "IX_oDs_SolicitanteCliente",
                table: "oDs",
                column: "SolicitanteCliente");

            migrationBuilder.CreateIndex(
                name: "IX_oDs_Supervisor",
                table: "oDs",
                column: "Supervisor");

            migrationBuilder.CreateIndex(
                name: "IX_oDs_TroncalesId",
                table: "oDs",
                column: "TroncalesId");

            migrationBuilder.CreateIndex(
                name: "IX_ordenCambios_ODSId",
                table: "ordenCambios",
                column: "ODSId");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_FkRelacionalModulosRoles",
                table: "Permisos",
                column: "FkRelacionalModulosRoles");

            migrationBuilder.CreateIndex(
                name: "IX_RelacionalModulosRoles_FkModulo",
                table: "RelacionalModulosRoles",
                column: "FkModulo");

            migrationBuilder.CreateIndex(
                name: "IX_RelacionalModulosRoles_FkRol",
                table: "RelacionalModulosRoles",
                column: "FkRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FkRol",
                table: "Usuarios",
                column: "FkRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actas");

            migrationBuilder.DropTable(
                name: "ordenCambios");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "oDs");

            migrationBuilder.DropTable(
                name: "RelacionalModulosRoles");

            migrationBuilder.DropTable(
                name: "AmpliacionContratos");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Troncales");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
