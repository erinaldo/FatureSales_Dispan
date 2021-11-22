using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;

namespace AppFatureClient.New.Class
{
    public class Utilities
    {
        public TITMMOV TITMMOV = new TITMMOV();

        public bool ExisteCampo(AppLib.Windows.CampoLookup lookup)
        {
            bool valida = true;

            if (string.IsNullOrEmpty(lookup.textBoxDESCRICAO.Text))
            {
                valida = false;
            }

            return valida;
        }

        public string GetVisaoUsuario(string tabela)
        {
            string sql = "";

            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM ZVISAOUSUARIO WHERE GRID = ? AND USUARIO = ? AND FILTRO = '' ORDER BY SEQUENCIA ASC", new object[] { tabela, AppLib.Context.Usuario });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(sql))
                    {
                        sql = "SELECT " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
                    }
                    else
                    {
                        sql = sql + ", " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
                    }
                }

                sql = sql + " FROM " + tabela + "";

                return sql;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string GetVisaoUsuario(string tabela, string query)
        {
            string sql = "";

            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM ZVISAOUSUARIO WHERE GRID = ? AND USUARIO = ? AND FILTRO = '' ORDER BY SEQUENCIA ASC", new object[] { tabela, AppLib.Context.Usuario });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(sql))
                    {
                        sql = "SELECT " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
                    }
                    else
                    {
                        sql = sql + ", " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
                    }
                }

                sql = sql + " FROM " + query + "";

