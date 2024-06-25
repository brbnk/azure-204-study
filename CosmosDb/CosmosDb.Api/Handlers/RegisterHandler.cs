using CosmodeDb.Domain.Account;
using CosmodeDb.Domain.Security.Interfaces;
using CosmodeDb.Domain.Security.Requests;
using CosmodeDb.Domain.Settings;
using CosmodeDb.Domain.Shared;
using CosmosDb.Domain.Account;
using Microsoft.Extensions.Options;

namespace CosmodeDb.Api.Handlers;

public sealed class RegisterHandler() : IRegisterHandler
{
    public Response Handle(RegisterRequest payload)
    {
        var response = new Response<User>();

        var email = new Email(payload.Email);

        if (!email.IsValid())
            return response;

        var password = new Password(payload.Password);

        var hash = password.GenerateHash(password.Salt);

        var user = new User(Name: payload.Name, Email: email.Address, Password: hash, Roles: []);

        response.Success = true;
        response.Content = user;

        return response;
    }
}
