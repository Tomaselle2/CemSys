using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class ActaDefuncion
{
    public int IdActaDefuncion { get; set; }

    public int? NroActa { get; set; }

    public int? Tomo { get; set; }

    public int? Folio { get; set; }

    public string? Serie { get; set; }

    public int? Age { get; set; }

    public virtual ICollection<Difunto> Difuntos { get; set; } = new List<Difunto>();
}
