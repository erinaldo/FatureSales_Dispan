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
    public partial class FormProcessoVisao : AppLib.Windows.FormVisao
    {
        public FormProcessoVisao()
        {
            InitializeComponent();
        }

        private void FormProcessoVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormProcessoCadastro().Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormProcessoCadastro f = new FormProcessoCadastro();
            f.Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormProcessoCadastro f = new FormProcessoCadastro();
            f.Excluir(grid1.GetDataRows());
        }
    }
}
