using CemSys.Interface;
using CemSys.Interface.SeccionesNichos;
using CemSys.Models;

namespace CemSys.Business
{
    public class SeccionesNichosBusiness : ISeccionesNichosBusiness
    {
        private readonly IRepositoryDB<TipoNumeracionParcela> _tipoNumeracionParcela;

        public SeccionesNichosBusiness(IRepositoryDB<TipoNumeracionParcela> tipoNumeracionParcela)
        {
            _tipoNumeracionParcela = tipoNumeracionParcela;
        }
        public Task CrearNichosNumeracionAntigua()
        {
            throw new NotImplementedException();
        }

        public Task CrearNichosNumeracionNueva()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TipoNumeracionParcela>> ListaTipoNumeracionParcela()
        {
            return await _tipoNumeracionParcela.EmitirListado();
        }
    }
}
