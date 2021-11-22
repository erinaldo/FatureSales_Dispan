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
    public partial class FormOffNovoClienteCadastro : AppLib.Windows.FormCadastroData
    {
        public FormOffNovoClienteCadastro()
        {
            InitializeComponent();

            campoTexto1CEP.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTexto1CEP.textEdit1.Properties.Mask.EditMask = "99999-999";

            campoTexto5CEPPGTO.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTexto5CEPPGTO.textEdit1.Properties.Mask.EditMask = "99999-999";

            campoTexto5CEPENTREGA.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTexto5CEPENTREGA.textEdit1.Properties.Mask.EditMask = "99999-999";
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
            Valida oValida = new Valida();
            //return oValida.ValidaCNPJCPF(campoTexto1CGCCFO.Get());

            if (string.IsNullOrEmpty(campoTexto1NOMEFANTASIA.Get()))    
            {
                MessageBox.Show("Campo Nome Fantasia obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(campoTexto1NOME.Get()))
            {
                MessageBox.Show("Campo Nome obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(
                
                
                
                
                
                
                campoListaRAMOATV.Get()))
            {
                MessageBox.Show("Campo Ramo de Atividade obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoLista1PESSOAFISOUJUR.Get() == "J")
            {
                if (campoTexto1CGCCFO.Get().Length != 18)
                {
                    MessageBox.Show("Para Pessoa Jurídica é necessário informar um CNPJ.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    if (campoTexto1CGCCFO.Get().Length == 18)
                    {
                        if (!oValida.ConsisteOffLineCNPJ(campoTexto1CGCCFO.Get(), campoTexto1.Get()))
                        {
                            if (string.IsNullOrEmpty(campoTexto1CGCCFO.Get()))
                                return true;
                            else
                            {
                                MessageBox.Show("CNPJ já informado em outro registro.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }                
                }
            }

            if (campoLista1PESSOAFISOUJUR.Get() == "F")
            {
                if (campoTexto1CGCCFO.Get().Length != 14)
                {
                    MessageBox.Show("Para Pessoa Física é necessário informar um CPF.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

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

        private void campoLista1PESSOAFISOUJUR_SelectedValueChanged(object sender, EventArgs e)
        {
            campoTexto1CGCCFO.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
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

        private void campoLookup1CODMUNICIPIO_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM ZGMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            campoLookup1CODMUNICIPIO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, Consulta, new Object[] { campoLookup1CODETD.Get(), campoLookup1CODMUNICIPIO.textBoxCODIGO.Text }).ToString();
        }

        private void campoLookup1CODMUNICIPIOPGTO_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM ZGMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            campoLookup1CODMUNICIPIOPGTO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, Consulta, new Object[] { campoLookup2CODETDPGTO.Get(), campoLookup1CODMUNICIPIOPGTO.textBoxCODIGO.Text }).ToString();
        }

        private void campoLookup1CODMUNICIPIOENTREGA_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM ZGMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            campoLookup1CODMUNICIPIOENTREGA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, Consulta, new Object[] { campoLookup2CODETDENTREGA.Get(), campoLookup1CODMUNICIPIOENTREGA.textBoxCODIGO.Text }).ToString();
        }

        private void campoTexto1CGCCFO_Leave(object sender, EventArgs e)
        {
            try
            {
                Valida oValida = new Valida();
                if (!oValida.ValidaCNPJCPF(campoTexto1CGCCFO.Get()))
                {
                    campoTexto1CGCCFO.Focus();
                    MessageBox.Show("CNPJ/CPF informado não é válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("CNPJ/CPF informado não é válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormOffClienteCadastro_AntesSalvar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(campoTexto1.Get()))
            {
                String Consulta = "SELECT RTRIM(REPLACE(CONVERT(CHAR,GETDATE(),11),'/','')) + RTRIM(REPLACE(CONVERT(CHAR,GETDATE(),14),':','')) CHAVE FROM ZGCOLIGADA WHERE CODCOLIGADA = ?";
                campoTexto1.Set(AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, Consulta, new Object[] { AppLib.Context.Empresa }).ToString());
                //campoInteiroCODCOLIGADA.Set(AppLib.Context.Empresa);
                campoInteiroCODCOLIGADA.Set(0);
            }
        }

        private void FormOffNovoClienteCadastro_AposSalvar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(campoTexto1.Get()))
            {

            }
            else
            { 
            
            }
        }

        private bool campoLookupCODRPR_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT CODRPR, ISNULL(NOMEFANTASIA, NOME) NOMEFANTASIA, SIGLA, CGC, CODETD, CIDADE, BAIRRO
FROM TRPR
WHERE CODCOLIGADA = ?
  AND INATIVO = 0";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODRPR, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private void FormOffNovoClienteCadastro_AposEditar(object sender, EventArgs e)
        {
            Valida v = new Valida();
            if (v.IsRepresentante())
            {
                /*
                string sSql = @"SELECT CODRPR FROM TRPR WHERE CODCOLIGADA = ? AND CODUSUARIO = ?";
                sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, AppLib.Context.Usuario });
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                if (dt.Rows.Count > 0)
                {
                    campoLookupCODRPR.textBoxCODIGO.Text = dt.Rows[0]["CODRPR"].ToString();
                    campoLookupCODRPR_SetDescricao(this, null);
                    campoLookupCODRPR.Enabled = false;
                }
                else
                {
                    campoLookupCODRPR.Enabled = true;
                }
                */

                campoLookupCODRPR.Enabled = false;
            }
            else
            {
                campoLookupCODRPR.Enabled = true;
            }
        }

        private void FormOffNovoClienteCadastro_AposNovo(object sender, EventArgs e)
        {
            Valida v = new Valida();
            if (v.IsRepresentante())
            {
                string sSql = @"SELECT CODRPR FROM TRPR WHERE CODCOLIGADA = ? AND CODUSUARIO = ?";
                sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, AppLib.Context.Usuario });
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                if (dt.Rows.Count > 0)
                {
                    campoLookupCODRPR.textBoxCODIGO.Text = dt.Rows[0]["CODRPR"].ToString();
                    //campoLookupCODRPR_SetDescricao(this, null);
                    campoLookupCODRPR.setDescricao();
                    campoLookupCODRPR.Enabled = false;
                }
                else
                {
                    campoLookupCODRPR.Enabled = true;
                }
            }
            else
            {
                campoLookupCODRPR.Enabled = true;
            }
        }

        private void campoLookupCODRPR_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM TRPR WHERE CODCOLIGADA = ? AND CODRPR = ? ";
            campoLookupCODRPR.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODRPR.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookupCODRPR.Get() }).ToString();
        }
    }
}
