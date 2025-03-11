namespace Backend.Models
{
  public class Product
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Colors { get; set; }
    public Fabric[]? Fabrics { get; set; }
    public Image[]? Images { get; set; }
  }
}