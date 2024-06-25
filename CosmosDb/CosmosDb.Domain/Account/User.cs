using System.Security.Claims;
using CosmodeDb.Domain.Account.Enums;
using CosmosDb.Domain;
using CosmosDb.Domain.Shared;

namespace CosmodeDb.Domain.Account;

public record User : DocumentBase
{
  public string Name { get; set; }

  public string Email { get; set; }

  public string Password { get; set; }

  public IList<string> Roles { get; set; }

  public User(string name, string email, string password, AccountType accountType)
  {
    Name = name;
    Email = email;
    Password = password;
    Roles = GetRoles(accountType);
  }

  public ClaimsIdentity GenerateClaims()
  {
    var ci = new ClaimsIdentity();

    ci.AddClaim(new Claim(ClaimTypes.Name, Name));
    ci.AddClaim(new Claim(ClaimTypes.Email, Email));
    ci.AddClaim(new Claim(Constants.CLAIM_TYPE_ID, Id));

    foreach (var role in Roles)
      ci.AddClaim(new Claim(ClaimTypes.Role, role));

    return ci;
  }

  private static List<string> GetRoles(AccountType accountType)
  {
    var roles = new List<string>();

    switch(accountType)
    {
      case AccountType.ADMIN:
        roles.AddRange([ 
          Constants.ADMIN_ROLE, 
          Constants.PREMIUM_ROLE, 
          Constants.FREE_ROLE 
        ]);
        break;
      case AccountType.PREMIUM:
        roles.AddRange([ 
          Constants.PREMIUM_ROLE, 
          Constants.FREE_ROLE 
        ]);
        break;
      case AccountType.FREE:
        roles.Add(Constants.FREE_ROLE);
        break;
    }

    return roles;
  }
}