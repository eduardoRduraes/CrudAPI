using System.Text.Json.Serialization;

namespace CrudAPI.Models;

public class Caso
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    
    [JsonIgnore]
    public Ong Ong { get; set; }
    
    public Guid OngId { get; set; }

    public Caso()
    {
        
    }

    public Caso(Guid id, string titulo, string descricao, decimal valor, Guid ongId)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Titulo = titulo;
        Descricao = descricao;
        Valor = valor;
        OngId = ongId;
    }
}