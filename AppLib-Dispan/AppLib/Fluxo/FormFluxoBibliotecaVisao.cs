using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormFluxoBibliotecaVisao : AppLib.Windows.FormVisao
    {
        private static FormFluxoBibliotecaVisao _instance = null;

        public static FormFluxoBibliotecaVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormFluxoBibliotecaVisao();

            return _instance;
        }

        private void FormFluxoBibliotecaVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormFluxoBibliotecaVisao()
        {
            InitializeComponent();
        }

        private void FormFluxoBibliotecaVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormFluxoBibliotecaCadastro f = new FormFluxoBibliotecaCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormFluxoBibliotecaCadastro f = new FormFluxoBibliotecaCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormFluxoBibliotecaCadastro f = new FormFluxoBibliotecaCadastro();
            f.Excluir(grid1);
        }


    }
}
