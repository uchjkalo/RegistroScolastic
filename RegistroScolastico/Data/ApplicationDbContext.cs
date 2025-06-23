using Microsoft.EntityFrameworkCore;
using RegistroScolastico.Models;

namespace RegistroScolastico.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Profilo> Profili { get; set; }
        public DbSet<AnnoFormativo> AnniFormativi { get; set; }
        public DbSet<Corso> Corsi { get; set; }
        public DbSet<Classe> Classi { get; set; }
        public DbSet<CollegioDocenti> CollegiDocenti { get; set; }
        public DbSet<Docente> Docenti { get; set; }
        public DbSet<Studente> Studenti { get; set; }
        public DbSet<Materia> Materie { get; set; }
        public DbSet<Modulo> Moduli { get; set; }
        public DbSet<MateriaClasse> MaterieClassi { get; set; }
        public DbSet<ModuloMateria> ModuliMaterie { get; set; }
        public DbSet<Presenza> Presenze { get; set; }
        public DbSet<Valutazione> Valutazioni { get; set; }
        public DbSet<Notifica> Notifiche { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Documento> Documenti { get; set; }
        public DbSet<Sezione> Sezioni { get; set; }
        public DbSet<Anno> Anni { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurazione della chiave primaria composta per ModuloMateria
            modelBuilder.Entity<ModuloMateria>()
                .HasKey(mm => new { mm.ModuloId, mm.MateriaId });

            modelBuilder.Entity<ModuloMateria>()
                .HasOne(mm => mm.Modulo)
                .WithMany(m => m.Materie)
                .HasForeignKey(mm => mm.ModuloId);

            modelBuilder.Entity<ModuloMateria>()
                .HasOne(mm => mm.Materia)
                .WithMany(m => m.Moduli)
                .HasForeignKey(mm => mm.MateriaId);

            // Configurazioni Relazionali
            modelBuilder.Entity<Utente>()
                .HasOne(u => u.Profilo)
                .WithOne(p => p.Utente)
                .HasForeignKey<Profilo>(p => p.UtenteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Classe>()
                .HasOne(c => c.AnnoFormativo)
                .WithMany(af => af.Classi)
                .HasForeignKey(c => c.AnnoFormativoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classe>()
                .HasOne(c => c.Corso)
                .WithMany(co => co.Classi)
                .HasForeignKey(c => c.CorsoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classe>()
                .HasOne(c => c.CollegioDocenti)
                .WithMany(cd => cd.Classi)
                .HasForeignKey(c => c.CollegioDocentiId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classe>()
                .HasIndex(c => new { c.AnnoId, c.SezioneId, c.AnnoFormativoId, c.CorsoId })
                .IsUnique();

            modelBuilder.Entity<Docente>()
                .HasMany(d => d.CollegiDocenti)
                .WithMany(cd => cd.Docenti)
                .UsingEntity<Dictionary<string, object>>(
                    "DocenteCollegio",
                    j => j.HasOne<CollegioDocenti>().WithMany().HasForeignKey("CollegioDocentiId"),
                    j => j.HasOne<Docente>().WithMany().HasForeignKey("DocenteId"));

            modelBuilder.Entity<Studente>()
                .HasOne(s => s.Classe)
                .WithMany(c => c.Studenti)
                .HasForeignKey(s => s.ClasseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tabelle di Join con payload
            modelBuilder.Entity<MateriaClasse>()
                .HasOne(mc => mc.Materia)
                .WithMany(m => m.Classi)
                .HasForeignKey(mc => mc.MateriaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MateriaClasse>()
                .HasOne(mc => mc.Classe)
                .WithMany(c => c.MaterieClassi)
                .HasForeignKey(mc => mc.ClasseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MateriaClasse>()
                .HasOne(mc => mc.Docente)
                .WithMany(d => d.MaterieInsegnate)
                .HasForeignKey(mc => mc.DocenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MateriaClasse>()
                .HasOne(mc => mc.CategoriaValutazione)
                .WithMany()
                .HasForeignKey(mc => mc.CategoriaValutazioneId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MateriaClasse>()
                .Property(mc => mc.Peso)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Valutazione>()
                .Property(v => v.VotoNumerico)
                .HasColumnType("decimal(18,2)");

            // Tabelle Operative
            modelBuilder.Entity<Presenza>()
                .HasOne(p => p.Studente)
                .WithMany(s => s.Presenze)
                .HasForeignKey(p => p.StudenteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Valutazione>()
                .HasOne(v => v.Studente)
                .WithMany(s => s.Valutazioni)
                .HasForeignKey(v => v.StudenteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Valutazione>()
                .HasOne(v => v.Materia)
                .WithMany()
                .HasForeignKey(v => v.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Valutazione>()
                .HasOne(v => v.Docente)
                .WithMany()
                .HasForeignKey(v => v.DocenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Valutazione>()
                .HasOne(v => v.CategoriaValutazione)
                .WithMany()
                .HasForeignKey(v => v.CategoriaValutazioneId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Tabelle Operative

            modelBuilder.Entity<Notifica>()
                .HasOne(n => n.Utente)
                .WithMany()
                .HasForeignKey(n => n.UtenteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Creatore)
                .WithMany()
                .HasForeignKey(e => e.CreatoreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Documento>()
                .HasOne(d => d.Caricatore)
                .WithMany()
                .HasForeignKey(d => d.CaricatoreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Documento>()
                .HasOne(d => d.Materia)
                .WithMany()
                .HasForeignKey(d => d.MateriaId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Documento>()
                .HasOne(d => d.Classe)
                .WithMany()
                .HasForeignKey(d => d.ClasseId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Indici Unici
            modelBuilder.Entity<Utente>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Utente>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Profilo>()
                .HasIndex(p => p.CodiceFiscale)
                .IsUnique();

            // Valori di Default
            modelBuilder.Entity<Utente>()
                .Property(u => u.DataCreazione)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<AnnoFormativo>()
                .Property(af => af.DataInizio)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Materia>()
                .Property(m => m.DataCreazione)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Modulo>()
                .Property(mo => mo.DataCreazione)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Presenza>()
                .Property(p => p.DataRegistrazione)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Valutazione>()
                .Property(v => v.Data)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Notifica>()
                .Property(n => n.Data)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Documento>()
                .Property(d => d.DataCaricamento)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Documento>()
                .Property(d => d.MateriaId)
                .IsRequired(false);

            modelBuilder.Entity<Documento>()
                .Property(d => d.ClasseId)
                .IsRequired(false);
        }
    }
}
