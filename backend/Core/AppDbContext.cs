using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Backend.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
  }
}