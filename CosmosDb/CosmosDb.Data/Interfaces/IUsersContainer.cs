using CosmodeDb.Domain.Account;

namespace CosmodeDb.Data.Interfaces;

public interface IUsersContainer
{
  public IEnumerable<User> GetAll();

  public void Add(User item);

  public User? GetById(string id);

  public User? GetByEmail(string email);

  public bool Exists(string email);
}