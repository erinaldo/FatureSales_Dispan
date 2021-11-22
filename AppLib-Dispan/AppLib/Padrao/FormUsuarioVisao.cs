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
    public partial class FormUsuarioVisao : AppLib.Windows.FormVisao
    {
        private static FormUsuarioVisao _instance = null;

        public static FormUsuarioVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormUsuarioVisao();

            return _instance;
        }

        private void FormUsuarioVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormUsuarioVisao()
        {
            InitializeComponent();
        }

        private void FormUsuarioVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Resetar Senha", null, ResetarSenha);
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormUsuarioCadastro f = new FormUsuarioCadastro();
            f.Conexao = this.grid1.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.campoLookup1.Conexao = f.Conexao;
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormUsuarioCadastro f = new FormUsuarioCadastro();
            f.Conexao = this.grid1.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.campoLookup1.Conexao = f.Conexao;
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormUsuarioCadastro f = new FormUsuarioCadastro();
            f.Conexao = this.grid1.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.campoLookup1.Conexao = f.Conexao;
            f.Excluir(grid1);
        }

        private void ResetarSenha(object sender, EventArgs e)
        {
            DataRowCollection drc = grid1.GetDataRows();

            if (drc.Count > 0)
            {
                for (int i = 0; i < drc.Count; i++)
                {
                    String USUARIO = drc[i]["USUARIO"].ToString();

                    String comando = @"UPDATE ZUSUARIO SET SENHA = 'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==' WHERE USUARIO = ?";

                    int temp = AppLib.Context.poolConnection.Get().ExecTransaction(comando, new Object[] { USUARIO });

                    if (temp == 1)
                    {
                        // OK
                    }
                    else
                    {
                        AppLib.Windows.FormMessageDefault.ShowError("Erro ao resetar a senha do usuario: " + USUARIO);
                    }
                }

                grid1.Atualizar(false);
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowError("Selecione o(s) registro(s).");
            }
        }

        

    }
}
