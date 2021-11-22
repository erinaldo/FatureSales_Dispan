using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.New.Forms.Process
{
    public partial class frmRenumerarItens : Form
    {
        public DataTable dtItens = new DataTable();
        public DataTable dtRenumeracaoItens = new DataTable();

        int[] numerosSequenciais;

        public frmRenumerarItens()
        {
            InitializeComponent();
        }

        private void frmRenumerarItens_Load(object sender, EventArgs e)
        {
            CarregaGrid();
            ConfiguraColunasGridView();
            RenomearColunas();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dtRenumeracaoItens = gcItens.DataSource as DataTable;

            if (ValidaSequencialRepetido())
            {
                New.Class.EnviromentHelper.ItensOrdenados = dtRenumeracaoItens;

                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        private void CarregaGrid()
        {
            gcItens.DataSource = dtItens;
            gvItens.BestFitColumns();
        }

        private void ConfiguraColunasGridView()
        {
            #region Visibilidade

            gvItens.Columns["UNIDADE"].Visible = false;
            gvItens.Columns["VALORPINTURA"].Visible = false;
            gvItens.Columns["AJUSTEVALOR"].Visible = false;
            gvItens.Columns["PRECOUNITARIO"].Visible = false;
            gvItens.Columns["VALORTOTAL"].Visible = false;
            gvItens.Columns["NUMEROCCF"].Visible = false;
            gvItens.Columns["DATAENTREGA"].Visible = false;
            gvItens.Columns["SALDO_FISICO"].Visible = false;
            gvItens.Columns["SALDO_PEDIDO"].Visible = false;
            gvItens.Columns["SALDO_DISPONIVEL"].Visible = false;
            gvItens.Columns["NUMEROCFOP"].Visible = false;
            gvItens.Columns["ALIQUOTAIPI"].Visible = false;
            gvItens.Columns["QUANTIDADE"].Visible = false;
            gvItens.Columns["HISTORICOLONGO"].Visible = false;
            gvItens.Columns["PRECOUNITARIOSELEC"].Visible = false;
            gvItens.Columns["IDNAT"].Visible = false;
            gvItens.Columns["APLICACAO"].Visible = false;
            gvItens.Columns["TIPOINOX"].Visible = false;
            gvItens.Columns["TIPOPINTURA"].Visible = false;
            gvItens.Columns["CORPINTURA"].Visible = false;
            gvItens.Columns["VALORDESCORIGINAL"].Visible = false;
            gvItens.Columns["VALORDESPORIGINAL"].Visible = false;
            gvItens.Columns["TIPOAJUSTEVALOR"].Visible = false;
            gvItens.Columns["NUMPEDIDO"].Visible = false;
            gvItens.Columns["NUMITEMPEDIDO"].Visible = false;
            gvItens.Columns["DISTANCIAMENTO"].Visible = false;
            gvItens.Columns["PRECOTABELA"].Visible = false;
            gvItens.Columns["SALDO_FISICO"].Visible = false;
            gvItens.Columns["SALDO_DISPONIVEL"].Visible = false;
            gvItens.Columns["VALORTOTAL"].Visible = false;

            #endregion

            #region Edição 

            gvItens.Columns["NSEQITMMOV"].OptionsColumn.AllowEdit = false;
            gvItens.Columns["CODAUXILIAR"].OptionsColumn.AllowEdit = false;
            gvItens.Columns["PRODUTO"].OptionsColumn.AllowEdit = false;
            gvItens.Columns["IDPRD"].OptionsColumn.AllowEdit = false;
            gvItens.Columns["CODIGOPRD"].OptionsColumn.AllowEdit = false;

            #endregion
        }

        private void RenomearColunas()
        {
            gvItens.Columns["NSEQITMMOV"].Caption = "Nº Sequencial";
            gvItens.Columns["NUMEROSEQUENCIAL"].Caption = "Ordem Sequencial";
            gvItens.Columns["CODAUXILIAR"].Caption = "Código Auxiliar";
            gvItens.Columns["PRODUTO"].Caption = "Produto";
            gvItens.Columns["IDPRD"].Caption = "ID Produto";
            gvItens.Columns["CODIGOPRD"].Caption = "Código do Produto";
        }

        private bool ValidaSequencialRepetido()
        {
            bool valida = true;

            numerosSequenciais = new int[dtRenumeracaoItens.Rows.Count];

            for (int i = 0; i < dtRenumeracaoItens.Rows.Count; i++)
            {
                numerosSequenciais[i] = Convert.ToInt32(dtRenumeracaoItens.Rows[i]["NUMEROSEQUENCIAL"]);
            }

            var groups = numerosSequenciais.GroupBy(v => v);

            foreach (var g in groups)
            {
                if (g.Count() > 1)
                {
                    XtraMessageBox.Show("Item com sequencial repetido, favor verificar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valida = false;
                    return valida;
                }           
            }

            return valida;
        }

        #endregion
    }
}
