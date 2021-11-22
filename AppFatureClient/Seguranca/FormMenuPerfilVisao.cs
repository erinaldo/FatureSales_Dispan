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
    public partial class FormMenuPerfilVisao : AppLib.Windows.FormVisao
    {
        public FormMenuPerfilVisao()
        {
            InitializeComponent();
        }

        private void FormMenuPerfilVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormMenuPerfilCadastro().Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormMenuPerfilCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormMenuPerfilCadastro().Excluir(grid1.GetDataRows());
        }
    }
}
