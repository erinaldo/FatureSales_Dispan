using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class GridObject : UserControl
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Habilitar botão novo")]
        public Boolean BotaoNovo { get; set; }

        [Category("_APP"), Description("Habilitar botão editar")]
        public Boolean BotaoEditar { get; set; }

        [Category("_APP"), Description("Habilitar botão excluir")]
        public Boolean BotaoExcluir { get; set; }

        [Category("_APP"), Description("Nome da grid")]
        public String NomeGrid { get; set; }

        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        [Category("_APP"), Description("Consulta da grid")]
        public String[] Consulta { get; set; }

        [Category("_APP"), Description("Parâmetros da consulta")]
        public Object[] Parametros = new Object[] { };

        [Category("_APP"), Description("Tipo de filtro")]
        public Global.Types.TipoFiltro TipoFiltro { get; set; }

        [Category("_APP"), Description("Filtro interno")]
        private String Filtro = "";

        [Category("_APP"), Description("BindingSource de registros")]
        public System.Windows.Forms.BindingSource bs = new BindingSource();

        #endregion

        #region CONSTRUTOR

        public GridObject()
        {
            InitializeComponent();

            BotaoNovo = false;
            BotaoEditar = false;
            BotaoExcluir = false;

            Conexao = "Start";

            TipoFiltro = Global.Types.TipoFiltro.Todos;
            this.Dock = DockStyle.Fill;
        }
        
        #endregion

        #region EVENTOS

        private void Grid2_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                try
                {
                    if (BotaoNovo)
                    {
                        toolStripButtonNOVO.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonNOVO.Enabled = false;
                    }
                }
                catch (Exception) { }

                try
                {
                    if (BotaoEditar)
                    {
                        toolStripButtonEDITAR.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonEDITAR.Enabled = false;
                    }
                }
                catch (Exception) { }

                try
                {
                    if (BotaoExcluir)
                    {
                        toolStripButtonEXCLUIR.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonEXCLUIR.Enabled = false;
                    }
                }
                catch (Exception) { }

                if (this.TipoFiltro != Global.Types.TipoFiltro.Nenhum)
                {
                    this.Atualizar(this, null);
                }
            }
        }

        private void toolStripButtonNOVO_Click(object sender, EventArgs e)
        {
            if (BotaoNovo)
            {
                this.Novo(this, null);
                this.toolStripButtonATUALIZAR_Click(this, null);
            }
            else
            {
                // MessageBox.Show("Botão desabilitado.");
            }
        }

        private void toolStripButtonEDITAR_Click(object sender, EventArgs e)
        {
            if (BotaoEditar)
            {
                if (gridView1.SelectedRowsCount <= 0)
                {
                    return;
                }

                this.Editar(this, null);
                this.toolStripButtonATUALIZAR_Click(this, null);
            }
            else
            {
                // MessageBox.Show("Botão desabilitado.");
            }
        }

        private void toolStripButtonEXCLUIR_Click(object sender, EventArgs e)
        {
            if (BotaoExcluir)
            {
                this.Excluir(this, null);
                this.toolStripButtonATUALIZAR_Click(this, null);
            }
            else
            {
                // MessageBox.Show("Botão desabilitado.");
            }
        }

        private void toolStripButtonPESQUISAR_Click(object sender, EventArgs e)
        {
            if (gridView1.IsFindPanelVisible)
            {
                gridView1.HideFindPanel();
            }
            else
            {
                gridView1.ShowFindPanel();
            }
        }

        private void toolStripButtonAGRUPAR_Click(object sender, EventArgs e)
        {
            gridView1.OptionsView.ShowGroupPanel = !gridView1.OptionsView.ShowGroupPanel;
        }

        public void toolStripButtonATUALIZAR_Click(object sender, EventArgs e)
        {
            this.Atualizar(this, null);
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            toolStripButtonEDITAR_Click(this, null);
        }

        private void selecionarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.SelectAll();
        }

        private void inverterSeleçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int registros = gridControl1.Views[0].DataRowCount;

            for (int i = 0; i < registros; i++)
            {
                gridView1.InvertRowSelection(i);
            }
        }

        private void limparSeleçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.ClearSelection();
        }

        private void exportarTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo texto|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Text, sfd.FileName);
            }
        }

        private void exportarRTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo formatado|*.rtf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Rtf, sfd.FileName);
            }
        }

        private void exportarHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo navegador|*.html";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Html, sfd.FileName);
            }
        }

        private void exportarCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo separador por vírgula|*.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Csv, sfd.FileName);
            }
        }

        private void exportarXLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Excel (formato antigo)|*.xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Xls, sfd.FileName);
            }
        }

        private void exportarXLSXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Excel|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Xlsx, sfd.FileName);
            }
        }

        private void exportarPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Adobe|*.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Pdf, sfd.FileName);
            }
        }

        private void toolStripButtonATUALIZAR_ButtonClick(object sender, EventArgs e)
        {
            this.toolStripButtonATUALIZAR_Click(this, null);
        }
        
        #endregion

        #region MÉTODOS

        public String GetConsulta()
        {
            String result = "";

            for (int i = 0; i < Consulta.Length; i++)
            {
                result += Consulta[i] + "\r\n";
            }

            return result;
        }

        public Object GetObjectRow()
        {
            return this.GetObjectRow(true);
        }

        public Object GetObjectRow(Boolean ValidarSelecao)
        {
            if (gridView1.SelectedRowsCount == 1)
            {
                int[] handles = gridView1.GetSelectedRows();
                return gridView1.GetRow(handles[0]);
            }
            else
            {
                if (ValidarSelecao)
                {
                    MessageBox.Show("Selecione um registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }

        public List<Object> GetObjectRows()
        {
            return this.GetObjectRows(true);
        }

        public List<Object> GetObjectRows(Boolean ValidarSelecao)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                List<Object> lista = new List<object>();
                int[] handles = gridView1.GetSelectedRows();

                for (int i = 0; i < handles.Length; i++)
                {
                    lista.Add(gridView1.GetRow(handles[i]));
                }

                return lista;
            }
            else
            {
                if (ValidarSelecao)
                {
                    MessageBox.Show("Selecione o(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }

        public System.Data.DataRow GetDataRow()
        {
            return this.GetDataRow(true);
        }

        public System.Data.DataRow GetDataRow(Boolean ValidarSelecao)
        {
            if (gridView1.SelectedRowsCount == 1)
            {
                int[] handles = gridView1.GetSelectedRows();
                return gridView1.GetDataRow(handles[0]);
            }
            else
            {
                if (ValidarSelecao)
                {
                    MessageBox.Show("Selecione um registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }

        public System.Data.DataRowCollection GetDataRows()
        {
            return this.GetDataRows(true);
        }

        public System.Data.DataRowCollection GetDataRows(Boolean ValidarSelecao)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                System.Data.DataTable dt = new DataTable();
                int[] handles = gridView1.GetSelectedRows();

                for (int i = 0; i < handles.Length; i++)
                {
                    if (i == 0)
                    {
                        for (int col = 0; col < gridView1.GetDataRow(handles[i]).Table.Columns.Count; col++)
                        {
                            dt.Columns.Add(gridView1.GetDataRow(handles[i]).Table.Columns[col].ColumnName);
                        }
                    }

                    dt.Rows.Add(gridView1.GetDataRow(handles[i]).ItemArray);
                }

                return dt.Rows;
            }
            else
            {
                if (ValidarSelecao)
                {
                    MessageBox.Show("Selecione o(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }

        public System.Windows.Forms.ToolStripItemCollection GetAnexos()
        {
            return this.toolStripButtonANEXOS.DropDownItems;
        }

        public System.Windows.Forms.ToolStripItemCollection GetProcessos()
        {
            return this.toolStripButtonPROCESSOS.DropDownItems;
        }

        public void Pesquisar(String texto)
        {
            gridView1.ShowFindPanel();
            gridView1.ApplyFindFilter(texto);
        }
        
        #endregion

        #region DELEGATE

        public delegate void SetNewParametrosHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar parametros da consulta"), DefaultValue(false)]
        public event SetNewParametrosHandler SetParametros;

        public delegate void NovoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão novo"), DefaultValue(false)]
        public event NovoHandler Novo;

        public delegate void EditarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão editar"), DefaultValue(false)]
        public event EditarHandler Editar;

        public delegate void ExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão excluir"), DefaultValue(false)]
        public event ExcluirHandler Excluir;

        public delegate void AtualizarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão atualizar"), DefaultValue(false)]
        public event AtualizarHandler Atualizar;
        
        #endregion
        
    }
}
