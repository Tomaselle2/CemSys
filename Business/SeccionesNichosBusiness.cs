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

        public async Task<List<TipoNumeracionParcela>> ListaTipoNumeracionParcela()
        {
            return await _tipoNumeracionParcela.EmitirListado();
        }

    }
}
