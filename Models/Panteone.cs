using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class Panteone
{
    public int IdPanteon { get; set; }

    public bool Visibilidad { get; set; }

    public int Difuntos { get; set; }

    public string? Descripcion { get; set; }

    public int NroLote { get; set; }

    public int IdSeccionPanteon { get; set; }

    public virtual SeccionesPanteone? IdSeccionPanteonNavigation { get; set; }

    public virtual ICollection<PanteonDifunto> PanteonDifuntos { get; set; } = new List<PanteonDifunto>();
}
