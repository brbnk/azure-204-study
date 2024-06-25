using CosmodeDb.Data.Interfaces;
using CosmodeDb.Domain.Account;

namespace CosmodeDb.Data.Mock;

public sealed class UsersContainer : IUsersContainer
{
  private readonly List<User> _data = [];

  public void Add(User item)
  {
    _data.Add(item);
  }

  public bool Exists(string email)
  {
    return _data.Any(u => u.Email.Equals(email));
  }

  public IEnumerable<User> GetAll()
  {
    return _data;
  }

  public User? GetByEmail(string email)
  {
    return _data.FirstOrDefault(u => u.Email.Equals(email));
  }
}