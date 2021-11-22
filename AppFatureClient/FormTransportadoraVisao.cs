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
    public partial class FormTransportadoraVisao : AppLib.Windows.FormVisao
    {
        private static FormTransportadoraVisao _instance = null;

        public static FormTransportadoraVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormTransportadoraVisao();
            return _instance;
        }

        public FormTransportadoraVisao()
        {
            InitializeComponent();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormTransportadoraCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormTransportadoraCadastro().Excluir(grid1.GetDataRows()); 
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormTransportadoraCadastro().Novo();
        }

        private void FormTransportadoraVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
