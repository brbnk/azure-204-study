using CosmodeDb.Domain.Security.Interfaces;
using CosmodeDb.Domain.Security.Requests;
using CosmosDb.Domain.Security.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDb.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public sealed class AccountController(IRegisterHandler registerHandler, ILoginHandler loginHandler) : ControllerBase
{
  [HttpPost("login")]
  public IActionResult Login([FromHeader] string email, [FromHeader] string password)
  {
    var request = new LoginRequest(email, password);

    var response = loginHandler.Handle(request);

    if (!response.Success)
    {
      return BadRequest(response);
    }

    return Ok(response);
  }

  [HttpPost("register")]
  public IActionResult Register([FromBody] RegisterRequest request)
  {
    var response = registerHandler.Handle(request);

    return Ok(response);
  }
}