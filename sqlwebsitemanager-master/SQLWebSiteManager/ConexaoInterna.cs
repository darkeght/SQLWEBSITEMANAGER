using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLWebSiteManager
{
    public class ConexaoInterna : Utils<ConexaoInterna>
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
                        string connstring = $"Server={Servidor}; Port={Porta}; database=rtdpj; UID={Login}; password={Senha};SslMode=none;default command timeout=80";
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
                string connstring = $"Server={Servidor}; Port={Porta}; database=rtdpj; UID={Login}; password={Senha};SslMode=none;default command timeout=80";
                connection = new MySqlConnection(connstring);
                connection.Open();
            }

            return true;
        }

        public void ConectionClose()
        {
            connection.Close();
            connection = null;
            this.Dispose();
        }
    }
}
