using System.Diagnostics.Contracts;
using CemSys.Interface;
using CemSys.Interface.Contratos;
using CemSys.Models;
using CemSys.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;




namespace CemSys.Business
{
    public class ContratosBusiness : IContratosBusiness
    {
        private readonly IRepositoryDB<Contrato> _contratoRepository;
        private readonly IRepositoryDB<Titular> _titularRepository;
        private readonly IRepositoryDB<ModalidadPago> _modalidadPagoRepository;
        public ContratosBusiness(
            IRepositoryDB<Contrato> contratoRepository,
            IRepositoryDB<Titular> titularRepository,
            IRepositoryDB<ModalidadPago> modalidadPagoRepository)
        {
            _contratoRepository = contratoRepository;
            _titularRepository = titularRepository;
            _modalidadPagoRepository = modalidadPagoRepository;
        }

        public async Task<List<DTO_modalidadPago>> ObtenerModalidadesPago()
        {
            try
            {
                return (await _modalidadPagoRepository.EmitirListado()).Where(x => x.Visibilidad == true)
                    .Select(x => new DTO_modalidadPago
                    {
                        Id = x.IdModalidad,
                        Nombre = x.Nombre
                    }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Titular?> ConsultarTitularPorDni(string dni)
        {
            try
            {
                var titulares = await _titularRepository.EmitirListado();
                return titulares.FirstOrDefault(t => t.Dni == dni);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> RegistrarContrato(Contrato modelo)
        {
            try
            {
                return await _contratoRepository.Registrar(modelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> RegistrarTitular(Titular modelo)
        {
            try
            {
                return await _titularRepository.Registrar(modelo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Contrato?> ConsultarContratoPorCuenta(string numeroCuenta)
        {
            return await _contratoRepository.ObtenerUnoAsync(c => c.NumeroCuenta == numeroCuenta);

        }


        public async Task<int> GuardarDocumentoContrato(int contratoId, IFormFile archivo)
        {
            try
            {
                // Lógica de guardado físico del archivo o almacenamiento binario
                // Esto depende del diseño que tengas (por ejemplo, guardar path o byte[] en BD)
                // Ejemplo ficticio:
                var contrato = await _contratoRepository.Consultar(contratoId);
                contrato.NombreArchivo = archivo.FileName;
                return await _contratoRepository.Modificar(contrato);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Más métodos si es necesario (por ejemplo: AnularContrato, EmitirListadoContratos, etc.)
    }
}
