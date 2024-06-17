using CosmosDb.Domain.Requests;
using CosmosDb.Domain.Shared;
using Newtonsoft.Json;

namespace CosmosDb.Domain.Products;

public record Product : DocumentBase
{
  public Product() { }

  public Product(CreateProductRequest request)
  {
    Category = request.Category;
    Name = request.Name;
    Quantity = request.Quantity;
    Price = request.Price;
    Clearance = request.Clearance;
  }

  [JsonProperty("category")]
  public string Category { get; set; }

  [JsonProperty("name")]
  public string Name { get; set; }

  [JsonProperty("quantity")]
  public int Quantity { get; set; }

  [JsonProperty("price")]
  public decimal Price { get; set; }

  [JsonProperty("clearence")]
  public bool Clearance { get; set; }
}