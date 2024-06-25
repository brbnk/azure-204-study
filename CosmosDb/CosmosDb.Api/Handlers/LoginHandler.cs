using CosmodeDb.Domain.Account;
using CosmodeDb.Domain.Security.Interfaces;
using CosmodeDb.Domain.Shared;
using CosmosDb.Domain.Account;
using CosmosDb.Domain.Security.Interfaces;
using CosmosDb.Domain.Security.Requests;

namespace CosmodeDb.Api.Handlers;

public sealed class LoginHandler(ITokenService tokenService) : ILoginHandler
{
    private readonly IEnumerable<User> _memoryUsers = [
        new User(Name: "Bruno Nakayabu", 
                 Email: "bruno.nakayabu@gmail.com", 
                 Password: "t3h+yPG793AZtKj/M1yZXA==.dA4PqzkRCOXYyt6GX4aZtgRdH6NGK1gz8ULHdvP2ndc=",
                 Roles: [ "student", "premium" ])
    ];

    public Response Handle(LoginRequest payload)
    {
        var email = new Email(payload.Email);

        if (!email.IsValid())
            return Response.Failed("The emails is invalid.");
        
        var user = _memoryUsers.FirstOrDefault(u => u.Email.Equals(email.Address));

        if (user is null)
            return Response.Failed("The user does not exist.");

        var password = new Password(payload.Password);

        password.SetHashedPasswordToCompare(user.Password);

        if (!password.IsValid())
            return Response.Failed("The password is incorrect.");

        var token = tokenService.Create(user);

        var response = new Response<string>() 
        {
            Success = true,
            Message = string.Empty,
            Content = token
        };

        return response;
    }
}
