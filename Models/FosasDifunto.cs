﻿using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class FosasDifunto
{
    public int IdFosasDifuntos { get; set; }

    public int DifuntoId { get; set; }

    public int FosaId { get; set; }

    public bool Visibilidad { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public string? Empresa { get; set; }

    public int? Usuario { get; set; }

    public virtual ICollection<ContratoConcesion> ContratoConcesions { get; set; } = new List<ContratoConcesion>();

    public virtual Difunto Difunto { get; set; } = null!;

    public virtual Fosa Fosa { get; set; } = null!;

    public virtual ICollection<Tramite> Tramites { get; set; } = new List<Tramite>();

    public virtual Usuario? UsuarioNavigation { get; set; }
}
