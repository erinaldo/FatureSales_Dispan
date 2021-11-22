using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.New.Forms
{
    public partial class frmSelecaoColunas : Form
    {
        private string tabela;
        DataTable dt = new DataTable();
        public frmSelecaoColunas(string _tabela)
        {
            InitializeComponent();
            tabela = _tabela;
            carregaGrid();
            RenomeiaColunasGridView();
            CriaColunaOrdenacao();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //coluna, descrição, visivel
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            DataRow row2 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()) - 1);
            object COLUNA1 = row1["COLUNA"];
            //object DESCRICAO1 = row1["DESCRICAO"];
            object VISIVEL1 = row1["VISIVEL"];
            object LARGURA1 = row1["LARGURA"];

            object COLUNA2 = row2["COLUNA"];
            //object DESCRICAO2 = row2["DESCRICAO"];
            object VISIVEL2 = row2["VISIVEL"];
            object LARGURA2 = row2["LARGURA"];

            row1["COLUNA"] = COLUNA2;
            //row1["DESCRICAO"] = DESCRICAO2;
            row1["VISIVEL"] = VISIVEL2;
            row1["LARGURA"] = LARGURA2;

            row2["COLUNA"] = COLUNA1;
            //row2["DESCRICAO"] = DESCRICAO1;
            row2["VISIVEL"] = VISIVEL1;
            row2["LARGURA"] = LARGURA1;

            gridView1.FocusedRowHandle = Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()) - 1;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //coluna, descrição, visivel
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            DataRow row2 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()) + 1);
            object COLUNA1 = row1["COLUNA"];
            //object DESCRICAO1 = row1["DESCRICAO"];
            object VISIVEL1 = row1["VISIVEL"];
            object LARGURA1 = row1["LARGURA"];

            object COLUNA2 = row2["COLUNA"];
            //object DESCRICAO2 = row2["DESCRICAO"];
            object VISIVEL2 = row2["VISIVEL"];
            object LARGURA2 = row2["LARGURA"];

            row1["COLUNA"] = COLUNA2;
            //row1["DESCRICAO"] = DESCRICAO2;
            row1["VISIVEL"] = VISIVEL2;
            row1["LARGURA"] = LARGURA2;

            row2["COLUNA"] = COLUNA1;
            //row2["DESCRICAO"] = DESCRICAO1;
            row2["VISIVEL"] = VISIVEL1;
            row2["LARGURA"] = LARGURA1;

            gridView1.FocusedRowHandle = Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()) + 1;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //coluna, descrição, visivel
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            DataRow row2 = gridView1.GetDataRow(0);
            object COLUNA1 = row1["COLUNA"];
            //object DESCRICAO1 = row1["DESCRICAO"];
            object VISIVEL1 = row1["VISIVEL"];
            object LARGURA1 = row1["LARGURA"];

            object COLUNA2 = row2["COLUNA"];
            //object DESCRICAO2 = row2["DESCRICAO"];
            object VISIVEL2 = row2["VISIVEL"];
            object LARGURA2 = row2["LARGURA"];

            row1["COLUNA"] = COLUNA2;
            //row1["DESCRICAO"] = DESCRICAO2;
            row1["VISIVEL"] = VISIVEL2;
            row1["LARGURA"] = LARGURA2;

            row2["COLUNA"] = COLUNA1;
            //row2["DESCRICAO"] = DESCRICAO1;
            row2["VISIVEL"] = VISIVEL1;
            row2["LARGURA"] = LARGURA1;

            gridView1.FocusedRowHandle = 0;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            //coluna, descrição, visivel
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            DataRow row2 = gridView1.GetDataRow(gridView1.RowCount - 1);
            object COLUNA1 = row1["COLUNA"];
            //object DESCRICAO1 = row1["DESCRICAO"];
            object VISIVEL1 = row1["VISIVEL"];
            object LARGURA1 = row1["LARGURA"];

            object COLUNA2 = row2["COLUNA"];
            //object DESCRICAO2 = row2["DESCRICAO"];
            object VISIVEL2 = row2["VISIVEL"];
            object LARGURA2 = row2["LARGURA"];

            row1["COLUNA"] = COLUNA2;
            //row1["DESCRICAO"] = DESCRICAO2;
            row1["VISIVEL"] = VISIVEL2;
            row1["L/ARGURA"] = LARGURA2;

            row2["COLUNA"] = COLUNA1;
            //row2["DESCRICAO"] = DESCRICAO1;
            row2["VISIVEL"] = VISIVEL1;
            row2["LARGURA"] = LARGURA1;

            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
        }

        private void btnChechedAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.GetDataRow(i)["VISIVEL"] = true;
            }
        }

        private void btnUncheckedAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.GetDataRow(i)["VISIVEL"] = false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidaColunaOrdenacao())
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("Processando");

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    AppLib.ORM.Jit ZVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "ZVISAOUSUARIO");

                    ZVISAOUSUARIO.Set("GRID", tabela);
                    ZVISAOUSUARIO.Set("USUARIO", AppLib.Context.Usuario);
                    ZVISAOUSUARIO.Set("COLUNA", gridView1.GetDataRow(i)["COLUNA"]);
                    ZVISAOUSUARIO.Set("FILTRO", "");
                    ZVISAOUSUARIO.Set("SEQUENCIA", gridView1.GetDataRow(i)["SEQUENCIA"]);
                    ZVISAOUSUARIO.Set("VISIVEL", gridView1.GetDataRow(i)["VISIVEL"].Equals(false) ? 0 : 1);
                    ZVISAOUSUARIO.Set("LARGURA", gridView1.GetDataRow(i)["LARGURA"]);
                    ZVISAOUSUARIO.Set("VISIBILIDADE", gridView1.GetDataRow(i)["VISIVEL"].Equals(false) ? 0 : 1);
                    ZVISAOUSUARIO.Set("ORDENACAO", gridView1.GetDataRow(i)["ORDENACAO"]);

                    ZVISAOUSUARIO.Update();
                }

                splashScreenManager1.CloseWaitForm();

                this.Dispose();
                GC.Collect();
            }
            else
            {
                XtraMessageBox.Show("Somente uma coluna pode ser ordenada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        private void carregaGrid()
        {
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT  
ZVISAOUSUARIO.VISIBILIDADE AS VISIVEL,
ZVISAOUSUARIO.ORDENACAO,
ZVISAOUSUARIO.COLUNA, 
ZVISAOUSUARIO.SEQUENCIA, 
ZVISAOUSUARIO.LARGURA,
ZVISAOUSUARIO.FIXO
FROM 
ZVISAOUSUARIO 
WHERE 
GRID = ?
AND USUARIO = ?
AND FILTRO = ''
GROUP BY 
ZVISAOUSUARIO.VISIBILIDADE,
ZVISAOUSUARIO.ORDENACAO,
ZVISAOUSUARIO.COLUNA, 
ZVISAOUSUARIO.SEQUENCIA, 
ZVISAOUSUARIO.LARGURA,
ZVISAOUSUARIO.FIXO
ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            gridControl1.DataSource = dt;
        }

        private void RenomeiaColunasGridView()
        {
            gridView1.Columns["VISIVEL"].Caption = "Visível";
            gridView1.Columns["ORDENACAO"].Caption = "Ordenação";
            gridView1.Columns["COLUNA"].Caption = "Coluna";
            gridView1.Columns["SEQUENCIA"].Caption = "Sequência";
            gridView1.Columns["LARGURA"].Caption = "Largura";
            gridView1.Columns["FIXO"].Caption = "Fixo";
        }

        private void CriaColunaOrdenacao()
        {
            RepositoryItemComboBox ric = new RepositoryItemComboBox();

            ric.Items.AddRange(new object[] { "", "ASC", "DESC" });
            gridControl1.RepositoryItems.Add(ric);
            gridView1.Columns[1].ColumnEdit = ric;
        }

        private bool ValidaColunaOrdenacao()
        {
            bool valida = true;
            int count = 0;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (!string.IsNullOrEmpty(gridView1.GetDataRow(i)["ORDENACAO"].ToString()))
                {
                    count++;
                }
            }

            if (count > 1)
            {
                valida = false;
            }

            return valida;
        }

        #endregion
    }
}
