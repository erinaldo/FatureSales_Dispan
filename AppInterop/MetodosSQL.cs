using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using AppLib;

namespace AppInterop
{
    class MetodosSQL
    {
        public static string CS { get; set; }

        public static SqlConnection GetConnection()
        {
            SqlConnection conection = new SqlConnection();

            conection.ConnectionString = CS;

            try
            {
                conection.Open();
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                conection.Close();
            }

            return conection;

        }

        public static void ExecQuery(string sql)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = CS;
            cmd = new SqlCommand(sql, con);

            int i = 0;

            try
            {
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }
        }

        public static Object ExecScalar(string sql)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = CS;
            cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                object i = cmd.ExecuteScalar();
                return i;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public static void ExecMultiple(List<String> sql)
        {
            string LastQuery = String.Empty;
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                SqlTransaction transaction;

                con.ConnectionString = CS;

                con.Open();

                transaction = con.BeginTransaction("MetodosSQL");

                try
                {


                    cmd.Connection = con;
                    cmd.Transaction = transaction;

                    foreach (String query in sql)
                    {
                        cmd.CommandText = query;
                        LastQuery = query;
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message, ex);
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception(ex2.Message, ex2);
                    }
                }
                finally
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Query: " + LastQuery, ex);
            }

        }

        public static object GetObject(String sql, String field)
        {
            object retorno = new object();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = CS;

            try
            {
                cmd = new SqlCommand(sql, con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    retorno = dr[field];
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }

            return retorno;
        }

        public static string GetField(String sql, String field)
        {
            string retorno = string.Empty;

            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = CS;

            try
            {
                cmd = new SqlCommand(sql, con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    retorno = Convert.ToString(String.Format("{0}", dr[field]));
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }

            return retorno;
        }

        public static DataTable GetDT(String sql)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = CS;
            try
            {
                cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 120;
                con.Open();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }
        }

        public static Boolean VerificaPermissao(String _processo)
        {
            try
            {
                string sql = String.Format(@"select count(1) as 'CONT' from ZUSUARIO ZU

                                             inner join ZPROCESSOPERFIL ZPP
                                             on ZPP.CODPERFIL = ZU.CODPERFIL

                                             where ZPP.CODPROCESSO = '{0}'
                                             and ZU.USUARIO = '{1}'", _processo, AppLib.Context.Usuario);

                int i = int.Parse(MetodosSQL.GetField(sql, "CONT"));

                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public static bool ExisteMovimento(int CodColigada, int IdMov)
        {
            try
            {
                bool mov = int.Parse(GetField(String.Format(@"SELECT Count(IDMOV) as 'CONT' FROM TMOV WHERE CODCOLIGADA = {0} AND IDMOV = {1}", CodColigada, IdMov), "CONT")) > 0;

                return mov;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}