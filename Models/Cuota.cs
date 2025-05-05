using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class Cuota
{
    public int IdCuota { get; set; }

    public int? Cuota1 { get; set; }

    public virtual ICollection<ContratoConcesion> ContratoConcesions { get; set; } = new List<ContratoConcesion>();
}
