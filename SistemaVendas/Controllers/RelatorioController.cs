using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasPeriodo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VendasPeriodo(RelatorioModel relatorio)
        {
            if (relatorio.DataInicio.Year == 1)
            {
                ViewBag.ListaVendas = new VendaModel().ListaVendas();
            }
            else
            {
                string DtInicio = relatorio.DataInicio.ToString("yyyy/MM/dd");
                string DtFim = relatorio.DataFim.ToString("yyyy/MM/dd");

                ViewBag.ListaVendas = new VendaModel().ListaVendasPeriodo(DtInicio, DtFim);

            }

            return View();
        }

        public IActionResult GraficoVendas()
        {
            List<GraficoProd> listaGrafProd = new GraficoProd().RetornaGrafico();

            string strValores = "";
            string strLabels = "";
            string strCores = "";

            var Aleatorio = new Random();

            for (int i = 0; i < listaGrafProd.Count; i++)
            {
                strValores += listaGrafProd[i].QtdeVendido.ToString() + ",";
                strLabels += "'" + listaGrafProd[i].DescrProd.ToString() + "',";

                // Escolhe as cores de forma Aleatório
                strCores += "'" + string.Format("#{0:x6}", Aleatorio.Next(0x1000000)) +"',";
            }

            ViewBag.Valores = strValores;
            ViewBag.Labels = strLabels;
            ViewBag.Cores = strCores;

            return View();
        }


        public IActionResult ComissaoVendedor()
        {
            return View();
        }
    }
}