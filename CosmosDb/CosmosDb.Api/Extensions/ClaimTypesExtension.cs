using System.Security.Claims;
using CosmosDb.Domain;

namespace CosmodeDb.Api.Extensions;

public static class ClaimTypesExtension
{
  public static string Email(this ClaimsPrincipal claimsPrincipal)
  {
    return claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "";
  }

  public static string Id(this ClaimsPrincipal claimsPrincipal)
  {
    return claimsPrincipal.Claims.FirstOrDefault(c => c.Type == Constants.CLAIM_TYPE_ID)?.Value ?? "";
  }
}