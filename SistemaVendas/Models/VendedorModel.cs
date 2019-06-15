using System;
using System.Collections.Generic;
using System.Data;
using SistemaVendas.Uteis;
using System.ComponentModel.DataAnnotations;

namespace SistemaVendas.Models
{
    public class VendedorModel
    {
        public string Id { get; set; }

       [Required(ErrorMessage = "Informe nome usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe email Vendedor")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe senha usuário")]
        public string Senha { get; set; }

        public List<VendedorModel> ListaVendedors()
        {
            List<VendedorModel> VendList = new List<VendedorModel>();
            VendedorModel Vendedor;
            DAL objDAL = new DAL();
            string strSQL = "SELECT id, nome, email, senha FROM Vendedor ORDER BY nome asc";
            DataTable dt = objDAL.RetornaDataTable(strSQL);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Vendedor = new VendedorModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Email = dt.Rows[i]["email"].ToString(),
                    Senha = dt.Rows[i]["senha"].ToString(),
                };

                VendList.Add(Vendedor);
            }

            return VendList;
        }


        public VendedorModel ListaVendedor(int? id)
        {
            VendedorModel Vendedor;
            DAL objDAL = new DAL();
            string strSQL = $"SELECT id, nome, email, senha FROM Vendedor WHERE id = '{id}'";
            DataTable dt = objDAL.RetornaDataTable(strSQL);

            Vendedor = new VendedorModel
            {
                Id = dt.Rows[0]["Id"].ToString(),
                Nome = dt.Rows[0]["nome"].ToString(),
                Email = dt.Rows[0]["email"].ToString(),
                Senha = dt.Rows[0]["senha"].ToString(),
            };

            return Vendedor;
        }



        public void Gravar()
        {
            DAL objDAL = new DAL();

            string strSQL;

            if (Id != null)
            {
                strSQL = $"UPDATE Vendedor  SET nome = '{Nome}', email = '{Email}' WHERE id = '{Id}' ";
            }
            else
            {
                strSQL = $"INSERT INTO Vendedor (nome, email, senha) values ('{Nome}', '{Email}', '123456')";
            }

            objDAL.ExecutarComandoSQL(strSQL);
        }

        public void Delete(int id)
        {
            DAL objDAL = new DAL();

            string strSQL = $"DELETE FROM Vendedor WHERE id ='{id}'";
            objDAL.ExecutarComandoSQL(strSQL);
        }

    }
}
