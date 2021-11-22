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
    public partial class FormUsuarioVisao : AppLib.Windows.FormVisao
    {
        public FormUsuarioVisao()
        {
            InitializeComponent();
        }

        private void FormUsuarioVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormUsuarioCadastro().Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormUsuarioCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormUsuarioCadastro().Excluir(grid1.GetDataRows());
        }
    }
}
