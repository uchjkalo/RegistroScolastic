using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroScolastico.Migrations
{
    /// <inheritdoc />
    public partial class Finalizzazione1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classi_AnnoId",
                table: "Classi");

            migrationBuilder.CreateIndex(
                name: "IX_Classi_AnnoId_SezioneId_AnnoFormativoId_CorsoId",
                table: "Classi",
                columns: new[] { "AnnoId", "SezioneId", "AnnoFormativoId", "CorsoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classi_AnnoId_SezioneId_AnnoFormativoId_CorsoId",
                table: "Classi");

            migrationBuilder.CreateIndex(
                name: "IX_Classi_AnnoId",
                table: "Classi",
                column: "AnnoId");
        }
    }
}
