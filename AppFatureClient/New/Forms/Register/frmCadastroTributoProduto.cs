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
    public partial class frmCadastroTributoProduto : Form
    {
        public int IDPRD;
        public bool edita = false;
        private string query = "";
        public string codigoTributo = "";
        public frmCadastroTributoProduto()
        {
            InitializeComponent();
        }

        private void frmCadastroTributoProduto_Load(object sender, EventArgs e)
        {
            if (edita)
            {
                CarregaCampos();
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

        #region Métodos

        private void CarregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM TTRBPRD WHERE IDPRD = ? AND CODCOLIGADA = ? AND CODTRB = ?", new object[] { IDPRD, AppLib.Context.Empresa, codigoTributo });

            clTributo.Set(dt.Rows[0]["CODTRB"].ToString());
            tbAliquota.Text = dt.Rows[0]["ALIQUOTA"].ToString();
            tbSituacaoTributariaEntrada.Text = dt.Rows[0]["SITTRIBUTARIAENT"].ToString();
            tbSituacaoTributariaSaida.Text = dt.Rows[0]["SITTRIBUTARIASAI"].ToString();
            tbCodigoContribuicaoSocial.Text = dt.Rows[0]["CODCONTRIBSOCIAL"].ToString();
        }

        private bool Salvar()
        {
            int result = 0;

            if (edita == false)
            {
                query = @"INSERT INTO TTRBPRD (CODCOLIGADA, IDPRD, CODTRB, ALIQUOTA, CODCONTRIBSOCIAL, SITTRIBUTARIAENT, SITTRIBUTARIASAI) VALUES(?, ?, ?, ?, ?, ?, ?)";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { AppLib.Context.Empresa, IDPRD, clTributo.Get(), Convert.ToDecimal(tbAliquota.EditValue), tbCodigoContribuicaoSocial.Text, tbSituacaoTributariaEntrada.Text, tbSituacaoTributariaSaida.Text});

                    if (result > 0)
                    {
                        XtraMessageBox.Show("Tributo do Produto cadastrado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível cadastrar o Tributo do Produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                query = @"UPDATE TTRBPRD 
                                SET 
                                ALIQUOTA = ?,
                                CODCONTRIBSOCIAL = ?,
                                SITTRIBUTARIAENT = ?,
                                SITTRIBUTARIASAI = ?
                                WHERE CODCOLIGADA = ? AND IDPRD = ? AND CODTRB = ?";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { Convert.ToDecimal(tbAliquota.EditValue), tbCodigoContribuicaoSocial.Text, tbSituacaoTributariaEntrada.Text, tbSituacaoTributariaSaida.Text, AppLib.Context.Empresa, IDPRD, clTributo.Get() });

                    if (result > 0)
                    {
                        XtraMessageBox.Show("Tributo do Produto editado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível editar o Tributo do Produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        #endregion
    }
}
