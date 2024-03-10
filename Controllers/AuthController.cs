using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase 
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService service)
    {
        _logger = logger;
        _authService = service;
    }

    [HttpPost]
    [Route("register")]
    public ActionResult CreateUser(User user) 
    {
        if (user == null || !ModelState.IsValid) {
            return BadRequest();
        }
        _authService.CreateUser(user);
        return NoContent();
    }

    [HttpGet]
    [Route("login")]
    public ActionResult<string> SignIn(string Username, string Password) 
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            return BadRequest();
        }

        var token = _authService.SignIn(Username, Password);

        if (string.IsNullOrWhiteSpace(token)) {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpGet]
    [Route("current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> GetCurrentUser() 
    {
        var username = User.Identity.Name;

        var user = _authService.GetUserByUsername(username);

        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }

    [HttpGet]
    [Route("username")]
    public ActionResult<User> GetUserByUsername(string username) 
    {
        var user = _authService.GetUserByUsername(username);

    if (user == null)
    {
        return NotFound("User not found");
    }

    return Ok(user);
    }

    [HttpPut]
    [Route("update")]
    public ActionResult<User> UpdateUser(User updatedUser)
    {
        if (updatedUser == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingUser = _authService.GetUserByUsername(updatedUser.Username);

        if (existingUser == null)
        {
            return NotFound("User not found");
        }

        return Ok(_authService.UpdateUser(updatedUser));
    }
}