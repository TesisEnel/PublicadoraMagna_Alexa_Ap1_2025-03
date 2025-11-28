using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class servicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloServicioPromocional_ServicioPromocional_ServicioPromocionalId",
                table: "ArticuloServicioPromocional");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicioPromocional",
                table: "ServicioPromocional");

            migrationBuilder.RenameTable(
                name: "ServicioPromocional",
                newName: "ServiciosPromocionales");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiciosPromocionales",
                table: "ServiciosPromocionales",
                column: "ServicioPromocionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloServicioPromocional_ServiciosPromocionales_ServicioPromocionalId",
                table: "ArticuloServicioPromocional",
                column: "ServicioPromocionalId",
                principalTable: "ServiciosPromocionales",
                principalColumn: "ServicioPromocionalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloServicioPromocional_ServiciosPromocionales_ServicioPromocionalId",
                table: "ArticuloServicioPromocional");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiciosPromocionales",
                table: "ServiciosPromocionales");

            migrationBuilder.RenameTable(
                name: "ServiciosPromocionales",
                newName: "ServicioPromocional");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicioPromocional",
                table: "ServicioPromocional",
                column: "ServicioPromocionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloServicioPromocional_ServicioPromocional_ServicioPromocionalId",
                table: "ArticuloServicioPromocional",
                column: "ServicioPromocionalId",
                principalTable: "ServicioPromocional",
                principalColumn: "ServicioPromocionalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
