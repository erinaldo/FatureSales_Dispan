using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppLib;
using AppLib.Windows;
using AppFatureClient.Classes;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class FormComissaoPorcentagemVisao : DevExpress.XtraEditors.XtraForm
    {
        public Boolean Selecionou { get; set; }
        string Anexo = String.Empty;

        public FormComissaoPorcentagemVisao()
        {
            InitializeComponent();
            MetodosSQL.CS = AppLib.Context.poolConnection.Get().ConnectionString;
        }


        private void FormConsulta_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Alterar Status", null, BaixarCarta);
            grid1.GetProcessos().Add("Imprimir carta", null, ImprimirCarta);
        }

        public void ImprimirCarta(object sender, EventArgs e)
        {

            if(grid1.GetDataRows().Count == 1)
            {
                DataRow dr = grid1.GetDataRow();

                RelCartaComissao report = new RelCartaComissao(dr["CODCARTA"].ToString());

                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {
                MessageBox.Show("Selecione uma ou apenas uma carta.", "Aviso.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
        }

        public void BaixarCarta(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Empty;
                string Status = String.Empty;

                DataRowCollection drc = grid1.GetDataRows();


                foreach (DataRow SelectedRow in drc)
                {
                    if (SelectedRow["STATUS"].ToString() == "ABERTA")
                    {
                        Status = "CONCLUIDO";
                    }
                    else
                    {
                        Status = "ABERTA";
                    }
                    sql = String.Format(@"update ZCOMISSAOCARTA
                                          set STATUS = '{0}'
                                          where CODCARTA = '{1}'", Status, SelectedRow["CODCARTA"].ToString());
                    MetodosSQL.ExecQuery(sql);
                    grid1.Atualizar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region VISÃO

        public void Mostrar()
        {
            this.WindowState = FormWindowState.Maximized;

            if (new AppLib.Security.Access().Consultar(grid1.Conexao, grid1.NomeGrid, AppLib.Context.Perfil))
            {
                this.Show();
            }
            else
            {
                MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso a este menu (" + grid1.NomeGrid + ").", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        public void Mostrar(System.Windows.Forms.Form _MdiParent)
        {
            this.MdiParent = _MdiParent;
            this.Mostrar();
        }

        #endregion

        #region LOOKUP

        public Boolean MostrarLookup(CampoLookup campoLookup1)
        {
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;

            if (campoLookup1.textBoxCODIGO.Text.Equals(""))
            {
                this.grid1.panelLookup.Visible = true;
                this.grid1.FormPai = this;
                this.ShowDialog();

                if (this.grid1.Selecionou)
                {
                    campoLookup1.dr = grid1.GetDataRow();
                    campoLookup1.textBoxCODIGO.Text = campoLookup1.dr[campoLookup1.ColunaCodigo].ToString();
                    campoLookup1.textBoxDESCRICAO.Text = campoLookup1.dr[campoLookup1.ColunaDescricao].ToString();
                }
            }
            else
            {
                this.grid1.Atualizar(false, true, this);

                DataTable dt = ((DataView)grid1.gridControl1.Views[0].DataSource).Table;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][campoLookup1.ColunaCodigo].ToString().Equals(campoLookup1.textBoxCODIGO.Text))
                    {
                        campoLookup1.dr = dt.Rows[i];
                        campoLookup1.textBoxDESCRICAO.Text = dt.Rows[i][campoLookup1.ColunaDescricao].ToString();
                        Selecionou = true;
                        i = dt.Rows.Count;
                    }
                }
            }

            return grid1.Selecionou;
        }

        public Boolean MostrarLookup(CampoLookup campoLookup1, String consulta, Object[] parametros, String filtro)
        {
            this.grid1.Conexao = campoLookup1.Conexao;
            this.grid1.Consulta = new String[] { consulta += " " + filtro };
            this.grid1.Parametros = parametros;
            return this.MostrarLookup(campoLookup1);
        }

        public bool MostrarLookup(CampoLookup campoLookup1, String filtro)
        {
            this.grid1.Conexao = campoLookup1.Conexao;
            this.grid1.Consulta = new String[] { string.Join(" ", this.grid1.Consulta) + " " + filtro };
            return this.MostrarLookup(campoLookup1);
        }

        public Boolean MostrarLookup(CampoLookup campoLookup1, String consulta, Object[] parametros)
        {
            this.grid1.NomeGrid = campoLookup1.ColunaTabela + "_" + campoLookup1.ColunaCodigo;
            return this.MostrarLookup(campoLookup1, consulta, parametros, "");
        }

        #endregion

        private void FormVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManager.ReleaseUnusedMemory(false);
        }

        public void Pesquisar(String texto)
        {
            grid1.Pesquisar(texto);
        }

        private void grid1_AposSelecao(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed.Equals(false))
            {
                this.gridData1.Atualizar(false);
            }

        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {

        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Empty;

                DataRowCollection drc = grid1.GetDataRows();


                foreach (DataRow SelectedRow in drc)
                {
                    sql = String.Format("delete from ZPORCENTAGEMCARTA where CODDESC = {0}", SelectedRow["CODDESC"].ToString());
                    MetodosSQL.ExecQuery(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {

        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            DataRow SelectRow = grid1.GetDataRow();

            FormComissaoPorcentagemCadastro frm = new FormComissaoPorcentagemCadastro();
            frm.COD = SelectRow["CODDESC"].ToString();
            frm.EDIT = true;
            frm.ShowDialog();
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormComissaoPorcentagemCadastro frm = new FormComissaoPorcentagemCadastro();
            frm.EDIT = false;
            frm.ShowDialog();
        }

        private void grid1_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Enter(object sender, EventArgs e)
        {

        }
    }
}
