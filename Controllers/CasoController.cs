using CrudAPI.DTOs;
using CrudAPI.Util;
using CrudAPI.Repositories;
using CrudAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CasoController : ControllerBase
{
    private readonly CasoService _casoService;

    public CasoController(CasoService casoService)
    {
        _casoService = casoService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CasoDTO data)
    {
        await _casoService.Add(data);
        return Ok(new { message = $"Caso adicionado com sucesso! {data.Titulo}" });
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var caso = await _casoService.Get(id);
        return Ok(caso);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _casoService.Delete(id);
        return Ok(new { message = $"Caso deletado com sucesso! {id}" });
    }
}