using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Data
{
    public class Connection
    {
        #region ATRIBUTOS

        public String Name { get; set; }
        public Global.Types.Database Database { get; set; }
        public String ConnectionString { get; set; }
        public int Timeout { get; set; }

        public SqlClient Sql { get; set; }
        public OracleClient Oracle { get; set; }
        public OleDb Ole { get; set; }
        public WebService Ws { get; set; }
        
        #endregion

        #region CONSTRUTOR

        public Connection() { }

        public Connection(String name, Global.Types.Database database, String connectionString)
        {
            try
            {
                Name = name;
                Database = database;
                ConnectionString = connectionString;
                Timeout = 30;

                if (database == Global.Types.Database.SqlClient)
                {
                    Sql = new SqlClient(connectionString);
                }

                if (database == Global.Types.Database.OracleClient)
                {
                    Oracle = new OracleClient(connectionString);
                }

                if (database == Global.Types.Database.SqlLocalDb)
                {
                    Ole = new OleDb(connectionString);
                }

                if (database == Global.Types.Database.SqlWebService)
                {
                    Ws = new WebService(connectionString);
                }

                if (database == Global.Types.Database.OracleWebService)
                {
                    Ws = new WebService(connectionString);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public AppLib.Data.Connection Clone()
        {
            AppLib.Data.Connection connClone = new Connection(this.Name, this.Database, this.ConnectionString);
            connClone.Timeout = this.Timeout;
            connClone.Test();
            return connClone;
        }
        
        #endregion

        #region MÉTODOS

        #region CONTROLE DE TRANSAÇÃO

        public Boolean BeginTransaction()
        {
            try
            {
                if (Database == Global.Types.Database.SqlClient)
                {
                    Sql.cmd.CommandTimeout = Timeout;
                    return Sql.BeginTransaction();
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    Oracle.cmd.CommandTimeout = Timeout;
                    return Oracle.BeginTransaction();
                }

                if (Database == Global.Types.Database.SqlLocalDb)
                {
                    Ole.cmd.CommandTimeout = Timeout;
                    return Ole.BeginTransaction();
                }

                if (Database == Global.Types.Database.SqlWebService)
                {
                    return Ws.BeginTransaction();
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    return Ws.BeginTransaction();
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Boolean Commit()
        {
            try
            {
                if (Database == Global.Types.Database.SqlClient)
                {
                    return Sql.Commit();
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    return Oracle.Commit();
                }

                if (Database == Global.Types.Database.SqlLocalDb)
                {
                    return Ole.Commit();
                }

                if (Database == Global.Types.Database.SqlWebService)
                {
                    return Ws.Commit();
                }

                if (Database == Global.Types.Database.OracleWebService)
                {
                    return Ws.Commit();
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Boolean Rollback()
        {
            try
            {
                if (Database == Global.Types.Database.SqlClient)
                {
                    return Sql.Rollback();
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    return Oracle.Rollback();
                }

                if (Database == Global.Types.Database.SqlLocalDb)
                {
                    return Ole.Rollback();
                }

                if (Database == Global.Types.Database.SqlWebService)
                {
                    return Ws.Rollback();
                }

                if (Database == Global.Types.Database.OracleWebService)
                {
                    return Ws.Rollback();
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion

        #region MANIPULAÇÃO DE DADOS

        public Object[] ParseInterrogacao(Object[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++ )
            {
                if ( parameters[i] != null )
                {
                    if (parameters[i].ToString().Contains("?"))
                    {
                        parameters[i] = parameters[i].ToString().Replace("?", "");
                    }
                }
            }

            return parameters;
        }

        public String ParseCommand(String command, Object[] parameters)
        {
            try
            {
                if (parameters == null)
                {
                    return command;
                }
                else
                {
                    if (parameters.Length == 0)
                    {
                        return command;
                    }
                    else
                    {
                        parameters = ParseInterrogacao(parameters);

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            int posicao = command.IndexOf("?");

                            if (posicao >= 0)
                            {

                                String antes = command.Substring(0, posicao);
                                String depois = command.Substring(posicao + 1);

                                #region CONVERTE O PARÂMETRO NA STRING TEMP

                                object parametro = parameters[i];
                                String temp = "";

                                int tipoConhecido = 0;

                                if (parametro == null)
                                {
                                    temp = "NULL";
                                    tipoConhecido++;
                                }
                                else
                                {
                                    if (parametro.GetType() == typeof(DateTime))
                                    {
                                        // VERSAO ANTIGA
                                        // DateTime dataHora = DateTime.Parse(parametro.ToString());

                                        DateTime dataHora = System.Convert.ToDateTime(parametro);

                                        if (Database == Global.Types.Database.SqlClient)
                                        {
                                            temp = Util.Format.dataSql(dataHora);
                                            tipoConhecido++;
                                        }

                                        if (Database == Global.Types.Database.OracleClient)
                                        {
                                            temp = Util.Format.dataOracle(dataHora);
                                            tipoConhecido++;
                                        }

                                        if (Database == Global.Types.Database.SqlLocalDb)
                                        {
                                            temp = Util.Format.dataSql(dataHora);
                                            tipoConhecido++;
                                        }

                                        if (Database == Global.Types.Database.SqlWebService)
                                        {
                                            temp = Util.Format.dataSql(dataHora);
                                            tipoConhecido++;
                                        }

                                        if (Database == Global.Types.Database.OracleWebService)
                                        {
                                            temp = Util.Format.dataOracle(dataHora);
                                            tipoConhecido++;
                                        }

                                    }

                                    if (parametro.GetType() == typeof(int))
                                    {
                                        temp = parametro.ToString();
                                        tipoConhecido++;
                                    }

                                    if (parametro.GetType() == typeof(decimal))
                                    {
                                        temp = parametro.ToString().Replace(",", ".");
                                        tipoConhecido++;
                                    }

                                    if (parametro.GetType() == typeof(String))
                                    {
                                        temp = "'" + parametro.ToString().Replace("'", "''") + "'";
                                        tipoConhecido++;
                                    }

                                    if (parametro.GetType() == typeof(DBNull))
                                    {
                                        temp = "NULL";
                                        tipoConhecido++;
                                    }

                                    // Se o tipo é desconhecido
                                    if (tipoConhecido == 0)
                                    {
                                        String tipoString = parametro.GetType().ToString();
                                        temp = "'" + parametro.ToString().Replace("'", "''") + "'";
                                    }
                                }

                                #endregion

                                command = antes + temp + depois;

                            }
                        }

                        return command;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public String ParseVariaveis(String comando)
        {
            comando = comando.Replace("$ONTEM", "'" + new AppLib.Data.Variaveis(Name).getOntem() + "'");
            comando = comando.Replace("$HOJE", "'" + new AppLib.Data.Variaveis(Name).getHoje() + "'");
            comando = comando.Replace("$AMANHA", "'" + new AppLib.Data.Variaveis(Name).getAmanha() + "'");
            comando = comando.Replace("$MES", "'" + new AppLib.Data.Variaveis(Name).getMes() + "'");
            comando = comando.Replace("$ANO", "'" + new AppLib.Data.Variaveis(Name).getAno() + "'");
            comando = comando.Replace("$USUARIO", "'" + new AppLib.Data.Variaveis(Name).getUsuario() + "'");
            comando = comando.Replace("$EMPRESA", new AppLib.Data.Variaveis(Name).getEmpresa().ToString());

            return comando;
        }

        public System.Data.DataTable ExecQuery(String command, params Object[] parameters)
        {
            try
            {
                command = this.ParseCommand(command, parameters);

                try
                {
                    command = this.ParseVariaveis(command);
                }
                catch { }

                if (Database == Global.Types.Database.SqlClient)
                {
                    Sql.Timeout = Timeout;
                    return Sql.ExecQuery(command);
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    Oracle.Timeout = Timeout;
                    return Oracle.ExecQuery(command);
                }

                if (Database == Global.Types.Database.SqlLocalDb)
                {
                    Ole.Timeout = Timeout;
                    return Ole.ExecQuery(command);
                }

                if (Database == Global.Types.Database.SqlWebService)
                {
                    return Ws.ExecQuery(command);
                }

                if (Database == Global.Types.Database.OracleWebService)
                {
                    return Ws.ExecQuery(command);
                }

                return null;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("QUERY: " + command);
                AppLib.Util.Log.Escrever("Comando: " + command + "\r\n" + ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int ExecTransaction(String command, params Object[] parameters)
        {
            try
            {
                command = this.ParseCommand(command, parameters);

                if (Database == Global.Types.Database.SqlClient)
                {
                    Sql.Timeout = Timeout;
                    return Sql.ExecTransaction(command);
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    Oracle.Timeout = Timeout;
                    return Oracle.ExecTransaction(command);
                }

                if (Database == Global.Types.Database.SqlLocalDb)
                {
                    Ole.Timeout = Timeout;
                    return Ole.ExecTransaction(command);
                }

                if (Database == Global.Types.Database.SqlWebService)
                {
                    return Ws.ExecTransaction(command);
                }

                if (Database == Global.Types.Database.OracleWebService)
                {
                    return Ws.ExecTransaction(command);
                }

                return 0;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                AppLib.Util.Log.Escrever("Comando: " + command + "\r\n" + ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int ExecProcedure(String command, String[] parameters, Object[] values)
        {
            try
            {
                // command = this.ParseCommand(command, parameters);

                if (Database == Global.Types.Database.SqlClient)
                {
                    Sql.Timeout = Timeout;
                    return Sql.ExecProcedure(command, parameters, values);
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    Oracle.Timeout = Timeout;
                    return Oracle.ExecProcedure(command, parameters, values);
                }

                if (Database == Global.Types.Database.SqlLocalDb)
                {
                    Ole.Timeout = Timeout;
                    return Ole.ExecProcedure(command, parameters, values);
                }

                if (Database == Global.Types.Database.SqlWebService)
                {
                    return Ws.ExecProcedure(command, parameters, values);
                }

                if (Database == Global.Types.Database.OracleWebService)
                {
                    return Ws.ExecProcedure(command, parameters, values);
                }

                return 0;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                AppLib.Util.Log.Escrever("Comando: " + command + "\r\n" + ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Object ExecGetField(Object returnIfIsNull, String command, params Object[] parameters)
        {
            System.Data.DataTable dt = this.ExecQuery(command, parameters);

            if (dt == null)
            {
                return returnIfIsNull;
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0] == DBNull.Value)
                    {
                        return returnIfIsNull;
                    }
                    else
                    {
                        return dt.Rows[0][0];
                    }
                }
                else
                {
                    return returnIfIsNull;
                }
            }
        }

        public Boolean ExecHasRows(String command, params Object[] parameters)
        {
            System.Data.DataTable dt = this.ExecQuery(command, parameters);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean Test()
        {
            try
            {
                if (Database == Global.Types.Database.SqlClient)
                {
                    return this.ExecHasRows("SELECT 1", null);
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    return this.ExecHasRows("SELECT 1 FROM DUAL", null);
                }

                if (Database == Global.Types.Database.SqlLocalDb)
                {
                    return this.ExecHasRows("SELECT 1", null);
                }

                if (Database == Global.Types.Database.SqlWebService)
                {
                    return this.ExecHasRows("SELECT 1", null);
                }

                if (Database == Global.Types.Database.OracleWebService)
                {
                    return this.ExecHasRows("SELECT 1 FROM DUAL", null);
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public System.Data.DataTable GetSchemaTable(String command, params Object[] parameters)
        {
            try
            {
                command = this.ParseCommand(command, parameters);

                if (Database == Global.Types.Database.SqlClient)
                {
                    Sql.Timeout = Timeout;
                    return Sql.GetSchemaTable(command);
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    Oracle.Timeout = Timeout;
                    return Oracle.GetSchemaTable(command);
                }

                if (Database == Global.Types.Database.SqlLocalDb)
                {
                    Ole.Timeout = Timeout;
                    return Ole.GetSchemaTable(command);
                }

                if (Database == Global.Types.Database.SqlWebService)
                {
                    return Ws.GetSchemaTable(command);
                }

                if (Database == Global.Types.Database.OracleWebService)
                {
                    return Ws.GetSchemaTable(command);
                }

                return null;
            }
            catch (Exception ex)
            {
                AppLib.Util.Log.Escrever("Comando: " + command + "\r\n" + ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int? GetIncrement()
        {
            if (Database == Global.Types.Database.SqlClient)
            {
                return Sql.Increment;
            }

            if (Database == Global.Types.Database.OracleClient)
            {
                return Oracle.Increment;
            }

            if (Database == Global.Types.Database.SqlLocalDb)
            {
                return Ole.Increment;
            }

            if (Database == Global.Types.Database.SqlWebService)
            {
                return Ws.Increment;
            }

            if (Database == Global.Types.Database.OracleWebService)
            {
                return Ws.Increment;
            }

            return null;
        }

        public DateTime GetDateTime()
        {
            try
            {
                if (Database == Global.Types.Database.SqlClient)
                {
                    return DateTime.Parse(this.ExecGetField(null, "SELECT GETDATE()", new Object[] { }).ToString());
                }

                if (Database == Global.Types.Database.OracleClient)
                {
                    return DateTime.Parse(this.ExecGetField(null, "SELECT SYSDATE FROM DUAL", new Object[] { }).ToString());
                }

                if (Database == Global.Types.Database.SqlLocalDb)
                {
                    return DateTime.Parse(this.ExecGetField(null, "SELECT GETDATE()", new Object[] { }).ToString());
                }

                if (Database == Global.Types.Database.SqlWebService)
                {
                    return DateTime.Parse(this.ExecGetField(null, "SELECT GETDATE()", new Object[] { }).ToString());
                }

                if (Database == Global.Types.Database.OracleWebService)
                {
                    return DateTime.Parse(this.ExecGetField(null, "SELECT SYSDATE FROM DUAL", new Object[] { }).ToString());
                }

                throw new Exception("Erro no método GetDateTime(), Enumerator Database não encontrado.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion

        #endregion

    }
}
