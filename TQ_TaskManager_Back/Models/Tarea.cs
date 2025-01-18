using System;
using System.Collections.Generic;

namespace TQ_TaskManager_Back.Models;

public partial class Tarea
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int Estadoid { get; set; }

    public int? Usuarioasignadoid { get; set; }

    public DateTime Fechacreacion { get; set; }

    public DateTime Fechaactualizacion { get; set; }

    public virtual Estado Estado { get; set; } = null!;

    public virtual Usuario? Usuarioasignado { get; set; }
}
