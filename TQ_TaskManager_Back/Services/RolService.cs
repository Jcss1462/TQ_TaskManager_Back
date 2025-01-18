using Microsoft.EntityFrameworkCore;
using TQ_TaskManager_Back.Contexts;
using TQ_TaskManager_Back.Models;

namespace TQ_TaskManager_Back.Services;

public class RolService : IRolService
{

    private readonly TQContext _context;
    public RolService(TQContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetAllRols()
    {
        return await _context.Roles.ToListAsync();
    }
}

public interface IRolService
{
    Task<List<Role>> GetAllRols();
}
