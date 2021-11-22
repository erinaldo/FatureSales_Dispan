using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.ERP
{
    public class Start
    {
        public static int ProximoIDLOG(int CODEMPRESA, String CODTABELA)
        {
            String Consulta = @"
SELECT IDLOG
FROM GLOG
WHERE CODEMPRESA = ?
  AND CODTABELA = ?";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(Consulta, new Object[] { CODEMPRESA, CODTABELA });

            if (dt.Rows.Count == 0)
            {
                String Comando = @"INSERT INTO GLOG (CODEMPRESA, CODTABELA, IDLOG) VALUES (?, ?, ?)";
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(Comando, new Object[] { CODEMPRESA, CODTABELA, 1 });
                return 1;
            }
            else
            {
                int IDLOG = int.Parse(dt.Rows[0][0].ToString());
                IDLOG++;

                String Comando = @"UPDATE GLOG SET IDLOG = ? WHERE CODEMPRESA = ? AND CODTABELA = ?";
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(Comando, new Object[] { IDLOG, CODEMPRESA, CODTABELA });
                return IDLOG;
            }
        }
    }
}
