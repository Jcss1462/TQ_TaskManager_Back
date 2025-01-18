using TQ_TaskManager_Back.Models;

namespace TQ_TaskManager_Back.Dtos
{
    public class UpdateTareaDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int? Usuarioasignadoid { get; set; }
        public int Estadoid { get; set; }

    }
}
