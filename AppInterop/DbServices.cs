using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RM;

namespace AppInterop
{
    public class DbServices
    {
        public RM.Lib.Data.DbServices DBS;

        private static RM.Lib.Data.DbServices _dbs = null;

        public DbServices()
        {

        }

        public static RM.Lib.Data.DbServices GetInstance()
        {
         
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                string conn = string.Concat("Server=", alias.ServerName, ";Database=", alias.DbName, ";User Id=", alias.UserName, ";Password=", alias.Password, ";");
                if (_dbs == null) _dbs = new RM.Lib.Data.DbServices(RM.Lib.Data.ProviderType.SqlClient, conn);
            _dbs.OpenConnection();
            return _dbs;
           
        }

        public void InitServer()
        {
            try
            {
                DBS = GetInstance();

            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public int GetNewAutoInc(int CodColigada, string CodSistema, string CodAutoInc)
        {
            int retorno = 0;

           // this.DBS.SkipControlColumns = true;

            try
            {
                //InitServer();
                string sSql = string.Empty;
                int ValAutoInc = 0;
                
                sSql = @"UPDATE GAUTOINC SET VALAUTOINC = VALAUTOINC + 1 WHERE CODCOLIGADA = ? AND CODSISTEMA = ? AND CODAUTOINC = ?";
                if (AppLib.Context.poolConnection.Get().ExecTransaction(sSql, new object[]{CodColigada, CodSistema, CodAutoInc}) == 0)
                {
                    sSql = @"INSERT INTO GAUTOINC (CODCOLIGADA, CODSISTEMA, CODAUTOINC, VALAUTOINC) VALUES (?, ?, ?, 1)";
                    AppLib.Context.poolConnection.Get().ExecTransaction(sSql, new object[] { CodColigada, CodSistema, CodAutoInc });

                }
                sSql = @"SELECT VALAUTOINC FROM GAUTOINC WHERE CODCOLIGADA = ? AND CODSISTEMA = ? AND CODAUTOINC = ?";
                ValAutoInc = Convert.ToInt32(AppLib.Context.poolConnection.Get().ExecGetField(0, sSql, new object[] { CodColigada, CodSistema, CodAutoInc }));


                //if (DBS.QueryExec(sSql, CodColigada, CodSistema, CodAutoInc) == 0)
                //{
                //    sSql = @"INSERT INTO GAUTOINC (CODCOLIGADA, CODSISTEMA, CODAUTOINC, VALAUTOINC) VALUES (:CODCOLIGADA, :CODSISTEMA, :CODAUTOINC, 1)";
                //    DBS.QueryExec(sSql, CodColigada, CodSistema, CodAutoInc);
                //}

                //sSql = @"SELECT VALAUTOINC FROM GAUTOINC WHERE CODCOLIGADA = :CODCOLIGADA AND CODSISTEMA = :CODSISTEMA AND CODAUTOINC = :CODAUTOINC";
                //ValAutoInc = Convert.ToInt32(DBS.QueryValue(0, sSql, CodColigada, CodSistema, CodAutoInc));
                retorno = ValAutoInc;
            }
            catch (Exception)
            {
                retorno = 0;
            }

            //this.DBS.SkipControlColumns = false;

            return retorno;
        }
    }
}