using CosmodeDb.Data.Mock;
using CosmosDb.Data.Interfaces;

namespace CosmodeDb.Data;

public sealed class InMemoryDatabase : IDatabase 
{
    private readonly UsersContainer _usersContainer = new();

    public InMemoryDatabase()
    {
      Connect();
    }

    public void Connect()
    {
      Console.WriteLine("Connected to in memory database");
    }

    public UsersContainer GetUsersContainer()
    {
      return _usersContainer;
    }
}