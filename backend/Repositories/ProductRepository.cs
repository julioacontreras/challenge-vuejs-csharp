using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
  public class ProductRepository
  {
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
      return await _context.Products.ToListAsync();
    }

    public Product? GetProduct(int id)
    {
      return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public Product CreateProduct(Product product)
    {
      _context.Products.Add(product);
      _context.SaveChanges();
      return product;
    }

    public Product? UpdateProduct(int id, Product newProduct)
    {
      Product? product = _context.Products.FirstOrDefault(p => p.Id == id);
      if (product != null)
      {
        product.Name = newProduct.Name;
        product.Settings = newProduct.Settings;
        _context.Products.Update(product);
        _context.SaveChanges();
        return product;
      }
      return null;
    }

    public void DeleteProduct(int id)
    {
      Product? product = _context.Products.FirstOrDefault(p => p.Id == id);
      if (product != null)
      {
        _context.Products.Remove(product);
        _context.SaveChanges();
      }
    }
  }


}
