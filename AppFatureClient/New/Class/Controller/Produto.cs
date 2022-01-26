using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFatureClient.New.Class.Controller
{
    public class Produto
    {
        #region Variáveis

        private New.Class.Utilities util = new Class.Utilities();

        public DataTable dtProduto = new DataTable();
        private string sql = "";
        public string condicao = "";

        #endregion

        #region Construtor

        public Produto() { }


        #endregion

        #region Métodos

        public void CarregaGrid(GridControl control, GridView view)
        {
            dtProduto = new DataTable();

            if (!string.IsNullOrEmpty(condicao))
            {
                string query = @"

(
SELECT        
	dbo.TPRODUTO.CODCOLPRD, 
	dbo.TPRODUTODEF.CODCOLIGADA, 
	dbo.TPRODUTO.IDPRD, 
	dbo.TPRODUTO.CODIGOPRD, 
	dbo.TPRODUTO.NOMEFANTASIA, 
	/*CONVERT(DECIMAL (10,2), dbo.TPRODUTODEF.PRECO1) AS PRECO_REVENDA,*/
    /*CONVERT(DECIMAL (10,2), dbo.ZTPRODUTOCOMPL.PRECOFIXO) AS PRECO_FIXO, */
	/*CONVERT(DECIMAL (10,2), dbo.ZTPRODUTOCOMPL.PRECOCALCULADO) AS PRECO_ACABADO,*/

    dbo.TPRODUTODEF.PRECO1 AS PRECO_REVENDA,
    dbo.ZTPRODUTOCOMPL.PRECOFIXO AS PRECO_FIXO,
	dbo.ZTPRODUTOCOMPL.PRECOCALCULADO AS PRECO_ACABADO,

	dbo.ZTPRODUTOCOMPL.CODDP COD_DP,
	dbo.ZTPRODUTOCOMPL.TABELAPRECO TAB_PRECO,
	dbo.ZTPRODUTOCOMPL.CHAPA CHAPA,
	dbo.ZTPRODUTOCOMPL.ACABAMENTO ACABAMENTO,
	dbo.TPRODUTO.CODIGOAUXILIAR, 
	dbo.TPRODUTO.ULTIMONIVEL, 
	dbo.TPRODUTO.TIPO, 
	dbo.TPRODUTO.INATIVO, 
    dbo.TPRODUTO.RECCREATEDBY AS USUARIOCRIACAO, 
	dbo.TPRODUTO.RECCREATEDON AS DATACRIACAO, 
	dbo.TPRODUTO.RECMODIFIEDBY AS USUARIOALTERACAO, 
	dbo.TPRODUTO.RECMODIFIEDON AS DATAALTERACAO, 
	ROUND(dbo.TPRODUTODEF.SALDOGERALFISICO,2) SALDOGERALFISICO ,
	CASE 
		TPRODUTODEF.CODTB2FAT 
			WHEN '02' THEN 'ACABADO' 
			WHEN '03' THEN 'REVENDA' 
	END TIPOPROD,

	CASE 
		TPRDFISCAL.SITUACAOMERCADORIA 
			WHEN '01' THEN 'REVENDA'
			WHEN '04' THEN 'ACABADO'
			WHEN '03' THEN 'ELABORAÇÃO'
			WHEN '02' THEN 'MATERIA PRIMA'
			WHEN '15' THEN 'OUTROS INSUMOS'
			WHEN '10' THEN 'PRODUTO INTERMEDIÁRIO'
	END SITUACAOMERCADORIA,
	(CASE WHEN dbo.ZTPRODUTOCOMPL.CALCCOMOACABADO = 1 THEN 'SIM' ELSE 'NÃO' END) CALCULACOMOACABADO,
	(CASE WHEN dbo.ZTPRODUTOCOMPL.USAPRECOFIXO = 1 THEN 'SIM' ELSE 'NÃO' END) USAPRECOFIXO

FROM            
	dbo.TPRODUTO (NOLOCK)
	INNER JOIN dbo.TPRODUTODEF (NOLOCK)
	ON dbo.TPRODUTO.CODCOLPRD = dbo.TPRODUTODEF.CODCOLIGADA AND dbo.TPRODUTO.IDPRD = dbo.TPRODUTODEF.IDPRD  
	LEFT OUTER JOIN dbo.ZTPRODUTOCOMPL (NOLOCK)
	ON dbo.TPRODUTO.CODCOLPRD = dbo.ZTPRODUTOCOMPL.CODCOLIGADA AND  dbo.TPRODUTO.IDPRD = dbo.ZTPRODUTOCOMPL.IDPRD
	INNER JOIN TPRDFISCAL (NOLOCK)
	ON TPRODUTO.CODCOLPRD = TPRDFISCAL.CODCOLIGADA AND TPRODUTO.IDPRD = TPRDFISCAL.IDPRD
WHERE        
	dbo.TPRODUTO.CODCOLPRD = 1

) ZVWTPRD

";

                //sql = util.GetVisaoUsuario("ZVWTPRD");

                sql = util.GetVisaoUsuario("ZVWTPRD", query);

                if (condicao.Contains("PRODUTO ACABADO"))
                {
                    condicao = condicao.Replace("PRODUTO ACABADO", "ACABADO");
                }

                dtProduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql + condicao, new object[] { });
            }

            control.DataSource = dtProduto;

            if (view.Columns.Count == 0)
            {
                view = control.MainView as GridView;
            }

            util.GetPropriedadesVisaoUsuario("ZVWTPRD", view);

            view.OptionsBehavior.Editable = true;
        }

        public GridView ConfiguraGridViewPadrao(GridControl control, GridView view)
        {
            // Habilita a edição do componente
            view.OptionsBehavior.Editable = true;

            // Desabilita/Habilita os campos conforme solicitação do processo de Atualização de Preço
            view.Columns["COD_DP"].OptionsColumn.AllowEdit = true;

            CarregaComboBoxCodDP(control, view);

            view.Columns["CODCOLPRD"].OptionsColumn.AllowEdit = false;
            view.Columns["CODCOLIGADA"].OptionsColumn.AllowEdit = false;
            view.Columns["IDPRD"].OptionsColumn.AllowEdit = false;
            view.Columns["CODIGOPRD"].OptionsColumn.AllowEdit = false;
            view.Columns["CODIGOAUXILIAR"].OptionsColumn.AllowEdit = false;
            view.Columns["NOMEFANTASIA"].OptionsColumn.AllowEdit = false;
            view.Columns["TIPOPROD"].OptionsColumn.AllowEdit = false;
            view.Columns["SITUACAOMERCADORIA"].OptionsColumn.AllowEdit = false;
            view.Columns["CHAPA"].OptionsColumn.AllowEdit = false;
            view.Columns["ACABAMENTO"].OptionsColumn.AllowEdit = false;
            view.Columns["TAB_PRECO"].OptionsColumn.AllowEdit = false;
            view.Columns["PRECO_FIXO"].OptionsColumn.AllowEdit = false;
            view.Columns["PRECO_ACABADO"].OptionsColumn.AllowEdit = false;
            view.Columns["PRECO_REVENDA"].OptionsColumn.AllowEdit = false;
            view.Columns["CALCULACOMOACABADO"].OptionsColumn.AllowEdit = false;
            view.Columns["USAPRECOFIXO"].OptionsColumn.AllowEdit = false;
            view.Columns["ULTIMONIVEL"].OptionsColumn.AllowEdit = false;
            view.Columns["TIPO"].OptionsColumn.AllowEdit = false;
            view.Columns["INATIVO"].OptionsColumn.AllowEdit = false;
            view.Columns["USUARIOCRIACAO"].OptionsColumn.AllowEdit = false;
            view.Columns["DATACRIACAO"].OptionsColumn.AllowEdit = false;
            view.Columns["USUARIOALTERACAO"].OptionsColumn.AllowEdit = false;
            view.Columns["DATAALTERACAO"].OptionsColumn.AllowEdit = false;
            view.Columns["SALDOGERALFISICO"].OptionsColumn.AllowEdit = false;

            return view;
        }

        public GridView ConfiguraGridViewPreco(GridView view, DataRow row)
        {
            // Obtém o Tipo de Produto
            string tipo = row["TIPOPROD"].ToString();

            int UsaPrecoFixo = 0;
            int calculadoComoAcabado = 0;

            // Habilita a edição do componente
            view.OptionsBehavior.Editable = true;

            // Desabilita/Habilita os campos conforme solicitação do processo de Atualização de Preço
            view.Columns["CODCOLPRD"].OptionsColumn.AllowEdit = false;
            view.Columns["CODCOLIGADA"].OptionsColumn.AllowEdit = false;
            view.Columns["IDPRD"].OptionsColumn.AllowEdit = false;
            view.Columns["CODIGOPRD"].OptionsColumn.AllowEdit = false;
            view.Columns["CODIGOAUXILIAR"].OptionsColumn.AllowEdit = false;
            view.Columns["NOMEFANTASIA"].OptionsColumn.AllowEdit = false;
            view.Columns["TIPOPROD"].OptionsColumn.AllowEdit = false;
            view.Columns["SITUACAOMERCADORIA"].OptionsColumn.AllowEdit = false;
            view.Columns["CHAPA"].OptionsColumn.AllowEdit = false;
            view.Columns["ACABAMENTO"].OptionsColumn.AllowEdit = false;
            view.Columns["TAB_PRECO"].OptionsColumn.AllowEdit = false;
            view.Columns["CALCULACOMOACABADO"].OptionsColumn.AllowEdit = false;
            view.Columns["USAPRECOFIXO"].OptionsColumn.AllowEdit = false;
            view.Columns["ULTIMONIVEL"].OptionsColumn.AllowEdit = false;
            view.Columns["TIPO"].OptionsColumn.AllowEdit = false;
            view.Columns["INATIVO"].OptionsColumn.AllowEdit = false;
            view.Columns["USUARIOCRIACAO"].OptionsColumn.AllowEdit = false;
            view.Columns["DATACRIACAO"].OptionsColumn.AllowEdit = false;
            view.Columns["USUARIOALTERACAO"].OptionsColumn.AllowEdit = false;
            view.Columns["DATAALTERACAO"].OptionsColumn.AllowEdit = false;
            view.Columns["SALDOGERALFISICO"].OptionsColumn.AllowEdit = false;

            //Formatação e Edição das Colunas
            var edit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edit.Mask.EditMask = "n2";

            view.Columns["PRECO_FIXO"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            view.Columns["PRECO_FIXO"].DisplayFormat.FormatString = "n2";           
            view.Columns["PRECO_FIXO"].ColumnEdit = edit;

            view.Columns["PRECO_ACABADO"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            view.Columns["PRECO_ACABADO"].DisplayFormat.FormatString = "n2";
            view.Columns["PRECO_ACABADO"].ColumnEdit = edit;

            view.Columns["PRECO_REVENDA"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            view.Columns["PRECO_REVENDA"].DisplayFormat.FormatString = "n2";
            view.Columns["PRECO_REVENDA"].ColumnEdit = edit;

            view.Columns["COD_DP"].OptionsColumn.AllowEdit = true;

            if (tipo == "ACABADO")
            {
                UsaPrecoFixo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT USAPRECOFIXO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]) }));

                // Se o parâmetro Preço Fixo estiver marcado, atualizar o campo PRECOFIXO da tabela ZTPRODUTOCOMPL
                if (UsaPrecoFixo > 0)
                {
                    view.Columns["PRECO_FIXO"].OptionsColumn.AllowEdit = true;
                    view.Columns["PRECO_ACABADO"].OptionsColumn.AllowEdit = false;
                    view.Columns["PRECO_REVENDA"].OptionsColumn.AllowEdit = false;

                    return view;

                }
                else
                {
                    view.Columns["PRECO_FIXO"].OptionsColumn.AllowEdit = false;
                    view.Columns["PRECO_ACABADO"].OptionsColumn.AllowEdit = false;
                    view.Columns["PRECO_REVENDA"].OptionsColumn.AllowEdit = false;

                    return view;
                }
            }

            if (tipo == "REVENDA")
            {
                calculadoComoAcabado = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT CALCCOMOACABADO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]) }));

                // Se o parâmetro Calculado Como Acabado estiver marcado, não atualizar o preço, do contrário, atualizar o campo 
                if (calculadoComoAcabado == 0)
                {
                    view.Columns["PRECO_REVENDA"].OptionsColumn.AllowEdit = true;
                    view.Columns["PRECO_ACABADO"].OptionsColumn.AllowEdit = false;
                    view.Columns["PRECO_FIXO"].OptionsColumn.AllowEdit = false;

                    return view;
                }
                else
                {
                    view.Columns["PRECO_ACABADO"].OptionsColumn.AllowEdit = false;
                    view.Columns["PRECO_FIXO"].OptionsColumn.AllowEdit = false;
                    view.Columns["PRECO_REVENDA"].OptionsColumn.AllowEdit = false;

                    return view;
                }
            }
            else
            {
                view.Columns["PRECO_ACABADO"].OptionsColumn.AllowEdit = false;
                view.Columns["PRECO_FIXO"].OptionsColumn.AllowEdit = false;
                view.Columns["PRECO_REVENDA"].OptionsColumn.AllowEdit = false;

                return view;
            }
        }

        public GridView ConfiguraGridViewRegraProduto(GridControl control, GridView view, DataRow row)
        {
            // Obtém o valor do Código DP
            string codDP = row["COD_DP"].ToString();

            DataTable dtRegra = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT FLAGCHAPA AS 'CHAPA', FLAGACABAMENTO AS 'ACABAMENTO' FROM ZTPRODUTOREGRA WHERE CODCOLIGADA = ? AND CODDP = ?", new object[] { AppLib.Context.Empresa, codDP });

            if (dtRegra.Rows.Count > 0)
            {
                // Valida o flag da Chapa
                if (Convert.ToInt32(dtRegra.Rows[0]["CHAPA"]) == 1)
                {
                    CarregaComboBoxChapaGrid(control, view);
                }

                // Valida o flag do Acabamento
                if (Convert.ToInt32(dtRegra.Rows[0]["ACABAMENTO"]) == 1)
                {
                    CarregaComboBoxAcabamentoGrid(control, view);
                }
            }

            return view;
        }

        public void AtualizaPreco(GridView view, DataRow row)
        {
            string tipo = row["TIPOPROD"].ToString();

            int UsaPrecoFixo = 0;
            int calculadoComoAcabado = 0;

            decimal valorAtualizado = 0;

            if (tipo == "ACABADO")
            {
                UsaPrecoFixo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT USAPRECOFIXO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]) }));

                // Se o parâmetro Preço Fixo estiver marcado, atualizar o campo PRECOFIXO da tabela ZTPRODUTOCOMPL
                if (UsaPrecoFixo > 0)
                {
                    valorAtualizado = Convert.ToDecimal(row["PRECO_FIXO"]);

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZTPRODUTOCOMPL SET PRECOFIXO = ? WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { valorAtualizado, AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]) });

                    return;
                }
                else
                {
                    return;
                }
            }
            else if (tipo == "REVENDA")
            {
                calculadoComoAcabado = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT CALCCOMOACABADO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]) }));

                // Se o parâmetro Calculado Como Acabado estiver marcado, não atualizar o preço, do contrário, atualizar o campo 
                if (calculadoComoAcabado == 0)
                {
                    valorAtualizado = Convert.ToDecimal(row["PRECO_REVENDA"]);

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE TPRODUTODEF SET PRECO1 = ? WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { valorAtualizado, AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]) });

                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        public void AtualizaRegraProduto(GridView view, DataRow row, string colunaSelecionada)
        {
            string codDP = row["COD_DP"].ToString();

            string valorParametro = "";

            if (colunaSelecionada == "CHAPA")
            {
                valorParametro = row[colunaSelecionada].ToString();

                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZTPRODUTOCOMPL SET CHAPA = ? WHERE CODCOLIGADA = ? AND IDPRD = ? AND CODDP = ? ", new object[] { valorParametro, AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]), codDP });

                return;
            }
            else
            {
                valorParametro = row[colunaSelecionada].ToString();

                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZTPRODUTOCOMPL SET ACABAMENTO = ? WHERE CODCOLIGADA = ? AND IDPRD = ? AND CODDP = ? ", new object[] { valorParametro, AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]), codDP });

                return;
            }
        }

        public void AtualizaCodigoDP(GridView view, DataRow row)
        {
            string codDP = row["COD_DP"].ToString();

            try
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZTPRODUTOCOMPL SET CODDP = ? WHERE CODCOLIGADA = ? AND IDPRD = ? ", new object[] { codDP, AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]) });
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        public GridView HabilitarCelulaProduto(GridView view, string coluna)
        {
            // Habilita a edição da célula da Grid
            view.Columns[coluna].OptionsColumn.AllowEdit = true;

            return view;
        }

        private void CarregaComboBoxCodDP(GridControl control, GridView view)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemComboBox combo = control.RepositoryItems.Add("ComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemComboBox;

            combo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            DataTable dtDP = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DISTINCT(CODDP) CODDP FROM ZTPRODUTOREGRA WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa });

            for (int i = 0; i < dtDP.Rows.Count; i++)
            {
                combo.Items.Add(dtDP.Rows[i]["CODDP"].ToString());
            }

            control.RepositoryItems.Add(combo);
            view.Columns["COD_DP"].ColumnEdit = combo;
        }

        private void CarregaComboBoxChapaGrid(GridControl control, GridView view)
        {
            HabilitarCelulaProduto(view, "CHAPA");

            DevExpress.XtraEditors.Repository.RepositoryItemComboBox combo = control.RepositoryItems.Add("ComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemComboBox;

            combo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            combo.Items.Add("A");
            combo.Items.Add("B");
            combo.Items.Add("C");
            combo.Items.Add("D");
            combo.Items.Add("E");
            combo.Items.Add("F");
            combo.Items.Add("G");
            combo.Items.Add("H");
            combo.Items.Add("X");

            control.RepositoryItems.Add(combo);
            view.Columns["CHAPA"].ColumnEdit = combo;
        }

        private void CarregaComboBoxAcabamentoGrid(GridControl control, GridView view)
        {
            HabilitarCelulaProduto(view, "ACABAMENTO");

            DevExpress.XtraEditors.Repository.RepositoryItemComboBox combo = control.RepositoryItems.Add("ComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemComboBox;

            combo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            combo.Items.Add("PZ");
            combo.Items.Add("GE");
            combo.Items.Add("NAT");
            combo.Items.Add("INOX");
            combo.Items.Add("GF");
            combo.Items.Add("AL");
            combo.Items.Add("GL");
            combo.Items.Add("PVC");
            combo.Items.Add("MIN");
            combo.Items.Add("BC");
            combo.Items.Add("CD");
            combo.Items.Add("GEOMET");

            control.RepositoryItems.Add(combo);
            view.Columns["ACABAMENTO"].ColumnEdit = combo;
        }

        public DataRowCollection GetDataRowCollection(GridView view)
        {
            DataTable dt = new DataTable();
            int[] handles = view.GetSelectedRows();

            for (int i = 0; i < handles.Length; i++)
            {
                if (i == 0)
                {
                    for (int col = 0; col < view.GetDataRow(handles[i]).Table.Columns.Count; col++)
                    {
                        dt.Columns.Add(view.GetDataRow(handles[i]).Table.Columns[col].ColumnName);
                    }
                }

                if (handles[i] >= 0)
                {
                    dt.Rows.Add(view.GetDataRow(handles[i]).ItemArray);
                }
            }

            return dt.Rows;
        }

        #endregion
    }
}
