using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Repositories;
using Backend.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

[ApiController]
[Route("api/product")]

public class ProductController : ControllerBase
{
    private readonly ProductRepository _productRepository;

    public ProductController(AppDbContext context)
    {
        _productRepository = new ProductRepository(context);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        IEnumerable<Product> products = await _productRepository.GetProducts();
        if (products == null)
        {
            return NotFound();
        }
        List<ProductViewModel> list = new List<ProductViewModel>();
        foreach (Product product in products)
        {
            ProductViewModel pvm = new ProductViewModel(product);
            list.Add(pvm);
        }
        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        Product? product = _productRepository.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct(Product product)
    {
        Product? response = _productRepository.CreateProduct(product);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpDelete("removeImage/{id}/{fabric}/{color}/{imageIdx}")]
    public IActionResult RemoveImageProduct(int id, string fabric, string color, int imageIdx)
    {
        Product? product = _productRepository.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }

        JObject settings = JObject.Parse(product.Settings);
        var fabrics = settings["fabrics"];
        if (fabrics == null)
        {
            return NotFound();
        }
        foreach (var fabricItem in fabrics)
        {
            if (fabricItem["name"]?.ToString() != fabric)
            {
                continue;
            }
            var colors = fabricItem["colors"];
            if (colors == null)
            {
                continue;
            }
            foreach (var colorItem in colors)
            {
                if (colorItem["name"]?.ToString() != color)
                {
                    continue;
                }
                var images = colorItem["images"] as JArray;
                if (images != null)
                {
                    images.RemoveAt(imageIdx);
                }
            }
        }
        var json = JsonConvert.SerializeObject(settings);
        product.Settings = json;
        _productRepository.UpdateProduct(id, product);
        return Ok(product);
    }

    [HttpPut("addImage/{id}/{fabric}/{color}")]
    public IActionResult AddImageProduct(int id, string fabric, string color, Image image)
    {
        Product? product = _productRepository.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }

        JObject settings = JObject.Parse(product.Settings);
        var fabrics = settings["fabrics"];
        if (fabrics == null)
        {
            return NotFound();
        }
        foreach (var fabricItem in fabrics)
        {
            if (fabricItem["name"]?.ToString() != fabric)
            {
                continue;
            }
            var colors = fabricItem["colors"];
            if (colors == null)
            {
                continue;
            }
            foreach (var colorItem in colors)
            {
                if (colorItem["name"]?.ToString() != color)
                {
                    continue;
                }
                var images = colorItem["images"] as JArray;
                if (images == null)
                {
                    images = new JArray();
                    colorItem["images"] = images;
                }
                images.Add(image.source);
            }
        }
        var json = JsonConvert.SerializeObject(settings);
        product.Settings = json;
        _productRepository.UpdateProduct(id, product);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _productRepository.DeleteProduct(id);
        return Ok();
    }
}
