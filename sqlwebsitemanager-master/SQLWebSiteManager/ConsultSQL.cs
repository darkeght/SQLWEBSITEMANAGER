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
            string CompletPath = Assembly.GetExecutingAssembly().Location;
            var vcontent = ReadText.GetInstace().LeArquivoTxt(CompletPath.Substring(0, CompletPath.LastIndexOf("\\") + 1) + "\\SQLWEBSITE.CONFIG").Split('|');
            content = new String[9];

            //  informações locais
            ConexaoInterna.GetInstace().Servidor = vcontent[0]; // endereço do banco
            ConexaoInterna.GetInstace().Login    = vcontent[1]; // usuario
            ConexaoInterna.GetInstace().Senha    = vcontent[2]; // senha
            ConexaoInterna.GetInstace().Porta    = vcontent[3]; // porta

            ConexaoInterna.GetInstace().IsConnect();

            //  Informações externas
            ConexaoExterna.GetInstace().Servidor = vcontent[4]; // endereço do banco
            ConexaoExterna.GetInstace().Login    = vcontent[5]; // usuario
            ConexaoExterna.GetInstace().Senha    = vcontent[6]; // senha
            ConexaoExterna.GetInstace().Porta    = vcontent[7]; // porta
            content[8]                           = vcontent[8]; // tabela
            
            ConexaoExterna.GetInstace().IsConnect();
        }


        public Consulta ReturnConsult()
        {
            Consulta retorno = new Consulta();

            using (var cmd = new MySqlCommand($"select id, tipo, parametros from {content[8]} where respondida = 0 and respondendo = 0 limit 1", ConexaoExterna.GetInstace().Connection))
            {
                while (retorno.Id == 0)
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        retorno.Id = Convert.ToInt32(reader["id"]);
                        retorno.Tipo = Convert.ToInt32(reader["tipo"]);
                        retorno.Parametros = reader["parametros"].ToString();
                    }

                    Thread.Sleep(200);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
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
                    retorno.Resposta = reader["descricao"].ToString();
                }
            }

            using (var cmd = new MySqlCommand($"update {content[8]} set resposta = '{retorno.Resposta}', respondida = 1 where id  = {pconsult.Id}", ConexaoExterna.GetInstace().Connection))
            {
                cmd.ExecuteNonQuery();
            }

            return retorno;
        }

        internal void RespostaDeConsultaCertidaoCasamento(Consulta consult)
        {
            /*
             * select
             * <informações que quer retornar para a tela>
             * from habilitacao 
             * left join pessoafr ivo on ivo.codigo_pfr = ivopef_hab 
             * left join pessoafr iva on iva.codigo_pfr = ivapef_hab
             * where (ivo.nom_pfr = 'nome informado em tela' or iva.nom_pfr = 'nome informado em tela');
             * OU
             * where (ivo.cpf_pfr = 'cpf informado em tela' or iva.cpf_pfr = 'cpf informado em tela');
             */
            throw new NotImplementedException();
        }

        internal void RespostaDeConsultaCertidaoNascimento(Consulta consult)
        {
            /*
             * select
             * <informações que quer retornar para a tela>
             * from nascimento
             * left join 
             * where nom_nas like 'nome do nascido informado em tela';
             * ou 
             * where cpf_nas like 'cpf do nascido informado em tela';
             */
            throw new NotImplementedException();
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

        internal void RespostaDeConsultaCertidaoObito(Consulta consult)
        {
            /*
            *select
            *< informações que quer retornar para a tela>
            *from obito
            *left join pessoafr on codigo_pfr = codpef_obi
            *where nom_pfr = 'nome informado em tela';
            * OU
            * cpf_pfr = 'CPF informado em tela'
            */

            throw new NotImplementedException();
        }

        internal void RespostaDeConsultaCertidaoEscritura(Consulta consult)
        {
            /*
             *select
             * <informações que quer retornar para a tela>
             * from lavratura_pessoa 
             * left join pessoafr on codigo_pfr = codpef_lpe
             * left join lavratura on codigo_lav = codlav_lpe
             * where
             * nom_pfr = 'nome informado em tela'
             * OU
             * cpf_pfr = 'CPF informado em tela'
             */
            throw new NotImplementedException();
        }
    }
}
