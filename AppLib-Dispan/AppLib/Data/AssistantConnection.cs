using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Data
{
    public class AssistantConnection
    {
        public String SqlClient(String serverName, String databaseName, String user, String password)
        {
            return "Data Source="+ serverName +";Initial Catalog="+ databaseName +";Persist Security Info=True;User ID="+ user +";Password=" + password;
        }

        public String SqlLocalDB(String Instance, String databaseName)
        {
            return "Provider=SQLNCLI11.1;Data Source=(localdb)\\"+ Instance +";Integrated Security=SSPI;Initial Catalog=" + databaseName;
        }

        public String OracleClient(String databaseName, String user, String password)
        {
            return "Data Source="+ databaseName +";Persist Security Info=True;User ID="+ user +";Password=" + password;
        }

        public String WebService(String Usuario, String Senha, String WebServiceAddress)
        {
            return Usuario + ";" + Senha + ";" + WebServiceAddress;
        }

    }
}
