using CosmodeDb.Domain.Account;
using CosmodeDb.Domain.Security.Interfaces;
using CosmodeDb.Domain.Security.Requests;
using CosmosDb.Domain.Security.Interfaces;
using CosmosDb.Domain.Security.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDb.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public sealed class AuthController(ITokenService tokenService, 
                                   IRegisterHandler registerHandler,
                                   ILoginHandler loginHandler) : ControllerBase
{

  [HttpPost("login")]
  public IActionResult Login([FromBody] LoginRequest request)
  {
    var response = loginHandler.Handle(request);

    return Ok(response);
  }

  [HttpPost("register")]
  public IActionResult Register([FromBody] RegisterRequest request)
  {
    var response = registerHandler.Handle(request);

    return Ok(response);
  }
}