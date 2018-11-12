using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLWebSiteManager
{
    public class ConexaoExterna : Utils<ConexaoExterna>
    {
        public bool erroTrue = false;
        public int CountConection = 0;
        public string Servidor = string.Empty;
        public string Porta = string.Empty;
        public string Login = string.Empty;
        public string Senha = string.Empty;
        
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get
            {
                if (connection != null)
                {
                    try
                    {
                        connection.Close();
                        connection.Open();
                        CountConection++;
                    }
                    catch (Exception erro)
                    {
                        string connstring = $"Persist Security Info=False; Server={Servidor}; Port={Porta}; database=usyswebc_websites; UID={Login}; password={Senha}; default command timeout=80";
                        connection = new MySqlConnection(connstring);
                        connection.Open();

                        return connection;
                    }

                }
                return connection;
            }
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                string connstring = $"Persist Security Info=False; Server={Servidor}; Port={Porta}; database=usyswebc_websites; UID={Login}; password={Senha}; default command timeout=80";
                connection = new MySqlConnection(connstring);
                connection.Open();
            }

            return connection.State == System.Data.ConnectionState.Open;
        }

        public void ConectionClose()
        {
            connection.Close();
            connection = null;
            this.Dispose();
        }
    }
}
