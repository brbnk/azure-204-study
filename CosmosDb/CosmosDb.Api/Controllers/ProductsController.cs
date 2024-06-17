using CosmosDb.Domain.Products;
using CosmosDb.Domain.Products.Interfaces;
using CosmosDb.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> AddProductAsync(CreateProductRequest request)
  {
    var newProduct = new Product(request);

    var product = await productService.AddAsync(newProduct);

    return Ok(product);
  }
}