using System.Text.Json.Serialization;

namespace CrudAPI.Models;

public class Ong
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string WhatsApp { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
   

    public ICollection<Caso> Casos { get; set; } = new List<Caso>();
    
    
        
    public Ong()
    {
        
    }
    
    public Ong(Guid id, string nome, string email, string whatsApp, string cidade, string estado)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Nome = nome;
        Email = email;
        WhatsApp = whatsApp;
        Cidade = cidade;
        Estado = estado;
    }
}