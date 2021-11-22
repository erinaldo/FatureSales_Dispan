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
    public partial class FormLimiteCredito : Form
    {
        public DataRow Cliente { get; set; }

        public FormLimiteCredito()
        {
            InitializeComponent();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja alterar o limite de crédito ?", "Mensagem", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string sSql = "UPDATE FCFO SET LIMITECREDITO = ? WHERE CODCOLIGADA = ? AND CODCFO = ?";
                AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { campoDecimal1.Get(), Cliente["CODCOLIGADA"], Cliente["CODCFO"] });

                this.Cursor = Cursors.Default;

                MessageBox.Show("Operação realizada com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormLimiteCredito_Load(object sender, EventArgs e)
        {
            lblValorAberto.Text = "0,00";
            lblSaldo.Text = "0,00";

            campoTexto1CODCFO.Set(Cliente["CODCFO"].ToString());
            campoTexto1NOMEFANTASIA.Set(Cliente["NOMEFANTASIA"].ToString());

            string sSql = "SELECT ISNULL(LIMITECREDITO,1) FROM FCFO WHERE CODCOLIGADA = ? AND CODCFO = ?";
            campoDecimal1.Set(Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { Cliente["CODCOLIGADA"], Cliente["CODCFO"] })));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sSql = @"SELECT ISNULL(X.VALORABERTO,0) VALORABERTO
FROM
(
SELECT 
SUM(ISNULL(L.VALORORIGINAL,0)) - SUM(ISNULL(L.VALORBAIXADO,0))  VALORABERTO
FROM   FLAN L (NOLOCK), FTDO D (NOLOCK), FCFO F
WHERE  L.STATUSLAN IN (0, 4) 
AND L.NFOUDUP <> 1
AND L.CODCOLIGADA = D.CODCOLIGADA 
AND L.CODTDO = D.CODTDO 
AND D.EDEVOLUCAO <> 4
AND D.EDEVOLUCAO <> 2
AND L.CODCOLCFO = ?
AND L.CODCFO = ?
AND L.PAGREC = 1
AND F.CODCOLIGADA = L.CODCOLCFO
AND F.CODCFO = L.CODCFO
)X";

                Decimal? ValorAberto = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, sSql, 0, campoTexto1CODCFO.Get()));
                Decimal? LimiteCredito = campoDecimal1.Get();
                lblValorAberto.Text = string.Format("{0:n}", ValorAberto);
                lblSaldo.Text = string.Format("{0:n}", LimiteCredito - ValorAberto);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Erro ao calcular o valor em aberto.", (ex.InnerException != null ? ex.InnerException.Message : ex.Message)));
            }
        }
    }
}
