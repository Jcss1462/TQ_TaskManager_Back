using Microsoft.EntityFrameworkCore;
using TQ_TaskManager_Back.Contexts;
using TQ_TaskManager_Back.Dtos;
using TQ_TaskManager_Back.Models;

namespace TQ_TaskManager_Back.Services;

public class RolService : IRolService
{

    private readonly TQContext _context;
    public RolService(TQContext context)
    {
        _context = context;
    }

    public async Task<List<RolDto>> GetAllRols()
    {
        return await _context.Roles
            .Select(r => new RolDto
            {
                Id = r.Id,
                Nombre = r.Nombre
            })
            .ToListAsync();
    }
}

public interface IRolService
{
    Task<List<RolDto>> GetAllRols();
}
