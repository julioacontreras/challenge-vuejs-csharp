using Backend.Models;
using Newtonsoft.Json.Linq;

public class ProductViewModel
{
  private void CalculateTotals(JObject settings)
  {
    TotalFabrics = 0;
    TotalColors = 0;
    TotalImages = 0;
    var fabrics = settings["fabrics"];
    if (fabrics == null)
    {
      return;
    }
    TotalFabrics = fabrics?.Count() ?? 0;
    foreach (var fabric in fabrics)
    {
      var colors = fabric["colors"];
      if (colors == null)
      {
        continue;
      }
      var c = fabric["colors"]?.Count() ?? 0;
      TotalColors += c;
      foreach (var color in colors)
      {
        var images = color["images"];
        if (images == null)
        {
          continue;
        }
        TotalImages += images?.Count() ?? 0;
      }
    }
  }
  public ProductViewModel(Product product)
  {
    Id = product.Id;
    Name = product.Name;
    JObject settings = JObject.Parse(product.Settings);
    CalculateTotals(settings);
  }
  public int? Id { get; set; }
  public string? Name { get; set; }
  public decimal TotalColors { get; set; }
  public decimal TotalFabrics { get; set; }
  public decimal TotalImages { get; set; }
}