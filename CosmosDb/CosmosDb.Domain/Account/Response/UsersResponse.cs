namespace CosmodeDb.Domain.Account.Response;

public sealed record UsersResponse
{
  public string Id { get; set; }
  
  public string Name { get; set; }
  
  public string Username { get; set; }
  
  public string Email { get; set; }
}