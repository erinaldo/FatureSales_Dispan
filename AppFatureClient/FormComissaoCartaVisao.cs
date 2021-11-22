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
    public partial class FormComissaoCartaVisao : DevExpress.XtraEditors.XtraForm
    {
        public Boolean Selecionou { get; set; }
        string Anexo = String.Empty;

        public FormComissaoCartaVisao()
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

            if (grid1.GetDataRows().Count == 1)
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
                string Historico = String.Empty;



                DataRowCollection drc = grid1.GetDataRows();


                foreach (DataRow SelectedRow in drc)
                {


                    sql = String.Format(@"select case when ANEXOCARTA is null or ANEXONFE is null then '0' else '1' end as 'Autoriza' from ZCOMISSAOCARTA where CODCARTA = {0}", SelectedRow["CODCARTA"].ToString());

                    if (MetodosSQL.GetField(sql, "Autoriza") == "1")
                    {
                        sql = String.Format(@"select HISTORICO from ZCOMISSAOCARTA where CODCARTA = {0}", SelectedRow["CODCARTA"].ToString());
                        Historico = MetodosSQL.GetField(sql, "HISTORICO");
                        Historico = String.Format(@"'{0}'", Historico);

                        if (SelectedRow["STATUS"].ToString() == "ABERTA")
                        {
                            Status = "CONCLUIDO";
                        }
                        else
                        {
                            Status = "ABERTA";
                            AppLib.Windows.FormMessagePrompt f = new FormMessagePrompt();
                            f.richTextBox1.Text = "Digite o motivo";
                            while (String.IsNullOrWhiteSpace(f.textBox1.Text))
                            {
                                f.ShowDialog();
                                if (String.IsNullOrWhiteSpace(f.textBox1.Text))
                                {
                                    MessageBox.Show("Você deve inserir um motivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    Historico = String.Format(@"{0} + CHAR(13)+CHAR(10) + '{1} - ' + CONVERT(VARCHAR(25), GETDATE(), 109) + ' {2}'", Historico, AppLib.Context.Usuario, f.textBox1.Text);
                                }
                            }
                        }

                        sql = String.Format(@"update ZCOMISSAOCARTA
                                          set STATUS = '{0}',
                                          USUARIOALTERACAO = '{1}', 
                                          DATAALTERACAO = GETDATE(),
                                          HISTORICO = {2}
                                          where CODCARTA = '{3}'", Status, AppLib.Context.Usuario, Historico, SelectedRow["CODCARTA"].ToString());
                        MetodosSQL.ExecQuery(sql);
                        grid1.Atualizar();
                    }
                    else
                    {
                        MessageBox.Show(String.Format("Carta {0} não pode ser finalizada pois não contem todos os anexos", SelectedRow["CODCARTA"].ToString()), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DialogResult r = MessageBox.Show("Deseja excluir os itens selecionados?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(r == DialogResult.Yes)
                {
                    string sql = String.Empty;

                    DataRowCollection drc = grid1.GetDataRows();


                    foreach (DataRow SelectedRow in drc)
                    {
                        if (SelectedRow["STATUS"].ToString() != "CONCLUIDO")
                        {
                            sql = String.Format(@"delete from ZCOMISSAOCARTA where CODCARTA = '{0}' and STATUS <> 'CONCLUIDO'", SelectedRow["CODCARTA"].ToString());
                            MetodosSQL.ExecQuery(sql);

                            sql = String.Format(@"update ZTMOVCOMISSAO
                                                set NCARTA = null
                                                where NCARTA = '{0}'", SelectedRow["CODCARTA"].ToString());
                            MetodosSQL.ExecQuery(sql);

                            grid1.Atualizar();
                        }
                        else
                        {
                            MessageBox.Show("Você não pode apagar uma carta com status CONCLUIDO");
                        }

                    }
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
            string rpr = SelectRow["CODRPR"].ToString();
            string carta = SelectRow["CODCARTA"].ToString();

            AppLib.Windows.FormComissaoCartaCadastro frm = new AppLib.Windows.FormComissaoCartaCadastro(rpr, carta);
            frm.ShowDialog();
        }

        private void grid1_Novo(object sender, EventArgs e)
        {          
            FormComissaoSelecaoRepresentante f = new FormComissaoSelecaoRepresentante();
            f.ShowDialog();

            string sql = String.Format(@"select COUNT(1) as 'CONT' from ZCOMISSAOCARTA where CODRPR = {0} and STATUS = 'ABERTA'", f.codrpr);

            Clipboard.SetText(sql);

            if (MetodosSQL.GetField(sql,"CONT") == "0")
            {
                if (f.codrpr != null)
                {
                    AppLib.Windows.FormComissaoCartaCadastro frm = new AppLib.Windows.FormComissaoCartaCadastro(f.codrpr, null);
                    frm.ShowDialog();
                }
            }
            else
            {
                sql = String.Format(@"select CODCARTA from ZCOMISSAOCARTA where CODRPR = {0} and STATUS = 'ABERTA'", f.codrpr);
                Clipboard.SetText(sql);
                MessageBox.Show(String.Format("Não é possivel abrir uma nova pois o representante já possui a carta {0} em aberto", MetodosSQL.GetField(sql,"CODCARTA"), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
            }
        }

        private void grid1_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Enter(object sender, EventArgs e)
        {

        }
    }
}
