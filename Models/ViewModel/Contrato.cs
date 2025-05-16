namespace CemSys.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public string? NombreArchivo { get; set; }
        public string? NumeroCuenta { get; set; } 
        public int TitularId { get; set; }
        public DateOnly Vencimiento { get; set; }
        public int CantidadAnios { get; set; }
        public decimal Precio { get; set; }
        public string ModalidadPago { get; set; } = string.Empty;
        public string DniTitular { get; set; } = string.Empty;
        public DateOnly FechaRegistro { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public required string Correo { get; set; }
        public required string Celular { get; set; }
    }
}

