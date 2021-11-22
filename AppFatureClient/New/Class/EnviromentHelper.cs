using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFatureClient.New.Class
{
    public static class EnviromentHelper
    {
        public static int? IDMOV { get; set; }

        public static int NSEQITEMMOV { get; set; }

        public static DataTable TABELAITEM { get; set; }

        public static int IndexAlias { get; set; }

        public static string Versao { get; set; }

        public static DataTable ItensOrdenados {get;set;}
    }
}
