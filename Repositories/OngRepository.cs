using CrudAPI.Data;
using CrudAPI.DTOs;
using CrudAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Repositories;

public class OngRepository
{
    private readonly AppDataContext _context;

    public OngRepository(AppDataContext context)
    {
        _context = context;
    }

    public async Task Add(OngDTO data)
    {
        var ong = new Ong(Guid.Empty, data.Nome, data.Email, data.WhatsApp, data.Cidade, data.Estado);
        await _context.Ongs.AddAsync(ong);
        await _context.SaveChangesAsync();
    }

    public async Task<Ong> Get(Guid ongId)
    {
        var ongs = await _context.Ongs
            .Include(o => o.Casos)
                .FirstOrDefaultAsync(o => o.Id == ongId);
        return ongs;
    }
}