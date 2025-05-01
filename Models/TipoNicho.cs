using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class TipoNicho
{
    public int IdTipoNicho { get; set; }

    public string TipoNicho1 { get; set; } = null!;

    public bool PorDefecto { get; set; }

    public virtual ICollection<Nicho> Nichos { get; set; } = new List<Nicho>();
}
