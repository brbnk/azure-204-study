using CosmosDb.Domain.Products.Interfaces;
using CosmosDb.Domain.Settings;
using CosmosDb.Services;

namespace CosmosDb.Api.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddSettings(this IServiceCollection services, ConfigurationManager configuration)
  {
    services.Configure<CosmosDbSettings>(configuration.GetSection(nameof(CosmosDbSettings)));
    
    return services;
  }

  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddScoped<IProductService, ProductService>();

    return services;
  }
}