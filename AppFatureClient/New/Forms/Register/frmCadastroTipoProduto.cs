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
    public partial class frmCadastroTipoProduto : Form
    {
        public string codigoTipoProduto = "";
        public bool edita = false;
        private string query = "";
        public frmCadastroTipoProduto()
        {
            InitializeComponent();
        }

        private void frmCadastroTipoProduto_Load(object sender, EventArgs e)
        {
            if (edita)
            {
                CarregaCampos();
                InativaComponentes();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar())
            {
                edita = true;
                codigoTipoProduto = tbCodigoTipoProduto.Text;
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

        private void InativaComponentes()
        {
            tbCodigoTipoProduto.Enabled = false;
        }
        private void CarregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM TTB2 WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND CODTB2FAT = '"+ codigoTipoProduto +"'");

            // Principal
            tbCodigoTipoProduto.Text = dt.Rows[0]["CODTB2FAT"].ToString();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            chkInativo.Checked = Convert.ToBoolean(dt.Rows[0]["INATIVO"]);
        }

        private bool Salvar()
        {
            int result = 0;

            if (edita == false)
            {
                query = @"INSERT INTO TTB2 (CODCOLIGADA, CODTB2FAT, DESCRICAO, INATIVO) VALUES(?, ?, ?, ?)";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { AppLib.Context.Empresa, tbCodigoTipoProduto.Text, tbDescricao.Text, Convert.ToInt32(chkInativo.Checked) });

                    if (result > 0)
                    {
                        XtraMessageBox.Show("Tipo de Produto cadastrado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível cadastrar o Tipo de Produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                query = @"UPDATE TTB2 
                                SET 
                                DESCRICAO = ?,
                                INATIVO = ?
                                WHERE CODCOLIGADA = ? AND CODTB2FAT = ?";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { tbDescricao.Text, Convert.ToInt32(chkInativo.Checked), AppLib.Context.Empresa, codigoTipoProduto });

                    if (result > 0)
                    {
                        XtraMessageBox.Show("Tipo de Produto editado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível editar o Tipo de Produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
