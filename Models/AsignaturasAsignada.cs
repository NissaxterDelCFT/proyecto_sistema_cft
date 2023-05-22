using System;
using System.Collections.Generic;

namespace proyecto_sistema_cft.Models;

public partial class AsignaturasAsignada
{
    public int EstudiantesId { get; set; }

    public int AsignaturasId { get; set; }

    public int Id { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public virtual Asignatura Asignaturas { get; set; } = null!;

    public virtual Estudiante Estudiantes { get; set; } = null!;
}
