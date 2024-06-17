using Newtonsoft.Json;

namespace CosmosDb.Domain.Shared;

public abstract record DocumentBase
{
  [JsonProperty("id")]
  public string Id { get; } = Guid.NewGuid().ToString();
}