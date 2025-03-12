using CrudAPI.Data;
using CrudAPI.DTOs;
using CrudAPI.Util;
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

    public async Task Add(Ong data)
    {
        await _context.Ongs.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public async Task<Ong> Get(Guid ongId)
    {
        var ongs = await _context.Ongs
            .Include(o => o.Casos)
                .FirstOrDefaultAsync(o => o.Id == ongId);
        return ongs;
    }

    public async Task<Ong> GetEmail(string email)
    {
        var ong = await _context.Ongs.FirstOrDefaultAsync(o => o.Email == email);
        return ong;
    }
}