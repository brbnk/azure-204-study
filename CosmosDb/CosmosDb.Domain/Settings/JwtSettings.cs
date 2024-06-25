namespace CosmosDb.Domain.Settings;

public sealed class JwtSettings
{
  public string PrivateKey { get; set; }

  public int ExpirationInHours { get; set; }
}