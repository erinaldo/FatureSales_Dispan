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
    public partial class FormProcessoPerfilVisao : AppLib.Windows.FormVisao
    {
        private static FormProcessoPerfilVisao _instance = null;

        public static FormProcessoPerfilVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormProcessoPerfilVisao();

            return _instance;
        }

        private void FormProcessoPerfilVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormProcessoPerfilVisao()
        {
            InitializeComponent();
        }

        private void FormProcessoPerfilVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormProcessoPerfilCadastro().Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormProcessoPerfilCadastro f = new FormProcessoPerfilCadastro();
            f.Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormProcessoPerfilCadastro f = new FormProcessoPerfilCadastro();
            f.Excluir(grid1.GetDataRows());
        }

        
    }
}
