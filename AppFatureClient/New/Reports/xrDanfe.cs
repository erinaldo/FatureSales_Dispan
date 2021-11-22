using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Xml;
using ZXing;

namespace AppFatureClient.New.Reports
{
    public partial class xrDanfe : DevExpress.XtraReports.UI.XtraReport
    {
        public int IDMOV;
        private DataSet dsXML;
        private ArrayList listItem = new ArrayList();
        private XmlDocument xDoc = new XmlDocument();

        public xrDanfe()
        {
            InitializeComponent();
        }

        private void xrDanfe_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            dsXML = StringToDataSet(GetXMLFormatado());

            CarregaCabecalho();
            CarregaEmitente();
            CarregaRemetente();
            CarregaCalculoImposto();
            CarregaTransportador();
            CarregaFaturaDuplicata();
            CarregaRodape();
        }

        private void DetailReport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CarregaItens();
            PreencheItens(DetailReport2);
        }

        #region Classes

        private class Item
        {
            public string cProd { get; set; }
            public string xProd { get; set; }
            public string NCM { get; set; }
            public string CFOP { get; set; }
            public string uCom { get; set; }
            public decimal qCom { get; set; }
            public decimal vUnCom { get; set; }
            public decimal vProd { get; set; }
            public string uTrib { get; set; }
            public string qTrib { get; set; }
            public string vUnTrib { get; set; }
            public string indTot { get; set; }
            public decimal vTotTrib { get; set; }
            public decimal orig { get; set; }
            public string CST { get; set; }
            public decimal vBCSTRet { get; set; }
            public decimal vICMSSTRet { get; set; }
            public string cEnq { get; set; }
            public decimal vIPI { get; set; }
            public decimal vBC { get; set; }
            public string xNomeComponente { get; set; }
            public string vComp { get; set; }
            public decimal pICMS { get; set; }
            public decimal pIPI { get; set; }
            public decimal vICMS { get; set; }
            public string nDup { get; set; }
            public DateTime dVenc { get; set; }
            public decimal vDup { get; set; }
            public string nFat { get; set; }
            public decimal vOrig { get; set; }
            public decimal vLiq { get; set; }
            public decimal CSOSN { get; set; }
            public string serie_numeroDoc { get; set; }
            public string chave { get; set; }
            public string tipoDoc { get; set; }
            public string cpf_cnpj { get; set; }
            public decimal vRec { get; set; }
            public DateTime dataEntrega { get; set; }
            public string numero { get; set; }
            public string cliente { get; set; }
            public string codProduto { get; set; }
            public string qtd { get; set; }
            public string total_qtd { get; set; }
            public string observacao { get; set; }
            public string transportadora { get; set; }
            public string codUnidOper { get; set; }
            public string cEan { get; set; }
        }

        #endregion

        #region Métodos

        private string GetXMLFormatado()
        {
            string xml = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT XMLNFE FROM TNFEESTADUAL WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, IDMOV }).ToString();

            int indexi = xml.IndexOf('<', 0);
            int indexf = xml.IndexOf('>', 0);

            string xmlFormatado = xml.Substring(indexi, indexf + 1);

            if (xmlFormatado.Contains("xml version"))
            {
                xml = xml.Replace(xmlFormatado, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            }
            else
            {
                xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + xml;
            }

            return xml;
        }

        public DataSet StringToDataSet(string XML)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();

                xdoc.LoadXml(XML);
                DataSet ds = new DataSet();
                ds.ReadXml(new XmlTextReader(new StringReader(xdoc.DocumentElement.OuterXml)));

                xDoc = xdoc;

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();

            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        private byte[] CriarCodigoBarraCodigo128(string texto)
        {
            BarcodeWriter writer = new BarcodeWriter()
            {
                Format = BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions()
                {
                    Height = 30,
                    Width = 30,
                    Margin = 0, 
                    PureBarcode = true
                }
            };

            byte[] arrayimagem = ImageToByte(writer.Write(texto));

            return arrayimagem;
        }

        private void CarregaCabecalho()
        {
            // Empresa
            tbEmpresa.Text = string.Format(@"RECEBEMOS DE " + dsXML.Tables["emit"].Rows[0]["xNome"].ToString() + " OS PRODUTOS E/OU SERVIÇOS CONSTANTES DA NOTA FISCAL ELETRÔNICA INDICADA ABAIXO.");

            // Números
            if (dsXML.Tables["ide"].Columns["nNF"] != null)
            {
                tbNumero.Text = dsXML.Tables["ide"].Rows[0]["nNF"].ToString();
                switch (tbNumero.Text.Length)
                {
                    case 1:
                        tbNumero.Text = "Nº 000.000.00" + tbNumero.Text;
                        break;
                    case 2:
                        tbNumero.Text = "Nº 000.000.0" + tbNumero.Text;
                        break;
                    case 3:
                        tbNumero.Text = "Nº 000.000." + tbNumero.Text;
                        break;
                    case 4:
                        tbNumero.Text = "Nº 000.00" + tbNumero.Text.Substring(0, 1) + "." + tbNumero.Text.Substring(1, 3);
                        break;
                    case 5:
                        tbNumero.Text = "Nº 000.0" + tbNumero.Text.Substring(0, 2) + "." + tbNumero.Text.Substring(2, 3);
                        break;
                    case 6:
                        tbNumero.Text = "Nº 000." + tbNumero.Text.Substring(0, 3) + "." + tbNumero.Text.Substring(3, 3);
                        break;
                    case 7:
                        tbNumero.Text = "Nº 00" + tbNumero.Text.Substring(0, 1) + "." + tbNumero.Text.Substring(1, 3) + "." + tbNumero.Text.Substring(4, 3);
                        break;
                    case 8:
                        tbNumero.Text = "Nº 0" + tbNumero.Text.Substring(0, 2) + "." + tbNumero.Text.Substring(2, 3) + "." + tbNumero.Text.Substring(5, 3);
                        break;
                    case 9:
                        tbNumero.Text = "Nº 0" + tbNumero.Text.Substring(0, 3) + "." + tbNumero.Text.Substring(3, 3) + "." + tbNumero.Text.Substring(6, 3);
                        break;

                    default:
                        break;
                }
            }

            // Série
            if (dsXML.Tables["ide"].Columns["serie"] != null)
            {
                tbNumeroSerie.Text = dsXML.Tables["ide"].Rows[0]["serie"].ToString();
                if (tbNumeroSerie.Text.Length.Equals(1))
                {
                    tbNumeroSerie.Text = "Série 00" + tbNumeroSerie.Text;
                }
                else if (tbNumeroSerie.Text.Length.Equals(2))
                {
                    tbNumeroSerie.Text = "Série 0" + tbNumeroSerie.Text;
                }
            }
        }

        private void CarregaEmitente()
        {
            // Logo do Emitente 
            //string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)";
            //sSql = AppLib.Context.poolConnection.Get("Start").ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            //byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new Object[] { });
            //MemoryStream ms = new MemoryStream(arrayimagem);
            //pbImagemEmitente.Image = Image.FromStream(ms);

            // Nome Emitente
            tbNomeEmitente.Text = dsXML.Tables["emit"].Rows[0]["xNome"].ToString();

            // Endereço
            tbEndEmitente.Text = dsXML.Tables["enderEmit"].Rows[0]["xLgr"].ToString();

            // Número
            tbNumeroEmitente.Text = dsXML.Tables["enderEmit"].Rows[0]["nro"].ToString();

            // Bairro
            tbBairroEmitente.Text = dsXML.Tables["enderEmit"].Rows[0]["xBairro"].ToString();

            // UF
            tbUFEmitente.Text = dsXML.Tables["enderEmit"].Rows[0]["UF"].ToString();

            // CEP
            tbCepEmitente.Text = (string.Format("{0:#####-###}", Convert.ToInt32(dsXML.Tables["enderEmit"].Rows[0]["CEP"].ToString())));

            // Fone
            tbFoneEmitente.Text = (string.Format("{0:#####-###}", Convert.ToInt32(dsXML.Tables["enderEmit"].Rows[0]["fone"].ToString())));

            // Tipo de Nota 
            if (dsXML.Tables["ide"].Columns["tpNF"] != null)
            {
                tbTipoNota.Text = dsXML.Tables["ide"].Rows[0]["tpNF"].ToString();
            }

            // Número
            tbNumero1.Text = tbNumero.Text;

            // Série
            tbSerie1.Text = tbNumeroSerie.Text;

            // Código de Barras
            MemoryStream msCb = new MemoryStream(CriarCodigoBarraCodigo128(dsXML.Tables["infNFe"].Rows[0]["Id"].ToString()));
            pbCodigoBarra.Image = Image.FromStream(msCb);

            // Chave de Acesso
            if (dsXML.Tables["infNFe"].Columns["Id"] != null)
            {
                string bloco1 = string.Format(@"{0:0000 0000 0000 0000}", Convert.ToUInt64(dsXML.Tables["infNFe"].Rows[0]["Id"].ToString().Remove(0, 3).Substring(0, 16)));
                string bloco2 = string.Format(@"{0: 0000 0000 0000 0000}", Convert.ToUInt64(dsXML.Tables["infNFe"].Rows[0]["Id"].ToString().Remove(0, 3).Substring(16, 16)));
                string bloco3 = string.Format(@"{0: 0000 0000 0000}", Convert.ToUInt64(dsXML.Tables["infNFe"].Rows[0]["Id"].ToString().Remove(0, 3).Substring(32, 12)));
                tbChaveAcesso.Text = bloco1 + bloco2 + bloco3;
            }

            // Natureza da Operação
            if (dsXML.Tables["ide"].Columns["natOp"] != null)
            {
                tbNaturezaOperacao.Text = dsXML.Tables["ide"].Rows[0]["natOp"].ToString();
            }

            // Protocolo de Autorização de Uso
            if (dsXML.Tables["infProt"].Columns["dhRecbto"] != null)
            {
                string[] separador;

                separador = dsXML.Tables["infProt"].Rows[0]["dhRecbto"].ToString().Split('T');
                DateTime dataProtolocolo = Convert.ToDateTime(separador[0].ToString());
                tbProtocoloAutorizacao.Text = dsXML.Tables["infProt"].Rows[0]["nProt"].ToString() + " - " + Convert.ToDateTime(separador[0].ToString()).ToShortDateString() + " " + Convert.ToDateTime(separador[1].ToString()).ToShortTimeString();
            }

            // Inscrição Estadual
            if (dsXML.Tables["emit"].Columns["IE"] != null)
            {
                tbInscricaoEstadual.Text = dsXML.Tables["emit"].Rows[0]["IE"].ToString();
            }

            // Inscrição Estadual do Subst. Tributário
            if (dsXML.Tables["enderEmit"] != null)
            {
                if (dsXML.Tables["enderEmit"].Columns["IEST"] != null)
                {
                    tbInscricaoEstadualSubst.Text = dsXML.Tables["enderEmit"].Rows[0]["IEST"].ToString();
                }
            }

            // CNPJ
            if (dsXML.Tables["emit"].Columns["CNPJ"] != null)
            {
                tbCnpj.Text = String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["emit"].Rows[0]["CNPJ"].ToString()));
            }
        }

        private void CarregaRemetente()
        {
            if (dsXML.Tables["dest"] != null)
            {
                // Razão Social
                if (dsXML.Tables["dest"].Columns["xNome"] != null)
                {
                    tbNomeRazao.Text = dsXML.Tables["dest"].Rows[0]["xNome"].ToString();
                }

                // CPF ou CNPJ
                if (dsXML.Tables["dest"].Columns["CNPJ"] != null)
                {
                    tbCnpjDest.Text = String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["dest"].Rows[0]["CNPJ"].ToString()));
                }
                if (dsXML.Tables["dest"].Columns["CPF"] != null)
                {
                    tbCnpjDest.Text = String.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(dsXML.Tables["dest"].Rows[0]["CPF"].ToString()));
                }

                // Data Emissão
                if (dsXML.Tables["ide"].Columns["dhEmi"] != null)
                {
                    tbDataEmissao.Text = Convert.ToDateTime(dsXML.Tables["ide"].Rows[0]["dhEmi"].ToString()).ToShortDateString();
                }

                if (dsXML.Tables["enderDest"] != null)
                {
                    // Endereço 
                    if (dsXML.Tables["enderDest"].Columns["xLgr"] != null)
                    {
                        tbEndDest.Text = dsXML.Tables["enderDest"].Rows[0]["xLgr"].ToString() + ", ";
                    }
                    if (dsXML.Tables["enderDest"].Columns["nro"] != null)
                    {
                        tbEndDest.Text = tbEndDest.Text + dsXML.Tables["enderDest"].Rows[0]["nro"].ToString();
                    }
                    if (dsXML.Tables["enderDest"].Columns["xCpl"] != null)
                    {
                        tbEndDest.Text = tbEndDest.Text + " - " + dsXML.Tables["enderDest"].Rows[0]["xCpl"].ToString();
                    }

                    // Bairro
                    if (dsXML.Tables["enderDest"].Columns["xBairro"] != null)
                    {
                        tbBairroDest.Text = dsXML.Tables["enderDest"].Rows[0]["xBairro"].ToString();
                    }

                    // CEP
                    if (dsXML.Tables["enderDest"].Columns["CEP"] != null)
                    {
                        tbCepDest.Text = string.Format("{0:00000-000}", Convert.ToUInt64(dsXML.Tables["enderDest"].Rows[0]["CEP"].ToString()));
                    }

                    // Data da Entrada/Saída
                    if (dsXML.Tables["ide"].Columns["dhSaiEnt"] != null)
                    {
                        tbDataSaidaDest.Text = Convert.ToDateTime(dsXML.Tables["ide"].Rows[0]["dhSaiEnt"].ToString()).ToShortDateString();
                    }

                    // Município
                    if (dsXML.Tables["enderDest"].Columns["xMun"] != null)
                    {
                        tbMunicipioDest.Text = dsXML.Tables["enderDest"].Rows[0]["xMun"].ToString();
                    }

                    // UF
                    if (dsXML.Tables["enderDest"].Columns["UF"] != null)
                    {
                        tbUfDest.Text = dsXML.Tables["enderDest"].Rows[0]["UF"].ToString();
                    }

                    // Fone/Fax
                    if (dsXML.Tables["enderDest"].Columns["fone"] != null)
                    {
                        tbFoneDest.Text = string.Format("{0:(00) 0000-0000}", Convert.ToUInt64(dsXML.Tables["enderDest"].Rows[0]["fone"].ToString()));
                    }

                    // Inscrição Estadual
                    if (dsXML.Tables["dest"].Columns["IE"] != null)
                    {
                        tbInscrDest.Text = dsXML.Tables["dest"].Rows[0]["IE"].ToString();
                    }

                    // Hora da Entrada/Saída
                    if (dsXML.Tables["ide"].Columns["dhSaiEnt"] != null)
                    {
                        string[] separador;

                        separador = dsXML.Tables["ide"].Rows[0]["dhSaiEnt"].ToString().Split('T');
                        separador = separador[1].ToString().Split('-');
                        tbHoraDest.Text = Convert.ToDateTime(separador[0].ToString()).ToLongTimeString();
                    }
                }
            }
        }

        private void CarregaCalculoImposto()
        {
            if (dsXML.Tables["ICMSTot"] != null)
            {
                // Base de Cálculo ICMS
                if (dsXML.Tables["ICMSTot"].Columns["vBC"] != null)
                {
                    tbBaseCalculoICMS.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vBC"].ToString().Replace(".", ",")));
                }

                // Valor do ICMS
                if (dsXML.Tables["ICMSTot"].Columns["vICMS"] != null)
                {
                    tbValorICMS.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vICMS"].ToString().Replace(".", ",")));
                }

                // Base de Cálculo ICMS S.T
                if (dsXML.Tables["ICMSTot"].Columns["vBCST"] != null)
                {
                    tbBaseCalculoICMSST.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vBCST"].ToString().Replace(".", ",")));
                }

                // Valor do ICMS ST
                if (dsXML.Tables["ICMSTot"].Columns["vST"] != null)
                {
                    tbValorICMSST.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vST"].ToString().Replace(".", ",")));
                }

                // Valor Total dos Produtos
                if (dsXML.Tables["ICMSTot"].Columns["vProd"] != null)
                {
                    tbValorTotalProdutos.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vProd"].ToString().Replace(".", ",")));
                }

                // Valor do Frete
                if (dsXML.Tables["ICMSTot"].Columns["vFrete"] != null)
                {
                    tbValorFrete.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vFrete"].ToString().Replace(".", ",")));
                }

                // Valor do Seguro
                if (dsXML.Tables["ICMSTot"].Columns["vSeg"] != null)
                {
                    tbValorSeguro.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vSeg"].ToString().Replace(".", ",")));
                }

                // Desconto
                if (dsXML.Tables["ICMSTot"].Columns["vDesc"] != null)
                {
                    tbDesconto.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vDesc"].ToString().Replace(".", ",")));
                }

                // Outras Despesas
                if (dsXML.Tables["ICMSTot"].Columns["vOutro"] != null)
                {
                    tbOutrasDespesas.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vOutro"].ToString().Replace(".", ",")));
                }

                // Valor Total do IPI
                if (dsXML.Tables["ICMSTot"].Columns["vIPI"] != null)
                {
                    tbValorTotalIPI.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vIPI"].ToString().Replace(".", ",")));
                }

                // Valor Total da Nota
                if (dsXML.Tables["ICMSTot"].Columns["vNF"] != null)
                {
                    tbValorTotalNota.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vNF"].ToString().Replace(".", ",")));
                }
            }
        }

        private void CarregaTransportador()
        {
            if (dsXML.Tables["transporta"] != null)
            {
                // Razão Social
                if (dsXML.Tables["transporta"].Columns["xNome"] != null)
                {
                    tbRazaoSocialTransportador.Text = dsXML.Tables["transporta"].Rows[0]["xNome"].ToString();
                }

                // Frete por Conta
                if (dsXML.Tables["transp"].Columns["modFrete"] != null)
                {
                    switch (dsXML.Tables["transp"].Rows[0]["modFrete"].ToString())
                    {
                        case "1":
                            tbFreteConta.Text = "(1) Dest/Rem";
                            break;
                        case "2":
                            tbFreteConta.Text = "(2) Terceiros";
                            break;
                        case "9":
                            tbFreteConta.Text = "(9) Sem Frete";
                            break;
                        default:
                            tbFreteConta.Text = "(0) Emitente";
                            break;
                    }
                }

                if (dsXML.Tables["veicTransp"] != null)
                {
                    // Placa do Veículo
                    if (dsXML.Tables["veicTransp"].Columns["placa"] != null)
                    {
                        tbPlacaVeiculo.Text = dsXML.Tables["veicTransp"].Rows[0]["placa"].ToString();
                    }

                    // UF
                    if (dsXML.Tables["veicTransp"].Columns["UF"] != null)
                    {
                        tbUfTransportador.Text = dsXML.Tables["veicTransp"].Rows[0]["UF"].ToString();
                    }
                }

                // CNPJ
                if (dsXML.Tables["transporta"].Columns["CNPJ"] != null)
                {
                    tbCNPJTransportador.Text = String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["transporta"].Rows[0]["CNPJ"].ToString()));
                }
                else if (dsXML.Tables["transporta"].Columns["CPF"] != null)
                {
                    tbCNPJTransportador.Text = String.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(dsXML.Tables["transporta"].Rows[0]["CPF"].ToString()));
                }

                // Endereço
                if (dsXML.Tables["transporta"].Columns["xEnder"] != null)
                {
                    tbEnderecoTransportador.Text = dsXML.Tables["transporta"].Rows[0]["xEnder"].ToString();
                }

                // Município
                if (dsXML.Tables["transporta"].Columns["xMun"] != null)
                {
                    tbMunicipioTransportador.Text = dsXML.Tables["transporta"].Rows[0]["xMun"].ToString();
                }

                // UF
                if (dsXML.Tables["transporta"].Columns["UF"] != null)
                {
                    tbUfTransportador2.Text = dsXML.Tables["transporta"].Rows[0]["UF"].ToString();
                }

                // Inscrição Estadual
                if (dsXML.Tables["transporta"].Columns["IE"] != null)
                {
                    tbInscricaoEstadualTransportador.Text = dsXML.Tables["transporta"].Rows[0]["IE"].ToString();
                }

                if (dsXML.Tables["vol"] != null)
                {
                    // Quantidade
                    if (dsXML.Tables["vol"].Columns["qVol"] != null)
                    {
                        tbQuantidadeTransportador.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["vol"].Rows[0]["qVol"].ToString()));
                    }

                    // Espécie
                    if (dsXML.Tables["vol"].Columns["esp"] != null)
                    {
                        tbEspecieTransportador.Text = dsXML.Tables["vol"].Rows[0]["esp"].ToString();
                    }

                    // Marca
                    if (dsXML.Tables["vol"].Columns["marca"] != null)
                    {
                        tbMarcaTransportador.Text = dsXML.Tables["vol"].Rows[0]["marca"].ToString();
                    }

                    // Peso Bruto
                    if (dsXML.Tables["vol"].Columns["pesoB"] != null)
                    {
                        tbPesoBrutoTransportador.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["vol"].Rows[0]["pesoB"].ToString().Replace(".", ",")));
                    }

                    // Peso Líquido
                    if (dsXML.Tables["vol"].Columns["pesoL"] != null)
                    {
                        tbPesoLiquidoTransportador.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["vol"].Rows[0]["pesoL"].ToString().Replace(".", ",")));
                    }
                }
            }
        }

        private bool ValidaFaturaDuplicada()
        {
            bool fat = true;

            if (dsXML.Tables.Contains("dup"))
            {
                for (int i = 0; i < dsXML.Tables["dup"].Rows.Count; i++)
                {
                    Item item = new Item();
                    item.nDup = dsXML.Tables["dup"].Rows[i]["nDup"].ToString();
                    item.dVenc = Convert.ToDateTime(dsXML.Tables["dup"].Rows[i]["dVenc"].ToString());
                    item.vDup = Convert.ToDecimal(dsXML.Tables["dup"].Rows[i]["vDup"].ToString().Replace(".", ","));
                    listItem.Add(item);
                    fat = false;
                }
            }
            else if (dsXML.Tables.Contains("fat"))
            {
                for (int i = 0; i < dsXML.Tables["fat"].Rows.Count; i++)
                {
                    Item item = new Item();
                    item.nFat = dsXML.Tables["fat"].Rows[i]["nFat"].ToString();
                    item.vOrig = Convert.ToDecimal(dsXML.Tables["fat"].Rows[i]["vOrig"].ToString().Replace(".", ","));
                    item.vLiq = Convert.ToDecimal(dsXML.Tables["fat"].Rows[i]["vLiq"].ToString().Replace(".", ","));
                    listItem.Add(item);
                    fat = true;
                }
            }

            this.DetailReport.DataSource = listItem;

            return fat;
        }

        private void CarregaFaturaDuplicata()
        {
            if (ValidaFaturaDuplicada())
            {
                if (listItem.Count > 0)
                {
                    // Número
                    tbNumeroFaturaDuplicata.Text = "Número Fat.:";
                    tbValorNumeroFaturaDuplicata.DataBindings.Add("Text", null, "nFat");

                    // Vencimento
                    tbVencimentoFaturaDuplicata.Text = "Valor Orig.:";
                    tbValorVencimentoFaturaDuplicata.DataBindings.Add("Text", null, "vOrig", "{0:n2}");

                    // Valor
                    tbValorFaturaDuplicata.Text = "Valor Liq.:";
                    tbValorValorFaturaDuplicata.DataBindings.Add("Text", null, "vLiq", "{0:n2}");
                }
                else
                {
                    tbValorNumeroFaturaDuplicata.Visible = false;
                    tbValorNumeroFaturaDuplicata.Visible = false;

                    tbVencimentoFaturaDuplicata.Visible = false;
                    tbValorVencimentoFaturaDuplicata.Visible = false;

                    tbValorFaturaDuplicata.Visible = false;
                    tbValorValorFaturaDuplicata.Visible = false;
                }
            }
            else
            {
                if (listItem.Count > 0)
                {
                    // Número
                    tbNumeroFaturaDuplicata.Text = "Número:";
                    tbValorNumeroFaturaDuplicata.DataBindings.Add("Text", null, "nDup");

                    // Vencimento
                    tbVencimentoFaturaDuplicata.Text = "Vencimento:";
                    tbValorVencimentoFaturaDuplicata.DataBindings.Add("Text", null, "dVenc", "{0: dd/MM/yyyy}");

                    // Valor
                    tbValorFaturaDuplicata.Text = "Valor:";
                    tbValorValorFaturaDuplicata.DataBindings.Add("Text", null, "vDup", "{0:n2}");
                }
                else
                {
                    tbValorNumeroFaturaDuplicata.Visible = false;
                    tbValorNumeroFaturaDuplicata.Visible = false;

                    tbVencimentoFaturaDuplicata.Visible = false;
                    tbValorVencimentoFaturaDuplicata.Visible = false;

                    tbValorFaturaDuplicata.Visible = false;
                    tbValorValorFaturaDuplicata.Visible = false;
                }
            }
        }

        private void CarregaItens()
        {
            listItem = new ArrayList();
            StringReader stringReader = new System.IO.StringReader(xDoc.InnerXml);
            XmlTextReader reader = new XmlTextReader(stringReader);
            string elemento;
            bool icms = false;
            Item item = new Item();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        elemento = reader.Name.ToString();
                        switch (elemento)
                        {
                            case "cProd":
                                item.cProd = reader.ReadString();
                                break;
                            case "xProd":
                                item.xProd = reader.ReadString() + " - " + item.cEan;
                                break;
                            case "cEAN":
                                item.cEan = reader.ReadString();
                                break;
                            case "vProd":
                                item.vProd = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "NCM":
                                item.NCM = reader.ReadString();
                                break;
                            case "orig":
                                item.orig = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "CFOP":
                                item.CFOP = reader.ReadString();
                                break;
                            case "uCom":
                                item.uCom = reader.ReadString();
                                break;
                            case "qCom":
                                item.qCom = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "vUnCom":
                                item.vUnCom = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "vBC":
                                item.vBC = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "vICMS":
                                item.vICMS = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "vIPI":
                                item.vIPI = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "pICMS":
                                item.pICMS = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "pIPI":
                                item.pIPI = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "CSOSN":
                                item.CSOSN = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "ICMS":
                                icms = true;
                                break;
                            case "CST":
                                if (icms.Equals(true))
                                {
                                    item.CST = reader.ReadString();
                                    icms = false;
                                }

                                break;
                            default:
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name.Equals("det"))
                        {
                            item.orig = item.orig + item.CSOSN;
                            listItem.Add(item);
                            item = new Item();
                        }
                        break;
                }
            }

            reader.Close();
        }

        private void PreencheItens(DetailReportBand detail)
        {
            detail.DataSource = listItem;

            tbCodigoProduto.DataBindings.Add("Text", null, "cProd");
            tbDescricaoProduto.DataBindings.Add("Text", null, "xProd");
            tbNCMSH.DataBindings.Add("Text", null, "NCM");
            tbOCST.DataBindings.Add("Text", null, "CST");
            tbCFOP.DataBindings.Add("Text", null, "CFOP");
            tbUnidade.DataBindings.Add("Text", null, "uCom");
            tbQuantidade.DataBindings.Add("Text", null, "qCom", "{0:n2}");
            tbValorUnitario.DataBindings.Add("Text", null, "vUnCom", "{0:n2}");
            tbValorTotal.DataBindings.Add("Text", null, "vProd", "{0:n2}");
            tbBaseCalculoICMSItem.DataBindings.Add("Text", null, "vBC", "{0:n2}");
            tbValorICMSItem.DataBindings.Add("Text", null, "vICMS", "{0:n2}");
            tbValorAliquotaICMS.DataBindings.Add("Text", null, "pICMS", "{0:n2}");
            tbValorIPIITem.DataBindings.Add("Text", null, "vIPI", "{0:n2}");
            tbAliquotaIPI.DataBindings.Add("Text", null, "pIPI", "{0:n2}");

            // Não existe mais o campo
            //xrLabel70.DataBindings.Add("Text", null, "orig");     
        }

        private void CarregaRodape()
        {
            /// 

            // Informações Adicionais
            if (dsXML.Tables["infAdic"] != null)
            {
                if (dsXML.Tables["infAdic"].Columns["infCpl"] != null)
                {
                    tbDadosAdicionais.Text = "Inf. Contribuinte: " + dsXML.Tables["infAdic"].Rows[0]["infCpl"].ToString();
                }
            }
        }

        #endregion
    }
}
