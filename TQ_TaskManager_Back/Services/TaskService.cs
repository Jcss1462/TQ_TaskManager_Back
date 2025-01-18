using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Threading;
using TQ_TaskManager_Back.Contexts;
using TQ_TaskManager_Back.Dtos;
using TQ_TaskManager_Back.Models;

namespace TQ_TaskManager_Back.Services;

public class TaskService : ITaskService
{

    private readonly TQContext _context;

    public TaskService(TQContext context)
    {
        _context = context;
    }

    public async Task CreateTask(CreateTareaDto createTareaDto)
    {


        Estado? estadoPendiente = _context.Estados.FirstOrDefault(r => r.Id == 1);
        Usuario? ususarioAsignado = _context.Usuarios.FirstOrDefault(r => r.Id == createTareaDto.Usuarioasignadoid);

        if (estadoPendiente == null)
        {
            throw new Exception("No se encontro el estado pendiente, al momento de crear la tarea");
        }

        if (ususarioAsignado == null)
        {
            throw new Exception("No se encontro el usuario, al momento de crear la tarea");
        }

        Tarea newTarea = new Tarea
        {
            Titulo = createTareaDto.Titulo,
            Descripcion = createTareaDto.Descripcion,
            Estado = estadoPendiente,
            Usuarioasignado= ususarioAsignado,
            Fechacreacion= DateTime.Now,
            Fechaactualizacion= DateTime.Now
        };

        await _context.Tareas.AddAsync(newTarea);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTask(int id)
    {
        Tarea? tareaToDelete = _context.Tareas.FirstOrDefault(r => r.Id == id);

        if(tareaToDelete == null)
        {
            throw new Exception("No se encontro la tarea con id: " + id+ ", al momento de eliminar la tarea");
        }

        _context.Tareas.Remove(tareaToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TareaDto>> GetListAllTasks()
    {
        #pragma warning disable CS8602 // Dereference of a possibly null reference.
        List<TareaDto> ListTareasDto = await _context.Tareas
            .Include(t => t.Usuarioasignado)
            .Include(t => t.Estado)
            .Select(u => new TareaDto
            {
                Id = u.Id,
                Titulo = u.Titulo,
                Descripcion = u.Descripcion,
                Usuarioasignadoid = u.Usuarioasignadoid,
                UsuarioasignadoCorreo = u.Usuarioasignado.Correo,
                Estadoid = u.Estadoid,
                EstadoidName = u.Estado.Nombre
            }).ToListAsync();
        #pragma warning restore CS8602 // Dereference of a possibly null reference.

        return ListTareasDto;
    }

    public async Task<TareaDto> GetTaskById(int id)
    {
        Tarea? tarea = await _context.Tareas
            .Include(t => t.Usuarioasignado)
            .Include(t => t.Estado)
            .Where(t => t.Id == id).FirstOrDefaultAsync();

        if (tarea == null)
        {
            throw new Exception("La tarea con id: " + id + ", No existe");
        }

        TareaDto tareaDto = new TareaDto {

            Id= tarea.Id,
            Titulo=tarea.Titulo,
            Descripcion=tarea.Descripcion,
            Usuarioasignadoid=tarea.Usuarioasignadoid,
            UsuarioasignadoCorreo=tarea.Usuarioasignado?.Correo,
            Estadoid=tarea.Estadoid,
            EstadoidName=tarea.Estado.Nombre
        };

        return tareaDto;
    }

    public async Task<List<TareaDto>> GetTaskListByUserId(int id)
    {

        #pragma warning disable CS8602 // Dereference of a possibly null reference.
        List<TareaDto> ListTareasDto = await _context.Tareas
            .Include(t => t.Usuarioasignado)
            .Include(t => t.Estado)
            .Where(t => t.Usuarioasignadoid == id)
            .Select(u => new TareaDto
            {
                Id = u.Id,
                Titulo = u.Titulo,
                Descripcion = u.Descripcion,
                Usuarioasignadoid = u.Usuarioasignadoid,
                UsuarioasignadoCorreo = u.Usuarioasignado.Correo,
                Estadoid = u.Estadoid,
                EstadoidName = u.Estado.Nombre
            }).ToListAsync();
        #pragma warning restore CS8602 // Dereference of a possibly null reference.

        return ListTareasDto;
    }

    public async Task UpdateTask(UpdateTareaDto updateTareaDto)
    {
        Tarea? tarea = _context.Tareas.FirstOrDefault(r => r.Id == updateTareaDto.Id);

        if (tarea == null)
        {
            throw new Exception("No se encontro la tarea con id" + updateTareaDto.Id + ", al momento de actualizar la tarea");
        }

        //valido cambios
        if (tarea.Estadoid != updateTareaDto.Estadoid) {

            Estado? nuevoEstado = _context.Estados.FirstOrDefault(r => r.Id == updateTareaDto.Estadoid);

            if (nuevoEstado == null)
            {
                throw new Exception("No se encontro el estado con id: " + updateTareaDto.Estadoid + " al momento de actualizr la tarea");
            }
            else {
                tarea.Estado = nuevoEstado;
            }

        }

        if (tarea.Usuarioasignadoid != updateTareaDto.Usuarioasignadoid)
        {

            Usuario? usuario = _context.Usuarios.FirstOrDefault(r => r.Id == updateTareaDto.Usuarioasignadoid);

            if (usuario == null)
            {
                throw new Exception("No se encontro el usuario con id: " + updateTareaDto.Usuarioasignadoid + " al momento de actualizr la tarea");
            }
            else
            {
                tarea.Usuarioasignado = usuario;
            }

        }

        tarea.Titulo = updateTareaDto.Titulo;
        tarea.Descripcion = updateTareaDto.Descripcion;
        tarea.Fechaactualizacion = DateTime.Now;

        await _context.SaveChangesAsync();

    }
}

public interface ITaskService
{
    Task CreateTask(CreateTareaDto createTareaDto);
    Task UpdateTask(UpdateTareaDto updateTareaDto);
    Task DeleteTask(int id);
    Task<TareaDto> GetTaskById(int id);
    Task<List<TareaDto>> GetTaskListByUserId(int id);
    Task<List<TareaDto>> GetListAllTasks();
}
