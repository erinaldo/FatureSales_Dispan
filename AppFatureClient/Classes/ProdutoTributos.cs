using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFatureClient.Classes
{
    public static class ProdutoTributos
    {
        public static void InsereEstados(int IDPRD)
        {
            try
            {
                string sql = String.Format(@"select CODETD from GETD
											where CODETD not in (
											select CODETD 
											from TPRDCODFISCAL
											where IDPRD = {0})", IDPRD);

                using (DataTable dt = MetodosSQL.GetDT(sql))
                {
                    sql = @"insert into TPRDCODFISCAL (CODCOLIGADA, IDPRD, CODETD, CODFISCAL, ALIQINTICMS, FATORREDUCAOICMS, TRIBUTADOICMSST, FATORSUBST, MODALIDADEBCICMS, MODALIDADEBCICMSST, PERFECP, RECCREATEDBY, RECCREATEDON)
                        values (/*CODCOLIGADA*/ 1, 
                                /*IDPRD*/ {0}, 
                                /*CODETD*/ '{1}', 
                                /*CODFISCAL*/ null, 
                                /*ALIQINTICMS*/ null, 
                                /*FATORREDUCAOICMS*/null, 
                                /*TRIBUTADOICMSST*/ null, 
                                /*FATORSUBST*/ null, 
                                /*MODALIDADEBCICMS*/ 3, 
                                /*MODALIDADEBCICMSST*/ 4, 
                                /*PERFECP*/ {3},
                                /*RECCREATEDBY*/ '{2}', 
                                /*RECCREATEDON*/ getdate())";

                    

                    List<string> insert = new List<string>();

                    foreach (DataRow row in dt.Rows)
                    {
                        decimal vPERFECP = 0;

                        if ((String)row["CODETD"] == "RJ")
                        {
                            vPERFECP = 2.0000M;
                        }

                        insert.Add(String.Format(sql, IDPRD, (String)row["CODETD"], AppLib.Context.Usuario, vPERFECP.ToString().Replace(",",".")));
                    }

                    MetodosSQL.ExecMultiple(insert);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
