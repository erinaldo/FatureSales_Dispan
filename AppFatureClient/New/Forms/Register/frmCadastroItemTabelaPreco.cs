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
    public partial class frmCadastroItemTabelaPreco : Form
    {
        #region Variáveis

        public int ID;
        public bool edita;
        public DataTable dtItens = new DataTable();
        public string chapa = "";

        #endregion

        public frmCadastroItemTabelaPreco()
        {
            InitializeComponent();
        }

        private void frmCadastroItemTabelaPreco_Load(object sender, EventArgs e)
        {
            CarregaComboBoxChapa();

            if (edita)
            {
                cbChapa.Enabled = false;

                CarregaCampos();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (edita == false)
            {
                AtualizaItem();
            }
            else
            {
                AdicionarItem();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (edita)
            {
                AtualizaItem();
                this.Dispose();
            }
            else
            {
                AdicionarItem();
                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        private void CarregaComboBoxChapa()
        {
            DataTable dtChapa = new DataTable();

            dtChapa.Columns.Add("Codigo", typeof(string));
            dtChapa.Columns.Add("Nome", typeof(string));

            dtChapa.Rows.Add("-", "- Selecione");
            dtChapa.Rows.Add("A", "A - 14");
            dtChapa.Rows.Add("B", "B - 16");
            dtChapa.Rows.Add("C", "C - 18");
            dtChapa.Rows.Add("D", "D - 19");
            dtChapa.Rows.Add("E", "E - 20");
            dtChapa.Rows.Add("F", "F - 22");
            dtChapa.Rows.Add("G", "G - 24");
            dtChapa.Rows.Add("H", "H - 26");
            dtChapa.Rows.Add("X", "X - 13");

            cbChapa.DataSource = dtChapa;
            cbChapa.ValueMember = "Codigo";
            cbChapa.DisplayMember = "Nome";
        }

        private void CarregaCampos()
        {
            DataTable dtCampos = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZTPRODUTOTABPRECOCOMPL WHERE CODCOLIGADA = ? AND ID = ? AND CHAPA = ?", new object[] { AppLib.Context.Empresa, ID, chapa });

            cbChapa.SelectedValue = dtCampos.Rows[0]["CHAPA"].ToString();
            tbPeso.EditValue = Convert.ToDecimal(dtCampos.Rows[0]["PESO"]);
            tbPreco.EditValue = Convert.ToDecimal(dtCampos.Rows[0]["PRECOKG"]);
            tbDescricao.Text = dtCampos.Rows[0]["DESCRICAOCHAPA"].ToString();
        }

        private bool ValidaCampos()
        {
            bool valida = true;

            if (string.IsNullOrEmpty(cbChapa.SelectedValue.ToString()))
            {
                if (cbChapa.SelectedValue.ToString() == "-")
                {
                    XtraMessageBox.Show("A Chapa deve ser informada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valida = false;
                }          
            }

            if (string.IsNullOrEmpty(tbPeso.Text))
            {
                XtraMessageBox.Show("O Peso deve ser informado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valida = false;
            }

            if (string.IsNullOrEmpty(tbPreco.Text))
            {
                XtraMessageBox.Show("O Preço deve ser informado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valida = false;
            }

            return valida;
        }

        private void AdicionarItem()
        {
            try
            {
                if (ValidaCampos())
                {
                    dtItens.Rows.Add
                        (
                            ID,
                            AppLib.Context.Empresa,
                            cbChapa.SelectedValue.ToString(),
                            tbDescricao.Text,
                            Convert.ToDecimal(tbPeso.Text),
                            Convert.ToDecimal(tbPreco.Text)
                        );
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Não foi possível adicionar um novo item para a Tabela de Preço.\r\nDetalhes: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void AtualizaItem()
        {
            try
            {
                if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZTPRODUTOTABPRECOCOMPL SET DESCRICAOCHAPA = ?, PESO = ?, PRECOKG = ? WHERE CODCOLIGADA = ? AND ID = ? AND CHAPA = ?", new object[] { tbDescricao.Text, Convert.ToDecimal(tbPeso.Text), Convert.ToDecimal(tbPreco.Text), AppLib.Context.Empresa, ID, chapa }) > 0)
                {
                    XtraMessageBox.Show("Item atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    XtraMessageBox.Show("Não foi possível adicionar um novo item para a Tabela de Preço.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }        
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Não foi possível atualizar o item.\r\nDetalhes: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion
    }
}
