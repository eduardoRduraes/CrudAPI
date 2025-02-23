using CrudAPI.DTOs;
using CrudAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CasoController : ControllerBase
{
    private readonly CasoRepository _repository;

    public CasoController(CasoRepository repository)
    {
        _repository = repository;
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
        await _repository.Get(id);
        return Ok(new { message = $"Caso deletado com sucesso! {id}" });
    }
}