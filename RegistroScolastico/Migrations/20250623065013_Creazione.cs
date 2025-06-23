using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroScolastico.Migrations
{
    /// <inheritdoc />
    public partial class Creazione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnniFormativi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataInizio = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataFine = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnniFormativi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaValutazione",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Valore = table.Column<int>(type: "int", nullable: false),
                    Colore = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataModifica = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaValutazione", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollegiDocenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegiDocenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Corsi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corsi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    DataModifica = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moduli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    DataModifica = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sezioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sezioni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruolo = table.Column<int>(type: "int", nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataUltimoAccesso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Attivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuliMaterie",
                columns: table => new
                {
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuliMaterie", x => new { x.ModuloId, x.MateriaId });
                    table.ForeignKey(
                        name: "FK_ModuliMaterie_Materie_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuliMaterie_Moduli_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Moduli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnoId = table.Column<int>(type: "int", nullable: false),
                    SezioneId = table.Column<int>(type: "int", nullable: false),
                    AnnoFormativoId = table.Column<int>(type: "int", nullable: false),
                    CorsoId = table.Column<int>(type: "int", nullable: false),
                    CollegioDocentiId = table.Column<int>(type: "int", nullable: true),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classi_AnniFormativi_AnnoFormativoId",
                        column: x => x.AnnoFormativoId,
                        principalTable: "AnniFormativi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classi_Anni_AnnoId",
                        column: x => x.AnnoId,
                        principalTable: "Anni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classi_CollegiDocenti_CollegioDocentiId",
                        column: x => x.CollegioDocentiId,
                        principalTable: "CollegiDocenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classi_Corsi_CorsoId",
                        column: x => x.CorsoId,
                        principalTable: "Corsi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classi_Sezioni_SezioneId",
                        column: x => x.SezioneId,
                        principalTable: "Sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Docenti",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docenti", x => x.UtenteId);
                    table.ForeignKey(
                        name: "FK_Docenti_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DataInizio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Luogo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventi_Utenti_CreatoreId",
                        column: x => x.CreatoreId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messaggi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MittenteId = table.Column<int>(type: "int", nullable: false),
                    DestinatarioId = table.Column<int>(type: "int", nullable: false),
                    Testo = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DataInvio = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Letto = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Notifiche",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    Testo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Letta = table.Column<bool>(type: "bit", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifiche", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifiche_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profili",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    DataNascita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InfoAggiuntive = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataModifica = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profili", x => x.UtenteId);
                    table.ForeignKey(
                        name: "FK_Profili_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PercorsoFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TipoFile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CaricatoreId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: true),
                    ClasseId = table.Column<int>(type: "int", nullable: true),
                    DataCaricamento = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documenti_Classi_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documenti_Materie_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documenti_Utenti_CaricatoreId",
                        column: x => x.CaricatoreId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    ClasseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.UtenteId);
                    table.ForeignKey(
                        name: "FK_Studenti_Classi_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studenti_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocenteCollegio",
                columns: table => new
                {
                    CollegioDocentiId = table.Column<int>(type: "int", nullable: false),
                    DocenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocenteCollegio", x => new { x.CollegioDocentiId, x.DocenteId });
                    table.ForeignKey(
                        name: "FK_DocenteCollegio_CollegiDocenti_CollegioDocentiId",
                        column: x => x.CollegioDocentiId,
                        principalTable: "CollegiDocenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocenteCollegio_Docenti_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "Docenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterieClassi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    ClasseId = table.Column<int>(type: "int", nullable: true),
                    DocenteId = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoriaValutazioneId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterieClassi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterieClassi_CategoriaValutazione_CategoriaValutazioneId",
                        column: x => x.CategoriaValutazioneId,
                        principalTable: "CategoriaValutazione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterieClassi_Classi_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterieClassi_Docenti_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "Docenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterieClassi_Materie_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Presenze",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudenteId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Presente = table.Column<bool>(type: "bit", nullable: false),
                    Giustificata = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRegistrazione = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presenze", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presenze_Studenti_StudenteId",
                        column: x => x.StudenteId,
                        principalTable: "Studenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valutazioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudenteId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    DocenteId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    VotoNumerico = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CategoriaValutazioneId = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Commento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valutazioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Valutazioni_CategoriaValutazione_CategoriaValutazioneId",
                        column: x => x.CategoriaValutazioneId,
                        principalTable: "CategoriaValutazione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Valutazioni_Docenti_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "Docenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Valutazioni_Materie_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Valutazioni_Studenti_StudenteId",
                        column: x => x.StudenteId,
                        principalTable: "Studenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classi_AnnoFormativoId",
                table: "Classi",
                column: "AnnoFormativoId");

            migrationBuilder.CreateIndex(
                name: "IX_Classi_AnnoId",
                table: "Classi",
                column: "AnnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Classi_CollegioDocentiId",
                table: "Classi",
                column: "CollegioDocentiId");

            migrationBuilder.CreateIndex(
                name: "IX_Classi_CorsoId",
                table: "Classi",
                column: "CorsoId");

            migrationBuilder.CreateIndex(
                name: "IX_Classi_SezioneId",
                table: "Classi",
                column: "SezioneId");

            migrationBuilder.CreateIndex(
                name: "IX_DocenteCollegio_DocenteId",
                table: "DocenteCollegio",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Documenti_CaricatoreId",
                table: "Documenti",
                column: "CaricatoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Documenti_ClasseId",
                table: "Documenti",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Documenti_MateriaId",
                table: "Documenti",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventi_CreatoreId",
                table: "Eventi",
                column: "CreatoreId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterieClassi_CategoriaValutazioneId",
                table: "MaterieClassi",
                column: "CategoriaValutazioneId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterieClassi_ClasseId",
                table: "MaterieClassi",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterieClassi_DocenteId",
                table: "MaterieClassi",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterieClassi_MateriaId",
                table: "MaterieClassi",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Messaggi_DestinatarioId",
                table: "Messaggi",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Messaggi_MittenteId",
                table: "Messaggi",
                column: "MittenteId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuliMaterie_MateriaId",
                table: "ModuliMaterie",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifiche_UtenteId",
                table: "Notifiche",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Presenze_StudenteId",
                table: "Presenze",
                column: "StudenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Profili_CodiceFiscale",
                table: "Profili",
                column: "CodiceFiscale",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_ClasseId",
                table: "Studenti",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Email",
                table: "Utenti",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Username",
                table: "Utenti",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Valutazioni_CategoriaValutazioneId",
                table: "Valutazioni",
                column: "CategoriaValutazioneId");

            migrationBuilder.CreateIndex(
                name: "IX_Valutazioni_DocenteId",
                table: "Valutazioni",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Valutazioni_MateriaId",
                table: "Valutazioni",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Valutazioni_StudenteId",
                table: "Valutazioni",
                column: "StudenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocenteCollegio");

            migrationBuilder.DropTable(
                name: "Documenti");

            migrationBuilder.DropTable(
                name: "Eventi");

            migrationBuilder.DropTable(
                name: "MaterieClassi");

            migrationBuilder.DropTable(
                name: "Messaggi");

            migrationBuilder.DropTable(
                name: "ModuliMaterie");

            migrationBuilder.DropTable(
                name: "Notifiche");

            migrationBuilder.DropTable(
                name: "Presenze");

            migrationBuilder.DropTable(
                name: "Profili");

            migrationBuilder.DropTable(
                name: "Valutazioni");

            migrationBuilder.DropTable(
                name: "Moduli");

            migrationBuilder.DropTable(
                name: "CategoriaValutazione");

            migrationBuilder.DropTable(
                name: "Docenti");

            migrationBuilder.DropTable(
                name: "Materie");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "Classi");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "AnniFormativi");

            migrationBuilder.DropTable(
                name: "Anni");

            migrationBuilder.DropTable(
                name: "CollegiDocenti");

            migrationBuilder.DropTable(
                name: "Corsi");

            migrationBuilder.DropTable(
                name: "Sezioni");
        }
    }
}
