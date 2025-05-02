using CemSys.Interface;
using CemSys.Models;

namespace CemSys.Business
{
    public class DifuntosBusiness
    {
        private readonly IRepositoryDB<Difunto> _difuntosRepository;
        public DifuntosBusiness(IRepositoryDB<Difunto> difuntosbd)
        {
            _difuntosRepository = difuntosbd;
        }

        //registrar difunto
        //listado estado difunto
        //listado secciones nichos
        //listado secciones fosas
        //listado nichos
        //listado fosas
        //registrar acta

    }
}
