using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class Difunto
{
    public int IdDifunto { get; set; }

    public bool Visibilidad { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Dni { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public DateOnly? FechaDefuncion { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public int? ActaDefuncion { get; set; }

    public int Estado { get; set; }

    public string? Descripcion { get; set; }

    public virtual ActaDefuncion? ActaDefuncionNavigation { get; set; }

    public virtual EstadoDifunto EstadoNavigation { get; set; } = null!;

    public virtual ICollection<FosasDifunto> FosasDifuntos { get; set; } = new List<FosasDifunto>();

    public virtual ICollection<NichosDifunto> NichosDifuntos { get; set; } = new List<NichosDifunto>();

    public virtual ICollection<PanteonDifunto> PanteonDifuntos { get; set; } = new List<PanteonDifunto>();
}
