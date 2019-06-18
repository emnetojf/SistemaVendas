using MySql.Data.MySqlClient;
using SistemaVendas.Uteis;
using System.ComponentModel.DataAnnotations;
using System.Data;



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
            string strSQL = $"select id, nome from vendedor where email = @email and senha = @senha";

            MySqlCommand command = new MySqlCommand();
            command.CommandText = strSQL;
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@senha", Senha);

            DAL objdal = new DAL();
            DataTable dt = objdal.RetornaDataTable(command);

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
