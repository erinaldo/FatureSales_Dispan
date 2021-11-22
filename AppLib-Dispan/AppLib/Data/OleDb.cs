using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Data
{
    public class OleDb
    {
        #region ATRIBUTOS

        public System.Data.OleDb.OleDbCommand cmd { get; set; }
        public System.Data.OleDb.OleDbConnection conn { get; set; }
        public System.Data.OleDb.OleDbTransaction tran { get; set; }

        public int Timeout { get; set; }

        public int? Increment { get; set; }
        
        #endregion

        #region CONSTRUTORES

        public OleDb() { }

        public OleDb(String connectionString)
        {
            conn = new System.Data.OleDb.OleDbConnection(connectionString);
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
                cmd = new System.Data.OleDb.OleDbCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                System.Data.OleDb.OleDbDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                this.Close();
                return dt;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int ExecTransaction(String command)
        {
            try
            {
                this.Open();
                cmd = new System.Data.OleDb.OleDbCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = System.Data.CommandType.Text;
                int result = cmd.ExecuteNonQuery();

                Increment = null;
                cmd.CommandText = "SELECT @@IDENTITY";

                try
                {
                    Increment = int.Parse(cmd.ExecuteScalar().ToString());
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
                cmd = new System.Data.OleDb.OleDbCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;

                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter(parameters[i], values[i]));
                }

                int result = cmd.ExecuteNonQuery();
                this.Close();
                return result;
            }
            catch (System.Data.OleDb.OleDbException ex)
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
                cmd = new System.Data.OleDb.OleDbCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                System.Data.OleDb.OleDbDataReader rd = cmd.ExecuteReader();
                dt = rd.GetSchemaTable();
                this.Close();
                return dt;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion

        #endregion
    }
}
