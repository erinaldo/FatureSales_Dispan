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
    public partial class FormProcessoVisao : AppLib.Windows.FormVisao
    {
        private static FormProcessoVisao _instance = null;

        public static FormProcessoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormProcessoVisao();

            return _instance;
        }

        private void FormProcessoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormProcessoVisao()
        {
            InitializeComponent();
        }

        private void FormProcessoVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormProcessoCadastro f = new FormProcessoCadastro();
            f.Conexao = this.grid1.Conexao;
            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormProcessoCadastro f = new FormProcessoCadastro();
            f.Conexao = this.grid1.Conexao;
            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormProcessoCadastro f = new FormProcessoCadastro();
            f.Conexao = this.grid1.Conexao;
            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }
            f.Excluir(grid1);
        }

        
    }
}
