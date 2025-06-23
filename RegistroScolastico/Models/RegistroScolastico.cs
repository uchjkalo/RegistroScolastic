using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroScolastico.Models
{
    // --- Enumerazioni ---
    public enum RuoloUtente
    {
        Amministratore,
        Docente,
        Studente
    }

    public enum TipoValutazione
    {
        Categoria, // Usa CategoriaValutazione
        Numero     // Usa un voto numerico
    }

    // --- Classi Base e di Profilo ---
    public class Utente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Lo username è obbligatorio.")]
        [MaxLength(50, ErrorMessage = "Lo username non può superare i 50 caratteri.")]
        public required string Username { get; set; } // Added 'required' modifier

        [Required(ErrorMessage = "L'email è obbligatoria.")]
        [EmailAddress(ErrorMessage = "Formato email non valido.")]
        [MaxLength(100, ErrorMessage = "L'email non può superare i 100 caratteri.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La password hash è obbligatoria.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Il ruolo è obbligatorio.")]
        public RuoloUtente Ruolo { get; set; }

        public Profilo Profilo { get; set; }

        public DateTime DataCreazione { get; set; } = DateTime.UtcNow;
        public DateTime? DataUltimoAccesso { get; set; }
        public bool Attivo { get; set; } = true;
    }

    public class Profilo
    {
        // La chiave primaria sarà anche la chiave esterna verso Utente
        [Key]
        [ForeignKey("Utente")]
        public int UtenteId { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [MaxLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        [MaxLength(50, ErrorMessage = "Il cognome non può superare i 50 caratteri.")]
        public string Cognome { get; set; }

        [MaxLength(16, ErrorMessage = "Il codice fiscale non può superare i 16 caratteri.")]
        public string CodiceFiscale { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascita { get; set; }

        [MaxLength(200, ErrorMessage = "L'indirizzo non può superare i 200 caratteri.")]
        public string Indirizzo { get; set; }

        [Phone(ErrorMessage = "Numero di telefono non valido.")]
        [MaxLength(20, ErrorMessage = "Il numero di telefono non può superare i 20 caratteri.")]
        public string Telefono { get; set; }

        [MaxLength(500, ErrorMessage = "Le informazioni aggiuntive non possono superare i 500 caratteri.")]
        public string? InfoAggiuntive { get; set; }

        public DateTime DataCreazione { get; set; } = DateTime.UtcNow;
        public DateTime? DataModifica { get; set; }

        // Navigation property per la relazione 1-a-1 con Utente
        public Utente Utente { get; set; }
    }

    // --- Classi Strutturali della Scuola ---
    public class AnnoFormativo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome dell'anno formativo è obbligatorio.")]
        [MaxLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInizio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFine { get; set; }

        public ICollection<Classe> Classi { get; set; }
    }

    public class Corso
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome del corso è obbligatorio.")]
        [MaxLength(100, ErrorMessage = "Il nome non può superare i 100 caratteri.")]
        public string Nome { get; set; }

        public ICollection<Classe> Classi { get; set; }
    }

    public class Classe
    {
        public int Id { get; set; }

        // Foreign key per Anno
        public int AnnoId { get; set; }
        public Anno Anno { get; set; }

        // Foreign key per Sezione
        public int SezioneId { get; set; }
        public Sezione Sezione { get; set; }

        [Required(ErrorMessage = "La specializzazione è obbligatoria.")]
        // Foreign key per AnnoFormativo
        public int AnnoFormativoId { get; set; }
        public AnnoFormativo AnnoFormativo { get; set; }

        // Foreign key per Corso
        public int CorsoId { get; set; }
        public Corso Corso { get; set; }

        // Navigation properties
        public int? CollegioDocentiId { get; set; }
        public CollegioDocenti CollegioDocenti { get; set; }
        public ICollection<Studente> Studenti { get; set; }
        public ICollection<MateriaClasse> MaterieClassi { get; set; }
        public DateTime DataCreazione { get; set; } = DateTime.UtcNow;
    }


    public class CollegioDocenti
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome del collegio docenti è obbligatorio.")]
        [MaxLength(100, ErrorMessage = "Il nome non può superare i 100 caratteri.")]
        public string Nome { get; set; }

        public ICollection<Docente> Docenti { get; set; }
        public ICollection<Classe> Classi { get; set; }

        public DateTime DataCreazione { get; set; } = DateTime.UtcNow;
    }

    // --- Ruoli Specifici (derivano concettualmente da Utente/Profilo) ---
    public class Docente
    {
        [Key]
        [ForeignKey("Utente")]
        public int UtenteId { get; set; }

        [Required]
        public Utente Utente { get; set; }

        // Navigation properties
        public ICollection<CollegioDocenti> CollegiDocenti { get; set; }
        public ICollection<MateriaClasse> MaterieInsegnate { get; set; }

        [NotMapped]
        public IEnumerable<Materia>? MaterieInsegnateDirette =>
                MaterieInsegnate?.Select(mc => mc.Materia).Distinct();
    }

    public class Studente
    {
        [Key]
        [ForeignKey("Utente")]
        public int UtenteId { get; set; }

        [Required]
        public Utente Utente { get; set; }

        // Foreign key per Classe
        public int ClasseId { get; set; }
        public Classe Classe { get; set; }

        // Navigation properties
        public ICollection<Valutazione> Valutazioni { get; set; }
        public ICollection<Presenza> Presenze { get; set; }
    }

    // --- Materie e Moduli ---
    public class Materia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della materia è obbligatorio.")]
        [MaxLength(100, ErrorMessage = "Il nome non può superare i 100 caratteri.")]
        public string Nome { get; set; }

        public DateTime? DataCreazione { get; set; } = DateTime.UtcNow;
        public DateTime? DataModifica { get; set; }

        public ICollection<MateriaClasse> Classi { get; set; }
        public ICollection<ModuloMateria> Moduli { get; set; } // Relazione con Moduli

        [NotMapped]
        public IEnumerable<Docente>? DocentiDiretti =>
            Classi?.Select(mc => mc.Docente).Distinct();
    }

    public class Modulo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome del modulo è obbligatorio.")]
        [MaxLength(100, ErrorMessage = "Il nome non può superare i 100 caratteri.")]
        public string Nome { get; set; }

        public DateTime? DataCreazione { get; set; } = DateTime.UtcNow;
        public DateTime? DataModifica { get; set; }

        public ICollection<ModuloMateria> Materie { get; set; } // Relazione con Materie
    }

    public class ModuloMateria
    {
        // Chiave composita per la tabella di join
        public int ModuloId { get; set; }
        public Modulo Modulo { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; }
    }


    // --- Tabelle di Join con payload ---
    public class MateriaClasse
    {
        public int Id { get; set; }
        public int MateriaId { get; set; }
        public Materia Materia { get; set; }
        public int? ClasseId { get; set; }
        public Classe Classe { get; set; }
        public int DocenteId { get; set; }
        public Docente Docente { get; set; }
        public decimal Peso { get; set; }

        // Adding the missing property to fix the error  
        public int? CategoriaValutazioneId { get; set; }
        public CategoriaValutazione? CategoriaValutazione { get; set; }
    }

    // --- Tabelle Operative (Presenze, Valutazioni, Comunicazioni, Eventi, Documenti) ---
    public class Presenza
    {
        public int Id { get; set; }

        public int StudenteId { get; set; }
        public Studente Studente { get; set; }

        [Required(ErrorMessage = "La data della presenza è obbligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public bool Presente { get; set; } = true; // True per presente, False per assente
        public bool Giustificata { get; set; } = false; // Solo se assente
        public string Note { get; set; }

        public DateTime DataRegistrazione { get; set; } = DateTime.UtcNow;
    }

    public class Valutazione
    {
        public int Id { get; set; }

        public int StudenteId { get; set; }
        public Studente Studente { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; }

        public int DocenteId { get; set; }
        public Docente Docente { get; set; }

        [Required(ErrorMessage = "Il tipo di valutazione è obbligatorio.")]
        public TipoValutazione Tipo { get; set; }

        [Range(0, 10, ErrorMessage = "Il voto deve essere tra 0 e 10.")]
        public decimal? VotoNumerico { get; set; } // Usato se TipoValutazione è Numero

        public int? CategoriaValutazioneId { get; set; }
        public CategoriaValutazione CategoriaValutazione { get; set; } // Usato se TipoValutazione è Categoria

        [Required(ErrorMessage = "La data della valutazione è obbligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; } = DateTime.UtcNow;

        [MaxLength(1000, ErrorMessage = "Il commento non può superare i 1000 caratteri.")]
        public string Commento { get; set; }
    }

    public class CategoriaValutazione
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della categoria è obbligatorio.")]
        [MaxLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Nome { get; set; }

        [Range(1, 10, ErrorMessage = "Il valore della categoria deve essere tra 1 e 10 (es. Ottimo: 10, Sufficiente: 6).")]
        public int Valore { get; set; }

        [MaxLength(20, ErrorMessage = "Il colore non può superare i 20 caratteri (es. '#FF0000').")]
        public string Colore { get; set; }

        public DateTime? DataCreazione { get; set; } = DateTime.UtcNow;
        public DateTime? DataModifica { get; set; }
    }

    public class Notifica
    {
        public int Id { get; set; }

        public int UtenteId { get; set; }
        public Utente Utente { get; set; }

        [Required(ErrorMessage = "Il testo della notifica è obbligatorio.")]
        [MaxLength(500, ErrorMessage = "La notifica non può superare i 500 caratteri.")]
        public string Testo { get; set; }

        public DateTime Data { get; set; } = DateTime.UtcNow;
        public bool Letta { get; set; } = false;
        public string Link { get; set; }
    }

    public class Evento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il titolo dell'evento è obbligatorio.")]
        [MaxLength(100, ErrorMessage = "Il titolo non può superare i 100 caratteri.")]
        public string Titolo { get; set; }

        [MaxLength(1000, ErrorMessage = "La descrizione non può superare i 1000 caratteri.")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "La data di inizio è obbligatoria.")]
        public DateTime DataInizio { get; set; }

        public DateTime DataFine { get; set; }

        [MaxLength(100, ErrorMessage = "Il luogo non può superare i 100 caratteri.")]
        public string Luogo { get; set; }

        public int CreatoreId { get; set; }
        public Utente Creatore { get; set; }
    }

    public class Documento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome del documento è obbligatorio.")]
        [MaxLength(200, ErrorMessage = "Il nome non può superare i 200 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il percorso del file è obbligatorio.")]
        [MaxLength(500, ErrorMessage = "Il percorso non può superare i 500 caratteri.")]
        public string PercorsoFile { get; set; }

        [MaxLength(50, ErrorMessage = "Il tipo di file non può superare i 50 caratteri (es. 'pdf', 'docx').")]
        public string TipoFile { get; set; }

        public int CaricatoreId { get; set; }
        public Utente Caricatore { get; set; }

        public int? MateriaId { get; set; }
        public Materia Materia { get; set; }

        public int? ClasseId { get; set; }
        public Classe Classe { get; set; }

        public DateTime DataCaricamento { get; set; } = DateTime.UtcNow;
    }

    public class Sezione
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della sezione è obbligatorio.")]
        [MaxLength(1, ErrorMessage = "La sezione non può superare 1 carattere (es. 'A').")]
        public string Nome { get; set; }
    }


    public class Anno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome dell'anno è obbligatorio.")]
        [MaxLength(5, ErrorMessage = "L'anno non può superare i 5 caratteri (es. '1°').")]
        public string Nome { get; set; }
    }

}