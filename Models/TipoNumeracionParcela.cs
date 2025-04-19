using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class TipoNumeracionParcela
{
    public int IdTipoNumeracionParcela { get; set; }

    public string TipoNumeracion { get; set; } = null!;

    public virtual ICollection<SeccionesNicho> SeccionesNichos { get; set; } = new List<SeccionesNicho>();
}
