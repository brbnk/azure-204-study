namespace CosmosDb.Domain.Requests;

public sealed record CreateProductRequest(
  string Category,
  string Name,
  int Quantity,
  decimal Price,
  bool Clearance
);