using CrudAPI.DTOs;
using CrudAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class OngController : ControllerBase
{
    
    private readonly OngRepository _ongRepository;

    public OngController(OngRepository ongRepository)
    {
        _ongRepository = ongRepository;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody]OngDTO data)
    {
        
        await _ongRepository.Add(data);
        return Ok(new { message = $"Ong cadastrado com sucesso! {data.Nome}" });
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var ong = await _ongRepository.Get(id);
        return Ok(ong);
    }
}

