using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLWebSiteManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Comfiguracoes cofig = new Comfiguracoes())
            {
                cofig.StartPosition = FormStartPosition.CenterScreen;
                cofig.ShowDialog();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static StringBuilder LogViewr = new StringBuilder();

        private static void VerifyConsultWebSite()
        {
            while (true)
            {
                using (ConsultSQL consultsql = ConsultSQL.GetInstace())
                {
                    //textBox1.Clear();

                    LogViewr.Append("Iniciando consulta de perguntas para a base de dados \r\n");

                    Consulta consult = consultsql.ReturnConsult();

                    LogViewr.Append("Pergunta encontrada iniciando tentativa de responder \r\n");

                    switch (consult.Tipo)
                    {
                        case 1:
                            {
                                LogViewr.Append("Iniciando uma pergunta de status de protocolo \r\n");

                                consultsql.RespostaDeConsultaPedidoCertidao(consult);

                                LogViewr.Append("Protocolo respondido \r\n");
                            }
                            break;
                        case 2:
                            {
                                LogViewr.Append("Iniciando uma pergunta de certidão de nascimento \r\n");

                                consultsql.RespostaDeConsultaCertidaoCasamento(consult);

                                LogViewr.Append("Pergunta respondida \r\n");
                            }
                            break;
                        case 3:
                            {
                                LogViewr.Append("Iniciando uma pergunta de certidão de nascimento \r\n");

                                consultsql.RespostaDeConsultaCertidaoNascimento(consult);

                                LogViewr.Append("Pergunta respondida \r\n");
                            }
                            break;
                        case 4:
                            {
                                LogViewr.Append("Iniciando uma pergunta de certidão de nascimento \r\n");

                                consultsql.RespostaDeConsultaCertidaoObito(consult);

                                LogViewr.Append("Pergunta respondida \r\n");
                            }
                            break;
                        case 5:
                            {
                                LogViewr.Append("Iniciando uma pergunta de certidão de nascimento \r\n");

                                consultsql.RespostaDeConsultaCertidaoEscritura(consult);

                                LogViewr.Append("Pergunta respondida \r\n");
                            }
                            break;
                    }


                    LogViewr.Append("Finalizando perguntas \r\n");
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Thread.Sleep(500);
                LogViewr.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.AppendText(LogViewr.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string CompletPath = Assembly.GetExecutingAssembly().Location;

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("SQL Web Site Manager", CompletPath);

            MessageBox.Show(CompletPath + " Registrado");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                VerifyConsultWebSite();
            }).Start();

            timer1.Start();
        }
    }
}
