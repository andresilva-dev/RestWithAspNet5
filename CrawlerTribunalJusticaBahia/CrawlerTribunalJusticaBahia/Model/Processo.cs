using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerTribunalJusticaBahia.Model
{
    public class Processo
    {
        public string NumeroDoProcesso { get; set; }
        public string Classe { get; set; }
        public string Area { get; set; }
        public string Assunto { get; set; }
        public string Origem { get; set; }
        public string NumeroDeOrigem { get; set; }
        public string Distribuicao { get; set; }
        public string Relator { get; set; }
    }
}
