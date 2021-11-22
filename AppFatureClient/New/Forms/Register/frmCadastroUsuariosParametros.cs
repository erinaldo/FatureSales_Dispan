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
    public partial class frmCadastroUsuariosParametros : Form
    {
        public int ID = 0;
        public bool edita = false;
        private string query = "";
        public frmCadastroUsuariosParametros()
        {
            InitializeComponent();
        }

        private void frmCadastroUsuariosParametros_Load(object sender, EventArgs e)
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
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZUSUARIOPARAM WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND ID = "+ ID +"");

            chkTabPorEnter.Checked = Convert.ToBoolean(dt.Rows[0]["TABPORENTER"]);
        }

        private bool Salvar()
        {
            int result = 0;

            if (edita == false)
            {
                query = @"INSERT INTO ZUSUARIOPARAM VALUES(?, ?, ?)";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { AppLib.Context.Usuario, AppLib.Context.Empresa, Convert.ToInt32(chkTabPorEnter.Checked) });

                    if (result > 0)
                    {
                        ID = getID();

                        XtraMessageBox.Show("Parâmetros cadastrados com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível cadastrar os parâmetros.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                query = @"UPDATE ZUSUARIOPARAM 
                                SET 
                                TABPORENTER = ?                               
                                WHERE CODCOLIGADA = ? AND CODUSUARIO = ? AND ID = ?";

                try
                {
                    result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { Convert.ToInt32(chkTabPorEnter.Checked), AppLib.Context.Empresa, AppLib.Context.Usuario, ID });

                    if (result > 0)
                    {
                        XtraMessageBox.Show("Parâmetros editados com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Não foi possível editar os parâmetros.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private int getID()
        {
            int id = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT MAX(ID) FROM ZUSUARIOPARAM", new object[] { }));

            return id;
        }

        #endregion
    }
}
