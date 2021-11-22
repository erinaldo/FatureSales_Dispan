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
    public partial class FormLogIntegracaoVisao : AppLib.Windows.FormVisao
    {
        private static FormLogIntegracaoVisao _instance = null;

        public static FormLogIntegracaoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormLogIntegracaoVisao();
            return _instance;
        }

        public FormLogIntegracaoVisao()
        {
            InitializeComponent();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormLogIntegracaoCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormLogIntegracaoCadastro().Excluir(grid1.GetDataRows());
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void FormLogIntegracaoVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Retornar para Correção", null, EnviarFilaIntegracao);
        }

        public void EnviarFilaIntegracao(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8009", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    FormEnviarFilaIntegracao f = new FormEnviarFilaIntegracao();
                    f.Movimentos = grid1.GetDataRows();
                    f.ShowDialog();

                    grid1.toolStripButtonATUALIZAR_Click(this, null);
                }
            }
        }

        private void FormLogIntegracaoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
