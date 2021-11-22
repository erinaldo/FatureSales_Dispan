using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AppLib.Windows
{
    public partial class Query
    {
        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        [Category("_APP"), Description("Consulta da query")]
        public String[] Consulta { get; set; }

        [Category("_APP"), Description("Parâmetros da query")]
        public String[] Parametros { get; set; }

        public System.Data.DataTable dt;

        public Query()
        {
            Conexao = "Start";
        }

        public String GetConsulta()
        {
            String result = "";

            for (int i = 0; i < Consulta.Length; i++)
            {
                result += Consulta[i] + "\r\n";
            }

            return result;
        }

        public void setDataTable()
        {
            String comando = "";

            try
            {
                comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(this.GetConsulta(), Parametros);
                dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(comando, new Object[] { });
            }
            catch (Exception ex)
            {
                new FormExceptionSQL().Mostrar("Erro ao carregar query", comando, ex);
            }
        }

    }
}
