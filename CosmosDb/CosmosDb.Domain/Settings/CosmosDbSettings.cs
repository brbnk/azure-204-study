namespace CosmosDb.Domain.Settings;

public sealed class CosmosDbSettings
{
  public string AccountEndpoint { get; set; }
  
  public string ConnectionString { get; set; }

  public string Database { get; set; }
  
  public string Account { get; set; }
}