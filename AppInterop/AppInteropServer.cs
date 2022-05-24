using System;
using System.Collections.Generic;
using System.Data;

namespace AppInterop
{
    public class AppInteropServer : DbServices
    {
        public class Tributo
        {
            public String TipoTributo { get; set; }
            public Decimal BaseDoCalculo { get; set; }
            public Decimal ValorDaAliquota { get; set; }
            public Decimal ValorDoTributo { get; set; }
        }

        public class PercentoItem
        {
            public String IDPRD { get; set; }
            public Decimal Percento { get; set; }
        }

        private decimal precoUnitario = 0;
        private decimal quantidade = 0;

        public MessageGet GetAppLib(string par1, string par2, string par3)
        {
            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            MessageGet msgg = new MessageGet();

            try
            {
                DataSet oDataSet = new DataSet("DataSet1");
                DataTable oDataTable = this.GetLocal(par1, par2, par3);
                oDataSet.Tables.Clear();
                oDataSet.Tables.Add(oDataTable);

                msgg.Retorno = oDataSet;
                msgg.Mensagem = string.Empty;
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msgg.Retorno = null;
                msgg.Mensagem = err;
            }

            return msgg;
        }

        public Message SetAppLib(string par1, string par2, string par3)
        {
            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            Message msg = new Message();

            try
            {
                int oRowsAffected = this.SetLocal(par1, par2, par3);

                msg.Retorno = oRowsAffected;
                msg.Mensagem = string.Empty;
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msg.Retorno = -1;
                msg.Mensagem = err;
            }

            return msg;
        }

        public MessageGet GetSchemaAppLib(string par1, string par2, string par3)
        {
            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            MessageGet msgg = new MessageGet();

            try
            {
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                DataSet oDataSet = new DataSet("DataSet1");

                AppLib.Data.Connection conn = new AppLib.Data.Connection(
                    "Start",
                    AppLib.Global.Types.Database.SqlClient,
                    new AppLib.Data.AssistantConnection().SqlClient(alias.ServerName, alias.DbName, alias.UserName, alias.Password));

                System.Data.DataTable dt = conn.GetSchemaTable(par3, new Object[] { });

                oDataSet.Tables.Clear();
                oDataSet.Tables.Add(dt);

                msgg.Retorno = oDataSet;
                msgg.Mensagem = string.Empty;
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msgg.Retorno = null;
                msgg.Mensagem = err;
            }

            return msgg;
        }

        private System.Data.DataTable GetLocal(String par1, String par2, String par3)
        {
            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
            AppLib.Data.Connection conn = new AppLib.Data.Connection(
                "Start",
                AppLib.Global.Types.Database.SqlClient,
                new AppLib.Data.AssistantConnection().SqlClient(alias.ServerName, alias.DbName, alias.UserName, alias.Password));

            System.Data.DataTable dt = conn.ExecQuery(par3, new Object[] { });
            return dt;
        }

        private int SetLocal(String par1, String par2, String par3)
        {
            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            AppLib.Util.Alias alias = new Util().GetAlias();
            AppLib.Data.Connection conn = new AppLib.Data.Connection(
                "Start",
                AppLib.Global.Types.Database.SqlClient,
                new AppLib.Data.AssistantConnection().SqlClient(alias.ServerName, alias.DbName, alias.UserName, alias.Password));

            int i = conn.ExecTransaction(par3, new Object[] { });
            return i;
        }

        [Obsolete("Este Método não pode ser mais usado")]
        public MessageGet Get(string par1, string par2, string par3)
        {
            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            Message msg = new Message();
            MessageGet msgg = new MessageGet();

            try
            {
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                msg = this.Autenticar(par1, par2);
                if (!(bool)msg.Retorno)
                {
                    msg.Retorno = null;
                }
                else
                {
                    this.InitServer();
                    DataSet oDataSet = new DataSet("DataSet1");
                    DataTable oDataTable = DBS.QuerySelect("Table1", par3);
                    oDataSet.Tables.Clear();
                    oDataSet.Tables.Add(oDataTable);

                    msgg.Retorno = oDataSet;
                    msgg.Mensagem = string.Empty;
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msgg.Retorno = null;
                msgg.Mensagem = err;
            }

            return msgg;
        }

        [Obsolete("Este Método não pode ser mais usado")]
        public Message Set(string par1, string par2, string par3)
        {
            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            Message msg = new Message();

            try
            {
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                msg = this.Autenticar(par1, par2);
                if (!(bool)msg.Retorno)
                {
                    msg.Retorno = -1;
                }
                else
                {
                    this.InitServer();
                    this.DBS.SkipControlColumns = true;
                    int oRowsAffected = DBS.QueryExec(par3);
                    this.DBS.SkipControlColumns = false;

                    msg.Retorno = oRowsAffected;
                    msg.Mensagem = string.Empty;
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msg.Retorno = -1;
                msg.Mensagem = err;
            }

            return msg;
        }

        [Obsolete("Este Método não pode ser mais usado")]
        public MessageGet Get2(string par1, string par2, string par3)
        {
            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            Message msg = new Message();
            MessageGet msgg = new MessageGet();

            try
            {
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                msg = this.Autenticar(par1, par2);
                if (!(bool)msg.Retorno)
                {
                    msg.Retorno = null;
                }
                else
                {
                    this.InitServer();
                    DataSet oDataSet = new DataSet("DataSet1");
                    DataTable oDataTable = new DataTable("Table1");
                    oDataTable = DBS.GetSchema(par3);
                    oDataSet.Tables.Clear();
                    oDataSet.Tables.Add(oDataTable.Copy());

                    msgg.Retorno = oDataSet;
                    msgg.Mensagem = string.Empty;
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msgg.Retorno = null;
                msgg.Mensagem = err;
            }

            return msgg;
        }

        public Message Autenticar2(string usuario, string senha)
        {
            int Point = 0;
            Message msg = new Message();
            msg.Retorno = true;
            msg.Mensagem = "Autenticação realizada com sucesso";

            try
            {
                if (string.IsNullOrEmpty(usuario))
                {
                    Point = 1;
                    throw new Exception("Usuário do Web Serice não informado.");
                }

                if (string.IsNullOrEmpty(senha))
                {
                    Point = 2;
                    throw new Exception("Senha do Web Serice não informada.");
                }

                String consulta1 = string.Concat(@"SELECT SENHA FROM GUSUARIO WHERE CODUSUARIO = '", usuario, "'");
                System.Data.DataTable dtConsulta1 = GetLocal(usuario, senha, consulta1);
                if (dtConsulta1.Rows.Count > 0)
                {
                    String senhaCorpore = dtConsulta1.Rows[0]["SENHA"].ToString();
                    RM.Lib.Criptografia.RMSCriptografia crip = new RM.Lib.Criptografia.RMSCriptografia();
                    while (!(crip.CheckPassword(senhaCorpore, senha)))
                    {
                        Point = 3;
                        throw new Exception();
                    }

                    //Verifica se o usuário informado tem acesso aos perfis do FATURE
                    consulta1 = string.Concat(@"SELECT COUNT(CODPERFIL) CONTADOR FROM GUSRPERFIL WHERE CODCOLIGADA = 1 AND CODSISTEMA = 'T' AND CODPERFIL LIKE 'APP%' AND CODUSUARIO = '", usuario, "'");
                    System.Data.DataTable dtConsulta2 = GetLocal(usuario, senha, consulta1);
                    if (dtConsulta1.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtConsulta2.Rows[0]["CONTADOR"]) <= 0)
                        {
                            Point = 4;
                            throw new Exception();
                        }
                    }
                }
                else
                {
                    Point = 5;
                    throw new Exception("Usuário ou Senha inválidos! \n\r O usuário ou senha utilizados para login não são válidos para acesso ao sistema. \n\r Verifique se o código do usuário está digitado corretamente e redigite sua senha. \n\r Verifique se a tecla CAPS LOCK não está pressionada acidentalmente.");
                }
            }
            catch (Exception ex)
            {
                string err = string.Empty;
                err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);

                if (Point == 1)
                    err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);

                if (Point == 2)
                    err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);

                if (Point == 3)
                    err = "Usuário ou Senha inválidos! \n\r O usuário ou senha utilizados para login não são válidos para acesso ao sistema. \n\r Verifique se o código do usuário está digitado corretamente e redigite sua senha. \n\r Verifique se a tecla CAPS LOCK não está pressionada acidentalmente.";

                if (Point == 4)
                    err = "Usuário não possui permissão de acesso ao AppFature.";

                if (Point == 5)
                    err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);

                msg.Retorno = false;
                msg.Mensagem = err;
            }

            return msg;
        }

        public Message Autenticar(string usuario, string senha)
        {
            Message msg = new Message();

            try
            {
                if (string.IsNullOrEmpty(usuario))
                    throw new Exception("Usuário do Web Service não informado.");

                if (string.IsNullOrEmpty(senha))
                    throw new Exception("Senha do Web Service não informada.");

                String consulta1 = string.Concat(@"SELECT SENHA FROM GUSUARIO WHERE CODUSUARIO = '", usuario, "'");
                System.Data.DataTable dtConsulta1 = GetLocal(usuario, senha, consulta1);
                if (dtConsulta1.Rows.Count > 0)
                {
                    String senhaCorpore = dtConsulta1.Rows[0]["SENHA"].ToString();
                    RM.Lib.Criptografia.RMSCriptografia crip = new RM.Lib.Criptografia.RMSCriptografia();
                    while (!(crip.CheckPassword(senhaCorpore, senha)))
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }

                msg.Retorno = true;
                msg.Mensagem = "Autenticação realizada com sucesso";

                /*
                RM.Lib.RMSLoginClient.AutoLogin(Alias, usuario, senha);
                if (!RM.Lib.RMSContextManager.IsAuthenticated())
                {
                    msg.Retorno = false;
                    msg.Mensagem = "Usuário não Autenticado";
                }
                else
                {
                    msg.Retorno = true;
                    msg.Mensagem = "Autenticação realizada com sucesso";
                }
                */
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                //string err = "Usuário ou Senha inválidos! \n\r O usuário ou senha utilizados para login não são válidos para acesso ao sistema. \n\r Verifique se o código do usuário está digitado corretamente e redigite sua senha. \n\r Verifique se a tecla CAPS LOCK não está pressionada acidentalmente.";
                msg.Retorno = false;
                msg.Mensagem = err;
            }

            return msg;
        }

        public Message OrcamentoSave(string usuario, string senha, MovMovimentoPar movimento)
        {
            MetodosSQL.CS = AppLib.Context.poolConnection.Get().ConnectionString;

            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            Message msg = new Message();
            string sPoint = string.Empty;
            AppLib.Data.Connection conn = new AppLib.Data.Connection();

            // Rateio 
            decimal rateioDesc = 0, percentualRateioDesc = 0, rateioFrete = 0, percentualRateioFrete = 0, valorTotalItem = 0, valorTotalMovimento = 0;

            try
            {
                sPoint = "Autenticação";
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                msg = this.Autenticar(usuario, senha);

                if (!(bool)msg.Retorno)
                {
                    msg.Retorno = 0;
                }
                else
                {
                    this.InitServer();

                    sPoint = "Existe Movimento";

                    if (!ExisteMovimento(movimento.CodColigada, movimento.IdMov))
                    {
                        #region Rotina Automática
                        /*
                        RM.Mov.Movimento.MovMovInclusaoPar oMovMovInclusaoPar = new RM.Mov.Movimento.MovMovInclusaoPar();

                        oMovMovInclusaoPar.CodColigada = movimento.CodColigada;
                        oMovMovInclusaoPar.CodTMv = movimento.CodTMv;                        
                        oMovMovInclusaoPar.CodFilial = movimento.CodFilial;
                        oMovMovInclusaoPar.CodLoc = movimento.CodLoc;
                        oMovMovInclusaoPar.CodColCfo = movimento.CodColCfo;
                        oMovMovInclusaoPar.CodCfo = movimento.CodCfo;
                        oMovMovInclusaoPar.Serie = movimento.Serie;
                        oMovMovInclusaoPar.DataEmissao = movimento.DataEmissao;
                        oMovMovInclusaoPar.DataEntrega = movimento.DataEntrega;
                        oMovMovInclusaoPar.PrazoEntrega = movimento.PrazoEntrega;
                        oMovMovInclusaoPar.CodRepresentante = movimento.CodRepresentante;
                        oMovMovInclusaoPar.FreteCIFouFOB = movimento.FreteCIFouFOB;
                        oMovMovInclusaoPar.CodTra = movimento.CodTra;
                        oMovMovInclusaoPar.CodCondicaoPagamento = movimento.CodCondicaoPagamento;

                        oMovMovInclusaoPar.ValorFrete = movimento.ValorFrete;
                        oMovMovInclusaoPar.PercentualFrete = movimento.PercentualFrete;
                        oMovMovInclusaoPar.ValorDesc = movimento.ValorDesc;
                        oMovMovInclusaoPar.PercentualDesc = movimento.PercentualDesc;
                        oMovMovInclusaoPar.ValorDesp = movimento.ValorDesp;
                        oMovMovInclusaoPar.PercentualDesp = movimento.PercentualDesp;
                        oMovMovInclusaoPar.ValorSeguro = movimento.ValorSeguro;
                        oMovMovInclusaoPar.PercentualSeguro = movimento.PercentualSeguro;
                        oMovMovInclusaoPar.ValorExtra1 = movimento.ValorExtra1;
                        oMovMovInclusaoPar.PercentualExtra1 = movimento.PercentualExtra1;

                        oMovMovInclusaoPar.Observacao = movimento.Observacao;
                        oMovMovInclusaoPar.CampoLivre1 = movimento.CampoLivre1;
                        oMovMovInclusaoPar.HistoricoLongo = movimento.HistoricoLongo;
                        oMovMovInclusaoPar.NumeroOrdem = movimento.NumeroOrdem;
                        oMovMovInclusaoPar.SegundoNumero = movimento.SegundoNumero;

                        oMovMovInclusaoPar.CodUsuario = movimento.CodUsuario;
                        oMovMovInclusaoPar.CodUsuarioLogado = movimento.CodUsuarioLogado;
                        oMovMovInclusaoPar.DataCriacao = movimento.DataCriacao;
                        oMovMovInclusaoPar.DataMovimento = movimento.DataMovimento;

                        //oMovMovInclusaoPar.CamposComplementares = movimento.CamposComplementares;
                        oMovMovInclusaoPar.CamposComplementares = new Dictionary<string, object>();
                        oMovMovInclusaoPar.CamposComplementares.Add("MSGFORNEC", movimento.MSGFORNEC);
                        oMovMovInclusaoPar.CamposComplementares.Add("CONDPGTO", movimento.CONDPGTO);
                        oMovMovInclusaoPar.CamposComplementares.Add("TPCULTURA", movimento.TPCULTURA);
                        oMovMovInclusaoPar.CamposComplementares.Add("FINANCIADO", movimento.FINANCIADO);
                        oMovMovInclusaoPar.CamposComplementares.Add("DEMONSBRINDE", movimento.DEMONSBRINDE);
                        oMovMovInclusaoPar.CamposComplementares.Add("PARAFINANC", movimento.PARAFINANC);

                        string sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        DataTable Itens = DBS.QuerySelect("ZTITMMOVORCTEMP", sSql, movimento.GuidId);
                        for (int i = 0; i < Itens.Rows.Count; i++)
                        {
                            RM.Mov.Movimento.MovMovItemMovPar oMovMovItemMovPar = new RM.Mov.Movimento.MovMovItemMovPar();

                            oMovMovItemMovPar.CodColigada = Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]);
                            oMovMovItemMovPar.NSeqItmMov = Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]);
                            oMovMovItemMovPar.IdPrd = Convert.ToInt32(Itens.Rows[i]["IDPRD"]);
                            oMovMovItemMovPar.IdPrdComposto = (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]);
                            oMovMovItemMovPar.CodUnd = Itens.Rows[i]["CODUND"].ToString();
                            oMovMovItemMovPar.Quantidade = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]);
                            oMovMovItemMovPar.PrecoUnitario = Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                            oMovMovItemMovPar.HistoricoLongo = Itens.Rows[i]["HISTORICOLONGO"].ToString();

                            //Campos Complementares
                            oMovMovItemMovPar.CamposComplementares = new Dictionary<string, object>();
                            oMovMovItemMovPar.CamposComplementares.Add("PRODPRICIPAL", Itens.Rows[i]["PRODPRICIPAL"].ToString());
                            oMovMovItemMovPar.CamposComplementares.Add("SEQUENCIAL", Itens.Rows[i]["SEQUENCIAL"].ToString());
                            oMovMovItemMovPar.CamposComplementares.Add("TIPOMAT", Itens.Rows[i]["TIPOMAT"].ToString());

                            RM.Mov.Movimento.MovMovTributoPar oMovMovTributoPar = new RM.Mov.Movimento.MovMovTributoPar();
                            oMovMovTributoPar.CodColigada = oMovMovItemMovPar.CodColigada;
                            oMovMovTributoPar.NSeqItmMov = oMovMovItemMovPar.NSeqItmMov;
                            oMovMovTributoPar.CodTrb = "IPI";
                            oMovMovTributoPar.Aliquota = Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]);
                            oMovMovTributoPar.BaseDeCalculo = oMovMovItemMovPar.Quantidade * ((oMovMovItemMovPar.PrecoUnitario == null) ? 0 : (decimal)oMovMovItemMovPar.PrecoUnitario);

                            oMovMovItemMovPar.TributosItemMov.Add(oMovMovTributoPar);                            
                            oMovMovInclusaoPar.ItemMovimento.Add(oMovMovItemMovPar);
                        }

                        sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        this.DBS.SkipControlColumns = true;
                        DBS.QueryExec(sSql, movimento.GuidId);
                        this.DBS.SkipControlColumns = false;

                        RM.Mov.Cst.Facade.CstMovFacade oCstMovFacade = CreateFacade<RM.Mov.Cst.Facade.CstMovFacade>();
                        List<RM.Mov.Movimento.MovMovInclusaoPar> lMovMovInclusaoPar = oCstMovFacade.Incluir(oMovMovInclusaoPar);

                        msg.Retorno = lMovMovInclusaoPar[0].IdMov;
                        msg.Mensagem = "Movimento criado com sucesso";

                        */
                        #endregion

                        #region Rotina Manual

                        //DBS.BeginTransaction();
                        //RMSAutoInc oRMSAutoInc = new RMSAutoInc(this.DBS);
                        //movimento.IdMov = oRMSAutoInc.GetNewValue("T", movimento.CodColigada, "IDMOV");
                        //movimento.NumeroMov = oRMSAutoInc.GetNewValue("T", movimento.CodColigada, "COV000000").ToString();

                        sPoint = string.Concat("Inclusão: [Busca Identificador do Movimento]");
                        movimento.IdMov = this.GetNewAutoInc(movimento.CodColigada, "T", "IDMOV");
                        conn = AppLib.Context.poolConnection.Get();

                        if (movimento.IdMov == 0)
                        {
                            throw new Exception("Inclusão: Identificador do movimento não pode ser 0 (zero).");
                        }

                        sPoint = string.Concat("Inclusão: [Busca Numero do Movimento]");
                        movimento.NumeroMov = this.GetNewAutoInc(movimento.CodColigada, "T", "ORC000001").ToString();
                        movimento.NumeroMov = movimento.NumeroMov.PadLeft(6, '0');

                        conn.BeginTransaction();

                        #region Cabeçalho do Orçamento

                        //                        string sSql = @"INSERT INTO TMOV (CODCOLIGADA, IDMOV, CODFILIAL, CODLOC, 
                        //                                        CODLOCENTREGA, CODLOCDESTINO, CODCFO, CODCFONATUREZA, NUMEROMOV, 
                        //                                        SERIE, CODTMV, TIPO, STATUS, MOVIMPRESSO, DOCIMPRESSO, FATIMPRESSA, 
                        //                                        DATAEMISSAO, DATASAIDA, DATAEXTRA1, DATAEXTRA2, CODRPR, 
                        //                                        COMISSAOREPRES, NORDEM, CODCPG, NUMEROTRIBUTOS, 
                        //                                        VALORBRUTO, VALORLIQUIDO, VALOROUTROS, OBSERVACAO, PERCENTUALFRETE, 
                        //                                        VALORFRETE, PERCENTUALSEGURO, VALORSEGURO, 
                        //                                        PERCENTUALDESC, VALORDESC, PERCENTUALDESP,
                        //                                        VALORDESP, PERCENTUALEXTRA1, VALOREXTRA1, 
                        //                                        PERCENTUALEXTRA2, VALOREXTRA2, PERCCOMISSAO, CODMEN, CODMEN2, VIADETRANSPORTE, 
                        //                                        PLACA, CODETDPLACA, PESOLIQUIDO, PESOBRUTO, MARCA, NUMERO, QUANTIDADE, 
                        //                                        ESPECIE, CODTB1FAT, CODTB2FAT, CODTB3FAT, CODTB4FAT, CODTB5FAT, 
                        //                                        CODTB1FLX, CODTB2FLX, CODTB3FLX, CODTB4FLX, CODTB5FLX, IDMOVRELAC, IDMOVLCTFLUXUS, 
                        //                                        IDMOVPEDDESDOBRADO, CODMOEVALORLIQUIDO, DATABASEMOV, DATAMOVIMENTO, 
                        //                                        NUMEROLCTGERADO, GEROUFATURA, NUMEROLCTABERTO, FLAGEXPORTACAO, EMITEBOLETA, CODMENDESCONTO, 
                        //                                        CODMENDESPESA, CODMENFRETE, FRETECIFOUFOB, USADESPFINANC, FLAGEXPORFISC, 
                        //                                        FLAGEXPORFAZENDA, VALORADIANTAMENTO, CODTRA, CODTRA2, STATUSLIBERACAO, 
                        //                                        CODCFOAUX, IDLOT, ITENSAGRUPADOS, FLAGIMPRESSAOFAT, DATACANCELAMENTOMOV, 
                        //                                        VALORRECEBIDO, SEGUNDONUMERO, CODCCUSTO, CODCXA, CODVEN1, CODVEN2, 
                        //                                        CODVEN3, CODVEN4, PERCCOMISSAOVEN2, CODCOLCFO, CODCOLCFONATUREZA, 
                        //                                        CODUSUARIO, CODFILIALENTREGA, CODFILIALDESTINO, FLAGAGRUPADOFLUXUS, 
                        //                                        CODCOLCXA, GERADOPORLOTE, CODDEPARTAMENTO, CODCCUSTODESTINO, CODEVENTO, STATUSEXPORTCONT, 
                        //                                        CODLOTE, STATUSCHEQUE, DATAENTREGA, DATAPROGRAMACAO, IDNAT, IDNAT2, 
                        //                                        CAMPOLIVRE1, CAMPOLIVRE2, CAMPOLIVRE3, GEROUCONTATRABALHO, GERADOPORCONTATRABALHO, 
                        //                                        HORULTIMAALTERACAO, CODLAF, DATAFECHAMENTO, NSEQDATAFECHAMENTO, NUMERORECIBO, 
                        //                                        IDLOTEPROCESSO, IDOBJOF, CODAGENDAMENTO, CHAPARESP, IDLOTEPROCESSOREFAT, INDUSOOBJ, 
                        //                                        SUBSERIE, STSCOMPRAS, CODLOCEXP, IDCLASSMOV, CODENTREGA, CODFAIXAENTREGA, DTHENTREGA, 
                        //                                        CONTABILIZADOPORTOTAL, CODLAFE, IDPRJ, NUMEROCUPOM, NUMEROCAIXA, FLAGEFEITOSALDO, 
                        //                                        INTEGRADOBONUM, CODMOELANCAMENTO, NAONUMERADO, FLAGPROCESSADO, ABATIMENTOICMS, TIPOCONSUMO, 
                        //                                        HORARIOEMISSAO, DATARETORNO, USUARIOCRIACAO, DATACRIACAO, 
                        //                                        IDCONTATOENTREGA, IDCONTATOCOBRANCA, STATUSSEPARACAO, STSEMAIL, VALORFRETECTRC, PONTOVENDA, 
                        //                                        PRAZOENTREGA, VALORBRUTOINTERNO, IDAIDF, IDSALDOESTOQUE, VINCULADOESTOQUEFL, 
                        //                                        IDREDUCAOZ, HORASAIDA, CODMUNSERVICO, CODETDMUNSERV, APROPRIADO, CODIGOSERVICO, DATADEDUCAO, 
                        //                                        CODDIARIO, SEQDIARIO, SEQDIARIOESTORNO, INSSEMOUTRAEMPRESA, IDMOVCTRC, DATAPROGRAMACAOANT, 
                        //                                        CODTDO, VALORDESCCONDICIONAL, VALORDESPCONDICIONAL, CODIGOIRRF, DEDUCAOIRRF, PERCENTBASEINSS, 
                        //                                        PERCBASEINSSEMPREGADO, CONTORCAMENTOANTIGO, CODDEPTODESTINO, DATACONTABILIZACAO, CODVIATRANSPORTE, 
                        //                                        VALORSERVICO, SEQUENCIALESTOQUE, DISTANCIA, UNCALCULO, FORMACALCULO, INTEGRADOAUTOMACAO, 
                        //                                        INTEGRAAPLICACAO, CLASSECONSUMO, TIPOASSINANTE, FASE, TIPOUTILIZACAO, GRUPOTENSAO, 
                        //                                        DATALANCAMENTO, EXTENPORANEO, RECIBONFESTATUS, RECIBONFETIPO, RECIBONFENUMERO, 
                        //                                        RECIBONFESITUACAO, IDMOVCFO, OCAUTONOMO, VALORMERCADORIAS, NATUREZAVOLUMES, VOLUMES, CRO, 
                        //                                        USARATEIOVALORFIN, RECIBONFESERIE, CODCOLCFOORIGEM, CODCFOORIGEM, VALORCTRCARATEAR, CODCOLCFOAUX, 
                        //                                        VRBASEINSSOUTRAEMPRESA, IDCEICFO, CHAVEACESSONFE, VLRSECCAT, VLRDESPACHO, VLRPEDAGIO, VLRFRETEOUTROS, 
                        //                                        ABATIMENTONAOTRIB, RATEIOCCUSTODEPTO, VALORRATEIOLAN, CODCOLCFOTRANSFAT, CODCFOTRANSFAT, 
                        //                                        CODUSUARIOAPROVADESC, IDINTEGRACAO, STATUSANTERIOR, VALORBRUTOORIG, VALORLIQUIDOORIG, VALOROUTROSORIG, 
                        //                                        VALORRATEIOLANORIG, DATAPROCESSAMENTO, IDNATFRETE, IDOPERACAO, 
                        //                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                        //                                    VALUES (:CODCOLIGADA, :IDMOV, :CODFILIAL, :CODLOC, 
                        //                                        :CODLOCENTREGA, :CODLOCDESTINO, :CODCFO, :CODCFONATUREZA, :NUMEROMOV, 
                        //                                        :SERIE, :CODTMV, :TIPO, :STATUS, :MOVIMPRESSO, :DOCIMPRESSO, :FATIMPRESSA, 
                        //                                        :DATAEMISSAO, :DATASAIDA, :DATAEXTRA1, :DATAEXTRA2, :CODRPR, 
                        //                                        :COMISSAOREPRES, :NORDEM, :CODCPG, :NUMEROTRIBUTOS, 
                        //                                        :VALORBRUTO, :VALORLIQUIDO, :VALOROUTROS, :OBSERVACAO, :PERCENTUALFRETE, 
                        //                                        :VALORFRETE, :PERCENTUALSEGURO, :VALORSEGURO, 
                        //                                        :PERCENTUALDESC, :VALORDESC, :PERCENTUALDESP,
                        //                                        :VALORDESP, :PERCENTUALEXTRA1, :VALOREXTRA1, 
                        //                                        :PERCENTUALEXTRA2, :VALOREXTRA2, :PERCCOMISSAO, :CODMEN, :CODMEN2, :VIADETRANSPORTE, 
                        //                                        :PLACA, :CODETDPLACA, :PESOLIQUIDO, :PESOBRUTO, :MARCA, :NUMERO, :QUANTIDADE, 
                        //                                        :ESPECIE, :CODTB1FAT, :CODTB2FAT, :CODTB3FAT, :CODTB4FAT, :CODTB5FAT, 
                        //                                        :CODTB1FLX, :CODTB2FLX, :CODTB3FLX, :CODTB4FLX, :CODTB5FLX, :IDMOVRELAC, :IDMOVLCTFLUXUS, 
                        //                                        :IDMOVPEDDESDOBRADO, :CODMOEVALORLIQUIDO, :DATABASEMOV, :DATAMOVIMENTO, 
                        //                                        :NUMEROLCTGERADO, :GEROUFATURA, :NUMEROLCTABERTO, :FLAGEXPORTACAO, :EMITEBOLETA, :CODMENDESCONTO, 
                        //                                        :CODMENDESPESA, :CODMENFRETE, :FRETECIFOUFOB, :USADESPFINANC, :FLAGEXPORFISC, 
                        //                                        :FLAGEXPORFAZENDA, :VALORADIANTAMENTO, :CODTRA, :CODTRA2, :STATUSLIBERACAO, 
                        //                                        :CODCFOAUX, :IDLOT, :ITENSAGRUPADOS, :FLAGIMPRESSAOFAT, :DATACANCELAMENTOMOV, 
                        //                                        :VALORRECEBIDO, :SEGUNDONUMERO, :CODCCUSTO, :CODCXA, :CODVEN1, :CODVEN2, 
                        //                                        :CODVEN3, :CODVEN4, :PERCCOMISSAOVEN2, :CODCOLCFO, :CODCOLCFONATUREZA, 
                        //                                        :CODUSUARIO, :CODFILIALENTREGA, :CODFILIALDESTINO, :FLAGAGRUPADOFLUXUS, 
                        //                                        :CODCOLCXA, :GERADOPORLOTE, :CODDEPARTAMENTO, :CODCCUSTODESTINO, :CODEVENTO, :STATUSEXPORTCONT, 
                        //                                        :CODLOTE, :STATUSCHEQUE, :DATAENTREGA, :DATAPROGRAMACAO, :IDNAT, :IDNAT2, 
                        //                                        :CAMPOLIVRE1, :CAMPOLIVRE2, :CAMPOLIVRE3, :GEROUCONTATRABALHO, :GERADOPORCONTATRABALHO, 
                        //                                        :HORULTIMAALTERACAO, :CODLAF, :DATAFECHAMENTO, :NSEQDATAFECHAMENTO, :NUMERORECIBO, 
                        //                                        :IDLOTEPROCESSO, :IDOBJOF, :CODAGENDAMENTO, :CHAPARESP, :IDLOTEPROCESSOREFAT, :INDUSOOBJ, 
                        //                                        :SUBSERIE, :STSCOMPRAS, :CODLOCEXP, :IDCLASSMOV, :CODENTREGA, :CODFAIXAENTREGA, :DTHENTREGA, 
                        //                                        :CONTABILIZADOPORTOTAL, :CODLAFE, :IDPRJ, :NUMEROCUPOM, :NUMEROCAIXA, :FLAGEFEITOSALDO, 
                        //                                        :INTEGRADOBONUM, :CODMOELANCAMENTO, :NAONUMERADO, :FLAGPROCESSADO, :ABATIMENTOICMS, :TIPOCONSUMO, 
                        //                                        :HORARIOEMISSAO, :DATARETORNO, :USUARIOCRIACAO, :DATACRIACAO, 
                        //                                        :IDCONTATOENTREGA, :IDCONTATOCOBRANCA, :STATUSSEPARACAO, :STSEMAIL, :VALORFRETECTRC, :PONTOVENDA, 
                        //                                        :PRAZOENTREGA, :VALORBRUTOINTERNO, :IDAIDF, :IDSALDOESTOQUE, :VINCULADOESTOQUEFL, 
                        //                                        :IDREDUCAOZ, :HORASAIDA, :CODMUNSERVICO, :CODETDMUNSERV, :APROPRIADO, :CODIGOSERVICO, :DATADEDUCAO, 
                        //                                        :CODDIARIO, :SEQDIARIO, :SEQDIARIOESTORNO, :INSSEMOUTRAEMPRESA, :IDMOVCTRC, :DATAPROGRAMACAOANT, 
                        //                                        :CODTDO, :VALORDESCCONDICIONAL, :VALORDESPCONDICIONAL, :CODIGOIRRF, :DEDUCAOIRRF, :PERCENTBASEINSS, 
                        //                                        :PERCBASEINSSEMPREGADO, :CONTORCAMENTOANTIGO, :CODDEPTODESTINO, :DATACONTABILIZACAO, :CODVIATRANSPORTE, 
                        //                                        :VALORSERVICO, :SEQUENCIALESTOQUE, :DISTANCIA, :UNCALCULO, :FORMACALCULO, :INTEGRADOAUTOMACAO, 
                        //                                        :INTEGRAAPLICACAO, :CLASSECONSUMO, :TIPOASSINANTE, :FASE, :TIPOUTILIZACAO, :GRUPOTENSAO, 
                        //                                        :DATALANCAMENTO, :EXTENPORANEO, :RECIBONFESTATUS, :RECIBONFETIPO, :RECIBONFENUMERO, 
                        //                                        :RECIBONFESITUACAO, :IDMOVCFO, :OCAUTONOMO, :VALORMERCADORIAS, :NATUREZAVOLUMES, :VOLUMES, :CRO, 
                        //                                        :USARATEIOVALORFIN, :RECIBONFESERIE, :CODCOLCFOORIGEM, :CODCFOORIGEM, :VALORCTRCARATEAR, :CODCOLCFOAUX, 
                        //                                        :VRBASEINSSOUTRAEMPRESA, :IDCEICFO, :CHAVEACESSONFE, :VLRSECCAT, :VLRDESPACHO, :VLRPEDAGIO, :VLRFRETEOUTROS, 
                        //                                        :ABATIMENTONAOTRIB, :RATEIOCCUSTODEPTO, :VALORRATEIOLAN, :CODCOLCFOTRANSFAT, :CODCFOTRANSFAT, 
                        //                                        :CODUSUARIOAPROVADESC, :IDINTEGRACAO, :STATUSANTERIOR, :VALORBRUTOORIG, :VALORLIQUIDOORIG, :VALOROUTROSORIG, 
                        //                                        :VALORRATEIOLANORIG, :DATAPROCESSAMENTO, :IDNATFRETE, :IDOPERACAO, 
                        //                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";
                        string sSql = @"INSERT INTO TMOV (CODCOLIGADA, IDMOV, CODFILIAL, CODLOC, 
                                        CODLOCENTREGA, CODLOCDESTINO, CODCFO, CODCFONATUREZA, NUMEROMOV, 
                                        SERIE, CODTMV, TIPO, STATUS, MOVIMPRESSO, DOCIMPRESSO, FATIMPRESSA, 
                                        DATAEMISSAO, DATASAIDA, DATAEXTRA1, DATAEXTRA2, CODRPR, 
                                        COMISSAOREPRES, NORDEM, CODCPG, NUMEROTRIBUTOS, 
                                        VALORBRUTO, VALORLIQUIDO, VALOROUTROS, OBSERVACAO, PERCENTUALFRETE, 
                                        VALORFRETE, PERCENTUALSEGURO, VALORSEGURO, 
                                        PERCENTUALDESC, VALORDESC, PERCENTUALDESP,
                                        VALORDESP, PERCENTUALEXTRA1, VALOREXTRA1, 
                                        PERCENTUALEXTRA2, VALOREXTRA2, PERCCOMISSAO, CODMEN, CODMEN2, VIADETRANSPORTE, 
                                        PLACA, CODETDPLACA, PESOLIQUIDO, PESOBRUTO, MARCA, NUMERO, QUANTIDADE, 
                                        ESPECIE, CODTB1FAT, CODTB2FAT, CODTB3FAT, CODTB4FAT, CODTB5FAT, 
                                        CODTB1FLX, CODTB2FLX, CODTB3FLX, CODTB4FLX, CODTB5FLX, IDMOVRELAC, IDMOVLCTFLUXUS, 
                                        IDMOVPEDDESDOBRADO, CODMOEVALORLIQUIDO, DATABASEMOV, DATAMOVIMENTO, 
                                        NUMEROLCTGERADO, GEROUFATURA, NUMEROLCTABERTO, FLAGEXPORTACAO, EMITEBOLETA, CODMENDESCONTO, 
                                        CODMENDESPESA, CODMENFRETE, FRETECIFOUFOB, USADESPFINANC, FLAGEXPORFISC, 
                                        FLAGEXPORFAZENDA, VALORADIANTAMENTO, CODTRA, CODTRA2, STATUSLIBERACAO, 
                                        CODCFOAUX, IDLOT, ITENSAGRUPADOS, FLAGIMPRESSAOFAT, DATACANCELAMENTOMOV, 
                                        VALORRECEBIDO, SEGUNDONUMERO, CODCCUSTO, CODCXA, CODVEN1, CODVEN2, 
                                        CODVEN3, CODVEN4, PERCCOMISSAOVEN2, CODCOLCFO, CODCOLCFONATUREZA, 
                                        CODUSUARIO, CODFILIALENTREGA, CODFILIALDESTINO, FLAGAGRUPADOFLUXUS, 
                                        CODCOLCXA, GERADOPORLOTE, CODDEPARTAMENTO, CODCCUSTODESTINO, CODEVENTO, STATUSEXPORTCONT, 
                                        CODLOTE, STATUSCHEQUE, DATAENTREGA, DATAPROGRAMACAO, IDNAT, IDNAT2, 
                                        CAMPOLIVRE1, CAMPOLIVRE2, CAMPOLIVRE3, GEROUCONTATRABALHO, GERADOPORCONTATRABALHO, 
                                        HORULTIMAALTERACAO, CODLAF, DATAFECHAMENTO, NSEQDATAFECHAMENTO, NUMERORECIBO, 
                                        IDLOTEPROCESSO, IDOBJOF, CODAGENDAMENTO, CHAPARESP, IDLOTEPROCESSOREFAT, INDUSOOBJ, 
                                        SUBSERIE, STSCOMPRAS, CODLOCEXP, IDCLASSMOV, CODENTREGA, CODFAIXAENTREGA, DTHENTREGA, 
                                        CONTABILIZADOPORTOTAL, CODLAFE, IDPRJ, NUMEROCUPOM, NUMEROCAIXA, FLAGEFEITOSALDO, 
                                        INTEGRADOBONUM, CODMOELANCAMENTO, NAONUMERADO, FLAGPROCESSADO, ABATIMENTOICMS, TIPOCONSUMO, 
                                        HORARIOEMISSAO, DATARETORNO, USUARIOCRIACAO, DATACRIACAO, 
                                        IDCONTATOENTREGA, IDCONTATOCOBRANCA, STATUSSEPARACAO, STSEMAIL, VALORFRETECTRC, PONTOVENDA, 
                                        PRAZOENTREGA, VALORBRUTOINTERNO, IDAIDF, IDSALDOESTOQUE, VINCULADOESTOQUEFL, 
                                        IDREDUCAOZ, HORASAIDA, CODMUNSERVICO, CODETDMUNSERV, APROPRIADO, CODIGOSERVICO, DATADEDUCAO, 
                                        CODDIARIO, SEQDIARIO, SEQDIARIOESTORNO, INSSEMOUTRAEMPRESA, IDMOVCTRC, DATAPROGRAMACAOANT, 
                                        CODTDO, VALORDESCCONDICIONAL, VALORDESPCONDICIONAL, CODIGOIRRF, DEDUCAOIRRF, PERCENTBASEINSS, 
                                        PERCBASEINSSEMPREGADO, CONTORCAMENTOANTIGO, CODDEPTODESTINO, DATACONTABILIZACAO, CODVIATRANSPORTE, 
                                        VALORSERVICO, SEQUENCIALESTOQUE, DISTANCIA, UNCALCULO, FORMACALCULO, INTEGRADOAUTOMACAO, 
                                        INTEGRAAPLICACAO, CLASSECONSUMO, TIPOASSINANTE, FASE, TIPOUTILIZACAO, GRUPOTENSAO, 
                                        DATALANCAMENTO, EXTENPORANEO, RECIBONFESTATUS, RECIBONFETIPO, RECIBONFENUMERO, 
                                        RECIBONFESITUACAO, IDMOVCFO, OCAUTONOMO, VALORMERCADORIAS, NATUREZAVOLUMES, VOLUMES, CRO, 
                                        USARATEIOVALORFIN, RECIBONFESERIE, CODCOLCFOORIGEM, CODCFOORIGEM, VALORCTRCARATEAR, CODCOLCFOAUX, 
                                        VRBASEINSSOUTRAEMPRESA, IDCEICFO, CHAVEACESSONFE, VLRSECCAT, VLRDESPACHO, VLRPEDAGIO, VLRFRETEOUTROS, 
                                        ABATIMENTONAOTRIB, RATEIOCCUSTODEPTO, VALORRATEIOLAN, CODCOLCFOTRANSFAT, CODCFOTRANSFAT, 
                                        CODUSUARIOAPROVADESC, IDINTEGRACAO, STATUSANTERIOR, VALORBRUTOORIG, VALORLIQUIDOORIG, VALOROUTROSORIG, 
                                        VALORRATEIOLANORIG, DATAPROCESSAMENTO, IDNATFRETE, IDOPERACAO, 
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                    VALUES (?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?,?,?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?,?, ?, ?,?, ?, ?,?, ?, ?, ?, ?, ?, 
                                        ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?,?, ?, ?, ?,?, ?,?, ?, ?,?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, 
                                        ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?,?, ?, ?, ?,?, ?,?, ?, ?, ?, ?, ?,?,?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?,?, ?,?, ?, ?, ?, ?, ?, ?, 
                                        ?, ?, ?,?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                        ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, 
                                        ?, ?, ?, ?, ?, ?,?, ?, ?, ?,?, ?, ?, ?)";

                        sPoint = string.Concat("Inclusão: [Grava TMOV]");
                        //DBS.QueryExec(sSql,
                        //    /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*CODFILIAL*/ movimento.CodFilial, /*CODLOC*/ movimento.CodLoc,
                        //    /*CODLOCENTREGA*/ null, /*CODLOCDESTINO*/ null, /*CODCFO*/ movimento.CodCfo, /*CODCFONATUREZA*/ null, /*NUMEROMOV*/ movimento.NumeroMov,
                        //    /*SERIE*/ "COV", /*CODTMV*/ "2.1.03", /*TIPO*/ "A", /*STATUS*/ "A", /*MOVIMPRESSO*/ 0, /*DOCIMPRESSO*/ 0, /*FATIMPRESSA*/ 0,
                        //    /*DATAEMISSAO*/ movimento.DataEmissao, /*DATASAIDA*/ null, /*DATAEXTRA1*/ null, /*DATAEXTRA2*/ null, /*CODRPR*/ movimento.CodRepresentante,
                        //    /*COMISSAOREPRES*/ 0, /*NORDEM*/ movimento.NumeroOrdem, /*CODCPG*/ "F001", /*NUMEROTRIBUTOS*/ null,
                        //    /*VALORBRUTO*/ 0, /*VALORLIQUIDO*/ 0, /*VALOROUTROS*/ 0, /*OBSERVACAO*/ movimento.Observacao, /*PERCENTUALFRETE*/ movimento.PercentualFrete,
                        //    /*VALORFRETE*/ movimento.ValorFrete, /*PERCENTUALSEGURO*/ movimento.PercentualSeguro, /*VALORSEGURO*/ movimento.ValorSeguro,
                        //    /*PERCENTUALDESC*/ movimento.PercentualDesc, /*VALORDESC*/ movimento.ValorDesc, /*PERCENTUALDESP*/ movimento.PercentualDesp,
                        //    /*VALORDESP*/ movimento.ValorDesp, /*PERCENTUALEXTRA1*/ movimento.PercentualExtra1, /*VALOREXTRA1*/ movimento.ValorExtra1,
                        //    /*PERCENTUALEXTRA2*/ null, /*VALOREXTRA2*/ null, /*PERCCOMISSAO*/ 0, /*CODMEN*/ null, /*CODMEN2*/ null, /*VIADETRANSPORTE*/ null,
                        //    /*PLACA*/ null, /*CODETDPLACA*/ null, /*PESOLIQUIDO*/ 0,  /*PESOBRUTO*/ 0, /*MARCA*/ null, /*NUMERO*/ null, /*QUANTIDADE*/ null,
                        //    /*ESPECIE*/ null, /*CODTB1FAT*/ null, /*CODTB2FAT*/ null, /*CODTB3FAT*/ null, /*CODTB4FAT*/ null, /*CODTB5FAT*/ null,
                        //    /*CODTB1FLX*/ null, /*CODTB2FLX*/ null, /*CODTB3FLX*/ null, /*CODTB4FLX*/ null, /*CODTB5FLX*/ null, /*IDMOVRELAC*/ null, /*IDMOVLCTFLUXUS*/ null,
                        //    /*IDMOVPEDDESDOBRADO*/ null, /*CODMOEVALORLIQUIDO*/ "R$", /*DATABASEMOV*/ null, /*DATAMOVIMENTO*/ movimento.DataMovimento,
                        //    /*NUMEROLCTGERADO*/ null, /*GEROUFATURA*/ 0, /*NUMEROLCTABERTO*/ null, /*FLAGEXPORTACAO*/ null, /*EMITEBOLETA*/ null, /*CODMENDESCONTO*/ null,
                        //    /*CODMENDESPESA*/ null, /*CODMENFRETE*/ null, /*FRETECIFOUFOB*/ movimento.FreteCIFouFOB, /*USADESPFINANC*/ null, /*FLAGEXPORFISC*/ null,
                        //    /*FLAGEXPORFAZENDA*/ null, /*VALORADIANTAMENTO*/ null, /*CODTRA*/ movimento.CodTra, /*CODTRA2*/ null, /*STATUSLIBERACAO*/ null,
                        //    /*CODCFOAUX*/ "CXXXXXXXXXX", /*IDLOT*/ null, /*ITENSAGRUPADOS*/ null, /*FLAGIMPRESSAOFAT*/ null, /*DATACANCELAMENTOMOV*/ null,
                        //    /*VALORRECEBIDO*/ null, /*SEGUNDONUMERO*/ movimento.SegundoNumero, /*CODCCUSTO*/ null, /*CODCXA*/ null, /*CODVEN1*/ null, /*CODVEN2*/ null,
                        //    /*CODVEN3*/ null, /*CODVEN4*/ null, /*PERCCOMISSAOVEN2*/ 0, /*CODCOLCFO*/ movimento.CodColCfo, /*CODCOLCFONATUREZA*/ null,
                        //    /*CODUSUARIO*/ movimento.CodUsuario, /*CODFILIALENTREGA*/ null, /*CODFILIALDESTINO*/ movimento.CodFilial, /*FLAGAGRUPADOFLUXUS*/ null,
                        //    /*CODCOLCXA*/ null, /*GERADOPORLOTE*/ 0, /*CODDEPARTAMENTO*/ null, /*CODCCUSTODESTINO*/ null, /*CODEVENTO*/ null, /*STATUSEXPORTCONT*/ 0,
                        //    /*CODLOTE*/ null, /*STATUSCHEQUE*/ null, /*DATAENTREGA*/ movimento.DataEntrega, /*DATAPROGRAMACAO*/ null, /*IDNAT*/ null, /*IDNAT2*/ null,
                        //    /*CAMPOLIVRE1*/ movimento.CampoLivre1, /*CAMPOLIVRE2*/ null, /*CAMPOLIVRE3*/ null, /*GEROUCONTATRABALHO*/ 0, /*GERADOPORCONTATRABALHO*/ 0,
                        //    /*HORULTIMAALTERACAO*/ DBS.ServerDate(), /*CODLAF*/ null, /*DATAFECHAMENTO*/ null, /*NSEQDATAFECHAMENTO*/ null, /*NUMERORECIBO*/ null,
                        //    /*IDLOTEPROCESSO*/ null, /*IDOBJOF*/ null, /*CODAGENDAMENTO*/ null, /*CHAPARESP*/ null, /*IDLOTEPROCESSOREFAT*/ null, /*INDUSOOBJ*/ 0,
                        //    /*SUBSERIE*/ null, /*STSCOMPRAS*/ null, /*CODLOCEXP*/ null, /*IDCLASSMOV*/ null, /*CODENTREGA*/ null, /*CODFAIXAENTREGA*/ null, /*DTHENTREGA*/ null,
                        //    /*CONTABILIZADOPORTOTAL*/ null, /*CODLAFE*/ null, /*IDPRJ*/ null, /*NUMEROCUPOM*/ null, /*NUMEROCAIXA*/ null, /*FLAGEFEITOSALDO*/ null,
                        //    /*INTEGRADOBONUM*/ 0, /*CODMOELANCAMENTO*/ null, /*NAONUMERADO*/ null, /*FLAGPROCESSADO*/ 0, /*ABATIMENTOICMS*/ 0, /*TIPOCONSUMO*/ null,
                        //    /*HORARIOEMISSAO*/ DBS.ServerDate(), /*DATARETORNO*/ null, /*USUARIOCRIACAO*/ movimento.CodUsuario, /*DATACRIACAO*/ DateTime.Today,
                        //    /*IDCONTATOENTREGA*/ null, /*IDCONTATOCOBRANCA*/ null, /*STATUSSEPARACAO*/ null, /*STSEMAIL*/ 0, /*VALORFRETECTRC*/ null, /*PONTOVENDA*/ null,
                        //    /*PRAZOENTREGA*/ movimento.PrazoEntrega, /*VALORBRUTOINTERNO*/ 0, /*IDAIDF*/ null, /*IDSALDOESTOQUE*/ null, /*VINCULADOESTOQUEFL*/ 0,
                        //    /*IDREDUCAOZ*/ null, /*HORASAIDA*/ null, /*CODMUNSERVICO*/ null, /*CODETDMUNSERV*/ null, /*APROPRIADO*/ null, /*CODIGOSERVICO*/ null, /*DATADEDUCAO*/ null,
                        //    /*CODDIARIO*/ null, /*SEQDIARIO*/ null, /*SEQDIARIOESTORNO*/ null, /*INSSEMOUTRAEMPRESA*/ null, /*IDMOVCTRC*/ null, /*DATAPROGRAMACAOANT*/ null,
                        //    /*CODTDO*/ null, /*VALORDESCCONDICIONAL*/ 0, /*VALORDESPCONDICIONAL*/ 0, /*CODIGOIRRF*/ null, /*DEDUCAOIRRF*/ null, /*PERCENTBASEINSS*/ null,
                        //    /*PERCBASEINSSEMPREGADO*/ null, /*CONTORCAMENTOANTIGO*/ null, /*CODDEPTODESTINO*/ null, /*DATACONTABILIZACAO*/ null, /*CODVIATRANSPORTE*/ null,
                        //    /*VALORSERVICO*/ null, /*SEQUENCIALESTOQUE*/ null, /*DISTANCIA*/ null, /*UNCALCULO*/ null, /*FORMACALCULO*/ null, /*INTEGRADOAUTOMACAO*/ 0,
                        //    /*INTEGRAAPLICACAO*/ "T", /*CLASSECONSUMO*/ null, /*TIPOASSINANTE*/ null, /*FASE*/ null, /*TIPOUTILIZACAO*/ null, /*GRUPOTENSAO*/ null,
                        //    /*DATALANCAMENTO*/ DateTime.Today, /*EXTENPORANEO*/ null, /*RECIBONFESTATUS*/ 0, /*RECIBONFETIPO*/ null, /*RECIBONFENUMERO*/ null,
                        //    /*RECIBONFESITUACAO*/ null, /*IDMOVCFO*/ movimento.IdMovCfo, /*OCAUTONOMO*/ null, /*VALORMERCADORIAS*/ 0, /*NATUREZAVOLUMES*/ null, /*VOLUMES*/ null, /*CRO*/ null,
                        //    /*USARATEIOVALORFIN*/ 0, /*RECIBONFESERIE*/ null, /*CODCOLCFOORIGEM*/ null, /*CODCFOORIGEM*/ null, /*VALORCTRCARATEAR*/ null, /*CODCOLCFOAUX*/ 0,
                        //    /*VRBASEINSSOUTRAEMPRESA*/ 0, /*IDCEICFO*/ null, /*CHAVEACESSONFE*/ null, /*VLRSECCAT*/ null, /*VLRDESPACHO*/ null, /*VLRPEDAGIO*/ null, /*VLRFRETEOUTROS*/ null,
                        //    /*ABATIMENTONAOTRIB*/ null, /*RATEIOCCUSTODEPTO*/ null, /*VALORRATEIOLAN*/ null, /*CODCOLCFOTRANSFAT*/ null, /*CODCFOTRANSFAT*/ null,
                        //    /*CODUSUARIOAPROVADESC*/ null, /*IDINTEGRACAO*/ null, /*STATUSANTERIOR*/ null, /*VALORBRUTOORIG*/ 0, /*VALORLIQUIDOORIG*/ 0, /*VALOROUTROSORIG*/ 0,
                        //    /*VALORRATEIOLANORIG*/ null, /*DATAPROCESSAMENTO*/ null, /*IDNATFRETE*/ null, /*IDOPERACAO*/ null,
                        //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());


                        string sql = conn.ParseCommand(sSql, new object[]{
                            /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*CODFILIAL*/ movimento.CodFilial, /*CODLOC*/ movimento.CodLoc,
                            /*CODLOCENTREGA*/ null, /*CODLOCDESTINO*/ null, /*CODCFO*/ movimento.CodCfo, /*CODCFONATUREZA*/ null, /*NUMEROMOV*/ movimento.NumeroMov,
                            /*SERIE*/ movimento.Serie, /*CODTMV*/ movimento.CodTMv, /*TIPO*/ "A", /*STATUS*/ "A", /*MOVIMPRESSO*/ 0, /*DOCIMPRESSO*/ 0, /*FATIMPRESSA*/ 0,
                            /*DATAEMISSAO*/ Convert.ToDateTime(movimento.DataEmissao).ToString("yyy-MM-dd"), /*DATASAIDA*/ null, /*DATAEXTRA1*/ null, /*DATAEXTRA2*/ null, /*CODRPR*/ movimento.CodRepresentante,
                            /*COMISSAOREPRES*/ 0, /*NORDEM*/ movimento.NumeroOrdem, /*CODCPG*/ movimento.CodCondicaoPagamento, /*NUMEROTRIBUTOS*/ null,
                            /*VALORBRUTO*/ 0, /*VALORLIQUIDO*/ 0, /*VALOROUTROS*/ 0, /*OBSERVACAO*/ movimento.Observacao, /*PERCENTUALFRETE*/ movimento.PercentualFrete,
                            /*VALORFRETE*/ movimento.ValorFrete, /*PERCENTUALSEGURO*/ movimento.PercentualSeguro, /*VALORSEGURO*/ movimento.ValorSeguro,
                            /*PERCENTUALDESC*/ movimento.PercentualDesc, /*VALORDESC*/ movimento.ValorDesc, /*PERCENTUALDESP*/ movimento.PercentualDesp,
                            /*VALORDESP*/ movimento.ValorDesp, /*PERCENTUALEXTRA1*/ movimento.PercentualExtra1, /*VALOREXTRA1*/ movimento.ValorExtra1,
                            /*PERCENTUALEXTRA2*/ null, /*VALOREXTRA2*/ null, /*PERCCOMISSAO*/ 0, /*CODMEN*/ null, /*CODMEN2*/ null, /*VIADETRANSPORTE*/ null,
                            /*PLACA*/ null, /*CODETDPLACA*/ null, /*PESOLIQUIDO*/ 0,  /*PESOBRUTO*/ 0, /*MARCA*/ null, /*NUMERO*/ null, /*QUANTIDADE*/ null,
                            /*ESPECIE*/ null, /*CODTB1FAT*/ null, /*CODTB2FAT*/ null, /*CODTB3FAT*/ null, /*CODTB4FAT*/ null, /*CODTB5FAT*/ null,
                            /*CODTB1FLX*/ movimento.CodTb1Opcional, /*CODTB2FLX*/ null, /*CODTB3FLX*/ null, /*CODTB4FLX*/ null, /*CODTB5FLX*/ null, /*IDMOVRELAC*/ null, /*IDMOVLCTFLUXUS*/ null,
                            /*IDMOVPEDDESDOBRADO*/ null, /*CODMOEVALORLIQUIDO*/ "R$", /*DATABASEMOV*/ null, /*DATAMOVIMENTO*/ movimento.DataMovimento,
                            /*NUMEROLCTGERADO*/ null, /*GEROUFATURA*/ 0, /*NUMEROLCTABERTO*/ null, /*FLAGEXPORTACAO*/ null, /*EMITEBOLETA*/ null, /*CODMENDESCONTO*/ null,
                            /*CODMENDESPESA*/ null, /*CODMENFRETE*/ null, /*FRETECIFOUFOB*/ movimento.FreteCIFouFOB, /*USADESPFINANC*/ null, /*FLAGEXPORFISC*/ null,
                            /*FLAGEXPORFAZENDA*/ null, /*VALORADIANTAMENTO*/ null, /*CODTRA*/ movimento.CodTra, /*CODTRA2*/ null, /*STATUSLIBERACAO*/ null,
                            /*CODCFOAUX*/ "CXXXXXXXXXX", /*IDLOT*/ null, /*ITENSAGRUPADOS*/ null, /*FLAGIMPRESSAOFAT*/ null, /*DATACANCELAMENTOMOV*/ null,
                            /*VALORRECEBIDO*/ null, /*SEGUNDONUMERO*/ movimento.SegundoNumero, /*CODCCUSTO*/ null, /*CODCXA*/ null, /*CODVEN1*/ movimento.CodVen1, /*CODVEN2*/ null,
                            /*CODVEN3*/ null, /*CODVEN4*/ null, /*PERCCOMISSAOVEN2*/ 0, /*CODCOLCFO*/ movimento.CodColCfo, /*CODCOLCFONATUREZA*/ null,
                            /*CODUSUARIO*/ movimento.CodUsuario, /*CODFILIALENTREGA*/ null, /*CODFILIALDESTINO*/ movimento.CodFilial, /*FLAGAGRUPADOFLUXUS*/ null,
                            /*CODCOLCXA*/ null, /*GERADOPORLOTE*/ 0, /*CODDEPARTAMENTO*/ null, /*CODCCUSTODESTINO*/ null, /*CODEVENTO*/ null, /*STATUSEXPORTCONT*/ 0,
                            /*CODLOTE*/ null, /*STATUSCHEQUE*/ null, /*DATAENTREGA*/ movimento.DataEntrega, /*DATAPROGRAMACAO*/ null, /*IDNAT*/ movimento.IdNat, /*IDNAT2*/ null,
                            /*CAMPOLIVRE1*/ movimento.CampoLivre1, /*CAMPOLIVRE2*/ null, /*CAMPOLIVRE3*/ null, /*GEROUCONTATRABALHO*/ 0, /*GERADOPORCONTATRABALHO*/ 0,
                            /*HORULTIMAALTERACAO*/ conn.GetDateTime(), /*CODLAF*/ null, /*DATAFECHAMENTO*/ null, /*NSEQDATAFECHAMENTO*/ null, /*NUMERORECIBO*/ null,
                            /*IDLOTEPROCESSO*/ null, /*IDOBJOF*/ null, /*CODAGENDAMENTO*/ null, /*CHAPARESP*/ null, /*IDLOTEPROCESSOREFAT*/ null, /*INDUSOOBJ*/ 0,
                            /*SUBSERIE*/ null, /*STSCOMPRAS*/ null, /*CODLOCEXP*/ null, /*IDCLASSMOV*/ null, /*CODENTREGA*/ null, /*CODFAIXAENTREGA*/ null, /*DTHENTREGA*/ null,
                            /*CONTABILIZADOPORTOTAL*/ null, /*CODLAFE*/ null, /*IDPRJ*/ null, /*NUMEROCUPOM*/ null, /*NUMEROCAIXA*/ null, /*FLAGEFEITOSALDO*/ null,
                            /*INTEGRADOBONUM*/ 0, /*CODMOELANCAMENTO*/ null, /*NAONUMERADO*/ null, /*FLAGPROCESSADO*/ 0, /*ABATIMENTOICMS*/ 0, /*TIPOCONSUMO*/ null,
                            /*HORARIOEMISSAO*/ conn.GetDateTime(), /*DATARETORNO*/ null, /*USUARIOCRIACAO*/ movimento.CodUsuario, /*DATACRIACAO*/ DateTime.Today,
                            /*IDCONTATOENTREGA*/ null, /*IDCONTATOCOBRANCA*/ null, /*STATUSSEPARACAO*/ null, /*STSEMAIL*/ 0, /*VALORFRETECTRC*/ null, /*PONTOVENDA*/ null,
                            /*PRAZOENTREGA*/ movimento.PrazoEntrega, /*VALORBRUTOINTERNO*/ 0, /*IDAIDF*/ null, /*IDSALDOESTOQUE*/ null, /*VINCULADOESTOQUEFL*/ 0,
                            /*IDREDUCAOZ*/ null, /*HORASAIDA*/ null, /*CODMUNSERVICO*/ null, /*CODETDMUNSERV*/ null, /*APROPRIADO*/ null, /*CODIGOSERVICO*/ null, /*DATADEDUCAO*/ null,
                            /*CODDIARIO*/ null, /*SEQDIARIO*/ null, /*SEQDIARIOESTORNO*/ null, /*INSSEMOUTRAEMPRESA*/ null, /*IDMOVCTRC*/ null, /*DATAPROGRAMACAOANT*/ null,
                            /*CODTDO*/ null, /*VALORDESCCONDICIONAL*/ 0, /*VALORDESPCONDICIONAL*/ 0, /*CODIGOIRRF*/ null, /*DEDUCAOIRRF*/ null, /*PERCENTBASEINSS*/ null,
                            /*PERCBASEINSSEMPREGADO*/ null, /*CONTORCAMENTOANTIGO*/ null, /*CODDEPTODESTINO*/ null, /*DATACONTABILIZACAO*/ null, /*CODVIATRANSPORTE*/ null,
                            /*VALORSERVICO*/ null, /*SEQUENCIALESTOQUE*/ null, /*DISTANCIA*/ null, /*UNCALCULO*/ null, /*FORMACALCULO*/ null, /*INTEGRADOAUTOMACAO*/ 0,
                            /*INTEGRAAPLICACAO*/ "T", /*CLASSECONSUMO*/ null, /*TIPOASSINANTE*/ null, /*FASE*/ null, /*TIPOUTILIZACAO*/ null, /*GRUPOTENSAO*/ null,
                            /*DATALANCAMENTO*/ DateTime.Today, /*EXTENPORANEO*/ null, /*RECIBONFESTATUS*/ 0, /*RECIBONFETIPO*/ null, /*RECIBONFENUMERO*/ null,
                            /*RECIBONFESITUACAO*/ null, /*IDMOVCFO*/ movimento.IdMovCfo, /*OCAUTONOMO*/ null, /*VALORMERCADORIAS*/ 0, /*NATUREZAVOLUMES*/ null, /*VOLUMES*/ null, /*CRO*/ null,
                            /*USARATEIOVALORFIN*/ 0, /*RECIBONFESERIE*/ null, /*CODCOLCFOORIGEM*/ null, /*CODCFOORIGEM*/ null, /*VALORCTRCARATEAR*/ null, /*CODCOLCFOAUX*/ 0,
                            /*VRBASEINSSOUTRAEMPRESA*/ 0, /*IDCEICFO*/ null, /*CHAVEACESSONFE*/ null, /*VLRSECCAT*/ null, /*VLRDESPACHO*/ null, /*VLRPEDAGIO*/ null, /*VLRFRETEOUTROS*/ null,
                            /*ABATIMENTONAOTRIB*/ null, /*RATEIOCCUSTODEPTO*/ null, /*VALORRATEIOLAN*/ null, /*CODCOLCFOTRANSFAT*/ null, /*CODCFOTRANSFAT*/ null,
                            /*CODUSUARIOAPROVADESC*/ null, /*IDINTEGRACAO*/ null, /*STATUSANTERIOR*/ null, /*VALORBRUTOORIG*/ 0, /*VALORLIQUIDOORIG*/ 0, /*VALOROUTROSORIG*/ 0,
                            /*VALORRATEIOLANORIG*/ null, /*DATAPROCESSAMENTO*/ null, /*IDNATFRETE*/ null, /*IDOPERACAO*/ null,
                            /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ conn.GetDateTime()});

                        //MetodosSQL.CS = conn.ConnectionString;
                        //MetodosSQL.ExecQuery(sql);

                        conn.ExecTransaction(sql, new object[] { });







                        //                        sSql = @"INSERT INTO TMOVHISTORICO (CODCOLIGADA, IDMOV, HISTORICOCURTO, HISTORICOLONGO, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                        //                                VALUES (:CODCOLIGADA, :IDMOV, :HISTORICOCURTO, :HISTORICOLONGO, :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                        sSql = @"INSERT INTO TMOVHISTORICO (CODCOLIGADA, IDMOV, HISTORICOCURTO, HISTORICOLONGO, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

                        sPoint = string.Concat("Inclusão: [Grava TMOVHISTORICO]");
                        //DBS.QueryExec(sSql,
                        //    /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*HISTORICOCURTO*/ null, /*HISTORICOLONGO*/ movimento.HistoricoLongo,
                        //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                        conn.ExecTransaction(sSql, new object[]{
                            /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*HISTORICOCURTO*/ null, /*HISTORICOLONGO*/ movimento.HistoricoLongo,
                            /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ conn.GetDateTime()});

                        //                        sSql = @"INSERT INTO TMOVCOMPL (CODCOLIGADA, IDMOV, MSGFORNEC, CONDPGTO, TPCULTURA, FINANCIADO, DEMONSBRINDE, PARAFINANC,
                        //                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, TIPOVENDA) 
                        //                                VALUES (:CODCOLIGADA, :IDMOV, :MSGFORNEC, :CONDPGTO, :TPCULTURA, :FINANCIADO, :DEMONSBRINDE, :PARAFINANC,
                        //                                    :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :TIPOVENDA)";

                        sSql = @"INSERT INTO TMOVCOMPL (CODCOLIGADA, IDMOV, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, APLICPROD, TIPOVENDA, NCONTRIB, UFORCAMENTO, NOMEFCFOORC, TIPODESC, VALORDESCRAT, CONTATOCOM, EMAILCOM, TELEFONECOM, VALORDESPRAT, SEMIMPOSTOS, USABENEFICIO) 
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";


                        sPoint = string.Concat("Inclusão: [Grava TMOVCOMPL]");
                        //DBS.QueryExec(sSql,
                        //    /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*MSGFORNEC*/ movimento.MSGFORNEC, /*CONDPGTO*/ movimento.CONDPGTO,
                        //    /*TPCULTURA*/ movimento.TPCULTURA, /*FINANCIADO*/ movimento.FINANCIADO, /*DEMONSBRINDE*/ movimento.DEMONSBRINDE, /*PARAFINANC*/ movimento.PARAFINANC,
                        //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(), /*TIPOVENDA*/ movimento.TIPOVENDA);

                        conn.ExecTransaction(sSql, new object[]{
                            /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ conn.GetDateTime(), 
                            /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ conn.GetDateTime(), /*APLICPROD*/ movimento.APLICPROD, /*TIPOVENDA*/ movimento.TIPOVENDA, /*NCONTRIB*/ movimento.NCONTRIB, /*UFORCAMENTO*/ movimento.UFORCAMENTO,
                            /*NOMEFCFOORC*/ movimento.NOMECLIENTEORC, /*TIPODESC*/ movimento.TIPODESC, /*VALORDESCRAT*/ movimento.VALORDESCRAT, /*CONTATOCOM*/ movimento.CONTATOCOM, /*EMAILCOM*/ movimento.EMAILCOM, /*TELEFONECOM*/ movimento.TELEFONECOM, /*VALORDESPRAT*/ movimento.VALORDESPRAT,
                            /*SEMIMPOSTOS*/ movimento.SEMIMPOSTOS, /*USABENEFICIO*/ movimento.POSSUIBENEFICIO});

                        #endregion

                        #region Item do Orçamento

                        sPoint = string.Concat("Inclusão: [Busca ZTITMMOVORCTEMP]");
                        //sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = ?";
                        //DataTable Itens = DBS.QuerySelect("ZTITMMOVORCTEMP", sSql, movimento.GuidId);
                        DataTable Itens = conn.ExecQuery(sSql, new object[] { movimento.GuidId });

                        sSql = String.Format(@"SELECT sum((QUANTIDADE*PRECOUNITARIO)) as 'TOTAL' FROM ZTITMMOVORCTEMP where ROWID = '{0}'", movimento.GuidId);

                        Decimal total = (Decimal)conn.ExecGetField(0, sSql, new object[] { });

                        for (int i = 0; i < Itens.Rows.Count; i++)
                        {
                            string sCODTB2FAT = null;
                            string sCODTB3FAT = null;
                            string sCODTB4FAT = null;

                            Decimal totalItem = (Decimal)Itens.Rows[i]["QUANTIDADE"] * (Decimal)Itens.Rows[i]["PRECOUNITARIO"];
                            Decimal percentItem = (totalItem * 100) / total;
                            percentItem = percentItem / 100;

                            sPoint = string.Concat("Inclusão: Item ", (i + 1), " CODTB2FAT, CODTB3FAT, CODTB4FAT");
                            //sSql = @"SELECT TPRD.CODTB2FAT, TPRD.CODTB3FAT, TPRD.CODTB4FAT FROM TPRD WHERE TPRD.CODCOLIGADA = :CODCOLIGADA AND TPRD.IDPRD = :IDPRD";
                            sSql = @"SELECT TPRD.CODTB2FAT, TPRD.CODTB3FAT, TPRD.CODTB4FAT FROM TPRD WHERE TPRD.CODCOLIGADA = ? AND TPRD.IDPRD = ?";


                            //                            DataTable dtPrd = DBS.QuerySelect("TPRD", sSql, Itens.Rows[i]["CODCOLIGADA"], Itens.Rows[i]["IDPRD"]);
                            DataTable dtPrd = conn.ExecQuery(sSql, new object[] { Itens.Rows[i]["CODCOLIGADA"], Itens.Rows[i]["IDPRD"] });
                            foreach (DataRow row in dtPrd.Rows)
                            {
                                sCODTB2FAT = (row["CODTB2FAT"] == DBNull.Value) ? null : row["CODTB2FAT"].ToString();
                                sCODTB3FAT = (row["CODTB3FAT"] == DBNull.Value) ? null : row["CODTB3FAT"].ToString();
                                sCODTB4FAT = (row["CODTB4FAT"] == DBNull.Value) ? null : row["CODTB4FAT"].ToString();
                            }

                            //                            sSql = @"INSERT INTO TITMMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, NUMEROSEQUENCIAL, IDPRD, CODTIP, QUANTIDADE, PRECOUNITARIO, PRECOTABELA, 
                            //                                        PERCENTUALDESC, VALORDESC, PERCENTUALDESP, VALORDESP, DATAEMISSAO, 
                            //                                        CODMEN, NUMEROTRIBUTOS, CODTB1FAT, CODTB2FAT, CODTB3FAT, CODTB4FAT, 
                            //                                        CODTB5FAT, CODTB1FLX, CODTB2FLX, CODTB3FLX, CODTB4FLX, CODTB5FLX, CAMPOLIVRE, 
                            //                                        CODUND, QUANTIDADEARECEBER, CODNAT, CODCPG, DATAENTREGA, PRATELEIRA, IDCNT, NSEQITMCNT, 
                            //                                        DATAINIFAT, DATAFIMFAT, FLAGEFEITOSALDO, VALORUNITARIO, VALORFINANCEIRO, 
                            //                                        IMPRIMEMOV, CODCCUSTO, FLAGREPASSE, ALIQORDENACAO, QUANTIDADEORIGINAL, 
                            //                                        IDNAT, FLAG, CHAPA, INICIO, TERMINO, PREVINICIO, STATUS, BLOCK, 
                            //                                        FLAGREFATURAMENTO, IDCNTDESTINO, NSEQITMCNTDEST, FATORCONVUND, IDPRJ, IDTRF, 
                            //                                        VALORTOTALITEM, VALORCODIGOPRD, TIPOCODIGOPRD, QTDUNDPEDIDO, TRIBUTACAOECF, 
                            //                                        CODFILIAL, CODDEPARTAMENTO, IDPRDCOMPOSTO, QUANTIDADESEPARADA, PERCENTCOMISSAO, INDICENCM, NCM, CODRPR, COMISSAOREPRES, 
                            //                                        NSEQITMCNTMEDICAO, VALORESCRITURACAO, VALORFINPEDIDO, VALORFRETECTRC, VALOROPFRM1, VALOROPFRM2, 
                            //                                        IDOBJOFICINA, PRECOEDITADO, QTDEVOLUMEUNITARIO, IDGRD, 
                            //                                        CODVEN1, CODLOCALBN, REGISTROEXPORTACAO, DATARE, PRECOTOTALEDITADO, CST, 
                            //                                        VALORDESCCONDICONALITM, VALORDESPCONDICIONALITM, DATAORCAMENTO, CODTBORCAMENTO, RATEIOFRETE, 
                            //                                        RATEIOSEGURO, RATEIODESC, RATEIODESP, RATEIOEXTRA1, RATEIOEXTRA2, RATEIOFRETECTRC, 
                            //                                        RATEIODEDMAT, RATEIODEDSUB, RATEIODEDOUT, IDCLASSIFENERGIACOMUNIC, VALORUNTORCAMENTO, 
                            //                                        VALSERVICONFE, CODLOC, VALORBEM, VALORLIQUIDO, CODIGOCODIF, CODMUNSERVICO, CODETDMUNSERV, RATEIOCCUSTODEPTO, CUSTOREPOSICAO, 
                            //                                        CUSTOREPOSICAOB, VALORFINTERCEIROS, VALORFINANCGERENCIAL, CODIGOSERVICO, VALORUNITGERENCIAL, 
                            //                                        IDINTEGRACAO, IDTABPRECO, VALORBRUTOITEM, VALORBRUTOITEMORIG, CODCOLTBORCAMENTO, CODPUBLIC, QUANTIDADETOTAL, 
                            //                                        PRODUTOSUBSTITUTO, CODTBGRUPOORC, PRECOUNITARIOSELEC, VALORRATEIOLAN, 
                            //                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                            //                                    VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :NUMEROSEQUENCIAL, :IDPRD, :CODTIP, :QUANTIDADE, :PRECOUNITARIO, :PRECOTABELA, 
                            //                                        :PERCENTUALDESC, :VALORDESC, :PERCENTUALDESP, :VALORDESP, :DATAEMISSAO, 
                            //                                        :CODMEN, :NUMEROTRIBUTOS, :CODTB1FAT, :CODTB2FAT, :CODTB3FAT, :CODTB4FAT, 
                            //                                        :CODTB5FAT, :CODTB1FLX, :CODTB2FLX, :CODTB3FLX, :CODTB4FLX, :CODTB5FLX, :CAMPOLIVRE, 
                            //                                        :CODUND, :QUANTIDADEARECEBER, :CODNAT, :CODCPG, :DATAENTREGA, :PRATELEIRA, :IDCNT, :NSEQITMCNT, 
                            //                                        :DATAINIFAT, :DATAFIMFAT, :FLAGEFEITOSALDO, :VALORUNITARIO, :VALORFINANCEIRO, 
                            //                                        :IMPRIMEMOV, :CODCCUSTO, :FLAGREPASSE, :ALIQORDENACAO, :QUANTIDADEORIGINAL, 
                            //                                        :IDNAT, :FLAG, :CHAPA, :INICIO, :TERMINO, :PREVINICIO, :STATUS, :BLOCK, 
                            //                                        :FLAGREFATURAMENTO, :IDCNTDESTINO, :NSEQITMCNTDEST, :FATORCONVUND, :IDPRJ, :IDTRF, 
                            //                                        :VALORTOTALITEM, :VALORCODIGOPRD, :TIPOCODIGOPRD, :QTDUNDPEDIDO, :TRIBUTACAOECF, 
                            //                                        :CODFILIAL, :CODDEPARTAMENTO, :IDPRDCOMPOSTO, :QUANTIDADESEPARADA, :PERCENTCOMISSAO, :INDICENCM, :NCM, :CODRPR, :COMISSAOREPRES, 
                            //                                        :NSEQITMCNTMEDICAO, :VALORESCRITURACAO, :VALORFINPEDIDO, :VALORFRETECTRC, :VALOROPFRM1, :VALOROPFRM2, 
                            //                                        :IDOBJOFICINA, :PRECOEDITADO, :QTDEVOLUMEUNITARIO, :IDGRD, 
                            //                                        :CODVEN1, :CODLOCALBN, :REGISTROEXPORTACAO, :DATARE, :PRECOTOTALEDITADO, :CST, 
                            //                                        :VALORDESCCONDICONALITM, :VALORDESPCONDICIONALITM, :DATAORCAMENTO, :CODTBORCAMENTO, :RATEIOFRETE, 
                            //                                        :RATEIOSEGURO, :RATEIODESC, :RATEIODESP, :RATEIOEXTRA1, :RATEIOEXTRA2, :RATEIOFRETECTRC, 
                            //                                        :RATEIODEDMAT, :RATEIODEDSUB, :RATEIODEDOUT, :IDCLASSIFENERGIACOMUNIC, :VALORUNTORCAMENTO, 
                            //                                        :VALSERVICONFE, :CODLOC, :VALORBEM, :VALORLIQUIDO, :CODIGOCODIF, :CODMUNSERVICO, :CODETDMUNSERV, :RATEIOCCUSTODEPTO, :CUSTOREPOSICAO, 
                            //                                        :CUSTOREPOSICAOB, :VALORFINTERCEIROS, :VALORFINANCGERENCIAL, :CODIGOSERVICO, :VALORUNITGERENCIAL, 
                            //                                        :IDINTEGRACAO, :IDTABPRECO, :VALORBRUTOITEM, :VALORBRUTOITEMORIG, :CODCOLTBORCAMENTO, :CODPUBLIC, :QUANTIDADETOTAL, 
                            //                                        :PRODUTOSUBSTITUTO, :CODTBGRUPOORC, :PRECOUNITARIOSELEC, :VALORRATEIOLAN, 
                            //                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                            sSql = @"INSERT INTO TITMMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, NUMEROSEQUENCIAL, IDPRD, CODTIP, QUANTIDADE, PRECOUNITARIO, PRECOTABELA, 
                                        PERCENTUALDESC, VALORDESC, PERCENTUALDESP, VALORDESP, DATAEMISSAO, 
                                        CODMEN, NUMEROTRIBUTOS, CODTB1FAT, CODTB2FAT, CODTB3FAT, CODTB4FAT, 
                                        CODTB5FAT, CODTB1FLX, CODTB2FLX, CODTB3FLX, CODTB4FLX, CODTB5FLX, CAMPOLIVRE, 
                                        CODUND, QUANTIDADEARECEBER, CODNAT, CODCPG, DATAENTREGA, PRATELEIRA, IDCNT, NSEQITMCNT, 
                                        DATAINIFAT, DATAFIMFAT, FLAGEFEITOSALDO, VALORUNITARIO, VALORFINANCEIRO, 
                                        IMPRIMEMOV, CODCCUSTO, FLAGREPASSE, ALIQORDENACAO, QUANTIDADEORIGINAL, 
                                        IDNAT, FLAG, CHAPA, INICIO, TERMINO, PREVINICIO, STATUS, BLOCK, 
                                        FLAGREFATURAMENTO, IDCNTDESTINO, NSEQITMCNTDEST, FATORCONVUND, IDPRJ, IDTRF, 
                                        VALORTOTALITEM, VALORCODIGOPRD, TIPOCODIGOPRD, QTDUNDPEDIDO, TRIBUTACAOECF, 
                                        CODFILIAL, CODDEPARTAMENTO, IDPRDCOMPOSTO, QUANTIDADESEPARADA, PERCENTCOMISSAO, INDICENCM, NCM, CODRPR, COMISSAOREPRES, 
                                        NSEQITMCNTMEDICAO, VALORESCRITURACAO, VALORFINPEDIDO, VALORFRETECTRC, VALOROPFRM1, VALOROPFRM2, 
                                        IDOBJOFICINA, PRECOEDITADO, QTDEVOLUMEUNITARIO, IDGRD, 
                                        CODVEN1, CODLOCALBN, REGISTROEXPORTACAO, DATARE, PRECOTOTALEDITADO, CST, 
                                        VALORDESCCONDICONALITM, VALORDESPCONDICIONALITM, DATAORCAMENTO, CODTBORCAMENTO, RATEIOFRETE, 
                                        RATEIOSEGURO, RATEIODESC, RATEIODESP, RATEIOEXTRA1, RATEIOEXTRA2, RATEIOFRETECTRC, 
                                        RATEIODEDMAT, RATEIODEDSUB, RATEIODEDOUT, IDCLASSIFENERGIACOMUNIC, VALORUNTORCAMENTO, 
                                        VALSERVICONFE, CODLOC, VALORBEM, VALORLIQUIDO, CODIGOCODIF, CODMUNSERVICO, CODETDMUNSERV, RATEIOCCUSTODEPTO, CUSTOREPOSICAO, 
                                        CUSTOREPOSICAOB, VALORFINTERCEIROS, VALORFINANCGERENCIAL, CODIGOSERVICO, VALORUNITGERENCIAL, 
                                        IDINTEGRACAO, IDTABPRECO, VALORBRUTOITEM, VALORBRUTOITEMORIG, CODCOLTBORCAMENTO, CODPUBLIC, QUANTIDADETOTAL, 
                                        PRODUTOSUBSTITUTO, CODTBGRUPOORC, PRECOUNITARIOSELEC, VALORRATEIOLAN, 
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?,?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, 
                                        ?, ?, ?, ?, ?,?, ?,?, ?,?,?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, 
                                        ?, ?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?,?, ?, ?, ?)";

                            sPoint = string.Concat("Inclusão: Item ", (i + 1), " Grava TITMMOV");
                            //DBS.QueryExec(sSql,
                            //    /*CODCOLIGADA*/ Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]),
                            //    /*IDMOV*/ movimento.IdMov,
                            //    /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                            //    /*NUMEROSEQUENCIAL*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                            //    /*IDPRD*/ Convert.ToInt32(Itens.Rows[i]["IDPRD"]),
                            //    /*CODTIP*/ null,
                            //    /*QUANTIDADE*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                            //    /*PRECOUNITARIO*/ Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                            //    /*PRECOTABELA*/ 0,
                            //    /*PERCENTUALDESC*/ null, /*VALORDESC*/ null, /*PERCENTUALDESP*/ null, /*VALORDESP*/ null, /*DATAEMISSAO*/ movimento.DataEmissao,
                            //    /*CODMEN*/ null, /*NUMEROTRIBUTOS*/ null, /*CODTB1FAT*/ null, /*CODTB2FAT*/ sCODTB2FAT, /*CODTB3FAT*/ sCODTB3FAT, /*CODTB4FAT:*/ sCODTB4FAT,
                            //    /*CODTB5FAT*/ null, /*CODTB1FLX*/ null, /*CODTB2FLX*/ null, /*CODTB3FLX*/ null, /*CODTB4FLX*/ null, /*CODTB5FLX*/ null, /*CAMPOLIVRE*/ null,
                            //    /*CODUND*/ Itens.Rows[i]["CODUND"].ToString(),
                            //    /*QUANTIDADEARECEBER*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                            //    /*CODNAT*/ null, /*CODCPG*/ null, /*DATAENTREGA*/ movimento.DataEntrega, /*PRATELEIRA*/ null, /*IDCNT*/ null, /*NSEQITMCNT*/ null,
                            //    /*DATAINIFAT*/ null, /*DATAFIMFAT*/ null, /*FLAGEFEITOSALDO*/ null, /*VALORUNITARIO*/ 0,  /*VALORFINANCEIRO*/ 0,
                            //    /*IMPRIMEMOV*/ null, /*CODCCUSTO*/ null, /*FLAGREPASSE*/ null, /*ALIQORDENACAO*/ 0,
                            //    /*QUANTIDADEORIGINAL*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                            //    /*IDNAT*/ null, /*FLAG*/ 0, /*CHAPA*/ null, /*INICIO*/ null, /*TERMINO*/ null, /*PREVINICIO*/ null, /*STATUS*/ null, /*BLOCK*/ null,
                            //    /*FLAGREFATURAMENTO*/ null, /*IDCNTDESTINO*/ null, /*NSEQITMCNTDEST*/ null, /*FATORCONVUND*/ 0, /*IDPRJ*/ null, /*IDTRF*/ null,
                            //    /*VALORTOTALITEM*/ 0, /*VALORCODIGOPRD*/ null, /*TIPOCODIGOPRD*/ null, /*QTDUNDPEDIDO*/ null, /*TRIBUTACAOECF*/ null,
                            //    /*CODFILIAL*/ movimento.CodFilial, /*CODDEPARTAMENTO*/ null,
                            //    /*IDPRDCOMPOSTO*/ (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]),
                            //    /*QUANTIDADESEPARADA*/ 0, /*PERCENTCOMISSAO*/ 0, /*INDICENCM*/ null, /*NCM*/ null, /*CODRPR*/ null, /*COMISSAOREPRES*/ 0,
                            //    /*NSEQITMCNTMEDICAO*/ null, /*VALORESCRITURACAO*/ 0, /*VALORFINPEDIDO*/ 0, /*VALORFRETECTRC*/ null, /*VALOROPFRM1*/ 0, /*VALOROPFRM2*/ 0,
                            //    /*IDOBJOFICINA*/ null, /*PRECOEDITADO*/ 0, /*QTDEVOLUMEUNITARIO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]), /*IDGRD*/ null,
                            //    /*CODVEN1*/ null, /*CODLOCALBN*/ null, /*REGISTROEXPORTACAO*/ null, /*DATARE*/ null, /*PRECOTOTALEDITADO*/ null, /*CST*/ null,
                            //    /*VALORDESCCONDICONALITM*/ 0, /*VALORDESPCONDICIONALITM*/ 0, /*DATAORCAMENTO*/ null, /*CODTBORCAMENTO*/ null, /*RATEIOFRETE*/ null,
                            //    /*RATEIOSEGURO*/ null, /*RATEIODESC*/ null, /*RATEIODESP*/ null, /*RATEIOEXTRA1*/ null, /*RATEIOEXTRA2*/ null, /*RATEIOFRETECTRC*/ null,
                            //    /*RATEIODEDMAT*/ null, /*RATEIODEDSUB*/ null, /*RATEIODEDOUT*/ null, /*IDCLASSIFENERGIACOMUNIC*/ null, /*VALORUNTORCAMENTO*/ 0,
                            //    /*VALSERVICONFE*/ 0, /*CODLOC*/ movimento.CodLoc, /*VALORBEM*/ 0,
                            //    /*VALORLIQUIDO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                            //    /*CODIGOCODIF*/ null, /*CODMUNSERVICO*/ null, /*CODETDMUNSERV*/ null, /*RATEIOCCUSTODEPTO*/ null, /*CUSTOREPOSICAO*/ null,
                            //    /*CUSTOREPOSICAOB*/ null, /*VALORFINTERCEIROS*/ null, /*VALORFINANCGERENCIAL*/ null, /*CODIGOSERVICO*/ null, /*VALORUNITGERENCIAL*/ null,
                            //    /*IDINTEGRACAO*/ null, /*IDTABPRECO*/ null,
                            //    /*VALORBRUTOITEM*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                            //    /*VALORBRUTOITEMORIG*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                            //    /*CODCOLTBORCAMENTO*/ null, /*CODPUBLIC*/ null, /*QUANTIDADETOTAL*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                            //    /*PRODUTOSUBSTITUTO*/ 0, /*CODTBGRUPOORC*/ null, /*PRECOUNITARIOSELEC*/ 0, /*VALORRATEIOLAN*/ null,
                            //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                            if (movimento.ValorFrete > 0)
                            {
                                valorTotalItem = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);

                                for (int j = 0; j < Itens.Rows.Count; j++)
                                {
                                    valorTotalMovimento += (Convert.ToDecimal(Itens.Rows[j]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[j]["PRECOUNITARIO"]));
                                }

                                //percentualRateioFrete = valorTotalItem / valorTotalMovimento;
                                //rateioFrete = percentualRateioFrete / Convert.ToDecimal(movimento.ValorFrete);

                                percentualRateioFrete = (valorTotalItem * 100) / valorTotalMovimento;
                                rateioFrete = (percentualRateioFrete * Convert.ToDecimal(movimento.ValorFrete)) / 100;

                                valorTotalMovimento = 0;
                            }

                            if (movimento.ValorDesc > 0)
                            {
                                valorTotalItem = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);

                                for (int j = 0; j < Itens.Rows.Count; j++)
                                {
                                    valorTotalMovimento += (Convert.ToDecimal(Itens.Rows[j]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[j]["PRECOUNITARIO"]));
                                }

                                percentualRateioDesc = valorTotalItem / valorTotalMovimento;
                                rateioDesc = percentualRateioDesc * Convert.ToDecimal(movimento.ValorDesc);

                                valorTotalMovimento = 0;
                            }

                            precoUnitario = Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                            precoUnitario = Math.Round(precoUnitario, 2);

                            quantidade = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]);
                            quantidade = Math.Round(quantidade, 2);

                            conn.ExecTransaction(sSql, new object[]{
                                /*CODCOLIGADA*/ Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]),
                                /*IDMOV*/ movimento.IdMov,
                                /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*NUMEROSEQUENCIAL*/ Convert.ToInt32(Itens.Rows[i]["NUMEROSEQUENCIAL"]),
                                /*IDPRD*/ Convert.ToInt32(Itens.Rows[i]["IDPRD"]),
                                /*CODTIP*/ null,
                                /*QUANTIDADE*/ quantidade,
                                /*PRECOUNITARIO*/ precoUnitario,
                                /*PRECOTABELA*/ 0,
                                /*PERCENTUALDESC*/ null, /*VALORDESC*/ null, /*PERCENTUALDESP*/ null, /*VALORDESP*/ null, /*DATAEMISSAO*/ movimento.DataEmissao,
                                /*CODMEN*/ null, /*NUMEROTRIBUTOS*/ null, /*CODTB1FAT*/ null, /*CODTB2FAT*/ sCODTB2FAT, /*CODTB3FAT*/ sCODTB3FAT, /*CODTB4FAT:*/ sCODTB4FAT,
                                /*CODTB5FAT*/ null, /*CODTB1FLX*/ null, /*CODTB2FLX*/ null, /*CODTB3FLX*/ null, /*CODTB4FLX*/ null, /*CODTB5FLX*/ null, /*CAMPOLIVRE*/ null,
                                /*CODUND*/ Itens.Rows[i]["CODUND"].ToString(),
                                /*QUANTIDADEARECEBER*/quantidade,
                                /*CODNAT*/ null, /*CODCPG*/ null, /*DATAENTREGA*/  Itens.Rows[i]["DATAENTREGA"]/*movimento.DataEntrega*/, /*PRATELEIRA*/ null, /*IDCNT*/ null, /*NSEQITMCNT*/ null,
                                /*DATAINIFAT*/ null, /*DATAFIMFAT*/ null, /*FLAGEFEITOSALDO*/ null, /*VALORUNITARIO*/ 0,  /*VALORFINANCEIRO*/ 0,
                                /*IMPRIMEMOV*/ null, /*CODCCUSTO*/ null, /*FLAGREPASSE*/ null, /*ALIQORDENACAO*/ 0,
                                /*QUANTIDADEORIGINAL*/ quantidade,
                                /*IDNAT*/ Itens.Rows[i]["IDNAT"].ToString(), /*FLAG*/ 0, /*CHAPA*/ null, /*INICIO*/ null, /*TERMINO*/ null, /*PREVINICIO*/ null, /*STATUS*/ null, /*BLOCK*/ null,
                                /*FLAGREFATURAMENTO*/ null, /*IDCNTDESTINO*/ null, /*NSEQITMCNTDEST*/ null, /*FATORCONVUND*/ 0, /*IDPRJ*/ null, /*IDTRF*/ null,
                                /*VALORTOTALITEM*/ (quantidade * precoUnitario), /*VALORCODIGOPRD*/ null, /*TIPOCODIGOPRD*/ null, /*QTDUNDPEDIDO*/ null, /*TRIBUTACAOECF*/ null,
                                /*CODFILIAL*/ movimento.CodFilial, /*CODDEPARTAMENTO*/ null,
                                /*IDPRDCOMPOSTO*/ (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]),
                                /*QUANTIDADESEPARADA*/ 0, /*PERCENTCOMISSAO*/ 0, /*INDICENCM*/ null, /*NCM*/ null, /*CODRPR*/ null, /*COMISSAOREPRES*/ 0,
                                /*NSEQITMCNTMEDICAO*/ null, /*VALORESCRITURACAO*/ 0, /*VALORFINPEDIDO*/ 0, /*VALORFRETECTRC*/ null, /*VALOROPFRM1*/ 0, /*VALOROPFRM2*/ 0,
                                /*IDOBJOFICINA*/ null, /*PRECOEDITADO*/ 0, /*QTDEVOLUMEUNITARIO*/ 0, /*IDGRD*/ null,
                                /*CODVEN1*/ movimento.CodVen1, /*CODLOCALBN*/ null, /*REGISTROEXPORTACAO*/ null, /*DATARE*/ null, /*PRECOTOTALEDITADO*/ null, /*CST*/ null,
                                /*VALORDESCCONDICONALITM*/ 0, /*VALORDESPCONDICIONALITM*/ 0, /*DATAORCAMENTO*/ null, /*CODTBORCAMENTO*/ null, /*RATEIOFRETE*/ rateioFrete,
                                /*RATEIOSEGURO*/ null, /*RATEIODESC*/ rateioDesc, /*RATEIODESP*/ (percentItem*movimento.ValorDesp), /*RATEIOEXTRA1*/ null, /*RATEIOEXTRA2*/ null, /*RATEIOFRETECTRC*/ null,
                                /*RATEIODEDMAT*/ null, /*RATEIODEDSUB*/ null, /*RATEIODEDOUT*/ null, /*IDCLASSIFENERGIACOMUNIC*/ null, /*VALORUNTORCAMENTO*/ 0,
                                /*VALSERVICONFE*/ 0, /*CODLOC*/ movimento.CodLoc, /*VALORBEM*/ 0,
                                /*VALORLIQUIDO*/ (quantidade * precoUnitario),
                                /*CODIGOCODIF*/ null, /*CODMUNSERVICO*/ null, /*CODETDMUNSERV*/ null, /*RATEIOCCUSTODEPTO*/ null, /*CUSTOREPOSICAO*/ null,
                                /*CUSTOREPOSICAOB*/ null, /*VALORFINTERCEIROS*/ null, /*VALORFINANCGERENCIAL*/ null, /*CODIGOSERVICO*/ null, /*VALORUNITGERENCIAL*/ null,
                                /*IDINTEGRACAO*/ null, /*IDTABPRECO*/ null,
                                /*VALORBRUTOITEM*/ (quantidade * precoUnitario),
                                /*VALORBRUTOITEMORIG*/ (quantidade * precoUnitario),
                                /*CODCOLTBORCAMENTO*/ null, /*CODPUBLIC*/ null, /*QUANTIDADETOTAL*/ quantidade,
                                /*PRODUTOSUBSTITUTO*/ 0, /*CODTBGRUPOORC*/ null, /*PRECOUNITARIOSELEC*/ Itens.Rows[i]["PRECOUNITARIOSELEC"].ToString(), /*VALORRATEIOLAN*/ null,
                                /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ conn.GetDateTime()});

                            //                            sSql = @"INSERT INTO TITMMOVHISTORICO (CODCOLIGADA, IDMOV, NSEQITMMOV, HISTORICOCURTO, HISTORICOLONGO, 
                            //                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                            //                                VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :HISTORICOCURTO, :HISTORICOLONGO, 
                            //                                    :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                            sSql = @"INSERT INTO TITMMOVHISTORICO (CODCOLIGADA, IDMOV, NSEQITMMOV, HISTORICOCURTO, HISTORICOLONGO, 
                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                VALUES (?, ?, ?, ?, ?,?, ?, ?, ?)";


                            sPoint = string.Concat("Inclusão: Item ", (i + 1), " Grava TITMMOVHISTORICO");
                            //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                            //    /*HISTORICOCURTO*/ null, /*HISTORICOLONGO*/ Itens.Rows[i]["HISTORICOLONGO"].ToString(),
                            //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                            conn.ExecTransaction(sSql, new object[]{ /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*HISTORICOCURTO*/ null, /*HISTORICOLONGO*/ Itens.Rows[i]["HISTORICOLONGO"].ToString(),
                                /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ conn.GetDateTime()});


                            //                            sSql = @"INSERT INTO TITMMOVCOMPL (CODCOLIGADA, IDMOV, NSEQITMMOV, PRODPRICIPAL, SEQUENCIAL, TIPOMAT, XPED,
                            //                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, PRDPADRAO, OBSPRDPADRAO)
                            //                                VALUES(:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :PRODPRICIPAL, :SEQUENCIAL, :TIPOMAT, :XPED,
                            //                                    :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :PRDPADRAO, :OBSPRDPADRAO)";

                            sSql = @"INSERT INTO TITMMOVCOMPL (CODCOLIGADA, IDMOV, NSEQITMMOV, XPED,
                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, APLICPROD, TIPO, VALORPINTURA, TIPOPINTURA, VALORDESCORIGINAL, CORPINTURA, PRECOTABELA)
                                VALUES(?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?)";

                            sPoint = string.Concat("Inclusão: Item ", (i + 1), " Grava TITMMOVCOMPL");
                            //DBS.QueryExec(sSql,
                            //    /*CODCOLIGADA*/ movimento.CodColigada,
                            //    /*IDMOV*/ movimento.IdMov,
                            //    /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                            //    /*PRODPRICIPAL*/ Itens.Rows[i]["PRODPRICIPAL"].ToString(),
                            //    /*SEQUENCIAL*/ Itens.Rows[i]["SEQUENCIAL"].ToString(),
                            //    /*TIPOMAT*/ Itens.Rows[i]["TIPOMAT"].ToString(), 
                            //    /*XPED*/ movimento.NumeroOrdem,
                            //    /*RECCREATEDBY*/ movimento.CodUsuario,
                            //    /*RECCREATEDON*/ DBS.ServerDate(),
                            //    /*RECMODIFIEDBY*/ movimento.CodUsuario,
                            //    /*RECMODIFIEDON*/ DBS.ServerDate(),
                            //    /*PRDPADRAO*/ Itens.Rows[i]["PRDPADRAO"].ToString(),
                            //    /*OBSPRDPADRAO*/  Itens.Rows[i]["OBSPRDPADRAO"].ToString());

                            conn.ExecTransaction(sSql, new object[]{
                                /*CODCOLIGADA*/ movimento.CodColigada,
                                /*IDMOV*/ movimento.IdMov,
                                /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*XPED*/ movimento.NumeroOrdem,
                                /*RECCREATEDBY*/ movimento.CodUsuario,
                                /*RECCREATEDON*/ conn.GetDateTime(),
                                /*RECMODIFIEDBY*/ movimento.CodUsuario,
                                /*RECMODIFIEDON*/ conn.GetDateTime(),
                                /*APLICPROD*/ Itens.Rows[i]["APLICPROD"].ToString(),
                                /*TIPO*/ Itens.Rows[i]["TIPO"].ToString(),
                                /*VALORPINTURA*/ Itens.Rows[i]["VALORPINTURA"].ToString().Replace(',','.'),
                                /*TIPOPINTURA*/ Itens.Rows[i]["TIPOPINTURA"].ToString(),
                                /*VALORDESCORIGINAL*/ Itens.Rows[i]["VALORORIGINAL"].ToString().Replace(',','.'),
                                /*CORPINTURA*/ Itens.Rows[i]["CORPINTURA"].ToString(),
                                /*PRECOTABELA*/ Convert.ToDecimal(Itens.Rows[i]["PRECOTABELA"])});
                                

                            //                            sSql = @"INSERT INTO TTRBMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, CODTRB, BASEDECALCULO, ALIQUOTA, VALOR, FATORREDUCAO, FATORSUBSTTRIB, 
                            //                                        BASEDECALCULOCALCULADA, EDITADO, VLRISENTO, CODRETENCAO, TIPORECOLHIMENTO, CODTRBBASE, PERCDIFERIMENTOPARCIALICMS, 
                            //                                        SITTRIBUTARIA, MODALIDADEBC, ALIQUOTAPORVALOR, CODUNDREFERENCIA, BASECHEIA, 
                            //                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, TIPORENDIMENTO, FORMATRIBUTACAO) 
                            //                                    VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :CODTRB, :BASEDECALCULO, :ALIQUOTA, :VALOR, :FATORREDUCAO, :FATORSUBSTTRIB, 
                            //                                        :BASEDECALCULOCALCULADA, :EDITADO, :VLRISENTO, :CODRETENCAO, :TIPORECOLHIMENTO, :CODTRBBASE, :PERCDIFERIMENTOPARCIALICMS, 
                            //                                        :SITTRIBUTARIA, :MODALIDADEBC, :ALIQUOTAPORVALOR, :CODUNDREFERENCIA, :BASECHEIA, 
                            //                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :TIPORENDIMENTO, :FORMATRIBUTACAO)";

                            sSql = @"insert into TITMMOVFISCAL (CODCOLIGADA, IDMOV, NSEQITMMOV, QTDECONTRATADA, RECCREATEDBY, RECCREATEDON, NUMPEDIDO, NUMITEMPEDIDO)
                                                                values (/*CODCOLIGADA*/ ?, 
                                                                        /*IDMOV*/ ?, 
                                                                        /*NSEQITMMOV*/ ?, 
                                                                        /*QTDECONTRATADA*/ 0.0, 
                                                                        /*RECCREATEDBY*/ ?, 
                                                                        /*RECCREATEDON*/ ?, 
                                                                        /*NUMPEDIDO*/ ?, 
                                                                        /*NUMITEMPEDIDO*/ ?)";

                            string nIPedido = Itens.Rows[i]["NUMITEMPEDIDO"].ToString();

                            Object NIP;

                            if (String.IsNullOrWhiteSpace(nIPedido))
                            {
                                NIP = DBNull.Value;
                            }
                            else
                            {
                                NIP = nIPedido;
                            }

                            conn.ExecTransaction(sSql, new object[]{
                                /*CODCOLIGADA*/ movimento.CodColigada,
                                /*IDMOV*/ movimento.IdMov,
                                /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*RECCREATEDBY*/ movimento.CodUsuario,
                                /*RECCREATEDON*/ conn.GetDateTime(),
                                /*NUMPEDIDO*/ Itens.Rows[i]["NUMPEDIDO"].ToString(),
                                /*NUMITEMPEDIDO*/ NIP});

                            sSql = @"INSERT INTO TTRBMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, CODTRB, BASEDECALCULO, ALIQUOTA, VALOR, FATORREDUCAO, FATORSUBSTTRIB, 
                                        BASEDECALCULOCALCULADA, EDITADO, VLRISENTO, CODRETENCAO, TIPORECOLHIMENTO, CODTRBBASE, PERCDIFERIMENTOPARCIALICMS, 
                                        SITTRIBUTARIA, MODALIDADEBC, ALIQUOTAPORVALOR, CODUNDREFERENCIA, BASECHEIA, 
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, TIPORENDIMENTO, FORMATRIBUTACAO) 
                                    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?)";


                        #region Calculo do Trbiuto

                            Decimal BaseCalculo = 0;
                            Decimal Aliquota = 0;
                            Decimal ValorTributo = 0;
                            DataTable dtTributo = new DataTable();
                            Tributo IPI = new Tributo();
                            Tributo PIS = new Tributo();
                            Tributo COFINS = new Tributo();
                            Tributo ICMS = new Tributo();
                            Tributo ICMSST = new Tributo();                        
                            List<Tributo> Tributos = new List<Tributo>();

                            sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de IPI");
                            #region Calculo do Trbiuto IPI

                            Aliquota = Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]);

                            if (Itens.Rows[i]["IDPRDCOMPOSTO"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]) == 0)
                                {
                                    BaseCalculo = (Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"])) - rateioDesc + rateioFrete;
                                    ValorTributo = new Util().Arredonda((BaseCalculo * Aliquota) / 100);
                                }
                            }
                            else
                            {
                                BaseCalculo = (Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"])) - rateioDesc + rateioFrete;
                                ValorTributo = new Util().Arredonda((BaseCalculo * Aliquota) / 100);
                            }

                            IPI.TipoTributo = "IPI";
                            IPI.BaseDoCalculo = BaseCalculo;
                            IPI.ValorDaAliquota = Aliquota;
                            IPI.ValorDoTributo = ValorTributo;

                            IPI.BaseDoCalculo = Math.Round(IPI.BaseDoCalculo, 4);
                            IPI.ValorDoTributo = Math.Round(IPI.ValorDoTributo, 4);

                            Tributos.Add(IPI);

                            #endregion

                            sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de PIS");
                            #region Calculo do Trbiuto PIS
                            sql = String.Format(@"select  (isnull(TITMMOV.VALORTOTALITEM, 0) - 
		                                                        isnull(TITMMOV.RATEIODESC, 0) + 
		                                                        isnull(TITMMOV.RATEIODESP, 0) + 
		                                                        isnull(TITMMOV. RATEIOFRETE, 0)) as 'BASECALCULO',
		                                                        DTRBNATUREZA.ALIQTRB as 'ALIQUOTA',
		                                                        NSEQITMMOV 

                                                        from TITMMOV (NOLOCK)

                                                        inner join DTRBNATUREZA (NOLOCK)
                                                        on DTRBNATUREZA.IDNAT = TITMMOV.IDNAT
                                                        and DTRBNATUREZA.CODCOLIGADA = TITMMOV.CODCOLIGADA

                                                        where TITMMOV.CODCOLIGADA = '{0}'
                                                        and IDMOV = '{1}'
                                                        and TITMMOV.NSEQITMMOV = '{2}'
                                                        and DTRBNATUREZA.CODTRB = 'PIS'", AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                PIS.TipoTributo = "PIS";
                                PIS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString());
                                PIS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString());
                                PIS.ValorDoTributo = PIS.BaseDoCalculo * (PIS.ValorDaAliquota / 100);

                                //PIS.BaseDoCalculo = Math.Round(PIS.BaseDoCalculo, 4);
                                //PIS.ValorDoTributo = Math.Round(PIS.ValorDoTributo, 4);
                            }

                            Tributos.Add(PIS);


                            #endregion

                            sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de COFINS");
                            #region Calculo do Trbiuto COFINS
                            sql = String.Format(@"select  (isnull(TITMMOV.VALORTOTALITEM, 0) - 
		                                                        isnull(TITMMOV.RATEIODESC, 0) + 
		                                                        isnull(TITMMOV.RATEIODESP, 0) + 
		                                                        isnull(TITMMOV. RATEIOFRETE, 0)) as 'BASECALCULO',
		                                                        DTRBNATUREZA.ALIQTRB as 'ALIQUOTA',
		                                                        NSEQITMMOV 

                                                        from TITMMOV (NOLOCK)

                                                        inner join DTRBNATUREZA (NOLOCK)
                                                        on DTRBNATUREZA.IDNAT = TITMMOV.IDNAT
                                                        and DTRBNATUREZA.CODCOLIGADA = TITMMOV.CODCOLIGADA

                                                        where TITMMOV.CODCOLIGADA = '{0}'
                                                        and IDMOV = '{1}'
                                                        and TITMMOV.NSEQITMMOV = '{2}'
                                                        and DTRBNATUREZA.CODTRB = 'COFINS'", AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                COFINS.TipoTributo = "COFINS";
                                COFINS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString());
                                COFINS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString());
                                COFINS.ValorDoTributo = COFINS.BaseDoCalculo * (COFINS.ValorDaAliquota / 100);

                                //COFINS.BaseDoCalculo = Math.Round(COFINS.BaseDoCalculo, 4);
                                //COFINS.ValorDoTributo = Math.Round(COFINS.ValorDoTributo, 4);
                            }

                            Tributos.Add(COFINS);


                            #endregion

                            sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de ICMS");
                            #region Calculo do Trbiuto ICMS

                            sql = String.Format(@"SELECT FATORICMS FROM DREGRAICMS (NOLOCK) WHERE IDREGRAICMS IN (
		                                                SELECT IDREGRAICMS FROM DCFOP (NOLOCK) WHERE CODCOLIGADA = 1 AND IDNAT = '{0}')
		                                                AND CODCOLIGADA = 1", Itens.Rows[i]["IDNAT"].ToString());

                            sql = String.Format(@"select CASE 
	
	                                                                WHEN 

		                                                                (SELECT FATORICMS FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
		                                                                AND CODCOLIGADA = 1) > 0 
		
		                                                                AND 
		
		                                                                (SELECT BASEICMSCOMIPI FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
		                                                                AND CODCOLIGADA = 1) = 1
		
	                                                                THEN
		                                                                isnull((
		                                                                (TITMMOV.VALORTOTALITEM + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) 
		                                                                - 
		                                                                ((TITMMOV.VALORTOTALITEM + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * '{1}' /100 )
		                                                                ),0)
		
	                                                                ELSE
		
		                                                                CASE
		
		                                                                WHEN
		
			                                                                (SELECT BASEICMSCOMIPI FROM DREGRAICMS WHERE IDREGRAICMS IN (
			                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
			                                                                AND CODCOLIGADA = 1) = 1		
		
		                                                                THEN
			
			                                                                isnull(((TITMMOV.VALORTOTALITEM +  '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE)),0)
		
		                                                                ELSE
		
			                                                                isnull(((TITMMOV.VALORTOTALITEM - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE)),0)

		                                                                END
                                                                END as 'BASECALCULO',
                                                                (SELECT ALIQICMS FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                    SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
                                                                    AND CODCOLIGADA = 1) as 'ALIQUOTA'

                                                                from TITMMOV (NOLOCK)

                                                                where CODCOLIGADA = '{2}'
                                                                and IDMOV = '{3}'
                                                                and NSEQITMMOV = '{4}'", IPI.ValorDoTributo.ToString().Replace(",", "."), MetodosSQL.GetField(sql, "FATORICMS").Replace(",", "."), AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]), Itens.Rows[i]["IDNAT"].ToString());

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                ICMS.TipoTributo = "ICMS";
                                ICMS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString());
                                ICMS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString());
                                ICMS.ValorDoTributo = new Util().Arredonda(ICMS.BaseDoCalculo * (ICMS.ValorDaAliquota / 100));

                                //ICMS.BaseDoCalculo = Math.Round(ICMS.BaseDoCalculo, 4);
                                //ICMS.ValorDoTributo = Math.Round(ICMS.ValorDoTributo, 4);
                            }

                            Tributos.Add(ICMS);

                            #endregion

                            sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de ICMSST");
                            #region Calculo do Trbiuto ICMSST

                            sql = String.Format(@"SELECT FATORREDST, ALIQSUBST, FATORSUBST FROM DREGRAICMS WHERE IDREGRAICMS IN (
                                                    SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{0}')
                                                    AND CODCOLIGADA = 1", Itens.Rows[i]["IDNAT"].ToString());

                            dtTributo = MetodosSQL.GetDT(sql);


                            #region Query ICMS-ST
                            foreach (DataRow row in dtTributo.Rows)
                            {
                                sql = String.Format(@"SELECT
CASE 
WHEN

(SELECT NCONTRIB 
	FROM TMOVCOMPL (nolock)
	WHERE CODCOLIGADA = 1 AND IDMOV = {5} /*IDMOV*/) = 0

THEN 
	
	0
   
ELSE 
			CASE 
			WHEN
				(SELECT 1
				FROM   DCFOP,
					   DREGRAICMS
				WHERE  DCFOP.CODCOLIGADA = DREGRAICMS.CODCOLIGADA
					   AND DCFOP.IDREGRAICMS = DREGRAICMS.IDREGRAICMS
					   AND DREGRAICMS.TIPOOPERACAOTRIBUTARIA IN ( 1, 2 )
					   AND DREGRAICMS.UTIBCDIFALICMSDENTRO IN ( 1, 2 )
					   AND DCFOP.CODCOLIGADA = 1 /*COLIGADA*/
					   AND DCFOP.IDNAT = {6})=1

             THEN (
					CASE
					WHEN 
						(SELECT 1
						  FROM DCFOP, DREGRAICMS
						 WHERE DCFOP.CODCOLIGADA = DREGRAICMS.CODCOLIGADA
						   AND DCFOP.IDREGRAICMS = DREGRAICMS.IDREGRAICMS
						   AND DREGRAICMS.TIPOOPERACAOTRIBUTARIA IN (1,2)
						   AND DREGRAICMS.UTIBCDIFALICMSDENTRO = 1
						   AND DCFOP.CODCOLIGADA = 1 /*COLIGADA*/
						   AND DCFOP.IDNAT = {6})=1       

					THEN 
							CASE
							WHEN 
							(SELECT FATORREDST FROM DREGRAICMS WHERE IDREGRAICMS IN (
							SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 /*COLIGADA*/ AND IDNAT = {6})
							AND CODCOLIGADA = 1 /*COLIGADA*/) > 0                

							THEN 
							/*
								(((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) -   
								((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORREDST' , 'V')/100) ))) +    
								((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) -   
								((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * TABNATITEM ('FATORREDST' , 'V')/100 ))) * (TABNATITEM ('FATORSUBST' , 'V')/100))) - LVL('ICMS') ) / (1 - ((TABNATITEM ('ALIQSUBST' , 'V'))/100))                
							*/
								((((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
								((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({3} /100) ))) +    
								((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
								((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * {3} /100 ))) * ({2} /100))) - {1} ) / (1 - (({4} )/100)))

							ELSE 
							/*
								 (((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) +     
								 ((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORSUBST' , 'V')/100)))  - LVL('ICMS') ) / (1 - ((TABNATITEM ('ALIQSUBST' , 'V'))/100))            
							*/
								 ((((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) +    
								 ((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({2} /100)))  - {1} ) / (1 - (({4})/100)))

							
							
							END       

					ELSE (
								CASE
								WHEN 
								(SELECT FATORREDST FROM DREGRAICMS WHERE IDREGRAICMS IN (
								SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 /*COLIGADA*/ AND IDNAT = {6})
								AND CODCOLIGADA = 1 /*COLIGADA*/)>0

								THEN 
								/*
									(((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) - 
									((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORREDST' , 'V')/100) ))) +
									((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) - 
									((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * TABNATITEM ('FATORREDST' , 'V')/100 ))) * (TABNATITEM ('FATORSUBST' , 'V')/100))) - LVL('ICMS') ) / (1 - ((TABNATITEM ('ALIQSUBST' , 'V'))/100))
								*/

									((((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
									((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({3} /100) ))) +    
									((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
									((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * {3} /100 ))) * ({2} /100))) - {1} ) / (1 - (({4} )/100)))



								ELSE 
								/*
									(((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) + 
									((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORSUBST' , 'V')/100)))  - LVL('ICMS') ) / (1 - ((TABNATITEM ('ALIQSUBST' , 'V'))/100))
								*/
									((((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) +    
									((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({2} /100)))  - {1} ) / (1 - (({4})/100)))


								END)    

					END
)

				ELSE 
						CASE
						WHEN 
						(SELECT FATORREDST FROM DREGRAICMS WHERE IDREGRAICMS IN (
						SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 /*COLIGADA*/ AND IDNAT = {6})
						AND CODCOLIGADA = 1 /*COLIGADA*/)>0

                       THEN 
					   /*
							(((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) - 
							((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORREDST' , 'V')/100) ))) +
							((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) - 
							((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * TABNATITEM ('FATORREDST' , 'V')/100 ))) * (TABNATITEM ('FATORSUBST' , 'V')/100))
						*/

							((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
							((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({3} /100) ))) +    
							((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
							((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * {3} /100 ))) * ({2} /100)))


                       ELSE 
					   /*
							(TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) + 
							((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORSUBST' , 'V')/100))
						*/

						((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) +    
						((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({2} /100)))

					   END

				END

END
AS'BASECALCULO'
FROM TITMMOV (NOLOCK) WHERE CODCOLIGADA = 1 /*COLIGADA*/ AND IDMOV = {5} AND NSEQITMMOV = {7}", /*VALORIPI*/ IPI.ValorDoTributo.ToString().Replace(",", "."),
                                       /*VALORICMS*/ ICMS.ValorDoTributo.ToString().Replace(",", "."),
                                       /*FATORSUBST*/ row["FATORSUBST"].ToString().Replace(",", "."),
                                       /*FATORREDST*/ row["FATORREDST"].ToString().Replace(",", "."),
                                       /*ALIQSUBST*/ row["ALIQSUBST"].ToString().Replace(",", "."),
                                       /*IDMOV*/ movimento.IdMov,
                                       /*IDNAT*/ Convert.ToInt32(Itens.Rows[i]["IDNAT"]),
                                       /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                                ICMSST.ValorDaAliquota = Convert.ToDecimal(row["ALIQSUBST"].ToString());
                            }
                            #endregion

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                ICMSST.TipoTributo = "ICMSST";
                                ICMSST.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString());
                                ICMSST.ValorDoTributo = new Util().Arredonda(ICMSST.BaseDoCalculo * (ICMSST.ValorDaAliquota / 100));

                                //ICMSST.BaseDoCalculo = Math.Round(ICMSST.BaseDoCalculo, 4);
                                //ICMSST.ValorDoTributo = Math.Round(ICMSST.ValorDoTributo, 4);
                            }

                            Tributos.Add(ICMSST);

                            #endregion

                            sPoint = string.Concat("Alteração: Item ", (i + 1), " Grava TTRBMOV");
                            //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                            //    /*CODTRB*/ "IPI", /*BASEDECALCULO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                            //    /*ALIQUOTA*/ Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]),
                            //    /*VALOR*/ ValorTributo, /*FATORREDUCAO*/ 0, /*FATORSUBSTTRIB*/ 0,
                            //    /*BASEDECALCULOCALCULADA*/ 0, /*EDITADO*/ 0, /*VLRISENTO*/ null, /*CODRETENCAO*/ "0561", /*TIPORECOLHIMENTO*/ null, /*CODTRBBASE*/ null,
                            //    /*PERCDIFERIMENTOPARCIALICMS*/ 0, /*SITTRIBUTARIA*/ null, /*MODALIDADEBC*/ null, /*ALIQUOTAPORVALOR*/ null, /*CODUNDREFERENCIA*/ null,
                            //    /*BASECHEIA*/ 0,
                            //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(),
                            //    /*TIPORENDIMENTO*/ null, /*FORMATRIBUTACAO*/ null);

                            //List<string> tributos = new List<string>();
                            //tributos.Add("IPI");
                            //tributos.Add("ICM-SF");
                            //tributos.Add("PIS-SF");
                            //tributos.Add("COF-SF");
                            //tributos.Add("ICMS");
                            //tributos.Add("ICMSST");

                            if (movimento.SEMIMPOSTOS == 0)
                            {
                                foreach (Tributo t in Tributos)
                                {
                                    t.ValorDoTributo = Math.Round(t.ValorDoTributo, 2);
                                    t.BaseDoCalculo = Math.Round(t.BaseDoCalculo, 2);

                                    conn.ExecTransaction(sSql, new object[]{ /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                    /*CODTRB*/ t.TipoTributo, /*BASEDECALCULO*/ t.BaseDoCalculo,
                                    /*ALIQUOTA*/ t.ValorDaAliquota,
                                    /*VALOR*/ t.ValorDoTributo, /*FATORREDUCAO*/ 0, /*FATORSUBSTTRIB*/ 0,
                                    /*BASEDECALCULOCALCULADA*/ 0, /*EDITADO*/ 0, /*VLRISENTO*/ null, /*CODRETENCAO*/ null, /*TIPORECOLHIMENTO*/ null, /*CODTRBBASE*/ null,
                                    /*PERCDIFERIMENTOPARCIALICMS*/ 0, /*SITTRIBUTARIA*/ null, /*MODALIDADEBC*/ null, /*ALIQUOTAPORVALOR*/ null, /*CODUNDREFERENCIA*/ null,
                                    /*BASECHEIA*/ 0,
                                    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ conn.GetDateTime(),
                                    /*TIPORENDIMENTO*/ null, /*FORMATRIBUTACAO*/ null});
                                }
                            }

                        }

                        #endregion

                        #region Valor Bruto do Movimento

                        sPoint = string.Concat("Inclusão: [Valor Bruto do Movimento]");
                        sSql = @"SELECT 
SUM(VALORTOTALITEM)
FROM TITMMOV
WHERE CODCOLIGADA = ?
AND IDMOV = ?";

                        movimento.ValorBruto = Convert.ToDecimal(conn.ExecGetField(0, sSql, movimento.CodColigada, movimento.IdMov));

                        #endregion

                        #region Despesa
                        sPoint = string.Concat("Inclusão: [Despesa]");
                        if (movimento.ValorDesp != null)
                        {
                            if (movimento.ValorDesp != 0)
                            {
                                movimento.PercentualDesp = new Util().Arredonda(((movimento.ValorDesp / movimento.ValorBruto) * 100));
                            }
                            else
                            {
                                if (movimento.PercentualDesp != null)
                                {
                                    if (movimento.PercentualDesp != 0)
                                    {
                                        movimento.ValorDesp = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualDesp) / 100));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (movimento.PercentualDesp != null)
                            {
                                if (movimento.PercentualDesp != 0)
                                {
                                    movimento.ValorDesp = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualDesp) / 100));
                                }
                            }
                        }
                        #endregion

                        #region Seguro
                        sPoint = string.Concat("Inclusão: [Seguro]");
                        if (movimento.ValorSeguro != null)
                        {
                            if (movimento.ValorSeguro != 0)
                            {
                                movimento.PercentualSeguro = new Util().Arredonda(((movimento.ValorSeguro / movimento.ValorBruto) * 100));
                            }
                            else
                            {
                                if (movimento.PercentualSeguro != null)
                                {
                                    if (movimento.PercentualSeguro != 0)
                                    {
                                        movimento.ValorSeguro = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualSeguro) / 100));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (movimento.PercentualSeguro != null)
                            {
                                if (movimento.PercentualSeguro != 0)
                                {
                                    movimento.ValorSeguro = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualSeguro) / 100));
                                }
                            }
                        }
                        #endregion

                        #region Frete
                        sPoint = string.Concat("Inclusão: [Frete]");
                        if (movimento.ValorFrete != null)
                        {
                            if (movimento.ValorFrete != 0)
                            {
                                movimento.PercentualFrete = new Util().Arredonda(((movimento.ValorFrete / movimento.ValorBruto) * 100));
                            }
                            else
                            {
                                if (movimento.PercentualFrete != null)
                                {
                                    if (movimento.PercentualFrete != 0)
                                    {
                                        movimento.ValorFrete = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualFrete) / 100));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (movimento.PercentualFrete != null)
                            {
                                if (movimento.PercentualFrete != 0)
                                {
                                    movimento.ValorFrete = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualFrete) / 100));
                                }
                            }
                        }
                        #endregion

                        #region Desconto
                        sPoint = string.Concat("Inclusão: [Desconto]");
                        if (movimento.ValorDesc != null)
                        {
                            if (movimento.ValorDesc != 0)
                            {
                                //movimento.PercentualDesc = new Util().Arredonda(((movimento.ValorDesc / movimento.ValorBruto) * 100));
                                movimento.PercentualDesc = ((movimento.ValorDesc / movimento.ValorBruto) * 100);
                            }
                            else
                            {
                                if (movimento.PercentualDesc != null)
                                {
                                    if (movimento.PercentualDesc != 0)
                                    {
                                        //movimento.ValorDesc = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualDesc) / 100));
                                        movimento.ValorDesc = ((movimento.ValorBruto * movimento.PercentualDesc) / 100);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (movimento.PercentualDesc != null)
                            {
                                if (movimento.PercentualDesc != 0)
                                {
                                    movimento.ValorDesc = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualFrete) / 100));
                                }
                            }
                        }
                        #endregion

                        #region Extra1
                        sPoint = string.Concat("Inclusão: [Extra1]");
                        if (movimento.ValorExtra1 != null)
                        {
                            if (movimento.ValorExtra1 != 0)
                            {
                                movimento.PercentualExtra1 = new Util().Arredonda(((movimento.ValorExtra1 / movimento.ValorBruto) * 100));
                            }
                            else
                            {
                                if (movimento.PercentualExtra1 != null)
                                {
                                    if (movimento.PercentualExtra1 != 0)
                                    {
                                        movimento.ValorExtra1 = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualExtra1) / 100));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (movimento.PercentualExtra1 != null)
                            {
                                if (movimento.PercentualExtra1 != 0)
                                {
                                    movimento.ValorExtra1 = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualExtra1) / 100));
                                }
                            }
                        }
                        #endregion

                        #region Valor Outros do Movimento
                        sPoint = string.Concat("Inclusão: [Valor Outros do Movimento]");

                        movimento.ValorOutros = movimento.ValorBruto;

                        #endregion

                        #region Valor Liquido do Movimento
                        sPoint = string.Concat("Inclusão: [Valor Liquido do Movimento]");
                        //movimento.ValorLiquido = movimento.ValorBruto + Convert.ToDecimal(movimento.ValorDesp) + Convert.ToDecimal(movimento.ValorSeguro) - Convert.ToDecimal(movimento.ValorDesc) + Convert.ToDecimal(movimento.ValorFrete) - Convert.ToDecimal(movimento.ValorExtra1);
                        sSql = @"select ROUND((((VALOR_BRUTO + VALORDESP + IPI - VALORDESC + VALORFRETE)) +  ICMSST),2) as 'LIQUIDO' from (
SELECT	
                                       
--(((VALOR BRUTO + VALOR DESPESA + VALOR IPI - VALOR DESCONTO + VALOR FRETE)) +  VALOR ICMSST)
                                        ISNULL((SELECT	
                                        SUM(ROUND(VALORTOTALITEM,2))
                                        FROM TITMMOV
                                        WHERE CODCOLIGADA = TM.CODCOLIGADA
                                        AND IDMOV = TM.IDMOV),0) VALOR_BRUTO,

	                                    TM.VALORDESP,

                                        ISNULL((SELECT
                                        SUM(ROUND(VALOR,2))
                                        FROM	TTRBMOV
                                        WHERE	TTRBMOV.CODCOLIGADA = TM.CODCOLIGADA
                                        AND	TTRBMOV.IDMOV = TM.IDMOV
                                        AND	TTRBMOV.CODTRB = 'IPI'),0) IPI,

										TM.VALORDESC,

	                                    TM.VALORFRETE,

                                        (SELECT 
	                                                                                    CASE 
		                                                                                    WHEN (ISNULL(ROUND(ST.VALOR,2), 0) - ISNULL(ROUND(ICMS.VALOR,2), 0)) < 0 
		                                                                                    THEN 0 
		                                                                                    ELSE (ISNULL(ROUND(ST.VALOR,2), 0) - ISNULL(ROUND(ICMS.VALOR,2), 0)) 
	                                                                                    END as 'ICMSST'
                                                                                    FROM TMOV,

                                                                                    (SELECT	SUM (ROUND(TTRBMOV.VALOR,2)) AS VALOR
                                                                                         FROM	TTRBMOV
                                                                                         WHERE	TTRBMOV.CODCOLIGADA = TM.CODCOLIGADA
                                                                                         AND	TTRBMOV.IDMOV = TM.IDMOV
                                                                                         AND	TTRBMOV.CODTRB = 'ICMSST') ST,

                                                                                    (SELECT	SUM (ROUND(TTRBMOV.VALOR,2)) AS VALOR
                                                                                         FROM	TTRBMOV
                                                                                         WHERE	TTRBMOV.CODCOLIGADA = TM.CODCOLIGADA
                                                                                         AND	TTRBMOV.IDMOV = TM.IDMOV
                                                                                         AND	TTRBMOV.CODTRB = 'ICMS') ICMS

                                                                                    WHERE CODCOLIGADA = TM.CODCOLIGADA
                                                                                    AND   IDMOV = TM.IDMOV) as 'ICMSST'

                                        FROM TMOV TM
                                        WHERE CODCOLIGADA = ?
                                        AND   IDMOV = ?) X";

                        //movimento.ValorBruto = Convert.ToDecimal(DBS.QueryValue(0, sSql, movimento.CodColigada, movimento.IdMov));
                        movimento.ValorLiquido = Convert.ToDecimal(conn.ExecGetField(0, sSql, new object[] { movimento.CodColigada, movimento.IdMov }));

                        #endregion

                        #region Atualiza Valores do Movimento
                        sPoint = string.Concat("Inclusão: [Atualiza Valores do Movimento]");
                        //                        sSql = @"UPDATE TMOV SET 
                        //                                VALORBRUTO = :VALORBRUTO, VALORLIQUIDO = :VALORLIQUIDO, VALOROUTROS = :VALOROUTROS, 
                        //                                PERCENTUALFRETE = :PERCENTUALFRETE, VALORFRETE = :VALORFRETE,
                        //                                PERCENTUALSEGURO = :PERCENTUALSEGURO, VALORSEGURO = :VALORSEGURO,
                        //                                PERCENTUALDESC = :PERCENTUALDESC, VALORDESC = :VALORDESC,
                        //                                PERCENTUALDESP = :PERCENTUALDESP, VALORDESP = :VALORDESP,
                        //                                PERCENTUALEXTRA1 = :PERCENTUALEXTRA1, VALOREXTRA1 = :VALOREXTRA1,
                        //                                VALORBRUTOORIG = :VALORBRUTOORIG, VALORLIQUIDOORIG = :VALORLIQUIDOORIG, VALOROUTROSORIG = :VALOROUTROSORIG
                        //                            WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";

                        sSql = @"UPDATE TMOV SET 
                                VALORBRUTO = ?, VALORLIQUIDO = ?, VALOROUTROS = ?, 
                                PERCENTUALFRETE = ?, VALORFRETE = ?,
                                PERCENTUALSEGURO = ?, VALORSEGURO = ?,
                                PERCENTUALDESC = ?, VALORDESC = ?,
                                PERCENTUALDESP = ?, VALORDESP = ?,
                                PERCENTUALEXTRA1 = ?, VALOREXTRA1 = ?,
                                VALORBRUTOORIG = ?, VALORLIQUIDOORIG = ?, VALOROUTROSORIG = ?, IDNAT = ?
                            WHERE CODCOLIGADA = ? AND IDMOV = ?";

                        //this.DBS.SkipControlColumns = true;
                        //DBS.QueryExec(sSql, movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros,
                        //    movimento.PercentualFrete, movimento.ValorFrete,
                        //    movimento.PercentualSeguro, movimento.ValorSeguro,
                        //    movimento.PercentualDesc, movimento.ValorDesc,
                        //    movimento.PercentualDesp, movimento.ValorDesp,
                        //    movimento.PercentualExtra1, movimento.ValorExtra1,
                        //    movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros,
                        //    movimento.CodColigada, movimento.IdMov);
                        //this.DBS.SkipControlColumns = false;


                        conn.ExecTransaction(sSql, new object[]{ movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros,
                            movimento.PercentualFrete, movimento.ValorFrete,
                            movimento.PercentualSeguro, movimento.ValorSeguro,
                            movimento.PercentualDesc, movimento.ValorDesc,
                            movimento.PercentualDesp, movimento.ValorDesp,
                            movimento.PercentualExtra1, movimento.ValorExtra1,
                            movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros, movimento.IdNat,
                            movimento.CodColigada, movimento.IdMov});

                        #endregion

                        sPoint = string.Concat("Inclusão: [Exclui ZTITMMOVORCTEMP]");
                        //sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = ?";
                        //this.DBS.SkipControlColumns = true;
                        //DBS.QueryExec(sSql, movimento.GuidId);
                        conn.ExecTransaction(sSql, new object[] { movimento.GuidId });
                        //this.DBS.SkipControlColumns = false;
                        conn.Commit();
                        //DBS.Commit();

                        msg.Retorno = movimento.IdMov;
                        msg.Mensagem = "Movimento criado com sucesso";

                        #endregion
                        #endregion
                    }
                    else
                    {
                        this.InitServer();

                        if (ValidaStatus(movimento.CodColigada, movimento.IdMov))
                        {
                            #region Rotina Automática
                            /*
                            RM.Mov.Movimento.MovMovAlteracaoPar oMovMovAlteracaoPar = new RM.Mov.Movimento.MovMovAlteracaoPar();

                            oMovMovAlteracaoPar.CodColigada = movimento.CodColigada;
                            oMovMovAlteracaoPar.IdMov = movimento.IdMov;
                            oMovMovAlteracaoPar.CodLoc = movimento.CodLoc;
                            oMovMovAlteracaoPar.CodColCfo = movimento.CodColCfo;
                            oMovMovAlteracaoPar.CodCfo = movimento.CodCfo;
                            oMovMovAlteracaoPar.Serie = movimento.Serie;
                            oMovMovAlteracaoPar.DataEmissao = movimento.DataEmissao;
                            oMovMovAlteracaoPar.DataEntrega = movimento.DataEntrega;
                            oMovMovAlteracaoPar.PrazoEntrega = movimento.PrazoEntrega;
                            oMovMovAlteracaoPar.CodRepresentante = movimento.CodRepresentante;
                            oMovMovAlteracaoPar.FreteCIFouFOB = movimento.FreteCIFouFOB;
                            oMovMovAlteracaoPar.CodTra = movimento.CodTra;
                            oMovMovAlteracaoPar.CodCondicaoPagamento = movimento.CodCondicaoPagamento;

                            oMovMovAlteracaoPar.ValorFrete = movimento.ValorFrete;
                            oMovMovAlteracaoPar.PercentualFrete = movimento.PercentualFrete;
                            oMovMovAlteracaoPar.ValorDesc = movimento.ValorDesc;
                            oMovMovAlteracaoPar.PercentualDesc = movimento.PercentualDesc;
                            oMovMovAlteracaoPar.ValorDesp = movimento.ValorDesp;
                            oMovMovAlteracaoPar.PercentualDesp = movimento.PercentualDesp;
                            oMovMovAlteracaoPar.ValorSeguro = movimento.ValorSeguro;
                            oMovMovAlteracaoPar.PercentualSeguro = movimento.PercentualSeguro;
                            oMovMovAlteracaoPar.ValorExtra1 = movimento.ValorExtra1;
                            oMovMovAlteracaoPar.PercentualExtra1 = movimento.PercentualExtra1;

                            oMovMovAlteracaoPar.Observacao = movimento.Observacao;
                            oMovMovAlteracaoPar.CampoLivre1 = movimento.CampoLivre1;
                            oMovMovAlteracaoPar.HistoricoLongo = movimento.HistoricoLongo;
                            oMovMovAlteracaoPar.NumeroOrdem = movimento.NumeroOrdem;
                            oMovMovAlteracaoPar.SegundoNumero = movimento.SegundoNumero;

                            oMovMovAlteracaoPar.CodUsuario = movimento.CodUsuario;
                            oMovMovAlteracaoPar.CodUsuarioLogado = movimento.CodUsuarioLogado;

                            //oMovMovAlteracaoPar.CamposComplementares = movimento.CamposComplementares;
                            oMovMovAlteracaoPar.CamposComplementares = new Dictionary<string, object>();
                            oMovMovAlteracaoPar.CamposComplementares.Add("MSGFORNEC", movimento.MSGFORNEC);
                            oMovMovAlteracaoPar.CamposComplementares.Add("CONDPGTO", movimento.CONDPGTO);
                            oMovMovAlteracaoPar.CamposComplementares.Add("TPCULTURA", movimento.TPCULTURA);
                            oMovMovAlteracaoPar.CamposComplementares.Add("FINANCIADO", movimento.FINANCIADO);
                            oMovMovAlteracaoPar.CamposComplementares.Add("DEMONSBRINDE", movimento.DEMONSBRINDE);
                            oMovMovAlteracaoPar.CamposComplementares.Add("PARAFINANC", movimento.PARAFINANC);

                            string sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                            DataTable Itens = DBS.QuerySelect("ZTITMMOVORCTEMP", sSql, movimento.GuidId);
                            for (int i = 0; i < Itens.Rows.Count; i++)
                            {
                                RM.Mov.Movimento.MovMovItemMovPar oMovMovItemMovPar = new RM.Mov.Movimento.MovMovItemMovPar();

                                oMovMovItemMovPar.CodColigada = Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]);
                                oMovMovItemMovPar.IdMov = movimento.IdMov;
                                oMovMovItemMovPar.NSeqItmMov = Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]);
                                oMovMovItemMovPar.IdPrd = Convert.ToInt32(Itens.Rows[i]["IDPRD"]);
                                oMovMovItemMovPar.IdPrdComposto = (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]);
                                oMovMovItemMovPar.CodUnd = Itens.Rows[i]["CODUND"].ToString();
                                oMovMovItemMovPar.Quantidade = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]);
                                oMovMovItemMovPar.QuantidadeOriginal = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]);
                                oMovMovItemMovPar.PrecoUnitario = Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                                oMovMovItemMovPar.HistoricoLongo = Itens.Rows[i]["HISTORICOLONGO"].ToString();

                                //Campos Complementares
                                oMovMovItemMovPar.CamposComplementares = new Dictionary<string, object>();
                                oMovMovItemMovPar.CamposComplementares.Add("PRODPRICIPAL", Itens.Rows[i]["PRODPRICIPAL"].ToString());
                                oMovMovItemMovPar.CamposComplementares.Add("SEQUENCIAL", Itens.Rows[i]["SEQUENCIAL"].ToString());
                                oMovMovItemMovPar.CamposComplementares.Add("TIPOMAT", Itens.Rows[i]["TIPOMAT"].ToString());

                                RM.Mov.Movimento.MovMovTributoPar oMovMovTributoPar = new RM.Mov.Movimento.MovMovTributoPar();
                                oMovMovTributoPar.IdMov = movimento.IdMov;
                                oMovMovTributoPar.CodColigada = oMovMovItemMovPar.CodColigada;
                                oMovMovTributoPar.NSeqItmMov = oMovMovItemMovPar.NSeqItmMov;
                                oMovMovTributoPar.CodTrb = "IPI";
                                oMovMovTributoPar.Aliquota = Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]);
                                oMovMovTributoPar.BaseDeCalculo = oMovMovItemMovPar.Quantidade * ((oMovMovItemMovPar.PrecoUnitario == null) ? 0 : (decimal)oMovMovItemMovPar.PrecoUnitario);

                                oMovMovItemMovPar.TributosItemMov.Add(oMovMovTributoPar);

                                if (oMovMovAlteracaoPar.ItemMovimento == null)
                                    oMovMovAlteracaoPar.ItemMovimento = new ObjectList<MovMovItemMovPar>();

                                oMovMovAlteracaoPar.ItemMovimento.Add(oMovMovItemMovPar);
                            }

                            sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                            this.DBS.SkipControlColumns = true;
                            DBS.QueryExec(sSql, movimento.GuidId);
                            this.DBS.SkipControlColumns = false;


                            ObjectList<RM.Mov.Movimento.MovMovAlteracaoPar> lMovMovAlteracaoPar = new ObjectList<MovMovAlteracaoPar>();
                            lMovMovAlteracaoPar.Add(oMovMovAlteracaoPar);

                            oMovMovAlteracaoPar.ApagarTributosAntigos = true;
                            oMovMovAlteracaoPar.CodSistemaLogado = "T";

                            //RM.Mov.Movimento.IMovMovMod oIMovMovMod = this.CreateModule<RM.Mov.Movimento.IMovMovMod>("MovMovMod");
                            //oIMovMovMod.AlteracaoMovimento(lMovMovAlteracaoPar);
                            //oIMovMovMod.Alteracao_Prepare(lMovMovAlteracaoPar);
                            //oIMovMovMod.Alteracao_Save();

                            RM.Mov.Cst.Facade.CstMovFacade oCstMovFacade = CreateFacade<RM.Mov.Cst.Facade.CstMovFacade>();
                            oCstMovFacade.Alterar(movimento.CodColigada, movimento.IdMov, oMovMovAlteracaoPar);

                            msg.Retorno = movimento.IdMov;
                            msg.Mensagem = "Movimento alterado com sucesso";
                            */
                            #endregion

                            #region Rotina Manual

                            conn = AppLib.Context.poolConnection.Get();
                            conn.BeginTransaction();

                            //DBS.BeginTransaction();

                            if (movimento.IdMov == 0)
                            {
                                throw new Exception("Alteração: Identificador do movimento não pode ser 0 (zero).");
                            }

                            #region Cabeçalho do Orçamento

                            //                            string sSql = @"UPDATE TMOV SET
                            //                                    CODCFO =  :CODCFO,
                            //                                    DATAEMISSAO = :DATAEMISSAO, 
                            //                                    CODRPR = :CODRPR,
                            //                                    NORDEM = :NORDEM, 
                            //                                    OBSERVACAO = :OBSERVACAO, 
                            //                                    PERCENTUALFRETE  = :PERCENTUALFRETE,
                            //                                    VALORFRETE = :VALORFRETE, 
                            //                                    PERCENTUALSEGURO = :PERCENTUALSEGURO, 
                            //                                    VALORSEGURO = :VALORSEGURO,
                            //                                    PERCENTUALDESC = :PERCENTUALDESC, 
                            //                                    VALORDESC = :VALORDESC, 
                            //                                    PERCENTUALDESP = :PERCENTUALDESP,
                            //                                    VALORDESP = :VALORDESP, 
                            //                                    PERCENTUALEXTRA1 = :PERCENTUALEXTRA1, 
                            //                                    VALOREXTRA1 = :VALOREXTRA1,
                            //                                    FRETECIFOUFOB  = :FRETECIFOUFOB, 
                            //                                    CODTRA = :CODTRA, 
                            //                                    SEGUNDONUMERO = :SEGUNDONUMERO, 
                            //                                    CODCOLCFO  = :CODCOLCFO,
                            //                                    DATAENTREGA  = :DATAENTREGA, 
                            //                                    CAMPOLIVRE1  = :CAMPOLIVRE1, 
                            //                                    HORULTIMAALTERACAO  = :HORULTIMAALTERACAO, 
                            //                                    PRAZOENTREGA = :PRAZOENTREGA, 
                            //                                    CODUSUARIO = :CODUSUARIO,
                            //                                    RECMODIFIEDBY  = :RECMODIFIEDBY, 
                            //                                    RECMODIFIEDON = :RECMODIFIEDON
                            //                                WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            string sSql;
                            sPoint = string.Concat("Alteração: [Grava TMOV]");
                            //DBS.QueryExec(sSql,
                            //    /*CODCFO*/ movimento.CodCfo,
                            //    /*DATAEMISSAO*/ movimento.DataEmissao,
                            //    /*CODRPR*/ movimento.CodRepresentante,
                            //    /*NORDEM*/ movimento.NumeroOrdem,
                            //    /*OBSERVACAO*/ movimento.Observacao,
                            //    /*PERCENTUALFRETE*/ movimento.PercentualFrete,
                            //    /*VALORFRETE*/ movimento.ValorFrete,
                            //    /*PERCENTUALSEGURO*/ movimento.PercentualSeguro,
                            //    /*VALORSEGURO*/ movimento.ValorSeguro,
                            //    /*PERCENTUALDESC*/ movimento.PercentualDesc,
                            //    /*VALORDESC*/ movimento.ValorDesc,
                            //    /*PERCENTUALDESP*/ movimento.PercentualDesp,
                            //    /*VALORDESP*/ movimento.ValorDesp,
                            //    /*PERCENTUALEXTRA1*/ movimento.PercentualExtra1,
                            //    /*VALOREXTRA1*/ movimento.ValorExtra1,
                            //    /*FRETECIFOUFOB*/ movimento.FreteCIFouFOB,
                            //    /*CODTRA*/ movimento.CodTra,
                            //    /*SEGUNDONUMERO*/ movimento.SegundoNumero,
                            //    /*CODCOLCFO*/ movimento.CodColCfo,
                            //    /*DATAENTREGA*/ movimento.DataEntrega,
                            //    /*CAMPOLIVRE1*/ movimento.CampoLivre1,
                            //    /*HORULTIMAALTERACAO*/ DBS.ServerDate(),
                            //    /*PRAZOENTREGA*/ movimento.PrazoEntrega,
                            //    /*CODUSUARIO*/ movimento.CodUsuario,
                            //    /*RECMODIFIEDBY*/ movimento.CodUsuario,
                            //    /*RECMODIFIEDON*/ DBS.ServerDate(),
                            //    /*CODCOLIGADA*/ movimento.CodColigada,
                            //    /*IDMOV*/ movimento.IdMov);

                            conn.ExecTransaction(@"UPDATE TMOV SET
                                    CODCFO =  ?,
                                    DATAEMISSAO = ?, 
                                    CODRPR = ?,
                                    NORDEM = ?, 
                                    OBSERVACAO = ?, 
                                    PERCENTUALFRETE  = ?,
                                    VALORFRETE = ?, 
                                    PERCENTUALSEGURO = ?, 
                                    VALORSEGURO = ?,
                                    PERCENTUALDESC = ?, 
                                    VALORDESC = ?, 
                                    PERCENTUALDESP = ?,
                                    VALORDESP = ?, 
                                    PERCENTUALEXTRA1 = ?, 
                                    VALOREXTRA1 = ?,
                                    FRETECIFOUFOB  = ?, 
                                    CODTRA = ?, 
                                    SEGUNDONUMERO = ?, 
                                    CODCOLCFO  = ?,
                                    DATAENTREGA  = ?, 
                                    CAMPOLIVRE1  = ?, 
                                    HORULTIMAALTERACAO  = ?, 
                                    PRAZOENTREGA = ?, 
                                    CODUSUARIO = ?,
                                    RECMODIFIEDBY  = ?, 
                                    RECMODIFIEDON = ?,
                                    IDNAT = ?,
                                    CODTB1FLX = ?,
                                    CODVEN1 = ?,
                                    CODCPG = ?
                                WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[]{
                                /*CODCFO*/ movimento.CodCfo,
                                /*DATAEMISSAO*/ Convert.ToDateTime(movimento.DataEmissao).ToString("yyy-MM-dd"),
                                /*CODRPR*/ movimento.CodRepresentante,
                                /*NORDEM*/ movimento.NumeroOrdem,
                                /*OBSERVACAO*/ movimento.Observacao,
                                /*PERCENTUALFRETE*/ movimento.PercentualFrete,
                                /*VALORFRETE*/ movimento.ValorFrete,
                                /*PERCENTUALSEGURO*/ movimento.PercentualSeguro,
                                /*VALORSEGURO*/ movimento.ValorSeguro,
                                /*PERCENTUALDESC*/ movimento.PercentualDesc,
                                /*VALORDESC*/ movimento.ValorDesc,
                                /*PERCENTUALDESP*/ movimento.PercentualDesp,
                                /*VALORDESP*/ movimento.ValorDesp,
                                /*PERCENTUALEXTRA1*/ movimento.PercentualExtra1,
                                /*VALOREXTRA1*/ movimento.ValorExtra1,
                                /*FRETECIFOUFOB*/ movimento.FreteCIFouFOB,
                                /*CODTRA*/ movimento.CodTra,
                                /*SEGUNDONUMERO*/ movimento.SegundoNumero,
                                /*CODCOLCFO*/ movimento.CodColCfo,
                                /*DATAENTREGA*/ movimento.DataEntrega,
                                /*CAMPOLIVRE1*/ movimento.CampoLivre1,
                                /*HORULTIMAALTERACAO*/ AppLib.Context.poolConnection.Get().GetDateTime(),
                                /*PRAZOENTREGA*/ movimento.PrazoEntrega,
                                /*CODUSUARIO*/ movimento.CodUsuario,
                                /*RECMODIFIEDBY*/ movimento.CodUsuario,
                                /*RECMODIFIEDON*/ AppLib.Context.poolConnection.Get().GetDateTime(),
                                /*IDNAT*/ movimento.IdNat,
                                /*CODTB1FLX*/ movimento.CodTb1Opcional,
                                /*CODVEN1*/ movimento.CodVen1,
                                /*CODCPG*/ movimento.CodCondicaoPagamento,
                                /*CODCOLIGADA*/ movimento.CodColigada,
                                /*IDMOV*/ movimento.IdMov});





                            //                            sSql = @"UPDATE TMOVHISTORICO SET 
                            //                                    HISTORICOLONGO = :HISTORICOLONGO, 
                            //                                    RECMODIFIEDBY = :RECMODIFIEDBY, 
                            //                                    RECMODIFIEDON = :RECMODIFIEDON
                            //                                WHERE CODCOLIGADA = :CODCOLIGADA
                            //                                    AND IDMOV = :IDMOV";

                            sSql = @"UPDATE TMOVHISTORICO SET 
                                                                HISTORICOLONGO = ?, 
                                                                RECMODIFIEDBY = ?, 
                                                                RECMODIFIEDON = ?
                                                            WHERE CODCOLIGADA = ?
                                                                AND IDMOV = ?";

                            sPoint = string.Concat("Alteração: [Grava TMOVHISTORICO]");
                            //DBS.QueryExec(sSql,
                            //    /*HISTORICOLONGO*/ movimento.HistoricoLongo,
                            //    /*RECMODIFIEDBY*/ movimento.CodUsuario,
                            //    /*RECMODIFIEDON*/ DBS.ServerDate(),
                            //    /*CODCOLIGADA*/ movimento.CodColigada,
                            //    /*IDMOV*/ movimento.IdMov);

                            conn.ExecTransaction(sSql, new object[]{
                                /*HISTORICOLONGO*/ movimento.HistoricoLongo,
                                /*RECMODIFIEDBY*/ movimento.CodUsuario,
                                /*RECMODIFIEDON*/ AppLib.Context.poolConnection.Get().GetDateTime(),
                                /*CODCOLIGADA*/ movimento.CodColigada,
                                /*IDMOV*/ movimento.IdMov});



                            //                            sSql = @"UPDATE TMOVCOMPL SET 
                            //                                    MSGFORNEC = :MSGFORNEC, 
                            //                                    CONDPGTO = :CONDPGTO, 
                            //                                    TPCULTURA = :TPCULTURA, 
                            //                                    TIPOVENDA = :TIPOVENDA,
                            //                                    FINANCIADO = :FINANCIADO, 
                            //                                    DEMONSBRINDE = :DEMONSBRINDE, 
                            //                                    PARAFINANC = :PARAFINANC,
                            //                                    RECMODIFIEDBY = :RECMODIFIEDBY, 
                            //                                    RECMODIFIEDON = :RECMODIFIEDON
                            //                                WHERE CODCOLIGADA = :CODCOLIGADA
                            //                                    AND IDMOV = :IDMOV";

                            sSql = @"UPDATE TMOVCOMPL SET 
                                                                TIPOVENDA = ?,
                                                                APLICPROD = ?,
                                                                NCONTRIB = ?, 
                                                                UFORCAMENTO = ?,
                                                                NOMEFCFOORC = ?,
                                                                RECMODIFIEDBY = ?, 
                                                                RECMODIFIEDON = ?,
                                                                TIPODESC = ?,
                                                                VALORDESCRAT = ?,
                                                                CONTATOCOM = ?,
                                                                EMAILCOM = ?,
                                                                TELEFONECOM = ?,
                                                                VALORDESPRAT = ?,
                                                                SEMIMPOSTOS = ?,
                                                                USABENEFICIO = ?
                                                            WHERE CODCOLIGADA = ?
                                                                AND IDMOV = ?";

                            sPoint = string.Concat("Alteração: [Grava TMOVCOMPL]");
                            //DBS.QueryExec(sSql,
                            //    /*MSGFORNEC*/ movimento.MSGFORNEC,
                            //    /*CONDPGTO*/ movimento.CONDPGTO,
                            //    /*TPCULTURA*/ movimento.TPCULTURA,
                            //    /*TIPOVENDA*/ movimento.TIPOVENDA,
                            //    /*FINANCIADO*/ movimento.FINANCIADO,
                            //    /*DEMONSBRINDE*/ movimento.DEMONSBRINDE,
                            //    /*PARAFINANC*/ movimento.PARAFINANC,
                            //    /*RECMODIFIEDBY*/ movimento.CodUsuario,
                            //    /*RECMODIFIEDON*/ DBS.ServerDate(),
                            //    /*CODCOLIGADA*/ movimento.CodColigada,
                            //    /*IDMOV*/ movimento.IdMov);

                            conn.ExecTransaction(sSql, new object[]{
                                ///*MSGFORNEC*/ movimento.MSGFORNEC,
                                ///*CONDPGTO*/ movimento.CONDPGTO,
                                ///*TPCULTURA*/ movimento.TPCULTURA,
                                /*TIPOVENDA*/ movimento.TIPOVENDA,
                                /*APLICPROD*/ movimento.APLICPROD,
                                /*NCONTRIB*/ movimento.NCONTRIB,
                                /*UFORCAMENTO*/ movimento.UFORCAMENTO,
                                /*NOMEFCFOORC*/ movimento.NOMECLIENTEORC,
                                ///*FINANCIADO*/ movimento.FINANCIADO,
                                ///*DEMONSBRINDE*/ movimento.DEMONSBRINDE,
                                ///*PARAFINANC*/ movimento.PARAFINANC,
                                /*RECMODIFIEDBY*/ movimento.CodUsuario,
                                /*RECMODIFIEDON*/ AppLib.Context.poolConnection.Get().GetDateTime(),
                                ///*CODPRDREVENDA*/ movimento.CODPRDREVENDA,
                                ///*DESCRICAOPRDREVENDA*/ movimento.DESCRICAOPRDREVENDA,
                                /*TIPODESC*/ movimento.TIPODESC, 
                                /*VALORDESCRAT*/ movimento.VALORDESCRAT,
                                /*CONTATOCOM*/ movimento.CONTATOCOM,
                                /*EMAILCOM*/ movimento.EMAILCOM,
                                /*TELEFONECOM*/ movimento.TELEFONECOM,
                                /*VALORDESPRAT*/ movimento.VALORDESPRAT,
                                /*SEMIMPOSTOS*/ movimento.SEMIMPOSTOS,
                                /*USABENEFICIO*/ movimento.POSSUIBENEFICIO,
                                /*CODCOLIGADA*/ movimento.CodColigada,
                                /*IDMOV*/ movimento.IdMov});


                            #endregion

                            #region Exclui Registro

                            //this.DBS.SkipControlColumns = true;
                            //sSql = @"DELETE FROM TITMMOVFISCAL WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            //sPoint = string.Concat("Alteração: [Exclui TITMMOVFISCAL]");
                            //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);

                            //sSql = @"DELETE FROM TTRBMOV WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            //sPoint = string.Concat("Alteração: [Exclui TTRBMOV]");
                            //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);

                            //sSql = @"DELETE FROM TITMMOVCOMPL WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            //sPoint = string.Concat("Alteração: [Exclui TITMMOVCOMPL]");
                            //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);

                            //sSql = @"DELETE FROM TITMMOVHISTORICO WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            //sPoint = string.Concat("Alteração: [Exclui TITMMOVHISTORICO]");
                            //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);

                            //sSql = @"DELETE FROM TITMMOV WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            //sPoint = string.Concat("Alteração: [Exclui TITMMOV]");
                            //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);
                            //this.DBS.SkipControlColumns = false;

                            List<string> contraint = new List<string>();
                            sPoint = string.Concat("Alteração: [Desabilita Chave Estrangeira]");
                            contraint.Add("ALTER TABLE TITMMOVRELAC NOCHECK CONSTRAINT FKTITMMOVRELAC_TITMMOVDESTINO");
                            contraint.Add("ALTER TABLE TITMMOVRELAC NOCHECK CONSTRAINT FKTITMMOVRELAC_TITMMOVORIGEM");
                            MetodosSQL.ExecMultiple(contraint);
                            contraint.Clear();


                            //this.DBS.SkipControlColumns = true;
                            sSql = @"DELETE FROM TITMMOVFISCAL WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            sPoint = string.Concat("Alteração: [Exclui TITMMOVFISCAL]");
                            conn.ExecTransaction(sSql, new object[] {/*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov });

                            sSql = @"DELETE FROM TTRBMOV WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            sPoint = string.Concat("Alteração: [Exclui TTRBMOV]");
                            conn.ExecTransaction(sSql, new object[] {/*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov });

                            sSql = @"DELETE FROM TITMMOVCOMPL WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            sPoint = string.Concat("Alteração: [Exclui TITMMOVCOMPL]");
                            conn.ExecTransaction(sSql, new object[] { /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov });

                            sSql = @"DELETE FROM TITMMOVHISTORICO WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            sPoint = string.Concat("Alteração: [Exclui TITMMOVHISTORICO]");
                            conn.ExecTransaction(sSql, new object[] {/*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov });

                            sSql = @"DELETE FROM TITMMOV WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            sPoint = string.Concat("Alteração: [Exclui TITMMOV]");
                            conn.ExecTransaction(sSql, new object[] { /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov });
                            //this.DBS.SkipControlColumns = false;

                            #endregion

                            #region Item do Orçamento

                            sPoint = string.Concat("Alteração: [Busca ZTITMMOVORCTEMP]");
                            //sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                            sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = ?";
                            //DataTable Itens = DBS.QuerySelect("ZTITMMOVORCTEMP", sSql, movimento.GuidId);
                            DataTable Itens = conn.ExecQuery(sSql, new object[] { movimento.GuidId });

                            sSql = String.Format(@"SELECT sum((QUANTIDADE*PRECOUNITARIO)) as 'TOTAL' FROM ZTITMMOVORCTEMP where ROWID = '{0}'", movimento.GuidId);

                            Decimal total = (Decimal)conn.ExecGetField(0, sSql, new object[] { });

                            for (int i = 0; i < Itens.Rows.Count; i++)
                            {
                                string sCODTB2FAT = null;
                                string sCODTB3FAT = null;
                                string sCODTB4FAT = null;

                                Decimal totalItem = (Decimal)Itens.Rows[i]["QUANTIDADE"] * (Decimal)Itens.Rows[i]["PRECOUNITARIO"];
                                Decimal percentItem = (totalItem * 100) / total;
                                percentItem = percentItem / 100;

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " CODTB2FAT, CODTB3FAT, CODTB4FAT");
                                //sSql = @"SELECT TPRD.CODTB2FAT, TPRD.CODTB3FAT, TPRD.CODTB4FAT FROM TPRD WHERE TPRD.CODCOLIGADA = :CODCOLIGADA AND TPRD.IDPRD = :IDPRD";
                                sSql = @"SELECT TPRD.CODTB2FAT, TPRD.CODTB3FAT, TPRD.CODTB4FAT FROM TPRD WHERE TPRD.CODCOLIGADA = ? AND TPRD.IDPRD = ?";
                                //DataTable dtPrd = DBS.QuerySelect("TPRD", sSql, Itens.Rows[i]["CODCOLIGADA"], Itens.Rows[i]["IDPRD"]);
                                DataTable dtPrd = conn.ExecQuery(sSql, new object[] { Itens.Rows[i]["CODCOLIGADA"], Itens.Rows[i]["IDPRD"] });
                                foreach (DataRow row in dtPrd.Rows)
                                {
                                    sCODTB2FAT = (row["CODTB2FAT"] == DBNull.Value) ? null : row["CODTB2FAT"].ToString();
                                    sCODTB3FAT = (row["CODTB3FAT"] == DBNull.Value) ? null : row["CODTB3FAT"].ToString();
                                    sCODTB4FAT = (row["CODTB4FAT"] == DBNull.Value) ? null : row["CODTB4FAT"].ToString();
                                }


                                //                                sSql = @"INSERT INTO TITMMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, NUMEROSEQUENCIAL, IDPRD, CODTIP, QUANTIDADE, PRECOUNITARIO, PRECOTABELA, 
                                //                                            PERCENTUALDESC, VALORDESC, PERCENTUALDESP, VALORDESP, DATAEMISSAO, 
                                //                                            CODMEN, NUMEROTRIBUTOS, CODTB1FAT, CODTB2FAT, CODTB3FAT, CODTB4FAT, 
                                //                                            CODTB5FAT, CODTB1FLX, CODTB2FLX, CODTB3FLX, CODTB4FLX, CODTB5FLX, CAMPOLIVRE, 
                                //                                            CODUND, QUANTIDADEARECEBER, CODNAT, CODCPG, DATAENTREGA, PRATELEIRA, IDCNT, NSEQITMCNT, 
                                //                                            DATAINIFAT, DATAFIMFAT, FLAGEFEITOSALDO, VALORUNITARIO, VALORFINANCEIRO, 
                                //                                            IMPRIMEMOV, CODCCUSTO, FLAGREPASSE, ALIQORDENACAO, QUANTIDADEORIGINAL, 
                                //                                            IDNAT, FLAG, CHAPA, INICIO, TERMINO, PREVINICIO, STATUS, BLOCK, 
                                //                                            FLAGREFATURAMENTO, IDCNTDESTINO, NSEQITMCNTDEST, FATORCONVUND, IDPRJ, IDTRF, 
                                //                                            VALORTOTALITEM, VALORCODIGOPRD, TIPOCODIGOPRD, QTDUNDPEDIDO, TRIBUTACAOECF, 
                                //                                            CODFILIAL, CODDEPARTAMENTO, IDPRDCOMPOSTO, QUANTIDADESEPARADA, PERCENTCOMISSAO, INDICENCM, NCM, CODRPR, COMISSAOREPRES, 
                                //                                            NSEQITMCNTMEDICAO, VALORESCRITURACAO, VALORFINPEDIDO, VALORFRETECTRC, VALOROPFRM1, VALOROPFRM2, 
                                //                                            IDOBJOFICINA, PRECOEDITADO, QTDEVOLUMEUNITARIO, IDGRD, 
                                //                                            CODVEN1, CODLOCALBN, REGISTROEXPORTACAO, DATARE, PRECOTOTALEDITADO, CST, 
                                //                                            VALORDESCCONDICONALITM, VALORDESPCONDICIONALITM, DATAORCAMENTO, CODTBORCAMENTO, RATEIOFRETE, 
                                //                                            RATEIOSEGURO, RATEIODESC, RATEIODESP, RATEIOEXTRA1, RATEIOEXTRA2, RATEIOFRETECTRC, 
                                //                                            RATEIODEDMAT, RATEIODEDSUB, RATEIODEDOUT, IDCLASSIFENERGIACOMUNIC, VALORUNTORCAMENTO, 
                                //                                            VALSERVICONFE, CODLOC, VALORBEM, VALORLIQUIDO, CODIGOCODIF, CODMUNSERVICO, CODETDMUNSERV, RATEIOCCUSTODEPTO, CUSTOREPOSICAO, 
                                //                                            CUSTOREPOSICAOB, VALORFINTERCEIROS, VALORFINANCGERENCIAL, CODIGOSERVICO, VALORUNITGERENCIAL, 
                                //                                            IDINTEGRACAO, IDTABPRECO, VALORBRUTOITEM, VALORBRUTOITEMORIG, CODCOLTBORCAMENTO, CODPUBLIC, QUANTIDADETOTAL, 
                                //                                            PRODUTOSUBSTITUTO, CODTBGRUPOORC, PRECOUNITARIOSELEC, VALORRATEIOLAN, 
                                //                                            RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                //                                        VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :NUMEROSEQUENCIAL, :IDPRD, :CODTIP, :QUANTIDADE, :PRECOUNITARIO, :PRECOTABELA, 
                                //                                            :PERCENTUALDESC, :VALORDESC, :PERCENTUALDESP, :VALORDESP, :DATAEMISSAO, 
                                //                                            :CODMEN, :NUMEROTRIBUTOS, :CODTB1FAT, :CODTB2FAT, :CODTB3FAT, :CODTB4FAT, 
                                //                                            :CODTB5FAT, :CODTB1FLX, :CODTB2FLX, :CODTB3FLX, :CODTB4FLX, :CODTB5FLX, :CAMPOLIVRE, 
                                //                                            :CODUND, :QUANTIDADEARECEBER, :CODNAT, :CODCPG, :DATAENTREGA, :PRATELEIRA, :IDCNT, :NSEQITMCNT, 
                                //                                            :DATAINIFAT, :DATAFIMFAT, :FLAGEFEITOSALDO, :VALORUNITARIO, :VALORFINANCEIRO, 
                                //                                            :IMPRIMEMOV, :CODCCUSTO, :FLAGREPASSE, :ALIQORDENACAO, :QUANTIDADEORIGINAL, 
                                //                                            :IDNAT, :FLAG, :CHAPA, :INICIO, :TERMINO, :PREVINICIO, :STATUS, :BLOCK, 
                                //                                            :FLAGREFATURAMENTO, :IDCNTDESTINO, :NSEQITMCNTDEST, :FATORCONVUND, :IDPRJ, :IDTRF, 
                                //                                            :VALORTOTALITEM, :VALORCODIGOPRD, :TIPOCODIGOPRD, :QTDUNDPEDIDO, :TRIBUTACAOECF, 
                                //                                            :CODFILIAL, :CODDEPARTAMENTO, :IDPRDCOMPOSTO, :QUANTIDADESEPARADA, :PERCENTCOMISSAO, :INDICENCM, :NCM, :CODRPR, :COMISSAOREPRES, 
                                //                                            :NSEQITMCNTMEDICAO, :VALORESCRITURACAO, :VALORFINPEDIDO, :VALORFRETECTRC, :VALOROPFRM1, :VALOROPFRM2, 
                                //                                            :IDOBJOFICINA, :PRECOEDITADO, :QTDEVOLUMEUNITARIO, :IDGRD, 
                                //                                            :CODVEN1, :CODLOCALBN, :REGISTROEXPORTACAO, :DATARE, :PRECOTOTALEDITADO, :CST, 
                                //                                            :VALORDESCCONDICONALITM, :VALORDESPCONDICIONALITM, :DATAORCAMENTO, :CODTBORCAMENTO, :RATEIOFRETE, 
                                //                                            :RATEIOSEGURO, :RATEIODESC, :RATEIODESP, :RATEIOEXTRA1, :RATEIOEXTRA2, :RATEIOFRETECTRC, 
                                //                                            :RATEIODEDMAT, :RATEIODEDSUB, :RATEIODEDOUT, :IDCLASSIFENERGIACOMUNIC, :VALORUNTORCAMENTO, 
                                //                                            :VALSERVICONFE, :CODLOC, :VALORBEM, :VALORLIQUIDO, :CODIGOCODIF, :CODMUNSERVICO, :CODETDMUNSERV, :RATEIOCCUSTODEPTO, :CUSTOREPOSICAO, 
                                //                                            :CUSTOREPOSICAOB, :VALORFINTERCEIROS, :VALORFINANCGERENCIAL, :CODIGOSERVICO, :VALORUNITGERENCIAL, 
                                //                                            :IDINTEGRACAO, :IDTABPRECO, :VALORBRUTOITEM, :VALORBRUTOITEMORIG, :CODCOLTBORCAMENTO, :CODPUBLIC, :QUANTIDADETOTAL, 
                                //                                            :PRODUTOSUBSTITUTO, :CODTBGRUPOORC, :PRECOUNITARIOSELEC, :VALORRATEIOLAN, 
                                //                                            :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                                sSql = @"INSERT INTO TITMMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, NUMEROSEQUENCIAL, IDPRD, CODTIP, QUANTIDADE, PRECOUNITARIO, PRECOTABELA, 
                                            PERCENTUALDESC, VALORDESC, PERCENTUALDESP, VALORDESP, DATAEMISSAO, 
                                            CODMEN, NUMEROTRIBUTOS, CODTB1FAT, CODTB2FAT, CODTB3FAT, CODTB4FAT, 
                                            CODTB5FAT, CODTB1FLX, CODTB2FLX, CODTB3FLX, CODTB4FLX, CODTB5FLX, CAMPOLIVRE, 
                                            CODUND, QUANTIDADEARECEBER, CODNAT, CODCPG, DATAENTREGA, PRATELEIRA, IDCNT, NSEQITMCNT, 
                                            DATAINIFAT, DATAFIMFAT, FLAGEFEITOSALDO, VALORUNITARIO, VALORFINANCEIRO, 
                                            IMPRIMEMOV, CODCCUSTO, FLAGREPASSE, ALIQORDENACAO, QUANTIDADEORIGINAL, 
                                            IDNAT, FLAG, CHAPA, INICIO, TERMINO, PREVINICIO, STATUS, BLOCK, 
                                            FLAGREFATURAMENTO, IDCNTDESTINO, NSEQITMCNTDEST, FATORCONVUND, IDPRJ, IDTRF, 
                                            VALORTOTALITEM, VALORCODIGOPRD, TIPOCODIGOPRD, QTDUNDPEDIDO, TRIBUTACAOECF, 
                                            CODFILIAL, CODDEPARTAMENTO, IDPRDCOMPOSTO, QUANTIDADESEPARADA, PERCENTCOMISSAO, INDICENCM, NCM, CODRPR, COMISSAOREPRES, 
                                            NSEQITMCNTMEDICAO, VALORESCRITURACAO, VALORFINPEDIDO, VALORFRETECTRC, VALOROPFRM1, VALOROPFRM2, 
                                            IDOBJOFICINA, PRECOEDITADO, QTDEVOLUMEUNITARIO, IDGRD, 
                                            CODVEN1, CODLOCALBN, REGISTROEXPORTACAO, DATARE, PRECOTOTALEDITADO, CST, 
                                            VALORDESCCONDICONALITM, VALORDESPCONDICIONALITM, DATAORCAMENTO, CODTBORCAMENTO, RATEIOFRETE, 
                                            RATEIOSEGURO, RATEIODESC, RATEIODESP, RATEIOEXTRA1, RATEIOEXTRA2, RATEIOFRETECTRC, 
                                            RATEIODEDMAT, RATEIODEDSUB, RATEIODEDOUT, IDCLASSIFENERGIACOMUNIC, VALORUNTORCAMENTO, 
                                            VALSERVICONFE, CODLOC, VALORBEM, VALORLIQUIDO, CODIGOCODIF, CODMUNSERVICO, CODETDMUNSERV, RATEIOCCUSTODEPTO, CUSTOREPOSICAO, 
                                            CUSTOREPOSICAOB, VALORFINTERCEIROS, VALORFINANCGERENCIAL, CODIGOSERVICO, VALORUNITGERENCIAL, 
                                            IDINTEGRACAO, IDTABPRECO, VALORBRUTOITEM, VALORBRUTOITEMORIG, CODCOLTBORCAMENTO, CODPUBLIC, QUANTIDADETOTAL, 
                                            PRODUTOSUBSTITUTO, CODTBGRUPOORC, PRECOUNITARIOSELEC, VALORRATEIOLAN, 
                                            RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " Grava TITMMOV");
                                //DBS.QueryExec(sSql,
                                //    /*CODCOLIGADA*/ Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]),
                                //    /*IDMOV*/ movimento.IdMov,
                                //    /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                //    /*NUMEROSEQUENCIAL*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                //    /*IDPRD*/ Convert.ToInt32(Itens.Rows[i]["IDPRD"]),
                                //    /*CODTIP*/ null,
                                //    /*QUANTIDADE*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                //    /*PRECOUNITARIO*/ Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                //    /*PRECOTABELA*/ 0,
                                //    /*PERCENTUALDESC*/ null, /*VALORDESC*/ null, /*PERCENTUALDESP*/ null, /*VALORDESP*/ null, /*DATAEMISSAO*/ movimento.DataEmissao,
                                //    /*CODMEN*/ null, /*NUMEROTRIBUTOS*/ null, /*CODTB1FAT*/ null, /*CODTB2FAT*/ sCODTB2FAT, /*CODTB3FAT*/ sCODTB3FAT, /*CODTB4FAT:*/ sCODTB4FAT,
                                //    /*CODTB5FAT*/ null, /*CODTB1FLX*/ null, /*CODTB2FLX*/ null, /*CODTB3FLX*/ null, /*CODTB4FLX*/ null, /*CODTB5FLX*/ null, /*CAMPOLIVRE*/ null,
                                //    /*CODUND*/ Itens.Rows[i]["CODUND"].ToString(),
                                //    /*QUANTIDADEARECEBER*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                //    /*CODNAT*/ null, /*CODCPG*/ null, /*DATAENTREGA*/ movimento.DataEntrega, /*PRATELEIRA*/ null, /*IDCNT*/ null, /*NSEQITMCNT*/ null,
                                //    /*DATAINIFAT*/ null, /*DATAFIMFAT*/ null, /*FLAGEFEITOSALDO*/ null, /*VALORUNITARIO*/ 0,  /*VALORFINANCEIRO*/ 0,
                                //    /*IMPRIMEMOV*/ null, /*CODCCUSTO*/ null, /*FLAGREPASSE*/ null, /*ALIQORDENACAO*/ 0,
                                //    /*QUANTIDADEORIGINAL*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                //    /*IDNAT*/ null, /*FLAG*/ 0, /*CHAPA*/ null, /*INICIO*/ null, /*TERMINO*/ null, /*PREVINICIO*/ null, /*STATUS*/ null, /*BLOCK*/ null,
                                //    /*FLAGREFATURAMENTO*/ null, /*IDCNTDESTINO*/ null, /*NSEQITMCNTDEST*/ null, /*FATORCONVUND*/ 0, /*IDPRJ*/ null, /*IDTRF*/ null,
                                //    /*VALORTOTALITEM*/ 0, /*VALORCODIGOPRD*/ null, /*TIPOCODIGOPRD*/ null, /*QTDUNDPEDIDO*/ null, /*TRIBUTACAOECF*/ null,
                                //    /*CODFILIAL*/ movimento.CodFilial, /*CODDEPARTAMENTO*/ null,
                                //    /*IDPRDCOMPOSTO*/ (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]),
                                //    /*QUANTIDADESEPARADA*/ 0, /*PERCENTCOMISSAO*/ 0, /*INDICENCM*/ null, /*NCM*/ null, /*CODRPR*/ null, /*COMISSAOREPRES*/ 0,
                                //    /*NSEQITMCNTMEDICAO*/ null, /*VALORESCRITURACAO*/ 0, /*VALORFINPEDIDO*/ 0, /*VALORFRETECTRC*/ null, /*VALOROPFRM1*/ 0, /*VALOROPFRM2*/ 0,
                                //    /*IDOBJOFICINA*/ null, /*PRECOEDITADO*/ 0, /*QTDEVOLUMEUNITARIO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]), /*IDGRD*/ null,
                                //    /*CODVEN1*/ null, /*CODLOCALBN*/ null, /*REGISTROEXPORTACAO*/ null, /*DATARE*/ null, /*PRECOTOTALEDITADO*/ null, /*CST*/ null,
                                //    /*VALORDESCCONDICONALITM*/ 0, /*VALORDESPCONDICIONALITM*/ 0, /*DATAORCAMENTO*/ null, /*CODTBORCAMENTO*/ null, /*RATEIOFRETE*/ null,
                                //    /*RATEIOSEGURO*/ null, /*RATEIODESC*/ null, /*RATEIODESP*/ null, /*RATEIOEXTRA1*/ null, /*RATEIOEXTRA2*/ null, /*RATEIOFRETECTRC*/ null,
                                //    /*RATEIODEDMAT*/ null, /*RATEIODEDSUB*/ null, /*RATEIODEDOUT*/ null, /*IDCLASSIFENERGIACOMUNIC*/ null, /*VALORUNTORCAMENTO*/ 0,
                                //    /*VALSERVICONFE*/ 0, /*CODLOC*/ movimento.CodLoc, /*VALORBEM*/ 0,
                                //    /*VALORLIQUIDO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                //    /*CODIGOCODIF*/ null, /*CODMUNSERVICO*/ null, /*CODETDMUNSERV*/ null, /*RATEIOCCUSTODEPTO*/ null, /*CUSTOREPOSICAO*/ null,
                                //    /*CUSTOREPOSICAOB*/ null, /*VALORFINTERCEIROS*/ null, /*VALORFINANCGERENCIAL*/ null, /*CODIGOSERVICO*/ null, /*VALORUNITGERENCIAL*/ null,
                                //    /*IDINTEGRACAO*/ null, /*IDTABPRECO*/ null,
                                //    /*VALORBRUTOITEM*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                //    /*VALORBRUTOITEMORIG*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                //    /*CODCOLTBORCAMENTO*/ null, /*CODPUBLIC*/ null, /*QUANTIDADETOTAL*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                //    /*PRODUTOSUBSTITUTO*/ 0, /*CODTBGRUPOORC*/ null, /*PRECOUNITARIOSELEC*/ 0, /*VALORRATEIOLAN*/ null,
                                //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                                if (movimento.ValorFrete > 0)
                                {
                                    valorTotalItem = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);

                                    for (int j = 0; j < Itens.Rows.Count; j++)
                                    {
                                        valorTotalMovimento += (Convert.ToDecimal(Itens.Rows[j]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[j]["PRECOUNITARIO"]));
                                    }

                                    //percentualRateioFrete = valorTotalItem / valorTotalMovimento;
                                    //rateioFrete = percentualRateioFrete / Convert.ToDecimal(movimento.ValorFrete);

                                    percentualRateioFrete = (valorTotalItem*100) / valorTotalMovimento;
                                    rateioFrete = (percentualRateioFrete * Convert.ToDecimal(movimento.ValorFrete)) / 100;

                                    valorTotalMovimento = 0;
                                }

                                if (movimento.ValorDesc > 0)
                                {
                                    valorTotalItem = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);

                                    for (int j = 0; j < Itens.Rows.Count; j++)
                                    {
                                        valorTotalMovimento += (Convert.ToDecimal(Itens.Rows[j]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[j]["PRECOUNITARIO"]));
                                    }

                                    percentualRateioDesc = valorTotalItem / valorTotalMovimento;
                                    rateioDesc = percentualRateioDesc * Convert.ToDecimal(movimento.ValorDesc);

                                    valorTotalMovimento = 0;
                                }

                                precoUnitario = Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                                precoUnitario = Math.Round(precoUnitario, 2);

                                quantidade = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]);
                                quantidade = Math.Round(quantidade, 2);

                                conn.ExecTransaction(sSql, new object[]{
                                    /*CODCOLIGADA*/ Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]),
                                    /*IDMOV*/ movimento.IdMov,
                                    /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                    /*NUMEROSEQUENCIAL*/ Convert.ToInt32(Itens.Rows[i]["NUMEROSEQUENCIAL"]),
                                    /*IDPRD*/ Convert.ToInt32(Itens.Rows[i]["IDPRD"]),
                                    /*CODTIP*/ null,
                                    /*QUANTIDADE*/ quantidade,
                                    /*PRECOUNITARIO*/ precoUnitario,
                                    /*PRECOTABELA*/ 0,
                                    /*PERCENTUALDESC*/ null, /*VALORDESC*/ null, /*PERCENTUALDESP*/ null, /*VALORDESP*/ null, /*DATAEMISSAO*/ movimento.DataEmissao,
                                    /*CODMEN*/ null, /*NUMEROTRIBUTOS*/ null, /*CODTB1FAT*/ null, /*CODTB2FAT*/ sCODTB2FAT, /*CODTB3FAT*/ sCODTB3FAT, /*CODTB4FAT:*/ sCODTB4FAT,
                                    /*CODTB5FAT*/ null, /*CODTB1FLX*/ null, /*CODTB2FLX*/ null, /*CODTB3FLX*/ null, /*CODTB4FLX*/ null, /*CODTB5FLX*/ null, /*CAMPOLIVRE*/ null,
                                    /*CODUND*/ Itens.Rows[i]["CODUND"].ToString(),
                                    /*QUANTIDADEARECEBER*/ quantidade,
                                    /*CODNAT*/ null, /*CODCPG*/ null, /*DATAENTREGA*/ Itens.Rows[i]["DATAENTREGA"]/*movimento.DataEntrega*/, /*PRATELEIRA*/ null, /*IDCNT*/ null, /*NSEQITMCNT*/ null,
                                    /*DATAINIFAT*/ null, /*DATAFIMFAT*/ null, /*FLAGEFEITOSALDO*/ null, /*VALORUNITARIO*/ 0,  /*VALORFINANCEIRO*/ 0,
                                    /*IMPRIMEMOV*/ null, /*CODCCUSTO*/ null, /*FLAGREPASSE*/ null, /*ALIQORDENACAO*/ 0,
                                    /*QUANTIDADEORIGINAL*/ quantidade,
                                    /*IDNAT*/ Itens.Rows[i]["IDNAT"].ToString(), /*FLAG*/ 0, /*CHAPA*/ null, /*INICIO*/ null, /*TERMINO*/ null, /*PREVINICIO*/ null, /*STATUS*/ null, /*BLOCK*/ null,
                                    /*FLAGREFATURAMENTO*/ null, /*IDCNTDESTINO*/ null, /*NSEQITMCNTDEST*/ null, /*FATORCONVUND*/ 0, /*IDPRJ*/ null, /*IDTRF*/ null,
                                    /*VALORTOTALITEM*/ (quantidade * precoUnitario), /*VALORCODIGOPRD*/ null, /*TIPOCODIGOPRD*/ null, /*QTDUNDPEDIDO*/ null, /*TRIBUTACAOECF*/ null,
                                    /*CODFILIAL*/ movimento.CodFilial, /*CODDEPARTAMENTO*/ null,
                                    /*IDPRDCOMPOSTO*/ (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]),
                                    /*QUANTIDADESEPARADA*/ 0, /*PERCENTCOMISSAO*/ 0, /*INDICENCM*/ null, /*NCM*/ null, /*CODRPR*/ null, /*COMISSAOREPRES*/ 0,
                                    /*NSEQITMCNTMEDICAO*/ null, /*VALORESCRITURACAO*/ 0, /*VALORFINPEDIDO*/ 0, /*VALORFRETECTRC*/ null, /*VALOROPFRM1*/ 0, /*VALOROPFRM2*/ 0,
                                    /*IDOBJOFICINA*/ null, /*PRECOEDITADO*/ 0, /*QTDEVOLUMEUNITARIO*/ 0, /*IDGRD*/ null,
                                    /*CODVEN1*/ movimento.CodVen1, /*CODLOCALBN*/ null, /*REGISTROEXPORTACAO*/ null, /*DATARE*/ null, /*PRECOTOTALEDITADO*/ null, /*CST*/ null,
                                    /*VALORDESCCONDICONALITM*/ 0, /*VALORDESPCONDICIONALITM*/ 0, /*DATAORCAMENTO*/ null, /*CODTBORCAMENTO*/ null, /*RATEIOFRETE*/ rateioFrete,
                                    /*RATEIOSEGURO*/ null, /*RATEIODESC*/ rateioDesc, /*RATEIODESP*/ (percentItem*movimento.ValorDesp), /*RATEIOEXTRA1*/ null, /*RATEIOEXTRA2*/ null, /*RATEIOFRETECTRC*/ null,
                                    /*RATEIODEDMAT*/ null, /*RATEIODEDSUB*/ null, /*RATEIODEDOUT*/ null, /*IDCLASSIFENERGIACOMUNIC*/ null, /*VALORUNTORCAMENTO*/ 0,
                                    /*VALSERVICONFE*/ 0, /*CODLOC*/ movimento.CodLoc, /*VALORBEM*/ 0,
                                    /*VALORLIQUIDO*/ (quantidade * precoUnitario),
                                    /*CODIGOCODIF*/ null, /*CODMUNSERVICO*/ null, /*CODETDMUNSERV*/ null, /*RATEIOCCUSTODEPTO*/ null, /*CUSTOREPOSICAO*/ null,
                                    /*CUSTOREPOSICAOB*/ null, /*VALORFINTERCEIROS*/ null, /*VALORFINANCGERENCIAL*/ null, /*CODIGOSERVICO*/ null, /*VALORUNITGERENCIAL*/ null,
                                    /*IDINTEGRACAO*/ null, /*IDTABPRECO*/ null,
                                    /*VALORBRUTOITEM*/ (quantidade * precoUnitario),
                                    /*VALORBRUTOITEMORIG*/ (quantidade * precoUnitario),
                                    /*CODCOLTBORCAMENTO*/ null, /*CODPUBLIC*/ null, /*QUANTIDADETOTAL*/ quantidade,
                                    /*PRODUTOSUBSTITUTO*/ 0, /*CODTBGRUPOORC*/ null, /*PRECOUNITARIOSELEC*/ Itens.Rows[i]["PRECOUNITARIOSELEC"].ToString(), /*VALORRATEIOLAN*/ null,
                                    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ conn.GetDateTime()});


                                //                                sSql = @"INSERT INTO TITMMOVHISTORICO (CODCOLIGADA, IDMOV, NSEQITMMOV, HISTORICOCURTO, HISTORICOLONGO, 
                                //                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                //                                    VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :HISTORICOCURTO, :HISTORICOLONGO, 
                                //                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                                sSql = @"INSERT INTO TITMMOVHISTORICO (CODCOLIGADA, IDMOV, NSEQITMMOV, HISTORICOCURTO, HISTORICOLONGO, 
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " Grava TITMMOVHISTORICO");
                                //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                //    /*HISTORICOCURTO*/ null, /*HISTORICOLONGO*/ Itens.Rows[i]["HISTORICOLONGO"].ToString(),
                                //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                                conn.ExecQuery(sSql, new object[]{/*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                    /*HISTORICOCURTO*/ null, /*HISTORICOLONGO*/ Itens.Rows[i]["HISTORICOLONGO"].ToString(),
                                    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ conn.GetDateTime()});


                                //                                sSql = @"INSERT INTO TITMMOVCOMPL (CODCOLIGADA, IDMOV, NSEQITMMOV, PRODPRICIPAL, SEQUENCIAL, TIPOMAT, XPED,
                                //                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, PRDPADRAO, OBSPRDPADRAO)
                                //                                    VALUES(:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :PRODPRICIPAL, :SEQUENCIAL, :TIPOMAT, :XPED,
                                //                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :PRDPADRAO, :OBSPRDPADRAO)";

                                sSql = @"INSERT INTO TITMMOVCOMPL (CODCOLIGADA, IDMOV, NSEQITMMOV, XPED,
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, APLICPROD, TIPO, VALORPINTURA, TIPOPINTURA, VALORDESCORIGINAL, CORPINTURA, PRECOTABELA)
                                    VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " Grava TITMMOVCOMPL");
                                //DBS.QueryExec(sSql,
                                //    /*CODCOLIGADA*/ movimento.CodColigada,
                                //    /*IDMOV*/ movimento.IdMov,
                                //    /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                //    /*PRODPRICIPAL*/ Itens.Rows[i]["PRODPRICIPAL"].ToString(),
                                //    /*SEQUENCIAL*/ Itens.Rows[i]["SEQUENCIAL"].ToString(),
                                //    /*TIPOMAT*/ Itens.Rows[i]["TIPOMAT"].ToString(),
                                //    /*XPED*/ movimento.NumeroOrdem,
                                //    /*RECCREATEDBY*/ movimento.CodUsuario,
                                //    /*RECCREATEDON*/ DBS.ServerDate(),
                                //    /*RECMODIFIEDBY*/ movimento.CodUsuario,
                                //    /*RECMODIFIEDON*/ DBS.ServerDate(),
                                //    /*PRDPADRAO*/ Itens.Rows[i]["PRDPADRAO"].ToString(),
                                //    /*OBSPRDPADRAO*/ Itens.Rows[i]["OBSPRDPADRAO"].ToString());


                                conn.ExecQuery(sSql, new object[]{
                                    /*CODCOLIGADA*/ movimento.CodColigada,
                                    /*IDMOV*/ movimento.IdMov,
                                    /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                    /*XPED*/ movimento.NumeroOrdem,
                                    /*RECCREATEDBY*/ movimento.CodUsuario,
                                    /*RECCREATEDON*/ conn.GetDateTime(),
                                    /*RECMODIFIEDBY*/ movimento.CodUsuario,
                                    /*RECMODIFIEDON*/ conn.GetDateTime(),
                                    /*APLICPROD */ Itens.Rows[i]["APLICPROD"].ToString(),
                                    /*TIPO*/ Itens.Rows[i]["TIPO"].ToString(),
                                    /*VALORPINTURA*/ Itens.Rows[i]["VALORPINTURA"].ToString().Replace(',','.'),
                                    /*TIPOPINTURA*/ Itens.Rows[i]["TIPOPINTURA"].ToString(),
                                    /*VALORDESCORIGINAL*/ Itens.Rows[i]["VALORORIGINAL"].ToString().Replace(',','.'),
                                    /*CORPINTURA*/ Itens.Rows[i]["CORPINTURA"].ToString(),
                                    /*PRECOTABELA*/ Convert.ToDecimal(Itens.Rows[i]["PRECOTABELA"])});

                                sSql = @"insert into TITMMOVFISCAL (CODCOLIGADA, IDMOV, NSEQITMMOV, QTDECONTRATADA, RECCREATEDBY, RECCREATEDON, NUMPEDIDO, NUMITEMPEDIDO)
                                                                values (/*CODCOLIGADA*/ ?, 
                                                                        /*IDMOV*/ ?, 
                                                                        /*NSEQITMMOV*/ ?, 
                                                                        /*QTDECONTRATADA*/ 0.0, 
                                                                        /*RECCREATEDBY*/ ?, 
                                                                        /*RECCREATEDON*/ ?, 
                                                                        /*NUMPEDIDO*/ ?, 
                                                                        /*NUMITEMPEDIDO*/ ?)";

                                string nIPedido = Itens.Rows[i]["NUMITEMPEDIDO"].ToString();

                                Object NIP;

                                if (String.IsNullOrWhiteSpace(nIPedido))
                                {
                                    NIP = DBNull.Value;
                                }
                                else
                                {
                                    NIP = nIPedido;
                                }

                                conn.ExecTransaction(sSql, new object[]{
                                /*CODCOLIGADA*/ movimento.CodColigada,
                                /*IDMOV*/ movimento.IdMov,
                                /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*RECCREATEDBY*/ movimento.CodUsuario,
                                /*RECCREATEDON*/ conn.GetDateTime(),
                                /*NUMPEDIDO*/ Itens.Rows[i]["NUMPEDIDO"].ToString(),
                                /*NUMITEMPEDIDO*/ NIP});


                                //                                sSql = @"INSERT INTO TTRBMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, CODTRB, BASEDECALCULO, ALIQUOTA, VALOR, FATORREDUCAO, FATORSUBSTTRIB, 
                                //                                            BASEDECALCULOCALCULADA, EDITADO, VLRISENTO, CODRETENCAO, TIPORECOLHIMENTO, CODTRBBASE, PERCDIFERIMENTOPARCIALICMS, 
                                //                                            SITTRIBUTARIA, MODALIDADEBC, ALIQUOTAPORVALOR, CODUNDREFERENCIA, BASECHEIA, 
                                //                                            RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, TIPORENDIMENTO, FORMATRIBUTACAO) 
                                //                                        VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :CODTRB, :BASEDECALCULO, :ALIQUOTA, :VALOR, :FATORREDUCAO, :FATORSUBSTTRIB, 
                                //                                            :BASEDECALCULOCALCULADA, :EDITADO, :VLRISENTO, :CODRETENCAO, :TIPORECOLHIMENTO, :CODTRBBASE, :PERCDIFERIMENTOPARCIALICMS, 
                                //                                            :SITTRIBUTARIA, :MODALIDADEBC, :ALIQUOTAPORVALOR, :CODUNDREFERENCIA, :BASECHEIA, 
                                //                                            :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :TIPORENDIMENTO, :FORMATRIBUTACAO)";

                                sSql = @"INSERT INTO TTRBMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, CODTRB, BASEDECALCULO, ALIQUOTA, VALOR, FATORREDUCAO, FATORSUBSTTRIB, 
                                            BASEDECALCULOCALCULADA, EDITADO, VLRISENTO, CODRETENCAO, TIPORECOLHIMENTO, CODTRBBASE, PERCDIFERIMENTOPARCIALICMS, 
                                            SITTRIBUTARIA, MODALIDADEBC, ALIQUOTAPORVALOR, CODUNDREFERENCIA, BASECHEIA, 
                                            RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, TIPORENDIMENTO, FORMATRIBUTACAO) 
                                        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                                #region Calculo do Trbiuto
                                string sql = String.Empty;
                                Decimal BaseCalculo = 0;
                                Decimal Aliquota = 0;
                                Decimal ValorTributo = 0;
                                DataTable dtTributo = new DataTable();
                                Tributo IPI = new Tributo();
                                Tributo PIS = new Tributo();
                                Tributo COFINS = new Tributo();
                                Tributo ICMS = new Tributo();
                                Tributo ICMSST = new Tributo();

                                List<Tributo> Tributos = new List<Tributo>();

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de IPI");
                                #region Calculo do Trbiuto IPI

                                Aliquota = Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]);

                                if (Itens.Rows[i]["IDPRDCOMPOSTO"] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]) == 0)
                                    {
                                        BaseCalculo = (Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"])) - rateioDesc + rateioFrete;
                                        ValorTributo = new Util().Arredonda((BaseCalculo * Aliquota) / 100);
                                    }
                                }
                                else
                                {
                                    BaseCalculo = (Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"])) - rateioDesc + rateioFrete;
                                    ValorTributo = new Util().Arredonda((BaseCalculo * Aliquota) / 100);
                                }

                                IPI.TipoTributo = "IPI";
                                IPI.BaseDoCalculo = BaseCalculo;
                                IPI.ValorDaAliquota = Aliquota;
                                IPI.ValorDoTributo = ValorTributo;

                                Tributos.Add(IPI);

                                #endregion

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de PIS");
                                #region Calculo do Trbiuto PIS
                                sql = String.Format(@"select  (isnull(TITMMOV.VALORTOTALITEM, 0) - 
		                                                        isnull(TITMMOV.RATEIODESC, 0) + 
		                                                        isnull(TITMMOV.RATEIODESP, 0) + 
		                                                        isnull(TITMMOV. RATEIOFRETE, 0)) as 'BASECALCULO',
		                                                        DTRBNATUREZA.ALIQTRB as 'ALIQUOTA',
		                                                        NSEQITMMOV 

                                                        from TITMMOV (NOLOCK)

                                                        inner join DTRBNATUREZA (NOLOCK)
                                                        on DTRBNATUREZA.IDNAT = TITMMOV.IDNAT
                                                        and DTRBNATUREZA.CODCOLIGADA = TITMMOV.CODCOLIGADA

                                                        where TITMMOV.CODCOLIGADA = '{0}'
                                                        and IDMOV = '{1}'
                                                        and TITMMOV.NSEQITMMOV = '{2}'
                                                        and DTRBNATUREZA.CODTRB = 'PIS'", AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                                dtTributo = MetodosSQL.GetDT(sql);

                                if (dtTributo.Rows.Count == 0)
                                {
                                    PIS.TipoTributo = "PIS";
                                }

                                foreach (DataRow row in dtTributo.Rows)
                                {
                                    PIS.TipoTributo = "PIS";
                                    PIS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString());
                                    PIS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString());
                                    PIS.ValorDoTributo = PIS.BaseDoCalculo * (PIS.ValorDaAliquota / 100);
                                }

                                Tributos.Add(PIS);


                                #endregion

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de COFINS");
                                #region Calculo do Trbiuto COFINS
                                sql = String.Format(@"select  (isnull(TITMMOV.VALORTOTALITEM, 0) - 
		                                                        isnull(TITMMOV.RATEIODESC, 0) + 
		                                                        isnull(TITMMOV.RATEIODESP, 0) + 
		                                                        isnull(TITMMOV. RATEIOFRETE, 0)) as 'BASECALCULO',
		                                                        DTRBNATUREZA.ALIQTRB as 'ALIQUOTA',
		                                                        NSEQITMMOV 

                                                        from TITMMOV (NOLOCK)

                                                        inner join DTRBNATUREZA (NOLOCK)
                                                        on DTRBNATUREZA.IDNAT = TITMMOV.IDNAT
                                                        and DTRBNATUREZA.CODCOLIGADA = TITMMOV.CODCOLIGADA

                                                        where TITMMOV.CODCOLIGADA = '{0}'
                                                        and IDMOV = '{1}'
                                                        and TITMMOV.NSEQITMMOV = '{2}'
                                                        and DTRBNATUREZA.CODTRB = 'COFINS'", AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                                dtTributo = MetodosSQL.GetDT(sql);

                                if (dtTributo.Rows.Count == 0)
                                {
                                    COFINS.TipoTributo = "COFINS";
                                }

                                foreach (DataRow row in dtTributo.Rows)
                                {
                                    COFINS.TipoTributo = "COFINS";
                                    COFINS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString());
                                    COFINS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString());
                                    COFINS.ValorDoTributo = COFINS.BaseDoCalculo * (COFINS.ValorDaAliquota / 100);
                                }

                                Tributos.Add(COFINS);


                                #endregion

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de ICMS");

                                #region Calculo do Trbiuto ICMS

                                sql = String.Format(@"SELECT FATORICMS FROM DREGRAICMS (NOLOCK) WHERE IDREGRAICMS IN (
		                                                SELECT IDREGRAICMS FROM DCFOP (NOLOCK) WHERE CODCOLIGADA = 1 AND IDNAT = '{0}')
		                                                AND CODCOLIGADA = 1", Itens.Rows[i]["IDNAT"].ToString());

                                sql = String.Format(@"select CASE 
	
	                                                                WHEN 

		                                                                (SELECT FATORICMS FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
		                                                                AND CODCOLIGADA = 1) > 0 
		
		                                                                AND 
		
		                                                                (SELECT BASEICMSCOMIPI FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
		                                                                AND CODCOLIGADA = 1) = 1
		
	                                                                THEN
		                                                                isnull((
		                                                                (TITMMOV.VALORTOTALITEM + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) 
		                                                                - 
		                                                                ((TITMMOV.VALORTOTALITEM + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * '{1}' /100 )
		                                                                ),0)
		
	                                                                ELSE
		
		                                                                CASE
		
		                                                                WHEN
		
			                                                                (SELECT BASEICMSCOMIPI FROM DREGRAICMS WHERE IDREGRAICMS IN (
			                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
			                                                                AND CODCOLIGADA = 1) = 1		
		
		                                                                THEN
			
			                                                                isnull(((TITMMOV.VALORTOTALITEM +  '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE)),0)
		
		                                                                ELSE
		
			                                                                isnull(((TITMMOV.VALORTOTALITEM - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE)),0)

		                                                                END
                                                                END as 'BASECALCULO',
                                                                (SELECT ALIQICMS FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                    SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
                                                                    AND CODCOLIGADA = 1) as 'ALIQUOTA'

                                                                from TITMMOV (NOLOCK)

                                                                where CODCOLIGADA = '{2}'
                                                                and IDMOV = '{3}'
                                                                and NSEQITMMOV = '{4}'", IPI.ValorDoTributo.ToString().Replace(",", "."), MetodosSQL.GetField(sql, "FATORICMS").Replace(",", "."), AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]), Itens.Rows[i]["IDNAT"].ToString());

                                dtTributo = MetodosSQL.GetDT(sql);

                                foreach (DataRow row in dtTributo.Rows)
                                {
                                    ICMS.TipoTributo = "ICMS";
                                    ICMS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString());
                                    ICMS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString());
                                    ICMS.ValorDoTributo = new Util().Arredonda(ICMS.BaseDoCalculo * (ICMS.ValorDaAliquota / 100));
                                }

                                Tributos.Add(ICMS);

                                #endregion

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " Cálculo de ICMSST");

                                #region Calculo do Trbiuto ICMSST

                                sql = String.Format(@"SELECT FATORREDST, ALIQSUBST, FATORSUBST FROM DREGRAICMS WHERE IDREGRAICMS IN (
                                                    SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{0}')
                                                    AND CODCOLIGADA = 1", Itens.Rows[i]["IDNAT"].ToString());

                                dtTributo = MetodosSQL.GetDT(sql);


                                #region Query ICMS-ST
                                foreach (DataRow row in dtTributo.Rows)
                                {
                                    sql = String.Format(@"SELECT
CASE 
WHEN

(SELECT NCONTRIB 
	FROM TMOVCOMPL (nolock)
	WHERE CODCOLIGADA = 1 AND IDMOV = {5} /*IDMOV*/) = 0

THEN 
	
	0
   
ELSE 
			CASE 
			WHEN
				(SELECT 1
				FROM   DCFOP,
					   DREGRAICMS
				WHERE  DCFOP.CODCOLIGADA = DREGRAICMS.CODCOLIGADA
					   AND DCFOP.IDREGRAICMS = DREGRAICMS.IDREGRAICMS
					   AND DREGRAICMS.TIPOOPERACAOTRIBUTARIA IN ( 1, 2 )
					   AND DREGRAICMS.UTIBCDIFALICMSDENTRO IN ( 1, 2 )
					   AND DCFOP.CODCOLIGADA = 1 /*COLIGADA*/
					   AND DCFOP.IDNAT = {6})=1

             THEN (
					CASE
					WHEN 
						(SELECT 1
						  FROM DCFOP, DREGRAICMS
						 WHERE DCFOP.CODCOLIGADA = DREGRAICMS.CODCOLIGADA
						   AND DCFOP.IDREGRAICMS = DREGRAICMS.IDREGRAICMS
						   AND DREGRAICMS.TIPOOPERACAOTRIBUTARIA IN (1,2)
						   AND DREGRAICMS.UTIBCDIFALICMSDENTRO = 1
						   AND DCFOP.CODCOLIGADA = 1 /*COLIGADA*/
						   AND DCFOP.IDNAT = {6})=1       

					THEN 
							CASE
							WHEN 
							(SELECT FATORREDST FROM DREGRAICMS WHERE IDREGRAICMS IN (
							SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 /*COLIGADA*/ AND IDNAT = {6})
							AND CODCOLIGADA = 1 /*COLIGADA*/) > 0                

							THEN 
							/*
								(((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) -   
								((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORREDST' , 'V')/100) ))) +    
								((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) -   
								((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * TABNATITEM ('FATORREDST' , 'V')/100 ))) * (TABNATITEM ('FATORSUBST' , 'V')/100))) - LVL('ICMS') ) / (1 - ((TABNATITEM ('ALIQSUBST' , 'V'))/100))                
							*/
								((((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
								((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({3} /100) ))) +    
								((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
								((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * {3} /100 ))) * ({2} /100))) - {1} ) / (1 - (({4} )/100)))

							ELSE 
							/*
								 (((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) +     
								 ((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORSUBST' , 'V')/100)))  - LVL('ICMS') ) / (1 - ((TABNATITEM ('ALIQSUBST' , 'V'))/100))            
							*/
								 ((((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) +    
								 ((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({2} /100)))  - {1} ) / (1 - (({4})/100)))

							
							
							END       

					ELSE (
								CASE
								WHEN 
								(SELECT FATORREDST FROM DREGRAICMS WHERE IDREGRAICMS IN (
								SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 /*COLIGADA*/ AND IDNAT = {6})
								AND CODCOLIGADA = 1 /*COLIGADA*/)>0

								THEN 
								/*
									(((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) - 
									((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORREDST' , 'V')/100) ))) +
									((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) - 
									((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * TABNATITEM ('FATORREDST' , 'V')/100 ))) * (TABNATITEM ('FATORSUBST' , 'V')/100))) - LVL('ICMS') ) / (1 - ((TABNATITEM ('ALIQSUBST' , 'V'))/100))
								*/

									((((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
									((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({3} /100) ))) +    
									((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
									((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * {3} /100 ))) * ({2} /100))) - {1} ) / (1 - (({4} )/100)))



								ELSE 
								/*
									(((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) + 
									((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORSUBST' , 'V')/100)))  - LVL('ICMS') ) / (1 - ((TABNATITEM ('ALIQSUBST' , 'V'))/100))
								*/
									((((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) +    
									((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({2} /100)))  - {1} ) / (1 - (({4})/100)))


								END)    

					END
)

				ELSE 
						CASE
						WHEN 
						(SELECT FATORREDST FROM DREGRAICMS WHERE IDREGRAICMS IN (
						SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 /*COLIGADA*/ AND IDNAT = {6})
						AND CODCOLIGADA = 1 /*COLIGADA*/)>0

                       THEN 
					   /*
							(((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) - 
							((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORREDST' , 'V')/100) ))) +
							((((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) - 
							((TABITM ('VALORTOTALITEM' , 'V')  + LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * TABNATITEM ('FATORREDST' , 'V')/100 ))) * (TABNATITEM ('FATORSUBST' , 'V')/100))
						*/

							((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
							((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({3} /100) ))) +    
							((((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
							((TITMMOV.VALORTOTALITEM  + {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * {3} /100 ))) * ({2} /100)))


                       ELSE 
					   /*
							(TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) + 
							((TABITM ('VALORTOTALITEM' , 'V') +  LVL ('IPI') - TABITM('RATEIODESC','V') + TABITM('RATEIODESP','V') + TABITM('RATEIOFRETE','V') + TABITM('RATEIOSEGURO','V')) * (TABNATITEM ('FATORSUBST' , 'V')/100))
						*/

						((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) +    
						((TITMMOV.VALORTOTALITEM +  {0} - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ({2} /100)))

					   END

				END

END
AS'BASECALCULO'
FROM TITMMOV (NOLOCK) WHERE CODCOLIGADA = 1 /*COLIGADA*/ AND IDMOV = {5} AND NSEQITMMOV = {7}", /*VALORIPI*/ IPI.ValorDoTributo.ToString().Replace(",", "."),
                                       /*VALORICMS*/ ICMS.ValorDoTributo.ToString().Replace(",", "."),
                                       /*FATORSUBST*/ row["FATORSUBST"].ToString().Replace(",", "."),
                                       /*FATORREDST*/ row["FATORREDST"].ToString().Replace(",", "."),
                                       /*ALIQSUBST*/ row["ALIQSUBST"].ToString().Replace(",", "."),
                                       /*IDMOV*/ movimento.IdMov,
                                       /*IDNAT*/ Convert.ToInt32(Itens.Rows[i]["IDNAT"]),
                                       /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                                    ICMSST.ValorDaAliquota = Convert.ToDecimal(row["ALIQSUBST"].ToString());
                                }
                                #endregion

                                dtTributo = MetodosSQL.GetDT(sql);

                                foreach (DataRow row in dtTributo.Rows)
                                {
                                    ICMSST.TipoTributo = "ICMSST";
                                    ICMSST.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString());
                                    ICMSST.ValorDoTributo = new Util().Arredonda(ICMSST.BaseDoCalculo * (ICMSST.ValorDaAliquota / 100));
                                }

                                Tributos.Add(ICMSST);

                                #endregion

                                sPoint = string.Concat("Alteração: Item ", (i + 1), " Grava TTRBMOV");
                                //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                //    /*CODTRB*/ "IPI", /*BASEDECALCULO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                //    /*ALIQUOTA*/ Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]),
                                //    /*VALOR*/ ValorTributo, /*FATORREDUCAO*/ 0, /*FATORSUBSTTRIB*/ 0,
                                //    /*BASEDECALCULOCALCULADA*/ 0, /*EDITADO*/ 0, /*VLRISENTO*/ null, /*CODRETENCAO*/ "0561", /*TIPORECOLHIMENTO*/ null, /*CODTRBBASE*/ null,
                                //    /*PERCDIFERIMENTOPARCIALICMS*/ 0, /*SITTRIBUTARIA*/ null, /*MODALIDADEBC*/ null, /*ALIQUOTAPORVALOR*/ null, /*CODUNDREFERENCIA*/ null,
                                //    /*BASECHEIA*/ 0,
                                //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(),
                                //    /*TIPORENDIMENTO*/ null, /*FORMATRIBUTACAO*/ null);

                                //List<string> tributos = new List<string>();
                                //tributos.Add("IPI");
                                //tributos.Add("ICM-SF");
                                //tributos.Add("PIS-SF");
                                //tributos.Add("COF-SF");
                                //tributos.Add("ICMS");
                                //tributos.Add("ICMSST");

                                if (movimento.SEMIMPOSTOS == 0)
                                {
                                    foreach (Tributo t in Tributos)
                                    {
                                        t.ValorDoTributo = Math.Round(t.ValorDoTributo, 2);
                                        t.BaseDoCalculo = Math.Round(t.BaseDoCalculo, 2);

                                        conn.ExecTransaction(sSql, new object[]{ /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                    /*CODTRB*/ t.TipoTributo, /*BASEDECALCULO*/ t.BaseDoCalculo,
                                    /*ALIQUOTA*/ t.ValorDaAliquota,
                                    /*VALOR*/ t.ValorDoTributo, /*FATORREDUCAO*/ 0, /*FATORSUBSTTRIB*/ 0,
                                    /*BASEDECALCULOCALCULADA*/ 0, /*EDITADO*/ 0, /*VLRISENTO*/ null, /*CODRETENCAO*/ null, /*TIPORECOLHIMENTO*/ null, /*CODTRBBASE*/ null,
                                    /*PERCDIFERIMENTOPARCIALICMS*/ 0, /*SITTRIBUTARIA*/ null, /*MODALIDADEBC*/ null, /*ALIQUOTAPORVALOR*/ null, /*CODUNDREFERENCIA*/ null,
                                    /*BASECHEIA*/ 0,
                                    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ conn.GetDateTime(),
                                    /*TIPORENDIMENTO*/ null, /*FORMATRIBUTACAO*/ null});
                                    }
                                }
                            }

                            #endregion

                            #region Valor Bruto do Movimento
                            sPoint = string.Concat("Alteração: Valor Bruto do Movimento]");
                            //                            sSql = @"
                            //        SELECT 
                            //        (
                            //        ISNULL((
                            //        SELECT SUM(QUANTIDADE * PRECOUNITARIO)
                            //        FROM TITMMOV
                            //        WHERE CODCOLIGADA = :CODCOLIGADA
                            //        AND IDMOV = :IDMOV
                            //        ),0)
                            //
                            //        +
                            //
                            //        ISNULL((
                            //        SELECT SUM(VALOR)
                            //        FROM TTRBMOV
                            //        WHERE CODTRB = 'IPI'
                            //        AND CODCOLIGADA = :CODCOLIGADA
                            //        AND IDMOV = :IDMOV
                            //        ),0)
                            //        ) VALOR
                            //
                            //        FROM GCOLIGADA 
                            //        WHERE CODCOLIGADA = :CODCOLIGADA";

                            sSql = @"SELECT 
SUM(VALORTOTALITEM)
FROM TITMMOV
WHERE CODCOLIGADA = ?
AND IDMOV = ?";

                            //movimento.ValorBruto = Convert.ToDecimal(DBS.QueryValue(0, sSql, movimento.CodColigada, movimento.IdMov));
                            movimento.ValorBruto = Convert.ToDecimal(conn.ExecGetField(0, sSql, new object[] { movimento.CodColigada, movimento.IdMov }));

                            #endregion

                            #region Despesa
                            sPoint = string.Concat("Alteração: [Despesa]");
                            if (movimento.ValorDesp != null)
                            {
                                if (movimento.ValorDesp != 0)
                                {
                                    movimento.PercentualDesp = new Util().Arredonda(((movimento.ValorDesp / movimento.ValorBruto) * 100));
                                }
                                else
                                {
                                    if (movimento.PercentualDesp != null)
                                    {
                                        if (movimento.PercentualDesp != 0)
                                        {
                                            movimento.ValorDesp = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualDesp) / 100));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (movimento.PercentualDesp != null)
                                {
                                    if (movimento.PercentualDesp != 0)
                                    {
                                        movimento.ValorDesp = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualDesp) / 100));
                                    }
                                }
                            }
                            #endregion

                            #region Seguro
                            sPoint = string.Concat("Alteração: [Seguro]");
                            if (movimento.ValorSeguro != null)
                            {
                                if (movimento.ValorSeguro != 0)
                                {
                                    movimento.PercentualSeguro = new Util().Arredonda(((movimento.ValorSeguro / movimento.ValorBruto) * 100));
                                }
                                else
                                {
                                    if (movimento.PercentualSeguro != null)
                                    {
                                        if (movimento.PercentualSeguro != 0)
                                        {
                                            movimento.ValorSeguro = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualSeguro) / 100));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (movimento.PercentualSeguro != null)
                                {
                                    if (movimento.PercentualSeguro != 0)
                                    {
                                        movimento.ValorSeguro = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualSeguro) / 100));
                                    }
                                }
                            }
                            #endregion

                            #region Frete
                            sPoint = string.Concat("Alteração: [Frete]");
                            if (movimento.ValorFrete != null)
                            {
                                if (movimento.ValorFrete != 0)
                                {
                                    movimento.PercentualFrete = new Util().Arredonda(((movimento.ValorFrete / movimento.ValorBruto) * 100));
                                }
                                else
                                {
                                    if (movimento.PercentualFrete != null)
                                    {
                                        if (movimento.PercentualFrete != 0)
                                        {
                                            movimento.ValorFrete = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualFrete) / 100));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (movimento.PercentualFrete != null)
                                {
                                    if (movimento.PercentualFrete != 0)
                                    {
                                        movimento.ValorFrete = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualFrete) / 100));
                                    }
                                }
                            }
                            #endregion

                            #region Desconto
                            sPoint = string.Concat("Alteração: [Desconto]");
                            if (movimento.ValorDesc != null)
                            {
                                if (movimento.ValorDesc != 0)
                                {
                                    //movimento.PercentualDesc = new Util().Arredonda(((movimento.ValorDesc / movimento.ValorBruto) * 100));
                                    movimento.PercentualDesc = ((movimento.ValorDesc / movimento.ValorBruto) * 100);
                                }
                                else
                                {
                                    if (movimento.PercentualDesc != null)
                                    {
                                        if (movimento.PercentualDesc != 0)
                                        {
                                            //movimento.ValorDesc = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualDesc) / 100));
                                            movimento.ValorDesc = ((movimento.ValorBruto * movimento.PercentualDesc) / 100);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (movimento.PercentualDesc != null)
                                {
                                    if (movimento.PercentualDesc != 0)
                                    {
                                        movimento.ValorDesc = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualFrete) / 100));
                                    }
                                }
                            }
                            #endregion

                            #region Extra1
                            sPoint = string.Concat("Alteração: [Extra1]");
                            if (movimento.ValorExtra1 != null)
                            {
                                if (movimento.ValorExtra1 != 0)
                                {
                                    movimento.PercentualExtra1 = new Util().Arredonda(((movimento.ValorExtra1 / movimento.ValorBruto) * 100));
                                }
                                else
                                {
                                    if (movimento.PercentualExtra1 != null)
                                    {
                                        if (movimento.PercentualExtra1 != 0)
                                        {
                                            movimento.ValorExtra1 = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualExtra1) / 100));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (movimento.PercentualExtra1 != null)
                                {
                                    if (movimento.PercentualExtra1 != 0)
                                    {
                                        movimento.ValorExtra1 = new Util().Arredonda(((movimento.ValorBruto * movimento.PercentualExtra1) / 100));
                                    }
                                }
                            }
                            #endregion

                            #region Valor Outros do Movimento
                            sPoint = string.Concat("Alteração: [Valor Outros do Movimento]");
                            movimento.ValorOutros = movimento.ValorBruto;

                            #endregion

                            #region Valor Liquido do Movimento
                            sPoint = string.Concat("Alteração: [Valor Liquido do Movimento]");
                            //movimento.ValorLiquido = movimento.ValorBruto + Convert.ToDecimal(movimento.ValorDesp) + Convert.ToDecimal(movimento.ValorSeguro) - Convert.ToDecimal(movimento.ValorDesc) + Convert.ToDecimal(movimento.ValorFrete) - Convert.ToDecimal(movimento.ValorExtra1);
                            sSql = @"select ROUND((((VALOR_BRUTO + VALORDESP + IPI - VALORDESC + VALORFRETE)) +  ICMSST),2) as 'LIQUIDO' from (
SELECT	
                                       
--(((VALOR BRUTO + VALOR DESPESA + VALOR IPI - VALOR DESCONTO + VALOR FRETE)) +  VALOR ICMSST)
                                        ISNULL((SELECT	
                                        SUM(ROUND(VALORTOTALITEM,2))
                                        FROM TITMMOV
                                        WHERE CODCOLIGADA = TM.CODCOLIGADA
                                        AND IDMOV = TM.IDMOV),0) VALOR_BRUTO,

	                                    TM.VALORDESP,

                                        ISNULL((SELECT
                                        SUM(ROUND(VALOR,2))
                                        FROM	TTRBMOV
                                        WHERE	TTRBMOV.CODCOLIGADA = TM.CODCOLIGADA
                                        AND	TTRBMOV.IDMOV = TM.IDMOV
                                        AND	TTRBMOV.CODTRB = 'IPI'),0) IPI,

										TM.VALORDESC,

	                                    TM.VALORFRETE,

                                        (SELECT 
	                                                                                    CASE 
		                                                                                    WHEN (ISNULL(ROUND(ST.VALOR,2), 0) - ISNULL(ROUND(ICMS.VALOR,2), 0)) < 0 
		                                                                                    THEN 0 
		                                                                                    ELSE (ISNULL(ROUND(ST.VALOR,2), 0) - ISNULL(ROUND(ICMS.VALOR,2), 0)) 
	                                                                                    END as 'ICMSST'
                                                                                    FROM TMOV,

                                                                                    (SELECT	SUM (ROUND(TTRBMOV.VALOR,2)) AS VALOR
                                                                                         FROM	TTRBMOV
                                                                                         WHERE	TTRBMOV.CODCOLIGADA = TM.CODCOLIGADA
                                                                                         AND	TTRBMOV.IDMOV = TM.IDMOV
                                                                                         AND	TTRBMOV.CODTRB = 'ICMSST') ST,

                                                                                    (SELECT	SUM (ROUND(TTRBMOV.VALOR,2)) AS VALOR
                                                                                         FROM	TTRBMOV
                                                                                         WHERE	TTRBMOV.CODCOLIGADA = TM.CODCOLIGADA
                                                                                         AND	TTRBMOV.IDMOV = TM.IDMOV
                                                                                         AND	TTRBMOV.CODTRB = 'ICMS') ICMS

                                                                                    WHERE CODCOLIGADA = TM.CODCOLIGADA
                                                                                    AND   IDMOV = TM.IDMOV) as 'ICMSST'

                                        FROM TMOV TM
                                        WHERE CODCOLIGADA = ?
                                        AND   IDMOV = ?) X";

                            //movimento.ValorBruto = Convert.ToDecimal(DBS.QueryValue(0, sSql, movimento.CodColigada, movimento.IdMov));
                            movimento.ValorLiquido = Convert.ToDecimal(conn.ExecGetField(0, sSql, new object[] { movimento.CodColigada, movimento.IdMov }));
                            //movimento.ValorLiquido = Convert.ToDecimal(conn.ExecGetField(0, sSql, new object[] { movimento.ValorBruto, movimento.ValorDesp, movimento.ValorDesc, movimento.ValorFrete, movimento.CodColigada, movimento.IdMov }));

                            #endregion

                            #region Atualiza Valores do Movimento
                            sPoint = string.Concat("Alteração: [Atualiza Valores do Movimento]");
                            //                            sSql = @"UPDATE TMOV SET 
                            //                                    VALORBRUTO = :VALORBRUTO, VALORLIQUIDO = :VALORLIQUIDO, VALOROUTROS = :VALOROUTROS, 
                            //                                    PERCENTUALFRETE = :PERCENTUALFRETE, VALORFRETE = :VALORFRETE,
                            //                                    PERCENTUALSEGURO = :PERCENTUALSEGURO, VALORSEGURO = :VALORSEGURO,
                            //                                    PERCENTUALDESC = :PERCENTUALDESC, VALORDESC = :VALORDESC,
                            //                                    PERCENTUALDESP = :PERCENTUALDESP, VALORDESP = :VALORDESP,
                            //                                    PERCENTUALEXTRA1 = :PERCENTUALEXTRA1, VALOREXTRA1 = :VALOREXTRA1,
                            //                                    VALORBRUTOORIG = :VALORBRUTOORIG, VALORLIQUIDOORIG = :VALORLIQUIDOORIG, VALOROUTROSORIG = :VALOROUTROSORIG
                            //                                WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";

                            sSql = @"UPDATE TMOV SET 
                                    VALORBRUTO = ?, VALORLIQUIDO = ?, VALOROUTROS = ?, 
                                    PERCENTUALFRETE = ?, VALORFRETE = ?,
                                    PERCENTUALSEGURO = ?, VALORSEGURO = ?,
                                    PERCENTUALDESC = ?, VALORDESC = ?,
                                    PERCENTUALDESP = ?, VALORDESP = ?,
                                    PERCENTUALEXTRA1 = ?, VALOREXTRA1 = ?,
                                    VALORBRUTOORIG = ?, VALORLIQUIDOORIG = ?, VALOROUTROSORIG = ?, IDNAT = ?
                                WHERE CODCOLIGADA = ? AND IDMOV = ?";

                            // this.DBS.SkipControlColumns = true;
                            //DBS.QueryExec(sSql, movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros,
                            //    movimento.PercentualFrete, movimento.ValorFrete,
                            //    movimento.PercentualSeguro, movimento.ValorSeguro,
                            //    movimento.PercentualDesc, movimento.ValorDesc,
                            //    movimento.PercentualDesp, movimento.ValorDesp,
                            //    movimento.PercentualExtra1, movimento.ValorExtra1,
                            //    movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros,
                            //    movimento.CodColigada, movimento.IdMov);
                            //this.DBS.SkipControlColumns = false;


                            conn.ExecTransaction(sSql, new object[]{movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros,
                               movimento.PercentualFrete, movimento.ValorFrete,
                               movimento.PercentualSeguro, movimento.ValorSeguro,
                               movimento.PercentualDesc, movimento.ValorDesc,
                               movimento.PercentualDesp, movimento.ValorDesp,
                               movimento.PercentualExtra1, movimento.ValorExtra1,
                               movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros, movimento.IdNat,
                               movimento.CodColigada, movimento.IdMov});

                            #endregion

                            sPoint = string.Concat("Alteração: [Exclui ZTITMMOVORCTEMP]");
                            //sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                            sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = ?";
                            // this.DBS.SkipControlColumns = true;
                            //DBS.QueryExec(sSql, movimento.GuidId);
                            // this.DBS.SkipControlColumns = false;
                            conn.ExecTransaction(sSql, movimento.GuidId);
                            conn.Commit();
                            //DBS.Commit();

                            msg.Retorno = movimento.IdMov;
                            msg.Mensagem = "Movimento alterado com sucesso";

                            #endregion

                            #region Exclui Relacionamento Itens

                            sPoint = string.Concat("Exclusão: [Exclui Relacionamento Itens]");

                            DataTable dtItensRelac = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DISTINCT R.IDMOVDESTINO, R.NSEQITMMOVDESTINO, R.CODCOLDESTINO
	                                                                                                           FROM TITMMOVRELAC R
		                                                                                                       INNER JOIN TITMMOV T
		                                                                                                       ON T.IDMOV = R.IDMOVDESTINO AND T.CODCOLIGADA = R.CODCOLDESTINO
	                                                                                                         WHERE R.IDMOVDESTINO = ? AND R.CODCOLDESTINO = ? AND R.NSEQITMMOVDESTINO NOT IN 
                                                                                                            (
                                                                                                                SELECT NSEQITMMOV 
	                                                                                                            FROM TITMMOV 
	                                                                                                            WHERE IDMOV = ? AND CODCOLIGADA = ?
                                                                                                            )", new object[] { movimento.IdMov, movimento.CodColigada, movimento.IdMov, movimento.CodColigada });

                            try
                            {
                                for (int i = 0; i < dtItensRelac.Rows.Count; i++)
                                {
                                    int deleteRelac = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM TITMMOVRELAC WHERE CODCOLDESTINO = ? AND IDMOVDESTINO = ? AND NSEQITMMOVDESTINO = ?", new object[] { dtItensRelac.Rows[i]["CODCOLDESTINO"], dtItensRelac.Rows[i]["IDMOVDESTINO"], dtItensRelac.Rows[i]["NSEQITMMOVDESTINO"] });

                                    if (deleteRelac > 0)
                                    {
                                        continue;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                throw new Exception("Erro ao excluir relação de itens do movimento [" + movimento.Numero + "] com identificador: [" + movimento.IdMov + "].");
                            }                       

                            #endregion

                            sPoint = string.Concat("Alteração: [Desabilita Chave Estrangeira]");
                            contraint.Add("ALTER TABLE TITMMOVRELAC CHECK CONSTRAINT FKTITMMOVRELAC_TITMMOVDESTINO");
                            contraint.Add("ALTER TABLE TITMMOVRELAC CHECK CONSTRAINT FKTITMMOVRELAC_TITMMOVORIGEM");
                            MetodosSQL.ExecMultiple(contraint);
                            contraint.Clear();

                            #endregion
                        }
                        else
                        {
                            throw new Exception("Movimento: [" + movimento.Numero + "] com identificador: [" + movimento.IdMov + "] não pode ser editado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                conn.Rollback();
                DBS.Rollback();

                string sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                this.DBS.SkipControlColumns = true;
                DBS.QueryExec(sSql, movimento.GuidId);
                this.DBS.SkipControlColumns = false;

                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msg.Retorno = 0;
                msg.Mensagem = sPoint + err;
            }



            return msg;
        }

        public Message RequisicaoPecasSave(string usuario, string senha, MovMovimentoPar movimento)
        {
            AppLib.MemoryManager.ReleaseUnusedMemory(false);
            Message msg = new Message();

            try
            {
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                msg = this.Autenticar(usuario, senha);
                if (!(bool)msg.Retorno)
                {
                    msg.Retorno = 0;
                }
                else
                {
                    this.InitServer();

                    if (!ExisteMovimento(movimento.CodColigada, movimento.IdMov))
                    {
                        #region Rotina Automática

                        /*
                        RM.Mov.Movimento.MovMovInclusaoPar oMovMovInclusaoPar = new RM.Mov.Movimento.MovMovInclusaoPar();

                        oMovMovInclusaoPar.CodColigada = movimento.CodColigada;
                        oMovMovInclusaoPar.CodTMv = movimento.CodTMv;
                        oMovMovInclusaoPar.CodFilial = movimento.CodFilial;
                        oMovMovInclusaoPar.CodLoc = movimento.CodLoc;
                        oMovMovInclusaoPar.CodColCfo = movimento.CodColCfo;
                        oMovMovInclusaoPar.CodCfo = movimento.CodCfo;
                        oMovMovInclusaoPar.Serie = movimento.Serie;
                        oMovMovInclusaoPar.DataEmissao = movimento.DataEmissao;
                        oMovMovInclusaoPar.DataEntrega = movimento.DataEntrega;
                        oMovMovInclusaoPar.PrazoEntrega = movimento.PrazoEntrega;
                        oMovMovInclusaoPar.CodRepresentante = movimento.CodRepresentante;
                        oMovMovInclusaoPar.FreteCIFouFOB = movimento.FreteCIFouFOB;
                        oMovMovInclusaoPar.CodTra = movimento.CodTra;

                        oMovMovInclusaoPar.Observacao = movimento.Observacao;
                        oMovMovInclusaoPar.HistoricoLongo = movimento.HistoricoLongo;
                        oMovMovInclusaoPar.NumeroOrdem = movimento.NumeroOrdem;

                        oMovMovInclusaoPar.CodUsuario = movimento.CodUsuario;
                        oMovMovInclusaoPar.CodUsuarioLogado = movimento.CodUsuarioLogado;
                        oMovMovInclusaoPar.DataCriacao = movimento.DataCriacao;
                        oMovMovInclusaoPar.DataMovimento = movimento.DataMovimento;

                        //oMovMovInclusaoPar.CamposComplementares = movimento.CamposComplementares;
                        oMovMovInclusaoPar.CamposComplementares = new Dictionary<string, object>();
                        oMovMovInclusaoPar.CamposComplementares.Add("CLASSREQ", movimento.CLASSREQ);

                        string sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        DataTable Itens = DBS.QuerySelect("ZTITMMOVORCTEMP", sSql, movimento.GuidId);
                        for (int i = 0; i < Itens.Rows.Count; i++)
                        {
                            RM.Mov.Movimento.MovMovItemMovPar oMovMovItemMovPar = new RM.Mov.Movimento.MovMovItemMovPar();

                            oMovMovItemMovPar.CodColigada = Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]);
                            oMovMovItemMovPar.NSeqItmMov = Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]);
                            oMovMovItemMovPar.IdPrd = Convert.ToInt32(Itens.Rows[i]["IDPRD"]);
                            oMovMovItemMovPar.IdPrdComposto = (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]);
                            oMovMovItemMovPar.CodUnd = Itens.Rows[i]["CODUND"].ToString();
                            oMovMovItemMovPar.Quantidade = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]);
                            oMovMovItemMovPar.PrecoUnitario = Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                            oMovMovItemMovPar.HistoricoLongo = Itens.Rows[i]["HISTORICOLONGO"].ToString();

                            RM.Mov.Movimento.MovMovTributoPar oMovMovTributoPar = new RM.Mov.Movimento.MovMovTributoPar();
                            oMovMovTributoPar.CodColigada = oMovMovItemMovPar.CodColigada;
                            oMovMovTributoPar.NSeqItmMov = oMovMovItemMovPar.NSeqItmMov;
                            oMovMovTributoPar.CodTrb = "IPI";
                            oMovMovTributoPar.Aliquota = Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]);
                            oMovMovTributoPar.BaseDeCalculo = oMovMovItemMovPar.Quantidade * ((oMovMovItemMovPar.PrecoUnitario == null) ? 0 : (decimal)oMovMovItemMovPar.PrecoUnitario);

                            oMovMovItemMovPar.TributosItemMov.Add(oMovMovTributoPar);
                            oMovMovInclusaoPar.ItemMovimento.Add(oMovMovItemMovPar);
                        }

                        sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        this.DBS.SkipControlColumns = true;
                        DBS.QueryExec(sSql, movimento.GuidId);
                        this.DBS.SkipControlColumns = false;

                        RM.Mov.Cst.Facade.CstMovFacade oCstMovFacade = CreateFacade<RM.Mov.Cst.Facade.CstMovFacade>();
                        List<RM.Mov.Movimento.MovMovInclusaoPar> lMovMovInclusaoPar = oCstMovFacade.Incluir(oMovMovInclusaoPar);
                        
                        msg.Retorno = lMovMovInclusaoPar[0].IdMov;
                        msg.Mensagem = "Movimento criado com sucesso";
                        */

                        #endregion

                        #region Rotina Manual

                        DBS.BeginTransaction();
                        //RMSAutoInc oRMSAutoInc = new RMSAutoInc(this.DBS);
                        //movimento.IdMov = oRMSAutoInc.GetNewValue("T", movimento.CodColigada, "IDMOV");
                        //movimento.NumeroMov = oRMSAutoInc.GetNewValue("T", movimento.CodColigada, "RQP000001").ToString();

                        movimento.IdMov = this.GetNewAutoInc(movimento.CodColigada, "T", "IDMOV");
                        movimento.NumeroMov = this.GetNewAutoInc(movimento.CodColigada, "T", "RQP000001").ToString();
                        movimento.NumeroMov = movimento.NumeroMov.PadLeft(6, '0');

                        string sSql = @"INSERT INTO TMOV (CODCOLIGADA, IDMOV, CODFILIAL, CODLOC, 
                                        CODLOCENTREGA, CODLOCDESTINO, CODCFO, CODCFONATUREZA, NUMEROMOV, 
                                        SERIE, CODTMV, TIPO, STATUS, MOVIMPRESSO, DOCIMPRESSO, FATIMPRESSA, 
                                        DATAEMISSAO, DATASAIDA, DATAEXTRA1, DATAEXTRA2, CODRPR, 
                                        COMISSAOREPRES, NORDEM, CODCPG, NUMEROTRIBUTOS, 
                                        VALORBRUTO, VALORLIQUIDO, VALOROUTROS, OBSERVACAO, PERCENTUALFRETE, 
                                        VALORFRETE, PERCENTUALSEGURO, VALORSEGURO, 
                                        PERCENTUALDESC, VALORDESC, PERCENTUALDESP,
                                        VALORDESP, PERCENTUALEXTRA1, VALOREXTRA1, 
                                        PERCENTUALEXTRA2, VALOREXTRA2, PERCCOMISSAO, CODMEN, CODMEN2, VIADETRANSPORTE, 
                                        PLACA, CODETDPLACA, PESOLIQUIDO, PESOBRUTO, MARCA, NUMERO, QUANTIDADE, 
                                        ESPECIE, CODTB1FAT, CODTB2FAT, CODTB3FAT, CODTB4FAT, CODTB5FAT, 
                                        CODTB1FLX, CODTB2FLX, CODTB3FLX, CODTB4FLX, CODTB5FLX, IDMOVRELAC, IDMOVLCTFLUXUS, 
                                        IDMOVPEDDESDOBRADO, CODMOEVALORLIQUIDO, DATABASEMOV, DATAMOVIMENTO, 
                                        NUMEROLCTGERADO, GEROUFATURA, NUMEROLCTABERTO, FLAGEXPORTACAO, EMITEBOLETA, CODMENDESCONTO, 
                                        CODMENDESPESA, CODMENFRETE, FRETECIFOUFOB, USADESPFINANC, FLAGEXPORFISC, 
                                        FLAGEXPORFAZENDA, VALORADIANTAMENTO, CODTRA, CODTRA2, STATUSLIBERACAO, 
                                        CODCFOAUX, IDLOT, ITENSAGRUPADOS, FLAGIMPRESSAOFAT, DATACANCELAMENTOMOV, 
                                        VALORRECEBIDO, SEGUNDONUMERO, CODCCUSTO, CODCXA, CODVEN1, CODVEN2, 
                                        CODVEN3, CODVEN4, PERCCOMISSAOVEN2, CODCOLCFO, CODCOLCFONATUREZA, 
                                        CODUSUARIO, CODFILIALENTREGA, CODFILIALDESTINO, FLAGAGRUPADOFLUXUS, 
                                        CODCOLCXA, GERADOPORLOTE, CODDEPARTAMENTO, CODCCUSTODESTINO, CODEVENTO, STATUSEXPORTCONT, 
                                        CODLOTE, STATUSCHEQUE, DATAENTREGA, DATAPROGRAMACAO, IDNAT, IDNAT2, 
                                        CAMPOLIVRE1, CAMPOLIVRE2, CAMPOLIVRE3, GEROUCONTATRABALHO, GERADOPORCONTATRABALHO, 
                                        HORULTIMAALTERACAO, CODLAF, DATAFECHAMENTO, NSEQDATAFECHAMENTO, NUMERORECIBO, 
                                        IDLOTEPROCESSO, IDOBJOF, CODAGENDAMENTO, CHAPARESP, IDLOTEPROCESSOREFAT, INDUSOOBJ, 
                                        SUBSERIE, STSCOMPRAS, CODLOCEXP, IDCLASSMOV, CODENTREGA, CODFAIXAENTREGA, DTHENTREGA, 
                                        CONTABILIZADOPORTOTAL, CODLAFE, IDPRJ, NUMEROCUPOM, NUMEROCAIXA, FLAGEFEITOSALDO, 
                                        INTEGRADOBONUM, CODMOELANCAMENTO, NAONUMERADO, FLAGPROCESSADO, ABATIMENTOICMS, TIPOCONSUMO, 
                                        HORARIOEMISSAO, DATARETORNO, USUARIOCRIACAO, DATACRIACAO, 
                                        IDCONTATOENTREGA, IDCONTATOCOBRANCA, STATUSSEPARACAO, STSEMAIL, VALORFRETECTRC, PONTOVENDA, 
                                        PRAZOENTREGA, VALORBRUTOINTERNO, IDAIDF, IDSALDOESTOQUE, VINCULADOESTOQUEFL, 
                                        IDREDUCAOZ, HORASAIDA, CODMUNSERVICO, CODETDMUNSERV, APROPRIADO, CODIGOSERVICO, DATADEDUCAO, 
                                        CODDIARIO, SEQDIARIO, SEQDIARIOESTORNO, INSSEMOUTRAEMPRESA, IDMOVCTRC, DATAPROGRAMACAOANT, 
                                        CODTDO, VALORDESCCONDICIONAL, VALORDESPCONDICIONAL, CODIGOIRRF, DEDUCAOIRRF, PERCENTBASEINSS, 
                                        PERCBASEINSSEMPREGADO, CONTORCAMENTOANTIGO, CODDEPTODESTINO, DATACONTABILIZACAO, CODVIATRANSPORTE, 
                                        VALORSERVICO, SEQUENCIALESTOQUE, DISTANCIA, UNCALCULO, FORMACALCULO, INTEGRADOAUTOMACAO, 
                                        INTEGRAAPLICACAO, CLASSECONSUMO, TIPOASSINANTE, FASE, TIPOUTILIZACAO, GRUPOTENSAO, 
                                        DATALANCAMENTO, EXTENPORANEO, RECIBONFESTATUS, RECIBONFETIPO, RECIBONFENUMERO, 
                                        RECIBONFESITUACAO, IDMOVCFO, OCAUTONOMO, VALORMERCADORIAS, NATUREZAVOLUMES, VOLUMES, CRO, 
                                        USARATEIOVALORFIN, RECIBONFESERIE, CODCOLCFOORIGEM, CODCFOORIGEM, VALORCTRCARATEAR, CODCOLCFOAUX, 
                                        VRBASEINSSOUTRAEMPRESA, IDCEICFO, CHAVEACESSONFE, VLRSECCAT, VLRDESPACHO, VLRPEDAGIO, VLRFRETEOUTROS, 
                                        ABATIMENTONAOTRIB, RATEIOCCUSTODEPTO, VALORRATEIOLAN, CODCOLCFOTRANSFAT, CODCFOTRANSFAT, 
                                        CODUSUARIOAPROVADESC, IDINTEGRACAO, STATUSANTERIOR, VALORBRUTOORIG, VALORLIQUIDOORIG, VALOROUTROSORIG, 
                                        VALORRATEIOLANORIG, DATAPROCESSAMENTO, IDNATFRETE, IDOPERACAO, 
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                    VALUES (:CODCOLIGADA, :IDMOV, :CODFILIAL, :CODLOC, 
                                        :CODLOCENTREGA, :CODLOCDESTINO, :CODCFO, :CODCFONATUREZA, :NUMEROMOV, 
                                        :SERIE, :CODTMV, :TIPO, :STATUS, :MOVIMPRESSO, :DOCIMPRESSO, :FATIMPRESSA, 
                                        :DATAEMISSAO, :DATASAIDA, :DATAEXTRA1, :DATAEXTRA2, :CODRPR, 
                                        :COMISSAOREPRES, :NORDEM, :CODCPG, :NUMEROTRIBUTOS, 
                                        :VALORBRUTO, :VALORLIQUIDO, :VALOROUTROS, :OBSERVACAO, :PERCENTUALFRETE, 
                                        :VALORFRETE, :PERCENTUALSEGURO, :VALORSEGURO, 
                                        :PERCENTUALDESC, :VALORDESC, :PERCENTUALDESP,
                                        :VALORDESP, :PERCENTUALEXTRA1, :VALOREXTRA1, 
                                        :PERCENTUALEXTRA2, :VALOREXTRA2, :PERCCOMISSAO, :CODMEN, :CODMEN2, :VIADETRANSPORTE, 
                                        :PLACA, :CODETDPLACA, :PESOLIQUIDO, :PESOBRUTO, :MARCA, :NUMERO, :QUANTIDADE, 
                                        :ESPECIE, :CODTB1FAT, :CODTB2FAT, :CODTB3FAT, :CODTB4FAT, :CODTB5FAT, 
                                        :CODTB1FLX, :CODTB2FLX, :CODTB3FLX, :CODTB4FLX, :CODTB5FLX, :IDMOVRELAC, :IDMOVLCTFLUXUS, 
                                        :IDMOVPEDDESDOBRADO, :CODMOEVALORLIQUIDO, :DATABASEMOV, :DATAMOVIMENTO, 
                                        :NUMEROLCTGERADO, :GEROUFATURA, :NUMEROLCTABERTO, :FLAGEXPORTACAO, :EMITEBOLETA, :CODMENDESCONTO, 
                                        :CODMENDESPESA, :CODMENFRETE, :FRETECIFOUFOB, :USADESPFINANC, :FLAGEXPORFISC, 
                                        :FLAGEXPORFAZENDA, :VALORADIANTAMENTO, :CODTRA, :CODTRA2, :STATUSLIBERACAO, 
                                        :CODCFOAUX, :IDLOT, :ITENSAGRUPADOS, :FLAGIMPRESSAOFAT, :DATACANCELAMENTOMOV, 
                                        :VALORRECEBIDO, :SEGUNDONUMERO, :CODCCUSTO, :CODCXA, :CODVEN1, :CODVEN2, 
                                        :CODVEN3, :CODVEN4, :PERCCOMISSAOVEN2, :CODCOLCFO, :CODCOLCFONATUREZA, 
                                        :CODUSUARIO, :CODFILIALENTREGA, :CODFILIALDESTINO, :FLAGAGRUPADOFLUXUS, 
                                        :CODCOLCXA, :GERADOPORLOTE, :CODDEPARTAMENTO, :CODCCUSTODESTINO, :CODEVENTO, :STATUSEXPORTCONT, 
                                        :CODLOTE, :STATUSCHEQUE, :DATAENTREGA, :DATAPROGRAMACAO, :IDNAT, :IDNAT2, 
                                        :CAMPOLIVRE1, :CAMPOLIVRE2, :CAMPOLIVRE3, :GEROUCONTATRABALHO, :GERADOPORCONTATRABALHO, 
                                        :HORULTIMAALTERACAO, :CODLAF, :DATAFECHAMENTO, :NSEQDATAFECHAMENTO, :NUMERORECIBO, 
                                        :IDLOTEPROCESSO, :IDOBJOF, :CODAGENDAMENTO, :CHAPARESP, :IDLOTEPROCESSOREFAT, :INDUSOOBJ, 
                                        :SUBSERIE, :STSCOMPRAS, :CODLOCEXP, :IDCLASSMOV, :CODENTREGA, :CODFAIXAENTREGA, :DTHENTREGA, 
                                        :CONTABILIZADOPORTOTAL, :CODLAFE, :IDPRJ, :NUMEROCUPOM, :NUMEROCAIXA, :FLAGEFEITOSALDO, 
                                        :INTEGRADOBONUM, :CODMOELANCAMENTO, :NAONUMERADO, :FLAGPROCESSADO, :ABATIMENTOICMS, :TIPOCONSUMO, 
                                        :HORARIOEMISSAO, :DATARETORNO, :USUARIOCRIACAO, :DATACRIACAO, 
                                        :IDCONTATOENTREGA, :IDCONTATOCOBRANCA, :STATUSSEPARACAO, :STSEMAIL, :VALORFRETECTRC, :PONTOVENDA, 
                                        :PRAZOENTREGA, :VALORBRUTOINTERNO, :IDAIDF, :IDSALDOESTOQUE, :VINCULADOESTOQUEFL, 
                                        :IDREDUCAOZ, :HORASAIDA, :CODMUNSERVICO, :CODETDMUNSERV, :APROPRIADO, :CODIGOSERVICO, :DATADEDUCAO, 
                                        :CODDIARIO, :SEQDIARIO, :SEQDIARIOESTORNO, :INSSEMOUTRAEMPRESA, :IDMOVCTRC, :DATAPROGRAMACAOANT, 
                                        :CODTDO, :VALORDESCCONDICIONAL, :VALORDESPCONDICIONAL, :CODIGOIRRF, :DEDUCAOIRRF, :PERCENTBASEINSS, 
                                        :PERCBASEINSSEMPREGADO, :CONTORCAMENTOANTIGO, :CODDEPTODESTINO, :DATACONTABILIZACAO, :CODVIATRANSPORTE, 
                                        :VALORSERVICO, :SEQUENCIALESTOQUE, :DISTANCIA, :UNCALCULO, :FORMACALCULO, :INTEGRADOAUTOMACAO, 
                                        :INTEGRAAPLICACAO, :CLASSECONSUMO, :TIPOASSINANTE, :FASE, :TIPOUTILIZACAO, :GRUPOTENSAO, 
                                        :DATALANCAMENTO, :EXTENPORANEO, :RECIBONFESTATUS, :RECIBONFETIPO, :RECIBONFENUMERO, 
                                        :RECIBONFESITUACAO, :IDMOVCFO, :OCAUTONOMO, :VALORMERCADORIAS, :NATUREZAVOLUMES, :VOLUMES, :CRO, 
                                        :USARATEIOVALORFIN, :RECIBONFESERIE, :CODCOLCFOORIGEM, :CODCFOORIGEM, :VALORCTRCARATEAR, :CODCOLCFOAUX, 
                                        :VRBASEINSSOUTRAEMPRESA, :IDCEICFO, :CHAVEACESSONFE, :VLRSECCAT, :VLRDESPACHO, :VLRPEDAGIO, :VLRFRETEOUTROS, 
                                        :ABATIMENTONAOTRIB, :RATEIOCCUSTODEPTO, :VALORRATEIOLAN, :CODCOLCFOTRANSFAT, :CODCFOTRANSFAT, 
                                        :CODUSUARIOAPROVADESC, :IDINTEGRACAO, :STATUSANTERIOR, :VALORBRUTOORIG, :VALORLIQUIDOORIG, :VALOROUTROSORIG, 
                                        :VALORRATEIOLANORIG, :DATAPROCESSAMENTO, :IDNATFRETE, :IDOPERACAO, 
                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                        DBS.QueryExec(sSql,
                            /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*CODFILIAL*/ movimento.CodFilial, /*CODLOC*/ movimento.CodLoc,
                            /*CODLOCENTREGA*/ null, /*CODLOCDESTINO*/ null, /*CODCFO*/ movimento.CodCfo, /*CODCFONATUREZA*/ null, /*NUMEROMOV*/ movimento.NumeroMov,
                            /*SERIE*/ "RQP", /*CODTMV*/ "2.1.05", /*TIPO*/ "A", /*STATUS*/ "A", /*MOVIMPRESSO*/ 0, /*DOCIMPRESSO*/ 0, /*FATIMPRESSA*/ 0,
                            /*DATAEMISSAO*/ movimento.DataEmissao, /*DATASAIDA*/ null, /*DATAEXTRA1*/ null, /*DATAEXTRA2*/ null, /*CODRPR*/ movimento.CodRepresentante,
                            /*COMISSAOREPRES*/ 0, /*NORDEM*/ movimento.NumeroOrdem, /*CODCPG*/ null, /*NUMEROTRIBUTOS*/ null,
                            /*VALORBRUTO*/ 0, /*VALORLIQUIDO*/ 0, /*VALOROUTROS*/ 0, /*OBSERVACAO*/ movimento.Observacao, /*PERCENTUALFRETE*/ null,
                            /*VALORFRETE*/ null, /*PERCENTUALSEGURO*/ null, /*VALORSEGURO*/ null,
                            /*PERCENTUALDESC*/ null, /*VALORDESC*/ null, /*PERCENTUALDESP*/ null,
                            /*VALORDESP*/ null, /*PERCENTUALEXTRA1*/ null, /*VALOREXTRA1*/ null,
                            /*PERCENTUALEXTRA2*/ null, /*VALOREXTRA2*/ null, /*PERCCOMISSAO*/ 0, /*CODMEN*/ null, /*CODMEN2*/ null, /*VIADETRANSPORTE*/ null,
                            /*PLACA*/ null, /*CODETDPLACA*/ null, /*PESOLIQUIDO*/ 0,  /*PESOBRUTO*/ 0, /*MARCA*/ null, /*NUMERO*/ null, /*QUANTIDADE*/ null,
                            /*ESPECIE*/ null, /*CODTB1FAT*/ null, /*CODTB2FAT*/ null, /*CODTB3FAT*/ null, /*CODTB4FAT*/ null, /*CODTB5FAT*/ null,
                            /*CODTB1FLX*/ null, /*CODTB2FLX*/ null, /*CODTB3FLX*/ null, /*CODTB4FLX*/ null, /*CODTB5FLX*/ null, /*IDMOVRELAC*/ null, /*IDMOVLCTFLUXUS*/ null,
                            /*IDMOVPEDDESDOBRADO*/ null, /*CODMOEVALORLIQUIDO*/ "R$", /*DATABASEMOV*/ null, /*DATAMOVIMENTO*/ movimento.DataMovimento,
                            /*NUMEROLCTGERADO*/ null, /*GEROUFATURA*/ 0, /*NUMEROLCTABERTO*/ null, /*FLAGEXPORTACAO*/ null, /*EMITEBOLETA*/ null, /*CODMENDESCONTO*/ null,
                            /*CODMENDESPESA*/ null, /*CODMENFRETE*/ null, /*FRETECIFOUFOB*/ movimento.FreteCIFouFOB, /*USADESPFINANC*/ null, /*FLAGEXPORFISC*/ null,
                            /*FLAGEXPORFAZENDA*/ null, /*VALORADIANTAMENTO*/ null, /*CODTRA*/ movimento.CodTra, /*CODTRA2*/ null, /*STATUSLIBERACAO*/ null,
                            /*CODCFOAUX*/ "CXXXXXXXXXX", /*IDLOT*/ null, /*ITENSAGRUPADOS*/ null, /*FLAGIMPRESSAOFAT*/ null, /*DATACANCELAMENTOMOV*/ null,
                            /*VALORRECEBIDO*/ null, /*SEGUNDONUMERO*/ movimento.SegundoNumero, /*CODCCUSTO*/ null, /*CODCXA*/ null, /*CODVEN1*/ movimento.CodVen1, /*CODVEN2*/ null,
                            /*CODVEN3*/ null, /*CODVEN4*/ null, /*PERCCOMISSAOVEN2*/ 0, /*CODCOLCFO*/ movimento.CodColCfo, /*CODCOLCFONATUREZA*/ null,
                            /*CODUSUARIO*/ movimento.CodUsuario, /*CODFILIALENTREGA*/ null, /*CODFILIALDESTINO*/ movimento.CodFilial, /*FLAGAGRUPADOFLUXUS*/ null,
                            /*CODCOLCXA*/ null, /*GERADOPORLOTE*/ 0, /*CODDEPARTAMENTO*/ null, /*CODCCUSTODESTINO*/ null, /*CODEVENTO*/ null, /*STATUSEXPORTCONT*/ 0,
                            /*CODLOTE*/ null, /*STATUSCHEQUE*/ null, /*DATAENTREGA*/ movimento.DataEntrega, /*DATAPROGRAMACAO*/ null, /*IDNAT*/ movimento.IdNat, /*IDNAT2*/ null,
                            /*CAMPOLIVRE1*/ null, /*CAMPOLIVRE2*/ null, /*CAMPOLIVRE3*/ null, /*GEROUCONTATRABALHO*/ 0, /*GERADOPORCONTATRABALHO*/ 0,
                            /*HORULTIMAALTERACAO*/ DBS.ServerDate(), /*CODLAF*/ null, /*DATAFECHAMENTO*/ null, /*NSEQDATAFECHAMENTO*/ null, /*NUMERORECIBO*/ null,
                            /*IDLOTEPROCESSO*/ null, /*IDOBJOF*/ null, /*CODAGENDAMENTO*/ null, /*CHAPARESP*/ null, /*IDLOTEPROCESSOREFAT*/ null, /*INDUSOOBJ*/ 0,
                            /*SUBSERIE*/ null, /*STSCOMPRAS*/ null, /*CODLOCEXP*/ null, /*IDCLASSMOV*/ null, /*CODENTREGA*/ null, /*CODFAIXAENTREGA*/ null, /*DTHENTREGA*/ null,
                            /*CONTABILIZADOPORTOTAL*/ null, /*CODLAFE*/ null, /*IDPRJ*/ null, /*NUMEROCUPOM*/ null, /*NUMEROCAIXA*/ null, /*FLAGEFEITOSALDO*/ null,
                            /*INTEGRADOBONUM*/ 0, /*CODMOELANCAMENTO*/ null, /*NAONUMERADO*/ null, /*FLAGPROCESSADO*/ 0, /*ABATIMENTOICMS*/ 0, /*TIPOCONSUMO*/ null,
                            /*HORARIOEMISSAO*/ DBS.ServerDate(), /*DATARETORNO*/ null, /*USUARIOCRIACAO*/ movimento.CodUsuario, /*DATACRIACAO*/ DateTime.Today,
                            /*IDCONTATOENTREGA*/ null, /*IDCONTATOCOBRANCA*/ null, /*STATUSSEPARACAO*/ null, /*STSEMAIL*/ 0, /*VALORFRETECTRC*/ null, /*PONTOVENDA*/ null,
                            /*PRAZOENTREGA*/ movimento.PrazoEntrega, /*VALORBRUTOINTERNO*/ 0, /*IDAIDF*/ null, /*IDSALDOESTOQUE*/ null, /*VINCULADOESTOQUEFL*/ 0,
                            /*IDREDUCAOZ*/ null, /*HORASAIDA*/ null, /*CODMUNSERVICO*/ null, /*CODETDMUNSERV*/ null, /*APROPRIADO*/ null, /*CODIGOSERVICO*/ null, /*DATADEDUCAO*/ null,
                            /*CODDIARIO*/ null, /*SEQDIARIO*/ null, /*SEQDIARIOESTORNO*/ null, /*INSSEMOUTRAEMPRESA*/ null, /*IDMOVCTRC*/ null, /*DATAPROGRAMACAOANT*/ null,
                            /*CODTDO*/ null, /*VALORDESCCONDICIONAL*/ 0, /*VALORDESPCONDICIONAL*/ 0, /*CODIGOIRRF*/ null, /*DEDUCAOIRRF*/ null, /*PERCENTBASEINSS*/ null,
                            /*PERCBASEINSSEMPREGADO*/ null, /*CONTORCAMENTOANTIGO*/ null, /*CODDEPTODESTINO*/ null, /*DATACONTABILIZACAO*/ null, /*CODVIATRANSPORTE*/ null,
                            /*VALORSERVICO*/ null, /*SEQUENCIALESTOQUE*/ null, /*DISTANCIA*/ null, /*UNCALCULO*/ null, /*FORMACALCULO*/ null, /*INTEGRADOAUTOMACAO*/ 0,
                            /*INTEGRAAPLICACAO*/ "T", /*CLASSECONSUMO*/ null, /*TIPOASSINANTE*/ null, /*FASE*/ null, /*TIPOUTILIZACAO*/ null, /*GRUPOTENSAO*/ null,
                            /*DATALANCAMENTO*/ DateTime.Today, /*EXTENPORANEO*/ null, /*RECIBONFESTATUS*/ 0, /*RECIBONFETIPO*/ null, /*RECIBONFENUMERO*/ null,
                            /*RECIBONFESITUACAO*/ null, /*IDMOVCFO*/ movimento.IdMovCfo, /*OCAUTONOMO*/ null, /*VALORMERCADORIAS*/ 0, /*NATUREZAVOLUMES*/ null, /*VOLUMES*/ null, /*CRO*/ null,
                            /*USARATEIOVALORFIN*/ 0, /*RECIBONFESERIE*/ null, /*CODCOLCFOORIGEM*/ null, /*CODCFOORIGEM*/ null, /*VALORCTRCARATEAR*/ null, /*CODCOLCFOAUX*/ 0,
                            /*VRBASEINSSOUTRAEMPRESA*/ 0, /*IDCEICFO*/ null, /*CHAVEACESSONFE*/ null, /*VLRSECCAT*/ null, /*VLRDESPACHO*/ null, /*VLRPEDAGIO*/ null, /*VLRFRETEOUTROS*/ null,
                            /*ABATIMENTONAOTRIB*/ null, /*RATEIOCCUSTODEPTO*/ null, /*VALORRATEIOLAN*/ null, /*CODCOLCFOTRANSFAT*/ null, /*CODCFOTRANSFAT*/ null,
                            /*CODUSUARIOAPROVADESC*/ null, /*IDINTEGRACAO*/ null, /*STATUSANTERIOR*/ null, /*VALORBRUTOORIG*/ 0, /*VALORLIQUIDOORIG*/ 0, /*VALOROUTROSORIG*/ 0,
                            /*VALORRATEIOLANORIG*/ null, /*DATAPROCESSAMENTO*/ null, /*IDNATFRETE*/ null, /*IDOPERACAO*/ null,
                            /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                        sSql = @"INSERT INTO TMOVHISTORICO (CODCOLIGADA, IDMOV, HISTORICOCURTO, HISTORICOLONGO, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                VALUES (:CODCOLIGADA, :IDMOV, :HISTORICOCURTO, :HISTORICOLONGO, :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                        DBS.QueryExec(sSql,
                            /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*HISTORICOCURTO*/ null, /*HISTORICOLONGO*/ movimento.HistoricoLongo,
                            /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                        sSql = @"INSERT INTO TMOVCOMPL (CODCOLIGADA, IDMOV, CLASSREQ, CLASSREQCPL,
                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, APLICPROD, TIPOVENDA) 
                                VALUES (:CODCOLIGADA, :IDMOV, :CLASSREQ,:CLASSREQCPL,
                                    :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :APLICPROD, :TIPOVENDA)";

                        DBS.QueryExec(sSql,
                            /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*CLASSREQ*/ movimento.CLASSREQ,/*CLASSREQCPL*/ movimento.CLASSREQCPL,
                            /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(), /*APLICPROD*/ movimento.APLICPROD, /*TIPOVENDA*/ movimento.TIPOVENDA);

                        sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        DataTable Itens = DBS.QuerySelect("ZTITMMOVORCTEMP", sSql, movimento.GuidId);
                        for (int i = 0; i < Itens.Rows.Count; i++)
                        {
                            string sCODTB2FAT = null;
                            string sCODTB3FAT = null;
                            string sCODTB4FAT = null;

                            sSql = @"SELECT TPRD.CODTB2FAT, TPRD.CODTB3FAT, TPRD.CODTB4FAT FROM TPRD WHERE TPRD.CODCOLIGADA = :CODCOLIGADA AND TPRD.IDPRD = :IDPRD";
                            DataTable dtPrd = DBS.QuerySelect("TPRD", sSql, Itens.Rows[i]["CODCOLIGADA"], Itens.Rows[i]["IDPRD"]);
                            foreach (DataRow row in dtPrd.Rows)
                            {
                                sCODTB2FAT = (row["CODTB2FAT"] == DBNull.Value) ? null : row["CODTB2FAT"].ToString();
                                sCODTB3FAT = (row["CODTB3FAT"] == DBNull.Value) ? null : row["CODTB3FAT"].ToString();
                                sCODTB4FAT = (row["CODTB4FAT"] == DBNull.Value) ? null : row["CODTB4FAT"].ToString();
                            }


                            sSql = @"INSERT INTO TITMMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, NUMEROSEQUENCIAL, IDPRD, CODTIP, QUANTIDADE, PRECOUNITARIO, PRECOTABELA, 
                                        PERCENTUALDESC, VALORDESC, PERCENTUALDESP, VALORDESP, DATAEMISSAO, 
                                        CODMEN, NUMEROTRIBUTOS, CODTB1FAT, CODTB2FAT, CODTB3FAT, CODTB4FAT, 
                                        CODTB5FAT, CODTB1FLX, CODTB2FLX, CODTB3FLX, CODTB4FLX, CODTB5FLX, CAMPOLIVRE, 
                                        CODUND, QUANTIDADEARECEBER, CODNAT, CODCPG, DATAENTREGA, PRATELEIRA, IDCNT, NSEQITMCNT, 
                                        DATAINIFAT, DATAFIMFAT, FLAGEFEITOSALDO, VALORUNITARIO, VALORFINANCEIRO, 
                                        IMPRIMEMOV, CODCCUSTO, FLAGREPASSE, ALIQORDENACAO, QUANTIDADEORIGINAL, 
                                        IDNAT, FLAG, CHAPA, INICIO, TERMINO, PREVINICIO, STATUS, BLOCK, 
                                        FLAGREFATURAMENTO, IDCNTDESTINO, NSEQITMCNTDEST, FATORCONVUND, IDPRJ, IDTRF, 
                                        VALORTOTALITEM, VALORCODIGOPRD, TIPOCODIGOPRD, QTDUNDPEDIDO, TRIBUTACAOECF, 
                                        CODFILIAL, CODDEPARTAMENTO, IDPRDCOMPOSTO, QUANTIDADESEPARADA, PERCENTCOMISSAO, INDICENCM, NCM, CODRPR, COMISSAOREPRES, 
                                        NSEQITMCNTMEDICAO, VALORESCRITURACAO, VALORFINPEDIDO, VALORFRETECTRC, VALOROPFRM1, VALOROPFRM2, 
                                        IDOBJOFICINA, PRECOEDITADO, QTDEVOLUMEUNITARIO, IDGRD, 
                                        CODVEN1, CODLOCALBN, REGISTROEXPORTACAO, DATARE, PRECOTOTALEDITADO, CST, 
                                        VALORDESCCONDICONALITM, VALORDESPCONDICIONALITM, DATAORCAMENTO, CODTBORCAMENTO, RATEIOFRETE, 
                                        RATEIOSEGURO, RATEIODESC, RATEIODESP, RATEIOEXTRA1, RATEIOEXTRA2, RATEIOFRETECTRC, 
                                        RATEIODEDMAT, RATEIODEDSUB, RATEIODEDOUT, IDCLASSIFENERGIACOMUNIC, VALORUNTORCAMENTO, 
                                        VALSERVICONFE, CODLOC, VALORBEM, VALORLIQUIDO, CODIGOCODIF, CODMUNSERVICO, CODETDMUNSERV, RATEIOCCUSTODEPTO, CUSTOREPOSICAO, 
                                        CUSTOREPOSICAOB, VALORFINTERCEIROS, VALORFINANCGERENCIAL, CODIGOSERVICO, VALORUNITGERENCIAL, 
                                        IDINTEGRACAO, IDTABPRECO, VALORBRUTOITEM, VALORBRUTOITEMORIG, CODCOLTBORCAMENTO, CODPUBLIC, QUANTIDADETOTAL, 
                                        PRODUTOSUBSTITUTO, CODTBGRUPOORC, PRECOUNITARIOSELEC, VALORRATEIOLAN, 
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                    VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :NUMEROSEQUENCIAL, :IDPRD, :CODTIP, :QUANTIDADE, :PRECOUNITARIO, :PRECOTABELA, 
                                        :PERCENTUALDESC, :VALORDESC, :PERCENTUALDESP, :VALORDESP, :DATAEMISSAO, 
                                        :CODMEN, :NUMEROTRIBUTOS, :CODTB1FAT, :CODTB2FAT, :CODTB3FAT, :CODTB4FAT, 
                                        :CODTB5FAT, :CODTB1FLX, :CODTB2FLX, :CODTB3FLX, :CODTB4FLX, :CODTB5FLX, :CAMPOLIVRE, 
                                        :CODUND, :QUANTIDADEARECEBER, :CODNAT, :CODCPG, :DATAENTREGA, :PRATELEIRA, :IDCNT, :NSEQITMCNT, 
                                        :DATAINIFAT, :DATAFIMFAT, :FLAGEFEITOSALDO, :VALORUNITARIO, :VALORFINANCEIRO, 
                                        :IMPRIMEMOV, :CODCCUSTO, :FLAGREPASSE, :ALIQORDENACAO, :QUANTIDADEORIGINAL, 
                                        :IDNAT, :FLAG, :CHAPA, :INICIO, :TERMINO, :PREVINICIO, :STATUS, :BLOCK, 
                                        :FLAGREFATURAMENTO, :IDCNTDESTINO, :NSEQITMCNTDEST, :FATORCONVUND, :IDPRJ, :IDTRF, 
                                        :VALORTOTALITEM, :VALORCODIGOPRD, :TIPOCODIGOPRD, :QTDUNDPEDIDO, :TRIBUTACAOECF, 
                                        :CODFILIAL, :CODDEPARTAMENTO, :IDPRDCOMPOSTO, :QUANTIDADESEPARADA, :PERCENTCOMISSAO, :INDICENCM, :NCM, :CODRPR, :COMISSAOREPRES, 
                                        :NSEQITMCNTMEDICAO, :VALORESCRITURACAO, :VALORFINPEDIDO, :VALORFRETECTRC, :VALOROPFRM1, :VALOROPFRM2, 
                                        :IDOBJOFICINA, :PRECOEDITADO, :QTDEVOLUMEUNITARIO, :IDGRD, 
                                        :CODVEN1, :CODLOCALBN, :REGISTROEXPORTACAO, :DATARE, :PRECOTOTALEDITADO, :CST, 
                                        :VALORDESCCONDICONALITM, :VALORDESPCONDICIONALITM, :DATAORCAMENTO, :CODTBORCAMENTO, :RATEIOFRETE, 
                                        :RATEIOSEGURO, :RATEIODESC, :RATEIODESP, :RATEIOEXTRA1, :RATEIOEXTRA2, :RATEIOFRETECTRC, 
                                        :RATEIODEDMAT, :RATEIODEDSUB, :RATEIODEDOUT, :IDCLASSIFENERGIACOMUNIC, :VALORUNTORCAMENTO, 
                                        :VALSERVICONFE, :CODLOC, :VALORBEM, :VALORLIQUIDO, :CODIGOCODIF, :CODMUNSERVICO, :CODETDMUNSERV, :RATEIOCCUSTODEPTO, :CUSTOREPOSICAO, 
                                        :CUSTOREPOSICAOB, :VALORFINTERCEIROS, :VALORFINANCGERENCIAL, :CODIGOSERVICO, :VALORUNITGERENCIAL, 
                                        :IDINTEGRACAO, :IDTABPRECO, :VALORBRUTOITEM, :VALORBRUTOITEMORIG, :CODCOLTBORCAMENTO, :CODPUBLIC, :QUANTIDADETOTAL, 
                                        :PRODUTOSUBSTITUTO, :CODTBGRUPOORC, :PRECOUNITARIOSELEC, :VALORRATEIOLAN, 
                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                            DBS.QueryExec(sSql,
                                /*CODCOLIGADA*/ Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]),
                                /*IDMOV*/ movimento.IdMov,
                                /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*NUMEROSEQUENCIAL*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*IDPRD*/ Convert.ToInt32(Itens.Rows[i]["IDPRD"]),
                                /*CODTIP*/ null,
                                /*QUANTIDADE*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                /*PRECOUNITARIO*/ Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                /*PRECOTABELA*/ 0,
                                /*PERCENTUALDESC*/ null, /*VALORDESC*/ null, /*PERCENTUALDESP*/ null, /*VALORDESP*/ null, /*DATAEMISSAO*/ movimento.DataEmissao,
                                /*CODMEN*/ null, /*NUMEROTRIBUTOS*/ null, /*CODTB1FAT*/ null, /*CODTB2FAT*/ sCODTB2FAT, /*CODTB3FAT*/ sCODTB3FAT, /*CODTB4FAT:*/ sCODTB4FAT,
                                /*CODTB5FAT*/ null, /*CODTB1FLX*/ null, /*CODTB2FLX*/ null, /*CODTB3FLX*/ null, /*CODTB4FLX*/ null, /*CODTB5FLX*/ null, /*CAMPOLIVRE*/ null,
                                /*CODUND*/ Itens.Rows[i]["CODUND"].ToString(),
                                /*QUANTIDADEARECEBER*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                /*CODNAT*/ null, /*CODCPG*/ null, /*DATAENTREGA*/ movimento.DataEntrega, /*PRATELEIRA*/ null, /*IDCNT*/ null, /*NSEQITMCNT*/ null,
                                /*DATAINIFAT*/ null, /*DATAFIMFAT*/ null, /*FLAGEFEITOSALDO*/ null, /*VALORUNITARIO*/ 0,  /*VALORFINANCEIRO*/ 0,
                                /*IMPRIMEMOV*/ null, /*CODCCUSTO*/ null, /*FLAGREPASSE*/ null, /*ALIQORDENACAO*/ 0,
                                /*QUANTIDADEORIGINAL*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                /*IDNAT*/ Convert.ToInt32(Itens.Rows[i]["IDPRD"]), /*FLAG*/ 0, /*CHAPA*/ null, /*INICIO*/ null, /*TERMINO*/ null, /*PREVINICIO*/ null, /*STATUS*/ null, /*BLOCK*/ null,
                                /*FLAGREFATURAMENTO*/ null, /*IDCNTDESTINO*/ null, /*NSEQITMCNTDEST*/ null, /*FATORCONVUND*/ 0, /*IDPRJ*/ null, /*IDTRF*/ null,
                                /*VALORTOTALITEM*/ 0, /*VALORCODIGOPRD*/ null, /*TIPOCODIGOPRD*/ null, /*QTDUNDPEDIDO*/ null, /*TRIBUTACAOECF*/ null,
                                /*CODFILIAL*/ movimento.CodFilial, /*CODDEPARTAMENTO*/ null,
                                /*IDPRDCOMPOSTO*/ (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]),
                                /*QUANTIDADESEPARADA*/ 0, /*PERCENTCOMISSAO*/ 0, /*INDICENCM*/ null, /*NCM*/ null, /*CODRPR*/ null, /*COMISSAOREPRES*/ 0,
                                /*NSEQITMCNTMEDICAO*/ null, /*VALORESCRITURACAO*/ 0, /*VALORFINPEDIDO*/ 0, /*VALORFRETECTRC*/ null, /*VALOROPFRM1*/ 0, /*VALOROPFRM2*/ 0,
                                /*IDOBJOFICINA*/ null, /*PRECOEDITADO*/ 0, /*QTDEVOLUMEUNITARIO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]), /*IDGRD*/ null,
                                /*CODVEN1*/ movimento.CodVen1, /*CODLOCALBN*/ null, /*REGISTROEXPORTACAO*/ null, /*DATARE*/ null, /*PRECOTOTALEDITADO*/ null, /*CST*/ null,
                                /*VALORDESCCONDICONALITM*/ 0, /*VALORDESPCONDICIONALITM*/ 0, /*DATAORCAMENTO*/ null, /*CODTBORCAMENTO*/ null, /*RATEIOFRETE*/ null,
                                /*RATEIOSEGURO*/ null, /*RATEIODESC*/ null, /*RATEIODESP*/ null, /*RATEIOEXTRA1*/ null, /*RATEIOEXTRA2*/ null, /*RATEIOFRETECTRC*/ null,
                                /*RATEIODEDMAT*/ null, /*RATEIODEDSUB*/ null, /*RATEIODEDOUT*/ null, /*IDCLASSIFENERGIACOMUNIC*/ null, /*VALORUNTORCAMENTO*/ 0,
                                /*VALSERVICONFE*/ 0, /*CODLOC*/ movimento.CodLoc, /*VALORBEM*/ 0,
                                /*VALORLIQUIDO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                /*CODIGOCODIF*/ null, /*CODMUNSERVICO*/ null, /*CODETDMUNSERV*/ null, /*RATEIOCCUSTODEPTO*/ null, /*CUSTOREPOSICAO*/ null,
                                /*CUSTOREPOSICAOB*/ null, /*VALORFINTERCEIROS*/ null, /*VALORFINANCGERENCIAL*/ null, /*CODIGOSERVICO*/ null, /*VALORUNITGERENCIAL*/ null,
                                /*IDINTEGRACAO*/ null, /*IDTABPRECO*/ null,
                                /*VALORBRUTOITEM*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                /*VALORBRUTOITEMORIG*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                /*CODCOLTBORCAMENTO*/ null, /*CODPUBLIC*/ null, /*QUANTIDADETOTAL*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                /*PRODUTOSUBSTITUTO*/ 0, /*CODTBGRUPOORC*/ null, /*PRECOUNITARIOSELEC*/ Itens.Rows[i]["PRECOUNITARIOSELEC"].ToString(), /*VALORRATEIOLAN*/ null,
                                /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                            sSql = @"INSERT INTO TITMMOVHISTORICO (CODCOLIGADA, IDMOV, NSEQITMMOV, HISTORICOCURTO, HISTORICOLONGO, 
                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :HISTORICOCURTO, :HISTORICOLONGO, 
                                    :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                            DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*HISTORICOCURTO*/ null, /*HISTORICOLONGO*/ Itens.Rows[i]["HISTORICOLONGO"].ToString(),
                                /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                            sSql = @"INSERT INTO TITMMOVCOMPL (CODCOLIGADA, IDMOV, NSEQITMMOV,
                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON)
                                VALUES(:CODCOLIGADA, :IDMOV, :NSEQITMMOV,
                                    :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :APLICPROD)";

                            DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(), /*APLICPROD*/ Itens.Rows[i]["APLICPROD"]);

                            DataTable dtOrcamentoItemComposto = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT * FROM ZORCAMENTOITEMCOMPOSTO WHERE CODCOLIGADA = ? AND IDMOV = ? AND = NSEQ = ?", new object[] { movimento.CodColigada, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]) });

                            if (dtOrcamentoItemComposto != null)
                            {
                                if (dtOrcamentoItemComposto.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dtOrcamentoItemComposto.Rows.Count; j++)
                                    {
                                        sSql = @"INSERT INTO ZORCAMENTOITEMCOMPOSTO (CODCOLIGADA, CODFILIAL, IDMOV, NSEQ, IDPRD, QUANTIDADE, PRECOUNITARIO)
                                VALUES(:CODCOLIGADA, :CODFILIAL, :IDMOV,
                                    :NSEQ, :IDPRD, :QUANTIDADE, :PRECOUNITARIO)";

                                        DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*CODFILIAL*/ movimento.CodFilial, /*IDMOV*/ movimento.IdMov, /*NSEQ*/ Convert.ToInt32(dtOrcamentoItemComposto.Rows[i]["NSEQ"]),
                                            /*IDPRD*/ Convert.ToInt32(dtOrcamentoItemComposto.Rows[i]["IDPRD"]), /*QUANTIDADE*/ Convert.ToDecimal(dtOrcamentoItemComposto.Rows[i]["QUANTIDADE"]), /*PRECOUNITARIO*/ Convert.ToDecimal(dtOrcamentoItemComposto.Rows[i]["PRECOUNITARIO"]));
                                    }
                                }
                            }

                            sSql = @"INSERT INTO TTRBMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, CODTRB, BASEDECALCULO, ALIQUOTA, VALOR, FATORREDUCAO, FATORSUBSTTRIB, 
                                        BASEDECALCULOCALCULADA, EDITADO, VLRISENTO, CODRETENCAO, TIPORECOLHIMENTO, CODTRBBASE, PERCDIFERIMENTOPARCIALICMS, 
                                        SITTRIBUTARIA, MODALIDADEBC, ALIQUOTAPORVALOR, CODUNDREFERENCIA, BASECHEIA, 
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, TIPORENDIMENTO, FORMATRIBUTACAO) 
                                    VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :CODTRB, :BASEDECALCULO, :ALIQUOTA, :VALOR, :FATORREDUCAO, :FATORSUBSTTRIB, 
                                        :BASEDECALCULOCALCULADA, :EDITADO, :VLRISENTO, :CODRETENCAO, :TIPORECOLHIMENTO, :CODTRBBASE, :PERCDIFERIMENTOPARCIALICMS, 
                                        :SITTRIBUTARIA, :MODALIDADEBC, :ALIQUOTAPORVALOR, :CODUNDREFERENCIA, :BASECHEIA, 
                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :TIPORENDIMENTO, :FORMATRIBUTACAO)";

                            string sql = String.Empty;

                            #region Calculo do Trbiuto

                            Decimal BaseCalculo = 0;
                            Decimal Aliquota = 0;
                            Decimal ValorTributo = 0;
                            DataTable dtTributo = new DataTable();
                            Tributo IPI = new Tributo();
                            Tributo PIS = new Tributo();
                            Tributo COFINS = new Tributo();
                            Tributo ICMS = new Tributo();
                            Tributo ICMSST = new Tributo();

                            List<Tributo> Tributos = new List<Tributo>();

                            #region Calculo do Trbiuto IPI

                            Aliquota = Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]);

                            if (Itens.Rows[i]["IDPRDCOMPOSTO"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]) == 0)
                                {
                                    BaseCalculo = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                                    ValorTributo = new Util().Arredonda((BaseCalculo * Aliquota) / 100);
                                }
                            }
                            else
                            {
                                BaseCalculo = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                                ValorTributo = new Util().Arredonda((BaseCalculo * Aliquota) / 100);
                            }

                            IPI.TipoTributo = "IPI";
                            IPI.BaseDoCalculo = BaseCalculo;
                            IPI.ValorDaAliquota = Aliquota;
                            IPI.ValorDoTributo = ValorTributo;

                            Tributos.Add(IPI);

                            #endregion

                            #region Calculo do Trbiuto PIS
                            sql = String.Format(@"select  (isnull(TITMMOV.VALORTOTALITEM, 0) - 
		                                                        isnull(TITMMOV.RATEIODESC, 0) + 
		                                                        isnull(TITMMOV.RATEIODESP, 0) + 
		                                                        isnull(TITMMOV. RATEIOFRETE, 0)) as 'BASECALCULO',
		                                                        DTRBNATUREZA.ALIQTRB as 'ALIQUOTA',
		                                                        NSEQITMMOV 

                                                        from TITMMOV (NOLOCK)

                                                        inner join DTRBNATUREZA (NOLOCK)
                                                        on DTRBNATUREZA.IDNAT = TITMMOV.IDNAT
                                                        and DTRBNATUREZA.CODCOLIGADA = TITMMOV.CODCOLIGADA

                                                        where TITMMOV.CODCOLIGADA = '{0}'
                                                        and IDMOV = '{1}'
                                                        and TITMMOV.NSEQITMMOV = '{2}'
                                                        and DTRBNATUREZA.CODTRB = 'PIS'", AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                PIS.TipoTributo = "PIS";
                                PIS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString().Replace(",", "."));
                                PIS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString().Replace(",", "."));
                                PIS.ValorDoTributo = PIS.BaseDoCalculo * PIS.ValorDaAliquota;
                            }

                            Tributos.Add(PIS);


                            #endregion

                            #region Calculo do Trbiuto COFINS
                            sql = String.Format(@"select  (isnull(TITMMOV.VALORTOTALITEM, 0) - 
		                                                        isnull(TITMMOV.RATEIODESC, 0) + 
		                                                        isnull(TITMMOV.RATEIODESP, 0) + 
		                                                        isnull(TITMMOV. RATEIOFRETE, 0)) as 'BASECALCULO',
		                                                        DTRBNATUREZA.ALIQTRB as 'ALIQUOTA',
		                                                        NSEQITMMOV 

                                                        from TITMMOV (NOLOCK)

                                                        inner join DTRBNATUREZA (NOLOCK)
                                                        on DTRBNATUREZA.IDNAT = TITMMOV.IDNAT
                                                        and DTRBNATUREZA.CODCOLIGADA = TITMMOV.CODCOLIGADA

                                                        where TITMMOV.CODCOLIGADA = '{0}'
                                                        and IDMOV = '{1}'
                                                        and TITMMOV.NSEQITMMOV = '{2}'
                                                        and DTRBNATUREZA.CODTRB = 'COFINS'", AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                COFINS.TipoTributo = "COFINS";
                                COFINS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString().Replace(",", "."));
                                COFINS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString().Replace(",", "."));
                                COFINS.ValorDoTributo = COFINS.BaseDoCalculo * COFINS.ValorDaAliquota;
                            }

                            Tributos.Add(COFINS);


                            #endregion

                            #region Calculo do Trbiuto ICMS

                            sql = String.Format(@"SELECT FATORICMS FROM DREGRAICMS (NOLOCK) WHERE IDREGRAICMS IN (
		                                                SELECT IDREGRAICMS FROM DCFOP (NOLOCK) WHERE CODCOLIGADA = 1 AND IDNAT = '{0}')
		                                                AND CODCOLIGADA = 1", Itens.Rows[i]["IDNAT"].ToString());

                            sql = String.Format(@"select CASE 
	
	                                                                WHEN 

		                                                                (SELECT FATORICMS FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
		                                                                AND CODCOLIGADA = 1) > 0 
		
		                                                                AND 
		
		                                                                (SELECT BASEICMSCOMIPI FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
		                                                                AND CODCOLIGADA = 1) = 1
		
	                                                                THEN
		                                                                isnull((
		                                                                (TITMMOV.VALORTOTALITEM + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) 
		                                                                - 
		                                                                ((TITMMOV.VALORTOTALITEM + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * '{1}' /100 )
		                                                                ),0)
		
	                                                                ELSE
		
		                                                                CASE
		
		                                                                WHEN
		
			                                                                (SELECT BASEICMSCOMIPI FROM DREGRAICMS WHERE IDREGRAICMS IN (
			                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
			                                                                AND CODCOLIGADA = 1) = 1		
		
		                                                                THEN
			
			                                                                isnull(((TITMMOV.VALORTOTALITEM +  '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE)),0)
		
		                                                                ELSE
		
			                                                                isnull(((TITMMOV.VALORTOTALITEM - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE)),0)

		                                                                END
                                                                END as 'BASECALCULO',
                                                                (SELECT ALIQICMS FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                    SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
                                                                    AND CODCOLIGADA = 1) as 'ALIQUOTA'

                                                                from TITMMOV (NOLOCK)

                                                                where CODCOLIGADA = '{2}'
                                                                and IDMOV = '{3}'
                                                                and NSEQITMMOV = '{4}'", IPI.ValorDoTributo.ToString().Replace(",", "."), MetodosSQL.GetField(sql, "FATORICMS").Replace(",", "."), AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]), Itens.Rows[i]["IDNAT"].ToString());

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                ICMS.TipoTributo = "ICMS";
                                ICMS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString().Replace(",", "."));
                                ICMS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString().Replace(",", "."));
                                ICMS.ValorDoTributo = ICMS.BaseDoCalculo * ICMS.ValorDaAliquota;
                            }

                            Tributos.Add(ICMS);

                            #endregion

                            #region Calculo do Trbiuto ICMSST

                            sql = String.Format(@"SELECT FATORREDST, ALIQSUBST, FATORSUBST FROM DREGRAICMS WHERE IDREGRAICMS IN (
                                                    SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{0}')
                                                    AND CODCOLIGADA = 1", Itens.Rows[i]["IDNAT"].ToString());

                            dtTributo = MetodosSQL.GetDT(sql);


                            #region Query ICMS-ST
                            foreach (DataRow row in dtTributo.Rows)
                            {
                                sql = String.Format(@"SELECT 

CASE 
	
WHEN 

	(SELECT OPERACAOCONSUMIDORFINAL 
	FROM TMOVFISCAL 
	WHERE CODCOLIGADA = 1 AND IDMOV = '{5}') = 1
		
THEN
		
	0
		
ELSE
		
	CASE
		
	WHEN 
	
		(SELECT 1
		  FROM DCFOP, DREGRAICMS
		 WHERE DCFOP.CODCOLIGADA = DREGRAICMS.CODCOLIGADA
		   AND DCFOP.IDREGRAICMS = DREGRAICMS.IDREGRAICMS
		   AND DREGRAICMS.TIPOOPERACAOTRIBUTARIA IN (1,2)
		   AND DREGRAICMS.UTIBCDIFALICMSDENTRO IN (1,2)
		   AND DCFOP.CODCOLIGADA = 1
		   AND DCFOP.CODNAT = '{6}') = 1
	THEN
			
		CASE
		
		WHEN
				 
			(SELECT 1
			  FROM DCFOP, DREGRAICMS
			 WHERE DCFOP.CODCOLIGADA = DREGRAICMS.CODCOLIGADA
			   AND DCFOP.IDREGRAICMS = DREGRAICMS.IDREGRAICMS
			   AND DREGRAICMS.TIPOOPERACAOTRIBUTARIA IN (1,2)
			   AND DREGRAICMS.UTIBCDIFALICMSDENTRO = 1
			   AND DCFOP.CODCOLIGADA = 1
			   AND DCFOP.CODNAT = '{6}') = 1
		THEN
					
			CASE
			
			WHEN
				(SELECT FATORREDST FROM DREGRAICMS WHERE IDREGRAICMS IN (
				SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{6}')
				AND CODCOLIGADA = 1) > 0 
			
			THEN
			
				((((((TITMMOV.VALORTOTALITEM  + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
                ((TITMMOV.VALORTOTALITEM  + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ('{3}'/100) ))) +    
                ((((TITMMOV.VALORTOTALITEM  + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
                ((TITMMOV.VALORTOTALITEM  + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * '{3}'/100 ))) * ('{2}'/100))) - '{1}' ) / (1 - (('{4}')/100)))
			
			ELSE
			
				((((TITMMOV.VALORTOTALITEM +  '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) +    
				((TITMMOV.VALORTOTALITEM +  '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ('{2}'/100)))  - '{1}' ) / (1 - (('{4}')/100)))

			END
		ELSE
			0
		END
	ELSE
		0
	END
END
as 'BASECALCULO'
FROM TITMMOV (NOLOCK) WHERE IDMOV = '{5}'", /*VALORIPI*/ IPI.ValorDoTributo.ToString().Replace(",", "."),
                                   /*VALORICMS*/ ICMS.ValorDoTributo.ToString().Replace(",", "."),
                                   /*FATORSUBST*/ row["FATORSUBST"].ToString().Replace(",", "."),
                                   /*FATORREDST*/ row["FATORREDST"].ToString().Replace(",", "."),
                                   /*ALIQSUBST*/ row["ALIQSUBST"].ToString().Replace(",", "."),
                                   /*IDMOV*/ movimento.IdMov,
                                   /*IDNAT*/ Convert.ToInt32(Itens.Rows[i]["IDNAT"]));

                                ICMSST.ValorDaAliquota = Convert.ToDecimal(row["ALIQSUBST"].ToString().Replace(",", "."));
                            }
                            #endregion

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                ICMSST.TipoTributo = "ICMSST";
                                ICMSST.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString().Replace(",", "."));

                                ICMSST.ValorDoTributo = ICMSST.BaseDoCalculo * ICMSST.ValorDaAliquota;
                            }



                            Tributos.Add(ICMSST);

                            #endregion


                            //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                            //    /*CODTRB*/ "IPI", /*BASEDECALCULO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                            //    /*ALIQUOTA*/ Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]),
                            //    /*VALOR*/ ValorTributo, /*FATORREDUCAO*/ 0, /*FATORSUBSTTRIB*/ 0,
                            //    /*BASEDECALCULOCALCULADA*/ 0, /*EDITADO*/ 0, /*VLRISENTO*/ null, /*CODRETENCAO*/ "0561", /*TIPORECOLHIMENTO*/ null, /*CODTRBBASE*/ null,
                            //    /*PERCDIFERIMENTOPARCIALICMS*/ 0, /*SITTRIBUTARIA*/ null, /*MODALIDADEBC*/ null, /*ALIQUOTAPORVALOR*/ null, /*CODUNDREFERENCIA*/ null,
                            //    /*BASECHEIA*/ 0,
                            //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(),
                            //    /*TIPORENDIMENTO*/ null, /*FORMATRIBUTACAO*/ null);

                            //List<string> tributos = new List<string>();
                            //tributos.Add("IPI");
                            //tributos.Add("ICM-SF");
                            //tributos.Add("PIS-SF");
                            //tributos.Add("COF-SF");
                            //tributos.Add("ICMS");
                            //tributos.Add("ICMSST");

                            foreach (Tributo t in Tributos)
                            {

                                t.ValorDoTributo = Math.Round(t.ValorDoTributo, 2);
                                t.BaseDoCalculo = Math.Round(t.BaseDoCalculo, 2);

                                DBS.QueryExec(sSql, new object[]{ /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                    /*CODTRB*/ t.TipoTributo, /*BASEDECALCULO*/ t.BaseDoCalculo,
                                    /*ALIQUOTA*/ t.ValorDaAliquota,
                                    /*VALOR*/ t.ValorDoTributo, /*FATORREDUCAO*/ 0, /*FATORSUBSTTRIB*/ 0,
                                    /*BASEDECALCULOCALCULADA*/ 0, /*EDITADO*/ 0, /*VLRISENTO*/ null, /*CODRETENCAO*/ null, /*TIPORECOLHIMENTO*/ null, /*CODTRBBASE*/ null,
                                    /*PERCDIFERIMENTOPARCIALICMS*/ 0, /*SITTRIBUTARIA*/ null, /*MODALIDADEBC*/ null, /*ALIQUOTAPORVALOR*/ null, /*CODUNDREFERENCIA*/ null,
                                    /*BASECHEIA*/ 0,
                                    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(),
                                    /*TIPORENDIMENTO*/ null, /*FORMATRIBUTACAO*/ null});
                            }

                        }

                        #region Valor Bruto do Movimento

                        sSql = @"
    SELECT 
    (
    ISNULL((
    SELECT SUM(QUANTIDADE * PRECOUNITARIO)
    FROM TITMMOV
    WHERE CODCOLIGADA = :CODCOLIGADA
    AND IDMOV = :IDMOV
    ),0)

    +

    ISNULL((
    SELECT SUM(VALOR)
    FROM TTRBMOV
    WHERE CODTRB = 'IPI'
    AND CODCOLIGADA = :CODCOLIGADA
    AND IDMOV = :IDMOV
    ),0)
    ) VALOR

    FROM GCOLIGADA 
    WHERE CODCOLIGADA = :CODCOLIGADA";

                        movimento.ValorBruto = Convert.ToDecimal(DBS.QueryValue(0, sSql, movimento.CodColigada, movimento.IdMov));

                        #endregion

                        #region Valor Outros do Movimento

                        movimento.ValorOutros = movimento.ValorBruto;

                        #endregion

                        #region Valor Liquido do Movimento

                        //movimento.ValorLiquido = movimento.ValorBruto;

                        #endregion

                        #region Atualiza Valores do Movimento

                        sSql = @"UPDATE TMOV SET 
                                VALORBRUTO = :VALORBRUTO, VALORLIQUIDO = :VALORLIQUIDO, VALOROUTROS = :VALOROUTROS, 
                                PERCENTUALFRETE = :PERCENTUALFRETE, VALORFRETE = :VALORFRETE,
                                PERCENTUALSEGURO = :PERCENTUALSEGURO, VALORSEGURO = :VALORSEGURO,
                                PERCENTUALDESC = :PERCENTUALDESC, VALORDESC = :VALORDESC,
                                PERCENTUALDESP = :PERCENTUALDESP, VALORDESP = :VALORDESP,
                                PERCENTUALEXTRA1 = :PERCENTUALEXTRA1, VALOREXTRA1 = :VALOREXTRA1,
                                VALORBRUTOORIG = :VALORBRUTOORIG, VALORLIQUIDOORIG = :VALORLIQUIDOORIG, VALOROUTROSORIG = :VALOROUTROSORIG, IDNAT = :IDNAT
                            WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";

                        this.DBS.SkipControlColumns = true;
                        DBS.QueryExec(sSql, movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros,
                            movimento.PercentualFrete, movimento.ValorFrete,
                            movimento.PercentualSeguro, movimento.ValorSeguro,
                            movimento.PercentualDesc, movimento.ValorDesc,
                            movimento.PercentualDesp, movimento.ValorDesp,
                            movimento.PercentualExtra1, movimento.ValorExtra1,
                            movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros, movimento.IdNat,
                            movimento.CodColigada, movimento.IdMov);
                        this.DBS.SkipControlColumns = false;

                        #endregion

                        sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        this.DBS.SkipControlColumns = true;
                        DBS.QueryExec(sSql, movimento.GuidId);
                        this.DBS.SkipControlColumns = false;

                        DBS.Commit();

                        msg.Retorno = movimento.IdMov;
                        msg.Mensagem = "Movimento criado com sucesso";

                        #endregion
                    }
                    else
                    {
                        #region Rotina Automática

                        /*
                        RM.Mov.Movimento.MovMovAlteracaoPar oMovMovAlteracaoPar = new RM.Mov.Movimento.MovMovAlteracaoPar();

                        oMovMovAlteracaoPar.CodColigada = movimento.CodColigada;
                        oMovMovAlteracaoPar.IdMov = movimento.IdMov;
                        oMovMovAlteracaoPar.CodLoc = movimento.CodLoc;
                        oMovMovAlteracaoPar.CodColCfo = movimento.CodColCfo;
                        oMovMovAlteracaoPar.CodCfo = movimento.CodCfo;
                        oMovMovAlteracaoPar.Serie = movimento.Serie;
                        oMovMovAlteracaoPar.DataEmissao = movimento.DataEmissao;
                        oMovMovAlteracaoPar.DataEntrega = movimento.DataEntrega;
                        oMovMovAlteracaoPar.PrazoEntrega = movimento.PrazoEntrega;
                        oMovMovAlteracaoPar.CodRepresentante = movimento.CodRepresentante;
                        oMovMovAlteracaoPar.FreteCIFouFOB = movimento.FreteCIFouFOB;
                        oMovMovAlteracaoPar.CodTra = movimento.CodTra;

                        oMovMovAlteracaoPar.Observacao = movimento.Observacao;
                        oMovMovAlteracaoPar.HistoricoLongo = movimento.HistoricoLongo;
                        oMovMovAlteracaoPar.NumeroOrdem = movimento.NumeroOrdem;

                        oMovMovAlteracaoPar.CodUsuario = movimento.CodUsuario;
                        oMovMovAlteracaoPar.CodUsuarioLogado = movimento.CodUsuarioLogado;

                        //oMovMovAlteracaoPar.CamposComplementares = movimento.CamposComplementares;
                        oMovMovAlteracaoPar.CamposComplementares = new Dictionary<string, object>();
                        oMovMovAlteracaoPar.CamposComplementares.Add("CLASSREQ", movimento.CLASSREQ);

                        string sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        DataTable Itens = DBS.QuerySelect("ZTITMMOVORCTEMP", sSql, movimento.GuidId);
                        for (int i = 0; i < Itens.Rows.Count; i++)
                        {
                            RM.Mov.Movimento.MovMovItemMovPar oMovMovItemMovPar = new RM.Mov.Movimento.MovMovItemMovPar();

                            oMovMovItemMovPar.CodColigada = Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]);
                            oMovMovItemMovPar.IdMov = movimento.IdMov;
                            oMovMovItemMovPar.NSeqItmMov = Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]);
                            oMovMovItemMovPar.IdPrd = Convert.ToInt32(Itens.Rows[i]["IDPRD"]);
                            oMovMovItemMovPar.IdPrdComposto = (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]);
                            oMovMovItemMovPar.CodUnd = Itens.Rows[i]["CODUND"].ToString();
                            oMovMovItemMovPar.Quantidade = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]);
                            oMovMovItemMovPar.PrecoUnitario = Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                            oMovMovItemMovPar.HistoricoLongo = Itens.Rows[i]["HISTORICOLONGO"].ToString();

                            RM.Mov.Movimento.MovMovTributoPar oMovMovTributoPar = new RM.Mov.Movimento.MovMovTributoPar();
                            oMovMovTributoPar.IdMov = movimento.IdMov;
                            oMovMovTributoPar.CodColigada = oMovMovItemMovPar.CodColigada;
                            oMovMovTributoPar.NSeqItmMov = oMovMovItemMovPar.NSeqItmMov;
                            oMovMovTributoPar.CodTrb = "IPI";
                            oMovMovTributoPar.Aliquota = Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]);
                            oMovMovTributoPar.BaseDeCalculo = oMovMovItemMovPar.Quantidade * ((oMovMovItemMovPar.PrecoUnitario == null) ? 0 : (decimal)oMovMovItemMovPar.PrecoUnitario);

                            oMovMovItemMovPar.TributosItemMov.Add(oMovMovTributoPar);

                            if (oMovMovAlteracaoPar.ItemMovimento == null)
                                oMovMovAlteracaoPar.ItemMovimento = new ObjectList<MovMovItemMovPar>();

                            oMovMovAlteracaoPar.ItemMovimento.Add(oMovMovItemMovPar);
                        }

                        sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        this.DBS.SkipControlColumns = true;
                        DBS.QueryExec(sSql, movimento.GuidId);
                        this.DBS.SkipControlColumns = false;

                        RM.Mov.Cst.Facade.CstMovFacade oCstMovFacade = CreateFacade<RM.Mov.Cst.Facade.CstMovFacade>();
                        oCstMovFacade.Alterar(movimento.CodColigada, movimento.IdMov, oMovMovAlteracaoPar);

                        msg.Retorno = movimento.IdMov;
                        msg.Mensagem = "Movimento alterado com sucesso";
                        */

                        #endregion

                        #region Rotina Manual

                        DBS.BeginTransaction();

                        string sSql = @"UPDATE TMOV SET
                            CODCFO =  :CODCFO,
                            DATAEMISSAO = :DATAEMISSAO, 
                            CODRPR = :CODRPR,
                            NORDEM = :NORDEM, 
                            OBSERVACAO = :OBSERVACAO, 
                            PERCENTUALFRETE  = :PERCENTUALFRETE,
                            VALORFRETE = :VALORFRETE, 
                            PERCENTUALSEGURO = :PERCENTUALSEGURO, 
                            VALORSEGURO = :VALORSEGURO,
                            PERCENTUALDESC = :PERCENTUALDESC, 
                            VALORDESC = :VALORDESC, 
                            PERCENTUALDESP = :PERCENTUALDESP,
                            VALORDESP = :VALORDESP, 
                            PERCENTUALEXTRA1 = :PERCENTUALEXTRA1, 
                            VALOREXTRA1 = :VALOREXTRA1,
                            FRETECIFOUFOB  = :FRETECIFOUFOB, 
                            CODTRA = :CODTRA, 
                            SEGUNDONUMERO = :SEGUNDONUMERO, 
                            CODCOLCFO  = :CODCOLCFO,
                            DATAENTREGA  = :DATAENTREGA, 
                            CAMPOLIVRE1  = :CAMPOLIVRE1, 
                            HORULTIMAALTERACAO  = :HORULTIMAALTERACAO, 
                            PRAZOENTREGA = :PRAZOENTREGA, 
                            CODUSUARIO = :CODUSUARIO,
                            RECMODIFIEDBY  = :RECMODIFIEDBY, 
                            RECMODIFIEDON = :RECMODIFIEDON,
                            IDNAT = :IDNAT
                        WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";

                        DBS.QueryExec(sSql,
                            /*CODCFO*/ movimento.CodCfo,
                            /*DATAEMISSAO*/ movimento.DataEmissao,
                            /*CODRPR*/ movimento.CodRepresentante,
                            /*NORDEM*/ movimento.NumeroOrdem,
                            /*OBSERVACAO*/ movimento.Observacao,
                            /*PERCENTUALFRETE*/ movimento.PercentualFrete,
                            /*VALORFRETE*/ movimento.ValorFrete,
                            /*PERCENTUALSEGURO*/ movimento.PercentualSeguro,
                            /*VALORSEGURO*/ movimento.ValorSeguro,
                            /*PERCENTUALDESC*/ movimento.PercentualDesc,
                            /*VALORDESC*/ movimento.ValorDesc,
                            /*PERCENTUALDESP*/ movimento.PercentualDesp,
                            /*VALORDESP*/ movimento.ValorDesp,
                            /*PERCENTUALEXTRA1*/ movimento.PercentualExtra1,
                            /*VALOREXTRA1*/ movimento.ValorExtra1,
                            /*FRETECIFOUFOB*/ movimento.FreteCIFouFOB,
                            /*CODTRA*/ movimento.CodTra,
                            /*SEGUNDONUMERO*/ movimento.SegundoNumero,
                            /*CODCOLCFO*/ movimento.CodColCfo,
                            /*DATAENTREGA*/ movimento.DataEntrega,
                            /*CAMPOLIVRE1*/ movimento.CampoLivre1,
                            /*HORULTIMAALTERACAO*/ DBS.ServerDate(),
                            /*PRAZOENTREGA*/ movimento.PrazoEntrega,
                            /*CODUSUARIO*/ movimento.CodUsuario,
                            /*RECMODIFIEDBY*/ movimento.CodUsuario,
                            /*RECMODIFIEDON*/ DBS.ServerDate(),
                            /*IDNAT*/ movimento.IdNat,
                            /*CODCOLIGADA*/ movimento.CodColigada,
                            /*IDMOV*/ movimento.IdMov);

                        sSql = @"UPDATE TMOVHISTORICO SET 
                                    HISTORICOLONGO = :HISTORICOLONGO, 
                                    RECMODIFIEDBY = :RECMODIFIEDBY, 
                                    RECMODIFIEDON = :RECMODIFIEDON
                                WHERE CODCOLIGADA = :CODCOLIGADA
                                    AND IDMOV = :IDMOV";

                        DBS.QueryExec(sSql,
                            /*HISTORICOLONGO*/ movimento.HistoricoLongo,
                            /*RECMODIFIEDBY*/ movimento.CodUsuario,
                            /*RECMODIFIEDON*/ DBS.ServerDate(),
                            /*CODCOLIGADA*/ movimento.CodColigada,
                            /*IDMOV*/ movimento.IdMov);

                        sSql = @"UPDATE TMOVCOMPL SET 
                                    CLASSREQ = :CLASSREQ, 
                                    CLASSREQCPL = :CLASSREQCPL,
                                    RECMODIFIEDBY = :RECMODIFIEDBY, 
                                    RECMODIFIEDON = :RECMODIFIEDON
                                WHERE CODCOLIGADA = :CODCOLIGADA
                                    AND IDMOV = :IDMOV";

                        DBS.QueryExec(sSql,
                            /*CLASSREQ*/ movimento.CLASSREQ,
                            /*CLASSREQCPL*/ movimento.CLASSREQCPL,
                            /*RECMODIFIEDBY*/ movimento.CodUsuario,
                            /*RECMODIFIEDON*/ DBS.ServerDate(),
                            /*CODCOLIGADA*/ movimento.CodColigada,
                            /*IDMOV*/ movimento.IdMov);

                        this.DBS.SkipControlColumns = true;
                        sSql = @"DELETE FROM TITMMOVFISCAL WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                        DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);

                        sSql = @"DELETE FROM TTRBMOV WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                        DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);

                        sSql = @"DELETE FROM TITMMOVCOMPL WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                        DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);

                        sSql = @"DELETE FROM TITMMOVHISTORICO WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                        DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);

                        sSql = @"DELETE FROM TITMMOV WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                        DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov);
                        this.DBS.SkipControlColumns = false;

                        sSql = @"SELECT * FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        DataTable Itens = DBS.QuerySelect("ZTITMMOVORCTEMP", sSql, movimento.GuidId);
                        for (int i = 0; i < Itens.Rows.Count; i++)
                        {
                            string sCODTB2FAT = null;
                            string sCODTB3FAT = null;
                            string sCODTB4FAT = null;

                            sSql = @"SELECT TPRD.CODTB2FAT, TPRD.CODTB3FAT, TPRD.CODTB4FAT FROM TPRD WHERE TPRD.CODCOLIGADA = :CODCOLIGADA AND TPRD.IDPRD = :IDPRD";
                            DataTable dtPrd = DBS.QuerySelect("TPRD", sSql, Itens.Rows[i]["CODCOLIGADA"], Itens.Rows[i]["IDPRD"]);
                            foreach (DataRow row in dtPrd.Rows)
                            {
                                sCODTB2FAT = (row["CODTB2FAT"] == DBNull.Value) ? null : row["CODTB2FAT"].ToString();
                                sCODTB3FAT = (row["CODTB3FAT"] == DBNull.Value) ? null : row["CODTB3FAT"].ToString();
                                sCODTB4FAT = (row["CODTB4FAT"] == DBNull.Value) ? null : row["CODTB4FAT"].ToString();
                            }

                            sSql = @"INSERT INTO TITMMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, NUMEROSEQUENCIAL, IDPRD, CODTIP, QUANTIDADE, PRECOUNITARIO, PRECOTABELA, 
                                            PERCENTUALDESC, VALORDESC, PERCENTUALDESP, VALORDESP, DATAEMISSAO, 
                                            CODMEN, NUMEROTRIBUTOS, CODTB1FAT, CODTB2FAT, CODTB3FAT, CODTB4FAT, 
                                            CODTB5FAT, CODTB1FLX, CODTB2FLX, CODTB3FLX, CODTB4FLX, CODTB5FLX, CAMPOLIVRE, 
                                            CODUND, QUANTIDADEARECEBER, CODNAT, CODCPG, DATAENTREGA, PRATELEIRA, IDCNT, NSEQITMCNT, 
                                            DATAINIFAT, DATAFIMFAT, FLAGEFEITOSALDO, VALORUNITARIO, VALORFINANCEIRO, 
                                            IMPRIMEMOV, CODCCUSTO, FLAGREPASSE, ALIQORDENACAO, QUANTIDADEORIGINAL, 
                                            IDNAT, FLAG, CHAPA, INICIO, TERMINO, PREVINICIO, STATUS, BLOCK, 
                                            FLAGREFATURAMENTO, IDCNTDESTINO, NSEQITMCNTDEST, FATORCONVUND, IDPRJ, IDTRF, 
                                            VALORTOTALITEM, VALORCODIGOPRD, TIPOCODIGOPRD, QTDUNDPEDIDO, TRIBUTACAOECF, 
                                            CODFILIAL, CODDEPARTAMENTO, IDPRDCOMPOSTO, QUANTIDADESEPARADA, PERCENTCOMISSAO, INDICENCM, NCM, CODRPR, COMISSAOREPRES, 
                                            NSEQITMCNTMEDICAO, VALORESCRITURACAO, VALORFINPEDIDO, VALORFRETECTRC, VALOROPFRM1, VALOROPFRM2, 
                                            IDOBJOFICINA, PRECOEDITADO, QTDEVOLUMEUNITARIO, IDGRD, 
                                            CODVEN1, CODLOCALBN, REGISTROEXPORTACAO, DATARE, PRECOTOTALEDITADO, CST, 
                                            VALORDESCCONDICONALITM, VALORDESPCONDICIONALITM, DATAORCAMENTO, CODTBORCAMENTO, RATEIOFRETE, 
                                            RATEIOSEGURO, RATEIODESC, RATEIODESP, RATEIOEXTRA1, RATEIOEXTRA2, RATEIOFRETECTRC, 
                                            RATEIODEDMAT, RATEIODEDSUB, RATEIODEDOUT, IDCLASSIFENERGIACOMUNIC, VALORUNTORCAMENTO, 
                                            VALSERVICONFE, CODLOC, VALORBEM, VALORLIQUIDO, CODIGOCODIF, CODMUNSERVICO, CODETDMUNSERV, RATEIOCCUSTODEPTO, CUSTOREPOSICAO, 
                                            CUSTOREPOSICAOB, VALORFINTERCEIROS, VALORFINANCGERENCIAL, CODIGOSERVICO, VALORUNITGERENCIAL, 
                                            IDINTEGRACAO, IDTABPRECO, VALORBRUTOITEM, VALORBRUTOITEMORIG, CODCOLTBORCAMENTO, CODPUBLIC, QUANTIDADETOTAL, 
                                            PRODUTOSUBSTITUTO, CODTBGRUPOORC, PRECOUNITARIOSELEC, VALORRATEIOLAN, 
                                            RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                        VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :NUMEROSEQUENCIAL, :IDPRD, :CODTIP, :QUANTIDADE, :PRECOUNITARIO, :PRECOTABELA, 
                                            :PERCENTUALDESC, :VALORDESC, :PERCENTUALDESP, :VALORDESP, :DATAEMISSAO, 
                                            :CODMEN, :NUMEROTRIBUTOS, :CODTB1FAT, :CODTB2FAT, :CODTB3FAT, :CODTB4FAT, 
                                            :CODTB5FAT, :CODTB1FLX, :CODTB2FLX, :CODTB3FLX, :CODTB4FLX, :CODTB5FLX, :CAMPOLIVRE, 
                                            :CODUND, :QUANTIDADEARECEBER, :CODNAT, :CODCPG, :DATAENTREGA, :PRATELEIRA, :IDCNT, :NSEQITMCNT, 
                                            :DATAINIFAT, :DATAFIMFAT, :FLAGEFEITOSALDO, :VALORUNITARIO, :VALORFINANCEIRO, 
                                            :IMPRIMEMOV, :CODCCUSTO, :FLAGREPASSE, :ALIQORDENACAO, :QUANTIDADEORIGINAL, 
                                            :IDNAT, :FLAG, :CHAPA, :INICIO, :TERMINO, :PREVINICIO, :STATUS, :BLOCK, 
                                            :FLAGREFATURAMENTO, :IDCNTDESTINO, :NSEQITMCNTDEST, :FATORCONVUND, :IDPRJ, :IDTRF, 
                                            :VALORTOTALITEM, :VALORCODIGOPRD, :TIPOCODIGOPRD, :QTDUNDPEDIDO, :TRIBUTACAOECF, 
                                            :CODFILIAL, :CODDEPARTAMENTO, :IDPRDCOMPOSTO, :QUANTIDADESEPARADA, :PERCENTCOMISSAO, :INDICENCM, :NCM, :CODRPR, :COMISSAOREPRES, 
                                            :NSEQITMCNTMEDICAO, :VALORESCRITURACAO, :VALORFINPEDIDO, :VALORFRETECTRC, :VALOROPFRM1, :VALOROPFRM2, 
                                            :IDOBJOFICINA, :PRECOEDITADO, :QTDEVOLUMEUNITARIO, :IDGRD, 
                                            :CODVEN1, :CODLOCALBN, :REGISTROEXPORTACAO, :DATARE, :PRECOTOTALEDITADO, :CST, 
                                            :VALORDESCCONDICONALITM, :VALORDESPCONDICIONALITM, :DATAORCAMENTO, :CODTBORCAMENTO, :RATEIOFRETE, 
                                            :RATEIOSEGURO, :RATEIODESC, :RATEIODESP, :RATEIOEXTRA1, :RATEIOEXTRA2, :RATEIOFRETECTRC, 
                                            :RATEIODEDMAT, :RATEIODEDSUB, :RATEIODEDOUT, :IDCLASSIFENERGIACOMUNIC, :VALORUNTORCAMENTO, 
                                            :VALSERVICONFE, :CODLOC, :VALORBEM, :VALORLIQUIDO, :CODIGOCODIF, :CODMUNSERVICO, :CODETDMUNSERV, :RATEIOCCUSTODEPTO, :CUSTOREPOSICAO, 
                                            :CUSTOREPOSICAOB, :VALORFINTERCEIROS, :VALORFINANCGERENCIAL, :CODIGOSERVICO, :VALORUNITGERENCIAL, 
                                            :IDINTEGRACAO, :IDTABPRECO, :VALORBRUTOITEM, :VALORBRUTOITEMORIG, :CODCOLTBORCAMENTO, :CODPUBLIC, :QUANTIDADETOTAL, 
                                            :PRODUTOSUBSTITUTO, :CODTBGRUPOORC, :PRECOUNITARIOSELEC, :VALORRATEIOLAN, 
                                            :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";


                            DBS.QueryExec(sSql,
                                /*CODCOLIGADA*/ Convert.ToInt32(Itens.Rows[i]["CODCOLIGADA"]),
                                /*IDMOV*/ movimento.IdMov,
                                /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*NUMEROSEQUENCIAL*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*IDPRD*/ Convert.ToInt32(Itens.Rows[i]["IDPRD"]),
                                /*CODTIP*/ null,
                                /*QUANTIDADE*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                /*PRECOUNITARIO*/ Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                /*PRECOTABELA*/ 0,
                                /*PERCENTUALDESC*/ null, /*VALORDESC*/ null, /*PERCENTUALDESP*/ null, /*VALORDESP*/ null, /*DATAEMISSAO*/ movimento.DataEmissao,
                                /*CODMEN*/ null, /*NUMEROTRIBUTOS*/ null, /*CODTB1FAT*/ null, /*CODTB2FAT*/ sCODTB2FAT, /*CODTB3FAT*/ sCODTB3FAT, /*CODTB4FAT:*/ sCODTB4FAT,
                                /*CODTB5FAT*/ null, /*CODTB1FLX*/ null, /*CODTB2FLX*/ null, /*CODTB3FLX*/ null, /*CODTB4FLX*/ null, /*CODTB5FLX*/ null, /*CAMPOLIVRE*/ null,
                                /*CODUND*/ Itens.Rows[i]["CODUND"].ToString(),
                                /*QUANTIDADEARECEBER*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                /*CODNAT*/ null, /*CODCPG*/ null, /*DATAENTREGA*/ movimento.DataEntrega, /*PRATELEIRA*/ null, /*IDCNT*/ null, /*NSEQITMCNT*/ null,
                                /*DATAINIFAT*/ null, /*DATAFIMFAT*/ null, /*FLAGEFEITOSALDO*/ null, /*VALORUNITARIO*/ 0,  /*VALORFINANCEIRO*/ 0,
                                /*IMPRIMEMOV*/ null, /*CODCCUSTO*/ null, /*FLAGREPASSE*/ null, /*ALIQORDENACAO*/ 0,
                                /*QUANTIDADEORIGINAL*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                /*IDNAT*/ Itens.Rows[i]["IDNAT"].ToString(), /*FLAG*/ 0, /*CHAPA*/ null, /*INICIO*/ null, /*TERMINO*/ null, /*PREVINICIO*/ null, /*STATUS*/ null, /*BLOCK*/ null,
                                /*FLAGREFATURAMENTO*/ null, /*IDCNTDESTINO*/ null, /*NSEQITMCNTDEST*/ null, /*FATORCONVUND*/ 0, /*IDPRJ*/ null, /*IDTRF*/ null,
                                /*VALORTOTALITEM*/ 0, /*VALORCODIGOPRD*/ null, /*TIPOCODIGOPRD*/ null, /*QTDUNDPEDIDO*/ null, /*TRIBUTACAOECF*/ null,
                                /*CODFILIAL*/ movimento.CodFilial, /*CODDEPARTAMENTO*/ null,
                                /*IDPRDCOMPOSTO*/ (Itens.Rows[i]["IDPRDCOMPOSTO"] == DBNull.Value) ? null : (int?)Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]),
                                /*QUANTIDADESEPARADA*/ 0, /*PERCENTCOMISSAO*/ 0, /*INDICENCM*/ null, /*NCM*/ null, /*CODRPR*/ null, /*COMISSAOREPRES*/ 0,
                                /*NSEQITMCNTMEDICAO*/ null, /*VALORESCRITURACAO*/ 0, /*VALORFINPEDIDO*/ 0, /*VALORFRETECTRC*/ null, /*VALOROPFRM1*/ 0, /*VALOROPFRM2*/ 0,
                                /*IDOBJOFICINA*/ null, /*PRECOEDITADO*/ 0, /*QTDEVOLUMEUNITARIO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]), /*IDGRD*/ null,
                                /*CODVEN1*/ movimento.CodVen1, /*CODLOCALBN*/ null, /*REGISTROEXPORTACAO*/ null, /*DATARE*/ null, /*PRECOTOTALEDITADO*/ null, /*CST*/ null,
                                /*VALORDESCCONDICONALITM*/ 0, /*VALORDESPCONDICIONALITM*/ 0, /*DATAORCAMENTO*/ null, /*CODTBORCAMENTO*/ null, /*RATEIOFRETE*/ null,
                                /*RATEIOSEGURO*/ null, /*RATEIODESC*/ null, /*RATEIODESP*/ null, /*RATEIOEXTRA1*/ null, /*RATEIOEXTRA2*/ null, /*RATEIOFRETECTRC*/ null,
                                /*RATEIODEDMAT*/ null, /*RATEIODEDSUB*/ null, /*RATEIODEDOUT*/ null, /*IDCLASSIFENERGIACOMUNIC*/ null, /*VALORUNTORCAMENTO*/ 0,
                                /*VALSERVICONFE*/ 0, /*CODLOC*/ movimento.CodLoc, /*VALORBEM*/ 0,
                                /*VALORLIQUIDO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                /*CODIGOCODIF*/ null, /*CODMUNSERVICO*/ null, /*CODETDMUNSERV*/ null, /*RATEIOCCUSTODEPTO*/ null, /*CUSTOREPOSICAO*/ null,
                                /*CUSTOREPOSICAOB*/ null, /*VALORFINTERCEIROS*/ null, /*VALORFINANCGERENCIAL*/ null, /*CODIGOSERVICO*/ null, /*VALORUNITGERENCIAL*/ null,
                                /*IDINTEGRACAO*/ null, /*IDTABPRECO*/ null,
                                /*VALORBRUTOITEM*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                /*VALORBRUTOITEMORIG*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                                /*CODCOLTBORCAMENTO*/ null, /*CODPUBLIC*/ null, /*QUANTIDADETOTAL*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]),
                                /*PRODUTOSUBSTITUTO*/ 0, /*CODTBGRUPOORC*/ null, /*PRECOUNITARIOSELEC*/ Itens.Rows[i]["PRECOUNITARIOSELEC"].ToString(), /*VALORRATEIOLAN*/ null,
                                /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                            sSql = @"INSERT INTO TITMMOVHISTORICO (CODCOLIGADA, IDMOV, NSEQITMMOV, HISTORICOCURTO, HISTORICOLONGO, 
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                    VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :HISTORICOCURTO, :HISTORICOLONGO, 
                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                            DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*HISTORICOCURTO*/ null, /*HISTORICOLONGO*/ Itens.Rows[i]["HISTORICOLONGO"].ToString(),
                                /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate());

                            sSql = @"INSERT INTO TITMMOVCOMPL (CODCOLIGADA, IDMOV, NSEQITMMOV, PRODPRICIPAL, SEQUENCIAL, TIPOMAT, 
                                        RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON)
                                    VALUES(:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :PRODPRICIPAL, :SEQUENCIAL, :TIPOMAT, 
                                        :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :APLICPROD)";

                            DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                /*PRODPRICIPAL*/ Itens.Rows[i]["PRODPRICIPAL"].ToString(),
                                /*SEQUENCIAL*/ Itens.Rows[i]["SEQUENCIAL"].ToString(),
                                /*TIPOMAT*/ Itens.Rows[i]["TIPOMAT"].ToString(),
                                /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(), /*APLICPROD*/ Itens.Rows[i]["APLICPROD"]);

                            sSql = @"INSERT INTO TTRBMOV (CODCOLIGADA, IDMOV, NSEQITMMOV, CODTRB, BASEDECALCULO, ALIQUOTA, VALOR, FATORREDUCAO, FATORSUBSTTRIB, 
                                            BASEDECALCULOCALCULADA, EDITADO, VLRISENTO, CODRETENCAO, TIPORECOLHIMENTO, CODTRBBASE, PERCDIFERIMENTOPARCIALICMS, 
                                            SITTRIBUTARIA, MODALIDADEBC, ALIQUOTAPORVALOR, CODUNDREFERENCIA, BASECHEIA, 
                                            RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON, TIPORENDIMENTO, FORMATRIBUTACAO) 
                                        VALUES (:CODCOLIGADA, :IDMOV, :NSEQITMMOV, :CODTRB, :BASEDECALCULO, :ALIQUOTA, :VALOR, :FATORREDUCAO, :FATORSUBSTTRIB, 
                                            :BASEDECALCULOCALCULADA, :EDITADO, :VLRISENTO, :CODRETENCAO, :TIPORECOLHIMENTO, :CODTRBBASE, :PERCDIFERIMENTOPARCIALICMS, 
                                            :SITTRIBUTARIA, :MODALIDADEBC, :ALIQUOTAPORVALOR, :CODUNDREFERENCIA, :BASECHEIA, 
                                            :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON, :TIPORENDIMENTO, :FORMATRIBUTACAO)";

                            string sql = String.Empty;

                            #region Calculo do Trbiuto

                            Decimal BaseCalculo = 0;
                            Decimal Aliquota = 0;
                            Decimal ValorTributo = 0;
                            DataTable dtTributo = new DataTable();
                            Tributo IPI = new Tributo();
                            Tributo PIS = new Tributo();
                            Tributo COFINS = new Tributo();
                            Tributo ICMS = new Tributo();
                            Tributo ICMSST = new Tributo();

                            List<Tributo> Tributos = new List<Tributo>();

                            #region Calculo do Trbiuto IPI

                            Aliquota = Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]);

                            if (Itens.Rows[i]["IDPRDCOMPOSTO"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(Itens.Rows[i]["IDPRDCOMPOSTO"]) == 0)
                                {
                                    BaseCalculo = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                                    ValorTributo = new Util().Arredonda((BaseCalculo * Aliquota) / 100);
                                }
                            }
                            else
                            {
                                BaseCalculo = Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]);
                                ValorTributo = new Util().Arredonda((BaseCalculo * Aliquota) / 100);
                            }

                            IPI.TipoTributo = "IPI";
                            IPI.BaseDoCalculo = BaseCalculo;
                            IPI.ValorDaAliquota = Aliquota;
                            IPI.ValorDoTributo = ValorTributo;

                            Tributos.Add(IPI);

                            #endregion

                            #region Calculo do Trbiuto PIS
                            sql = String.Format(@"select  (isnull(TITMMOV.VALORTOTALITEM, 0) - 
		                                                        isnull(TITMMOV.RATEIODESC, 0) + 
		                                                        isnull(TITMMOV.RATEIODESP, 0) + 
		                                                        isnull(TITMMOV. RATEIOFRETE, 0)) as 'BASECALCULO',
		                                                        DTRBNATUREZA.ALIQTRB as 'ALIQUOTA',
		                                                        NSEQITMMOV 

                                                        from TITMMOV (NOLOCK)

                                                        inner join DTRBNATUREZA (NOLOCK)
                                                        on DTRBNATUREZA.IDNAT = TITMMOV.IDNAT
                                                        and DTRBNATUREZA.CODCOLIGADA = TITMMOV.CODCOLIGADA

                                                        where TITMMOV.CODCOLIGADA = '{0}'
                                                        and IDMOV = '{1}'
                                                        and TITMMOV.NSEQITMMOV = '{2}'
                                                        and DTRBNATUREZA.CODTRB = 'PIS'", AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                PIS.TipoTributo = "PIS";
                                PIS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString().Replace(",", "."));
                                PIS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString().Replace(",", "."));
                                PIS.ValorDoTributo = PIS.BaseDoCalculo * PIS.ValorDaAliquota;
                            }

                            Tributos.Add(PIS);


                            #endregion

                            #region Calculo do Trbiuto COFINS
                            sql = String.Format(@"select  (isnull(TITMMOV.VALORTOTALITEM, 0) - 
		                                                        isnull(TITMMOV.RATEIODESC, 0) + 
		                                                        isnull(TITMMOV.RATEIODESP, 0) + 
		                                                        isnull(TITMMOV. RATEIOFRETE, 0)) as 'BASECALCULO',
		                                                        DTRBNATUREZA.ALIQTRB as 'ALIQUOTA',
		                                                        NSEQITMMOV 

                                                        from TITMMOV (NOLOCK)

                                                        inner join DTRBNATUREZA (NOLOCK)
                                                        on DTRBNATUREZA.IDNAT = TITMMOV.IDNAT
                                                        and DTRBNATUREZA.CODCOLIGADA = TITMMOV.CODCOLIGADA

                                                        where TITMMOV.CODCOLIGADA = '{0}'
                                                        and IDMOV = '{1}'
                                                        and TITMMOV.NSEQITMMOV = '{2}'
                                                        and DTRBNATUREZA.CODTRB = 'COFINS'", AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]));

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                COFINS.TipoTributo = "COFINS";
                                COFINS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString().Replace(",", "."));
                                COFINS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString().Replace(",", "."));
                                COFINS.ValorDoTributo = COFINS.BaseDoCalculo * COFINS.ValorDaAliquota;
                            }

                            Tributos.Add(COFINS);


                            #endregion

                            #region Calculo do Trbiuto ICMS

                            sql = String.Format(@"SELECT FATORICMS FROM DREGRAICMS (NOLOCK) WHERE IDREGRAICMS IN (
		                                                SELECT IDREGRAICMS FROM DCFOP (NOLOCK) WHERE CODCOLIGADA = 1 AND IDNAT = '{0}')
		                                                AND CODCOLIGADA = 1", Itens.Rows[i]["IDNAT"].ToString());

                            sql = String.Format(@"select CASE 
	
	                                                                WHEN 

		                                                                (SELECT FATORICMS FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
		                                                                AND CODCOLIGADA = 1) > 0 
		
		                                                                AND 
		
		                                                                (SELECT BASEICMSCOMIPI FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
		                                                                AND CODCOLIGADA = 1) = 1
		
	                                                                THEN
		                                                                isnull((
		                                                                (TITMMOV.VALORTOTALITEM + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) 
		                                                                - 
		                                                                ((TITMMOV.VALORTOTALITEM + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * '{1}' /100 )
		                                                                ),0)
		
	                                                                ELSE
		
		                                                                CASE
		
		                                                                WHEN
		
			                                                                (SELECT BASEICMSCOMIPI FROM DREGRAICMS WHERE IDREGRAICMS IN (
			                                                                SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
			                                                                AND CODCOLIGADA = 1) = 1		
		
		                                                                THEN
			
			                                                                isnull(((TITMMOV.VALORTOTALITEM +  '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE)),0)
		
		                                                                ELSE
		
			                                                                isnull(((TITMMOV.VALORTOTALITEM - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE)),0)

		                                                                END
                                                                END as 'BASECALCULO',
                                                                (SELECT ALIQICMS FROM DREGRAICMS WHERE IDREGRAICMS IN (
		                                                                    SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{5}')
                                                                    AND CODCOLIGADA = 1) as 'ALIQUOTA'

                                                                from TITMMOV (NOLOCK)

                                                                where CODCOLIGADA = '{2}'
                                                                and IDMOV = '{3}'
                                                                and NSEQITMMOV = '{4}'", IPI.ValorDoTributo.ToString().Replace(",", "."), MetodosSQL.GetField(sql, "FATORICMS").Replace(",", "."), AppLib.Context.Empresa, movimento.IdMov, Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]), Itens.Rows[i]["IDNAT"].ToString());

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                ICMS.TipoTributo = "ICMS";
                                ICMS.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString().Replace(",", "."));
                                ICMS.ValorDaAliquota = Convert.ToDecimal(row["ALIQUOTA"].ToString().Replace(",", "."));
                                ICMS.ValorDoTributo = ICMS.BaseDoCalculo * ICMS.ValorDaAliquota;
                            }

                            Tributos.Add(ICMS);

                            #endregion

                            #region Calculo do Trbiuto ICMSST

                            sql = String.Format(@"SELECT FATORREDST, ALIQSUBST, FATORSUBST FROM DREGRAICMS WHERE IDREGRAICMS IN (
                                                    SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{0}')
                                                    AND CODCOLIGADA = 1", Itens.Rows[i]["IDNAT"].ToString());

                            dtTributo = MetodosSQL.GetDT(sql);


                            #region Query ICMS-ST
                            foreach (DataRow row in dtTributo.Rows)
                            {
                                sql = String.Format(@"SELECT 

CASE 
	
WHEN 

	(SELECT OPERACAOCONSUMIDORFINAL 
	FROM TMOVFISCAL 
	WHERE CODCOLIGADA = 1 AND IDMOV = '{5}') = 1
		
THEN
		
	0
		
ELSE
		
	CASE
		
	WHEN 
	
		(SELECT 1
		  FROM DCFOP, DREGRAICMS
		 WHERE DCFOP.CODCOLIGADA = DREGRAICMS.CODCOLIGADA
		   AND DCFOP.IDREGRAICMS = DREGRAICMS.IDREGRAICMS
		   AND DREGRAICMS.TIPOOPERACAOTRIBUTARIA IN (1,2)
		   AND DREGRAICMS.UTIBCDIFALICMSDENTRO IN (1,2)
		   AND DCFOP.CODCOLIGADA = 1
		   AND DCFOP.CODNAT = '{6}') = 1
	THEN
			
		CASE
		
		WHEN
				 
			(SELECT 1
			  FROM DCFOP, DREGRAICMS
			 WHERE DCFOP.CODCOLIGADA = DREGRAICMS.CODCOLIGADA
			   AND DCFOP.IDREGRAICMS = DREGRAICMS.IDREGRAICMS
			   AND DREGRAICMS.TIPOOPERACAOTRIBUTARIA IN (1,2)
			   AND DREGRAICMS.UTIBCDIFALICMSDENTRO = 1
			   AND DCFOP.CODCOLIGADA = 1
			   AND DCFOP.CODNAT = '{6}') = 1
		THEN
					
			CASE
			
			WHEN
				(SELECT FATORREDST FROM DREGRAICMS WHERE IDREGRAICMS IN (
				SELECT IDREGRAICMS FROM DCFOP WHERE CODCOLIGADA = 1 AND IDNAT = '{6}')
				AND CODCOLIGADA = 1) > 0 
			
			THEN
			
				((((((TITMMOV.VALORTOTALITEM  + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
                ((TITMMOV.VALORTOTALITEM  + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ('{3}'/100) ))) +    
                ((((TITMMOV.VALORTOTALITEM  + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) -   
                ((TITMMOV.VALORTOTALITEM  + '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * '{3}'/100 ))) * ('{2}'/100))) - '{1}' ) / (1 - (('{4}')/100)))
			
			ELSE
			
				((((TITMMOV.VALORTOTALITEM +  '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) +    
				((TITMMOV.VALORTOTALITEM +  '{0}' - TITMMOV.RATEIODESC + TITMMOV.RATEIODESP + TITMMOV.RATEIOFRETE) * ('{2}'/100)))  - '{1}' ) / (1 - (('{4}')/100)))

			END
		ELSE
			0
		END
	ELSE
		0
	END
END
as 'BASECALCULO'
FROM TITMMOV (NOLOCK) WHERE IDMOV = '{5}'", /*VALORIPI*/ IPI.ValorDoTributo.ToString().Replace(",", "."),
                                   /*VALORICMS*/ ICMS.ValorDoTributo.ToString().Replace(",", "."),
                                   /*FATORSUBST*/ row["FATORSUBST"].ToString().Replace(",", "."),
                                   /*FATORREDST*/ row["FATORREDST"].ToString().Replace(",", "."),
                                   /*ALIQSUBST*/ row["ALIQSUBST"].ToString().Replace(",", "."),
                                   /*IDMOV*/ movimento.IdMov,
                                   /*IDNAT*/ Convert.ToInt32(Itens.Rows[i]["IDNAT"]));

                                ICMSST.ValorDaAliquota = Convert.ToDecimal(row["ALIQSUBST"].ToString().Replace(",", "."));
                            }
                            #endregion

                            dtTributo = MetodosSQL.GetDT(sql);

                            foreach (DataRow row in dtTributo.Rows)
                            {
                                ICMSST.TipoTributo = "ICMSST";
                                ICMSST.BaseDoCalculo = Convert.ToDecimal(row["BASECALCULO"].ToString().Replace(",", "."));

                                ICMSST.ValorDoTributo = ICMSST.BaseDoCalculo * ICMSST.ValorDaAliquota;
                            }



                            Tributos.Add(ICMSST);

                            #endregion


                            //DBS.QueryExec(sSql, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                            //    /*CODTRB*/ "IPI", /*BASEDECALCULO*/ Convert.ToDecimal(Itens.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(Itens.Rows[i]["PRECOUNITARIO"]),
                            //    /*ALIQUOTA*/ Convert.ToDecimal(Itens.Rows[i]["ALIQUOTAIPI"]),
                            //    /*VALOR*/ ValorTributo, /*FATORREDUCAO*/ 0, /*FATORSUBSTTRIB*/ 0,
                            //    /*BASEDECALCULOCALCULADA*/ 0, /*EDITADO*/ 0, /*VLRISENTO*/ null, /*CODRETENCAO*/ "0561", /*TIPORECOLHIMENTO*/ null, /*CODTRBBASE*/ null,
                            //    /*PERCDIFERIMENTOPARCIALICMS*/ 0, /*SITTRIBUTARIA*/ null, /*MODALIDADEBC*/ null, /*ALIQUOTAPORVALOR*/ null, /*CODUNDREFERENCIA*/ null,
                            //    /*BASECHEIA*/ 0,
                            //    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(),
                            //    /*TIPORENDIMENTO*/ null, /*FORMATRIBUTACAO*/ null);

                            //List<string> tributos = new List<string>();
                            //tributos.Add("IPI");
                            //tributos.Add("ICM-SF");
                            //tributos.Add("PIS-SF");
                            //tributos.Add("COF-SF");
                            //tributos.Add("ICMS");
                            //tributos.Add("ICMSST");

                            foreach (Tributo t in Tributos)
                            {
                                t.ValorDoTributo = Math.Round(t.ValorDoTributo, 2);
                                t.BaseDoCalculo = Math.Round(t.BaseDoCalculo, 2);

                                DBS.QueryExec(sSql, new object[]{ /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov, /*NSEQITMMOV*/ Convert.ToInt32(Itens.Rows[i]["NSEQITMMOV"]),
                                    /*CODTRB*/ t.TipoTributo, /*BASEDECALCULO*/ t.BaseDoCalculo,
                                    /*ALIQUOTA*/ t.ValorDaAliquota,
                                    /*VALOR*/ t.ValorDoTributo, /*FATORREDUCAO*/ 0, /*FATORSUBSTTRIB*/ 0,
                                    /*BASEDECALCULOCALCULADA*/ 0, /*EDITADO*/ 0, /*VLRISENTO*/ null, /*CODRETENCAO*/ null, /*TIPORECOLHIMENTO*/ null, /*CODTRBBASE*/ null,
                                    /*PERCDIFERIMENTOPARCIALICMS*/ 0, /*SITTRIBUTARIA*/ null, /*MODALIDADEBC*/ null, /*ALIQUOTAPORVALOR*/ null, /*CODUNDREFERENCIA*/ null,
                                    /*BASECHEIA*/ 0,
                                    /*RECCREATEDBY*/ movimento.CodUsuario, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ movimento.CodUsuario, /*RECMODIFIEDON*/ DBS.ServerDate(),
                                    /*TIPORENDIMENTO*/ null, /*FORMATRIBUTACAO*/ null});
                            }
                        }

                        #region Valor Bruto do Movimento

                        sSql = @"
    SELECT 
    (
    ISNULL((
    SELECT SUM(QUANTIDADE * PRECOUNITARIO)
    FROM TITMMOV
    WHERE CODCOLIGADA = :CODCOLIGADA
    AND IDMOV = :IDMOV
    ),0)

    +

    ISNULL((
    SELECT SUM(VALOR)
    FROM TTRBMOV
    WHERE CODTRB = 'IPI'
    AND CODCOLIGADA = :CODCOLIGADA
    AND IDMOV = :IDMOV
    ),0)
    ) VALOR

    FROM GCOLIGADA 
    WHERE CODCOLIGADA = :CODCOLIGADA";

                        movimento.ValorBruto = Convert.ToDecimal(DBS.QueryValue(0, sSql, movimento.CodColigada, movimento.IdMov));

                        #endregion

                        #region Valor Outros do Movimento

                        movimento.ValorOutros = movimento.ValorBruto;

                        #endregion

                        #region Valor Liquido do Movimento

                        //movimento.ValorLiquido = movimento.ValorBruto;

                        #endregion

                        #region Atualiza Valores do Movimento

                        sSql = @"UPDATE TMOV SET 
                                VALORBRUTO = :VALORBRUTO, VALORLIQUIDO = :VALORLIQUIDO, VALOROUTROS = :VALOROUTROS, 
                                PERCENTUALFRETE = :PERCENTUALFRETE, VALORFRETE = :VALORFRETE,
                                PERCENTUALSEGURO = :PERCENTUALSEGURO, VALORSEGURO = :VALORSEGURO,
                                PERCENTUALDESC = :PERCENTUALDESC, VALORDESC = :VALORDESC,
                                PERCENTUALDESP = :PERCENTUALDESP, VALORDESP = :VALORDESP,
                                PERCENTUALEXTRA1 = :PERCENTUALEXTRA1, VALOREXTRA1 = :VALOREXTRA1,
                                VALORBRUTOORIG = :VALORBRUTOORIG, VALORLIQUIDOORIG = :VALORLIQUIDOORIG, VALOROUTROSORIG = :VALOROUTROSORIG, IDNAT = :IDNAT
                            WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";

                        this.DBS.SkipControlColumns = true;
                        DBS.QueryExec(sSql, movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros,
                            movimento.PercentualFrete, movimento.ValorFrete,
                            movimento.PercentualSeguro, movimento.ValorSeguro,
                            movimento.PercentualDesc, movimento.ValorDesc,
                            movimento.PercentualDesp, movimento.ValorDesp,
                            movimento.PercentualExtra1, movimento.ValorExtra1,
                            movimento.ValorBruto, movimento.ValorLiquido, movimento.ValorOutros, movimento.IdNat,
                            movimento.CodColigada, movimento.IdMov);
                        this.DBS.SkipControlColumns = false;

                        #endregion

                        sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                        this.DBS.SkipControlColumns = true;
                        DBS.QueryExec(sSql, movimento.GuidId);
                        this.DBS.SkipControlColumns = false;



                        DBS.Commit();

                        msg.Retorno = movimento.IdMov;
                        msg.Mensagem = "Movimento alterado com sucesso";

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                DBS.Rollback();

                string sSql = @"DELETE FROM ZTITMMOVORCTEMP WHERE ROWID = :ROWID";
                this.DBS.SkipControlColumns = true;
                DBS.QueryExec(sSql, movimento.GuidId);
                this.DBS.SkipControlColumns = false;

                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msg.Retorno = 0;
                msg.Mensagem = err;
            }
            return msg;
        }

        public Message MovimentoFaturar(string usuario, string senha, MovMovimentoFatPar movimento)
        {
            Message msg = new Message();
            string sSql = string.Empty;
            string sPoint = string.Empty;

            try
            {
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                msg = this.Autenticar(usuario, senha);
                if (!(bool)msg.Retorno)
                {
                    msg.Retorno = 0;
                }
                else
                {
                    this.InitServer();
                    #region Rotina Automática
                    /*
                    ObjectList<MovMovCopiaFatPar> lMovMovCopiaFatPar = new ObjectList<MovMovCopiaFatPar>();

                    MovMovCopiaFatPar oMovMovCopiaFatPar = new MovMovCopiaFatPar();

                    #region Prepara Movimento

                    for (int i = 0;i < movimento.IdMov.Count; i++)
                    {
                        oMovMovCopiaFatPar.CodColigada = movimento.CodColigada;
                        oMovMovCopiaFatPar.CodTmvOrigem = movimento.CodTmvOrigem;
                        oMovMovCopiaFatPar.CodUsuario = movimento.CodUsuario;
                        oMovMovCopiaFatPar.dataEmissao = movimento.dataEmissao;
                        oMovMovCopiaFatPar.realizaBaixaPedido = movimento.realizaBaixaPedido;
                        oMovMovCopiaFatPar.TipoFaturamento = movimento.TipoFaturamento;
                        oMovMovCopiaFatPar.CodSistema = movimento.CodSistema;
                        oMovMovCopiaFatPar.CodTmvDestino = movimento.CodTmvDestino;
                        oMovMovCopiaFatPar.IdExercicioFiscal = movimento.IdExercicioFiscal;
                        oMovMovCopiaFatPar.numeroMov = movimento.numeroMov;

                        oMovMovCopiaFatPar.IdMov.Add(movimento.IdMov[i]);

                        foreach (MovMovimentoItemFatPar oMovMovimentoItemFatPar in movimento.listaMovItemFatAutomatico)
                        {
                            MovItemFatAutomatico oMovItemFatAutomatico = new MovItemFatAutomatico()
                            {
                                Checked = oMovMovimentoItemFatPar.Checked,
                                CodColigada = oMovMovimentoItemFatPar.CodColigada,
                                IdMov = oMovMovimentoItemFatPar.IdMov,
                                NSeqItmMov = oMovMovimentoItemFatPar.NSeqItmMov,
                                Quantidade = oMovMovimentoItemFatPar.Quantidade
                            };
                            oMovMovCopiaFatPar.listaMovItemFatAutomatico.Add(oMovItemFatAutomatico);
                        }
                    }

                    lMovMovCopiaFatPar.Add(oMovMovCopiaFatPar);

                    RM.Mov.Faturamento.IMovFaturamentoMod oIMovFaturamentoMod = this.CreateModule<RM.Mov.Faturamento.IMovFaturamentoMod>("MovFaturamentoMod");
                    ObjectList<MovMovInclusaoFatPar> lMovMovInclusaoFatPar = oIMovFaturamentoMod.Faturamento_Prepare(lMovMovCopiaFatPar);

                    #endregion

                    #region Complemento / Item

                    foreach (MovMovInclusaoFatPar oMovMovInclusaoFatPar in lMovMovInclusaoFatPar)
                    {
                        //oMovMovInclusaoFatPar.IdNat = 1;
                        DateTime data = DBS.ServerDate();
                        oMovMovInclusaoFatPar.DataEmissao = movimento.dataEmissao;
                        //oMovMovInclusaoFatPar.Observacao = string.Empty;
                        //oMovMovInclusaoFatPar.NumeroMov = movimento.numeroMov;
                        //oMovMovInclusaoFatPar.DataEntrega = data;
                        //oMovMovInclusaoFatPar.CodCCusto = "";

                        //MovMovRatCCuPar oMovMovRatCCuPar = new MovMovRatCCuPar();
                        //oMovMovRatCCuPar.CodCCusto = "";
                        //oMovMovRatCCuPar.Percentual = 100;

                        //oMovMovInclusaoFatPar.RateioCCu.Add(oMovMovRatCCuPar);

                        foreach (MovMovItemMovPar oMovMovItemMovPar in oMovMovInclusaoFatPar.ItemMovimento)
                        {
                            //Para cada item do novo movimento, marcar a atributo Flag = 1 para atribuir esse item ao novo movimento
                            //Setar as quantidades caso o movimento seja faturado parcialmente
                            oMovMovItemMovPar.Flag = 1;
                        }
                    }

                    #endregion

                    #region Fatura Movimento

                    List<MovMovInclusaoPar> listResult = new List<MovMovInclusaoPar>();
                    listResult = oIMovFaturamentoMod.FaturamentoEtapa2_Save(lMovMovInclusaoFatPar);

                    msg.Retorno = listResult[0].IdMov;
                    msg.Mensagem = "Faturamento realizado com sucesso";

                    #endregion
                    */
                    #endregion

                    #region Rotina Manual

                    sSql = @"SELECT IDMOV FROM ZTMOVFAT WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND STATUS IN ('A','R')";
                    if (this.DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov[0]))
                    {
                        throw new Exception("Movimento em faturamento por outro processo.");
                    }

                    sSql = @"SELECT IDMOV FROM TMOV WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND STATUS = 'C'";
                    if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov[0]))
                    {
                        throw new Exception("Movimento cancelado não pode ser faturado.");
                    }

                    sSql = @"SELECT IDMOV FROM TMOV WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND STATUS = 'F'";
                    if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov[0]))
                    {
                        throw new Exception("Movimento já faturado.");
                    }
                    else
                    {
                        sSql = @"SELECT IDMOV FROM ZTMOVFAT WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                        if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov[0]))
                        {
                            sSql = @"UPDATE ZTMOVFAT SET STATUS = 'A', DATAINCLUSAO = :DATAINCLUSAO, DATAEXECUCAO = NULL, MENSAGEM = NULL, CODUSUARIO = :CODUSUARIO, CODTMVDES = :CODTMVDES
                                WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            DBS.QueryExec(sSql, DBS.ServerDate(), movimento.CodUsuario, movimento.CodTmvDestino, movimento.CodColigada, movimento.IdMov[0]);
                        }
                        else
                        {
                            sSql = @"INSERT INTO ZTMOVFAT (CODCOLIGADA, IDMOV, CODTMVORI, CODTMVDES, IDMOVDES, STATUS, DATAINCLUSAO, DATAEXECUCAO, MENSAGEM, CODUSUARIO)
                                VALUES (:CODCOLIGADA, :IDMOV, :CODTMVORI, :CODTMVDES, :IDMOVDES, :STATUS, :DATAINCLUSAO, :DATAEXECUCAO, :MENSAGEM, :CODUSUARIO)";
                            DBS.QueryExec(sSql, movimento.CodColigada, movimento.IdMov[0], movimento.CodTmvOrigem, movimento.CodTmvDestino,
                                null, "A", DBS.ServerDate(), null, null, movimento.CodUsuario);
                        }
                        #region teste
                        /*
                        bool Faturado = false;
                        while (!Faturado)
                        {
                            sSql = "SELECT STATUS FROM ZTMOVFAT WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            string sStatus = DBS.QueryValue(string.Empty, sSql, movimento.CodColigada, movimento.IdMov[0]).ToString();
                            if (sStatus != "A")
                            {
                                if (sStatus == "R")
                                {
                                    //System.Threading.Thread.Sleep(5000); //5s
                                }

                                if (sStatus == "F")
                                {
                                    Faturado = true;

                                    sSql = "SELECT IDMOVDES FROM ZTMOVFAT WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                                    int IdMovdestino = Convert.ToInt32(DBS.QueryValue(0, sSql, movimento.CodColigada, movimento.IdMov[0]));

                                    msg.Retorno = IdMovdestino;
                                    msg.Mensagem = "Faturamento realizado com sucesso";
                                }

                                if (sStatus == "E")
                                {
                                    Faturado = true;

                                    sSql = "SELECT MENSAGEM FROM ZTMOVFAT WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                                    string sMensagem = DBS.QueryValue(string.Empty, sSql, movimento.CodColigada, movimento.IdMov[0]).ToString();

                                    throw new Exception(sMensagem);
                                }
                            }
                            else
                            {
                                //System.Threading.Thread.Sleep(5000); //5s
                            }
                        }
                        */
                        #endregion teste

                        sSql = @"update TITMMOVFISCAL 
                                            set RECMODIFIEDON = getDate(),
                                            RECMODIFIEDBY = :USUARIO
                                     where CODCOLIGADA = :CODCOLIGADA and IDMOV = :IDMOV";

                        DBS.QueryExec(sSql, /*USUARIO*/ movimento.CodUsuario, /*CODCOLIGADA*/ movimento.CodColigada, /*IDMOV*/ movimento.IdMov[0]);

                        //sSql = String.Format(@"update TITMMOVFISCAL 
                        //                    set NUMPEDIDO = '{0}'
                        //             where CODCOLIGADA = {1} and IDMOV = {2}", movimento.numeroMov, movimento.CodColigada, movimento.IdMov[0]);

                        //MetodosSQL.ExecQuery(sSql);

                        msg.Retorno = movimento.IdMov[0];
                        msg.Mensagem = "Faturamento em execução";
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msg.Retorno = 0;
                msg.Mensagem = err;
            }

            return msg;
        }

        public Message CancelaMovimento(string usuario, string senha, MovCancelamentoPar movimento)
        {
            Message msg = new Message();
            string sSql = string.Empty;

            try
            {
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                msg = this.Autenticar(usuario, senha);
                if (!(bool)msg.Retorno)
                {
                    msg.Retorno = 0;
                }
                else
                {
                    this.InitServer();

                    #region Rotina Automática
                    /*
                    RM.Mov.Movimento.MovMovCancelamentoPar oMovMovCancelamentoPar = new MovMovCancelamentoPar();
                    oMovMovCancelamentoPar.CodColigada = movimento.CodColigada;
                    oMovMovCancelamentoPar.IdMov = movimento.IdMov;
                    oMovMovCancelamentoPar.CodUsuarioLogado = movimento.CodUsuarioLogado;
                    oMovMovCancelamentoPar.CodSistemaLogado = "T";
                    oMovMovCancelamentoPar.DataCancelamento = DateTime.Today;//DBS.ServerNow();
                    oMovMovCancelamentoPar.MotivoCancelamento = movimento.MotivoCancelamento;

                    RM.Mov.Movimento.IMovMovMod oIMovMovMod = CreateModule<RM.Mov.Movimento.IMovMovMod>("MovMovMod");
                    RM.Lib.ObjectList<MovMovCancelamentoPar> lMovMovCancelamentoPar = new RM.Lib.ObjectList<MovMovCancelamentoPar>();
                    lMovMovCancelamentoPar.Add(oMovMovCancelamentoPar);
                    oIMovMovMod.CancelaMovimento_Prepare(lMovMovCancelamentoPar);
                    oIMovMovMod.CancelaMovimento_Save();

                    msg.Retorno = lMovMovCancelamentoPar[0].IdMov;
                    msg.Mensagem = "Movimento cancelado com sucesso";
                    */
                    #endregion

                    #region Rotina Manual

                    sSql = @"SELECT IDMOV FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND STATUS IN ('A','R') AND OPERACAO = 'C'";
                    if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov))
                    {
                        throw new Exception("Movimento em cancelamento por outro processo.");
                    }

                    sSql = @"SELECT IDMOV FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND STATUS IN ('A','R') AND OPERACAO = 'E'";
                    if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov))
                    {
                        throw new Exception("Movimento em exclusão por outro processo.");
                    }

                    sSql = @"SELECT IDMOV FROM TMOV WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND STATUS = 'C'";
                    if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov))
                    {
                        throw new Exception("Movimento já cancelado.");
                    }
                    else
                    {
                        sSql = @"SELECT IDMOV FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                        if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov))
                        {
                            sSql = @"UPDATE ZTMOVCANEXC SET STATUS = 'A', OPERACAO = 'C', DATAINCLUSAO = :DATAINCLUSAO, DATAEXECUCAO = NULL, MENSAGEM = NULL,
                                DATACANCELAMENTO = :DATACANCELAMENTO, MOTIVOCANCELAMENTO = :MOTIVOCANCELAMENTO, CODUSUARIO = :CODUSUARIO
                                WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            DBS.QueryExec(sSql, DBS.ServerDate(), movimento.DataCancelamento, movimento.MotivoCancelamento, movimento.CodUsuarioLogado, movimento.CodColigada, movimento.IdMov);
                        }
                        else
                        {
                            sSql = @"INSERT INTO ZTMOVCANEXC (CODCOLIGADA, IDMOV, CODTMV, OPERACAO, STATUS, DATAINCLUSAO, DATAEXECUCAO, MENSAGEM, 
                                    DATACANCELAMENTO, MOTIVOCANCELAMENTO, CODUSUARIO)
                                VALUES (:CODCOLIGADA, :IDMOV, :CODTMV, :OPERACAO, :STATUS, :DATAINCLUSAO, :DATAEXECUCAO, :MENSAGEM, 
                                    :DATACANCELAMENTO, :MOTIVOCANCELAMENTO, :CODUSUARIO)";
                            DBS.QueryExec(sSql, movimento.CodColigada, movimento.IdMov, null, "C", "A", DBS.ServerDate(), null, null,
                                    movimento.DataCancelamento, movimento.MotivoCancelamento, movimento.CodUsuarioLogado);
                        }

                        /*
                        bool Cancelado = false;
                        while (!Cancelado)
                        {
                            sSql = "SELECT STATUS FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            string sStatus = DBS.QueryValue(string.Empty, sSql, movimento.CodColigada, movimento.IdMov).ToString();
                            if (sStatus != "A")
                            {
                                if (sStatus == "R")
                                {
                                    System.Threading.Thread.Sleep(5000); //5s                                    
                                }

                                if (sStatus == "S")
                                {
                                    Cancelado = true;

                                    msg.Retorno = movimento.IdMov;
                                    msg.Mensagem = "Cancelamento realizado com sucesso";
                                }

                                if (sStatus == "E")
                                {
                                    Cancelado = true;

                                    sSql = "SELECT MENSAGEM FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND OPERACAO = 'C'";
                                    string sMensagem = DBS.QueryValue(string.Empty, sSql, movimento.CodColigada, movimento.IdMov).ToString();

                                    throw new Exception(sMensagem);
                                }
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(5000); //5s
                            }
                        }
                        */
                    }

                    msg.Retorno = movimento.IdMov;
                    msg.Mensagem = "Cancelamento em execução";

                    #endregion
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msg.Retorno = 0;
                msg.Mensagem = err;
            }

            return msg;
        }

        public Message ExcluiMovimento(string usuario, string senha, MovExclusaoPar movimento)
        {
            Message msg = new Message();
            string sSql = string.Empty;

            try
            {
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                msg = this.Autenticar(usuario, senha);
                if (!(bool)msg.Retorno)
                {
                    msg.Retorno = 0;
                }
                else
                {
                    this.InitServer();
                    #region Rotina Automativa
                    /*
                    RM.Mov.Movimento.MovMovExclusaoPar oMovMovExclusaoPar = new MovMovExclusaoPar();
                    oMovMovExclusaoPar.CodColigada = movimento.CodColigada;
                    oMovMovExclusaoPar.IdMov = movimento.IdMov;
                    oMovMovExclusaoPar.CodUsuarioLogado = movimento.CodUsuarioLogado;
                    oMovMovExclusaoPar.CodSistemaLogado = "T";

                    RM.Mov.Movimento.IMovMovMod oIMovMovMod = CreateModule<RM.Mov.Movimento.IMovMovMod>("MovMovMod");
                    RM.Lib.ObjectList<MovMovExclusaoPar> lMovMovExclusaoPar = new RM.Lib.ObjectList<MovMovExclusaoPar>();
                    lMovMovExclusaoPar.Add(oMovMovExclusaoPar);
                    oIMovMovMod.Exclusao_Prepare(lMovMovExclusaoPar);
                    oIMovMovMod.Exclusao_Save();
                    
                    msg.Retorno = lMovMovExclusaoPar[0].IdMov;
                    msg.Mensagem = "Movimento excluido com sucesso";
                    */
                    #endregion

                    #region Rotina Manual

                    sSql = @"SELECT IDMOV FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND STATUS IN ('A','R') AND OPERACAO = 'C'";
                    if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov))
                    {
                        throw new Exception("Movimento em cancelamento por outro processo.");
                    }

                    sSql = @"SELECT IDMOV FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND STATUS IN ('A','R') AND OPERACAO = 'E'";
                    if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov))
                    {
                        throw new Exception("Movimento em exclusão por outro processo.");
                    }

                    sSql = @"SELECT IDMOV FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND STATUS IN ('S') AND OPERACAO = 'E'";
                    if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov))
                    {
                        throw new Exception("Movimento excluido por outro processo.");
                    }
                    else
                    {
                        sSql = @"SELECT IDMOV FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                        if (DBS.QueryFind(sSql, movimento.CodColigada, movimento.IdMov))
                        {
                            sSql = @"UPDATE ZTMOVCANEXC SET STATUS = 'A', OPERACAO = 'E', DATAINCLUSAO = :DATAINCLUSAO, DATAEXECUCAO = NULL, MENSAGEM = NULL,
                                DATACANCELAMENTO = :DATACANCELAMENTO, MOTIVOCANCELAMENTO = :MOTIVOCANCELAMENTO, CODUSUARIO = :CODUSUARIO
                                WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            DBS.QueryExec(sSql, DBS.ServerDate(), null, null, movimento.CodUsuarioLogado, movimento.CodColigada, movimento.IdMov);
                        }
                        else
                        {
                            sSql = @"INSERT INTO ZTMOVCANEXC (CODCOLIGADA, IDMOV, CODTMV, OPERACAO, STATUS, DATAINCLUSAO, DATAEXECUCAO, MENSAGEM, 
                                    DATACANCELAMENTO, MOTIVOCANCELAMENTO, CODUSUARIO)
                                VALUES (:CODCOLIGADA, :IDMOV, :CODTMV, :OPERACAO, :STATUS, :DATAINCLUSAO, :DATAEXECUCAO, :MENSAGEM, 
                                    :DATACANCELAMENTO, :MOTIVOCANCELAMENTO, :CODUSUARIO)";
                            DBS.QueryExec(sSql, movimento.CodColigada, movimento.IdMov, null, "E", "A", DBS.ServerDate(), null, null, null, null, movimento.CodUsuarioLogado);
                        }

                        /*
                        bool Cancelado = false;
                        while (!Cancelado)
                        {
                            sSql = "SELECT STATUS FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                            string sStatus = DBS.QueryValue(string.Empty, sSql, movimento.CodColigada, movimento.IdMov).ToString();
                            if (sStatus != "A")
                            {
                                if (sStatus == "R")
                                {
                                    System.Threading.Thread.Sleep(5000); //5s                                    
                                }

                                if (sStatus == "S")
                                {
                                    Cancelado = true;

                                    msg.Retorno = movimento.IdMov;
                                    msg.Mensagem = "Exclusão realizada com sucesso";
                                }

                                if (sStatus == "E")
                                {
                                    Cancelado = true;

                                    sSql = "SELECT MENSAGEM FROM ZTMOVCANEXC WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV AND OPERACAO = 'E'";
                                    string sMensagem = DBS.QueryValue(string.Empty, sSql, movimento.CodColigada, movimento.IdMov).ToString();

                                    throw new Exception(sMensagem);
                                }
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(5000); //5s
                            }
                        }
                        */
                    }


                    msg.Retorno = movimento.IdMov;
                    msg.Mensagem = "Exclusão em execução";

                    #endregion
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msg.Retorno = 0;
                msg.Mensagem = err;
            }

            return msg;
        }

        public Message ClienteFornecedorSave(string usuario, string senha, FinCliForPar clifor)
        {
            Message msg = new Message();
            string sSql = string.Empty;
            string sPoint = string.Empty;
            AppLib.Data.Connection conn = new AppLib.Data.Connection();

            try
            {
                AppLib.Util.Alias alias = new Util().GetAlias(EnviromentHelper.IndexAlias);
                msg = this.Autenticar(usuario, senha);
                if (!(bool)msg.Retorno)
                {
                    msg.Retorno = 0;
                }
                else
                {
                    #region Rotina Automatica

                    /*
                    this.InitServer();

                    RMSAutoInc oRMSAutoInc = new RMSAutoInc(this.DBS);
                    RM.Fin.CliFor.IFinCFOMod oIFinCFOMod = this.CreateModule<RM.Fin.CliFor.IFinCFOMod>("FinCFOMod");
                    List<RM.Fin.CliFor.FinCFOInclusaoPar> lFinCFOInclusaoPar = new List<RM.Fin.CliFor.FinCFOInclusaoPar>();
                    List<RM.Fin.CliFor.FinCFOAlteracaoPar> lFinCFOAlteracaoPar = new List<RM.Fin.CliFor.FinCFOAlteracaoPar>();

                    if (string.IsNullOrEmpty(clifor.CodCfo))
                    {
                        #region Inclusão

                        bool Flag = false;

                        if (!string.IsNullOrEmpty(clifor.CGCCFO))
                        {
                            if(!ValidaCNPJCPF(clifor.CGCCFO))
                                throw new Exception("CNPJ/CPF informado não é válido.");

                            if (clifor.CGCCFO.Equals("000.000.000-00")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("111.111.111-11")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("222.222.222-22")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("333.333.333-33")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("444.444.444-44")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("555.555.555-55")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("666.666.666-66")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("777.777.777-77")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("888.888.888-88")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("999.999.999-99")) { Flag = true; }

                            if (clifor.CGCCFO.Equals("00.000.000/0000-00")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("11.111.111/1111-11")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("22.222.222/2222-22")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("33.333.333/3333-33")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("44.444.444/4444-44")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("55.555.555/5555-55")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("66.666.666/6666-66")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("77.777.777/7777-77")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("88.888.888/8888-88")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("99.999.999/9999-99")) { Flag = true; }

                            if (!Flag)
                            {
                                if (clifor.PessoaFisOuJur == "J")
                                {
                                    sSql = @"SELECT CODCFO FROM FCFO WHERE CODCOLIGADA = :CODCOLIGADA AND CGCCFO = :CGCCFO";
                                    if (DBS.QueryFind(sSql, clifor.CodColigada, clifor.CGCCFO))
                                    {
                                        if (clifor.CodCfo != DBS.QueryValue(0, sSql, clifor.CodColigada, clifor.CGCCFO).ToString())
                                            throw new Exception("CNPJ informado já esta cadastrado.");
                                    }
                                }
                            }
                            else
                            {
                                clifor.CGCCFO = null;
                            }
                        }

                        RM.Fin.CliFor.FinCFOInclusaoPar oFinCFOInclusaoPar = new RM.Fin.CliFor.FinCFOInclusaoPar();

                        oFinCFOInclusaoPar.CodColigada = clifor.CodColigada;
                        oFinCFOInclusaoPar.CodCfo = oRMSAutoInc.GetNewValue("F", (int)clifor.CodColigada, "CODCFO").ToString().PadLeft(7, '0');
                        oFinCFOInclusaoPar.IdCfo = oRMSAutoInc.GetNewValue("F", (int)clifor.CodColigada, "IDCFO");

                        oFinCFOInclusaoPar.NomeFantasia = clifor.NomeFantasia;
                        oFinCFOInclusaoPar.Nome = clifor.Nome;
                        oFinCFOInclusaoPar.CGCCFO = clifor.CGCCFO;
                        oFinCFOInclusaoPar.InscrEstadual = clifor.InscrEstadual;
                        if (clifor.PagRec.Equals(1))
                        {
                            oFinCFOInclusaoPar.PagRec = RM.Fin.Lib.FinCFOCliForEnum.Cliente;
                        }
                        else if (clifor.PagRec.Equals(2))
                        {
                            oFinCFOInclusaoPar.PagRec = RM.Fin.Lib.FinCFOCliForEnum.Fornecedor;
                        }
                        else
                        {
                            oFinCFOInclusaoPar.PagRec = RM.Fin.Lib.FinCFOCliForEnum.Ambos;
                        }

                        oFinCFOInclusaoPar.Rua = clifor.Rua;
                        oFinCFOInclusaoPar.Numero = clifor.Numero;
                        oFinCFOInclusaoPar.Complemento = clifor.Complemento;
                        oFinCFOInclusaoPar.Bairro = clifor.Bairro;
                        oFinCFOInclusaoPar.Cidade = clifor.Cidade;
                        oFinCFOInclusaoPar.CODETD = clifor.CODETD;
                        oFinCFOInclusaoPar.CEP = clifor.CEP;

                        oFinCFOInclusaoPar.Telefone = clifor.Telefone;
                        oFinCFOInclusaoPar.FAX = clifor.FAX;
                        oFinCFOInclusaoPar.Telex = clifor.Telex;
                        oFinCFOInclusaoPar.Email = clifor.Email;
                        oFinCFOInclusaoPar.Contato = clifor.Contato;

                        oFinCFOInclusaoPar.RuaPgto = clifor.RuaPgto;
                        oFinCFOInclusaoPar.NumeroPgto = clifor.NumeroPgto;
                        oFinCFOInclusaoPar.ComplementoPgto = clifor.ComplementoPgto;
                        oFinCFOInclusaoPar.BairroPgto = clifor.BairroPgto;
                        oFinCFOInclusaoPar.CidadePgto = clifor.CidadePgto;
                        oFinCFOInclusaoPar.CODETDPgto = clifor.CODETDPgto;
                        oFinCFOInclusaoPar.CEPPgto = clifor.CEPPgto;

                        oFinCFOInclusaoPar.TelefonePgto = clifor.TelefonePgto;
                        oFinCFOInclusaoPar.FAXPgto = clifor.FAXPgto;
                        oFinCFOInclusaoPar.EmailPgto = clifor.EmailPgto;
                        oFinCFOInclusaoPar.ContatoPgto = clifor.ContatoPgto;

                        oFinCFOInclusaoPar.RuaEntrega = clifor.RuaEntrega;
                        oFinCFOInclusaoPar.NumeroEntrega = clifor.NumeroEntrega;
                        oFinCFOInclusaoPar.ComplemEntrega = clifor.ComplemEntrega;
                        oFinCFOInclusaoPar.BairroEntrega = clifor.BairroEntrega;
                        oFinCFOInclusaoPar.CidadeEntrega = clifor.CidadeEntrega;
                        oFinCFOInclusaoPar.CODETDEntrega = clifor.CODETDEntrega;
                        oFinCFOInclusaoPar.CEPEntrega = clifor.CEPEntrega;

                        oFinCFOInclusaoPar.TelefoneEntrega = clifor.TelefoneEntrega;
                        oFinCFOInclusaoPar.FAXEntrega = clifor.FAXEntrega;
                        oFinCFOInclusaoPar.EmailEntrega = clifor.EmailEntrega;
                        oFinCFOInclusaoPar.ContatoEntrega = clifor.ContatoEntrega;

                        oFinCFOInclusaoPar.CODTCF = clifor.CODTCF;
                        oFinCFOInclusaoPar.CodCOLTCF = clifor.CodCOLTCF;

                        oFinCFOInclusaoPar.CodMunicipio = clifor.CodMunicipio;
                        oFinCFOInclusaoPar.CodMunicipioEntrega = clifor.CodMunicipioEntrega;
                        oFinCFOInclusaoPar.CodMunicipioPgto = clifor.CodMunicipioPgto;

                        oFinCFOInclusaoPar.PessoaFisOuJur = (clifor.PessoaFisOuJur == "F") ? RM.Fin.Lib.FinCFOPessoaFisJurEnum.PessoaFisica : RM.Fin.Lib.FinCFOPessoaFisJurEnum.PessoaJuridica;

                        oFinCFOInclusaoPar.IDPais = clifor.IDPais;
                        oFinCFOInclusaoPar.IDPaisPgto = clifor.IDPaisPgto;
                        oFinCFOInclusaoPar.IDPaisEntrega = clifor.IDPaisEntrega;

                        oFinCFOInclusaoPar.TipoRua = clifor.TipoRua;
                        oFinCFOInclusaoPar.TipoBairro = clifor.TipoBairro;
                        oFinCFOInclusaoPar.TipoRuaEntrega = clifor.TipoRuaEntrega;
                        oFinCFOInclusaoPar.TipoBairroEntrega = clifor.TipoBairroEntrega;
                        oFinCFOInclusaoPar.TipoRuaPgto = clifor.TipoRuaPgto;
                        oFinCFOInclusaoPar.TipoBairroPgto = clifor.TipoBairroPgto;

                        if (clifor.RamoAtiv == null || clifor.RamoAtiv == 0)
                            oFinCFOInclusaoPar.RamoAtiv = RM.Fin.Lib.FinCFORamoAtivEnum.Nenhum;

                        oFinCFOInclusaoPar.Suframa = clifor.Suframa;
                        oFinCFOInclusaoPar.LimiteCredito = clifor.LimiteCredito;
                        oFinCFOInclusaoPar.Ativo = clifor.Ativo;

                        oFinCFOInclusaoPar.UsuarioCriacao = clifor.UsuarioCriacao;
                        oFinCFOInclusaoPar.DataCriacao = DBS.ServerDate();
                        oFinCFOInclusaoPar.ColigadaCorrente = (int)RM.Lib.RMSContextManager.Principal.CodColigada;

                        Dictionary<string, object> CamposComplementares = new Dictionary<string, object>();
                        CamposComplementares.Add("HISTORICOATUAL", clifor.HISTORICOATUAL);
                        oFinCFOInclusaoPar.CamposComplementares = CamposComplementares;

                        lFinCFOInclusaoPar.Clear();
                        lFinCFOInclusaoPar.Add(oFinCFOInclusaoPar);

                        oIFinCFOMod.Inclusao_Prepare(lFinCFOInclusaoPar);
                        oIFinCFOMod.Inclusao_Save();

                        //Atualiza o Ramo de Atividade
                        sSql = @"UPDATE FCFO SET RAMOATIV = :RAMOATIV WHERE CODCOLIGADA = :CODCOLIGADA AND CGCCFO = :CGCCFO";
                        this.DBS.SkipControlColumns = true;
                        DBS.QueryExec(sSql, clifor.RamoAtiv, clifor.CodColigada, clifor.CGCCFO);
                        this.DBS.SkipControlColumns = false;

                        //Cria tabela FCFODEF
                        sSql = @"SELECT CODCFO FROM FCFODEF WHERE CODCOLIGADA = :CODCOLIGADA AND CODCOLCFO = :CODCOLCFO AND CODCFO = :CODCFO";
                        if (DBS.QueryFind(sSql, 1, oFinCFOInclusaoPar.CodColigada, oFinCFOInclusaoPar.CodCfo))
                        {
                            sSql = @"UPDATE FCFODEF SET CODRPR = :CODRPR WHERE CODCOLIGADA = :CODCOLIGADA AND CODCOLCFO = :CODCOLCFO AND CODCFO = :CODCFO";
                            this.DBS.SkipControlColumns = true;
                            DBS.QueryExec(sSql, 1, clifor.CodRpr, oFinCFOInclusaoPar.CodColigada, oFinCFOInclusaoPar.CodCfo);
                            this.DBS.SkipControlColumns = false;
                        }
                        else
                        {
                            sSql = @"INSERT INTO FCFODEF (CODCOLIGADA, CODCFO, CODCOLCFO, CODRPR) VALUES (:CODCOLIGADA, :CODCFO, :CODCOLCFO, :CODRPR)";
                            this.DBS.SkipControlColumns = true;
                            DBS.QueryExec(sSql, 1, oFinCFOInclusaoPar.CodCfo, oFinCFOInclusaoPar.CodColigada, clifor.CodRpr);
                            this.DBS.SkipControlColumns = false;
                        }

                        msg.Retorno = oFinCFOInclusaoPar.CodCfo;
                        msg.Mensagem = "Cadastro realizado com sucesso.";

                        #endregion
                    }
                    else
                    {
                        #region Alteração

                        bool Flag = false;

                        if (!string.IsNullOrEmpty(clifor.CGCCFO))
                        {
                            if (!ValidaCNPJCPF(clifor.CGCCFO))
                                throw new Exception("CNPJ/CPF informado não é válido.");

                            if (clifor.CGCCFO.Equals("000.000.000-00")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("111.111.111-11")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("222.222.222-22")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("333.333.333-33")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("444.444.444-44")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("555.555.555-55")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("666.666.666-66")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("777.777.777-77")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("888.888.888-88")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("999.999.999-99")) { Flag = true; }

                            if (clifor.CGCCFO.Equals("00.000.000/0000-00")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("11.111.111/1111-11")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("22.222.222/2222-22")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("33.333.333/3333-33")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("44.444.444/4444-44")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("55.555.555/5555-55")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("66.666.666/6666-66")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("77.777.777/7777-77")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("88.888.888/8888-88")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("99.999.999/9999-99")) { Flag = true; }

                            if (!Flag)
                            {
                                if (clifor.PessoaFisOuJur == "J")
                                {
                                    sSql = @"SELECT CODCFO FROM FCFO WHERE CODCOLIGADA = :CODCOLIGADA AND CGCCFO = :CGCCFO";
                                    if (DBS.QueryFind(sSql, clifor.CodColigada, clifor.CGCCFO))
                                    {
                                        if (clifor.CodCfo != DBS.QueryValue(0, sSql, clifor.CodColigada, clifor.CGCCFO).ToString())
                                            throw new Exception("CNPJ informado já esta cadastrado.");
                                    }
                                }
                            }
                            else
                            {
                                clifor.CGCCFO = null;
                            }
                        }


                        RM.Fin.CliFor.FinCFOAlteracaoPar oFinCFOAlteracaoPar = new RM.Fin.CliFor.FinCFOAlteracaoPar();

                        oFinCFOAlteracaoPar.CodColigada = (int)clifor.CodColigada;
                        oFinCFOAlteracaoPar.CodCfo = clifor.CodCfo;

                        oFinCFOAlteracaoPar.NomeFantasia = clifor.NomeFantasia;
                        oFinCFOAlteracaoPar.Nome = clifor.Nome;
                        oFinCFOAlteracaoPar.CGCCFO = clifor.CGCCFO;
                        oFinCFOAlteracaoPar.InscrEstadual = clifor.InscrEstadual;
                        if (clifor.PagRec.Equals(1))
                        {
                            oFinCFOAlteracaoPar.PagRec = RM.Fin.Lib.FinCFOCliForEnum.Cliente;
                        }
                        else if (clifor.PagRec.Equals(2))
                        {
                            oFinCFOAlteracaoPar.PagRec = RM.Fin.Lib.FinCFOCliForEnum.Fornecedor;
                        }
                        else
                        {
                            oFinCFOAlteracaoPar.PagRec = RM.Fin.Lib.FinCFOCliForEnum.Ambos;
                        }

                        oFinCFOAlteracaoPar.Rua = clifor.Rua;
                        oFinCFOAlteracaoPar.Numero = clifor.Numero;
                        oFinCFOAlteracaoPar.Complemento = clifor.Complemento;
                        oFinCFOAlteracaoPar.Bairro = clifor.Bairro;
                        oFinCFOAlteracaoPar.Cidade = clifor.Cidade;
                        oFinCFOAlteracaoPar.CODETD = clifor.CODETD;
                        oFinCFOAlteracaoPar.CEP = clifor.CEP;

                        oFinCFOAlteracaoPar.Telefone = clifor.Telefone;
                        oFinCFOAlteracaoPar.FAX = clifor.FAX;
                        oFinCFOAlteracaoPar.Telex = clifor.Telex;
                        oFinCFOAlteracaoPar.Email = clifor.Email;
                        oFinCFOAlteracaoPar.Contato = clifor.Contato;

                        oFinCFOAlteracaoPar.RuaPgto = clifor.RuaPgto;
                        oFinCFOAlteracaoPar.NumeroPgto = clifor.NumeroPgto;
                        oFinCFOAlteracaoPar.ComplementoPgto = clifor.ComplementoPgto;
                        oFinCFOAlteracaoPar.BairroPgto = clifor.BairroPgto;
                        oFinCFOAlteracaoPar.CidadePgto = clifor.CidadePgto;
                        oFinCFOAlteracaoPar.CODETDPgto = clifor.CODETDPgto;
                        oFinCFOAlteracaoPar.CEPPgto = clifor.CEPPgto;

                        oFinCFOAlteracaoPar.TelefonePgto = clifor.TelefonePgto;
                        oFinCFOAlteracaoPar.FAXPgto = clifor.FAXPgto;
                        oFinCFOAlteracaoPar.EmailPgto = clifor.EmailPgto;
                        oFinCFOAlteracaoPar.ContatoPgto = clifor.ContatoPgto;

                        oFinCFOAlteracaoPar.RuaEntrega = clifor.RuaEntrega;
                        oFinCFOAlteracaoPar.NumeroEntrega = clifor.NumeroEntrega;
                        oFinCFOAlteracaoPar.ComplemEntrega = clifor.ComplemEntrega;
                        oFinCFOAlteracaoPar.BairroEntrega = clifor.BairroEntrega;
                        oFinCFOAlteracaoPar.CidadeEntrega = clifor.CidadeEntrega;
                        oFinCFOAlteracaoPar.CODETDEntrega = clifor.CODETDEntrega;
                        oFinCFOAlteracaoPar.CEPEntrega = clifor.CEPEntrega;

                        oFinCFOAlteracaoPar.TelefoneEntrega = clifor.TelefoneEntrega;
                        oFinCFOAlteracaoPar.FAXEntrega = clifor.FAXEntrega;
                        oFinCFOAlteracaoPar.EmailEntrega = clifor.EmailEntrega;
                        oFinCFOAlteracaoPar.ContatoEntrega = clifor.ContatoEntrega;

                        oFinCFOAlteracaoPar.FAX = clifor.FAX;
                        oFinCFOAlteracaoPar.Telex = clifor.Telex;
                        oFinCFOAlteracaoPar.Email = clifor.Email;

                        oFinCFOAlteracaoPar.CODTCF = clifor.CODTCF;
                        oFinCFOAlteracaoPar.CodCOLTCF = clifor.CodCOLTCF;

                        oFinCFOAlteracaoPar.CodMunicipio = clifor.CodMunicipio;
                        oFinCFOAlteracaoPar.CodMunicipioEntrega = clifor.CodMunicipioEntrega;
                        oFinCFOAlteracaoPar.CodMunicipioPgto = clifor.CodMunicipioPgto;

                        oFinCFOAlteracaoPar.PessoaFisOuJur = (clifor.PessoaFisOuJur == "F") ? RM.Fin.Lib.FinCFOPessoaFisJurEnum.PessoaFisica : RM.Fin.Lib.FinCFOPessoaFisJurEnum.PessoaJuridica;

                        oFinCFOAlteracaoPar.TipoRua = clifor.TipoRua;
                        oFinCFOAlteracaoPar.TipoBairro = clifor.TipoBairro;
                        oFinCFOAlteracaoPar.TipoRuaEntrega = clifor.TipoRuaEntrega;
                        oFinCFOAlteracaoPar.TipoBairroEntrega = clifor.TipoBairroEntrega;
                        oFinCFOAlteracaoPar.TipoRuaPgto = clifor.TipoRuaPgto;
                        oFinCFOAlteracaoPar.TipoBairroPgto = clifor.TipoBairroPgto;

                        oFinCFOAlteracaoPar.Telefone = clifor.Telefone;
                        oFinCFOAlteracaoPar.Telex = clifor.Telex;
                        oFinCFOAlteracaoPar.Contato = clifor.Contato;

                        if (clifor.RamoAtiv == null || clifor.RamoAtiv == 0)
                            oFinCFOAlteracaoPar.RamoAtiv = RM.Fin.Lib.FinCFORamoAtivEnum.Nenhum;

                        oFinCFOAlteracaoPar.Suframa = clifor.Suframa;
                        oFinCFOAlteracaoPar.LimiteCredito = clifor.LimiteCredito;
                        oFinCFOAlteracaoPar.Ativo = Convert.ToInt32(clifor.Ativo);
                        oFinCFOAlteracaoPar.UsuarioAlteracao = clifor.UsuarioAlteracao;
                        oFinCFOAlteracaoPar.DataAlteracao = DBS.ServerDate();
                        oFinCFOAlteracaoPar.ColigadaCorrente = (int)RM.Lib.RMSContextManager.Principal.CodColigada;

                        Dictionary<string, object> CamposComplementares = new Dictionary<string, object>();
                        CamposComplementares.Add("HISTORICOATUAL", clifor.HISTORICOATUAL);
                        oFinCFOAlteracaoPar.CamposComplementares = CamposComplementares;

                        lFinCFOAlteracaoPar.Clear();
                        lFinCFOAlteracaoPar.Add(oFinCFOAlteracaoPar);

                        oIFinCFOMod.Alteracao_Prepare(lFinCFOAlteracaoPar);
                        oIFinCFOMod.Alteracao_Save();

                        //Atualiza o Ramo de Atividade
                        sSql = @"UPDATE FCFO SET RAMOATIV = :RAMOATIV WHERE CODCOLIGADA = :CODCOLIGADA AND CGCCFO = :CGCCFO";
                        this.DBS.SkipControlColumns = true;
                        DBS.QueryExec(sSql, clifor.RamoAtiv, clifor.CodColigada, clifor.CGCCFO);
                        this.DBS.SkipControlColumns = false;

                        //Cria tabela FCFODEF
                        sSql = @"SELECT CODCFO FROM FCFODEF WHERE CODCOLIGADA = :CODCOLIGADA AND CODCOLCFO = :CODCOLCFO AND CODCFO = :CODCFO";
                        if (DBS.QueryFind(sSql, 1, oFinCFOAlteracaoPar.CodColigada, oFinCFOAlteracaoPar.CodCfo))
                        {
                            sSql = @"UPDATE FCFODEF SET CODRPR = :CODRPR WHERE CODCOLIGADA = :CODCOLIGADA AND CODCOLCFO = :CODCOLCFO AND CODCFO = :CODCFO";
                            this.DBS.SkipControlColumns = true;
                            DBS.QueryExec(sSql, clifor.CodRpr, 1, oFinCFOAlteracaoPar.CodColigada, oFinCFOAlteracaoPar.CodCfo);
                            this.DBS.SkipControlColumns = false;
                        }
                        else
                        {
                            sSql = @"INSERT INTO FCFODEF (CODCOLIGADA, CODCFO, CODCOLCFO, CODRPR) VALUES (:CODCOLIGADA, :CODCFO, :CODCOLCFO, :CODRPR)";
                            this.DBS.SkipControlColumns = true;
                            DBS.QueryExec(sSql, 1, oFinCFOAlteracaoPar.CodCfo, oFinCFOAlteracaoPar.CodColigada, clifor.CodRpr);
                            this.DBS.SkipControlColumns = false;
                        }

                        msg.Retorno = oFinCFOAlteracaoPar.CodCfo;
                        msg.Mensagem = "Alteração realizada com sucesso.";

                        #endregion
                    }
                    */

                    #endregion

                    #region Rotina Manual

                    //this.InitServer();
                    conn = AppLib.Context.poolConnection.Get();

                    if (string.IsNullOrEmpty(clifor.CodCfo))
                    {
                        #region Inclusão

                        bool Flag = false;

                        if (!string.IsNullOrEmpty(clifor.CGCCFO))
                        {
                            if (!new Util().ValidaCNPJCPF(clifor.CGCCFO))
                                throw new Exception("CNPJ/CPF informado não é válido.");

                            if (clifor.CGCCFO.Equals("000.000.000-00")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("111.111.111-11")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("222.222.222-22")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("333.333.333-33")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("444.444.444-44")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("555.555.555-55")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("666.666.666-66")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("777.777.777-77")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("888.888.888-88")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("999.999.999-99")) { Flag = true; }

                            if (clifor.CGCCFO.Equals("00.000.000/0000-00")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("11.111.111/1111-11")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("22.222.222/2222-22")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("33.333.333/3333-33")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("44.444.444/4444-44")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("55.555.555/5555-55")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("66.666.666/6666-66")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("77.777.777/7777-77")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("88.888.888/8888-88")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("99.999.999/9999-99")) { Flag = true; }

                            if (!Flag)
                            {
                                if (clifor.PessoaFisOuJur == "J")
                                {
                                    //sSql = @"SELECT CODCFO FROM FCFO WHERE CODCOLIGADA = :CODCOLIGADA AND CGCCFO = :CGCCFO";
                                    sSql = @"SELECT COUNT(CODCFO) FROM FCFO WHERE CODCOLIGADA = ? AND CGCCFO = ?";
                                    int QtdeCliente = Convert.ToInt32(conn.ExecGetField(0, @"SELECT COUNT(CODCFO) FROM FCFO WHERE CODCOLIGADA = ? AND CGCCFO = ?", new object[] { clifor.CodColigada, clifor.CGCCFO }));
                                    if (QtdeCliente > 0)
                                    {
                                        msg.Retorno = 0;
                                        msg.Mensagem = "CNPJ informado já esta cadastrado.";
                                        return msg;
                                    }
                                    //if (DBS.QueryFind(sSql, clifor.CodColigada, clifor.CGCCFO))
                                    //{
                                    //    if (clifor.CodCfo != DBS.QueryValue(0, sSql, clifor.CodColigada, clifor.CGCCFO).ToString())
                                    //        throw new Exception("CNPJ informado já esta cadastrado.");
                                    //}
                                }
                            }
                            else
                            {
                                clifor.CGCCFO = null;
                            }
                        }

                        clifor.CodCfo = this.GetNewAutoInc((int)clifor.CodColigada, "F", "CODCFO").ToString().PadLeft(7, '0');
                        clifor.IdCfo = this.GetNewAutoInc((int)clifor.CodColigada, "F", "IDCFO");
                        int IdHistorico = this.GetNewAutoInc((int)clifor.CodColigada, "F", "IDHISTORICO");

                        //sSql = @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = :CODMUNICIPIO AND CODETDMUNICIPIO = :CODETDMUNICIPIO";
                        object retorno = conn.ExecGetField(null, @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = ? AND CODETDMUNICIPIO = ?", new object[] { clifor.CodMunicipio, clifor.CODETD });//DBS.QueryValue(null, sSql, clifor.CodMunicipio, clifor.CODETD);
                        clifor.Cidade = (retorno == null) ? null : retorno.ToString();

                        //sSql = @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = :CODMUNICIPIO AND CODETDMUNICIPIO = :CODETDMUNICIPIO";
                        retorno = conn.ExecGetField(null, @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = ? AND CODETDMUNICIPIO = ?", new object[] { clifor.CodMunicipioPgto, clifor.CODETDPgto });//DBS.QueryValue(null, sSql, clifor.CodMunicipioPgto, clifor.CODETDPgto);
                        clifor.CidadePgto = (retorno == null) ? null : retorno.ToString();

                        //sSql = @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = :CODMUNICIPIO AND CODETDMUNICIPIO = :CODETDMUNICIPIO";
                        retorno = conn.ExecGetField(null, @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = ? AND CODETDMUNICIPIO = ?", new object[] { clifor.CodMunicipioEntrega, clifor.CODETDEntrega });//DBS.QueryValue(null, sSql, clifor.CodMunicipioEntrega, clifor.CODETDEntrega);
                        clifor.CidadeEntrega = (retorno == null) ? null : retorno.ToString();

                        conn.BeginTransaction();
                        //DBS.BeginTransaction();
                        //                        sSql = @"INSERT INTO FCFO (CODCOLIGADA, CODCFO, NOMEFANTASIA, NOME, CGCCFO, INSCRESTADUAL, PAGREC, RUA, NUMERO, COMPLEMENTO, BAIRRO, CIDADE,
                        //                                    CODETD, CEP, TELEFONE, RUAPGTO, NUMEROPGTO, COMPLEMENTOPGTO, BAIRROPGTO, CIDADEPGTO, CODETDPGTO, CEPPGTO, TELEFONEPGTO,
                        //                                    RUAENTREGA, NUMEROENTREGA, COMPLEMENTREGA, BAIRROENTREGA, CIDADEENTREGA, CODETDENTREGA, CEPENTREGA, TELEFONEENTREGA, FAX,
                        //                                    TELEX, EMAIL, CONTATO, CODTCF, ATIVO, LIMITECREDITO, VALORULTIMOLAN, TIPOINSCRCNAB, SIMBMOEDAINDEX, DATAULTALTERACAO,
                        //                                    DATACRIACAO, DATAULTMOVIMENTO, CONTEVENTOCONTAB, CAMPOLIVRE, CAMPOALFAOP1, CAMPOALFAOP2, CAMPOALFAOP3, VALOROP1, VALOROP2,
                        //                                    VALOROP3, DATAOP1, DATAOP2, DATAOP3, CODTRA, CHAPA, STATUSCOTACAO, DTINICATIVIDADES, PATRIMONIO, NUMFUNCIONARIOS, CODCOLCHAVESESTRANG,
                        //                                    CODCOLTCF, FAXDEDICADO, CODMUNICIPIO, CODCOLCONTAGER, CODCONTAGER, FORMAPAGAMENTO, IDENTPORCNPJ, INSCRMUNICIPAL, PESSOAFISOUJUR,
                        //                                    CONTATOPGTO, CONTATOENTREGA, PAIS, PAISPAGTO, PAISENTREGA, ULTIMODOCUMENTO, CONTRIBUINTE, CFOIMOB, TIPODOC, CODFINALIDADE, AGRUPCOB,
                        //                                    CODCARGO, CODVINCULO, ENDCOBC, CIDENTIDADE, CI_ORGAO, CI_UF, CODPROF, CODPAGTOGPS, FAXENTREGA, EMAILENTREGA, FAXPGTO, EMAILPGTO,
                        //                                    SATISFACAO, VALFRETE, TPTOMADOR, CONTRIBUINTEISS, NUMDEPENDENTES, EMPRESA, ESTADOCIVIL, CODCOLCXA, CODCXA, PRODUTORRURAL, USUARIOALTERACAO,
                        //                                    SUFRAMA, CODMUNICIPIOPGTO, CODMUNICIPIOENTREGA, ORGAOPUBLICO, TELEFONECOMERCIAL, CAIXAPOSTAL, CAIXAPOSTALENTREGA, CAIXAPOSTALPAGAMENTO,
                        //                                    CATEGORIAAUTONOMO, CBOAUTONOMO, CIAUTONOMO, IDCFO, CODIGOINSS, VROUTRASDEDUCOESIRRF, CODRECEITA, CEI, OPTANTEPELOSIMPLES, TIPORUA,
                        //                                    TIPOBAIRRO, REGIMEISS, RETENCAOISS, DTNASCIMENTO, USUARIOCRIACAO, TIPOOPCOMBUSTIVEL, INSCRESTADUALST, LOCALIDADE, LOCALIDADEPGTO,
                        //                                    LOCALIDADEENTREGA, TIPORUAPGTO, TIPORUAENTREGA, TIPOBAIRROPGTO, TIPOBAIRROENTREGA, PORTE, RAMOATIV, NIT, CEPCAIXAPOSTAL, NUMDIASATRASO,
                        //                                    IDPAIS, IDPAISPGTO, IDPAISENTREGA, TIPOCONTRIBUINTEINSS, NACIONALIDADE, CODCOLCFOFISCAL, IDCFOFISCAL, EMAILFISCAL, CALCULAAVP,
                        //                                    CODUSUARIOACESSO, IDINTEGRACAO, USARCUMULATRETENCAOPAGAR, NIF, SITUACAONIF, TIPORENDIMENTO, FORMATRIBUTACAO, INDNATRET,
                        //                                    DOCUMENTOESTRANGEIRO,
                        //                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON)
                        //                                VALUES (:CODCOLIGADA, :CODCFO, :NOMEFANTASIA, :NOME, :CGCCFO, :INSCRESTADUAL, :PAGREC, :RUA, :NUMERO, :COMPLEMENTO, :BAIRRO, :CIDADE,
                        //                                    :CODETD, :CEP, :TELEFONE, :RUAPGTO, :NUMEROPGTO, :COMPLEMENTOPGTO, :BAIRROPGTO, :CIDADEPGTO, :CODETDPGTO, :CEPPGTO, :TELEFONEPGTO,
                        //                                    :RUAENTREGA, :NUMEROENTREGA, :COMPLEMENTREGA, :BAIRROENTREGA, :CIDADEENTREGA, :CODETDENTREGA, :CEPENTREGA, :TELEFONEENTREGA, :FAX,
                        //                                    :TELEX, :EMAIL, :CONTATO, :CODTCF, :ATIVO, :LIMITECREDITO, :VALORULTIMOLAN, :TIPOINSCRCNAB, :SIMBMOEDAINDEX, :DATAULTALTERACAO,
                        //                                    :DATACRIACAO, :DATAULTMOVIMENTO, :CONTEVENTOCONTAB, :CAMPOLIVRE, :CAMPOALFAOP1, :CAMPOALFAOP2, :CAMPOALFAOP3, :VALOROP1, :VALOROP2,
                        //                                    :VALOROP3, :DATAOP1, :DATAOP2, :DATAOP3, :CODTRA, :CHAPA, :STATUSCOTACAO, :DTINICATIVIDADES, :PATRIMONIO, :NUMFUNCIONARIOS, :CODCOLCHAVESESTRANG,
                        //                                    :CODCOLTCF, :FAXDEDICADO, :CODMUNICIPIO, :CODCOLCONTAGER, :CODCONTAGER, :FORMAPAGAMENTO, :IDENTPORCNPJ, :INSCRMUNICIPAL, :PESSOAFISOUJUR,
                        //                                    :CONTATOPGTO, :CONTATOENTREGA, :PAIS, :PAISPAGTO, :PAISENTREGA, :ULTIMODOCUMENTO, :CONTRIBUINTE, :CFOIMOB, :TIPODOC, :CODFINALIDADE, :AGRUPCOB,
                        //                                    :CODCARGO, :CODVINCULO, :ENDCOBC, :CIDENTIDADE, :CI_ORGAO, :CI_UF, :CODPROF, :CODPAGTOGPS, :FAXENTREGA, :EMAILENTREGA, :FAXPGTO, :EMAILPGTO,
                        //                                    :SATISFACAO, :VALFRETE, :TPTOMADOR, :CONTRIBUINTEISS, :NUMDEPENDENTES, :EMPRESA, :ESTADOCIVIL, :CODCOLCXA, :CODCXA, :PRODUTORRURAL, :USUARIOALTERACAO,
                        //                                    :SUFRAMA, :CODMUNICIPIOPGTO, :CODMUNICIPIOENTREGA, :ORGAOPUBLICO, :TELEFONECOMERCIAL, :CAIXAPOSTAL, :CAIXAPOSTALENTREGA, :CAIXAPOSTALPAGAMENTO,
                        //                                    :CATEGORIAAUTONOMO, :CBOAUTONOMO, :CIAUTONOMO, :IDCFO, :CODIGOINSS, :VROUTRASDEDUCOESIRRF, :CODRECEITA, :CEI, :OPTANTEPELOSIMPLES, :TIPORUA,
                        //                                    :TIPOBAIRRO, :REGIMEISS, :RETENCAOISS, :DTNASCIMENTO, :USUARIOCRIACAO, :TIPOOPCOMBUSTIVEL, :INSCRESTADUALST, :LOCALIDADE, :LOCALIDADEPGTO,
                        //                                    :LOCALIDADEENTREGA, :TIPORUAPGTO, :TIPORUAENTREGA, :TIPOBAIRROPGTO, :TIPOBAIRROENTREGA, :PORTE, :RAMOATIV, :NIT, :CEPCAIXAPOSTAL, :NUMDIASATRASO,
                        //                                    :IDPAIS, :IDPAISPGTO, :IDPAISENTREGA, :TIPOCONTRIBUINTEINSS, :NACIONALIDADE, :CODCOLCFOFISCAL, :IDCFOFISCAL, :EMAILFISCAL, :CALCULAAVP,
                        //                                    :CODUSUARIOACESSO, :IDINTEGRACAO, :USARCUMULATRETENCAOPAGAR, :NIF, :SITUACAONIF, :TIPORENDIMENTO, :FORMATRIBUTACAO, :INDNATRET,
                        //                                    :DOCUMENTOESTRANGEIRO,
                        //                                    :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                        sSql = @"INSERT INTO FCFO (CODCOLIGADA, CODCFO, NOMEFANTASIA, NOME, CGCCFO, INSCRESTADUAL, PAGREC, RUA, NUMERO, COMPLEMENTO, BAIRRO, CIDADE,
                                    CODETD, CEP, TELEFONE, RUAPGTO, NUMEROPGTO, COMPLEMENTOPGTO, BAIRROPGTO, CIDADEPGTO, CODETDPGTO, CEPPGTO, TELEFONEPGTO,
                                    RUAENTREGA, NUMEROENTREGA, COMPLEMENTREGA, BAIRROENTREGA, CIDADEENTREGA, CODETDENTREGA, CEPENTREGA, TELEFONEENTREGA, FAX,
                                    TELEX, EMAIL, CONTATO, CODTCF, ATIVO, LIMITECREDITO, VALORULTIMOLAN, TIPOINSCRCNAB, SIMBMOEDAINDEX, DATAULTALTERACAO,
                                    DATACRIACAO, DATAULTMOVIMENTO, CONTEVENTOCONTAB, CAMPOLIVRE, CAMPOALFAOP1, CAMPOALFAOP2, CAMPOALFAOP3, VALOROP1, VALOROP2,
                                    VALOROP3, DATAOP1, DATAOP2, DATAOP3, CODTRA, CHAPA, STATUSCOTACAO, DTINICATIVIDADES, PATRIMONIO, NUMFUNCIONARIOS, CODCOLCHAVESESTRANG,
                                    CODCOLTCF, FAXDEDICADO, CODMUNICIPIO, CODCOLCONTAGER, CODCONTAGER, FORMAPAGAMENTO, IDENTPORCNPJ, INSCRMUNICIPAL, PESSOAFISOUJUR,
                                    CONTATOPGTO, CONTATOENTREGA, PAIS, PAISPAGTO, PAISENTREGA, ULTIMODOCUMENTO, CONTRIBUINTE, CFOIMOB, TIPODOC, CODFINALIDADE, AGRUPCOB,
                                    CODCARGO, CODVINCULO, ENDCOBC, CIDENTIDADE, CI_ORGAO, CI_UF, CODPROF, CODPAGTOGPS, FAXENTREGA, EMAILENTREGA, FAXPGTO, EMAILPGTO,
                                    SATISFACAO, VALFRETE, TPTOMADOR, CONTRIBUINTEISS, NUMDEPENDENTES, EMPRESA, ESTADOCIVIL, CODCOLCXA, CODCXA, PRODUTORRURAL, USUARIOALTERACAO,
                                    SUFRAMA, CODMUNICIPIOPGTO, CODMUNICIPIOENTREGA, ORGAOPUBLICO, TELEFONECOMERCIAL, CAIXAPOSTAL, CAIXAPOSTALENTREGA, CAIXAPOSTALPAGAMENTO,
                                    CATEGORIAAUTONOMO, CBOAUTONOMO, CIAUTONOMO, IDCFO, CODIGOINSS, VROUTRASDEDUCOESIRRF, CODRECEITA, CEI, OPTANTEPELOSIMPLES, TIPORUA,
                                    TIPOBAIRRO, REGIMEISS, RETENCAOISS, DTNASCIMENTO, USUARIOCRIACAO, TIPOOPCOMBUSTIVEL, INSCRESTADUALST, LOCALIDADE, LOCALIDADEPGTO,
                                    LOCALIDADEENTREGA, TIPORUAPGTO, TIPORUAENTREGA, TIPOBAIRROPGTO, TIPOBAIRROENTREGA, PORTE, RAMOATIV, NIT, CEPCAIXAPOSTAL, NUMDIASATRASO,
                                    IDPAIS, IDPAISPGTO, IDPAISENTREGA, TIPOCONTRIBUINTEINSS, NACIONALIDADE, CODCOLCFOFISCAL, IDCFOFISCAL, EMAILFISCAL, CALCULAAVP,
                                    CODUSUARIOACESSO, IDINTEGRACAO, USARCUMULATRETENCAOPAGAR, NIF, SITUACAONIF, TIPORENDIMENTO, FORMATRIBUTACAO, INDNATRET,
                                    DOCUMENTOESTRANGEIRO,
                                    RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?,?,?,?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?,?,?, ?, ?, ?, ?, ?, ?, ?,?,?, ?, ?, ?)";

                        //DBS.QueryExec(sSql, /*CODCOLIGADA*/ clifor.CodColigada, /*CODCFO*/ clifor.CodCfo, /*NOMEFANTASIA*/ clifor.NomeFantasia, /*NOME*/ clifor.Nome,
                        //    /*CGCCFO*/ clifor.CGCCFO, /*INSCRESTADUAL*/ clifor.InscrEstadual, /*PAGREC*/ clifor.PagRec, /*RUA*/ clifor.Rua, /*NUMERO*/ clifor.Numero,
                        //    /*COMPLEMENTO*/ clifor.Complemento, /*BAIRRO*/ clifor.Bairro, /*CIDADE*/ clifor.Cidade, /*CODETD*/ clifor.CODETD, /*CEP*/ clifor.CEP,
                        //    /*TELEFONE*/ clifor.Telefone, /*RUAPGTO*/ clifor.RuaPgto, /*NUMEROPGTO*/ clifor.NumeroPgto, /*COMPLEMENTOPGTO*/ clifor.ComplementoPgto,
                        //    /*BAIRROPGTO*/ clifor.BairroPgto, /*CIDADEPGTO*/ clifor.CidadePgto, /*CODETDPGTO*/ clifor.CODETDPgto, /*CEPPGTO*/ clifor.CEPPgto,
                        //    /*TELEFONEPGTO*/ clifor.TelefonePgto, /*RUAENTREGA*/ clifor.RuaEntrega, /*NUMEROENTREGA*/ clifor.NumeroEntrega,
                        //    /*COMPLEMENTREGA*/ clifor.ComplemEntrega, /*BAIRROENTREGA*/ clifor.BairroEntrega, /*CIDADEENTREGA*/ clifor.CidadeEntrega,
                        //    /*CODETDENTREGA*/ clifor.CODETDEntrega, /*CEPENTREGA*/ clifor.CEPEntrega, /*TELEFONEENTREGA*/ clifor.TelefoneEntrega,
                        //    /*FAX*/ clifor.FAX, /*TELEX*/ clifor.Telex, /*EMAIL*/ clifor.Email, /*CONTATO*/ clifor.Contato, /*CODTCF*/ clifor.CODTCF,
                        //    /*ATIVO*/ clifor.Ativo, /*LIMITECREDITO*/ clifor.LimiteCredito, /*VALORULTIMOLAN*/ 0, /*TIPOINSCRCNAB*/ null, /*SIMBMOEDAINDEX*/ null,
                        //    /*DATAULTALTERACAO*/ null, /*DATACRIACAO*/ DBS.ServerDate(), /*DATAULTMOVIMENTO*/ null, /*CONTEVENTOCONTAB*/ null, /*CAMPOLIVRE*/ null,
                        //    /*CAMPOALFAOP1*/ null, /*CAMPOALFAOP2*/ null, /*CAMPOALFAOP3*/ null, /*VALOROP1*/ 0, /*VALOROP2*/ 0, /*VALOROP3*/ 0,
                        //    /*DATAOP1*/ null, /*DATAOP2*/ null, /*DATAOP3*/ null, /*CODTRA*/ null, /*CHAPA*/ null, /*STATUSCOTACAO*/ null, /*DTINICATIVIDADES*/ null,
                        //    /*PATRIMONIO*/ 0, /*NUMFUNCIONARIOS*/ 0, /*CODCOLCHAVESESTRANG*/ null, /*CODCOLTCF*/ clifor.CodCOLTCF, /*FAXDEDICADO*/ null,
                        //    /*CODMUNICIPIO*/ clifor.CodMunicipio, /*CODCOLCONTAGER*/ null, /*CODCONTAGER*/ null, /*FORMAPAGAMENTO*/ null, /*IDENTPORCNPJ*/ null,
                        //    /*INSCRMUNICIPAL*/ null, /*PESSOAFISOUJUR*/ clifor.PessoaFisOuJur, /*CONTATOPGTO*/ clifor.ContatoPgto,
                        //    /*CONTATOENTREGA*/ clifor.ContatoEntrega, /*PAIS*/ "Brasil", /*PAISPAGTO*/ "Brasil", /*PAISENTREGA*/ "Brasil", /*ULTIMODOCUMENTO*/ null,
                        //    /*CONTRIBUINTE*/ clifor.ContribuinteICMS, /*CFOIMOB*/ 0, /*TIPODOC*/ null, /*CODFINALIDADE*/ null, /*AGRUPCOB*/ null, /*CODCARGO*/ null, /*CODVINCULO*/ null,
                        //    /*ENDCOBC*/ null, /*CIDENTIDADE*/ null, /*CI_ORGAO*/ null, /*CI_UF*/ null, /*CODPROF*/ null, /*CODPAGTOGPS*/ null,
                        //    /*FAXENTREGA*/ clifor.FAXEntrega, /*EMAILENTREGA*/ clifor.EmailEntrega, /*FAXPGTO*/ clifor.FAXPgto, /*EMAILPGTO*/ clifor.EmailPgto,
                        //    /*SATISFACAO*/ null, /*VALFRETE*/ 0, /*TPTOMADOR*/ 0, /*CONTRIBUINTEISS*/ 0, /*NUMDEPENDENTES*/ 0, /*EMPRESA*/ null, /*ESTADOCIVIL*/ null,
                        //    /*CODCOLCXA*/ null, /*CODCXA*/ null, /*PRODUTORRURAL*/ null, /*USUARIOALTERACAO*/ null, /*SUFRAMA*/ clifor.Suframa,
                        //    /*CODMUNICIPIOPGTO*/ clifor.CodMunicipioPgto, /*CODMUNICIPIOENTREGA*/ clifor.CodMunicipioEntrega, /*ORGAOPUBLICO*/ 0,
                        //    /*TELEFONECOMERCIAL*/ null, /*CAIXAPOSTAL*/ null, /*CAIXAPOSTALENTREGA*/ null, /*CAIXAPOSTALPAGAMENTO*/ null, /*CATEGORIAAUTONOMO*/ 0,
                        //    /*CBOAUTONOMO*/ null, /*CIAUTONOMO*/ null, /*IDCFO*/ clifor.IdCfo, /*CODIGOINSS*/ null, /*VROUTRASDEDUCOESIRRF*/ 0, /*CODRECEITA*/ null,
                        //    /*CEI*/ null, /*OPTANTEPELOSIMPLES*/ 0, /*TIPORUA*/ clifor.TipoRua, /*TIPOBAIRRO*/ clifor.TipoBairro, /*REGIMEISS*/ "N", /*RETENCAOISS*/ 0,
                        //    /*DTNASCIMENTO*/ null, /*USUARIOCRIACAO*/ clifor.UsuarioCriacao, /*TIPOOPCOMBUSTIVEL*/ 3, /*INSCRESTADUALST*/ null, /*LOCALIDADE*/ null,
                        //    /*LOCALIDADEPGTO*/ null, /*LOCALIDADEENTREGA*/ null, /*TIPORUAPGTO*/ clifor.TipoRuaPgto, /*TIPORUAENTREGA*/ clifor.TipoRuaEntrega,
                        //    /*TIPOBAIRROPGTO*/ clifor.TipoBairroPgto, /*TIPOBAIRROENTREGA*/ clifor.TipoBairroEntrega, /*PORTE*/ 0, /*RAMOATIV*/ clifor.RamoAtiv,
                        //    /*NIT*/ null, /*CEPCAIXAPOSTAL*/ null, /*NUMDIASATRASO*/ 0, /*IDPAIS*/ clifor.IDPais, /*IDPAISPGTO*/ clifor.IDPaisPgto,
                        //    /*IDPAISENTREGA*/ clifor.IDPaisEntrega, /*TIPOCONTRIBUINTEINSS*/ 0, /*NACIONALIDADE*/ clifor.Nacionalidade, /*CODCOLCFOFISCAL*/ null, /*IDCFOFISCAL*/ null,
                        //    /*EMAILFISCAL*/ null, /*CALCULAAVP*/ 0, /*CODUSUARIOACESSO*/ null, /*IDINTEGRACAO*/ null, /*USARCUMULATRETENCAOPAGAR*/ 1, /*NIF*/ null,
                        //    /*SITUACAONIF*/ 0, /*TIPORENDIMENTO*/ "000", /*FORMATRIBUTACAO*/ "00", /*INDNATRET*/ null,
                        //    /*DOCUMENTOESTRANGEIRO*/ clifor.DocumentosEstrangeiro,
                        //    /*RECCREATEDBY*/ clifor.UsuarioCriacao, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ DBS.ServerDate());

                        conn.ExecTransaction(sSql, /*CODCOLIGADA*/ clifor.CodColigada, /*CODCFO*/ clifor.CodCfo, /*NOMEFANTASIA*/ clifor.NomeFantasia, /*NOME*/ clifor.Nome,
                            /*CGCCFO*/ clifor.CGCCFO, /*INSCRESTADUAL*/ clifor.InscrEstadual, /*PAGREC*/ clifor.PagRec, /*RUA*/ clifor.Rua, /*NUMERO*/ clifor.Numero,
                            /*COMPLEMENTO*/ clifor.Complemento, /*BAIRRO*/ clifor.Bairro, /*CIDADE*/ clifor.Cidade, /*CODETD*/ clifor.CODETD, /*CEP*/ clifor.CEP,
                            /*TELEFONE*/ clifor.Telefone, /*RUAPGTO*/ clifor.RuaPgto, /*NUMEROPGTO*/ clifor.NumeroPgto, /*COMPLEMENTOPGTO*/ clifor.ComplementoPgto,
                            /*BAIRROPGTO*/ clifor.BairroPgto, /*CIDADEPGTO*/ clifor.CidadePgto, /*CODETDPGTO*/ clifor.CODETDPgto, /*CEPPGTO*/ clifor.CEPPgto,
                            /*TELEFONEPGTO*/ clifor.TelefonePgto, /*RUAENTREGA*/ clifor.RuaEntrega, /*NUMEROENTREGA*/ clifor.NumeroEntrega,
                            /*COMPLEMENTREGA*/ clifor.ComplemEntrega, /*BAIRROENTREGA*/ clifor.BairroEntrega, /*CIDADEENTREGA*/ clifor.CidadeEntrega,
                            /*CODETDENTREGA*/ clifor.CODETDEntrega, /*CEPENTREGA*/ clifor.CEPEntrega, /*TELEFONEENTREGA*/ clifor.TelefoneEntrega,
                            /*FAX*/ clifor.FAX, /*TELEX*/ clifor.Telex, /*EMAIL*/ clifor.Email, /*CONTATO*/ clifor.Contato, /*CODTCF*/ clifor.CODTCF,
                            /*ATIVO*/ clifor.Ativo, /*LIMITECREDITO*/ clifor.LimiteCredito, /*VALORULTIMOLAN*/ 0, /*TIPOINSCRCNAB*/ null, /*SIMBMOEDAINDEX*/ null,
                            /*DATAULTALTERACAO*/ null, /*DATACRIACAO*/ conn.GetDateTime(), /*DATAULTMOVIMENTO*/ null, /*CONTEVENTOCONTAB*/ null, /*CAMPOLIVRE*/ null,
                            /*CAMPOALFAOP1*/ null, /*CAMPOALFAOP2*/ null, /*CAMPOALFAOP3*/ null, /*VALOROP1*/ 0, /*VALOROP2*/ 0, /*VALOROP3*/ 0,
                            /*DATAOP1*/ null, /*DATAOP2*/ null, /*DATAOP3*/ null, /*CODTRA*/ null, /*CHAPA*/ null, /*STATUSCOTACAO*/ null, /*DTINICATIVIDADES*/ null,
                            /*PATRIMONIO*/ 0, /*NUMFUNCIONARIOS*/ 0, /*CODCOLCHAVESESTRANG*/ null, /*CODCOLTCF*/ clifor.CodCOLTCF, /*FAXDEDICADO*/ null,
                            /*CODMUNICIPIO*/ clifor.CodMunicipio, /*CODCOLCONTAGER*/ null, /*CODCONTAGER*/ null, /*FORMAPAGAMENTO*/ null, /*IDENTPORCNPJ*/ null,
                            /*INSCRMUNICIPAL*/ null, /*PESSOAFISOUJUR*/ clifor.PessoaFisOuJur, /*CONTATOPGTO*/ clifor.ContatoPgto,
                            /*CONTATOENTREGA*/ clifor.ContatoEntrega, /*PAIS*/ "Brasil", /*PAISPAGTO*/ "Brasil", /*PAISENTREGA*/ "Brasil", /*ULTIMODOCUMENTO*/ null,
                            /*CONTRIBUINTE*/ clifor.ContribuinteICMS, /*CFOIMOB*/ 0, /*TIPODOC*/ null, /*CODFINALIDADE*/ null, /*AGRUPCOB*/ null, /*CODCARGO*/ null, /*CODVINCULO*/ null,
                            /*ENDCOBC*/ null, /*CIDENTIDADE*/ null, /*CI_ORGAO*/ null, /*CI_UF*/ null, /*CODPROF*/ null, /*CODPAGTOGPS*/ null,
                            /*FAXENTREGA*/ clifor.FAXEntrega, /*EMAILENTREGA*/ clifor.EmailEntrega, /*FAXPGTO*/ clifor.FAXPgto, /*EMAILPGTO*/ clifor.EmailPgto,
                            /*SATISFACAO*/ null, /*VALFRETE*/ 0, /*TPTOMADOR*/ 0, /*CONTRIBUINTEISS*/ 0, /*NUMDEPENDENTES*/ 0, /*EMPRESA*/ null, /*ESTADOCIVIL*/ null,
                            /*CODCOLCXA*/ null, /*CODCXA*/ null, /*PRODUTORRURAL*/ null, /*USUARIOALTERACAO*/ null, /*SUFRAMA*/ clifor.Suframa,
                            /*CODMUNICIPIOPGTO*/ clifor.CodMunicipioPgto, /*CODMUNICIPIOENTREGA*/ clifor.CodMunicipioEntrega, /*ORGAOPUBLICO*/ 0,
                            /*TELEFONECOMERCIAL*/ null, /*CAIXAPOSTAL*/ null, /*CAIXAPOSTALENTREGA*/ null, /*CAIXAPOSTALPAGAMENTO*/ null, /*CATEGORIAAUTONOMO*/ 0,
                            /*CBOAUTONOMO*/ null, /*CIAUTONOMO*/ null, /*IDCFO*/ clifor.IdCfo, /*CODIGOINSS*/ null, /*VROUTRASDEDUCOESIRRF*/ 0, /*CODRECEITA*/ null,
                            /*CEI*/ null, /*OPTANTEPELOSIMPLES*/ 0, /*TIPORUA*/ clifor.TipoRua, /*TIPOBAIRRO*/ clifor.TipoBairro, /*REGIMEISS*/ "N", /*RETENCAOISS*/ 0,
                            /*DTNASCIMENTO*/ null, /*USUARIOCRIACAO*/ clifor.UsuarioCriacao, /*TIPOOPCOMBUSTIVEL*/ 3, /*INSCRESTADUALST*/ null, /*LOCALIDADE*/ null,
                            /*LOCALIDADEPGTO*/ null, /*LOCALIDADEENTREGA*/ null, /*TIPORUAPGTO*/ clifor.TipoRuaPgto, /*TIPORUAENTREGA*/ clifor.TipoRuaEntrega,
                            /*TIPOBAIRROPGTO*/ clifor.TipoBairroPgto, /*TIPOBAIRROENTREGA*/ clifor.TipoBairroEntrega, /*PORTE*/ 0, /*RAMOATIV*/ clifor.RamoAtiv,
                            /*NIT*/ null, /*CEPCAIXAPOSTAL*/ null, /*NUMDIASATRASO*/ 0, /*IDPAIS*/ clifor.IDPais, /*IDPAISPGTO*/ clifor.IDPaisPgto,
                            /*IDPAISENTREGA*/ clifor.IDPaisEntrega, /*TIPOCONTRIBUINTEINSS*/ 0, /*NACIONALIDADE*/ clifor.Nacionalidade, /*CODCOLCFOFISCAL*/ null, /*IDCFOFISCAL*/ null,
                            /*EMAILFISCAL*/ null, /*CALCULAAVP*/ 0, /*CODUSUARIOACESSO*/ null, /*IDINTEGRACAO*/ null, /*USARCUMULATRETENCAOPAGAR*/ 1, /*NIF*/ null,
                            /*SITUACAONIF*/ 0, /*TIPORENDIMENTO*/ "000", /*FORMATRIBUTACAO*/ "00", /*INDNATRET*/ null,
                            /*DOCUMENTOESTRANGEIRO*/ clifor.DocumentosEstrangeiro,
                            /*RECCREATEDBY*/ clifor.UsuarioCriacao, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ conn.GetDateTime());

                        if (clifor.HISTORICOATUAL != null)
                            if (clifor.HISTORICOATUAL.ToString() == string.Empty)
                                clifor.HISTORICOATUAL = null;

                        sPoint = "FCFOCOMPL - ";
                        //                        sSql = @"INSERT INTO FCFOCOMPL (CODCOLIGADA, CODCFO, HISTORICOATUAL, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                        //                                    VALUES (:CODCOLIGADA, :CODCFO, :HISTORICOATUAL, :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                        sSql = @"INSERT INTO FCFOCOMPL (CODCOLIGADA, CODCFO, HISTORICOATUAL, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                    VALUES (:CODCOLIGADA, :CODCFO, :HISTORICOATUAL, :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";


                        //DBS.QueryExec(sSql, /*CODCOLIGADA*/ clifor.CodColigada, /*CODCFO*/ clifor.CodCfo, /*HISTORICOATUAL*/ clifor.HISTORICOATUAL, /*RECCREATEDBY*/ clifor.UsuarioCriacao, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ DBS.ServerDate());
                        conn.ExecTransaction(@"INSERT INTO FCFOCOMPL (CODCOLIGADA, CODCFO, HISTORICOATUAL, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON) 
                                    VALUES (?, ?, ?, ?, ?, ?, ?)", new object[] {/*CODCOLIGADA*/ clifor.CodColigada, /*CODCFO*/ clifor.CodCfo, /*HISTORICOATUAL*/ clifor.HISTORICOATUAL,  /*RECCREATEDBY*/ clifor.UsuarioCriacao, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ conn.GetDateTime() });

                        sPoint = "FCFOHISTORICO - ";

                        //                        sSql = @"INSERT INTO FCFOHISTORICO (CODCOLIGADA, IDHISTORICO, CODCFO, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON)
                        //                                    VALUES (:CODCOLIGADA, :IDHISTORICO, :CODCFO, :RECCREATEDBY, :RECCREATEDON, :RECMODIFIEDBY, :RECMODIFIEDON)";

                        //                        DBS.QueryExec(sSql, clifor.CodColigada, IdHistorico, clifor.CodCfo,
                        //                            /*RECCREATEDBY*/ clifor.UsuarioCriacao, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ DBS.ServerDate());
                        conn.ExecTransaction(@"INSERT INTO FCFOHISTORICO (CODCOLIGADA, IDHISTORICO, CODCFO, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON)
                                  VALUES (?, ?, ?, ?, ?, ?, ?)", new object[] { clifor.CodColigada, IdHistorico, clifor.CodCfo,
                            /*RECCREATEDBY*/ clifor.UsuarioCriacao, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ conn.GetDateTime()});


                        sPoint = "Busca FCFODEF - ";


                        // sSql = @"SELECT CODCFO FROM FCFODEF WHERE CODCOLIGADA = :CODCOLIGADA AND CODCOLCFO = :CODCOLCFO AND CODCFO = :CODCFO";

                        if (Convert.ToInt32(conn.ExecGetField(0, @"SELECT CODCFO FROM FCFODEF WHERE CODCOLIGADA = ? AND CODCOLCFO = ? AND CODCFO = ?", new object[] { 1, clifor.CodColigada, clifor.CodCfo })) > 0)
                        {
                            sPoint = "Atualiza FCFODEF - ";
                            conn.ExecTransaction(@"UPDATE FCFODEF SET CODRPR = ? WHERE CODCOLIGADA = ? AND CODCOLCFO = ? AND CODCFO = ?", new object[] { 1, clifor.CodRpr, clifor.CodColigada, clifor.CodCfo });

                        }
                        else
                        {
                            sPoint = "Insere FCFODEF - ";
                            conn.ExecTransaction(@"INSERT INTO FCFODEF (CODCOLIGADA, CODCFO, CODCOLCFO, CODRPR) VALUES (?, ?, ?, ?)", new object[] { 1, clifor.CodCfo, clifor.CodColigada, clifor.CodRpr });
                        }
                        //if (DBS.QueryFind(sSql, 1, clifor.CodColigada, clifor.CodCfo))
                        //{
                        //    sPoint = "Atualiza FCFODEF - ";
                        //    sSql = @"UPDATE FCFODEF SET CODRPR = :CODRPR WHERE CODCOLIGADA = :CODCOLIGADA AND CODCOLCFO = :CODCOLCFO AND CODCFO = :CODCFO";
                        //    this.DBS.SkipControlColumns = true;
                        //    DBS.QueryExec(sSql, 1, clifor.CodRpr, clifor.CodColigada, clifor.CodCfo);
                        //    this.DBS.SkipControlColumns = false;
                        //}
                        //else
                        //{
                        //    sPoint = "Insere FCFODEF - ";
                        //    sSql = @"INSERT INTO FCFODEF (CODCOLIGADA, CODCFO, CODCOLCFO, CODRPR) VALUES (:CODCOLIGADA, :CODCFO, :CODCOLCFO, :CODRPR)";
                        //    this.DBS.SkipControlColumns = true;
                        //    DBS.QueryExec(sSql, 1, clifor.CodCfo, clifor.CodColigada, clifor.CodRpr);
                        //    this.DBS.SkipControlColumns = false;
                        //}

                        // DBS.Commit();
                        conn.Commit();
                        msg.Retorno = clifor.CodCfo;
                        msg.Mensagem = "Cadastro realizado com sucesso.";

                        #endregion
                    }
                    else
                    {
                        #region Alteração

                        bool Flag = false;

                        if (!string.IsNullOrEmpty(clifor.CGCCFO))
                        {
                            if (!new Util().ValidaCNPJCPF(clifor.CGCCFO))
                                throw new Exception("CNPJ/CPF informado não é válido.");

                            if (clifor.CGCCFO.Equals("000.000.000-00")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("111.111.111-11")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("222.222.222-22")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("333.333.333-33")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("444.444.444-44")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("555.555.555-55")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("666.666.666-66")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("777.777.777-77")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("888.888.888-88")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("999.999.999-99")) { Flag = true; }

                            if (clifor.CGCCFO.Equals("00.000.000/0000-00")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("11.111.111/1111-11")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("22.222.222/2222-22")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("33.333.333/3333-33")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("44.444.444/4444-44")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("55.555.555/5555-55")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("66.666.666/6666-66")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("77.777.777/7777-77")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("88.888.888/8888-88")) { Flag = true; }
                            if (clifor.CGCCFO.Equals("99.999.999/9999-99")) { Flag = true; }

                            if (!Flag)
                            {
                                if (clifor.PessoaFisOuJur == "J")
                                {
                                    //sSql = @"SELECT CODCFO FROM FCFO WHERE CODCOLIGADA = :CODCOLIGADA AND CGCCFO = :CGCCFO";
                                    //if (DBS.QueryFind(sSql, clifor.CodColigada, clifor.CGCCFO))
                                    //{
                                    //    if (clifor.CodCfo != DBS.QueryValue(0, sSql, clifor.CodColigada, clifor.CGCCFO).ToString())
                                    //        throw new Exception("CNPJ informado já esta cadastrado.");
                                    //}
                                    if (Convert.ToInt32(conn.ExecGetField(0, @"SELECT COUNT(CODCFO) FROM FCFO WHERE CODCOLIGADA = ? AND CGCCFO = ?", new object[] { clifor.CodColigada, clifor.CGCCFO })) > 0)
                                    {
                                        if (clifor.CodCfo != conn.ExecGetField("0", @"SELECT CODCFO FROM FCFO WHERE CODCOLIGADA = ? AND CGCCFO = ?", clifor.CodColigada, clifor.CGCCFO).ToString())
                                        {
                                            throw new Exception("CNPJ informado já esta cadastrado.");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                clifor.CGCCFO = null;
                            }
                        }

                        //sSql = @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = :CODMUNICIPIO AND CODETDMUNICIPIO = :CODETDMUNICIPIO";
                        //object retorno = DBS.QueryValue(null, sSql, clifor.CodMunicipio, clifor.CODETD);
                        if (clifor.IDPais == 1)
                        {
                            sSql = @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = ? AND CODETDMUNICIPIO = ?";
                            object retorno1 = conn.ExecGetField(null, sSql, clifor.CodMunicipio, clifor.CODETD);
                            clifor.Cidade = (retorno1 == null) ? null : retorno1.ToString();
                        }


                        //sSql = @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = :CODMUNICIPIO AND CODETDMUNICIPIO = :CODETDMUNICIPIO";
                        //retorno = DBS.QueryValue(null, sSql, clifor.CodMunicipioPgto, clifor.CODETDPgto);
                        sSql = @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = ? AND CODETDMUNICIPIO = ?";
                        object retorno = conn.ExecGetField(null, sSql, clifor.CodMunicipioPgto, clifor.CODETDPgto);
                        clifor.CidadePgto = (retorno == null) ? null : retorno.ToString();

                        //sSql = @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = :CODMUNICIPIO AND CODETDMUNICIPIO = :CODETDMUNICIPIO";
                        //retorno = DBS.QueryValue(null, sSql, clifor.CodMunicipioEntrega, clifor.CODETDEntrega);
                        sSql = @"SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODMUNICIPIO = ? AND CODETDMUNICIPIO = ?";
                        retorno = conn.ExecGetField(null, sSql, clifor.CodMunicipioEntrega, clifor.CODETDEntrega);
                        clifor.CidadeEntrega = (retorno == null) ? null : retorno.ToString();

                        //DBS.BeginTransaction();
                        conn.BeginTransaction();
                        //                        sSql = @"UPDATE FCFO SET NOMEFANTASIA = :NOMEFANTASIA, NOME = :NOME, CGCCFO = :CGCCFO, INSCRESTADUAL = :INSCRESTADUAL, PAGREC = :PAGREC, 
                        //                            RUA = :RUA, NUMERO = :NUMERO, COMPLEMENTO = :COMPLEMENTO, BAIRRO = :BAIRRO, CIDADE = :CIDADE, CODETD = :CODETD, CEP = :CEP,
                        //                            TELEFONE = :TELEFONE, RUAPGTO = :RUAPGTO, NUMEROPGTO = :NUMEROPGTO, COMPLEMENTOPGTO = :COMPLEMENTOPGTO, BAIRROPGTO = :BAIRROPGTO, 
                        //                            CIDADEPGTO = :CIDADEPGTO, CODETDPGTO = :CODETDPGTO, CEPPGTO = :CEPPGTO, TELEFONEPGTO = :TELEFONEPGTO, RUAENTREGA = :RUAENTREGA, 
                        //                            NUMEROENTREGA = :NUMEROENTREGA, COMPLEMENTREGA = :COMPLEMENTREGA, BAIRROENTREGA = :BAIRROENTREGA, CIDADEENTREGA = :CIDADEENTREGA,
                        //                            CODETDENTREGA = :CODETDENTREGA, CEPENTREGA = :CEPENTREGA, TELEFONEENTREGA = :TELEFONEENTREGA, FAX = :FAX, TELEX = :TELEX, EMAIL = :EMAIL, 
                        //                            CONTATO = :CONTATO, CODTCF = :CODTCF, ATIVO = :ATIVO, LIMITECREDITO = :LIMITECREDITO, DATAULTALTERACAO = :DATAULTALTERACAO, CODCOLTCF = :CODCOLTCF,
                        //                            CODMUNICIPIO = :CODMUNICIPIO, PESSOAFISOUJUR = :PESSOAFISOUJUR, CONTATOPGTO = :CONTATOPGTO, CONTATOENTREGA = :CONTATOENTREGA, 
                        //                            FAXENTREGA = :FAXENTREGA, EMAILENTREGA = :EMAILENTREGA, FAXPGTO = :FAXPGTO, EMAILPGTO = :EMAILPGTO, USUARIOALTERACAO = :USUARIOALTERACAO, 
                        //                            SUFRAMA = :SUFRAMA, CODMUNICIPIOPGTO = :CODMUNICIPIOPGTO, CODMUNICIPIOENTREGA = :CODMUNICIPIOENTREGA, TIPORUA = :TIPORUA, TIPOBAIRRO = :TIPOBAIRRO, 
                        //                            TIPORUAPGTO = :TIPORUAPGTO, TIPORUAENTREGA = :TIPORUAENTREGA, TIPOBAIRROPGTO = :TIPOBAIRROPGTO, TIPOBAIRROENTREGA = :TIPOBAIRROENTREGA, 
                        //                            RAMOATIV = :RAMOATIV, IDPAIS = :IDPAIS, IDPAISPGTO = :IDPAISPGTO, IDPAISENTREGA = :IDPAISENTREGA, 
                        //                            CONTRIBUINTE = :CONTRIBUINTE, NACIONALIDADE = :NACIONALIDADE, DOCUMENTOESTRANGEIRO = :DOCUMENTOESTRANGEIRO,
                        //                            RECCREATEDBY = :RECCREATEDBY, RECCREATEDON = :RECCREATEDON, RECMODIFIEDBY = :RECMODIFIEDBY, RECMODIFIEDON = :RECMODIFIEDON
                        //                            WHERE CODCOLIGADA = :CODCOLIGADA AND CODCFO = :CODCFO";
                        sSql = @"UPDATE FCFO SET NOMEFANTASIA = ?, NOME = ?, CGCCFO = ?, INSCRESTADUAL = ?, PAGREC = ?, 
                            RUA = ?, NUMERO = ?, COMPLEMENTO = ?, BAIRRO = ?, CIDADE = ?, CODETD = ?, CEP = ?,
                            TELEFONE = ?, RUAPGTO = ?, NUMEROPGTO = ?, COMPLEMENTOPGTO = ?, BAIRROPGTO = ?, 
                            CIDADEPGTO = ?, CODETDPGTO = ?, CEPPGTO = ?, TELEFONEPGTO = ?, RUAENTREGA = ?, 
                            NUMEROENTREGA = ?, COMPLEMENTREGA = ?, BAIRROENTREGA = ?, CIDADEENTREGA = ?,
                            CODETDENTREGA = ?, CEPENTREGA = ?, TELEFONEENTREGA = ?, FAX = ?, TELEX = ?, EMAIL = ?, 
                            CONTATO = ?, CODTCF = ?, ATIVO = ?, LIMITECREDITO = ?, DATAULTALTERACAO = ?, CODCOLTCF = ?,
                            CODMUNICIPIO = ?, PESSOAFISOUJUR = ?, CONTATOPGTO = ?, CONTATOENTREGA = ?, 
                            FAXENTREGA = ?, EMAILENTREGA = ?, FAXPGTO = ?, EMAILPGTO = ?, USUARIOALTERACAO = ?, 
                            SUFRAMA = ?, CODMUNICIPIOPGTO = ?, CODMUNICIPIOENTREGA = ?, TIPORUA = ?, TIPOBAIRRO = ?, 
                            TIPORUAPGTO = ?, TIPORUAENTREGA = ?, TIPOBAIRROPGTO = ?, TIPOBAIRROENTREGA = ?, 
                            RAMOATIV = ?, IDPAIS = ?, IDPAISPGTO = ?, IDPAISENTREGA = ?, 
                            CONTRIBUINTE = ?, NACIONALIDADE = ?, DOCUMENTOESTRANGEIRO = ?,
                            RECMODIFIEDBY = ?, RECMODIFIEDON = ?
                            WHERE CODCOLIGADA = ? AND CODCFO = ?";

                        //DBS.QueryExec(sSql, /*NOMEFANTASIA*/ clifor.NomeFantasia, /*NOME*/ clifor.Nome, /*CGCCFO*/ clifor.CGCCFO, /*INSCRESTADUAL*/ clifor.InscrEstadual,
                        //    /*PAGREC*/ clifor.PagRec, /*RUA*/ clifor.Rua, /*NUMERO*/ clifor.Numero, /*COMPLEMENTO*/ clifor.Complemento, /*BAIRRO*/ clifor.Bairro,
                        //    /*CIDADE*/ clifor.Cidade, /*CODETD*/ clifor.CODETD, /*CEP*/ clifor.CEP, /*TELEFONE*/ clifor.Telefone, /*RUAPGTO*/ clifor.RuaPgto,
                        //    /*NUMEROPGTO*/ clifor.NumeroPgto, /*COMPLEMENTOPGTO*/ clifor.ComplementoPgto, /*BAIRROPGTO*/ clifor.BairroPgto, /*CIDADEPGTO*/ clifor.CidadePgto,
                        //    /*CODETDPGTO*/ clifor.CODETDPgto, /*CEPPGTO*/ clifor.CEPPgto, /*TELEFONEPGTO*/ clifor.TelefonePgto, /*RUAENTREGA*/ clifor.RuaEntrega,
                        //    /*NUMEROENTREGA*/ clifor.NumeroEntrega, /*COMPLEMENTREGA*/ clifor.ComplemEntrega, /*BAIRROENTREGA*/ clifor.BairroEntrega,
                        //    /*CIDADEENTREGA*/ clifor.CidadeEntrega, /*CODETDENTREGA*/ clifor.CODETDEntrega, /*CEPENTREGA*/ clifor.CEPEntrega,
                        //    /*TELEFONEENTREGA*/ clifor.TelefoneEntrega, /*FAX*/ clifor.FAX, /*TELEX*/ clifor.Telex, /*EMAIL*/ clifor.Email, /*CONTATO*/ clifor.Contato,
                        //    /*CODTCF*/ clifor.CODTCF, /*ATIVO*/ clifor.Ativo, /*LIMITECREDITO*/ clifor.LimiteCredito, /*DATAULTALTERACAO*/ DateTime.Today,
                        //    /*CODCOLTCF*/ clifor.CodCOLTCF, /*CODMUNICIPIO*/ clifor.CodMunicipio, /*PESSOAFISOUJUR*/ clifor.PessoaFisOuJur, /*CONTATOPGTO*/ clifor.ContatoPgto,
                        //    /*CONTATOENTREGA*/ clifor.ContatoEntrega, /*FAXENTREGA*/ clifor.FAXEntrega, /*EMAILENTREGA*/ clifor.EmailEntrega, /*FAXPGTO*/ clifor.FAXPgto,
                        //    /*EMAILPGTO*/ clifor.EmailPgto, /*USUARIOALTERACAO*/ clifor.UsuarioAlteracao, /*SUFRAMA*/ clifor.Suframa,
                        //    /*CODMUNICIPIOPGTO*/ clifor.CodMunicipioPgto, /*CODMUNICIPIOENTREGA*/ clifor.CodMunicipioEntrega, /*TIPORUA*/ clifor.TipoRua,
                        //    /*TIPOBAIRRO*/ clifor.TipoBairro, /*TIPORUAPGTO*/ clifor.TipoRuaPgto, /*TIPORUAENTREGA*/ clifor.TipoRuaEntrega,
                        //    /*TIPOBAIRROPGTO*/ clifor.TipoBairroPgto, /*TIPOBAIRROENTREGA*/ clifor.TipoBairroEntrega, /*RAMOATIV*/ clifor.RamoAtiv,
                        //    /*IDPAIS*/ clifor.IDPais, /*IDPAISPGTO*/ clifor.IDPaisPgto, /*IDPAISENTREGA*/ clifor.IDPaisEntrega,
                        //    /*CONTRIBUINTE*/ clifor.ContribuinteICMS, /*NACIONALIDADE*/ clifor.Nacionalidade, /*DOCUMENTOESTRANGEIRO*/ clifor.DocumentosEstrangeiro,
                        //    /*RECCREATEDBY*/ clifor.UsuarioCriacao, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ DBS.ServerDate(),
                        //    clifor.CodColigada, clifor.CodCfo);

                        conn.ExecTransaction(sSql, /*NOMEFANTASIA*/ clifor.NomeFantasia, /*NOME*/ clifor.Nome, /*CGCCFO*/ clifor.CGCCFO, /*INSCRESTADUAL*/ clifor.InscrEstadual,
                            /*PAGREC*/ clifor.PagRec, /*RUA*/ clifor.Rua, /*NUMERO*/ clifor.Numero, /*COMPLEMENTO*/ clifor.Complemento, /*BAIRRO*/ clifor.Bairro,
                            /*CIDADE*/ clifor.Cidade, /*CODETD*/ clifor.CODETD, /*CEP*/ clifor.CEP, /*TELEFONE*/ clifor.Telefone, /*RUAPGTO*/ clifor.RuaPgto,
                            /*NUMEROPGTO*/ clifor.NumeroPgto, /*COMPLEMENTOPGTO*/ clifor.ComplementoPgto, /*BAIRROPGTO*/ clifor.BairroPgto, /*CIDADEPGTO*/ clifor.CidadePgto,
                            /*CODETDPGTO*/ clifor.CODETDPgto, /*CEPPGTO*/ clifor.CEPPgto, /*TELEFONEPGTO*/ clifor.TelefonePgto, /*RUAENTREGA*/ clifor.RuaEntrega,
                            /*NUMEROENTREGA*/ clifor.NumeroEntrega, /*COMPLEMENTREGA*/ clifor.ComplemEntrega, /*BAIRROENTREGA*/ clifor.BairroEntrega,
                            /*CIDADEENTREGA*/ clifor.CidadeEntrega, /*CODETDENTREGA*/ clifor.CODETDEntrega, /*CEPENTREGA*/ clifor.CEPEntrega,
                            /*TELEFONEENTREGA*/ clifor.TelefoneEntrega, /*FAX*/ clifor.FAX, /*TELEX*/ clifor.Telex, /*EMAIL*/ clifor.Email, /*CONTATO*/ clifor.Contato,
                            /*CODTCF*/ clifor.CODTCF, /*ATIVO*/ clifor.Ativo, /*LIMITECREDITO*/ clifor.LimiteCredito, /*DATAULTALTERACAO*/ DateTime.Today,
                            /*CODCOLTCF*/ clifor.CodCOLTCF, /*CODMUNICIPIO*/ clifor.CodMunicipio, /*PESSOAFISOUJUR*/ clifor.PessoaFisOuJur, /*CONTATOPGTO*/ clifor.ContatoPgto,
                            /*CONTATOENTREGA*/ clifor.ContatoEntrega, /*FAXENTREGA*/ clifor.FAXEntrega, /*EMAILENTREGA*/ clifor.EmailEntrega, /*FAXPGTO*/ clifor.FAXPgto,
                            /*EMAILPGTO*/ clifor.EmailPgto, /*USUARIOALTERACAO*/ clifor.UsuarioAlteracao, /*SUFRAMA*/ clifor.Suframa,
                            /*CODMUNICIPIOPGTO*/ clifor.CodMunicipioPgto, /*CODMUNICIPIOENTREGA*/ clifor.CodMunicipioEntrega, /*TIPORUA*/ clifor.TipoRua,
                            /*TIPOBAIRRO*/ clifor.TipoBairro, /*TIPORUAPGTO*/ clifor.TipoRuaPgto, /*TIPORUAENTREGA*/ clifor.TipoRuaEntrega,
                            /*TIPOBAIRROPGTO*/ clifor.TipoBairroPgto, /*TIPOBAIRROENTREGA*/ clifor.TipoBairroEntrega, /*RAMOATIV*/ clifor.RamoAtiv,
                            /*IDPAIS*/ clifor.IDPais, /*IDPAISPGTO*/ clifor.IDPaisPgto, /*IDPAISENTREGA*/ clifor.IDPaisEntrega,
                            /*CONTRIBUINTE*/ clifor.ContribuinteICMS, /*NACIONALIDADE*/ clifor.Nacionalidade, /*DOCUMENTOESTRANGEIRO*/ clifor.DocumentosEstrangeiro,
                            /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ conn.GetDateTime(),
                         clifor.CodColigada, clifor.CodCfo);

                        #region FCFOCOMPL

                        //if (clifor.HISTORICOATUAL != null)
                        //    if (clifor.HISTORICOATUAL == string.Empty)
                        //        clifor.HISTORICOATUAL = null;

                        ////                        sSql = @"UPDATE FCFOCOMPL SET HISTORICOATUAL = :HISTORICOATUAL, 
                        ////                                    RECCREATEDBY = :RECCREATEDBY, RECCREATEDON = :RECCREATEDON, RECMODIFIEDBY = :RECMODIFIEDBY, RECMODIFIEDON = :RECMODIFIEDON
                        ////                                WHERE CODCOLIGADA = :CODCOLIGADA AND CODCFO = :CODCFO";

                        ////                        DBS.QueryExec(sSql, /*HISTORICOATUAL*/ clifor.HISTORICOATUAL,
                        ////                            /*RECCREATEDBY*/ clifor.UsuarioCriacao, /*RECCREATEDON*/ DBS.ServerDate(), /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ DBS.ServerDate(),
                        ////                            /*CODCOLIGADA*/ clifor.CodColigada, /*CODCFO*/ clifor.CodCfo);

                        //sSql = @"UPDATE FCFOCOMPL SET HISTORICOATUAL = ?, 
                        //            RECCREATEDBY = ?, RECCREATEDON = ?, RECMODIFIEDBY = ?, RECMODIFIEDON = ?
                        //        WHERE CODCOLIGADA = ? AND CODCFO = ?";

                        //conn.ExecTransaction(sSql, /*HISTORICOATUAL*/ clifor.HISTORICOATUAL,
                        //    /*RECCREATEDBY*/ clifor.UsuarioCriacao, /*RECCREATEDON*/ conn.GetDateTime(), /*RECMODIFIEDBY*/ clifor.UsuarioCriacao, /*RECMODIFIEDON*/ conn.GetDateTime(),
                        //    /*CODCOLIGADA*/ clifor.CodColigada, /*CODCFO*/ clifor.CodCfo);

                        #endregion

                        //sSql = @"SELECT CODCFO FROM FCFODEF WHERE CODCOLIGADA = :CODCOLIGADA AND CODCOLCFO = :CODCOLCFO AND CODCFO = :CODCFO";
                        //if (DBS.QueryFind(sSql, 1, clifor.CodColigada, clifor.CodCfo))
                        //{
                        //    sSql = @"UPDATE FCFODEF SET CODRPR = :CODRPR WHERE CODCOLIGADA = :CODCOLIGADA AND CODCOLCFO = :CODCOLCFO AND CODCFO = :CODCFO";
                        //    this.DBS.SkipControlColumns = true;
                        //    DBS.QueryExec(sSql, clifor.CodRpr, 1, clifor.CodColigada, clifor.CodCfo);
                        //    this.DBS.SkipControlColumns = false;
                        //}
                        //else
                        //{
                        //    sSql = @"INSERT INTO FCFODEF (CODCOLIGADA, CODCFO, CODCOLCFO, CODRPR) VALUES (:CODCOLIGADA, :CODCFO, :CODCOLCFO, :CODRPR)";
                        //    this.DBS.SkipControlColumns = true;
                        //    DBS.QueryExec(sSql, 1, clifor.CodCfo, clifor.CodColigada, clifor.CodRpr);
                        //    this.DBS.SkipControlColumns = false;
                        //}

                        if (Convert.ToInt32(conn.ExecGetField(0, @"SELECT COUNT(CODCFO) FROM FCFODEF WHERE CODCOLIGADA = ? AND CODCOLCFO = ? AND CODCFO = ?", new object[] { 1, clifor.CodColigada, clifor.CodCfo })) > 0)
                        {
                            sSql = @"UPDATE FCFODEF SET CODRPR = ? WHERE CODCOLIGADA = ? AND CODCOLCFO = ? AND CODCFO = ?";
                            conn.ExecTransaction(sSql, clifor.CodRpr, 1, clifor.CodColigada, clifor.CodCfo);
                        }
                        else
                        {
                            sSql = @"INSERT INTO FCFODEF (CODCOLIGADA, CODCFO, CODCOLCFO, CODRPR) VALUES (?, ?, ?, ?)";
                            conn.ExecTransaction(sSql, 1, clifor.CodCfo, clifor.CodColigada, clifor.CodRpr);
                        }

                        //DBS.Commit();
                        conn.Commit();
                        msg.Retorno = clifor.CodCfo;
                        msg.Mensagem = "Alteração realizada com sucesso.";

                        #endregion
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                //DBS.Rollback();
                conn.Rollback();
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                msg.Retorno = 0;
                msg.Mensagem = sPoint + err;
                //msg.Mensagem = ex.ToString();
            }

            return msg;
        }

        private bool ValidaStatus(int CodColigada, int IdMov)
        {
            bool status = Convert.ToBoolean(AppLib.Context.poolConnection.Get().ExecGetField(false, "SELECT COUNT(IDMOV) FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ? AND STATUS = 'A'", new object[] { CodColigada, IdMov }));

            return status;
        }

        private bool ExisteMovimento(int CodColigada, int IdMov)
        {
            try
            {
                bool mov = Convert.ToBoolean(AppLib.Context.poolConnection.Get().ExecGetField(false, "SELECT Count(IDMOV) FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { CodColigada, IdMov }));

                return mov;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
#endregion
#endregion