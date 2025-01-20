using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TQ_TaskManager_Back.Dtos;
using TQ_TaskManager_Back.Services;

namespace TQ_TaskManager_Back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskContoller : ControllerBase
{
    ITaskService _taskService;

    public TaskContoller(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost("createTask")]
    [Authorize]
    public async Task<IActionResult> CreateTask([FromBody] CreateTareaDto createTareaDto)
    {
        await _taskService.CreateTask(createTareaDto);
        return Ok();
    }


    [HttpPut("upadateTask")]
    [Authorize]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTareaDto updateTareaDto)
    {
        await _taskService.UpdateTask(updateTareaDto);
        return Ok();
    }


    [HttpDelete("deleteTask/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await _taskService.DeleteTask(id);
        return Ok();
    }


    [HttpGet("getTaskById/{id}")]
    [Authorize]
    public async Task<IActionResult> GetTaskById(int id)
    {
        return Ok(await _taskService.GetTaskById(id));
    }


    [HttpGet("getTaskListByUserId/{id}")]
    [Authorize]
    public async Task<IActionResult> GetTaskListByUserId(int id)
    {
        return Ok(await _taskService.GetTaskListByUserId(id));
    }


    [HttpGet("getListAllTasks")]
    [Authorize]
    public async Task<IActionResult> GetListAllTasks()
    {
        return Ok(await _taskService.GetListAllTasks());
    }




}
