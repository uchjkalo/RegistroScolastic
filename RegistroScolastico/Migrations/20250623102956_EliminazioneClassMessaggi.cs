using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroScolastico.Migrations
{
    /// <inheritdoc />
    public partial class EliminazioneClassMessaggi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messaggi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messaggi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinatarioId = table.Column<int>(type: "int", nullable: false),
                    MittenteId = table.Column<int>(type: "int", nullable: false),
                    DataInvio = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Letto = table.Column<bool>(type: "bit", nullable: false),
                    Testo = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messaggi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messaggi_Utenti_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messaggi_Utenti_MittenteId",
                        column: x => x.MittenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messaggi_DestinatarioId",
                table: "Messaggi",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Messaggi_MittenteId",
                table: "Messaggi",
                column: "MittenteId");
        }
    }
}
