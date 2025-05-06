using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class SeccionesPanteone
{
    public int IdSeccionPanteones { get; set; }

    public string Nombre { get; set; } = null!;

    public int Panteones { get; set; }

    public bool Visibilidad { get; set; }

    public virtual ICollection<Panteone> PanteonesNavigation { get; set; } = new List<Panteone>();
}
