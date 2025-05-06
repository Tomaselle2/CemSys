using CemSys.Models;
using CemSys.Models.DTO;
using Microsoft.AspNetCore.Http;


namespace CemSys.Interface.Contratos

{
    public interface IContratosBusiness
    {
        // Registrar un contrato de concesión
        Task<int> RegistrarContrato(Contrato modelo);

        // Consultar datos del titular por DNI
        Task<Titular?> ConsultarTitularPorDni(string dni);

        // Obtener lista de modalidades de pago
        public async Task<List<string>> ObtenerModalidadesPago()
        {
            // Simulando una tarea async (por ejemplo, consulta a DB)
            var modalidades = await Task.Run(() => new List<string> { "Contado", "Crédito", "Débito" });

            return modalidades;
        }


        Task<Contrato?> ConsultarContratoPorCuenta(string numeroCuenta);
        


        public Task<bool> GuardarDocumentoContrato(int contratoId, IFormFile documento)
        {
            // Solo un stub por ahora. Aquí deberías guardar el archivo en disco o base de datos y asociarlo al contrato.
            // Ejemplo: guardar ruta en el modelo y actualizar
            return Task.FromResult(true);
        }

        public Task<byte[]> GenerarPDFContrato(int contratoId)
        {
            // Supuesto: obtener contrato y generar PDF (aquí solo retornamos bytes vacíos como ejemplo)
            return Task.FromResult(new byte[0]);
        }

    }
}

