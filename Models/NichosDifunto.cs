using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class NichosDifunto
{
    public int IdNichosDifuntos { get; set; }

    public int DifuntoId { get; set; }

    public int NichoId { get; set; }

    public virtual Difunto Difunto { get; set; } = null!;

    public virtual Nicho Nicho { get; set; } = null!;
}
