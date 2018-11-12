using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void VerifyConsultWebSite()
        {
            using (ConsultSQL consultsql = ConsultSQL.GetInstace())
            {
                textBox1.Clear();

                textBox1.AppendText("Iniciando consulta de perguntas para a base de dados \r\n");

                Consulta consult = consultsql.ReturnConsult();

                textBox1.AppendText("Pergunta encontrada iniciando tentativa de responder \r\n");

                switch (consult.Tipo)
                {
                    case 1: {
                            textBox1.AppendText("Iniciando uma pergunta de status de protocolo \r\n");

                            consultsql.RespostaDeConsultaPedidoCertidao(consult);

                            textBox1.AppendText("Protocolo respondido \r\n");
                        } break;
                }


                textBox1.AppendText("Finalizando perguntas \r\n");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            VerifyConsultWebSite();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("SQL Web Site Manager", @"SQLWebSiteManager.exe");
        }
    }
}
