using CosmodeDb.Domain.Account;
using CosmodeDb.Domain.Account.Response;

namespace CosmodeDb.Domain.Mappers;

public static class UsersMap
{
  public static UsersResponse MapToResponse(this User user)
  {
    return new() 
    {
      Id = user.Id,
      Name = user.Name,
      Username = user.Name,
      Email = user.Email
    };
  }
}