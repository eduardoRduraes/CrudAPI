using CrudAPI.DTOs;
using CrudAPI.Models;
using CrudAPI.Repositories;
using CrudAPI.Util;

namespace CrudAPI.Services;

public class LoginService
{
    private readonly OngRepository _ongRepository;
    private readonly PasswordHash _passwordHash;
    
    public LoginService(OngRepository ongRepository, PasswordHash passwordHash)
    {
        _ongRepository = ongRepository;
        _passwordHash = passwordHash;
    }

    public async Task<Ong> Login(LoginOngDTO request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new AppException(400, "Email and password are required.");
        }

        var ong = await _ongRepository.GetEmail(request.Email) 
                  ?? throw new AppException(401, "Email or password is incorrect.");

        var passwordVerify = _passwordHash.VerifyPassword(request.Password, ong.Password);

        if (!passwordVerify)
        {
            throw new AppException(401, "Email or password is incorrect.");
        }

        return ong;
    }
    
}