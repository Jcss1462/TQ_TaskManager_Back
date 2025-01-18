using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TQ_TaskManager_Back.Services;

namespace TQ_TaskManager_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadoController : ControllerBase
{
    IEstadoService _estadoService;

    public EstadoController(IEstadoService estadoService)
    {
        _estadoService = estadoService;
    }


    [HttpGet("getAllEstados")]
    public async Task<IActionResult> GetAllEstados()
    {
        return Ok(await _estadoService.GetAllEstados());
    }


}
