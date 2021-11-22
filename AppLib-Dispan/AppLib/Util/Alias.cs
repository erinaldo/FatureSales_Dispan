using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Util
{
    public class Alias
    {
        public string Name { get; set; }
        public string DbType { get; set; }
        public string ServerName { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Alias()
        { 
        
        }

        public bool GetAlias()
        {
            return false;            
        }
    }
}
