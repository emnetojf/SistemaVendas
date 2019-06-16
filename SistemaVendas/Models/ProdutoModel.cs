using SistemaVendas.Uteis;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization; 

namespace SistemaVendas.Models
{
    public class ProdutoModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Informe nome produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe descrição produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe preço produto")]
        [DisplayFormat(DataFormatString = "{0:F2}")] // Formato 0.00
        public double Preco { get; set; }

        [Required(ErrorMessage = "Informe quantidade produto")]
        [DisplayFormat(DataFormatString = "{0:F2}")] // Formato 0.00
        public double Quant { get; set; }

        [Required(ErrorMessage = "Informe unidade produto")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "{0} deve conter no mínimo {2} e no máximo {1}!")]
        public string Unid { get; set; }

        [Required(ErrorMessage = "Informe foto produto")]
        public string foto { get; set; }

        public List<ProdutoModel> ListaProdutos()
        {
            List<ProdutoModel> ProdList = new List<ProdutoModel>();
            ProdutoModel Produto;
            DAL objDAL = new DAL();
            string strSQL = "SELECT id, nome, descricao, preco, quant, unidade, foto FROM Produto ORDER BY nome asc";
            DataTable dt = objDAL.RetornaDataTable(strSQL);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Produto = new ProdutoModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Descricao = dt.Rows[i]["descricao"].ToString(),
                    Preco = double.Parse(dt.Rows[i]["preco"].ToString()),
                    Quant = double.Parse(dt.Rows[i]["quant"].ToString()),
                    Unid = dt.Rows[i]["unidade"].ToString(),
                    foto = dt.Rows[i]["foto"].ToString()
                };

                ProdList.Add(Produto);
            }

            return ProdList;
        }


        public ProdutoModel ListaProduto(int? id)
        {
            ProdutoModel Produto;
            DAL objDAL = new DAL();
            string strSQL = $"SELECT id, nome, descricao, preco, quant, unidade, foto FROM Produto WHERE id = '{id}'";
            DataTable dt = objDAL.RetornaDataTable(strSQL);

            Produto = new ProdutoModel
            {
                Id = dt.Rows[0]["Id"].ToString(),
                Nome = dt.Rows[0]["nome"].ToString(),
                Descricao = dt.Rows[0]["descricao"].ToString(),
                Preco = double.Parse(dt.Rows[0]["preco"].ToString()),
                Quant = double.Parse(dt.Rows[0]["quant"].ToString()),                
                Unid = dt.Rows[0]["unidade"].ToString(),
                foto = dt.Rows[0]["foto"].ToString()
            };

            return Produto;
        }



        public void Gravar()
        {
            DAL objDAL = new DAL();

            string strSQL;

            if (Id != null)
            {
                strSQL = $"UPDATE Produto  SET " +
                         $"nome = '{Nome}', " +
                         $"descricao ='{Descricao}', " +
                         $"preco = '{Preco.ToString("F2", CultureInfo.InvariantCulture)}', " +
                         $"quant = '{Quant.ToString("F2", CultureInfo.InvariantCulture)}', " +
                         $"unidade = '{Unid}', " +
                         $"foto = '{foto}' " +
                         $"WHERE id = '{Id}' ";
            }
            else
            {
                strSQL = $"INSERT INTO Produto (nome, descricao, preco, quant, unidade, foto) " +
                         $"values ('{Nome}', '{Descricao}', " +
                         $"'{Preco.ToString("F2", CultureInfo.InvariantCulture)}', '" +
                         $"{Quant.ToString("F2", CultureInfo.InvariantCulture)}', " +
                         $"'{Unid}', '{foto}')";
            }

            objDAL.ExecutarComandoSQL(strSQL);
        }

        public void Delete(int id)
        {
            DAL objDAL = new DAL();

            string strSQL = $"DELETE FROM Produto WHERE id ='{id}'";
            objDAL.ExecutarComandoSQL(strSQL);
        }

    }
}
