using CrudAPI.DTOs;
using CrudAPI.Services;
using CrudAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers;
[ApiController]
[Route("/api/auth")]
public class LoginController :ControllerBase
{
    private readonly LoginService _loginService;
    private readonly Auth _auth;
    
    public LoginController(LoginService loginService, Auth auth)
    {
        _loginService = loginService;
        _auth = auth;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginOngDTO request)
    {
        var ong = await _loginService.Login(request);
        
       var token = _auth.GenerateToken(ong.Id);
        
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
        // Remove o cookie de autenticação
        Response.Cookies.Delete("token", new CookieOptions
        {
            HttpOnly = true,  // Impede acesso via JavaScript
            Secure = true,    // Garante que só seja enviado via HTTPS
            SameSite = SameSiteMode.Strict, // Evita envio do cookie para domínios externos
        });

        // Retorna uma resposta indicando que o logout foi bem-sucedido
        return Ok(new { message = "Logout successful" });
    }

}