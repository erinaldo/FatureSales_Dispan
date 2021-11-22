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
    public partial class FormOffClienteVisao : AppLib.Windows.FormVisao
    {
        private static FormOffClienteVisao _instance = null;

        public static FormOffClienteVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormOffClienteVisao();
            return _instance;
        }

        public FormOffClienteVisao()
        {
            InitializeComponent();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormOffClienteCadastro().Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormOffClienteCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            MessageBox.Show("Não é possível realizar exclusão de clientes.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormOffClienteVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
