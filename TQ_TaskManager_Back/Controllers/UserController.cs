using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TQ_TaskManager_Back.Services;

namespace TQ_TaskManager_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet("getAllUsers")]
    [Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userService.GetListOfUsers());
    }


}
