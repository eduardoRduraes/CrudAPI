using CrudAPI.DTOs;
using CrudAPI.Models;
using CrudAPI.Repositories;
using CrudAPI.Util;

namespace CrudAPI.Services;


public class CasoService
{
    private readonly CasoRepository _casoRepository;
    private readonly ILogger<CasoService> _logger;

    public CasoService(CasoRepository casoRepository, ILogger<CasoService> logger)
    {
        _casoRepository = casoRepository;
        _logger = logger;
    }

    public async Task Add(CasoDTO data)
    {
        if (data == null)
        {
            throw new AppException(400, "Os dados do caso não podem ser nulos.");
        }

        var caso = new Caso(Guid.NewGuid(), data.Titulo, data.Descricao, data.Valor, data.OngId);
        await _casoRepository.Add(caso);
    }

    public async Task<Caso> Get(Guid id)
    {
        var caso = await _casoRepository.Get(id) ?? throw new AppException(404, "Nenhum caso encontrado com este ID.");
        return caso;
    }

    public async Task Delete(Guid id)
    {
        var caso = await _casoRepository.Get(id) ?? throw new AppException(404, "Nenhum caso encontrado com este ID para exclusão.");
        await _casoRepository.Delete(caso);
    }
}

