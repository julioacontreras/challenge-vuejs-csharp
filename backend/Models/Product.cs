using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
  public class Product
  {
    public int? Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }

    [Column(TypeName = "text")]

    public required string Settings { get; set; }

  }

}