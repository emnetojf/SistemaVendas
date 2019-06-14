using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using SistemaVendas.Uteis;
using System.ComponentModel.DataAnnotations;



namespace SistemaVendas.Models
{
    public class LoginModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe email usuário")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe senha usuário")]
        public string Senha { get; set; }

        public bool ValidarLogin()
        {
            string strSQL = $"select id, nome from Vendedor where email = '{Email}' and senha = '{Senha}'";

            DAL objdal = new DAL();
            DataTable dt = objdal.RetornaDataTable(strSQL);

            if (dt.Rows.Count == 1)
            {
                Id = dt.Rows[0]["id"].ToString();
                Nome = dt.Rows[0]["nome"].ToString();

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
