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
    public partial class FormCondPagamentoVisao : AppLib.Windows.FormVisao
    {
        private static FormCondPagamentoVisao _instance = null;

        public static FormCondPagamentoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormCondPagamentoVisao();
            return _instance;
        } 

        public FormCondPagamentoVisao()
        {
            InitializeComponent();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormCondPagamentoCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormCondPagamentoCadastro().Excluir(grid1.GetDataRows()); 
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormCondPagamentoCadastro().Novo();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void FormCondPagamentoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
