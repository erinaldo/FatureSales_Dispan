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
    public partial class FormConexaoVisao : AppLib.Windows.FormVisao
    {
        private static FormConexaoVisao _instance = null;

        public static FormConexaoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormConexaoVisao();

            return _instance;
        }

        private void FormConexaoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormConexaoVisao()
        {
            InitializeComponent();
        }

        private void FormConexaoVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormConexaoCadastro f = new FormConexaoCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormConexaoCadastro f = new FormConexaoCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormConexaoCadastro f = new FormConexaoCadastro();
            f.Excluir(grid1);
        }

        private void grid1_AposAtualizar(object sender, EventArgs e)
        {

        }

        private void grid1_AntesAtualizar(object sender, EventArgs e)
        {
            
        }

        
    }
}
