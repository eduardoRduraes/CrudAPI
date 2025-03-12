using CrudAPI.Data;
using CrudAPI.Models;

namespace CrudAPI.Repositories;

public class CasoRepository
{
    private readonly AppDataContext _context;

    public CasoRepository(AppDataContext context)
    {
        _context = context;
    }

    public async Task Add(Caso data)
    {
        
        await _context.Casos.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public async Task<Caso> Get(Guid id)
    { 
       var caso = await _context.Casos.FindAsync(id);
       return caso;
    }

    public async Task Delete(Caso data)
    {
        _context.Casos.Remove(data);
        await _context.SaveChangesAsync();
    }
}