using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQLWebSiteManager
{
    public class ConsultSQL : Utils<ConsultSQL>
    {
        private MySqlConnection mConn;
        //private readonly MySqlDataAdapter mAdapter;
        //private readonly DataSet mDataSet;
        private readonly string ConectionNewBaseExternal = "Persist Security Info=False; Server={0}; Port=3306; database=usyswebc_websites; UID={1}; password={2};default command timeout=780";
        private readonly string ConectionNewBaseINternal = "Persist Security Info=False; Server={0}; Port=3306; database=rtdpj; UID={1}; password={2};default command timeout=780";

        public Consulta ReturnConsult()
        {
            var retorno = new Consulta();

            string CompletPath = Path.GetTempPath() + "\\SQLWEBSITEMANAGER";
            var content = ReadText.GetInstace().LeArquivoTxt(CompletPath + "\\SQLWEBSITE.CONFIG").Split('|');

            mConn = new MySqlConnection(string.Format(ConectionNewBaseExternal, content[3], content[4], content[5]));
           

            MySqlCommand command = new MySqlCommand($"select id,tipo,parametros from {content[6]} where respondida = 0 and respondendo = 0 limit 1", mConn);

            while (retorno.Id == 0)
            {
                mConn.Open();
                //Executa a Query SQL
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retorno.Id = Convert.ToInt32(reader["id"]);
                    retorno.Tipo = Convert.ToInt32(reader["tipo"]);
                    retorno.Parametros = reader["parametros"].ToString();
                }

                mConn.Close();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Thread.Sleep(200);
            }

            if (retorno.Id != 0)
            {
                mConn.Open();
                command = new MySqlCommand($"update {content[6]} set respondendo = 1 where id  = {retorno.Id}", mConn);
                command.ExecuteNonQuery();
                mConn.Close();
            }

            return retorno;
        }

        public Consulta RespostaDeProtocolos(Consulta pconsult)
        {
            var retorno = new Consulta();

            string CompletPath = Path.GetTempPath() + "\\SQLWEBSITEMANAGER";
            var content = ReadText.GetInstace().LeArquivoTxt(CompletPath + "\\SQLWEBSITE.CONFIG").Split('|');

            mConn = new MySqlConnection(string.Format(ConectionNewBaseINternal, content[0], content[1], content[2]));
            mConn.Open();

            MySqlCommand command = new MySqlCommand(string.Format("select descri_pcs from pedido left join pedido_certidao on codped_pec = codigo_ped left join pedido_certidao_situacao on codigo_pcs = codsit_pec where numpro_ped = {0} limit 1",
                pconsult.Parametros), mConn);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                retorno.Resposta = reader["descri_pcs"].ToString();
            }

            mConn.Close();


            mConn = new MySqlConnection(string.Format(ConectionNewBaseExternal, content[3], content[4], content[5]));
            mConn.Open();

           command = new MySqlCommand($"update {content[6]} set resposta = '{retorno.Resposta}',respondida = 1 where id  = { pconsult.Id}", mConn);

            command.ExecuteNonQuery();

            mConn.Close();

            return retorno;
        }
    }
}
