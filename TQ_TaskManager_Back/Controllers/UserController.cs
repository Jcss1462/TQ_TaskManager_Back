using Microsoft.AspNetCore.Mvc;
using TQ_TaskManager_Back.Dtos;
using TQ_TaskManager_Back.Models;
using TQ_TaskManager_Back.Services;

namespace TQ_TaskManager_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    IUserService _userService;
    IAuthService _authService;

    public UserController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto user)
    {
        await _userService.RegisterUserAsync(user);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {

        if (!await _userService.AuthenticateAsync(request))
        {
            throw new Exception("Usuario o contraseña incorrectas");
        }

        Usuario usuario = await _userService.GetUsuarioByEmail(request.Email);

        var token = _authService.GenerateJwtToken(request.Email, usuario.Id, usuario.Rolid);
        return Ok(new { token });
    }
}
