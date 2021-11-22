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
    public partial class FormPerfilVisao : AppLib.Windows.FormVisao
    {
        public FormPerfilVisao()
        {
            InitializeComponent();
        }

        private void FormPerfilVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormPerfilCadastro().Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormPerfilCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormPerfilCadastro().Excluir(grid1.GetDataRows());
        }
    }
}
