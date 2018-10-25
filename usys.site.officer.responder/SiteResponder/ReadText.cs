using System.IO;
using System.Security.Permissions;
using System.Text;

namespace SiteResponder
{
    public class ReadText : Utils<ReadText>
    {
        public string LeArquivoTxt(string File)
        {

            StringBuilder Retorno = new StringBuilder();
            string TextReder = string.Empty;
            FileInfo fi = new FileInfo(File);
            try
            {
                FileIOPermission permission = new FileIOPermission(FileIOPermissionAccess.Read, System.Security.AccessControl.AccessControlActions.Change, fi.FullName);

                using (StreamReader texto = new StreamReader(File))
                {
                    Retorno.Append(texto.ReadToEnd());
                }
            }
            catch
            {
                Retorno.Append(fi.OpenText());
            }

            return Retorno.ToString();
        }

        public void GravaAquivoTxt(string File, string pContent)
        {
            using (StreamWriter writer = new StreamWriter(File))
            {
                writer.Write(pContent);
            }
        }
    }
}
