namespace CosmodeDb.Domain.Security.Requests;

public sealed record RegisterRequest(string Name, string UserName, string Email, string Password);