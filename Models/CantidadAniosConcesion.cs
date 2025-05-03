using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class CantidadAniosConcesion
{
    public int IdCantidadAnios { get; set; }

    public int? Cantidad { get; set; }

    public virtual ICollection<ContratoConcesion> ContratoConcesions { get; set; } = new List<ContratoConcesion>();
}
