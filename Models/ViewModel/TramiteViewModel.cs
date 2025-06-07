namespace CemSys.Models.ViewModel
{
    public class TramiteViewModel
    {
        public int IdTramite { get; set; }
        public string? nombreDifunto { get; set; }
        public string? apellidoDifunto { get; set; }
        public string? DNI { get; set; }
        public DateOnly? fechaDefuncion { get; set; }
        public DateOnly? fechaNacimiento { get; set; }

        public int? actaDefuncion { get; set; }
        public int? tomo { get; set; }
        public int? folio { get; set; }
        public string? serie { get; set; }
        public int? age { get; set; }

        public string? estadoDifunto { get; set; }
        public string? datosAdicionales { get; set; }

        public DateTime? fechaIngreso { get; set; }
        public string? empresa { get; set; }

        public string? usuario { get; set; }
        public string? seccion { get; set; }
        public string? ubicacion { get; set; }

        // Propiedades para las claves foráneas
        public int? idNichoDifuntoFK { get; set; }
        public int? idFosaDifuntoFK { get; set; }
        public int? idPanteonDifuntoFK { get; set; }

        // Propiedades para los objetos relacionados 
        public NichosDifunto nichosDifuntos { get; set; }
        public FosasDifunto fosasDifuntos { get; set; }
        public PanteonDifunto panteonesDifuntos { get; set; }

        // Propiedad calculada

        public object? DifuntoRelacionado
        {
            get
            {
                if (nichosDifuntos != null)
                    return nichosDifuntos;

                if (fosasDifuntos != null)
                    return fosasDifuntos;

                if (panteonesDifuntos != null)
                    return panteonesDifuntos;

                return null;
            }
        }
    }
}
