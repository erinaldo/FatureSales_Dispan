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
    public partial class FormRepresentantesVisao : AppLib.Windows.FormVisao
    {
        private static FormRepresentantesVisao _instance = null;

        public static FormRepresentantesVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormRepresentantesVisao();
            return _instance;
        }
        public FormRepresentantesVisao()
        {
            InitializeComponent();
        }

        private void FormRepresentantesVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void grid1_SetParametros_1(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Editar_1(object sender, EventArgs e)
        {
            new FormRepresentantesCadastro(true).Editar(grid1.bs);
        }

        private void grid1_Excluir_1(object sender, EventArgs e)
        {
            new FormRepresentantesCadastro().Excluir(grid1.GetDataRows());
        }

        private void grid1_Novo_1(object sender, EventArgs e)
        {
            new FormRepresentantesCadastro().Novo();
        }
    }
}
