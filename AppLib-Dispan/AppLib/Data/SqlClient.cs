using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Data
{
    public class SqlClient
    {
        #region ATRIBUTOS

        public System.Data.SqlClient.SqlCommand cmd { get; set; }
        public System.Data.SqlClient.SqlConnection conn { get; set; }
        public System.Data.SqlClient.SqlTransaction tran { get; set; }

        public int Timeout { get; set; }

        public int? Increment { get; set; }
        
        #endregion

        #region CONSTRUTORES

        public SqlClient() { }

        public SqlClient(String connectionString)
        {
            conn = new System.Data.SqlClient.SqlConnection(connectionString);
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
                cmd = new System.Data.SqlClient.SqlCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                System.Data.SqlClient.SqlDataReader rd = cmd.ExecuteReader();
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
                cmd = new System.Data.SqlClient.SqlCommand(command, conn, tran);
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

        public int ExecProcedure(String command, String[] parameters, params Object[] values)
        {
            try
            {
                this.Open();
                cmd = new System.Data.SqlClient.SqlCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;

                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter(parameters[i], values[i]));
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
                cmd = new System.Data.SqlClient.SqlCommand(command, conn, tran);
                cmd.CommandTimeout = Timeout;
                System.Data.SqlClient.SqlDataReader rd = cmd.ExecuteReader();
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
