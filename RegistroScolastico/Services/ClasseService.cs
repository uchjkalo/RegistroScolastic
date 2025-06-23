using Microsoft.EntityFrameworkCore;
using RegistroScolastico.Interfacce;
using RegistroScolastico.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RegistroScolastico.Data;

public class ClasseService : IClasseService
{
    private readonly ApplicationDbContext _context;

    public ClasseService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Classe>> GetClassiAsync()
    {
        return await _context.Classi.ToListAsync();
    }

    public async Task<Classe> GetClasseByIdAsync(int id)
    {
        var classe = await _context.Classi.FindAsync(id);
        if (classe == null)
        {
            throw new InvalidOperationException($"Classe with ID {id} not found.");
        }
        return classe;
    }

    public async Task AddClasseAsync(Classe classe)
    {
        _context.Classi.Add(classe);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateClasseAsync(Classe classe)
    {
        _context.Entry(classe).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClasseAsync(int id)
    {
        var classe = await _context.Classi.FindAsync(id);
        if (classe != null)
        {
            _context.Classi.Remove(classe);
            await _context.SaveChangesAsync();
        }
    }
}
