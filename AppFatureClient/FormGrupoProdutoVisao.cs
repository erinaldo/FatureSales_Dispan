using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormGrupoProdutoVisao : AppLib.Windows.FormVisao
    {
        private static FormGrupoProdutoVisao _instance = null;

        public static FormGrupoProdutoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormGrupoProdutoVisao();
            return _instance;
        }

        public FormGrupoProdutoVisao()
        {
            InitializeComponent();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormGrupoProdutoCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormGrupoProdutoCadastro().Excluir(grid1.GetDataRows()); 
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormGrupoProdutoCadastro().Novo();
        }

        private void FormGrupoProdutoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
