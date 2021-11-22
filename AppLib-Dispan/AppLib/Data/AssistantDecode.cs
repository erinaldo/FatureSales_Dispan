using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Data
{
    public class AssistantDecode
    {
        public static System.Data.SqlClient.SqlConnectionStringBuilder SqlClient(String stringConnetion)
        {
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(stringConnetion);
            return builder;
        }

        public static System.Data.OracleClient.OracleConnectionStringBuilder OracleClient(String stringConnetion)
        {
            System.Data.OracleClient.OracleConnectionStringBuilder builder = new System.Data.OracleClient.OracleConnectionStringBuilder(stringConnetion);
            return builder;
        }

    }
}
