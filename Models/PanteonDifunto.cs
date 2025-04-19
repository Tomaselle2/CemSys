using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class PanteonDifunto
{
    public int IdPanteonDifuntos { get; set; }

    public int DifuntoId { get; set; }

    public int PanteonId { get; set; }

    public virtual Difunto Difunto { get; set; } = null!;

    public virtual Panteone Panteon { get; set; } = null!;
}