                return sql;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void GetPropriedadesVisaoUsuario(string tabela, GridView view)
        {
            int sequencia = 0;
            int visivel = 0;
            int largura = 0;
            string coluna = "";

            DataTable dtPropriedadesUsuario = null;

            try
            {
                dtPropriedadesUsuario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZVISAOUSUARIO WHERE GRID = ? AND USUARIO = ? AND FILTRO = '' ORDER BY SEQUENCIA ASC", new object[] { tabela, AppLib.Context.Usuario });

                if (dtPropriedadesUsuario.Rows.Count > 0)
                {
                    // esconde todas as colunas da grid
                    //for (int i = 0; i < view.Columns.Count; i++)
                    //{
                    //    view.Columns[i].Visible = false;
                    //}

                    for (int i = 0; i < dtPropriedadesUsuario.Rows.Count; i++)
                    {
                        coluna = dtPropriedadesUsuario.Rows[i]["COLUNA"].ToString();
                        sequencia = Convert.ToInt32(dtPropriedadesUsuario.Rows[i]["SEQUENCIA"]);
                        visivel = Convert.ToInt32(dtPropriedadesUsuario.Rows[i]["VISIVEL"]);
                        largura = Convert.ToInt32(dtPropriedadesUsuario.Rows[i]["LARGURA"]);

                        try
                        {
                            // Visível
                            if (visivel == 1)
                            {
                                view.Columns[i].Visible = true;
                            }
                            else
                            {
                                view.Columns[i].Visible = false;
                            }

                            // Sequência 
                            //view.Columns[i].VisibleIndex = sequencia;

                            // Largura
                            view.Columns[coluna].Width = largura;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SalvarDisposicao(string tabela, GridView view)
        {
            // Atualiza a Visão do usuário para não ter visibilidade de nenhuma coluna, em seguida, atribui novamente a visibilidade às colunas visíveis.
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZVISAOUSUARIO SET VISIVEL = 0, VISIBILIDADE = 0 WHERE USUARIO = ? AND GRID = ? AND FILTRO = ''", new object[] { AppLib.Context.Usuario, tabela });

            for (int i = 0; i < view.VisibleColumns.Count; i++)
            {
                try
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZVISAOUSUARIO SET SEQUENCIA = ?, LARGURA = ?, VISIVEL = ?, VISIBILIDADE = ? WHERE GRID = ? AND USUARIO = ? AND COLUNA = ? AND FILTRO = ''", new object[] { i, view.VisibleColumns[i].Width, 1, 1, tabela, AppLib.Context.Usuario, view.VisibleColumns[i].FieldName });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SalvarSequenciaVisaoUsuario(string tabela, GridView view)
        {
            // Atualiza a Visão do usuário para não ter visibilidade de nenhuma coluna, em seguida, atribui novamente a visibilidade às colunas visíveis.
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZVISAOUSUARIO SET VISIVEL = 0, VISIBILIDADE = 0 WHERE USUARIO = ? AND GRID = ? AND FILTRO = ''", new object[] { AppLib.Context.Usuario, tabela });

            for (int i = 0; i < view.VisibleColumns.Count; i++)
            {
                try
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZVISAOUSUARIO SET SEQUENCIA = ?, VISIVEL = 1, VISIBILIDADE = 1 WHERE GRID = ? AND USUARIO = ? AND COLUNA = ? AND FILTRO = ''", new object[] { i, tabela, AppLib.Context.Usuario, view.VisibleColumns[i].FieldName });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SalvarLarguraVisaoUsuario(int tamanho, string tabela, string coluna)
        {
            try
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZVISAOUSUARIO SET LARGURA = ? WHERE GRID = ? AND USUARIO = ? AND COLUNA = ? AND FILTRO = ''", new object[] { tamanho, tabela, AppLib.Context.Usuario, coluna });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetVisaoUsuario(GridView view, string tabela)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT SEQUENCIA, COLUNA, LARGURA, VISIBILIDADE AS VISIVEL FROM ZVISAOUSUARIO WHERE GRID = ? AND USUARIO = ? ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        dt.PrimaryKey = new DataColumn[] { dt.Columns["COLUNA"] };
                        DataRow result = dt.Rows.Find(new object[] { view.Columns[i].FieldName.ToString() });

                        if (result != null)
                        {
                            if (Convert.ToInt32(result["VISIVEL"]) == 1)
                            {
                                view.Columns[i].VisibleIndex = Convert.ToInt32(result["SEQUENCIA"]);
                                view.Columns[i].Width = Convert.ToInt32(result["LARGURA"]);
                            }
                            else
                            {
                                view.Columns[i].Visible = false;
                            }
                        }
                    }

                    SetOrdenacao(view, GetOrdenacaoVisaoUsuario());
                }
                else
                {
                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        AppLib.ORM.Jit ZVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "ZVISAOUSUARIO");

                        ZVISAOUSUARIO.Set("GRID", "ITENS");
                        ZVISAOUSUARIO.Set("USUARIO", AppLib.Context.Usuario);
                        ZVISAOUSUARIO.Set("COLUNA", view.Columns[i].FieldName);
                        ZVISAOUSUARIO.Set("FILTRO", "");

                        if (view.Columns[i].FieldName == "NSEQITMMOV")
                        {
                            ZVISAOUSUARIO.Set("LARGURA", 50);
                        }
                        else if (view.Columns[i].FieldName == "NUMEROSEQUENCIAL")
                        {
                            ZVISAOUSUARIO.Set("LARGURA", 80);
                        }
                        else if (view.Columns[i].FieldName == "CODAUXILIAR")
                        {
                            ZVISAOUSUARIO.Set("LARGURA", 110);
                        }
                        else if (view.Columns[i].FieldName == "PRODUTO")
                        {
                            ZVISAOUSUARIO.Set("LARGURA", 400);
                        }
                        else if (view.Columns[i].FieldName == "UNIDADE")
                        {
                            ZVISAOUSUARIO.Set("LARGURA", 50);
                        }
                        else
                        {
                            ZVISAOUSUARIO.Set("LARGURA", 100);
                        }

                        ZVISAOUSUARIO.Set("VISIVEL", 1);
                        ZVISAOUSUARIO.Set("VISIBILIDADE", 1);
                        ZVISAOUSUARIO.Set("SEQUENCIA", i);

                        if (view.Columns[i].SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
                        {
                            ZVISAOUSUARIO.Set("ORDENACAO", "DESC");
                        }
                        else if (view.Columns[i].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
                        {
                            ZVISAOUSUARIO.Set("ORDENACAO", "ASC");
                        }
                        else
                        {
                            ZVISAOUSUARIO.Set("ORDENACAO", "");
                        }

                        ZVISAOUSUARIO.Set("ALINHAMENTO", "E");
                        ZVISAOUSUARIO.Set("AGRUPAR", 0);
                        ZVISAOUSUARIO.Set("FORMATO", "");

                        ZVISAOUSUARIO.Save();
                    }
                }
            }
        }

        public string GetOrdenacaoVisaoUsuario()
        {
            string colunaOrdenada = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT CONCAT(COLUNA,' ', ORDENACAO) AS 'COLUNA_ORDENADA' 
                                                                                                    FROM ZVISAOUSUARIO
                                                                                                    WHERE ORDENACAO <> ''").ToString();

            return colunaOrdenada;
        }

        private void SetOrdenacao(GridView view, string ordenacao)
        {
            view.ClearSorting();

            string coluna = "";
            string valorOrdenacao = "";

            int espaco = ordenacao.IndexOf(" ");

            string[] valoresOrdenacao = ordenacao.Split(new char[0]);

            if (!string.IsNullOrEmpty(ordenacao))
            {
                coluna = valoresOrdenacao[0];
                valorOrdenacao = valoresOrdenacao[1];

                switch (valorOrdenacao)
                {
                    case "ASC":
                        view.Columns[coluna].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                        break;
                    case "DESC":
                        view.Columns[coluna].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                        break;
                    default:
                        break;
                }
            }
        }

        public void RenomearColunasGridView(GridView view, string tabela)
        {
            switch (tabela)
            {
                case "ZTPRODUTOTABPRECO":

                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        if (view.Columns[i].FieldName == "CODCOLIGADA")
                        {
                            view.Columns[i].Caption = "Código da Coligada";
                        }

                        if (view.Columns[i].FieldName == "CODIGO")
                        {
                            view.Columns[i].Caption = "Código da Tabela de Preço";
                        }

                        if (view.Columns[i].FieldName == "ACABAMENTO")
                        {
                            view.Columns[i].Caption = "Acabamento";
                        }

                        if (view.Columns[i].FieldName == "INICIOVIGENCIA")
                        {
                            view.Columns[i].Caption = "Início Vigência";
                        }

                        if (view.Columns[i].FieldName == "FIMVIGENCIA")
                        {
                            view.Columns[i].Caption = "Fim Vigência";
                        }

                        if (view.Columns[i].FieldName == "TIPO")
                        {
                            view.Columns[i].Caption = "Tipo";
                        }

                        if (view.Columns[i].FieldName == "INATIVO")
                        {
                            view.Columns[i].Caption = "Status";
                        }

                        if (view.Columns[i].FieldName == "VIGENTE")
                        {
                            view.Columns[i].Caption = "Vigente";
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        public void RenomearColunasDetalhesGridView(GridView view, string tabela)
        {
            switch (tabela)
            {
                case "ZTPRODUTOTABPRECOCOMPL":

                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        if (view.Columns[i].FieldName == "CODCOLIGADA")
                        {
                            view.Columns[i].Caption = "Código da Coligada";
                        }

                        if (view.Columns[i].FieldName == "CHAPA")
                        {
                            view.Columns[i].Caption = "Chapa";
                        }

                        if (view.Columns[i].FieldName == "DESCRICAOCHAPA")
                        {
                            view.Columns[i].Caption = "Descrição";
                        }

                        if (view.Columns[i].FieldName == "PESO")
                        {
                            view.Columns[i].Caption = "Peso";
                        }

                        if (view.Columns[i].FieldName == "PRECOKG")
                        {
                            view.Columns[i].Caption = "Preço KG";
                        }
                    }

                    ConfigurarGridView(view, tabela);

                    break;
                default:
                    break;
            }
        }

        private void ConfigurarGridView(GridView view, string tabela)
        {
            switch (tabela)
            {
                case "ZTPRODUTOTABPRECOCOMPL":

                    // Visibilidade das Colunas
                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        if (view.Columns[i].FieldName == "ID")
                        {
                            view.Columns[i].Visible = false;
                        }
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
