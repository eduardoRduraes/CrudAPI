using System.Text.Json.Serialization;

namespace CrudAPI.DTOs;

public class CasoDTO
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public Guid OngId { get; set; }


}