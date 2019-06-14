using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class VendedorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaVendedors = new VendedorModel().ListaVendedors();
            return View();
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.Vendedor = new VendedorModel().ListaVendedor(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(VendedorModel Vendedor)
        {
            if (!ModelState.IsValid)
            {
                Vendedor.Gravar();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            ViewData["IdExcluir"] = id;
            return View();
        }


        public IActionResult DeleteVendedor(int id)
        {
            new VendedorModel().Delete(id);
            return View();
        }

    }
}