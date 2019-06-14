using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using SistemaVendas.Uteis;

namespace SistemaVendas.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login(int? id)
        {
            // realiza logout
            if (id != null)
            {
                if(id == 0)
                {
                    HttpContext.Session.SetString("idlogado", string.Empty);
                    HttpContext.Session.SetString("nomelogado", string.Empty);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                bool loginOk = login.ValidarLogin();

                if (loginOk)
                {
                    HttpContext.Session.SetString("idlogado", login.Id);
                    HttpContext.Session.SetString("nomelogado", login.Nome);
                    return RedirectToAction("Menu", "Home");
                }
                else
                {
                    TempData["ErroLogin"] = "Email e senha inválidos!";
                }
            }

            return View();
        }


        
        public IActionResult Menu()
        {
            return View();
        }
        

        public IActionResult Index()
        {
           return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
