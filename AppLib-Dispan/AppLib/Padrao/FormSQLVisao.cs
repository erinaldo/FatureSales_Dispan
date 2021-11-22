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
    public partial class FormSQLVisao : AppLib.Windows.FormVisao
    {
        private static FormSQLVisao _instance = null;

        public static FormSQLVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormSQLVisao();

            return _instance;
        }

        private void FormSQLVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormSQLVisao()
        {
            InitializeComponent();
        }

        private void FormSQLVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormSQLCadastro f = new FormSQLCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormSQLCadastro f = new FormSQLCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormSQLCadastro f = new FormSQLCadastro();
            f.Excluir(grid1);
        }

        
    }
}
