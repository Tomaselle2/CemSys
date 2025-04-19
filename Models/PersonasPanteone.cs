using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class PersonasPanteone
{
    public int IdPersonaFosas { get; set; }

    public int PersonalId { get; set; }

    public int PanteonSeId { get; set; }

    public virtual Panteone PanteonSe { get; set; } = null!;

    public virtual Persona Personal { get; set; } = null!;
}
