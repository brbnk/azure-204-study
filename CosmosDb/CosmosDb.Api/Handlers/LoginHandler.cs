using CosmodeDb.Domain.Account;
using CosmodeDb.Domain.Security.Interfaces;
using CosmodeDb.Domain.Shared;
using CosmosDb.Data.Interfaces;
using CosmosDb.Domain.Account;
using CosmosDb.Domain.Security.Interfaces;
using CosmosDb.Domain.Security.Requests;

namespace CosmodeDb.Api.Handlers;

public sealed class LoginHandler(ITokenService tokenService,
                                 IDatabase database) : ILoginHandler
{
    public Response Handle(LoginRequest payload)
    {
        var email = new Email(payload.Email);

        if (!email.IsValid())
            return Response.Failed("The email is invalid.");

        var usersContainer = database.GetUsersContainer();

        var user = usersContainer.GetByEmail(email.Address);

        if (user is null)
            return Response.Failed("The user does not exist.");

        var password = new Password(payload.Password);

        password.SetHashedPasswordToCompare(user.Password);

        if (!password.IsValid())
            return Response.Failed("The password is incorrect.");

        var token = tokenService.Create(user);

        var response = new Response<string>();

        return response.Succeed(token);
    }
}
