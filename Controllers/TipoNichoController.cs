﻿using CemSys.Interface;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CemSys.Controllers
{
    public class TipoNichoController : Controller
    {
        private readonly IRepositoryBusiness<TipoNicho> _serviceBusiness;
        public TipoNichoController(IRepositoryBusiness<TipoNicho> tipoNichoBusiness)
        {
            _serviceBusiness = tipoNichoBusiness;
        }

        ABMRepositoryVM<TipoNicho> viewModel = new ABMRepositoryVM<TipoNicho> ();

        public async Task<IActionResult> Index(ABMRepositoryVM<TipoNicho> viewModel)
        {
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;

            if (viewModel.Lista.Count == 0)
            {
                viewModel.Lista = await EmitirListado();
                viewModel.EsModificacion = false;
                
            }
            return View(viewModel);
        }

        private async Task<List<TipoNicho>> EmitirListado()
        {
            List<TipoNicho> lista = new List<TipoNicho>();
            try
            {
                lista = await _serviceBusiness.EmitirListado();
                ViewData["MensajeEmitir"] = "Exito al emitir listado";
            }
            catch (Exception ex)
            {
                ViewData["MensajeEmitir"] = ex.Message;
            }

            return lista;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(string tipo) {
            int resultado = 0;

            TipoNicho modelo = new TipoNicho();
            modelo.TipoNicho1 = tipo;

            try
            {
                resultado = await _serviceBusiness.Registrar(modelo);
                ViewData["MensajeRegistrar"] = "Exito al registrar";
            }
            catch (Exception ex)
            {
                ViewData["MensajeRegistrar"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
               await _serviceBusiness.Eliminar(id);
                ViewData["MensajeEliminar"] = "Exito al eliminar";

            }
            catch (Exception ex)
            {
                ViewData["MensajeEliminar"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Modificar(string tipo, int id)
        {
            int resultado = 0;
            TipoNicho modelo = await _serviceBusiness.Consultar(id);
            modelo.TipoNicho1 = tipo;
            

            try
            {
               resultado = await _serviceBusiness.Modificar(modelo);
               ViewData["MensajeModificar"] = "Modificado con éxito";

            }
            catch (Exception ex)
            {
                ViewData["MensajeModificar"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {
            TipoNicho modelo = await _serviceBusiness.Consultar(id);
            viewModel.Modelo = modelo;
            viewModel.EsModificacion = true;
            viewModel.Lista = await EmitirListado();

            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EstablecerPredeterminado(int idTipoPredeterminado)
        {
            // 1. Quitar el predeterminado actual
            var tipos = await _serviceBusiness.EmitirListado();
            var predeterminadoNuevo = await _serviceBusiness.Consultar(idTipoPredeterminado);
            foreach (var tipo in tipos)
            {
                if(tipo.PorDefecto == true)
                {
                    tipo.PorDefecto = false;
                    await _serviceBusiness.Modificar(tipo);
                    predeterminadoNuevo.PorDefecto = true;
                    await _serviceBusiness.Modificar(predeterminadoNuevo);
                }
                else
                {
                    predeterminadoNuevo.PorDefecto = true;
                    await _serviceBusiness.Modificar(predeterminadoNuevo);
                }
            }

            return RedirectToAction("Index");
        }

    }
}
