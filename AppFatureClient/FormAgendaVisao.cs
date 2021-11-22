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
    public partial class FormAgendaVisao : AppLib.Windows.FormVisao
    {
        private static FormAgendaVisao _instance = null;

        public static FormAgendaVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormAgendaVisao();
            return _instance;
        }
        public FormAgendaVisao()
        {
            InitializeComponent();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormAgendaCadastro f = new FormAgendaCadastro();
            f.Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir esse registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                FormAgendaCadastro f = new FormAgendaCadastro();
                f.Excluir(grid1.GetDataRows());
            }
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormAgendaCadastro f = new FormAgendaCadastro();
            f.Novo();
        }

        private void FormAgendaVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
