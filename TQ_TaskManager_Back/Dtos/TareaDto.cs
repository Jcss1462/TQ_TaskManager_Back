using TQ_TaskManager_Back.Models;

namespace TQ_TaskManager_Back.Dtos
{
    public class TareaDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int? Usuarioasignadoid { get; set; }

        public string? UsuarioasignadoCorreo { get; set; }
        public int Estadoid { get; set; }
        public string? EstadoidName { get; set; }

    }
}
