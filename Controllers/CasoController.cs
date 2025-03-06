using CrudAPI.DTOs;
using CrudAPI.Util;
using CrudAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CasoController : ControllerBase
{
    private readonly CasoRepository _repository;
    private readonly TokenService _tokenService;

    public CasoController(CasoRepository repository, TokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CasoDTO data)
    {
        await _repository.Add(data);
        return Ok(new { message = $"Caso adicionado com sucesso! {data.Titulo}" });
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var caso = await _repository.Get(id);
        return Ok(caso);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var data = await _repository.Get(id);
        if (data != null)
            await _repository.Delete(data);
        return Ok(new { message = $"Caso deletado com sucesso! {id}" });
    }
}