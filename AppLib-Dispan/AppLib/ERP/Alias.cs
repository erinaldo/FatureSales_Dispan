using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.ERP
{
    public class Alias
    {
        public String Nome { get; set; }
        public String DbType { get; set; }
        public String DbServer { get; set; }
        public String DbName { get; set; }

        public Alias(String _Nome, String _DbType, String _DbServer, String _DbName)
        {
            Nome = _Nome;
            DbType = _DbType;
            DbServer = _DbServer;
            DbName = _DbName;
        }
    }
}
