using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class EstadoDifunto
{
    public int IdEstadoDifunto { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Difunto> Difuntos { get; set; } = new List<Difunto>();
}
