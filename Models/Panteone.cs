using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class Panteone
{
    public int IdPanteon { get; set; }

    public bool Visibilidad { get; set; }

    public int Difuntos { get; set; }

    public int? NroCuenta { get; set; }

    public virtual ICollection<ContratoConcesion> ContratoConcesions { get; set; } = new List<ContratoConcesion>();

    public virtual NumeroCuentum? NroCuentaNavigation { get; set; }

    public virtual ICollection<PanteonDifunto> PanteonDifuntos { get; set; } = new List<PanteonDifunto>();
}
