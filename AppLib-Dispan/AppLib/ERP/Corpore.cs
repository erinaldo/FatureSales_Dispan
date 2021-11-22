using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.ERP
{
    public class Corpore
    {
        public static String GetValorTag(String linha, String Tag)
        {
            if (linha.ToUpper().Contains("<" + Tag.ToUpper() + ">"))
            {
                int inicio = linha.ToUpper().IndexOf("<" + Tag.ToUpper() + ">");
                inicio += (2 + Tag.ToUpper().Length);

                int final = linha.ToUpper().IndexOf("</" + Tag.ToUpper() + ">");
                final -= inicio;

                return linha.Substring(inicio, final);
            }

            return String.Empty;
        }

        public static List<Alias> LoadAlias()
        {
            try
            {
                String arquivo = "Alias.dat";
                List<Alias> result = new List<Alias>();

                if (System.IO.File.Exists(arquivo))
                {
                    String[] conteudo = System.IO.File.ReadAllLines(arquivo);

                    String Nome = String.Empty;
                    String DbType = String.Empty;
                    String DbServer = String.Empty;
                    String DbName = String.Empty;

                    for (int i = 0; i < conteudo.Length; i++)
                    {
                        String linha = conteudo[i];

                        if (GetValorTag(linha, "ALIAS") != String.Empty)
                        {
                            if (Nome != String.Empty)
                            {
                                Alias alias = new Alias(Nome, DbType, DbServer, DbName);
                                result.Add(alias);

                                Nome = String.Empty;
                                DbType = String.Empty;
                                DbServer = String.Empty;
                                DbName = String.Empty;
                            }

                            Nome = GetValorTag(linha, "Alias");
                        }

                        if (GetValorTag(linha, "DbType") != String.Empty)
                        {
                            DbType = GetValorTag(linha, "DbType");
                        }

                        if (GetValorTag(linha, "DbServer") != String.Empty)
                        {
                            DbServer = GetValorTag(linha, "DbServer");
                        }

                        if (GetValorTag(linha, "DbName") != String.Empty)
                        {
                            DbName = GetValorTag(linha, "DbName");
                        }
                    }

                    if (Nome != String.Empty)
                    {
                        Alias alias = new Alias(Nome, DbType, DbServer, DbName);
                        result.Add(alias);

                        Nome = String.Empty;
                        DbType = String.Empty;
                        DbServer = String.Empty;
                        DbName = String.Empty;
                    }

                    if (result.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception("Não foi encontrado confiugrações de alias.");
                    }
                }
                else
                {
                    throw new Exception("Arquivo Alias.dat não encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static List<String> GetAlias(List<Alias> listAlias)
        {
            List<String> result = new List<String>();

            for (int i = 0; i < listAlias.Count; i++)
            {
                result.Add(listAlias[i].Nome);
            }

            return result;
        }

        //public static String GetStringConexaoSQLRM(String Servidor, String Banco, String UsuarioRM)
        //{
        //    Properties.Settings sett = new Properties.Settings();

        //    AppLib.Data.Connection conn = new AppLib.Data.Connection("Start", AppLib.Global.Types.Database.SqlClient,
        //        new AppLib.Data.AssistantConnection().SqlClient(Servidor, Banco, "sysdba", "masterkey"));

        //    String Consulta = @"SELECT DBUSERNAME, DBPASSWORD
        //                        FROM GACESSO
        //                        WHERE CODACESSO IN (SELECT CODACESSO
					   //                             FROM GUSUARIO
					   //                             WHERE CODUSUARIO = ?)";

        //    System.Data.DataTable dt = conn.ExecQuery(Consulta, new Object[] { UsuarioRM });

        //    if (dt.Rows.Count > 0)
        //    {
        //        RM.Lib.Criptografia.RMSCriptografia criptografia = new RM.Lib.Criptografia.RMSCriptografia();

        //        String DBUSERNAME = criptografia.DecryptCorporeLogin(dt.Rows[0]["DBUSERNAME"].ToString());
        //        String DBPASSWORD = criptografia.DecryptCorporeLogin(dt.Rows[0]["DBPASSWORD"].ToString());

        //        return "Data Source=" + Servidor + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + DBUSERNAME + ";Password=" + DBPASSWORD;
        //    }
        //    else
        //    {
        //        throw new Exception("Usuário não encontrado.");
        //    }
        //}

        //public static String GetStringConexaoOracleRM(String Servidor, String UsuarioRM)
        //{
        //    Properties.Settings sett = new Properties.Settings();

        //    AppLib.Data.Connection conn = new AppLib.Data.Connection("Start", AppLib.Global.Types.Database.OracleClient,
        //        new AppLib.Data.AssistantConnection().OracleClient(Servidor, "sysdba", "masterkey"));

        //    String Consulta = @"SELECT DBUSERNAME, DBPASSWORD
        //                        FROM GACESSO
        //                        WHERE CODACESSO IN (SELECT CODACESSO
					   //                             FROM GUSUARIO
					   //                             WHERE CODUSUARIO = ?)";

        //    System.Data.DataTable dt = conn.ExecQuery(Consulta, new Object[] { UsuarioRM });

        //    if (dt.Rows.Count > 0)
        //    {
        //        RM.Lib.Criptografia.RMSCriptografia criptografia = new RM.Lib.Criptografia.RMSCriptografia();

        //        String DBUSERNAME = criptografia.DecryptCorporeLogin(dt.Rows[0]["DBUSERNAME"].ToString());
        //        String DBPASSWORD = criptografia.DecryptCorporeLogin(dt.Rows[0]["DBPASSWORD"].ToString());

        //        return "Data Source=" + Servidor + ";Persist Security Info=True;User ID=" + DBUSERNAME + ";Password=" + DBPASSWORD;
        //    }
        //    else
        //    {
        //        throw new Exception("Usuário não encontrado.");
        //    }
        //}

        public static int ProximoVALAUTOINC(int CODCOLIGADA, String CODSISTEMA, String CODAUTOINC)
        {
            String Consulta = @"
SELECT VALAUTOINC
FROM GAUTOINC
WHERE CODCOLIGADA = ?
  AND CODSISTEMA = ?
  AND CODAUTOINC = ?";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(Consulta, new Object[] { CODCOLIGADA, CODSISTEMA, CODAUTOINC });

            if (dt.Rows.Count == 0)
            {
                String Comando = @"INSERT INTO GAUTOINC (CODCOLIGADA, CODSISTEMA, CODAUTOINC, VALAUTOINC) VALUES (?, ?, ?, 1)";
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(Comando, new Object[] { CODCOLIGADA, CODSISTEMA, CODAUTOINC });
                return 1;
            }
            else
            {
                int temp = int.Parse(dt.Rows[0][0].ToString());
                temp++;

                String Comando = @"UPDATE GAUTOINC SET VALAUTOINC = ? WHERE CODCOLIGADA = ? AND CODSISTEMA = ? AND CODAUTOINC = ?";
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(Comando, new Object[] { temp, CODCOLIGADA, CODSISTEMA, CODAUTOINC });
                return temp;
            }
        }
    }
}
