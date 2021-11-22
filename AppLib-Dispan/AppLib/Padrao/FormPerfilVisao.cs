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
    public partial class FormPerfilVisao : AppLib.Windows.FormVisao
    {
        private static FormPerfilVisao _instance = null;

        public static FormPerfilVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormPerfilVisao();

            return _instance;
        }

        private void FormPerfilVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormPerfilVisao()
        {
            InitializeComponent();
        }

        private void FormPerfilVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormPerfilCadastro f = new FormPerfilCadastro();
            f.Conexao = this.grid1.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.gridData1.Conexao = f.Conexao;
            f.gridData2.Conexao = f.Conexao;
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormPerfilCadastro f = new FormPerfilCadastro();
            f.Conexao = this.grid1.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.gridData1.Conexao = f.Conexao;
            f.gridData2.Conexao = f.Conexao;
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormPerfilCadastro f = new FormPerfilCadastro();
            f.Conexao = this.grid1.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.gridData1.Conexao = f.Conexao;
            f.gridData2.Conexao = f.Conexao;
            f.Excluir(grid1);
        }

        
    }
}
