using CosmodeDb.Data.Mock;
using CosmodeDb.Domain.Account;
using CosmodeDb.Domain.Security.Interfaces;
using CosmodeDb.Domain.Security.Requests;
using CosmodeDb.Domain.Shared;
using CosmosDb.Data.Interfaces;
using CosmosDb.Domain.Account;

namespace CosmodeDb.Api.Handlers;

public sealed class RegisterHandler(IDatabase database) : IRegisterHandler
{
    public Response Handle(RegisterRequest payload)
    {
        var email = new Email(payload.Email);

        if (!email.IsValid())
            return Response.Failed("The email is invalid!");

        var usersContainer = database.GetUsersContainer();

        if (usersContainer.Exists(email.Address))
            return Response.Failed("User already exists!");

        var password = new Password(payload.Password);

        var hash = password.GenerateHash(password.Salt);

        usersContainer.Add(new User(Name: payload.Name, 
                                    Email: email.Address,
                                    Password: hash, 
                                    Roles: []));

        return Response.Succeed("The user was created with success!");
    }
}
