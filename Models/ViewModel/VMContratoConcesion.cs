using CemSys.Models.DTO;

namespace CemSys.Models.ViewModel
{
    public class VMContratoConcesion
    {
        public Contrato Contrato { get; set; } = new Contrato
        {
            Correo = string.Empty,
            Celular = string.Empty
        };


        // Información del contrato
        public int CantidadAnios { get; set; }
        public DateOnly Vencimiento { get; set; }
        public decimal Precio { get; set; }
        public string ModalidadPago { get; set; } = string.Empty;

        // Información del titular
        public string NombreTitular { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;

        // Información general
        public string NumeroCuenta { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;

        // Documentos adjuntos
        public IFormFile? DocumentoAdjunto { get; set; }

        // Fecha actual para control o validación
        public DateOnly FechaActual { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
