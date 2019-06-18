using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class VendaController : Controller
    {
        // Recebe o contexto HTTP via injeção de dependência
        private IHttpContextAccessor httpContext;

        public VendaController(IHttpContextAccessor HttpContextAccessor)
        {
            httpContext = HttpContextAccessor;
        }
        

        [HttpGet]
        public IActionResult Registrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(VendaModel venda)
        {
            // Captura o id logado no sistema 
            venda.VendedorId = httpContext.HttpContext.Session.GetString("idlogado");

            venda.Inserir();
            CarregarDados();
            return View();
        }

        private void CarregarDados()
        {
            ViewBag.ListaClientes = new VendaModel().RetornaListaClientes();
            ViewBag.ListaVendedores = new VendaModel().RetornaListaVendedores();
            ViewBag.ListaProdutos = new VendaModel().RetornaListaProdutos();
        }

        public IActionResult Index()
        {
            ViewBag.ListaVendas = new VendaModel().ListaVendas();
            return View();
        }
    }
}