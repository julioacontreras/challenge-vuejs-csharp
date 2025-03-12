using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Models
{
  public class Product
  {
    public int? Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }

    [Column(TypeName = "jsonb")]

    public Dictionary<string, object> Settings { get; set; } = new();

  }
}