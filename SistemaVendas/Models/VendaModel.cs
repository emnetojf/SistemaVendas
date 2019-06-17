using Newtonsoft.Json;
using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;

namespace SistemaVendas.Models
{
    public class VendaModel
    {
        public string Id { get; set; }
        public string DataVenda { get; set; }

        public string ClienteId { get; set; }
        public string VendedorId { get; set; }

        public string ListaProdutos { get; set; }

        

        [DisplayFormat(DataFormatString = "{0:F2}")] // Formato 0.00
        public double TotalVend { get; set; }

        public List<ClienteModel> RetornaListaClientes()
        {
            return new ClienteModel().ListaClientes();
        }

        public List<VendedorModel> RetornaListaVendedores()
        {
            return new VendedorModel().ListaVendedors();
        }

        public List<ProdutoModel> RetornaListaProdutos()
        {
            return new ProdutoModel().ListaProdutos();
        }


        public List<VendaModel> ListaVendas ()
        {
            List<VendaModel> VendaList = new List<VendaModel>();
            VendaModel Venda;
            DAL objDAL = new DAL();
            string strSQL = "select vendas.id, vendas.data, vendas.total, v.nome as vendedor, c.nome as cliente " +
                            "from vendas " +
                            "inner join cliente c on c.id = vendas.Cliente_id " +
                            "inner join vendedor v on v.id = vendas.Vendedor_id " +
                            "order by vendas.data, vendas.total ";

            DataTable dt = objDAL.RetornaDataTable(strSQL);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Venda = new VendaModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    DataVenda = dt.Rows[i]["data"].ToString(),
                    TotalVend = double.Parse(dt.Rows[i]["total"].ToString()),
                    VendedorId = dt.Rows[i]["vendedor"].ToString(),
                    ClienteId = dt.Rows[i]["cliente"].ToString()
                };

                VendaList.Add(Venda);
            }

            return VendaList;
        }


        // lista Vendas periodo
        public List<VendaModel> ListaVendasPeriodo(string DataInicio, string DataFim)
        {
            List<VendaModel> VendaList = new List<VendaModel>();
            VendaModel Venda;
            DAL objDAL = new DAL();
            string strSQL = "select vendas.id, vendas.data, vendas.total, v.nome as vendedor, c.nome as cliente " +
                            "from vendas " +
                            "inner join cliente c on c.id = vendas.Cliente_id " +
                            "inner join vendedor v on v.id = vendas.Vendedor_id " +
                            $"where vendas.data >= '{DataInicio}' and vendas.data <= '{DataFim}' " + 
                            "order by vendas.data, vendas.total ";

            DataTable dt = objDAL.RetornaDataTable(strSQL);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Venda = new VendaModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    DataVenda = dt.Rows[i]["data"].ToString(),
                    TotalVend = double.Parse(dt.Rows[i]["total"].ToString()),
                    VendedorId = dt.Rows[i]["vendedor"].ToString(),
                    ClienteId = dt.Rows[i]["cliente"].ToString()
                };

                VendaList.Add(Venda);
            }

            return VendaList;
        }





        public void Inserir()
        {
            DAL objdal = new DAL();

            string strData = DateTime.Now.Date.ToString("yyyy/MM/dd"); 

            string strSQL = "INSERT INTO vendas (data, total, Vendedor_id, Cliente_id)" +
                            $"VALUES ('{strData}','{TotalVend.ToString("F2", CultureInfo.InvariantCulture)}','{VendedorId}','{ClienteId}')";

            objdal.ExecutarComandoSQL(strSQL);

            // Recupera o id da venda
            strSQL = $"SELECT id FROM vendas WHERE data = '{strData}' and Vendedor_id = '{VendedorId}' and Cliente_id = '{ClienteId}' order by id desc limit 1"; 
            DataTable dt = objdal.RetornaDataTable(strSQL);
            string IdVend = dt.Rows[0]["id"].ToString();

            // Deserializar Json da lista produtos e grava-los
            List<ItemVendaModel> ListItemVendas = JsonConvert.DeserializeObject<List<ItemVendaModel>>(ListaProdutos);

            for (int i = 0; i < ListItemVendas.Count; i++)
            {
                strSQL = "INSERT INTO itemvenda (Produto_id, Vendas_id, quant, precoprod) " +
                        $"VALUES({ ListItemVendas[i].CodProd.ToString() }, " +
                        $"{ IdVend }, " +
                        $"{ ListItemVendas[i].QuantProd.ToString() }, " +
                        $"{ ListItemVendas[i].Preco.ToString().Replace(",", ".") }) ";

                objdal.ExecutarComandoSQL(strSQL);
            }
        }

    }
}
