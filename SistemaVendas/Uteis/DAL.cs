using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace SistemaVendas.Uteis
{
    // Data Access Layer
    public class DAL
    {
        public static string Server = "localhost";
        public static string Database = "sistema_venda";
        public static string User = "developer";
        public static string Password = "123456";
        public static string ConnectionString = $"server={Server}; database={Database}; userid={User}; password={Password};";
        //Sslmode=none; Charset=utf8;                                               
        public static MySqlConnection Connection;

        public DAL()
        {
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
        }

        // Retornando uma seleção
        public DataTable RetornaDataTable(String sql)
        {
            //Cria objeto Datatable
            DataTable data = new DataTable();

            //Cria objeto command
            MySqlCommand command = new MySqlCommand(sql, Connection);

            // Cria objeto data adapter
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(data);

            return data;
        }

        public void ExecutarComandoSQL(String sql)
        {
            //Cria objeto command
            MySqlCommand command = new MySqlCommand(sql, Connection);

            //Executa o command
            command.ExecuteNonQuery();
        }
    }
}
