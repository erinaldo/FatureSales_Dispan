using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TotvsSOA.SDK;
using TotvsSOA.SDK.SOAManager;

namespace AppFatureClient
{
    public partial class FormClienteCadastro : AppLib.Windows.FormCadastroObject
    {
        public FormClienteCadastro()
        {
            InitializeComponent();

            campoTexto1CEP.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTexto1CEP.textEdit1.Properties.Mask.EditMask = "99999-999";

            campoTexto5CEPPGTO.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTexto5CEPPGTO.textEdit1.Properties.Mask.EditMask = "99999-999";

            campoTexto5CEPENTREGA.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTexto5CEPENTREGA.textEdit1.Properties.Mask.EditMask = "99999-999";

            campoMemo1.richTextBox1.ReadOnly = true;
            campoMemo1.richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;
        }

        #region CAMPOS LOOKUP

        private bool campoLookup1TIPORUA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM DTIPORUA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1TIPORUA, consulta1, new Object[] { });
        }

        private bool campoLookup1TIPOBAIRRO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM DTIPOBAIRRO";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1TIPOBAIRRO, consulta1, new Object[] { });
        }

        private bool campoLookup1IDPAIS_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT IDPAIS, DESCRICAO FROM GPAIS";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1IDPAIS, consulta1, new Object[] { });
        }

        private bool campoLookup1CODETD_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODETD, NOME FROM GETD";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODETD, consulta1, new Object[] { });
        }

        private bool campoLookup1CODMUNICIPIO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODMUNICIPIO, NOMEMUNICIPIO, CODETDMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODMUNICIPIO, consulta1, new Object[] { campoLookup1CODETD.Get() });
        }

        private bool campoLookup5TIPORUAPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM DTIPORUA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup5TIPORUAPGTO, consulta1, new Object[] { });
        }

        private bool campoLookup4TIPOBAIRROPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM DTIPOBAIRRO";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup4TIPOBAIRROPGTO, consulta1, new Object[] { });
        }

        private bool campoLookup3IDPAISPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT IDPAIS, DESCRICAO FROM GPAIS";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup3IDPAISPGTO, consulta1, new Object[] { });
        }

        private bool campoLookup2CODETDPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODETD, NOME FROM GETD";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup2CODETDPGTO, consulta1, new Object[] { });
        }

        private bool campoLookup1CODMUNICIPIOPGTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODMUNICIPIO, NOMEMUNICIPIO, CODETDMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODMUNICIPIOPGTO, consulta1, new Object[] { campoLookup2CODETDPGTO.Get() });
        }

        private bool campoLookup5TIPORUAENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM DTIPORUA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup5TIPORUAENTREGA, consulta1, new Object[] { });
        }

        private bool campoLookup4TIPOBAIRROENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODIGO, DESCRICAO FROM DTIPOBAIRRO";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup4TIPOBAIRROENTREGA, consulta1, new Object[] { });
        }

        private bool campoLookup3IDPAISENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT IDPAIS, DESCRICAO FROM GPAIS";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup3IDPAISENTREGA, consulta1, new Object[] { });
        }

        private bool campoLookup2CODETDENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODETD, NOME FROM GETD";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup2CODETDENTREGA, consulta1, new Object[] { });
        }

        private bool campoLookup1CODMUNICIPIOENTREGA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODMUNICIPIO, NOMEMUNICIPIO, CODETDMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODMUNICIPIOENTREGA, consulta1, new Object[] { campoLookup2CODETDENTREGA.Get() });
        }

        private void campoLookup1CODMUNICIPIO_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            campoLookup1CODMUNICIPIO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, Consulta, new Object[] { campoLookup1CODETD.Get(), campoLookup1CODMUNICIPIO.textBoxCODIGO.Text }).ToString();
        }

        private void campoLookup1CODMUNICIPIOPGTO_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            campoLookup1CODMUNICIPIOPGTO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, Consulta, new Object[] { campoLookup2CODETDPGTO.Get(), campoLookup1CODMUNICIPIOPGTO.textBoxCODIGO.Text }).ToString();
        }

        private void campoLookup1CODMUNICIPIOENTREGA_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            campoLookup1CODMUNICIPIOENTREGA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, Consulta, new Object[] { campoLookup2CODETDENTREGA.Get(), campoLookup1CODMUNICIPIOENTREGA.textBoxCODIGO.Text }).ToString();
        }

        #endregion

        private bool FormClienteCadastro_Validar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(campoTexto1NOMEFANTASIA.Get()))
            {
                MessageBox.Show("Campo Nome Fantasia obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(campoTexto1EMAIL.Get()))
            {
                MessageBox.Show("Campo E-mail obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //Valida oValida = new Valida();
            //return oValida.ValidaCNPJCPF(campoTexto1CGCCFO.Get());
            try
            {
                Valida v = new Valida();
                if (v.IsRepresentante())
                {
                    if (campoLista1PESSOAFISOUJUR.Get() == "F")
                    {
                        if (string.IsNullOrEmpty(campoTexto1CODCFO.Get()))
                        {
                            if (!string.IsNullOrEmpty(campoTexto1CGCCFO.Get()))
                            {
                                string sSql = @"SELECT CGCCFO FROM FCFO WHERE CODCOLIGADA = ? AND CGCCFO = ?";
                                sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { 0, campoTexto1CGCCFO.Get() });
                                System.Data.DataTable dt2 = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                                if (dt2.Rows.Count > 0)
                                {
                                    MessageBox.Show("Cliente ja cadastrado, favor entrar em contato com o comercial.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Validação de Registro - " + ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (campoLista2.comboBox1.SelectedValue.ToString() == "1")
            {
                if (string.IsNullOrEmpty(campoTexto1INSCRESTADUAL.Get()))
                {
                    MessageBox.Show("Campo Insc. Estadual obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(campoLookup1IDPAIS.Get()))
            {
                if (campoLookup1IDPAIS.Get() != "1" && string.IsNullOrEmpty(campoTexto6.Get()))
                {
                    MessageBox.Show("Campo Documentos Estrangeiro obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(campoLista3.Get()))
            {
                if (campoLista3.Get() != "0" && string.IsNullOrEmpty(campoTexto6.Get()))
                {
                    MessageBox.Show("Campo Documentos Estrangeiro obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }

            return true;
        }

        private void FormClienteCadastro_Preparar(object sender, EventArgs e)
        {
            // implementar
        }

        private Boolean FormClienteCadastro_Salvar2(object sender, EventArgs e)
        {
            string sPoint = string.Empty;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                AppInterop.FinCliForPar cliente = new AppInterop.FinCliForPar();

                cliente.CodColigada = 0;
                sPoint = "Camada: [Client], Campo: [Código do Cliente]";
                cliente.CodCfo = campoTexto1CODCFO.Get();
                sPoint = "Camada: [Client], Campo: [Nome Fantasia]";
                cliente.NomeFantasia = campoTexto1NOMEFANTASIA.Get();
                sPoint = "Camada: [Client], Campo: [Nome]";
                cliente.Nome = campoTexto1NOME.Get();
                sPoint = "Camada: [Client], Campo: [CNPJ/CPF]";
                cliente.CGCCFO = campoTexto1CGCCFO.Get();
                sPoint = "Camada: [Client], Campo: [Inscrição Estadual]";
                cliente.InscrEstadual = campoTexto1INSCRESTADUAL.Get();
                sPoint = "Camada: [Client], Campo: [Inscrição Municipal]";
                cliente.InscrMunicipal = campoTexto1INSCRMUNICIPAL.Get();
                sPoint = "Camada: [Client], Campo: [Classificação]";
                cliente.PagRec = int.Parse(campoLista1PAGREC.Get());
                sPoint = "Camada: [Client], Campo: [Categoria]";
                cliente.PessoaFisOuJur = campoLista1PESSOAFISOUJUR.Get();

                sPoint = "Camada: [Client], Campo: [Rua]";
                cliente.Rua = campoTexto1RUA.Get();
                sPoint = "Camada: [Client], Campo: [Número]";
                cliente.Numero = campoTexto1NUMERO.Get();
                sPoint = "Camada: [Client], Campo: [Complemento]";
                cliente.Complemento = campoTexto1COMPLEMENTO.Get();
                sPoint = "Camada: [Client], Campo: [Bairro]";
                cliente.Bairro = campoTexto1BAIRRO.Get();
                sPoint = "Camada: [Client], Campo: [Estado]";
                cliente.CODETD = campoLookup1CODETD.Get();
                sPoint = "Camada: [Client], Campo: [CEP]";
                cliente.CEP = campoTexto1CEP.Get();
                sPoint = "Camada: [Client], Campo: [Telefone]";
                cliente.Telefone = campoTextoTELEFONE.Get();
                sPoint = "Camada: [Client], Campo: [Celular]";
                cliente.Telex = campoTextoTELEX.Get();
                sPoint = "Camada: [Client], Campo: [FAX]";
                cliente.FAX = campoTexto1FAX.Get();
                sPoint = "Camada: [Client], Campo: [Contato]";
                cliente.Contato = campoTextoCONTATO.Get();
                sPoint = "Camada: [Client], Campo: [Email]";
                cliente.Email = campoTexto1EMAIL.Get();

                sPoint = "Camada: [Client], Campo: [Rua Pagamento]";
                cliente.RuaPgto = campoTexto4RUAPGTO.Get();
                sPoint = "Camada: [Client], Campo: [Número Pagamento]";
                cliente.NumeroPgto = campoTexto3NUMEROPGTO.Get();
                sPoint = "Camada: [Client], Campo: [Complemento Pagamento]";
                cliente.ComplementoPgto = campoTexto2COMPLEMENTOPGTO.Get();
                sPoint = "Camada: [Client], Campo: [Bairro Pagamento]";
                cliente.BairroPgto = campoTexto1BAIRROPGTO.Get();
                sPoint = "Camada: [Client], Campo: [Estado Pagamento]";
                cliente.CODETDPgto = campoLookup2CODETDPGTO.Get();
                sPoint = "Camada: [Client], Campo: [CEP Pagamento]";
                cliente.CEPPgto = campoTexto5CEPPGTO.Get();
                sPoint = "Camada: [Client], Campo: [Telefone Pagamento]";
                cliente.TelefonePgto = campoTexto3TELEFONEPGTO.Get();
                sPoint = "Camada: [Client], Campo: [FAX Pagamento]";
                cliente.FAXPgto = campoTexto1FAXPGTO.Get();
                sPoint = "Camada: [Client], Campo: [Contato Pagamento]";
                cliente.ContatoPgto = campoTexto2CONTATOPGTO.Get();
                sPoint = "Camada: [Client], Campo: [Email Pagamento]";
                cliente.EmailPgto = campoTexto4EMAILPGTO.Get();

                sPoint = "Camada: [Client], Campo: [Rua Entrega]";
                cliente.RuaEntrega = campoTexto4RUAENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Número Entrega]";
                cliente.NumeroEntrega = campoTexto3NUMEROENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Complemento Entrega]";
                cliente.ComplemEntrega = campoTexto2COMPLEMENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Bairro Entrega]";
                cliente.BairroEntrega = campoTexto1BAIRROENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Estado Entrega]";
                cliente.CODETDEntrega = campoLookup2CODETDENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [CEP Entrega]";
                cliente.CEPEntrega = campoTexto5CEPENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Telefone Entrega]";
                cliente.TelefoneEntrega = campoTexto3TELEFONEENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [FAX Entrega]";
                cliente.FAXEntrega = campoTexto1FAXENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Contato Entrega]";
                cliente.ContatoEntrega = campoTexto2CONTATOENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Email Entrega]";
                cliente.EmailEntrega = campoTexto4EMAILENTREGA.Get();

                sPoint = "Camada: [Client], Campo: [Código do Municipio]";
                cliente.CodMunicipio = campoLookup1CODMUNICIPIO.Get();
                sPoint = "Camada: [Client], Campo: [Código do Municipio Pagamento]";
                cliente.CodMunicipioPgto = campoLookup1CODMUNICIPIOPGTO.Get();
                sPoint = "Camada: [Client], Campo: [Código do Municipio Entrega]";
                cliente.CodMunicipioEntrega = campoLookup1CODMUNICIPIOENTREGA.Get();

                if (!string.IsNullOrEmpty(campoLookup1CODMUNICIPIO.textBoxDESCRICAO.Text))
                {
                    sPoint = "Camada: [Client], Campo: [Municipio]";
                    cliente.Cidade = campoLookup1CODMUNICIPIO.textBoxDESCRICAO.Text;
                }
                else
                {
                    sPoint = "Camada: [Client], Campo: [Municipio]";
                    cliente.Cidade = txtCidade.Get();

                }
                sPoint = "Camada: [Client], Campo: [Municipio Pagamento]";
                cliente.CidadePgto = campoLookup1CODMUNICIPIOPGTO.textBoxDESCRICAO.Text;
                sPoint = "Camada: [Client], Campo: [Municipio Entrega]";
                cliente.CidadeEntrega = campoLookup1CODMUNICIPIOENTREGA.textBoxDESCRICAO.Text;

                sPoint = "Camada: [Client], Campo: [Pais]";
                cliente.IDPais = (campoLookup1IDPAIS.Get() == null) ? null : (int?)int.Parse(campoLookup1IDPAIS.Get());
                sPoint = "Camada: [Client], Campo: [Pais Pagamento]";
                cliente.IDPaisPgto = (campoLookup3IDPAISPGTO.Get() == null) ? null : (int?)int.Parse(campoLookup3IDPAISPGTO.Get());
                sPoint = "Camada: [Client], Campo: [Pais Entrega]";
                cliente.IDPaisEntrega = (campoLookup3IDPAISENTREGA.Get() == null) ? null : (int?)int.Parse(campoLookup3IDPAISENTREGA.Get());

                sPoint = "Camada: [Client], Campo: [Tipo Rua]";
                cliente.TipoRua = (campoLookup1TIPORUA.Get() == null) ? null : (int?)int.Parse(campoLookup1TIPORUA.Get());
                sPoint = "Camada: [Client], Campo: [Tipo Bairro]";
                cliente.TipoBairro = (campoLookup1TIPOBAIRRO.Get() == null) ? null : (int?)int.Parse(campoLookup1TIPOBAIRRO.Get());
                sPoint = "Camada: [Client], Campo: [Tipo Rua Pagamento]";
                cliente.TipoRuaPgto = (campoLookup5TIPORUAPGTO.Get() == null) ? null : (int?)int.Parse(campoLookup5TIPORUAPGTO.Get());
                sPoint = "Camada: [Client], Campo: [Tipo Bairro Pagamento]";
                cliente.TipoBairroPgto = (campoLookup4TIPOBAIRROPGTO.Get() == null) ? null : (int?)int.Parse(campoLookup4TIPOBAIRROPGTO.Get());
                sPoint = "Camada: [Client], Campo: [Tipo Rua Entrega]";
                cliente.TipoRuaEntrega = (campoLookup5TIPORUAENTREGA.Get() == null) ? null : (int?)int.Parse(campoLookup5TIPORUAENTREGA.Get());
                sPoint = "Camada: [Client], Campo: [Tipo Bairro Entrega]";
                cliente.TipoBairroEntrega = (campoLookup4TIPOBAIRROENTREGA.Get() == null) ? null : (int?)int.Parse(campoLookup4TIPOBAIRROENTREGA.Get());

                sPoint = "Camada: [Client], Campo: [Ramo de Atividade]";
                try
                {
                    cliente.RamoAtiv = (campoListaRAMOATV.Get() == null) ? 0 : (int?)int.Parse(campoListaRAMOATV.Get());
                }
                catch { cliente.RamoAtiv = 1; }

                sPoint = "Camada: [Client], Campo: [Limite de Crédito]";
                cliente.LimiteCredito = campoDecimalLIMITECREDITO.Get();
                sPoint = "Camada: [Client], Campo: [SUFRAMA]";
                cliente.Suframa = campoTexto1.Get();
                sPoint = "Camada: [Client], Campo: [Ativo]";
                cliente.Ativo = int.Parse(campoLista1.Get());
                sPoint = "Camada: [Client], Campo: [Usuário de Criação]";
                cliente.UsuarioCriacao = AppLib.Context.Usuario;
                sPoint = "Camada: [Client], Campo: [Usuário de Alteração]";
                cliente.UsuarioAlteracao = AppLib.Context.Usuario;
                sPoint = "Camada: [Client], Campo: [Histórico Atual]";
                cliente.HISTORICOATUAL = campoMemoMSGFORNEC.Get();
                sPoint = "Camada: [Client], Campo: [Representante]";
                cliente.CodRpr = campoLookupCODRPR.Get();
                sPoint = "Camada: [Client], Campo: [Contribuinte ICMS]";
                cliente.ContribuinteICMS = campoLista2.Get();
                sPoint = "Camada: [Client], Campo: [Nacionalidade]";
                cliente.Nacionalidade = campoLista3.Get();
                sPoint = "Camada: [Client], Campo: [Documentos Estrangeiro]";
                cliente.DocumentosEstrangeiro = campoTexto6.Get();

                AppInterop.Message mensagem;
                if (FatureContexto.Remoto)
                {
                    mensagem = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.ClienteFornecedorSave(AppLib.Context.Usuario, AppLib.Context.Senha, new Util().ConvertToWSFinCliForPar(cliente)));
                }
                else
                {
                    mensagem = FatureContexto.ServiceClient.ClienteFornecedorSave(AppLib.Context.Usuario, AppLib.Context.Senha, cliente);
                }

                if (mensagem.Retorno.ToString() != "0")
                {
                    sPoint = "Camada: [Client], Rotina: Recupera Código do Cliente]";
                    campoTexto1CODCFO.Set(mensagem.Retorno.ToString());

                    this.Cursor = Cursors.Default;
                    return true;
                }
                else
                {
                    sPoint = "Camada: [Server]";
                    throw new Exception(mensagem.Mensagem);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(string.Concat(sPoint, " - ", ex.Message), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void FormClienteCadastro_Load(object sender, EventArgs e)
        {
            GetProcessos().Add("Receita Federal - Consulta de CPF e CNPJ", null, ReceitaFederalConsultaCPFCNPJ);
            GetProcessos().Add("Importar Dados – Consulta de CPF e CNPJ (XML)", null, CarregaDadosCadastraisReceitaFederal);
            GetProcessos().Add("Sintegra - Consulta Nacional ao Cadastro", null, SintegraConsultaNacionalCadastro);
            GetProcessos().Add("Importar Dados – Consulta Nacional ao Cadastro (XML)", null, CarregaDadosCadastraisSintegra);

            campoTexto1EMAIL.textEdit1.ToolTip = "Caso o cliente não possuir email, favor informar o email do representante.";
        }

        public void CarregaDadosCadastraisReceitaFederal(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(this.Conexao, "APP8002", AppLib.Context.Perfil))
            {
                string dir = Convert.ToString(AppDomain.CurrentDomain.BaseDirectory);
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = dir;
                openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                        try
                        {
                            List<TotvsMashup> maschups = new List<TotvsMashup>();
                            xmlDoc.Load(openFileDialog1.FileName);
                            System.Xml.XmlNodeList item = xmlDoc.GetElementsByTagName("TotvsMashup");
                            if (item.Count > 0)
                            {
                                for (int i = 0; i < item.Count; i++)
                                {
                                    TotvsMashup maschup = new TotvsMashup();
                                    System.Xml.XmlNodeList filhos = item[i].ChildNodes;
                                    for (int j = 0; j < filhos.Count; j++)
                                    {
                                        if (filhos[j].Name == "Name")
                                        {
                                            maschup.Name = filhos[j].InnerText;
                                        }
                                        if (filhos[j].Name == "Type")
                                        {
                                            maschup.Type = filhos[j].InnerText;
                                        }
                                        if (filhos[j].Name == "Value")
                                        {
                                            maschup.Value = filhos[j].InnerText;
                                            maschups.Add(maschup);
                                        }
                                    }
                                }
                            }

                            if (maschups.Count > 0)
                            {
                                for (int i = 0; i < maschups.Count; i++)
                                {
                                    if (maschups[i].Name == "NumDoc")
                                        campoTexto1CGCCFO.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Nome")
                                        campoTexto1NOME.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Endereço")
                                        campoTexto1RUA.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Número")
                                        campoTexto1NUMERO.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Complemento")
                                        campoTexto1COMPLEMENTO.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Bairro")
                                        campoTexto1BAIRRO.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Estado")
                                    {
                                        campoLookup1CODETD.textBoxCODIGO.Text = maschups[i].Value;
                                        campoLookup1CODETD.textBox1_Leave(this, null);
                                    }
                                    if (maschups[i].Name == "Cidade")
                                    {
                                        String Consulta = "SELECT CODMUNICIPIO FROM GMUNICIPIO WHERE NOMEMUNICIPIO = ?";
                                        campoLookup1CODMUNICIPIO.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, Consulta, new Object[] { maschups[i].Value }).ToString();
                                        campoLookup1CODMUNICIPIO.textBox1_Leave(this, null);
                                    }

                                    if (maschups[i].Name == "CEP")
                                        campoTexto1CEP.Set(maschups[i].Value);
                                }
                            }
                            else
                            {
                                throw new Exception("Erro ao carregar arquivo XML. Arquivo em formato inválido.");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Concat("Erro ao carregar arquivo XML.", (ex.InnerException != null ? ex.InnerException.Message : ex.Message)));
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public void CarregaDadosCadastraisSintegra(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(this.Conexao, "APP8003", AppLib.Context.Perfil))
            {
                string dir = Convert.ToString(AppDomain.CurrentDomain.BaseDirectory);
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = dir;
                openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                        try
                        {
                            List<TotvsMashup> maschups = new List<TotvsMashup>();
                            xmlDoc.Load(openFileDialog1.FileName);
                            System.Xml.XmlNodeList item = xmlDoc.GetElementsByTagName("TotvsMashup");
                            if (item.Count > 0)
                            {
                                for (int i = 0; i < item.Count; i++)
                                {
                                    TotvsMashup maschup = new TotvsMashup();
                                    System.Xml.XmlNodeList filhos = item[i].ChildNodes;
                                    for (int j = 0; j < filhos.Count; j++)
                                    {
                                        if (filhos[j].Name == "Name")
                                        {
                                            maschup.Name = filhos[j].InnerText;
                                        }
                                        if (filhos[j].Name == "Type")
                                        {
                                            maschup.Type = filhos[j].InnerText;
                                        }
                                        if (filhos[j].Name == "Value")
                                        {
                                            maschup.Value = filhos[j].InnerText;
                                            maschups.Add(maschup);
                                        }
                                    }
                                }
                            }

                            if (maschups.Count > 0)
                            {
                                for (int i = 0; i < maschups.Count; i++)
                                {
                                    if (maschups[i].Name == "CNPJ")
                                        campoTexto1CGCCFO.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Inscrição")
                                        campoTexto1INSCRESTADUAL.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Razão")
                                        campoTexto1NOME.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Logradouro")
                                        campoTexto1RUA.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Número")
                                        campoTexto1NUMERO.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Complemento")
                                        campoTexto1COMPLEMENTO.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Bairro")
                                        campoTexto1BAIRRO.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Estado")
                                    {
                                        campoLookup1CODETD.textBoxCODIGO.Text = maschups[i].Value;
                                        campoLookup1CODETD.textBox1_Leave(this, null);
                                    }
                                    if (maschups[i].Name == "Cidade")
                                    {
                                        String Consulta = "SELECT CODMUNICIPIO FROM GMUNICIPIO WHERE NOMEMUNICIPIO = ?";
                                        campoLookup1CODMUNICIPIO.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, Consulta, new Object[] { maschups[i].Value }).ToString();
                                        campoLookup1CODMUNICIPIO.textBox1_Leave(this, null);
                                    }

                                    if (maschups[i].Name == "CEP")
                                        campoTexto1CEP.Set(maschups[i].Value);
                                    if (maschups[i].Name == "Telefone")
                                        campoTextoTELEFONE.Set(maschups[i].Value);
                                    if (maschups[i].Name == "E-Mail")
                                        campoTexto1EMAIL.Set(maschups[i].Value);
                                }
                            }
                            else
                            {
                                throw new Exception("Erro ao carregar arquivo XML. Arquivo em formato inválido.");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Concat("Erro ao carregar arquivo XML.", (ex.InnerException != null ? ex.InnerException.Message : ex.Message)));
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public void ReceitaFederalConsultaCPFCNPJ(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(this.Conexao, "APP8004", AppLib.Context.Perfil))
            {
                try
                {
                    SOAServiceInfo ServiceInfo = (SOAServiceInfo)null;
                    SOAWinClient client = (SOAWinClient)null;
                    client = new SOAWinClient();
                    client.ShowExport = true;
                    client.ServerAddress = "mashups.totvs.com.br";
                    client.UserName = "itinit";
                    client.Password = "itinit";

                    client.ServiceType = "SOAMashupStudioService";
                    foreach (SOAServiceInfo soaServiceInfo in client.GetServicesInfo(SOASearchType.ServiceType, client.ServiceType))
                    {
                        if (soaServiceInfo.ServiceName == "ReceitaFederal.CPF_CNPJ")
                        {
                            ServiceInfo = soaServiceInfo;
                        }
                    }

                    if (ServiceInfo != null)
                    {
                        client.ServiceType = ServiceInfo.ServiceType;
                        client.ServiceName = ServiceInfo.ServiceName;
                        client.ServiceVersion = 0;
                        client.Initialize(true);
                        client.Execute();
                    }
                    else
                    {
                        throw new Exception("Serviço não encontrado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void SintegraConsultaNacionalCadastro(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(this.Conexao, "APP8005", AppLib.Context.Perfil))
            {
                try
                {
                    SOAServiceInfo ServiceInfo = (SOAServiceInfo)null;
                    SOAWinClient client = (SOAWinClient)null;
                    client = new SOAWinClient();
                    client.ShowExport = true;
                    client.ServerAddress = "mashups.totvs.com.br";
                    client.UserName = "itinit";
                    client.Password = "itinit";

                    client.ServiceType = "SOAMashupStudioService";
                    foreach (SOAServiceInfo soaServiceInfo in client.GetServicesInfo(SOASearchType.ServiceType, client.ServiceType))
                    {
                        if (soaServiceInfo.ServiceName == "Sintegra.ConsultaNacional")
                        {
                            ServiceInfo = soaServiceInfo;
                        }
                    }

                    if (ServiceInfo != null)
                    {
                        client.ServiceType = ServiceInfo.ServiceType;
                        client.ServiceName = ServiceInfo.ServiceName;
                        client.ServiceVersion = 0;
                        client.Initialize(true);
                        client.Execute();
                    }
                    else
                    {
                        throw new Exception("Serviço não encontrado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        private void FormClienteCadastro_Excluir2(object sender, EventArgs e)
        {
            MessageBox.Show("Operação não permitida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void campoLista1PESSOAFISOUJUR_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void campoTexto1CGCCFO_Leave(object sender, EventArgs e)
        {
            try
            {
                Valida oValida = new Valida();
                string cgccfo = campoTexto1CGCCFO.Get();
                if (!oValida.ValidaCNPJCPF(cgccfo))
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

        private void FormClienteCadastro_AposSalvar(object sender, EventArgs e)
        {
            campoMemoMSGFORNEC.Set(null);
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

        private void FormClienteCadastro_AposNovo(object sender, EventArgs e)
        {
            campoLista3.Set("0");
            //campoLista3_SelectedIndexChanged(this, null);
            campoLista3_AposSelecao(this, null);


            Valida v = new Valida();
            if (v.IsRepresentante())
            {
                string sSql = @"SELECT CODRPR FROM ZTRPR WHERE CODCOLIGADA = ? AND CODUSUARIO = ?";
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { AppLib.Context.Empresa, AppLib.Context.Usuario });

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

        private void FormClienteCadastro_AposEditar(object sender, EventArgs e)
        {
            //campoLista3_SelectedIndexChanged(this, null);
            campoLista3_AposSelecao(this, null);

            Valida v = new Valida();
            //if (v.IsRepresentante())
            //{
            //    /*
            //    string sSql = @"SELECT CODRPR FROM TRPR WHERE CODCOLIGADA = ? AND CODUSUARIO = ?";
            //    sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, AppLib.Context.Usuario });
            //    System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            //    if (dt.Rows.Count > 0)
            //    {
            //        campoLookupCODRPR.textBoxCODIGO.Text = dt.Rows[0]["CODRPR"].ToString();
            //        campoLookupCODRPR_SetDescricao(this, null);
            //        campoLookupCODRPR.Enabled = false;
            //    }
            //    else
            //    {
            //        campoLookupCODRPR.Enabled = true;
            //    }
            //    */

            //    campoLookupCODRPR.Enabled = false;
            //}
            //else
            //{
            //    campoLookupCODRPR.Enabled = true;
            //}
        }

        private void campoLista2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (campoLista2.comboBox1.SelectedValue.ToString() == "1")
            {
                campoTexto1INSCRESTADUAL.textEdit1.Enabled = true;
                campoTexto1INSCRESTADUAL.textEdit1.Text = string.Empty;
            }
            else
            {
                if (campoLista2.comboBox1.SelectedValue.ToString() == "2")
                {
                    campoTexto1INSCRESTADUAL.textEdit1.Enabled = false;
                    campoTexto1INSCRESTADUAL.textEdit1.Text = string.Empty;
                }
                else
                {
                    campoTexto1INSCRESTADUAL.textEdit1.Enabled = true;
                    campoTexto1INSCRESTADUAL.textEdit1.Text = string.Empty;
                }
            }
        }

        //private void campoLista3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (campoLista3.comboBox1.SelectedValue.ToString() == "0")
        //    {
        //        campoTexto6.textEdit1.Enabled = false;
        //        campoTexto6.textEdit1.Text = string.Empty;
        //    }
        //    else
        //    {
        //        campoTexto6.textEdit1.Enabled = true;
        //    }
        //}

        private void campoLookupCODRPR_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM TRPR WHERE CODCOLIGADA = ? AND CODRPR = ?";
            campoLookupCODRPR.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODRPR.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODRPR.Get() }).ToString();
        }

        private void btnConsultaReceita_Click(object sender, EventArgs e)
        {
            if (campoLista1PESSOAFISOUJUR.Get() == "F")
            {
                Consultas.frmReceitaFederal frmCPF = new Consultas.frmReceitaFederal();
                frmCPF.maskedTextBox1.Text = campoTexto1CGCCFO.textEdit1.Text;
                //frmCPF.maskedTextBox2.Text = psDateBox1.Text;
                frmCPF.ShowDialog();
            }
            else
            {
                Consultas.frmConsultaCNPJ frmCNPJ = new Consultas.frmConsultaCNPJ();
                frmCNPJ.maskedTextBox1.Text = campoTexto1CGCCFO.textEdit1.Text;
                frmCNPJ.ShowDialog();
                if (frmCNPJ.copiar == true)
                {
                    //Realiza a cópia dos dados do form
                    campoTexto1NOME.textEdit1.Text = frmCNPJ.txtRazaoSocial.Text;
                    campoTexto1NOMEFANTASIA.textEdit1.Text = frmCNPJ.txtNomeFantasia.Text;
                    campoTexto1CEP.textEdit1.Text = frmCNPJ.txtCep.Text.Replace(".", "");
                    campoTexto1RUA.textEdit1.Text = frmCNPJ.txtLogr.Text;
                    campoTexto1NUMERO.textEdit1.Text = frmCNPJ.txtNumero.Text;
                    campoTexto1BAIRRO.textEdit1.Text = frmCNPJ.txtBairro.Text;
                    campoLookup1CODETD.textBoxCODIGO.Text = frmCNPJ.txtUF.Text;
                    campoLookup1CODETD.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT NOME FROM GETD WHERE CODETD = ?", new object[] { frmCNPJ.txtUF.Text }).ToString();
                    campoLookup1CODMUNICIPIO.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT CODMUNICIPIO FROM GMUNICIPIO WHERE NOMEMUNICIPIO = ? AND CODETDMUNICIPIO = ?", new object[] { frmCNPJ.txtMunicipio.Text.Replace("'", " ").ToString(), frmCNPJ.txtUF.Text }).ToString();

                    campoLookup1CODMUNICIPIO.textBoxDESCRICAO.Text = frmCNPJ.txtMunicipio.Text.Replace("'", " ").ToString();
                    campoTextoTELEFONE.textEdit1.Text = frmCNPJ.txtTelefone.Text;
                }
            }
        }

        private void btnSiteSintegra_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(campoTexto1CGCCFO.textEdit1.Text.Replace(".", "").ToString().Replace("-", "").Replace("/", ""));

            Consultas.frmWebBrowserSintegra frm = new Consultas.frmWebBrowserSintegra();
            frm.ShowDialog();
        }

        private void btnSearchCep_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(campoTexto1CEP.textEdit1.Text))
            {
                //AppLib.Util.Correios.Endereco endereco = new AppLib.Util.Correios.Endereco();
                //endereco = AppLib.Util.Correios.BuscaCep.GetEndereco(campoTexto1CEP.textEdit1.Text, 10000);
                //campoTexto1RUA.textEdit1.Text = endereco.logradouro;
                //campoTexto1BAIRRO.textEdit1.Text = endereco.bairro;
                //campoLookup1CODETD.textBoxCODIGO.Text = endereco.estado;
                //campoLookup1CODETD.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT NOME FROM GETD WHERE CODETD = ?", new object[] { endereco.estado }).ToString();
                //campoLookup1CODMUNICIPIO.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT CODMUNICIPIO FROM GMUNICIPIO WHERE NOMEMUNICIPIO = ? AND CODETDMUNICIPIO = ?", new object[] { endereco.cidade.Replace("&#39;", " ").ToString(), endereco.estado }).ToString();
                //campoLookup1CODMUNICIPIO.textBoxDESCRICAO.Text = endereco.cidade.Replace("&#39;", " ").ToString();
                AppFatureClient.Classes.WebCEP web = new Classes.WebCEP(campoTexto1CEP.textEdit1.Text);
                campoTexto1RUA.textEdit1.Text = web.Lagradouro;
                campoLookup1TIPORUA.textBoxDESCRICAO.Text = web.TipoLagradouro;
                campoLookup1TIPORUA.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODIGO FROM DTIPORUA WHERE DESCRICAO = ?", new object[] { web.TipoLagradouro }).ToString();
                campoTexto1BAIRRO.textEdit1.Text = web.Bairro;
                campoLookup1CODETD.textBoxCODIGO.Text = web.UF;
                campoLookup1CODETD.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GETD WHERE CODETD = ?", new object[] { web.UF }).ToString();
                campoLookup1CODMUNICIPIO.textBoxDESCRICAO.Text = web.Cidade;
                campoLookup1CODMUNICIPIO.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODMUNICIPIO FROM GMUNICIPIO WHERE NOMEMUNICIPIO = ?", new object[] { web.Cidade }).ToString();
                campoLookup1IDPAIS.textBoxCODIGO.Text = "1";
                campoLookup1IDPAIS.textBoxDESCRICAO.Text = "Brasil";

            }
            else
            {
                MessageBox.Show("Favor preecher o CEP corretamente", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(campoTexto5CEPPGTO.textEdit1.Text))
            {
                //AppLib.Util.Correios.Endereco endereco = new AppLib.Util.Correios.Endereco();
                //endereco = AppLib.Util.Correios.BuscaCep.GetEndereco(campoTexto5CEPPGTO.textEdit1.Text, 10000);
                //campoTexto4RUAPGTO.textEdit1.Text = endereco.logradouro;
                //campoTexto1BAIRROPGTO.textEdit1.Text = endereco.bairro;
                //campoLookup2CODETDPGTO.textBoxCODIGO.Text = endereco.estado;
                //campoLookup2CODETDPGTO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT NOME FROM GETD WHERE CODETD = ?", new object[] { endereco.estado }).ToString();
                //campoLookup1CODMUNICIPIOPGTO.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT CODMUNICIPIO FROM GMUNICIPIO WHERE NOMEMUNICIPIO = ? AND CODETDMUNICIPIO = ?", new object[] { endereco.cidade.Replace("&#39;", " ").ToString(), endereco.estado }).ToString();
                //campoLookup1CODMUNICIPIOPGTO.textBoxDESCRICAO.Text = endereco.cidade.Replace("&#39;", " ").ToString();
                AppFatureClient.Classes.WebCEP web = new Classes.WebCEP(campoTexto5CEPPGTO.textEdit1.Text);
                campoTexto4RUAPGTO.textEdit1.Text = web.Lagradouro;
                campoLookup5TIPORUAPGTO.textBoxDESCRICAO.Text = web.TipoLagradouro;
                campoLookup5TIPORUAPGTO.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODIGO FROM DTIPORUA WHERE DESCRICAO = ?", new object[] { web.TipoLagradouro }).ToString();
                campoTexto1BAIRROPGTO.textEdit1.Text = web.Bairro;
                campoLookup2CODETDPGTO.textBoxCODIGO.Text = web.UF;
                campoLookup2CODETDPGTO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GETD WHERE CODETD = ?", new object[] { web.UF }).ToString();
                campoLookup1CODMUNICIPIOPGTO.textBoxDESCRICAO.Text = web.Cidade;
                campoLookup1CODMUNICIPIOPGTO.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODMUNICIPIO FROM GMUNICIPIO WHERE NOMEMUNICIPIO = ?", new object[] { web.Cidade }).ToString();
                campoLookup3IDPAISPGTO.textBoxCODIGO.Text = "1";
                campoLookup3IDPAISPGTO.textBoxDESCRICAO.Text = "Brasil";


            }
            else
            {
                MessageBox.Show("Favor preecher o CEP corretamente", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(campoTexto5CEPENTREGA.textEdit1.Text))
            {
                //AppLib.Util.Correios.Endereco endereco = new AppLib.Util.Correios.Endereco();
                //endereco = AppLib.Util.Correios.BuscaCep.GetEndereco(campoTexto5CEPPGTO.textEdit1.Text, 10000);
                //campoTexto4RUAENTREGA.textEdit1.Text = endereco.logradouro;
                //campoTexto1BAIRROENTREGA.textEdit1.Text = endereco.bairro;
                //campoLookup2CODETDENTREGA.textBoxCODIGO.Text = endereco.estado;
                //campoLookup2CODETDENTREGA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT NOME FROM GETD WHERE CODETD = ?", new object[] { endereco.estado }).ToString();
                //campoLookup1CODMUNICIPIOENTREGA.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT CODMUNICIPIO FROM GMUNICIPIO WHERE NOMEMUNICIPIO = ? AND CODETDMUNICIPIO = ?", new object[] { endereco.cidade.Replace("&#39;", " ").ToString(), endereco.estado }).ToString();
                //campoLookup1CODMUNICIPIOENTREGA.textBoxDESCRICAO.Text = endereco.cidade.Replace("&#39;", " ").ToString();
                AppFatureClient.Classes.WebCEP web = new Classes.WebCEP(campoTexto5CEPENTREGA.textEdit1.Text);
                campoTexto4RUAENTREGA.textEdit1.Text = web.Lagradouro;
                campoLookup5TIPORUAENTREGA.textBoxDESCRICAO.Text = web.TipoLagradouro;
                campoLookup5TIPORUAENTREGA.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODIGO FROM DTIPORUA WHERE DESCRICAO = ?", new object[] { web.TipoLagradouro }).ToString();
                campoTexto1BAIRROENTREGA.textEdit1.Text = web.Bairro;
                campoLookup2CODETDENTREGA.textBoxCODIGO.Text = web.UF;
                campoLookup2CODETDENTREGA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GETD WHERE CODETD = ?", new object[] { web.UF }).ToString();
                campoLookup1CODMUNICIPIOENTREGA.textBoxDESCRICAO.Text = web.Cidade;
                campoLookup1CODMUNICIPIOENTREGA.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODMUNICIPIO FROM GMUNICIPIO WHERE NOMEMUNICIPIO = ?", new object[] { web.Cidade }).ToString();
                campoLookup3IDPAISENTREGA.textBoxCODIGO.Text = "1";
                campoLookup3IDPAISENTREGA.textBoxDESCRICAO.Text = "Brasil";

            }
            else
            {
                MessageBox.Show("Favor preecher o CEP corretamente", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void campoLookup1IDPAIS_AposSelecao(object sender, EventArgs e)
        {
            if (campoLookup1IDPAIS.textBoxDESCRICAO.Text != "Brasil")
            {
                campoLookup1CODMUNICIPIO.Visible = false;
                txtCidade.Visible = true;
            }
            else
            {
                campoLookup1CODMUNICIPIO.Visible = true;
                txtCidade.Visible = false;
            }
        }

        private void campoLista1PESSOAFISOUJUR_AposSelecao(object sender, EventArgs e)
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

        private void campoLista3_AposSelecao(object sender, EventArgs e)
        {
            if (campoLista3.comboBox1.SelectedValue.ToString() == "0")
            {
                campoTexto6.textEdit1.Enabled = false;
                campoTexto6.textEdit1.Text = string.Empty;
            }
            else
            {
                campoTexto6.textEdit1.Enabled = true;
            }
        }
    }
}
