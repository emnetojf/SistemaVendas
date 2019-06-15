using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaProdutos = new ProdutoModel().ListaProdutos();
            return View();
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.Produto = new ProdutoModel().ListaProduto(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProdutoModel Produto)
        {
            if (ModelState.IsValid)
            {
                Produto.Gravar();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            ViewData["IdExcluir"] = id;
            return View();
        }


        public IActionResult DeleteProduto(int id)
        {
            new ProdutoModel().Delete(id);
            return View();
        }

    }
}