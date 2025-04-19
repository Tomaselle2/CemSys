using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class SeccionesNicho
{
    public int IdSeccionNicho { get; set; }

    public string Nombre { get; set; } = null!;

    public int Filas { get; set; }

    public int Nichos { get; set; }

    public bool Visibilidad { get; set; }

    public int TipoNumeracion { get; set; }

    public virtual ICollection<Nicho> NichosNavigation { get; set; } = new List<Nicho>();

    public virtual TipoNumeracionParcela TipoNumeracionNavigation { get; set; } = null!;
}
