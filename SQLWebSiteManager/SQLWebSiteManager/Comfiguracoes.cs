using System;
using System.IO;
using System.Windows.Forms;

namespace SQLWebSiteManager
{
    public partial class Comfiguracoes : Form
    {
        public Comfiguracoes()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tbxExternalAdress_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CompletPath = Path.GetTempPath() + "\\SQLWEBSITEMANAGER";

            if (!Directory.Exists(CompletPath))
            {
                Directory.CreateDirectory(CompletPath);
            }

            var textContent = $"{tbxLocalAdress.Text}|{tbxInternalUser.Text}|{tbxInternalPass.Text }|{tbxExternalAdress.Text }|{tbxUserExternal.Text }|{tbxPassExternal.Text }|{tbxConsulttable.Text}";

            ReadText.GetInstace().GravaAquivoTxt(CompletPath + "\\SQLWEBSITE.CONFIG", textContent);

            this.Close();
        }

        private void Comfiguracoes_Load(object sender, EventArgs e)
        {
            string CompletPath = Path.GetTempPath() + "\\SQLWEBSITEMANAGER";

            if (!File.Exists(CompletPath + "\\SQLWEBSITE.CONFIG"))
                return;

            var content = ReadText.GetInstace().LeArquivoTxt(CompletPath + "\\SQLWEBSITE.CONFIG").Split('|');

            if(content.Length > 6)
            {
                tbxLocalAdress.Text = content[0];
                tbxInternalUser.Text = content[1];
                tbxInternalPass.Text = content[2];
                tbxExternalAdress.Text = content[3];
                tbxUserExternal.Text = content[4];
                tbxPassExternal.Text = content[5];
                tbxConsulttable.Text = content[6];
            }
        }
    }
}
