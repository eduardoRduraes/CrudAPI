using CrudAPI.DTOs;
using CrudAPI.Repositories;
using CrudAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers;
[ApiController]
[Route("/api/auth")]
public class LoginController :ControllerBase
{
    private readonly OngRepository _ongRepository;
    private readonly AuthService _authService;
    private readonly PasswordService _passwordService;
    
    public LoginController(OngRepository ongRepository, AuthService authService, PasswordService passwordService)
    {
        _ongRepository = ongRepository;
        _authService = authService;
        _passwordService = passwordService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginOngDTO request)
    {
        var ong = await _ongRepository.GetEmail(request.Email);
        
        if(ong == null)
            return Unauthorized(new { message = "Email or password is incorrect" });
        
        var passwordVerify = _passwordService.VerifyPassword(request.Password, ong.Password);
        
        if (!passwordVerify)
            return Unauthorized(new { message = "Email or password is incorrect" });

        var token = _authService.GenerateToken(ong.Id);
        
        Response.Cookies.Append("token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(5)
        });
        
        return Ok(token);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");
        return Ok(new { message = "Logout successful" });
    }
}