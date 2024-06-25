using System.Security.Claims;
using CosmosDb.Domain.Shared;

namespace CosmodeDb.Domain.Account;

public record User(string Name, string Email, string Password, IEnumerable<string> Roles) : DocumentBase
{
  public string Name { get; set; } = Name;

  public string Email { get; set; } = Email;

  public string Password { get; set; } = Password;

  public IEnumerable<string> Roles { get; set; } = Roles;

  public ClaimsIdentity GenerateClaims()
  {
    var ci = new ClaimsIdentity();

    ci.AddClaim(new Claim(ClaimTypes.Name, Name));
    ci.AddClaim(new Claim(ClaimTypes.Email, Email));

    foreach (var role in Roles)
      ci.AddClaim(new Claim(ClaimTypes.Role, role));

    return ci;
  }
}