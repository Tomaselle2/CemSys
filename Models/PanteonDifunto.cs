using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class PanteonDifunto
{
    public int IdPanteonDifuntos { get; set; }

    public int DifuntoId { get; set; }

    public int PanteonId { get; set; }

    public bool Visibilidad { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public string? Empresa { get; set; }

    public int? Usuario { get; set; }

    public virtual ICollection<ContratoConcesion> ContratoConcesions { get; set; } = new List<ContratoConcesion>();

    public virtual Difunto Difunto { get; set; } = null!;

    public virtual Panteone Panteon { get; set; } = null!;

    public virtual Usuario? UsuarioNavigation { get; set; }
}
