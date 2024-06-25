using CosmodeDb.Api.Extensions;
using CosmodeDb.Domain.Mappers;
using CosmosDb.Data.Interfaces;
using CosmosDb.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmodeDb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(IDatabase database) : ControllerBase
{
  [HttpGet]
  public IActionResult GetUserById()
  {
    var user = database.GetUsersContainer()
                       .GetById(User.Id());

    return Ok(user.MapToResponse());
  }

  [HttpGet("all")]
  [Authorize(Policy = Constants.ADMIN_POLICY)]
  public IActionResult GetAllUsers()
  {
    var users = database.GetUsersContainer()
                        .GetAll();

    return Ok(users.Select(u => u.MapToResponse()));
  }
}