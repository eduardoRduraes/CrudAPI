using CrudAPI.DTOs;
using CrudAPI.Models;
using CrudAPI.Repositories;
using CrudAPI.Util;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Services;

public class OngService
{
    private readonly OngRepository _ongRepository;
    private readonly PasswordHash _passwordHash;

    public OngService(OngRepository ongRepository, PasswordHash  passwordHash)
    {
        _ongRepository = ongRepository;
        _passwordHash = passwordHash;

    }

    public async Task Add(OngDTO data)
    {
        try
        {
            data.Password = _passwordHash.HashPassword(data.Password);
            var ong = new Ong(Guid.Empty, data.Nome, data.Email, data.Password, data.WhatsApp, data.Cidade, data.Estado);
            await _ongRepository.Add(ong);
            
        }
        catch (DbUpdateException dbEx) // Erro relacionado ao banco de dados
        {
            throw new AppException(400, "Erro ao salvar ONG no banco de dados.");
        }
        catch (ArgumentException argEx) // Erros de argumentos inválidos
        {
            throw new AppException(422, "Os dados fornecidos são inválidos: " + argEx.Message);
        }
        catch (Exception ex) // Qualquer outro erro inesperado
        {
            throw new AppException(500, "Ocorreu um erro inesperado ao tentar registrar a ONG.");
        }
    }

    public async Task<Ong> Get(Guid id)
    {
        try
        {
            var ong = await _ongRepository.Get(id);

            if (ong == null)
            {
                throw new AppException(404, "Nenhuma ONG encontrada com este ID.");
            }

            return ong;
        }
        catch (Exception ex)
        {
            throw new AppException(500, "Erro ao buscar ONG pelo ID.");
        }
    }


    public async Task<Ong> GetEmail(string email)
    {
        try
        {
            var ong = await _ongRepository.GetEmail(email);

            if (ong == null)
            {
                throw new AppException(404, "Nenhuma ONG encontrada com este e-mail.");
            }

            return ong;
        }
        catch (Exception ex)
        {
            throw new AppException(500, "Erro ao buscar ONG pelo e-mail.");
        }
    }

}