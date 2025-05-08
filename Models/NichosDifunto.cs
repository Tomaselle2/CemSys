using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class NichosDifunto
{
    public int IdNichosDifuntos { get; set; }

    public int DifuntoId { get; set; }

    public int NichoId { get; set; }

    public bool Visibilidad { get; set; }

    public virtual ICollection<ContratoConcesion> ContratoConcesions { get; set; } = new List<ContratoConcesion>();

    public virtual Difunto Difunto { get; set; } = null!;

    public virtual Nicho Nicho { get; set; } = null!;
}
