using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient.Seguranca
{
    public partial class FormProcessoPerfilVisao : AppLib.Windows.FormVisao
    {
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
