namespace CosmosDb.Domain.Products.Requests;

public sealed record CreateProductRequest(
  string Category,
  string Name,
  int Quantity,
  decimal Price,
  bool Clearance
);