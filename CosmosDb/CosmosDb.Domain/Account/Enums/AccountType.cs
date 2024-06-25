using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CosmodeDb.Domain.Account.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum AccountType
{
  ADMIN,
  PREMIUM,
  FREE
}