using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AppLib;

namespace AppFatureClient.Classes
{

    class Tools
    {
        public class StringToFormula
        {
            private string[] _operators = { "-", "+", "/", "*", "^" };
            private Func<double, double, double>[] _operations = {
                        (a1, a2) => a1 - a2,
                        (a1, a2) => a1 + a2,
                        (a1, a2) => a1 / a2,
                        (a1, a2) => a1 * a2,
                        (a1, a2) => Math.Pow(a1, a2)
            };

            public double Eval(string expression)
            {
                List<string> tokens = getTokens(expression);
                Stack<double> operandStack = new Stack<double>();
                Stack<string> operatorStack = new Stack<string>();
                int tokenIndex = 0;

                while (tokenIndex < tokens.Count)
                {
                    string token = tokens[tokenIndex];
                    if (token == "(")
                    {
                        string subExpr = getSubExpression(tokens, ref tokenIndex);
                        operandStack.Push(Eval(subExpr));
                        continue;
                    }
                    if (token == ")")
                    {
                        throw new ArgumentException("Mis-matched parentheses in expression");
                    }
                    //If this is an operator  
                    if (Array.IndexOf(_operators, token) >= 0)
                    {
                        while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
                        {
                            string op = operatorStack.Pop();
                            double arg2 = operandStack.Pop();
                            double arg1 = operandStack.Pop();
                            operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                        }
                        operatorStack.Push(token);
                    }
                    else
                    {
                        operandStack.Push(double.Parse(token));
                    }
                    tokenIndex += 1;
                }

                while (operatorStack.Count > 0)
                {
                    string op = operatorStack.Pop();
                    double arg2 = operandStack.Pop();
                    double arg1 = operandStack.Pop();
                    operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                }
                return operandStack.Pop();
            }

            private string getSubExpression(List<string> tokens, ref int index)
            {
                StringBuilder subExpr = new StringBuilder();
                int parenlevels = 1;
                index += 1;
                while (index < tokens.Count && parenlevels > 0)
                {
                    string token = tokens[index];
                    if (tokens[index] == "(")
                    {
                        parenlevels += 1;
                    }

                    if (tokens[index] == ")")
                    {
                        parenlevels -= 1;
                    }

                    if (parenlevels > 0)
                    {
                        subExpr.Append(token);
                    }

                    index += 1;
                }

                if ((parenlevels > 0))
                {
                    throw new ArgumentException("Mis-matched parentheses in expression");
                }
                return subExpr.ToString();
            }

            private List<string> getTokens(string expression)
            {
                string operators = "()^*/+-";
                List<string> tokens = new List<string>();
                StringBuilder sb = new StringBuilder();

                foreach (char c in expression.Replace(" ", string.Empty))
                {
                    if (operators.IndexOf(c) >= 0)
                    {
                        if ((sb.Length > 0))
                        {
                            tokens.Add(sb.ToString());
                            sb.Length = 0;
                        }
                        tokens.Add(c.ToString());
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }

                if ((sb.Length > 0))
                {
                    tokens.Add(sb.ToString());
                }
                return tokens;
            }
        }

        public static double Calc(String Equantion)
        {
            try
            {
                StringToFormula stf = new StringToFormula();
                return stf.Eval(Equantion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }

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
