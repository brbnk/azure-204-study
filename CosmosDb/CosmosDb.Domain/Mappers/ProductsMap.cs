using CosmosDb.Domain.Products;
using CosmosDb.Domain.Products.Requests;

namespace CosmosDb.Domain.Mappers;

public static class ProductsMap
{
  public static Product MapToProduct(this CreateProductRequest request)
  {
    return new()
    {
      Category = request.Category,
      Name = request.Name,
      Quantity = request.Quantity,
      Price = request.Price,
      Clearance = request.Clearance
    };
  }
}