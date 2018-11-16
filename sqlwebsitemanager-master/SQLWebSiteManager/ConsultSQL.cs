using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;

namespace SQLWebSiteManager
{
    public class ConsultSQL : Utils<ConsultSQL>
    {
        private string[] content = null;

        public ConsultSQL()
        {
            string CompletPath = Assembly.GetExecutingAssembly().Location.Replace("\\SQLWebSiteManager.exe", "");
            var vcontent = ReadText.GetInstace().LeArquivoTxt(CompletPath + "\\SQLWEBSITEMANAGERCONFIG\\SQLWEBSITE.CONFIG").Split('|');
            content = new String[9];

            //  informações locais
            ConexaoInterna.GetInstace().Servidor = vcontent[0]; // endereço do banco
            ConexaoInterna.GetInstace().Login    = vcontent[1]; // usuario
            ConexaoInterna.GetInstace().Senha    = vcontent[2]; // senha
            ConexaoInterna.GetInstace().Porta    = vcontent[7]; // porta

            ConexaoInterna.GetInstace().IsConnect();

            //  Informações externas
            ConexaoExterna.GetInstace().Servidor = vcontent[3]; // endereço do banco
            ConexaoExterna.GetInstace().Login    = vcontent[4]; // usuario
            ConexaoExterna.GetInstace().Senha    = vcontent[5]; // senha
            ConexaoExterna.GetInstace().Porta    = vcontent[8]; // porta
            content[8]                           = vcontent[6]; // tabela
            
            ConexaoExterna.GetInstace().IsConnect();
        }


