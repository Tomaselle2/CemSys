using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class Tramite
{
    public int IdTramite { get; set; }

    public int? IdNichosDifuntosFk { get; set; }

    public int? IdFosasDifuntosFk { get; set; }

    public int? IdPanteonesDifuntos { get; set; }

    public virtual FosasDifunto? IdFosasDifuntosFkNavigation { get; set; }

    public virtual NichosDifunto? IdNichosDifuntosFkNavigation { get; set; }

    public virtual PanteonDifunto? IdPanteonesDifuntosNavigation { get; set; }
}
