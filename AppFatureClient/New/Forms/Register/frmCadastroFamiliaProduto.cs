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
    public partial class frmCadastroFamiliaProduto : Form
    {
        public string codigoFamiliaProduto = "";
        public bool edita = false;
        private string query = "";
        public frmCadastroFamiliaProduto()
        {
            InitializeComponent();
        }

        private void frmCadastroFamiliaProduto_Load(object sender, EventArgs e)
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
                codigoFamiliaProduto = tbCodigoFamiliaProduto.Text;
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
            tbCodigoFamiliaProduto.Enabled = false;
        }
        private void CarregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM TTB4 WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND CODTB4FAT = '" + codigoFamiliaProduto + "'");

            // Principal
            tbCodigoFamiliaProduto.Text = dt.Rows[0]["CODTB4FAT"].ToString();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            chkInativo.Checked = Convert.ToBoolean(dt.Rows[0]["INATIVO"]);
        }

        private bool Salvar()
        {
            int result = 0;

            if (edita == false)
            {
                query = @"INSERT INTO TTB4 (CODCOLIGADA, CODTB4FAT, DESCRICAO, INATIVO) VALUES(?, ?, ?, ?)";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { AppLib.Context.Empresa, tbCodigoFamiliaProduto.Text, tbDescricao.Text, Convert.ToInt32(chkInativo.Checked) });

                    if (result > 0)
                    {
                        XtraMessageBox.Show("Família do Produto cadastrada com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível cadastrar a Família do Produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                query = @"UPDATE TTB4 
                                SET 
                                DESCRICAO = ?,
                                INATIVO = ?
                                WHERE CODCOLIGADA = ? AND CODTB4FAT = ?";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { tbDescricao.Text, Convert.ToInt32(chkInativo.Checked), AppLib.Context.Empresa, codigoFamiliaProduto });

                    if (result > 0)
                    {
                        XtraMessageBox.Show("Família do Produto editada com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível editar a Família do Produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
