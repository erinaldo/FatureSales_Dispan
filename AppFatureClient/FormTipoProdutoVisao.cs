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
    public partial class FormTipoProdutoVisao : AppLib.Windows.FormVisao
    {
        private static FormTipoProdutoVisao _instance = null;

        public static FormTipoProdutoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormTipoProdutoVisao();
            return _instance;
        }

        public FormTipoProdutoVisao()
        {
            InitializeComponent();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormTipoProdutoCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormTipoProdutoCadastro().Excluir(grid1.GetDataRows()); 
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormTipoProdutoCadastro().Novo();
        }

        private void FormTipoProdutoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
