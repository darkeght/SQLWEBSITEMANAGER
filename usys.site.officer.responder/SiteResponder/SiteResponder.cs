using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace SiteResponder
{
    public partial class SiteResponder : ServiceBase
    {
        /// <summary>
        /// Chamada inicial da aplicação, main
        /// </summary>
        /// <returns></returns>
        public SiteResponder()
        {
            InitializeComponent();
            string logEntrada = string.Empty;

            eventLog = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("Serviço de resposta de site"))
            {
                System.Diagnostics.EventLog.CreateEventSource("Serviço de resposta de site", "SiteResponderLog");
            }

            eventLog.Source = "Serviço de resposta de site";
            eventLog.Log = "SiteResponderLog";
            
            //  Configura o timer para rodar a cada minuto
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = (3 * 1000); // 5 segundos
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        /// <summary>
        /// Executado quando o serviço é iniciado
        /// </summary>
        /// <returns></returns>
        protected override void OnStart(string[] args)
        {
            // Atualiza o status do serviço para - Início em andamento
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog.WriteEntry("Aplicação de serviço de resposta de site iniciada.");

            // Atualiza o status do serviço para - Em execução
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        /// <summary>
        /// Executado quando o serviço é parado
        /// </summary>
        /// <returns></returns>
        protected override void OnStop()
        {
            // Atualiza o status do serviço para - parada pendente
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog.WriteEntry("Aplicação de serviço de resposta de site parada.");

            // Atualiza o serviço para - parado
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        /// <summary>
        /// Executado quando a execução do serviço é retomada
        /// </summary>
        /// <returns></returns>
        protected override void OnContinue()
        {
            //  Cria um log na pasta da aplicação, informando que ela foi iniciada novamente
            eventLog.WriteEntry("Aplicação de serviço de resposta de site retomada.");
        }

        /// <summary>
        /// Executado quando o timer inicial é disparado
        /// </summary>
        /// <returns></returns>
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            try
            {
                // Insert monitoring activities here.  
                eventLog.WriteEntry("Lendo requisições.", System.Diagnostics.EventLogEntryType.Information, eventId++);

                using (ConsultSQL consultsql = ConsultSQL.GetInstace())
                {
                    eventLog.WriteEntry("Iniciando consulta de perguntas para a base de dados \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);

                    Consulta consult = consultsql.ReturnConsult();

                    //  se achou consulta
                    if(consult.Id > 0)
                    {
                        eventLog.WriteEntry("Pergunta encontrada iniciando tentativa de responder \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);

                        /*
                         * 1 - consulta de pedido de certidão
                         * 2 - consulta de certidao de escritura
                         * 3 - consulta de cartao de assinatura
                         * 4 - consulta de certidao de nascimento
                         * 5 - consulta de certidao de casamento
                         * 6 - consulta de certidão de óbito
                         * 
                         * 100 - inclusão de pedido de certidão
                        */
                        switch (consult.Tipo)
                        {
                            case 1:
                                {
                                    //  PEDIDO DE CERTIDÃO
                                    eventLog.WriteEntry("Iniciando uma pergunta de status de protocolo de pedido de certidão \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);

                                    consultsql.RespostaDeConsultaPedidoCertidao(consult);

                                    eventLog.WriteEntry("Pergunta respondida \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);
                                }
                                break;
                            case 2:
                                {
                                    //  CERTIDÃO DE ESCRITURA
                                    eventLog.WriteEntry("Iniciando uma pergunta de certidão de escritura \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);

                                    consultsql.RespostaDeConsultaCertidaoEscritura(consult);

                                    eventLog.WriteEntry("Pergunta respondida \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);
                                }
                                break;
                            case 3:
                                {
                                    //  CARTAO DE ASSINATURA
                                    eventLog.WriteEntry("Iniciando uma pergunta de cartao de assinatura \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);

                                    consultsql.RespostaDeConsultaCartaoAssinatura(consult);

                                    eventLog.WriteEntry("Pergunta respondida \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);
                                }
                                break;
                            case 4:
                                {
                                    //  CERTIDAO NASCIMENTO
                                    eventLog.WriteEntry("Iniciando uma pergunta de certidão de nascimento \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);

                                    consultsql.RespostaDeConsultaCertidaoNascimento(consult);

                                    eventLog.WriteEntry("Pergunta respondida \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);
                                }
                                break;
                            case 5:
                                {
                                    //  CERTIDAO CASAMENTO
                                    eventLog.WriteEntry("Iniciando uma pergunta de certidão de casamento \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);

                                    consultsql.RespostaDeConsultaCertidaoCasamento(consult);

                                    eventLog.WriteEntry("Pergunta respondida \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);
                                }
                                break;
                            case 6:
                                {
                                    //  CERTIDAO OBITO
                                    eventLog.WriteEntry("Iniciando uma pergunta de certidão de obito \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);

                                    consultsql.RespostaDeConsultaCertidaoObito(consult);

                                    eventLog.WriteEntry("Pergunta respondida \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);
                                }
                                break;
                        }
                        eventLog.WriteEntry("Finalizando perguntas \r\n", System.Diagnostics.EventLogEntryType.Information, eventId++);
                    }
                    else
                    {
                        eventLog.WriteEntry("Nenhuma não encontrada", System.Diagnostics.EventLogEntryType.Information, eventId++);
                    }
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                /*
                //  se retornou um MD5 do protocolo
                if (retornoEnvio.Contains("xml|MD5"))
                {
                    eventLog.WriteEntry(retornoEnvio, System.Diagnostics.EventLogEntryType.Information, eventId++);

                    //  envia e-mail para a Officer
                    controleSelo.EnviarEmail(retornoEnvio, "APROVADA carga de selos");
                }
                else
                {
                    //  informa o log de erro
                    eventLog.WriteEntry(retornoEnvio, System.Diagnostics.EventLogEntryType.Error, eventId++);

                    //  envia e-mail para a Officer
                    controleSelo.EnviarEmail(retornoEnvio, "REJEITADA carga de selos");
                }
                */
            }
            catch (System.Exception e)
            {
                //  informa o log de erro
                eventLog.WriteEntry(e.ToString(), System.Diagnostics.EventLogEntryType.Error, eventId++);
            }
            finally
            {
                //  limpa as configurações apos o processamento
                //ControleParametro.GetParameters().LimparConfiguracao();
            }
             
        }
        
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        public int eventId { get; private set; }
    }
}
