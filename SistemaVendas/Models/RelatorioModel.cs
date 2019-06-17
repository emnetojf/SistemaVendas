using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Data;

namespace SistemaVendas.Models
{
    public class RelatorioModel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }

    public class GraficoProd
    {
        public double QtdeVendido { get; set; }
        public string DescrProd { get; set; }

        public List<GraficoProd> RetornaGrafico()
        {
            DAL objDAL = new DAL();
            string strSQL = "SELECT p.nome as Produto, sum(i.quant) as Quant  " +
                            "FROM itemvenda i" +
                            " INNER JOIN produto p on p.id = i.Produto_id" +
                            "  group by p.nome";
            DataTable dt = objDAL.RetornaDataTable(strSQL);

            List<GraficoProd> ListGrafProd = new List<GraficoProd>();
            GraficoProd item;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new GraficoProd();

                item.DescrProd = dt.Rows[i]["Produto"].ToString();
                item.QtdeVendido = (double.Parse(dt.Rows[i]["Quant"].ToString()) * 1.0);
                
                ListGrafProd.Add(item);
            }

            return ListGrafProd;
        }
    }
}