using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLWebSiteManager
{
    public class Consulta
    {
        public int Id { get; set; } = 0;
        public int Tipo { get; set; } = 0;
        public string Resposta { get; set; } = string.Empty;
        public string Parametros { get; set; } = string.Empty;
    }
}
