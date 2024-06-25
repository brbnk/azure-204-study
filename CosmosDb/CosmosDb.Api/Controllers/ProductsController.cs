using CosmosDb.Domain.Mappers;
using CosmosDb.Domain.Products.Interfaces;
using CosmosDb.Domain.Products.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDb.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> AddProductAsync(CreateProductRequest request)
  {
    var product = await productService.AddAsync(request.MapToProduct());

    return Ok(product);
  }
}