using MediatR;
using Microsoft.AspNetCore.Mvc;
using Othello.Application.UseCases;

namespace Othello.Presentation.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.UserExists) return Conflict("User already exists.");
        if (result.UserCreated) return StatusCode(201, "User created successfully.");

        return StatusCode(500, "An error occurred while creating the user.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.UserAuthenticated) return Ok(new {token = result.Token});

        return Unauthorized("Invalid username or password.");
    }
}