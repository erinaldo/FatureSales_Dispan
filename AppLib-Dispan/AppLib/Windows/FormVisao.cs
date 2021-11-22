using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class FormVisao : DevExpress.XtraEditors.XtraForm
    {
        public Boolean Selecionou { get; set; }

        public FormVisao()
        {
            InitializeComponent();
        }

        private void FormConsulta_Load(object sender, EventArgs e)
        {
            //if (this.grid1.Consulta[0].ToString().Contains("TTB2") || this.grid1.Consulta[0].ToString().Contains("TTB3") || this.grid1.Consulta[0].ToString().Contains("TTB4"))
            //{
            //    this.grid1.toolStripButtonNOVO.Enabled = true;
            //    this.grid1.toolStripButtonEDITAR.Enabled = true;
            //}
        }

        #region VISÃO

        public void Mostrar()
        {
            this.WindowState = FormWindowState.Maximized;

            if (new Security.Access().Consultar(grid1.Conexao, grid1.NomeGrid, AppLib.Context.Perfil))
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
            this.grid1.TipoFiltro = Global.Types.TipoFiltro.Todos;

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
        
    }
}
