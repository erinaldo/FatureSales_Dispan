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
    public partial class FormMenuVisao : AppLib.Windows.FormVisao
    {
        private static FormMenuVisao _instance = null;

        public static FormMenuVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormMenuVisao();

            return _instance;
        }

        private void FormMenuVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormMenuVisao()
        {
            InitializeComponent();
        }

        private void FormMenuVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormMenuCadastro f = new FormMenuCadastro();
            f.Conexao = this.grid1.Conexao;
            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormMenuCadastro f = new FormMenuCadastro();
            f.Conexao = this.grid1.Conexao;
            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormMenuCadastro f = new FormMenuCadastro();
            f.Conexao = this.grid1.Conexao;
            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }
            f.Excluir(grid1);
        }

        
    }
}
