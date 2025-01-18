using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TQ_TaskManager_Back.Contexts;
using TQ_TaskManager_Back.Dtos;
using TQ_TaskManager_Back.Models;

namespace TQ_TaskManager_Back.Services;

public class UserService : IUserService
{

    private readonly TQContext _context;
    private readonly ISecurityService _securityService;
    public UserService(TQContext context, ISecurityService securityService)
    {
        _context = context;
        _securityService = securityService;
    }

    public async Task<bool> AuthenticateAsync(LoginRequestDto loginRequest)
    {

        string hasPassword = _securityService.HashPassword(loginRequest.Password);

        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == loginRequest.Email && u.Contrasena == hasPassword);

        return await Task.FromResult(usuario != null);
    }

    public async Task<Usuario> GetUsuarioByEmail(string email)
    {

        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);

        if (usuario == null)
        {
            throw new Exception($"El usuario con email: {email}, no existe.");
        }
        return usuario;
    }

    public async Task RegisterUserAsync(RegisterRequestDto user)
    {

        if (!IsValidEmail(user.Email))
        {
            throw new Exception($"El email: {user.Email}, no tiene un formato válido.");
        }

        if (await UserExistsAsync(user.Email))
        {
            throw new Exception($"El email: {user.Email}, ya está en uso.");
        }

        Role? rolUsuario = _context.Roles.FirstOrDefault(r => r.Id==2);

        if (rolUsuario == null) {
            throw new Exception($"No se encontro el rol de usuario al crear el nuevo usuario");
        }


        var newUser = new Usuario
        {
            Correo = user.Email,
            Contrasena = _securityService.HashPassword(user.Password),
            Nombre= user.Name,
            Rol= rolUsuario
        };

        _context.Usuarios.Add(newUser);
        await _context.SaveChangesAsync();

    }

    public async Task<bool> UserExistsAsync(string email)
    {
        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);
        return await Task.FromResult(usuario != null); ;
    }

    private bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }

}

public interface IUserService
{
    Task RegisterUserAsync(RegisterRequestDto user);
    Task<bool> AuthenticateAsync(LoginRequestDto loginRequest);
    Task<bool> UserExistsAsync(string email);
    Task<Usuario> GetUsuarioByEmail(string email);

}
