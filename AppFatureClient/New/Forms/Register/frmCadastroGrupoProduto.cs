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
    public partial class frmCadastroGrupoProduto : Form
    {
        public string codigoGrupoProduto = "";
        public bool edita = false;
        private string query = "";
        public frmCadastroGrupoProduto()
        {
            InitializeComponent();
        }

        private void frmCadastroGrupoProduto_Load(object sender, EventArgs e)
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
                codigoGrupoProduto = tbCodigoGrupoProduto.Text;
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
            tbCodigoGrupoProduto.Enabled = false;
        }

        private void CarregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM TTB3 WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND CODTB3FAT = '" + codigoGrupoProduto + "'");

            // Principal
            tbCodigoGrupoProduto.Text = dt.Rows[0]["CODTB3FAT"].ToString();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            chkInativo.Checked = Convert.ToBoolean(dt.Rows[0]["INATIVO"]);
        }

        private bool Salvar()
        {
            int result = 0;

            if (edita == false)
            {
                query = @"INSERT INTO TTB3 (CODCOLIGADA, CODTB3FAT, DESCRICAO, INATIVO) VALUES(?, ?, ?, ?)";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { AppLib.Context.Empresa, tbCodigoGrupoProduto.Text, tbDescricao.Text, Convert.ToInt32(chkInativo.Checked) });

                    if (result > 0)
                    {
                        XtraMessageBox.Show("Grupo de Produto cadastrado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível cadastrar o Grupo de Produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                query = @"UPDATE TTB3 
                                SET 
                                DESCRICAO = ?,
                                INATIVO = ?
                                WHERE CODCOLIGADA = ? AND CODTB3FAT = ?";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { tbDescricao.Text, Convert.ToInt32(chkInativo.Checked), AppLib.Context.Empresa, codigoGrupoProduto });

                    if (result > 0)
                    {
                        XtraMessageBox.Show("Grupo de Produto editado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível editar o Grupo de Produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
