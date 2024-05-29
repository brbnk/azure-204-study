using ImageUploader.Domain.Interfaces;
using ImageUploader.Domain.FileStorage;
using ImageUploader.Services;

namespace ImageUploader.Api.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddServices(this IServiceCollection services) 
  {
    services.AddScoped<IFileService, FileService>();

    return services;
  }

  public static IServiceCollection ConfigureOptions(this IServiceCollection services, ConfigurationManager configuration)
  {
    services.Configure<FileStorageSettings>(configuration.GetSection(nameof(FileStorageSettings)));
    
    return services;
  }
}