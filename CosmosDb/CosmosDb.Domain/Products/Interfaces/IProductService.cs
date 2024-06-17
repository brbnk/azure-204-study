namespace CosmosDb.Domain.Products.Interfaces;

public interface IProductService
{
  public Task<Product> AddAsync(Product product);
}