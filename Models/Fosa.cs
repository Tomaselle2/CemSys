using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class Fosa
{
    public int IdFosa { get; set; }

    public bool Visibilidad { get; set; }

    public int Difuntos { get; set; }

    public int Seccion { get; set; }

    public int? NroCuenta { get; set; }

    public virtual ICollection<FosasDifunto> FosasDifuntos { get; set; } = new List<FosasDifunto>();

    public virtual NumeroCuentum? NroCuentaNavigation { get; set; }

    public virtual ICollection<PersonasFosa> PersonasFosas { get; set; } = new List<PersonasFosa>();

    public virtual SeccionesFosa SeccionNavigation { get; set; } = null!;
}
