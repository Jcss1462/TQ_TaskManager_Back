using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TQ_TaskManager_Back.Services;

namespace TQ_TaskManager_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolController : ControllerBase
{
    IRolService _rolService;

    public RolController(IRolService rolService)
    {
        _rolService = rolService;
    }


    [HttpGet("getAllRols")]
    public async Task<IActionResult> GetAllRols()
    {
        return Ok(await _rolService.GetAllRols());
    }


}
