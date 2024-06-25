using CosmodeDb.Domain.Account;

namespace CosmosDb.Domain.Security.Interfaces;

public interface ITokenService
{
  public string Create(User user);
}