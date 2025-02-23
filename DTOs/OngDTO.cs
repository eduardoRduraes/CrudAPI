using System.Text.Json.Serialization;

namespace CrudAPI.DTOs;

public class OngDTO
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string WhatsApp { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
}