using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class Nicho
{
    public int IdNicho { get; set; }

    public bool Visibilidad { get; set; }

    public int Difuntos { get; set; }

    public int Seccion { get; set; }

    public int TipoNicho { get; set; }

    public int NroNicho { get; set; }

    public int NroFila { get; set; }

    public virtual ICollection<NichosDifunto> NichosDifuntos { get; set; } = new List<NichosDifunto>();

    public virtual SeccionesNicho SeccionNavigation { get; set; } = null!;

    public virtual TipoNicho TipoNichoNavigation { get; set; } = null!;
}
