using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class NumeroCuentum
{
    public int IdNumeroCuenta { get; set; }

    public int Numero { get; set; }

    public virtual ICollection<ContratoConcesion> ContratoConcesions { get; set; } = new List<ContratoConcesion>();
}
