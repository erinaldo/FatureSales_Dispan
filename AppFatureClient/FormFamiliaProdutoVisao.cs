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
    public partial class FormFamiliaProdutoVisao : AppLib.Windows.FormVisao
    {
        private static FormFamiliaProdutoVisao _instance = null;

        public static FormFamiliaProdutoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormFamiliaProdutoVisao();
            return _instance;
        }

        public FormFamiliaProdutoVisao()
        {
            InitializeComponent();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormFamiliaProdutoCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormFamiliaProdutoCadastro().Excluir(grid1.GetDataRows());
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormFamiliaProdutoCadastro().Novo();
        }

        private void FormFamiliaProdutoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void FormFamiliaProdutoVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Aplica Representante", null, aplicaRepresentante);
        }

        private void aplicaRepresentante(object sender, EventArgs e)
        {
            DataRowCollection drc = grid1.GetDataRows();
            if (drc != null)
            {
                FormProcessoAplicarRepresentante frm = new FormProcessoAplicarRepresentante(drc);
                frm.ShowDialog();
            }
        }
    }
}
