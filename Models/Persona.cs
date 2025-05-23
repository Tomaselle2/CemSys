﻿using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public bool Visibilidad { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Dni { get; set; }

    public string? Email { get; set; }

    public string? Celular { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Domicilio { get; set; }

    public int CategoriaPersona { get; set; }

    public virtual TipoCategoriaPersona CategoriaPersonaNavigation { get; set; } = null!;

    public virtual ICollection<ContratoConcesion> Contratos { get; set; } = new List<ContratoConcesion>();
}
