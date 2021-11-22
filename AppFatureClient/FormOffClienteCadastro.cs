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
    public partial class FormOffClienteCadastro : AppLib.Windows.FormCadastroData
    {
        public FormOffClienteCadastro()
        {
            InitializeComponent();
        }

        private bool campoLookup1TIPORUA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM ZDTIPORUA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1TIPORUA, consulta1, new Object[] { });
        }

        private bool campoLookup1TIPOBAIRRO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM ZDTIPOBAIRRO";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1TIPOBAIRRO, consulta1, new Object[] { });
        }

        private bool campoLookup1IDPAIS_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT IDPAIS, DESCRICAO FROM ZGPAIS";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1IDPAIS, consulta1, new Object[] { });
        }

        private bool campoLookup1CODETD_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODETD, NOME FROM ZGETD";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODETD, consulta1, new Object[] { });
        }

        private bool campoLookup1CODMUNICIPIO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODMUNICIPIO, NOMEMUNICIPIO, CODETDMUNICIPIO FROM ZGMUNICIPIO WHERE CODETDMUNICIPIO = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODMUNICIPIO, consulta1, new Object[] { campoLookup1CODETD.Get() });
        }

        private bool campoLookup5TIPORUAPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM ZDTIPORUA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup5TIPORUAPGTO, consulta1, new Object[] { });
        }

        private bool campoLookup4TIPOBAIRROPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM ZDTIPOBAIRRO";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup4TIPOBAIRROPGTO, consulta1, new Object[] { });
        }

        private bool campoLookup3IDPAISPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT IDPAIS, DESCRICAO FROM ZGPAIS";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup3IDPAISPGTO, consulta1, new Object[] { });
        }

        private bool campoLookup2CODETDPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODETD, NOME FROM ZGETD";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup2CODETDPGTO, consulta1, new Object[] { });
        }

        private bool campoLookup1CODMUNICIPIOPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODMUNICIPIO, NOMEMUNICIPIO, CODETDMUNICIPIO FROM ZGMUNICIPIO WHERE CODETDMUNICIPIO = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODMUNICIPIOPGTO, consulta1, new Object[] { campoLookup2CODETDPGTO.Get() });
        }

        private bool campoLookup5TIPORUAENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM ZDTIPORUA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup5TIPORUAENTREGA, consulta1, new Object[] { });
        }

        private bool campoLookup4TIPOBAIRROENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM ZDTIPOBAIRRO";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup4TIPOBAIRROENTREGA, consulta1, new Object[] { });
        }

        private bool campoLookup3IDPAISENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT IDPAIS, DESCRICAO FROM ZGPAIS";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup3IDPAISENTREGA, consulta1, new Object[] { });
        }

        private bool campoLookup2CODETDENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODETD, NOME FROM ZGETD";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup2CODETDENTREGA, consulta1, new Object[] { });
        }

        private bool campoLookup1CODMUNICIPIOENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODMUNICIPIO, NOMEMUNICIPIO, CODETDMUNICIPIO FROM ZGMUNICIPIO WHERE CODETDMUNICIPIO = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODMUNICIPIOENTREGA, consulta1, new Object[] { campoLookup2CODETDENTREGA.Get() });
        }

        private bool FormOffClienteCadastro_Validar(object sender, EventArgs e)
        {
            //Valida oValida = new Valida();
            //return oValida.ValidaCNPJCPF(campoTexto1CGCCFO.Get());

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            campoTexto4RUAPGTO.textEdit1.Text = campoTexto1RUA.Get();
            campoTexto3NUMEROPGTO.textEdit1.Text = campoTexto1NUMERO.Get();
            campoTexto2COMPLEMENTOPGTO.textEdit1.Text = campoTexto1COMPLEMENTO.Get();
            campoTexto1BAIRROPGTO.textEdit1.Text = campoTexto1BAIRRO.Get();
            campoLookup2CODETDPGTO.textBoxCODIGO.Text = campoLookup1CODETD.Get();
            campoLookup2CODETDPGTO.textBox1_Leave(this, null);
            campoTexto5CEPPGTO.textEdit1.Text = campoTexto1CEP.Get();
            campoLookup1CODMUNICIPIOPGTO.textBoxCODIGO.Text = campoLookup1CODMUNICIPIO.Get();
            campoLookup1CODMUNICIPIOPGTO.textBox1_Leave(this, null);
            campoLookup3IDPAISPGTO.textBoxCODIGO.Text = campoLookup1IDPAIS.Get();
            campoLookup3IDPAISPGTO.textBox1_Leave(this, null);
            campoLookup5TIPORUAPGTO.textBoxCODIGO.Text = campoLookup1TIPORUA.Get();
            campoLookup5TIPORUAPGTO.textBox1_Leave(this, null);
            campoLookup4TIPOBAIRROPGTO.textBoxCODIGO.Text = campoLookup1TIPOBAIRRO.Get();
            campoLookup4TIPOBAIRROPGTO.textBox1_Leave(this, null);

            campoTexto3TELEFONEPGTO.textEdit1.Text = campoTextoTELEFONE.Get();
            campoTexto1FAXPGTO.textEdit1.Text = campoTexto1FAX.Get();
            campoTexto2CONTATOPGTO.textEdit1.Text = campoTextoCONTATO.Get();
            campoTexto4EMAILPGTO.textEdit1.Text = campoTexto1EMAIL.Get();

            campoTexto4RUAENTREGA.textEdit1.Text = campoTexto1RUA.Get();
            campoTexto3NUMEROENTREGA.textEdit1.Text = campoTexto1NUMERO.Get();
            campoTexto2COMPLEMENTREGA.textEdit1.Text = campoTexto1COMPLEMENTO.Get();
            campoTexto1BAIRROENTREGA.textEdit1.Text = campoTexto1BAIRRO.Get();
            campoLookup2CODETDENTREGA.textBoxCODIGO.Text = campoLookup1CODETD.Get();
            campoLookup2CODETDENTREGA.textBox1_Leave(this, null);
            campoTexto5CEPENTREGA.textEdit1.Text = campoTexto1CEP.Get();
            campoLookup1CODMUNICIPIOENTREGA.textBoxCODIGO.Text = campoLookup1CODMUNICIPIO.Get();
            campoLookup1CODMUNICIPIOENTREGA.textBox1_Leave(this, null);
            campoLookup3IDPAISENTREGA.textBoxCODIGO.Text = campoLookup1IDPAIS.Get();
            campoLookup3IDPAISENTREGA.textBox1_Leave(this, null);
            campoLookup5TIPORUAENTREGA.textBoxCODIGO.Text = campoLookup1TIPORUA.Get();
            campoLookup5TIPORUAENTREGA.textBox1_Leave(this, null);
            campoLookup4TIPOBAIRROENTREGA.textBoxCODIGO.Text = campoLookup1TIPOBAIRRO.Get();
            campoLookup4TIPOBAIRROENTREGA.textBox1_Leave(this, null);

            campoTexto3TELEFONEENTREGA.textEdit1.Text = campoTextoTELEFONE.Get();
            campoTexto1FAXENTREGA.textEdit1.Text = campoTexto1FAX.Get();
            campoTexto2CONTATOENTREGA.textEdit1.Text = campoTextoCONTATO.Get();
            campoTexto4EMAILENTREGA.textEdit1.Text = campoTexto1EMAIL.Get();
        }

       

        private void campoLookup1CODMUNICIPIO_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM ZGMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            campoLookup1CODMUNICIPIO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, Consulta, new Object[] { campoLookup1CODMUNICIPIO.textBoxCODIGO.Text, campoLookup1CODETD.Get() }).ToString();
        }


        private void campoLookup1CODMUNICIPIOPGTO_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM ZGMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            campoLookup1CODMUNICIPIOPGTO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, Consulta, new Object[] { campoLookup1CODMUNICIPIOPGTO.textBoxCODIGO.Text, campoLookup2CODETDPGTO.Get() }).ToString();
        }

        

        

        private void campoLookup1CODMUNICIPIOENTREGA_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM ZGMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            campoLookup1CODMUNICIPIOENTREGA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, Consulta, new Object[] { campoLookup1CODMUNICIPIOENTREGA.textBoxCODIGO.Text, campoLookup2CODETDENTREGA.Get() }).ToString();
        }

        private void campoTexto1CGCCFO_Leave(object sender, EventArgs e)
        {
            Valida oValida = new Valida();
            if (!oValida.ValidaCNPJCPF(campoTexto1CGCCFO.Get()))
            {
                campoTexto1CGCCFO.Focus();
                MessageBox.Show("CNPJ/CPF informado não é válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormOffClienteCadastro_AntesSalvar(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(campoTexto1CODCFO.Get()))
                campoInteiroCODCOLIGADA.Set(AppLib.Context.Empresa);
        }

        private void campoLista1PESSOAFISOUJUR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (campoLista1PESSOAFISOUJUR.comboBox1.SelectedValue != null)
            {
                if (campoLista1PESSOAFISOUJUR.comboBox1.SelectedValue.ToString() == "F")
                    campoTexto1CGCCFO.textEdit1.Properties.Mask.EditMask = "000.000.000-00";
                else
                    if (campoLista1PESSOAFISOUJUR.comboBox1.SelectedValue.ToString() == "J")
                        campoTexto1CGCCFO.textEdit1.Properties.Mask.EditMask = "00.000.000/0000-00";
                    else
                        campoTexto1CGCCFO.textEdit1.Properties.Mask.EditMask = "000.000.000-00";
            }
        }
    }
}
