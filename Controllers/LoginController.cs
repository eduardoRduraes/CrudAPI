using CrudAPI.DTOs;
using CrudAPI.Etc;
using CrudAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers;
[ApiController]
[Route("/api/auth")]
public class LoginController :ControllerBase
{
    private readonly OngRepository _ongRepository;
    private readonly AuthService _authService;
    
    public LoginController(OngRepository ongRepository, AuthService authService)
    {
        _ongRepository = ongRepository;
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginOngDTO request)
    {
        var ong = await _ongRepository.GetIdEmail(request.Email, request.Password);
        
        if(ong == null)
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