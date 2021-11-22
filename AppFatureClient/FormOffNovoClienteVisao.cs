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
    public partial class FormOffNovoClienteVisao : AppLib.Windows.FormVisao
    {
        private static FormOffNovoClienteVisao _instance = null;

        public static FormOffNovoClienteVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormOffNovoClienteVisao();
            return _instance;
        }

        public FormOffNovoClienteVisao()
        {
            InitializeComponent();

            //this.grid1.GetProcessos().Add("Enviar Cliente Selecionados", null, EnviarClientesSelecionados);
        }

        public void EnviarClientesSelecionados(object sender, EventArgs e)
        {
            System.Data.DataRowCollection drc = grid1.GetDataRows();

            if (drc != null)
            {
                MessageBox.Show("ATENÇÃO: Os cliestes selecionados serão liberados para envio, mas o envio ocorrerá somente se estiver conectado á internet.\r\n\nCaso não esteja conectado á internet:\r\n- Feche este programa de digitação de novos clientes.\r\n- Conecte á internet.\r\n- Abra este programa novamente.\r\n- Faça o login para que os clientes sejam enviados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                for (int i = 0; i < drc.Count; i++)
                {
                    int CODCOLIGADA = int.Parse(drc[i]["CODCOLIGADA"].ToString());
                    string IDOFF = drc[i]["IDOFF"].ToString();

                    String Comando1 = "UPDATE ZFCFOX SET ENVIAR = 1, RECCREATEDBY = ?, RECCREATEDON = ?, RECMODIFIEDBY = ?, RECMODIFIEDON = ? WHERE CODCOLIGADA = ? AND IDOFF = ?";
                    int temp1 = AppLib.Context.poolConnection.Get("Conn2").ExecTransaction(Comando1, new Object[] { AppLib.Context.Usuario, DateTime.Now, AppLib.Context.Usuario, DateTime.Now, CODCOLIGADA, IDOFF });
                }

                grid1.toolStripButtonATUALIZAR_Click(this, null);
            }
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormOffNovoClienteCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            MessageBox.Show("Não é possível realizar exclusão de clientes.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormOffNovoClienteCadastro().Novo();
        }

        private void FormOffNovoClienteVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
