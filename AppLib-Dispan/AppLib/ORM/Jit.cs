using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.ORM
{
    public class Jit
    {
        public List<Metadados> Metadados = new List<Metadados>();
        public List<CampoValor> CampoValor = new List<CampoValor>();

        public AppLib.Data.Connection Connection = new AppLib.Data.Connection();
        private String Tabela;

        public List<Constraint> Constraints = new List<Constraint>();

        public Jit() { }

        public Jit(AppLib.Data.Connection connection, String table)
        {
            Connection = connection;
            Tabela = table;

            String Consulta = Engine.SelectMetadados(connection);

            System.Data.DataTable dt = connection.ExecQuery(Consulta, new Object[] { Tabela });

            Metadados.Clear();

            #region CARREGA LISTA DE METADADOS

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Metadados temp = new Metadados();

                temp.NomeTabela = dt.Rows[i]["NOMETABELA"].ToString();
                temp.NomeCampo = dt.Rows[i]["NOMECAMPO"].ToString();

                temp.TipoCampo = dt.Rows[i]["TIPOCAMPO"].ToString();
                temp.Tamanho = int.Parse(dt.Rows[i]["TAMANHO"].ToString());
                temp.Precisao = int.Parse(dt.Rows[i]["PRECISAO"].ToString());
                temp.Escala = int.Parse(dt.Rows[i]["ESCALA"].ToString());

                String chave = dt.Rows[i]["CHAVE"].ToString();
                if (chave.Equals("S"))
                {
                    temp.Chave = true;
                }
                else
                {
                    temp.Chave = false;
                }

                String autoincremento = dt.Rows[i]["AUTOINCREMENTO"].ToString();
                if (autoincremento.Equals("S"))
                {
                    temp.AutoIncremento = true;
                }
                else
                {
                    temp.AutoIncremento = false;
                }

                String nulo = dt.Rows[i]["NULO"].ToString();
                if (nulo.Equals("S"))
                {
                    temp.Nulo = true;
                }
                else
                {
                    temp.Nulo = false;
                }

                String StrVirtual = dt.Rows[i]["VIRTUAL"].ToString();
                if (StrVirtual.Equals("S"))
                {
                    temp.Virtual = true;
                }
                else
                {
                    temp.Virtual = false;
                }

                Metadados.Add(temp);
            }
            
            #endregion
        }

        public void Clear()
        {
            CampoValor.Clear();
        }

        public void Set(String campo, Object valor)
        {
            for (int i = 0; i < CampoValor.Count; i++)
            {
                if (CampoValor[i].Campo.Equals(campo))
                {
                    CampoValor.RemoveAt(i);
                }
            }

            CampoValor.Add(new CampoValor(campo, valor));
        }

        public Object Get(String campo)
        {
            for (int i = 0; i < CampoValor.Count; i++)
            {
                if (CampoValor[i].Campo.Equals(campo))
                {
                    return CampoValor[i].Valor;
                }
            }

            return null;
        }

        public System.Data.DataTable Select()
        {
            String Query = "SELECT ";

            for (int i = 0; i < Metadados.Count; i++)
            {
                Query += Metadados[i].NomeCampo + ", ";
            }

            Query = Query.Substring(0, Query.Length - 2);

            Query += " FROM " + Tabela + " WHERE ";

            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].Chave)
                {
                    Query += Metadados[i].NomeCampo + " = ? AND ";
                }
            }

            Query = Query.Substring(0, Query.Length - 5);

            Object[] Parametros = new Object[] { };

            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].Chave)
                {
                    for (int k = 0; k < CampoValor.Count; k++)
                    {
                        if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                        {
                            Array.Resize(ref Parametros, (Parametros.Length + 1));
                            Parametros[Parametros.Length - 1] = CampoValor[k].Valor;
                        }
                    }
                }
            }

            try
            {
                Query = Connection.ParseCommand(Query, Parametros);
                System.Data.DataTable dt = Connection.ExecQuery(Query, new Object[] { });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.Clear();

                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        for (int x = 0; x < Metadados.Count; x++)
                        {
                            if (dt.Columns[k].ColumnName.Equals(Metadados[x].NomeCampo))
                            {
                                if (dt.Rows[i][k].ToString().Equals(""))
                                {
                                    if (Metadados[x].Nulo)
                                    {
                                        CampoValor.Add(new CampoValor(dt.Columns[k].ColumnName, null));
                                    }
                                    else
                                    {
                                        CampoValor.Add(new CampoValor(dt.Columns[k].ColumnName, dt.Rows[i][k]));
                                    }
                                }
                                else
                                {
                                    CampoValor.Add(new CampoValor(dt.Columns[k].ColumnName, dt.Rows[i][k]));
                                }
                            }
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao executar comando Select.", Query, ex);
                return null;
            }
        }

        public int Insert()
        {
            String Command = "INSERT INTO " + Tabela + " ( ";

            for (int i = 0; i < Metadados.Count; i++)
            {
                if ( Metadados[i].AutoIncremento == false )
                {
                    if (Metadados[i].Virtual == false)
                    {
                        for (int k = 0; k < CampoValor.Count; k++)
                        {
                            if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                            {
                                Command += Metadados[i].NomeCampo + ", ";
                            }
                        }    
                    }
                }
            }

            Command = Command.Substring(0, Command.Length - 2);

            Command += " ) VALUES ( ";

            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].AutoIncremento == false)
                {
                    if (Metadados[i].Virtual == false)
                    {
                        for (int k = 0; k < CampoValor.Count; k++)
                        {
                            if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                            {
                                Command += "?, ";
                            }
                        }
                    }
                }
            }

            Command = Command.Substring(0, Command.Length - 2);

            Command += " )";

            Object[] Parametros = new Object[] { };

            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].AutoIncremento == false)
                {
                    if (Metadados[i].Virtual == false)
                    {
                        for (int k = 0; k < CampoValor.Count; k++)
                        {
                            if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                            {
                                Array.Resize(ref Parametros, (Parametros.Length + 1));
                                Parametros[Parametros.Length - 1] = CampoValor[k].Valor;
                            }
                        }    
                    }
                }
            }

            return Connection.ExecTransaction(Command, Parametros);
        }

        public Boolean TodosCamposSaoChave()
        {
            int ContChave = 0;

            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].Chave)
                {
                    ContChave++;
                }
            }

            if (Metadados.Count == ContChave)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean CampoChave(String Campo)
        {
            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].NomeCampo.ToUpper().Equals(Campo.ToUpper()))
                {
                    if (Metadados[i].Chave)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public int Update()
        {
            if (TodosCamposSaoChave())
            {
                // Não é necessário atualizar o registro
                return 1;
            }
            else
            {
                String Command = "UPDATE " + Tabela + " SET ";

                for (int i = 0; i < Metadados.Count; i++)
                {
                    if (Metadados[i].Chave == false)
                    {
                        if (Metadados[i].AutoIncremento == false)
                        {
                            if (Metadados[i].Virtual == false)
                            {
                                for (int k = 0; k < CampoValor.Count; k++)
                                {
                                    if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                                    {
                                        Command += Metadados[i].NomeCampo + " = ?, ";
                                    }
                                }
                            }
                        }
                    }
                }

                Command = Command.Substring(0, Command.Length - 2);

                Command += " WHERE ";

                for (int i = 0; i < Metadados.Count; i++)
                {
                    if (Metadados[i].Chave)
                    {
                        Command += Metadados[i].NomeCampo + " = ? AND ";
                    }
                }

                Command = Command.Substring(0, Command.Length - 5);

                Object[] Parametros = new Object[] { };

                for (int i = 0; i < Metadados.Count; i++)
                {
                    if (Metadados[i].Chave == false)
                    {
                        if (Metadados[i].AutoIncremento == false)
                        {
                            if (Metadados[i].Virtual == false)
                            {
                                for (int k = 0; k < CampoValor.Count; k++)
                                {
                                    if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                                    {
                                        Array.Resize(ref Parametros, (Parametros.Length + 1));
                                        Parametros[Parametros.Length - 1] = CampoValor[k].Valor;
                                    }
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < Metadados.Count; i++)
                {
                    if (Metadados[i].Chave)
                    {
                        for (int k = 0; k < CampoValor.Count; k++)
                        {
                            if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                            {
                                Array.Resize(ref Parametros, (Parametros.Length + 1));
                                Parametros[Parametros.Length - 1] = CampoValor[k].Valor;
                            }
                        }
                    }
                }

                return Connection.ExecTransaction(Command, Parametros);
            }            
        }

        public int Delete()
        {
            ValidateDelete();

            String Command = "DELETE " + Tabela + " WHERE ";

            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].Chave)
                {
                    Command += Metadados[i].NomeCampo + " = ? AND ";
                }
            }

            Command = Command.Substring(0, Command.Length - 5);

            Object[] Parametros = new Object[] { };

            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].Chave)
                {
                    for (int k = 0; k < CampoValor.Count; k++)
                    {
                        if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                        {
                            Array.Resize(ref Parametros, (Parametros.Length + 1));
                            Parametros[Parametros.Length - 1] = CampoValor[k].Valor;
                        }
                    }
                }
            }

            return Connection.ExecTransaction(Command, Parametros);
        }

        public int Count()
        {
            String Query = "SELECT COUNT(*) FROM " + Tabela + " WHERE ";

            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].Chave)
                {
                    Query += Metadados[i].NomeCampo + " = ? AND ";
                }
            }

            Query = Query.Substring(0, Query.Length - 5);

            Object[] Parametros = new Object[] { };

            for (int i = 0; i < Metadados.Count; i++)
            {
                if (Metadados[i].Chave)
                {
                    for (int k = 0; k < CampoValor.Count; k++)
                    {
                        if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                        {
                            Array.Resize(ref Parametros, (Parametros.Length + 1));
                            Parametros[Parametros.Length - 1] = CampoValor[k].Valor;
                        }
                    }
                }
            }

            System.Data.DataTable dt = Connection.ExecQuery(Query, Parametros);

            try
            {
                int temp = int.Parse(dt.Rows[0][0].ToString());
                return temp;
            }
            catch
            {
                // para o caso de campo auto incremento sempre vai dar erro.
                return 0;
            }            
        }

        public Boolean ValidateSave()
        {
            for (int i = 0; i < Metadados.Count; i++)
            {
                for (int k = 0; k < CampoValor.Count; k++)
                {
                    if (Metadados[i].NomeCampo.Equals(CampoValor[k].Campo))
                    {
                        Type TipoLinguagem = Engine.BuscarTipo(Connection, Metadados[i].TipoCampo, Metadados[i].Nulo);

                        #region VALIDATE NULLABLE

                        if (Metadados[i].AutoIncremento == false)
                        {
                            if (Metadados[i].Nulo == false)
                            {
                                if (CampoValor[k].Valor == null)
                                {
                                    throw new Exception("Campo " + Metadados[i].NomeTabela + "." + Metadados[i].NomeCampo + " não pode ser nulo.");
                                }
                            }
                        }
                        
                        #endregion

                        #region VALIDATE LENGTH

                        if (Metadados[i].TipoCampo.ToUpper().Equals("TEXT"))
                        {
                            // não fazer nada
                        }
                        else
                        {
                            if (TipoLinguagem == typeof(String))
                            {
                                if (CampoValor[k].Valor != null)
                                {
                                    if (CampoValor[k].Valor.ToString().Length > Metadados[i].Tamanho)
                                    {
                                        throw new Exception("Campo " + Metadados[i].NomeTabela + "." + Metadados[i].NomeCampo + " com " + CampoValor[k].Valor.ToString().Length + " caracteres, excedeu o limite de " + Metadados[i].Tamanho + " caracteres.");
                                    }
                                }
                            }
                        }

                        #endregion

                        #region VALIDATE VALUE

                        try
                        {
                            if ((CampoValor[k].Valor != null) && ( CampoValor[k].Valor.ToString().Equals("") == false ))
                            {
                                if (Metadados[i].Nulo)
                                {
                                    if (TipoLinguagem == typeof(int?))
                                    {
                                        int temp = int.Parse(CampoValor[k].Valor.ToString());
                                    }

                                    if (TipoLinguagem == typeof(double?))
                                    {
                                        double temp = double.Parse(CampoValor[k].Valor.ToString());
                                    }

                                    if (TipoLinguagem == typeof(DateTime?))
                                    {
                                        DateTime temp = DateTime.Parse(CampoValor[k].Valor.ToString());
                                    }

                                    if (TipoLinguagem == typeof(String))
                                    {
                                        String temp = CampoValor[k].Valor.ToString();
                                    }
                                }
                                else
                                {
                                    if (TipoLinguagem == typeof(int))
                                    {
                                        int temp = int.Parse(CampoValor[k].Valor.ToString());
                                    }

                                    if (TipoLinguagem == typeof(double))
                                    {
                                        double temp = double.Parse(CampoValor[k].Valor.ToString());
                                    }

                                    if (TipoLinguagem == typeof(DateTime))
                                    {
                                        DateTime temp = DateTime.Parse(CampoValor[k].Valor.ToString());
                                    }

                                    if (TipoLinguagem == typeof(String))
                                    {
                                        String temp = CampoValor[k].Valor.ToString();
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw new Exception("Campo " + Metadados[i].NomeTabela + "." + Metadados[i].NomeCampo + " com valor inválido.");
                        }

                        #endregion

                        #region VALIDATE FK

                        // desenvolver...

                        #endregion

                    }
                }
            }

            return true;
        }

        public Boolean ValidateDelete()
        {
            #region VALIDATE IF PK IS USED

            // desenvolver...
            
            #endregion

            return true;
        }

        public int Save()
        {
            ValidateSave();

            if (Count() == 0)
            {
                return Insert();
            }
            else
            {
                return Update();
            }
        }

        public void LoadConstraints()
        {
            Constraints = null;
            Constraints = new List<Constraint>();

            // Carregar tabelas
            String queryConstraint = Engine.SelectFK(Connection);
            String queryConstraintRef = Engine.SelectFKRef(Connection);

            System.Data.DataTable dtConstraint = Connection.ExecQuery(queryConstraint, new Object[] { Tabela });

            for (int i = 0; i < dtConstraint.Rows.Count; i++)
            {
                Constraint constraint = new Constraint();
                constraint.NomeConstraint = dtConstraint.Rows[i]["CONSTRAINTNAME"].ToString();
                constraint.TabelaPK = dtConstraint.Rows[i]["TABLEPK"].ToString();
                constraint.TabelaFK = dtConstraint.Rows[i]["TABLEFK"].ToString();

                // Carregar references
                System.Data.DataTable dtConstraintRef = Connection.ExecQuery(queryConstraintRef, new Object[] { Tabela, constraint.NomeConstraint });

                for (int k = 0; k < dtConstraintRef.Rows.Count; k++)
                {
                    ConstraintRef constraintRef = new ConstraintRef();
                    constraintRef.NomeConstraint = constraint.NomeConstraint;
                    constraintRef.TabelaPK = constraint.TabelaPK;
                    constraintRef.ColunaPK = dtConstraintRef.Rows[k]["COLUMNPK"].ToString();
                    constraintRef.TabelaFK = constraint.TabelaFK;
                    constraintRef.ColunaFK = dtConstraintRef.Rows[k]["COLUMNFK"].ToString();

                    constraint.ConstraintRef.Add(constraintRef);
                }

                Constraints.Add(constraint);
            }
        }
    }

}
