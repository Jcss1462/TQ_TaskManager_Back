using Microsoft.EntityFrameworkCore;
using TQ_TaskManager_Back.Contexts;
using TQ_TaskManager_Back.Dtos;
using TQ_TaskManager_Back.Models;

namespace TQ_TaskManager_Back.Services;

public class EstadoService : IEstadoService
{

    private readonly TQContext _context;
    public EstadoService(TQContext context)
    {
        _context = context;
    }

    public async Task<List<EstadoDto>> GetAllEstados()
    {
        return await _context.Estados
            .Select(e => new EstadoDto
            {
                Id = e.Id,
                Nombre = e.Nombre
            })
            .ToListAsync();
    }
}

public interface IEstadoService
{
    Task<List<EstadoDto>> GetAllEstados();
}
