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

namespace AppFatureClient.New.Forms.Register
{
    public partial class frmCadastroTabelaPreco : Form
    {
        #region Variáveis

        public int ID;
        public bool edita = false;
        public bool copia = false;
        public DataTable dtItensTabelaPreco = new DataTable();

        #endregion

        public frmCadastroTabelaPreco()
        {
            InitializeComponent();
        }

        private void frmCadastroTabelaPreco_Load(object sender, EventArgs e)
        {
            CarregaComboBoxAcabamento();
            CarregaComboBoxTipo();
            DesabilitaBotoes();

            if (copia)
            {
                CriaTabelaItens();
                CarregaCampos(true);
            }
            else
            {
                if (edita)
                {
                    CriaTabelaItens();
                    CarregaCampos(false);
                }
                else
                {
                    cbTipo.Enabled = false;

                    CriaTabelaItens();
                    DesabilitaBotoes();
                }
            }
        }

        private void cbAcabamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAcabamento.SelectedValue.ToString() == "INOX")
            {
                cbTipo.Enabled = true;
            }
            else
            {
                cbTipo.Enabled = false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar())
            {
                edita = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Salvar())
            {
                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Item Tabela de Preço

        private void btnNovo_Click(object sender, EventArgs e)
        {
            New.Forms.Register.frmCadastroItemTabelaPreco frmCadastroItemTabelaPreco = new frmCadastroItemTabelaPreco();
            frmCadastroItemTabelaPreco.dtItens = dtItensTabelaPreco;
            frmCadastroItemTabelaPreco.ShowDialog();

            CarregaGridItens();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                EditarItem(row);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (XtraMessageBox.Show("Deseja excluir o item selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                    if (ExcluirItem(row))
                    {
                        XtraMessageBox.Show("Item excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CarregaItens();
                        return;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível excluir o item selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (btnPesquisar.Text == "Pesquisar")
            {
                gridView1.OptionsFind.AlwaysVisible = true;
                btnPesquisar.Text = "Cancelar";
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = false;
                btnPesquisar.Text = "Pesquisar";
            }
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (btnAgrupar.Text == "Agrupar")
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
            else
            {
                gridView1.ClearGrouping();
                gridView1.OptionsView.ShowGroupPanel = false;
                btnAgrupar.Text = "Agrupar";
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                EditarItem(row);
            }
        }

        #endregion

        #region Métodos - Tabela de Preço

        private void CarregaComboBoxAcabamento()
        {
            DataTable dtAcabamento = new DataTable();

            dtAcabamento.Columns.Add("Codigo", typeof(string));
            dtAcabamento.Columns.Add("Nome", typeof(string));

            dtAcabamento.Rows.Add("-", "- Selecione");
            dtAcabamento.Rows.Add("PZ", "Pré-Zincado");
            dtAcabamento.Rows.Add("GE", "Galv. Eletrolítico");
            dtAcabamento.Rows.Add("NAT", "Natural");
            dtAcabamento.Rows.Add("INOX", "Aço Inox");
            dtAcabamento.Rows.Add("GF", "Galv. a Fogo");
            dtAcabamento.Rows.Add("AL", "Alumínio");
            dtAcabamento.Rows.Add("GL", "Galvalume");
            dtAcabamento.Rows.Add("PVC", "PVC");
            dtAcabamento.Rows.Add("MIN", "Minimizada");
            dtAcabamento.Rows.Add("BC", "Bicromatizado");
            dtAcabamento.Rows.Add("CD", "Cadmiado");

            cbAcabamento.DataSource = dtAcabamento;
            cbAcabamento.ValueMember = "Codigo";
            cbAcabamento.DisplayMember = "Nome";
        }

        private void CarregaComboBoxTipo()
        {
            DataTable dtTipo = new DataTable();

            dtTipo.Columns.Add("Codigo", typeof(string));
            dtTipo.Columns.Add("Nome", typeof(string));

            dtTipo.Rows.Add("-", "- Selecione");
            dtTipo.Rows.Add("304", "304 - Inox");
            dtTipo.Rows.Add("316", "316 - Inox");

            cbTipo.DataSource = dtTipo;
            cbTipo.ValueMember = "Codigo";
            cbTipo.DisplayMember = "Nome";
        }

        private void CarregaCampos(bool copia)
        {
            if (copia)
            {
                DataTable dtTabelaPreco = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZTPRODUTOTABPRECO WHERE ID = ?", new object[] { ID });

                // Campos Tabela de Preço
                if (dtTabelaPreco.Rows.Count > 0)
                {
                    tbCodigoTabelaPreco.Text = dtTabelaPreco.Rows[0]["CODIGO"].ToString();
                    cbAcabamento.SelectedValue = dtTabelaPreco.Rows[0]["ACABAMENTO"].ToString();
                    cbTipo.SelectedValue = dtTabelaPreco.Rows[0]["TIPO"].ToString();
                }

                CarregaItens();

                tbCodigoTabelaPreco.Select();
            }
            else
            {
                DataTable dtTabelaPreco = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZTPRODUTOTABPRECO WHERE ID = ?", new object[] { ID });

                // Campos Tabela de Preço
                if (dtTabelaPreco.Rows.Count > 0)
                {
                    tbCodigoTabelaPreco.Text = dtTabelaPreco.Rows[0]["CODIGO"].ToString();
                    cbAcabamento.SelectedValue = dtTabelaPreco.Rows[0]["ACABAMENTO"].ToString();
                    cbTipo.SelectedValue = dtTabelaPreco.Rows[0]["TIPO"].ToString();

                    if (dtTabelaPreco.Rows[0]["INICIOVIGENCIA"] != DBNull.Value)
                    {
                        dteInicio.DateTime = Convert.ToDateTime(dtTabelaPreco.Rows[0]["INICIOVIGENCIA"]);
                    }

                    if (dtTabelaPreco.Rows[0]["FIMVIGENCIA"] != DBNull.Value)
                    {
                        dteFim.DateTime = Convert.ToDateTime(dtTabelaPreco.Rows[0]["FIMVIGENCIA"]);
                    }
                }

                CarregaItens();
            }
        }

        private void DesabilitaBotoes()
        {
            btnFiltros.Enabled = false;
            toolStripDropDownButton1.Enabled = false;
            toolStripDropDownButton2.Enabled = false;
            toolStripDropDownButton3.Enabled = false;
        }

        private bool ValidaTabelaPreco()
        {
            bool valida = true;

            if (string.IsNullOrEmpty(tbCodigoTabelaPreco.Text))
            {
                XtraMessageBox.Show("O Código deve ser informado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valida = false;
            }

            if (string.IsNullOrEmpty(cbAcabamento.SelectedValue.ToString()))
            {
                if (cbAcabamento.SelectedValue.ToString() == "-")
                {
                    XtraMessageBox.Show("O Acabamento deve ser informado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valida = false;
                }
            }

            if (string.IsNullOrEmpty(dteInicio.Text))
            {
                XtraMessageBox.Show("A Data de Início deve ser informada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valida = false;
            }

            if (string.IsNullOrEmpty(dteFim.Text))
            {
                XtraMessageBox.Show("A Data Final deve ser informada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valida = false;
            }

            return valida;
        }

        private bool Salvar()
        {
            if (ValidaTabelaPreco())
            {
                try
                {
                    if (edita)
                    {
                        if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZTPRODUTOTABPRECO SET CODIGO = ?, ACABAMENTO = ?, INICIOVIGENCIA = ?, FIMVIGENCIA = ?, TIPO = ? WHERE CODCOLIGADA = ? AND ID = ?", new object[] { tbCodigoTabelaPreco.Text, cbAcabamento.SelectedValue.ToString(), dteInicio.DateTime.ToString("yyy-MM-dd"), dteFim.DateTime.ToString("yyy-MM-dd"), (cbTipo.SelectedValue == null ? "" : cbTipo.SelectedValue.ToString()), AppLib.Context.Empresa, ID }) > 0)
                        {
                            if (SalvarItens())
                            {
                                XtraMessageBox.Show("Tabela de Preço editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO ZTPRODUTOTABPRECO VALUES (?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, tbCodigoTabelaPreco.Text, cbAcabamento.SelectedValue.ToString(), dteInicio.DateTime.ToString("yyy-MM-dd"), dteFim.DateTime.ToString("yyy-MM-dd"), cbTipo.SelectedValue.ToString(), 0 }) > 0)
                        {
                            ID = GetMaxID();

                            if (SalvarItens())
                            {
                                XtraMessageBox.Show("Tabela de Preço cadastrada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private int GetMaxID()
        {
            int id = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT MAX(ID) FROM ZTPRODUTOTABPRECO WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa }));

            return id;
        }

        #endregion

        #region Métodos - Itens

        private void CriaTabelaItens()
        {
            dtItensTabelaPreco.Columns.Add(new DataColumn("ID", typeof(int)));
            dtItensTabelaPreco.Columns.Add(new DataColumn("Código da Coligada", typeof(string)));
            dtItensTabelaPreco.Columns.Add(new DataColumn("Chapa", typeof(string)));
            dtItensTabelaPreco.Columns.Add(new DataColumn("Descrição", typeof(string)));
            dtItensTabelaPreco.Columns.Add(new DataColumn("Peso", typeof(decimal)));
            dtItensTabelaPreco.Columns.Add(new DataColumn("Preço KG", typeof(decimal)));
        }
        private void CarregaItens()
        {
            DataTable dtItens = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZTPRODUTOTABPRECOCOMPL WHERE ID = ?", new object[] { ID });

            // Itens da Tabela de Preço
            if (dtItens.Rows.Count > 0)
            {
                dtItensTabelaPreco = new DataTable();
                CriaTabelaItens();

                for (int i = 0; i < dtItens.Rows.Count; i++)
                {
                    dtItensTabelaPreco.Rows.Add
                        (
                        ID,
                        AppLib.Context.Empresa,
                        dtItens.Rows[i]["CHAPA"].ToString(),
                        dtItens.Rows[i]["DESCRICAOCHAPA"].ToString(),
                        Convert.ToDecimal(dtItens.Rows[i]["PESO"]),
                        Convert.ToDecimal(dtItens.Rows[i]["PRECOKG"])
                        );
                }

                CarregaGridItens();
            }
        }

        private void CarregaGridItens()
        {
            gridControl1.DataSource = dtItensTabelaPreco;

            ConfigurarGridItens();
        }

        private void ConfigurarGridItens()
        {
            // Visibilidade das colunas
            gridView1.Columns["ID"].Visible = false;

            // Edição das colunas
            gridView1.Columns["Código da Coligada"].OptionsColumn.AllowEdit = false;
            gridView1.Columns["Chapa"].OptionsColumn.AllowEdit = false;
            gridView1.Columns["Descrição"].OptionsColumn.AllowEdit = false;
            gridView1.Columns["Peso"].OptionsColumn.AllowEdit = false;

            gridView1.BestFitColumns();
        }

        private bool VerificaItem(DataRow row, bool copia)
        {
            if (copia)
            {
                int itemExiste = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT COUNT(*) FROM ZTPRODUTOTABPRECOCOMPL WHERE CODCOLIGADA = ? AND ID = ? AND CHAPA = ?", new object[] { AppLib.Context.Empresa, ID, row["CHAPA"].ToString() }));

                if (itemExiste > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                int itemExiste = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT COUNT(*) FROM ZTPRODUTOTABPRECOCOMPL WHERE CODCOLIGADA = ? AND ID = ? AND CHAPA = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["ID"]), row["CHAPA"].ToString() }));

                if (itemExiste > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void EditarItem(DataRow row)
        {
            New.Forms.Register.frmCadastroItemTabelaPreco frmCadastroItemTabelaPreco = new Register.frmCadastroItemTabelaPreco();

            frmCadastroItemTabelaPreco.edita = true;
            frmCadastroItemTabelaPreco.ID = Convert.ToInt32(row["ID"]);
            frmCadastroItemTabelaPreco.chapa = row["CHAPA"].ToString();

            frmCadastroItemTabelaPreco.ShowDialog();

            CarregaItens();
        }

        private bool ExcluirItem(DataRow row)
        {
            bool excluiu = true;

            try
            {
                if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM ZTPRODUTOTABPRECOCOMPL WHERE CODCOLIGADA = ? AND ID = ? AND CHAPA =?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["ID"]), row["CHAPA"].ToString() }) <= 0)
                {
                    excluiu = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                excluiu = false;
            }

            return excluiu;
        }

        private bool SalvarItens()
        {
            try
            {
                for (int i = 0; i < dtItensTabelaPreco.Rows.Count; i++)
                {
                    if (VerificaItem(dtItensTabelaPreco.Rows[i], copia))
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZTPRODUTOTABPRECOCOMPL SET DESCRICAOCHAPA = ?, PESO = ?, PRECOKG = ? WHERE CODCOLIGADA = ? AND ID = ? AND CHAPA = ?", new object[] { dtItensTabelaPreco.Rows[i]["Descrição"].ToString(), Convert.ToDecimal(dtItensTabelaPreco.Rows[i]["Peso"]), Convert.ToDecimal(dtItensTabelaPreco.Rows[i]["Preço KG"]), AppLib.Context.Empresa, ID, dtItensTabelaPreco.Rows[i]["Chapa"].ToString() });
                    }
                    else
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO ZTPRODUTOTABPRECOCOMPL VALUES(?, ?, ?, ?, ?, ?)", new object[] { ID, AppLib.Context.Empresa, dtItensTabelaPreco.Rows[i]["Chapa"].ToString(), dtItensTabelaPreco.Rows[i]["Descrição"].ToString(), Convert.ToDecimal(dtItensTabelaPreco.Rows[i]["Peso"]), Convert.ToDecimal(dtItensTabelaPreco.Rows[i]["Preço KG"]) });
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro ao salvar os itens da Tabela de Preço.\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion
    }
}
