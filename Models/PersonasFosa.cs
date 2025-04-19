using System;
using System.Collections.Generic;

namespace CemSys.Models;

public partial class PersonasFosa
{
    public int IdPersonaFosas { get; set; }

    public int PersonalId { get; set; }

    public int FosaId { get; set; }

    public virtual Fosa Fosa { get; set; } = null!;

    public virtual Persona Personal { get; set; } = null!;
}
