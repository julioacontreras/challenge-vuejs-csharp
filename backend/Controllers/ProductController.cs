using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Repositories;
using Backend.Models;

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
        return Ok(products);
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

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, Product product)
    {
        Product? response = _productRepository.UpdateProduct(id, product);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _productRepository.DeleteProduct(id);
        return Ok();
    }
}
