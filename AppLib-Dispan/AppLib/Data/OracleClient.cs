using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Data
{
    public class OracleClient
    {
        #region ATRIBUTOS

        public System.Data.OracleClient.OracleCommand cmd { get; set; }
        public System.Data.OracleClient.OracleConnection conn { get; set; }
        public System.Data.OracleClient.OracleTransaction tran { get; set; }

        public int Timeout { get; set; }

        public int? Increment { get; set; }
        
        #endregion

        #region CONSTRUTORES

        public OracleClient() { }

        public OracleClient(String connectionString)
        {
            conn = new System.Data.OracleClient.OracleConnection(connectionString);
            Timeout = 30;
        }
        
        #endregion

        #region MÉTODOS

        #region CONTROLE DE TRANSAÇÃO

        private Boolean Open()
        {
            try
            {
                if ((tran == null) && (conn.State != System.Data.ConnectionState.Open))
                {
                    conn.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private Boolean Close()
        {
            try
            {
                if ((tran == null) && (conn.State != System.Data.ConnectionState.Closed))
                {
                    conn.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Boolean BeginTransaction()
        {
            try
            {
                this.Open();
                tran = conn.BeginTransaction();
                cmd.CommandTimeout = Timeout;
                return true;
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
                tran.Commit();
                tran = null;
                this.Close();
                return true;
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
                tran.Rollback();
                tran = null;
                this.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        
        #endregion

        #region MANIPULAÇÃO DE DADOS

        public System.Data.DataTable ExecQuery(String command)
        {
            try
            {
                this.Open();
                System.Data.DataTable dt = new System.Data.DataTable("tabela");
                cmd = new System.Data.OracleClient.OracleCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                System.Data.OracleClient.OracleDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                this.Close();
                return dt;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int ExecTransaction(String command)
        {
            try
            {
                this.Open();
                cmd = new System.Data.OracleClient.OracleCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = System.Data.CommandType.Text;
                int result = cmd.ExecuteNonQuery();

                Increment = null;

                try
                {
                    System.Data.DataTable dt = this.ExecQuery(AppLib.Util.Database.SelectSequence(command));

                    if (dt.Rows.Count > 0)
                    {
                        String nomeSequence = dt.Rows[0][0].ToString();
                        System.Data.DataTable dt2 = this.ExecQuery("SELECT " + nomeSequence + ".CURRVAL FROM DUAL");

                        if (dt2.Rows.Count > 0)
                        {
                            Increment = int.Parse(dt2.Rows[0][0].ToString());
                        }
                    }                    
                }
                catch (Exception) { }

                this.Close();
                return result;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int ExecProcedure(String command, String[] parameters, Object[] values)
        {
            try
            {
                this.Open();
                cmd = new System.Data.OracleClient.OracleCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(new System.Data.OracleClient.OracleParameter(parameters[i], values[i]));
                }

                int result = cmd.ExecuteNonQuery();
                this.Close();
                return result;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public System.Data.DataTable GetSchemaTable(String command)
        {
            try
            {
                this.Open();
                System.Data.DataTable dt = new System.Data.DataTable();
                cmd = new System.Data.OracleClient.OracleCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                System.Data.OracleClient.OracleDataReader rd = cmd.ExecuteReader();
                dt = rd.GetSchemaTable();
                this.Close();
                return dt;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion

        #endregion
    }
}
