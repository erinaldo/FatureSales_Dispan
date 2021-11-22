using System;
using System.Collections.Generic;
using System.Web;

namespace AppInterop
{
    [Serializable]
    public class MovMovimentoPar
    {
        public string GuidId;
        public int CodColigada;
        public string CodSistemaLogado;
        public string CodUsuarioLogado;
        public int? IdExercicioFiscal;
        public int IdMov;

        public decimal AbatimentoICMS;
        public bool? Apropriado;
        public string CampoLivre1;
        public string CampoLivre2;
        public string CampoLivre3;
        public string ChapaResp;
        public string ChaveAcessoNFe;
        public string ClasseConsumo;
        public int? CodAgendamento;
        public string CodCCusto;
        public string CodCCustoDestino;
        public string CodCfo;
        public string CodCfoAux;
        public string CodCfoNatureza;
        public string CodCfoOrigem;
        public string CodCfoTransFat;
        public int? CodColCfo;
        public int CodColCfoAux;
        public int? CodColCfoNatureza;
        public string CodColCfoOrigem;
        public int? CodColCfoTransFat;
        public int? CodColCxa;
        public string CodCondicaoPagamento;
        public string CodCxa;
        public string CodDepartamento;
        public string CodDeptoDestino;
        public string CodDiario;
        public string CodEntrega;
        public string CodEtdMunServ;
        public string CodEtdPlaca;
        public int? CodEvento;
        public string CodFaixaEntrega;
        public int CodFilial;
        public short? CodFilialDestino;
        public short? CodFilialEntrega;
        public string CodigoIRRF;
        public string CodigoServico;
        public string CodLancamentoFiscal;
        public string CodLoc;
        public string CodLocalExpedicao;
        public string CodLocDestino;
        public string CodLocEntrega;
        public int? CodLote;
        public string CodMen;
        public string CodMen2;
        public string CodMenDesconto;
        public string CodMenDespesa;
        public string CodMenFrete;
        public string CodMoeLancamento;
        public string CodMoeValorLiquido;
        public string CodMunServico;
        public string CodRepresentante;
        public string CodTb1Fat;

        //CodTb1Flx
        public string CodTb1Opcional;
        public string CodTb2Fat;
        
        //"CodTb2Flx
        public string CodTb2Opcional;
        public string CodTb3Fat;

        //CodTb3Flx
        public string CodTb3Opcional;
        public string CodTb4Fat;

        //CodTb4Flx
        public string CodTb4Opcional;
        public string CodTb5Fat;