        public Consulta ReturnConsult()
        {
            Consulta retorno = new Consulta();

            while (retorno.Id == 0)
            {
                try
                {
                    using (var cmd = new MySqlCommand($"select id, tipo, parametros from {content[8]} where respondida = 0 and respondendo = 0 limit 1", ConexaoExterna.GetInstace().Connection))
                    {
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            retorno.Id = Convert.ToInt32(reader["id"]);
                            retorno.Tipo = Convert.ToInt32(reader["tipo"]);
                            retorno.Parametros = reader["parametros"].ToString();
                        }

                        Thread.Sleep(500);

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
                catch
                {
                    //erro continua ate conectar novamente
                }
            }

            using (var cmd = new MySqlCommand($"update {content[8]} set respondendo = 1 where id  = {retorno.Id}", ConexaoExterna.GetInstace().Connection))
            {
                cmd.ExecuteNonQuery();
            }

            return retorno;
        }
        
        public Consulta RespostaDeConsultaPedidoCertidao(Consulta pconsult)
        {
            Consulta retorno = new Consulta();

            using (var cmd = new MySqlCommand(@"select
                                                case
                                                when status_ped = 1 Then 'Aguardando emissão'
                                                When status_ped = 2 Then 'Aguardando retirada'
                                                When status_ped = 3 Then 'Aguardando solução'
                                                When status_ped = 4 Then 'Concluído'
                                                end as status_pedido
                                                from pedido where numpro_ped = @parametro", ConexaoInterna.GetInstace().Connection))
            {
                cmd.Parameters.AddWithValue("@parametro", pconsult.Parametros);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    retorno.Resposta = reader["status_pedido"].ToString();
                }
            }

            using (var cmd = new MySqlCommand($"update {content[8]} set resposta = '{retorno.Resposta}', respondida = 1 where id  = {pconsult.Id}", ConexaoExterna.GetInstace().Connection))
            {
                cmd.ExecuteNonQuery();
            }

            return retorno;
        }

        internal Consulta RespostaDeConsultaCertidaoCasamento(Consulta consult)
        {
            var parametros = consult.Parametros.Split(';');
            var condicoes = "";


            if (parametros[0].ToString() == "1")
                condicoes = "where (ivo.nom_pfr = @parametros1 or iva.nom_pfr = @parametros1)";
            else
                condicoes = "where (ivo.cpf_pfr = @parametros1 or iva.cpf_pfr = @parametros1)";

            Consulta retorno = new Consulta();

            if (parametros.Length > 1)
            using (var cmd = new MySqlCommand(@"select
cs.livro_cas,cs.folha_cas
from habilitacao hb
left join pessoafr ivo on ivo.codigo_pfr = hb.ivopef_hab
left join pessoafr iva on iva.codigo_pfr = hb.ivapef_hab
left join casamento  cs on cs.codhab_cas = hb.codigo_hab " + condicoes, ConexaoInterna.GetInstace().Connection))
            {
                cmd.Parameters.AddWithValue("@parametros1", parametros[1].ToString());
                    //cmd.Parameters.AddWithValue("@parametro2", parametros[2].ToString());

                    var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    retorno.Resposta = reader["livro_cas"].ToString() + ";" + reader["folha_cas"].ToString();
                     break;
                }
            }

            using (var cmd = new MySqlCommand($"update {content[8]} set resposta = '{retorno.Resposta}', respondida = 1 where id  = {consult.Id}", ConexaoExterna.GetInstace().Connection))
            {
                cmd.ExecuteNonQuery();
            }

            return retorno;
        }

        internal Consulta RespostaDeConsultaCertidaoNascimento(Consulta consult)
        {
            var parametros = consult.Parametros.Split(';');
            var condicoes = "";


            if (parametros[0].ToString() == "1")
                condicoes = "where nom_nas = @parametro";
            else
                condicoes = "where cpf_nas = @parametro";

            Consulta retorno = new Consulta();

            if (parametros.Length > 1)
                using (var cmd = new MySqlCommand(@"
select
nascimento.livro_nas,nascimento.folha_nas
from nascimento " + condicoes, ConexaoInterna.GetInstace().Connection))
                {
                    cmd.Parameters.AddWithValue("@parametro", parametros[1].ToString());

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        retorno.Resposta = reader["livro_nas"].ToString() + ";" + reader["folha_nas"].ToString();
                    }
                }

            using (var cmd = new MySqlCommand($"update {content[8]} set resposta = '{retorno.Resposta}', respondida = 1 where id  = {consult.Id}", ConexaoExterna.GetInstace().Connection))
            {
                cmd.ExecuteNonQuery();
            }

            return retorno;
        }

        internal void RespostaDeConsultaCartaoAssinatura(Consulta consult)
        {
            /*
            * select
            * < informações que quer retornar para a tela>
            * from signatario
            * where
            * #se for por nome
            * nom_sig = 'nome informado em tela'
            * #se for por documento CPF
            * cpf_sig = 'CPF informado em tela'
            */
            throw new NotImplementedException();
        }

        internal Consulta RespostaDeConsultaCertidaoObito(Consulta consult)
        {
            var parametros = consult.Parametros.Split(';');
            var condicoes = "";

            if (parametros[0].ToString() == "1")
                condicoes = "where nom_pfr = @parametro";
            else
                condicoes = "where cpf_pfr = @parametro";

            Consulta retorno = new Consulta();

            if (parametros.Length > 1)
                using (var cmd = new MySqlCommand(@"
select
obito.livro_obi,obito.folha_obi
from obito
left join pessoafr on codigo_pfr = codpef_obi " + condicoes, ConexaoInterna.GetInstace().Connection))
                {
                    cmd.Parameters.AddWithValue("@parametro", parametros[1].ToString());

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        retorno.Resposta = reader["livro_obi"].ToString() + ";" + reader["folha_obi"].ToString();
                    }
                }

            using (var cmd = new MySqlCommand($"update {content[8]} set resposta = '{retorno.Resposta}', respondida = 1 where id  = {consult.Id}", ConexaoExterna.GetInstace().Connection))
            {
                cmd.ExecuteNonQuery();
            }

            return retorno;
        }

        internal Consulta RespostaDeConsultaCertidaoEscritura(Consulta consult)
        {
            var parametros = consult.Parametros.Split(';');
            var condicoes = "";

            if (parametros[0].ToString() == "1")
                condicoes = "where nom_pfr = @parametro1 and tiplav_lav = @parametro";
            else
                condicoes = "where cpf_pfr = @parametro1 and tiplav_lav = @parametro";

            Consulta retorno = new Consulta();

            if (parametros.Length > 2)
                using (var cmd = new MySqlCommand(@"
select
lavratura.livro_lav,lavratura.folha_lav
from lavratura_pessoa 
left join pessoafr on codigo_pfr = codpef_lpe
left join lavratura on codigo_lav = codlav_lpe
" + condicoes, ConexaoInterna.GetInstace().Connection))
                {
                    cmd.Parameters.AddWithValue("@parametro1", parametros[2].ToString());
                    cmd.Parameters.AddWithValue("@parametro", parametros[1].ToString());

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        retorno.Resposta = reader["livro_lav"].ToString() + ";" + reader["folha_lav"].ToString();
                    }
                }

            using (var cmd = new MySqlCommand($"update {content[8]} set resposta = '{retorno.Resposta}', respondida = 1 where id  = {consult.Id}", ConexaoExterna.GetInstace().Connection))
            {
                cmd.ExecuteNonQuery();
            }

            return retorno;
        }
    }
}
