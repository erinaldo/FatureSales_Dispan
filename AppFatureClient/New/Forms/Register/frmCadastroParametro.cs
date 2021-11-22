using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.New.Forms.Register
{
    public partial class frmCadastroParametro : Form
    {
        public frmCadastroParametro()
        {
            InitializeComponent();
        }

        private void frmCadastroParametro_Load(object sender, EventArgs e)
        {
            CarregaCampos();
        }

        private void btnVersaoSistema_Click(object sender, EventArgs e)
        {
            tbVersaoClient.Text = New.Class.EnviromentHelper.Versao;
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar())
            {
                XtraMessageBox.Show("Parâmetros editados com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                XtraMessageBox.Show("Não foi possível editar os parâmetros.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Salvar())
            {
                XtraMessageBox.Show("Parâmetros editados com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                XtraMessageBox.Show("Não foi possível editar os parâmetros.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        private void CarregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZPARAMFATURE WHERE CODCOLIGADA = "+ AppLib.Context.Empresa +"");

            // Geral
            chkIntegraCliente.Checked = Convert.ToBoolean(dt.Rows[0]["INTCLIFOR"]);
            chkIntegraMovimentoOrcamento.Checked = Convert.ToBoolean(dt.Rows[0]["INTMOVORC"]);
            chkFaturaMovimento.Checked = Convert.ToBoolean(dt.Rows[0]["INTFATURA"]);
            chkCancelaMovimento.Checked = Convert.ToBoolean(dt.Rows[0]["INTCANMOV"]);
            chkExcluiMovimento.Checked = Convert.ToBoolean(dt.Rows[0]["INTEXCMOV"]);
            chkAtualizaRegistroMovimento.Checked = Convert.ToBoolean(dt.Rows[0]["ATUREGMOV"]);
            chkRegistroCliente.Checked = Convert.ToBoolean(dt.Rows[0]["ATUREGCLI"]);
            chkVisualizaVendaGeral.Checked = Convert.ToBoolean(dt.Rows[0]["EDITAORCOUTVEN"]);

            // Controle de Versão
            tbVersaoClient.Text = dt.Rows[0]["VERSAOCLIENT"].ToString();
            tbVersaoServer.Text = dt.Rows[0]["VERSAOSERVER"].ToString();
            chkExigeVersao.Checked = Convert.ToBoolean(dt.Rows[0]["EXIGEVERSAO"]);

            // E-mail
            tbUsuarioEmail.Text = dt.Rows[0]["USUARIOEMAIL"].ToString();
            tbSenhaEmail.Text = dt.Rows[0]["SENHAEMAIL"].ToString();
            tbEnderecoEmail.Text = dt.Rows[0]["ENDERECOEMAIL"].ToString();
            tbPorta.Text = dt.Rows[0]["PORTAEMAIL"].ToString();
            chkSSL.Checked = Convert.ToBoolean(dt.Rows[0]["SSLEMAIL"]);
            tbEnviarComo.Text = dt.Rows[0]["ENVIARCOMO"].ToString();
            tbEnviarComoDisplay.Text = dt.Rows[0]["ENVIARCOMODISPLAY"].ToString();
            tbMotivo.Text = dt.Rows[0]["MOTIVOEMAIL"].ToString();
            tbEnviarPara.Text = dt.Rows[0]["ENVIARPARA"].ToString();
            tbCorpoEmail.Text = dt.Rows[0]["CORPOEMAIL"].ToString();
            tbCorpoEmailPedido.Text = dt.Rows[0]["CORPOEMAILPEDIDO"].ToString();

            // Orçamento
            tbValidadeProposta.Text = dt.Rows[0]["VALPROPOSTA"].ToString();

            // Preço
            tbPrazoRecalculoPreco.Text = dt.Rows[0]["PRAZORECALPRECO"].ToString();
            chkUsaPrecoCalculado.Checked = Convert.ToBoolean(dt.Rows[0]["USAPRECOCALCULADO"]);
            chkUsaPrecoCalculadoRevenda.Checked = Convert.ToBoolean(dt.Rows[0]["USAPRECOCALCULADOREVENDA"]);
            chkUsaPrecoCalculadoFixo.Checked = Convert.ToBoolean(dt.Rows[0]["USAPRECOCALCULADOFIXO"]);
        }
        private bool Salvar()
        {
            string query = @"UPDATE ZPARAMFATURE 
                                SET 
                                INTCLIFOR = ?, 
                                INTMOVORC = ?, 
                                INTFATURA = ?,
                                INTCANMOV = ?,
                                INTEXCMOV = ?,
                                ATUREGMOV = ?,
                                EXIGEVERSAO = ?,
                                VERSAOCLIENT = ?,
                                VERSAOSERVER = ?,
                                ATUREGCLI = ?,
                                ENDERECOEMAIL = ?,
                                PORTAEMAIL = ?,
                                USUARIOEMAIL = ?,
                                SENHAEMAIL = ?,
                                SSLEMAIL = ?,
                                ENVIARCOMO = ?,
                                ENVIARCOMODISPLAY = ?,
                                ENVIARPARA = ?,
                                VALPROPOSTA = ?,
                                MOTIVOEMAIL = ?,
                                CORPOEMAIL = ?,
                                EDITAORCOUTVEN = ?,
                                PRAZORECALPRECO = ?,
                                CORPOEMAILPEDIDO = ?,
                                USAPRECOCALCULADO = ?,
                                USAPRECOCALCULADOREVENDA = ?,
                                USAPRECOCALCULADOFIXO = ?
                                WHERE CODCOLIGADA = ?";

            try
            {
                int result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] 
                { 
                    Convert.ToInt32(chkIntegraCliente.Checked), 
                    Convert.ToInt32(chkIntegraMovimentoOrcamento.Checked),
                    Convert.ToInt32(chkFaturaMovimento.Checked),
                    Convert.ToInt32(chkCancelaMovimento.Checked),
                    Convert.ToInt32(chkExcluiMovimento.Checked),
                    Convert.ToInt32(chkAtualizaRegistroMovimento.Checked),
                    Convert.ToInt32(chkExigeVersao.Checked),
                    (tbVersaoClient.Text == "" ? null : tbVersaoClient.Text),
                    (tbVersaoServer.Text == "" ? null : tbVersaoServer.Text),
                    Convert.ToInt32(chkRegistroCliente.Checked),
                    tbEnderecoEmail.Text,
                    tbPorta.Text,
                    tbUsuarioEmail.Text,
                    tbSenhaEmail.Text,
                    Convert.ToInt32(chkSSL.Checked),
                    tbEnviarComo.Text,
                    tbEnviarComoDisplay.Text,
                    tbEnviarPara.Text,
                    tbValidadeProposta.Text,
                    tbMotivo.Text,
                    tbCorpoEmail.Text,
                    Convert.ToInt32(chkVisualizaVendaGeral.Checked),
                    tbPrazoRecalculoPreco.Text,
                    tbCorpoEmailPedido.Text,
                    Convert.ToInt32(chkUsaPrecoCalculado.Checked),
                    Convert.ToInt32(chkUsaPrecoCalculadoRevenda.Checked),
                    Convert.ToInt32(chkUsaPrecoCalculadoFixo.Checked),
                    AppLib.Context.Empresa
                });

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion
    }
}