        //CodTb5Flx
        public string CodTb5Opcional;
        public string CodTipoDocumento;
        public string CodTMv;
        public string CodTra;
        public string CodTra2;
        public string CodUsuario;
        public string CodUsuarioAprovaDesc;
        public string CodVen1;
        public string CodVen2;
        public string CodVen3;
        public string CodVen4;
        public string CodViaTransporte;
        public decimal ComissaoRepres;
        public int? ContabilizadoPorTotal;
        public int? ContReinicioOperacao;
        public DateTime? DataBaseMov;
        public DateTime? DataCancelamentoMov;
        public DateTime? DataContabilizacao;
        public DateTime? DataCriacao;
        public DateTime? DataDeducao;
        public DateTime? DataEmissao;
        public DateTime? DataEntrega;
        public DateTime? DataExtra1;
        public DateTime? DataExtra2;
        public DateTime? DataFechamento;
        public DateTime? DataHoraEntrega;
        public DateTime? DataLancamento;
        public DateTime DataMovimento;
        public DateTime? DataProcessamento;
        public DateTime? DataProgramacao;
        public DateTime? DataProgramacaoAnt;
        public DateTime? DataRetorno;
        public DateTime? DataSaida;
        public decimal? DeducaoIRRF;
        public int? Distancia;
        public bool DocImpresso;
        public bool EmailEnviado;
        public string EmiteBoleta;
        public string Especie;
        public int? Extenporaneo;
        public string Fase;
        public bool FatImpressa;
        public short? FlagAgrupadoFluxus;
        public int? FlagEfeitoSaldo;
        public int? FlagExporFazenda;
        public int? FlagExporFisc;
        public int? FlagExportacao;
        public string FlagImpressaoFat;
        public bool FlagProcessado;
        public string FormaCalculo;
        public int? FreteCIFouFOB;
        public bool GeradoPorContaTrabalho;
        public bool GeradoPorLote;
        public bool GerouContaTrabalho;
        public bool GerouFatura;
        public string GrupoTensao;
        public string HistoricoCurto;
        public string HistoricoLongo;
        public DateTime? HorarioEmissao;
        public DateTime? HoraSaida;
        public DateTime? HoraUltimaAlteracao;
        public short? IdAutorizacoes;
        public int? IdCeiCfo;
        public int? IdClassMov;
        public int? IdContatoCobranca;
        public int? IdContatoEntrega;
        public string IdIntegracao;
        public int? IdLot;
        public int? IdLoteProcesso;
        public int? IdLoteProcessoReFat;
        public int? IdMovCfo;
        public int? IdMovCTRC;
        public int? IdMovLctFluxus;
        public int? IdMovPedDesdobrado;
        public int? IdMovRelac;
        public int? IdNat;
        public int? IdNat2;
        public int? IdNatFrete;
        public string IdObjeto;
        public int? IdProjeto;
        public int? IdReducaoZ;
        public int? IdSaldoEstoque;
        public int? IdXmlTotvsColab;
        public decimal IndicaUsoObjeto;
        public string IntegraAplicacao;
        public short? IntegradoAutomacao;
        public bool IntegradoBonum;
        public int? ItensAgrupados;
        public bool LancamentoContabilExcluido;
        public string LoteEscrituracaoEntrada;
        public string Marca;
        public bool MovImpresso;
        public string NaoNumerado;
        public string NaturezaVolumes;
        public int? NSeqDataFechamento;
        public int? Numero;
        public int? NumeroCaixa;
        public int? NumeroCupom;
        public int? NumeroLctAberto;
        public int? NumeroLctGerado;
        public string NumeroMov;
        public string NumeroOrdem;
        public string NumeroRecibo;
        public string NumeroReciboNF;
        public int? NumeroTributos;
        public string Observacao;
        public int? OcAutonomo;
        public object OwnerData;
        public decimal? PercBaseINSSEmpregado;
        public decimal PercComissao;
        public decimal PercComissaoVen2;
        public decimal? PercentBaseINSS;
        public decimal? PercentualDesc;
        public decimal? PercentualDesp;
        public decimal? PercentualExtra1;
        public decimal? PercentualExtra2;
        public decimal? PercentualFrete;
        public decimal? PercentualSeguro;
        public decimal PesoBruto;
        public decimal PesoLiquido;
        public string Placa;
        public string PontoVenda;
        public string PrazoFabricacao;
        public int? PrazoEntrega;
        public int? Quantidade;
        public string SegundoNumero;
        public string SeqDiario;
        public string SeqDiarioEstorno;
        public int? SequencialEstoque;
        public string Serie;
        public string SerieRecibo;
        public int? SituacaoRecibo;
        public string Status;
        public int? StatusCheque;
        public string StatusCompras;
        public short? StatusExportCont;
        public int? StatusLiberacao;
        public string StatusRecibo;
        public string StatusSeparacao;
        public string SubSerie;
        public string Tipo;
        public string TipoAssinante;
        public int? TipoConsumo;
        public int? TipoRecibo;
        public string TipoUtilizacao;
        public string UnidadeCalculoFrete;
        public int? UsaDespFinanc;
        public bool UsaRateioValorFin;
        public string UsuarioCriacao;
        public decimal? ValorAdiantamento;
        public decimal ValorBruto;
        public decimal ValorBrutoInterno;
        public decimal? ValorDesc;
        public decimal ValorDescCondicional;
        public decimal? ValorDesp;
        public decimal ValorDespCondicional;
        public decimal? ValorExtra1;
        public decimal? ValorExtra2;
        public decimal? ValorFrete;
        public decimal ValorLiquido;
        public decimal ValorLiquidoInformado;
        public decimal ValorMercadorias;
        public decimal ValorOutros;
        public decimal? ValorRecebido;
        public decimal? ValorSeguro;
        public decimal? ValorServico;
        public string ViaDeTransporte;
        public short? VinculadoEstFrenteLoja;
        public decimal? VlrDespacho;
        public decimal? VlrFreteOutros;
        public decimal? VlrPedagio;
        public decimal? VlrseCCAT;
        public string Volumes;
        public decimal VrBaseINSSOutraEmpresa;
        //Para cada campo complementar
        public object MSGFORNEC;
        public object CONDPGTO;
        public object TPCULTURA;
        public object FINANCIADO;
        public object DEMONSBRINDE;
        public object PARAFINANC;
        public object CLASSREQ;
        public object CLASSREQCPL;
        public object TIPOVENDA;
        public object APLICPROD;
        public object PRDPADRAO;
        public object OBSPRDPADRAO;
        public string CODPRDREVENDA { get; set; }
        public string DESCRICAOPRDREVENDA { get; set; }

