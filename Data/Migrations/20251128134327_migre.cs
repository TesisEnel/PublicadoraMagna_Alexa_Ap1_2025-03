using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class migre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPagos_Articulos_ArticuloId",
                table: "DetallesPagos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPagos_Pagos_PagoInstitucionId",
                table: "DetallesPagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Instituciones_InstitucionId",
                table: "Pagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pagos",
                table: "Pagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesPagos",
                table: "DetallesPagos");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Articulos");

            migrationBuilder.RenameTable(
                name: "Pagos",
                newName: "PagosInstitucion");

            migrationBuilder.RenameTable(
                name: "DetallesPagos",
                newName: "DetallesPagosInstitucion");

            migrationBuilder.RenameIndex(
                name: "IX_Pagos_InstitucionId",
                table: "PagosInstitucion",
                newName: "IX_PagosInstitucion_InstitucionId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPagos_PagoInstitucionId",
                table: "DetallesPagosInstitucion",
                newName: "IX_DetallesPagosInstitucion_PagoInstitucionId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPagos_ArticuloId",
                table: "DetallesPagosInstitucion",
                newName: "IX_DetallesPagosInstitucion_ArticuloId");

            migrationBuilder.AddColumn<bool>(
                name: "EsActivo",
                table: "Periodistas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "Periodistas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmailAdmin",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EsLibre",
                table: "Articulos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PagosInstitucion",
                table: "PagosInstitucion",
                column: "PagoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesPagosInstitucion",
                table: "DetallesPagosInstitucion",
                column: "DetallePagoInstitucionId");

            migrationBuilder.CreateTable(
                name: "PagosPeriodistas",
                columns: table => new
                {
                    PagoPeriodistaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodistaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagosPeriodistas", x => x.PagoPeriodistaId);
                    table.ForeignKey(
                        name: "FK_PagosPeriodistas_Periodistas_PeriodistaId",
                        column: x => x.PeriodistaId,
                        principalTable: "Periodistas",
                        principalColumn: "PeriodistaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesPagosPeriodistas",
                columns: table => new
                {
                    DetallePagoPeriodistaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PagoPeriodistaId = table.Column<int>(type: "int", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPagosPeriodistas", x => x.DetallePagoPeriodistaId);
                    table.ForeignKey(
                        name: "FK_DetallesPagosPeriodistas_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "ArticuloId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesPagosPeriodistas_PagosPeriodistas_PagoPeriodistaId",
                        column: x => x.PagoPeriodistaId,
                        principalTable: "PagosPeriodistas",
                        principalColumn: "PagoPeriodistaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPagosPeriodistas_ArticuloId",
                table: "DetallesPagosPeriodistas",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPagosPeriodistas_PagoPeriodistaId",
                table: "DetallesPagosPeriodistas",
                column: "PagoPeriodistaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosPeriodistas_PeriodistaId",
                table: "PagosPeriodistas",
                column: "PeriodistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPagosInstitucion_Articulos_ArticuloId",
                table: "DetallesPagosInstitucion",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "ArticuloId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPagosInstitucion_PagosInstitucion_PagoInstitucionId",
                table: "DetallesPagosInstitucion",
                column: "PagoInstitucionId",
                principalTable: "PagosInstitucion",
                principalColumn: "PagoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PagosInstitucion_Instituciones_InstitucionId",
                table: "PagosInstitucion",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "InstitucionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPagosInstitucion_Articulos_ArticuloId",
                table: "DetallesPagosInstitucion");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPagosInstitucion_PagosInstitucion_PagoInstitucionId",
                table: "DetallesPagosInstitucion");

            migrationBuilder.DropForeignKey(
                name: "FK_PagosInstitucion_Instituciones_InstitucionId",
                table: "PagosInstitucion");

            migrationBuilder.DropTable(
                name: "DetallesPagosPeriodistas");

            migrationBuilder.DropTable(
                name: "PagosPeriodistas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PagosInstitucion",
                table: "PagosInstitucion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesPagosInstitucion",
                table: "DetallesPagosInstitucion");

            migrationBuilder.DropColumn(
                name: "EsActivo",
                table: "Periodistas");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "Periodistas");

            migrationBuilder.DropColumn(
                name: "EmailAdmin",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "EsLibre",
                table: "Articulos");

            migrationBuilder.RenameTable(
                name: "PagosInstitucion",
                newName: "Pagos");

            migrationBuilder.RenameTable(
                name: "DetallesPagosInstitucion",
                newName: "DetallesPagos");

            migrationBuilder.RenameIndex(
                name: "IX_PagosInstitucion_InstitucionId",
                table: "Pagos",
                newName: "IX_Pagos_InstitucionId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPagosInstitucion_PagoInstitucionId",
                table: "DetallesPagos",
                newName: "IX_DetallesPagos_PagoInstitucionId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPagosInstitucion_ArticuloId",
                table: "DetallesPagos",
                newName: "IX_DetallesPagos_ArticuloId");

            migrationBuilder.AddColumn<string>(
                name: "AutorId",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pagos",
                table: "Pagos",
                column: "PagoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesPagos",
                table: "DetallesPagos",
                column: "DetallePagoInstitucionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPagos_Articulos_ArticuloId",
                table: "DetallesPagos",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "ArticuloId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPagos_Pagos_PagoInstitucionId",
                table: "DetallesPagos",
                column: "PagoInstitucionId",
                principalTable: "Pagos",
                principalColumn: "PagoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Instituciones_InstitucionId",
                table: "Pagos",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "InstitucionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
