using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class VendaController : Controller
    {
        [HttpGet]
        public IActionResult Registrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(VendaModel venda)
        {
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