        public int NCONTRIB;
        public string UFORCAMENTO;
        public string NOMECLIENTEORC;

        public string CONTATOCOM;
        public string TELEFONECOM;
        public string EMAILCOM;

        public decimal VALORDESCRAT { get; set; }
        public decimal VALORDESPRAT { get; set; }
        public int TIPODESC { get; set; }

        public int SEMIMPOSTOS { get; set; }
        public int POSSUIBENEFICIO { get; set; }







        //public Dictionary<string, object> CamposComplementares;

        public List<MovMovimentoItemPar> ItemMovimento;

        //public ObjectList<MovMovDispositivoSegPar> DispositivoSeg;
        //public ObjectList<MovMovFiscalPar> Fiscal;
        //public MovMovCtbLancamentoContabilPar LancamentoContabilPar;
        //public ObjectList<MovMovMovRelacExpPar> MovRelacExportacao;
        //public ObjectList<MovMovMovRelacPar> MovRelacionado;
        //public ObjectList<MovMovMovRelacPar> MovRelacionadoCopiaRef;
        //public ObjectList<MovVinculoMovPar> MovVinculoMov;
        //public ObjectList<MovMovNFEletronicaPar> NFEletronica;
        //public ObjectList<MovMovNFTerceirosPar> NFTerceiros;
        //public ObjectList<MovMovNormaPar> Norma;
        //public ObjectList<MovMovOrdemServicoPar> OrdemServico;
        //public ObjectList<MovMovPagtoPar> Pagamentos;
        //public ObjectList<MovMovProcessoJudicialPar> ProcessoJudicial;
        //public ObjectList<MovMovRatCCuPar> RateioCCu;
        //public ObjectList<MovMovRatDepPar> RateioDep;
        //public ObjectList<MovMovTrbItmAgrupadoPar> TrbItmAgrupado;
        //public ObjectList<MovMovTributoPar> TributosMov;
        //public ObjectList<MovMovComponenteCargaPar> ComponenteCarga;
        //public ObjectList<MovMovCTRCEntradaPar> CTRCEntrada;
        //public ObjectList<MovMovCTRCMovPar> CTRCMov;
        //public ObjectList<MovMovCTRCSaidaPar> CTRCSaida;
        //public ObjectList<MovMovDadosTranspPar> DadosTransp;

        public MovMovimentoPar()
        {
            ItemMovimento = new List<MovMovimentoItemPar>();        
        }
    }
}