using Azure.Identity;
using CosmosDb.Domain.Products;
using CosmosDb.Domain.Products.Interfaces;
using CosmosDb.Domain.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace CosmosDb.Services;

public sealed class ProductService : IProductService
{
  private const string CONTAINER_NAME = "products";

  private readonly CosmosDbSettings _settings;
  private readonly Container _container;

  public ProductService(IOptions<CosmosDbSettings> options)
  {
    _settings = options.Value;

    var client = new CosmosClient(accountEndpoint: _settings.AccountEndpoint, tokenCredential: new DefaultAzureCredential());
    var database = client.GetDatabase(_settings.Database);
    _container = database.GetContainer(CONTAINER_NAME);
  }

  public async Task<Product> AddAsync(Product product)
  {
    var response = await _container.UpsertItemAsync(item: product, partitionKey: new PartitionKey(product.Category));

    return response.Resource;
  }
}