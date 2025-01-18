using TQ_TaskManager_Back.Models;

namespace TQ_TaskManager_Back.Dtos;

public class UsuarioDto
{

    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public int Rolid { get; set; }

    public string RolDescription { get; set; } = null!;

}
