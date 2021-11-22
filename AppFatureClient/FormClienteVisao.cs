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
    public partial class FormClienteVisao : AppLib.Windows.FormVisao
    {
        private static FormClienteVisao _instance = null;

        public static FormClienteVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormClienteVisao();
            return _instance;
        }        

        public FormClienteVisao()
        {
            InitializeComponent();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormClienteCadastro().Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormClienteCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            MessageBox.Show("Não é possível realizar exclusão de clientes.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormClienteVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Alterar Limite de Crédito", null, AlterarLimiteCredito);
        }

        public void AlterarLimiteCredito(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8001", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FormLimiteCredito f = new FormLimiteCredito();
                    f.Cliente = grid1.GetDataRow();
                    f.ShowDialog();
                }
            }
        }

        private void FormClienteVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
