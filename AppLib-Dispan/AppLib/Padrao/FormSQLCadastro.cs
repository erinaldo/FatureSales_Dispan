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
    public partial class FormSQLCadastro : AppLib.Windows.FormCadastroData
    {
        public FormSQLCadastro()
        {
            InitializeComponent();
        }

        private void FormSQLCadastro_Load(object sender, EventArgs e)
        {
            this.GetProcessos().Add("Testar", null, Testar);
        }

        private void FormSQLCadastro_AntesSalvar(object sender, EventArgs e)
        {
            DateTime AGORA = AppLib.Context.poolConnection.Get().GetDateTime();

            if (Acao == Global.Types.Acao.Novo)
            {
                campoDATACRIACAO.Set(AGORA);
                campoTextoUSUARIOCRIACAO.Set(AppLib.Context.Usuario);
            }

            campoDATAALTERACAO.Set(AGORA);
            campoTextoUSUARIOALTERACAO.Set(AppLib.Context.Usuario);
        }

        private void Testar(object sender, EventArgs e)
        {
            #region CONTA OS PARÂMETROS

            String consulta = richTextBox1.Text;
            int contParametros = 0;
            Boolean cancelou = false;

            for (int i = 0; i < consulta.Length; i++)
            {
                String letra = consulta.Substring(i, 1);

                if (letra.Equals("?"))
                {
                    contParametros++;
                }
            }
            
            #endregion

            #region PEDE OS PARÂMETROS

            List<Object> parametros = new List<Object>();

            for (int i = 0; i < contParametros; i++)
            {
                AppLib.Windows.FormMessagePrompt f = new Windows.FormMessagePrompt();
                String valor = f.Mostrar("Informe o " + (i + 1) + ". parâmetro: ", String.Empty);

                if (f.confirmacao == Global.Types.Confirmacao.OK)
                {
                    parametros.Add(valor);
                }

                if (f.confirmacao == Global.Types.Confirmacao.Cancelar)
                {
                    cancelou = true;
                }

                if (cancelou)
                {
                    i = contParametros;
                }
            }
            
            #endregion

            #region MOSTRA O RESULTADO

            if ( ! cancelou)
            {
                try
                {
                    AppLib.Padrao.FormSQLTeste f = new FormSQLTeste();
                    f.gridControl1.DataSource = AppLib.Context.poolConnection.Get(campoTextoCONEXAO.Get()).ExecQuery(consulta, parametros.ToArray());
                    f.WindowState = FormWindowState.Maximized;
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Erro: " + ex.Message);
                }
            }
            
            #endregion

        }

        private void FormSQLCadastro_AposNovo(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void FormSQLCadastro_AposEditar(object sender, EventArgs e)
        {
            String consulta = @"SELECT COMANDO FROM ZSQL WHERE IDSQL = ?";
            String COMANDO = AppLib.Context.poolConnection.Get().ExecGetField(String.Empty, consulta, new Object[] { campoInteiroIDSQL.Get() }).ToString();
            richTextBox1.Text = COMANDO;
        }

        private void FormSQLCadastro_AposSalvar(object sender, EventArgs e)
        {
            String comando = @"UPDATE ZSQL SET COMANDO = '"+ richTextBox1.Text.Replace("'", "''") +"' WHERE IDSQL = " + campoInteiroIDSQL.Get();
            int temp = AppLib.Context.poolConnection.Get().ExecTransaction(comando, new Object[] { });
        }

    }
}
