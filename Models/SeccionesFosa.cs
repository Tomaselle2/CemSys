using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class SeccionesFosa
{
    public int IdSeccionFosa { get; set; }

    public string Nombre { get; set; } = null!;

    public int Fosas { get; set; }

    public bool Visibilidad { get; set; }

    public virtual ICollection<Fosa> FosasNavigation { get; set; } = new List<Fosa>();
}
