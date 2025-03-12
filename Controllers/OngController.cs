using CrudAPI.DTOs;
using CrudAPI.Repositories;
using CrudAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class OngController : ControllerBase
{
    
    private readonly OngService _ongService;

    public OngController(OngService ongService)
    {
        _ongService = ongService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody]OngDTO data)
    {
        await _ongService.Add(data);
        return Ok(new { message = $"Ong cadastrado com sucesso! {data.Nome}" });
    }

    [Authorize]
    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var ong = await _ongService.Get(id);
        return Ok(ong);
    }
}

