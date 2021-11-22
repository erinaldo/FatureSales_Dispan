using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Padrao
{
    public partial class FormMudarSenha : DevExpress.XtraEditors.XtraForm
    {
        public FormMudarSenha()
        {
            InitializeComponent();
        }

        private void FormMudarSenha_Load(object sender, EventArgs e)
        {
            textBoxSenhaAtual.Focus();
        }

        public Boolean Validar()
        {
            // Senha atual inválida.
            String SENHAATUALBD = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, "SELECT SENHA FROM ZUSUARIO WHERE USUARIO = ?", new Object[] { AppLib.Context.Usuario }).ToString();
            String SENHAATUALTELA = new AppLib.Util.Criptografia().Hash(AppLib.Util.Criptografia.OpcoesHash.SHA512, textBoxSenhaAtual.Text);
            if ( ! SENHAATUALBD.Equals(SENHAATUALTELA))
            {
                AppLib.Windows.FormMessageDefault.ShowError("Senha atual inválida.");
                return false;
            }

            // Use no mínimo 6 caracteres.
            if (textBoxNovaSenha.Text.Length < 6)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Use no mínimo 6 caracteres.");
                return false;
            }

            // Nova senha não confere.
            if (!textBoxNovaSenha.Text.Equals(textBoxConfirmeNovaSenha.Text))
            {
                AppLib.Windows.FormMessageDefault.ShowError("Nova senha não confere.");
                return false;
            }

            // Informe uma nova senha diferente e confirme.
            if (textBoxSenhaAtual.Text.Equals(textBoxNovaSenha.Text))
            {
                AppLib.Windows.FormMessageDefault.ShowError("Informe uma nova senha diferente e confirme.");
                return false;
            }

            // Nova senha não é segura.
            if (textBoxNovaSenha.Text.Equals("123456") || 
                textBoxNovaSenha.Text.Equals("111111") || 
                textBoxNovaSenha.Text.Equals("222222") || 
                textBoxNovaSenha.Text.Equals("333333") || 
                textBoxNovaSenha.Text.Equals("444444") || 
                textBoxNovaSenha.Text.Equals("555555") || 
                textBoxNovaSenha.Text.Equals("666666") || 
                textBoxNovaSenha.Text.Equals("777777") || 
                textBoxNovaSenha.Text.Equals("888888") || 
                textBoxNovaSenha.Text.Equals("999999") || 
                textBoxNovaSenha.Text.Equals("000000"))
            {
                AppLib.Windows.FormMessageDefault.ShowError("Nova senha não é segura.");
                return false;
            }

            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.Validar())
            {
                String SENHA = new AppLib.Util.Criptografia().Hash(AppLib.Util.Criptografia.OpcoesHash.SHA512, textBoxNovaSenha.Text);

                AppLib.ORM.Jit reg = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "ZUSUARIO");
                reg.Set("USUARIO", AppLib.Context.Usuario);
                reg.Select();

                reg.Set("SENHA", SENHA);
                reg.Set("DATAALTERACAO", DateTime.Now);
                reg.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                reg.Save();

                AppLib.Windows.FormMessageDefault.ShowInfo("Senha alterada com sucesso.");
                this.Close();
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxSenhaAtual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxNovaSenha.Focus();
            }
        }

        private void textBoxNovaSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxConfirmeNovaSenha.Focus();
            }
        }

        private void textBoxConfirmeNovaSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(this, null);
            }
        }
    }
}
