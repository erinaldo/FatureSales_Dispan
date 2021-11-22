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
    public partial class FormLicenca : Form
    {
        public FormLicenca()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                wsContrato.ServiceSoapClient ws = new wsContrato.ServiceSoapClient();
                wsContrato.Contrato[] contratosws = ws.GetContract(textBox1.Text);

                List<Contrato> contratos = new List<Contrato>();
                String temp = "merbibuziberbibonzi";

                if (contratosws.Length > 0)
                {
                    for (int i = 0; i < contratosws.Length; i++)
                    {
                        Contrato contrato = new Contrato();
                        contrato.IDPRODUTO = new AppLib.Util.Criptografia().Encoder(AppLib.Util.Criptografia.OpcoesEncoder.TripleDES, contratosws[i].IDPRODUTO.ToString(), temp);
                        contrato.IDPROCESSO = new AppLib.Util.Criptografia().Encoder(AppLib.Util.Criptografia.OpcoesEncoder.TripleDES, contratosws[i].IDPROCESSO.ToString(), temp);
                        contrato.QUANTIDADE = new AppLib.Util.Criptografia().Encoder(AppLib.Util.Criptografia.OpcoesEncoder.TripleDES, contratosws[i].QUANTIDADE.ToString(), temp);
                        contrato.VALIDADE = new AppLib.Util.Criptografia().Encoder(AppLib.Util.Criptografia.OpcoesEncoder.TripleDES, contratosws[i].VALIDADE.ToString(), temp);
                        contrato.COMPLEMENTO = new AppLib.Util.Criptografia().Encoder(AppLib.Util.Criptografia.OpcoesEncoder.TripleDES, contratosws[i].COMPLEMENTO, temp);

                        contratos.Add(contrato);
                    }

                    Properties.Settings sett = new Properties.Settings();
                    AppLib.Util.ObjetoXML oxml = new AppLib.Util.ObjetoXML();
                    sett.Local = oxml.Escrever(contratos);
                    sett.Save();

                    MessageBox.Show("Contrato obtido com sucesso.");
                    this.Close();
                }
                else
                {
                    throw new Exception("invalid CNPJ error.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verique o CNPJ informado.\r\nMensagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, null);
            }
        }
    }
}
