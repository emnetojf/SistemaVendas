using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaClientes = new ClienteModel().ListaClientes(); 
            return View();
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.Cliente = new ClienteModel().ListaCliente(id);              
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClienteModel cliente)
        {
            if (!ModelState.IsValid)
            {
                cliente.Gravar();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            ViewData["IdExcluir"] = id;
            return View();
        }


        public IActionResult DeleteCliente(int id)
        {
            new ClienteModel().Delete(id);
            return View();
        }

    }
}