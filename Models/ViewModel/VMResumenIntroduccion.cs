namespace CemSys.Models.ViewModel
{
    public class VMResumenIntroduccion
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

        public DateTime? fechaIngreso { get; set; }
        public string? empresa { get; set; }

        public string? usuario { get; set; }
        public string? seccion { get; set; }
        public string? ubicacion { get; set; }
    }
}
