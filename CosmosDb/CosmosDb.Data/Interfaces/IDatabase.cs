using CosmodeDb.Data.Mock;

namespace CosmosDb.Data.Interfaces;

public interface IDatabase
{
  public void Connect();

  public UsersContainer GetUsersContainer();
}