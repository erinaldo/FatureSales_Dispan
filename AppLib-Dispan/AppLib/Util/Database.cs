using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Util
{
    public static class Database
    {
        public static String getTableInsert(String comandoInsert)
        {
            String[] palavras = comandoInsert.Split(' ');
            String tabela = null;
            Boolean proximo = false;
            for (int i = 0; i < palavras.Length; i++)
            {
                if (proximo)
                {
                    tabela = palavras[i].ToUpper();
                    i = palavras.Length;
                }
                else
                {
                    if (palavras[i].ToUpper().Equals("INTO"))
                    {
                        proximo = true;
                    }
                }
            }

            return tabela;
        }

        public static String SelectSequence(String comandoInsert)
        {
            String tabela = getTableInsert(comandoInsert);

            String consulta = @"
SELECT NOMETABELA || '_' || NOMECAMPO || '_SEQ' FROM (

SELECT
TABLE_NAME NOMETABELA,
COLUMN_NAME NOMECAMPO,
( SELECT CASE WHEN ( COUNT(*) > 0 ) THEN 'S' ELSE 'N' END FROM SYS.ALL_SEQUENCES WHERE SEQUENCE_NAME = TABLE_NAME || '_' || COLUMN_NAME || '_SEQ' ) AUTOINCREMENTO

FROM ALL_TAB_COLS

WHERE TABLE_NAME = '" + tabela + @"'

) X

WHERE AUTOINCREMENTO = 'S'";

            return consulta;
        }

        public static int LoadConexoes()
        {
            String consulta = @"SELECT * FROM ZCONEXAO";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(consulta, new Object[] { });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String ALIAS = dt.Rows[i]["ALIAS"].ToString();
                String TIPO = dt.Rows[i]["TIPO"].ToString();
                String  STRING = dt.Rows[i]["STRING"].ToString();

                if (TIPO.Equals("SqlClient"))
                {
                    AppLib.Context.poolConnection.Add(ALIAS, Global.Types.Database.SqlClient, STRING);
                }

                if (TIPO.Equals("OracleClient"))
                {
                    AppLib.Context.poolConnection.Add(ALIAS, Global.Types.Database.OracleClient, STRING);
                }

                if (TIPO.Equals("SqlLocalDb"))
                {
                    AppLib.Context.poolConnection.Add(ALIAS, Global.Types.Database.SqlLocalDb, STRING);
                }

                if (TIPO.Equals("SqlWebService"))
                {
                    AppLib.Context.poolConnection.Add(ALIAS, Global.Types.Database.SqlWebService, STRING);
                }

                if (TIPO.Equals("OracleWebService"))
                {
                    AppLib.Context.poolConnection.Add(ALIAS, Global.Types.Database.OracleWebService, STRING);
                }
            }

            return dt.Rows.Count;
        }

        public static int ReloadConexoes()
        {
            // Remove todas conexões exceto a Default
            for (int i = AppLib.Context.poolConnection.Size(); i != 0; i--)
            {
                String ALIAS = AppLib.Context.poolConnection.Get(i-1).Name;

                if (ALIAS.Equals("Start"))
                {
                    // OK
                }
                else
                {
                    if (AppLib.Context.poolConnection.Remove(ALIAS))
                    {
                        // OK
                    }
                    else
                    {
                        AppLib.Windows.FormMessageDefault.ShowError("Erro ao remover a conexão: " + ALIAS);
                    }
                }
            }

            return LoadConexoes();
        }

    }
}
