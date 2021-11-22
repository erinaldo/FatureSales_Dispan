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
    public partial class FormAlterarCaminhao : Form
    {
        public DateTime dataCarregamento;
        public int idProgramacaoCarregamento;
        public FormAlterarCaminhao()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbCaminhao.Text) || string.IsNullOrEmpty(cmbTipo.Text))
            {
                MessageBox.Show("Favor selecionar umas das opção válidas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE ZPROGRAMACAOCARREGAMENTO SET CAMINHAO = ?, TIPOCAMINHAO = ? WHERE IDPROGRAMACAOCARREGAMENTO = ?", new object[] { cmbCaminhao.Text, cmbTipo.Text, idProgramacaoCarregamento });
            MessageBox.Show("Alteração realizada com sucesso.","Informãção do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void FormAlterarCaminhao_Load(object sender, EventArgs e)
        {
            verificaCaminhao();
        }
        private void verificaCaminhao()
        {
            int caminhao = 1;
            string sql = @"SELECT CONVERT(INT, CAMINHAO) CAMINHAO FROM ZPROGRAMACAOCARREGAMENTO WHERE DATAPROGRAMADA = ? AND CODCOLIGADA = ? GROUP BY CAMINHAO ORDER BY CAMINHAO";
            DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new object[] { dataCarregamento, AppLib.Context.Empresa });
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (!string.IsNullOrEmpty(dt.Rows[i]["CAMINHAO"].ToString()))
                {
                    if (caminhao.Equals(Convert.ToInt32(dt.Rows[i]["CAMINHAO"])))
                    {
                        caminhao++;
                    }
                }


            }
            cmbCaminhao.Text = Convert.ToString(caminhao);
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new object[] { dataCarregamento, AppLib.Context.Empresa };
        }
    }
}
