using CrudAPI.Data;
using CrudAPI.DTOs;
using CrudAPI.Util;
using CrudAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Repositories;

public class OngRepository
{
    private readonly AppDataContext _context;
    private readonly PasswordService _passwordService;

    public OngRepository(AppDataContext context, PasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task Add(OngDTO data)
    {
        data.Password = _passwordService.HashPassword(data.Password);
        var ong = new Ong(Guid.Empty, data.Nome, data.Email, data.Password, data.WhatsApp, data.Cidade, data.Estado);
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

    public async Task<Ong> GetEmail(String email)
    {
        var ong = await _context.Ongs.FirstOrDefaultAsync(o => o.Email == email);
        return ong;
    }
}