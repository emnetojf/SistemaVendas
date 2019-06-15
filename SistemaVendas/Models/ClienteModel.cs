using SistemaVendas.Uteis;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SistemaVendas.Models
{
    public class ClienteModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Informe CPF usuário")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe nome usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe email cliente")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe senha usuário")]
        public string Senha { get; set; }

        public List<ClienteModel> ListaClientes()
        {
            List<ClienteModel> CliList = new List<ClienteModel>();
            ClienteModel cliente;
            DAL objDAL = new DAL();
            string strSQL = "SELECT id, CPF, nome, email, senha FROM cliente ORDER BY nome asc";
            DataTable dt = objDAL.RetornaDataTable(strSQL);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cliente = new ClienteModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    CPF = dt.Rows[i]["CPF"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Email = dt.Rows[i]["email"].ToString(),
                    Senha = dt.Rows[i]["senha"].ToString(),
                };

                CliList.Add(cliente);
            }

            return CliList;
        }


        public ClienteModel ListaCliente(int? id)
        {
            ClienteModel cliente;
            DAL objDAL = new DAL();
            string strSQL = $"SELECT id, CPF, nome, email, senha FROM cliente WHERE id = '{id}'";
            DataTable dt = objDAL.RetornaDataTable(strSQL);

            cliente = new ClienteModel
            {
                Id = dt.Rows[0]["Id"].ToString(),
                CPF = dt.Rows[0]["CPF"].ToString(),
                Nome = dt.Rows[0]["nome"].ToString(),
                Email = dt.Rows[0]["email"].ToString(),
                Senha = dt.Rows[0]["senha"].ToString(),
            };

            return cliente;
        }



        public void Gravar()
        {
            DAL objDAL = new DAL();

            string strSQL;

            if (Id != null)
            {
                strSQL = $"UPDATE cliente  SET nome = '{Nome}', CPF = '{CPF}', email = '{Email}' WHERE id = '{Id}' ";
            }
            else
            {
                strSQL = $"INSERT INTO cliente (nome, CPF, email, senha) values ('{Nome}', '{CPF}', '{Email}', '123456')";
            }

            objDAL.ExecutarComandoSQL(strSQL);
        }

        public void Delete(int id)
        {
            DAL objDAL = new DAL();
            
            string strSQL = $"DELETE FROM cliente WHERE id ='{id}'";
            objDAL.ExecutarComandoSQL(strSQL);
        }

    }
}
