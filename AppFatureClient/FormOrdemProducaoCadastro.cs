using AppFatureClient.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormOrdemProducaoCadastro : AppLib.Windows.FormCadastroData
    {
        // João Pedro Luchiari - 03/01/2018

        public DataRow row;

        public FormOrdemProducaoCadastro()
        {
            InitializeComponent();

            try
            {
                if (MetodosSQL.VerificaPermissao("APP8049"))
                {
                    groupBox1.Visible = false;
                    gridData1.Visible = false;
                    gridParcelas.Visible = false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        private bool campoLookupCODCFO_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCFO, "SELECT CODCFO, NOMEFANTASIA FROM FCFO WHERE CODCOLIGADA IN (0, ?)", new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookupCODCPG_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT CODCPG, NOME
FROM TCPG
WHERE CODCOLIGADA = ?
  AND PLANOVENDA = 1
  AND INATIVO = 0";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCPG, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormLancamentoCadastro f = new FormLancamentoCadastro();
            f.CodColigada = Convert.ToInt32(campoInteiro2.Get());
            f.IdMov = Convert.ToInt32(campoInteiro1.Get());
            f.Editar(gridData1.bs);
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new object[] { campoInteiro2.Get(), campoInteiro1.Get() };
        }

        private void campoInteiroPRAZOENTREGA_Leave(object sender, EventArgs e)
        {
            int prazodias = Convert.ToInt32(campoInteiroPRAZOENTREGA.Get());
            campoDataENTREGA.Set(Convert.ToDateTime(campoDataEMISSAO.Get()).AddDays(prazodias));
        }

        private void campoDataENTREGA_Leave(object sender, EventArgs e)
        {
            DateTime Emissao, Entrega;

            if (campoDataEMISSAO.Get() == null)
                return;
            else
                Emissao = (DateTime)campoDataEMISSAO.Get();


            if (campoDataENTREGA.Get() == null)
                return;
            else
                Entrega = (DateTime)campoDataENTREGA.Get();

            campoInteiroPRAZOENTREGA.Set((Entrega - Emissao).Days);
        }

        private void campoDataEMISSAO_Leave(object sender, EventArgs e)
        {
            DateTime Emissao, Entrega;

            if (campoDataEMISSAO.Get() == null)
                return;
            else
                Emissao = (DateTime)campoDataEMISSAO.Get();


            if (campoDataENTREGA.Get() == null)
                return;
            else
                Entrega = (DateTime)campoDataENTREGA.Get();

            campoInteiroPRAZOENTREGA.Set((Entrega - Emissao).Days);
        }

        private bool FormOrdemProducaoCadastro_ValidarSalvar(object sender, EventArgs e)
        {
            if (campoDataEMISSAO.Get() == null)
            {
                MessageBox.Show("Data de Emissão obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoDataENTREGA.Get() == null)
            {
                MessageBox.Show("Data de Entrega obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoInteiroPRAZOENTREGA.Get() == null)
            {
                MessageBox.Show("Prazo de Entrega obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoInteiroPRAZOENTREGA.Get() < 0)
            {
                MessageBox.Show("Data de Entrega deve ser maior ou igual a Data de Emissão.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (row["STATUS"].ToString() == "FATURADO")
            {
                MessageBox.Show("Não é permitido editar ordem de produção 'faturada'.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //this.BotaoSalvar = false;
                //this.BotaoOK = false;
                return false;
            }

            return true;
        }

        private void campoLookupCODCFO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM FCFO WHERE CODCOLIGADA = ? AND CODCFO = ?";
            campoLookupCODCFO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCFO.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookupCODCFO.Get() }).ToString();

        }

        private void campoLookupCODCPG_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TCPG WHERE CODCOLIGADA = ? AND CODCPG = ?";
            campoLookupCODCPG.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCPG.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookupCODCPG.Get() }).ToString();
        }

        private void gridParcelas_SetParametros(object sender, EventArgs e)
        {
            gridParcelas.Parametros = new object[] { campoInteiro2.Get(), campoInteiro1.Get() };
        }       
    }
}
