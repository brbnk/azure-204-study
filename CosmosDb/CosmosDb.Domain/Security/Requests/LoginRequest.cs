namespace CosmosDb.Domain.Security.Requests;

public sealed record LoginRequest(string Email, string Password);