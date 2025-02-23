using CrudAPI.Data;
using CrudAPI.DTOs;
using CrudAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Repositories;

public class CasoRepository
{
    private readonly AppDataContext _context;

    public CasoRepository(AppDataContext context)
    {
        _context = context;
    }

    public async Task Add(CasoDTO data)
    {
        var caso = new Caso(Guid.Empty, data.Titulo, data.Descricao, data.Valor, data.OngId);
        await _context.Casos.AddAsync(caso);
        await _context.SaveChangesAsync();
    }

    public async Task<Caso> Get(Guid id)
    {
        return await _context.Casos.FindAsync(id);
    }

    public async Task Delete(Caso data)
    {
        _context.Casos.Remove(data);
        await _context.SaveChangesAsync();
    }
}