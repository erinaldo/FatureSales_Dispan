using System;
using System.Collections.Generic;
using System.Web;

namespace AppInterop
{
    [Serializable]
    public class MovMovimentoItemPar
    {
        public int CodColigada;
        public int NSeqItmMov;
        public int NumeroSequencial;
        public int IdPrd;
        public int? IdPrdComposto;
        public string CodUnd;
        public decimal Quantidade;
        public decimal? PrecoUnitario;
        public decimal? AliquotaIPI;
        public string HistoricoLongo;

        //Para cada campo complementar
        public object PRODPRICIPAL;
        public object SEQUENCIAL;
        public object TIPOMAT;
        public object PRDPADRAO;
        public object OBSPRDPADRAO;
        public string PRDREVENDA;
        public string PRECOUNITARIOSELEC;
        public DateTime? DATAENTREGA;

        public int? IDNAT { get; set; }
        public string APLICAPROD;
        public String NUMPEDIDO { get; set; }
        public String NUMITEMPEDIDO { get; set; }
        public String TIPOINOX { get; set; }

        public decimal PRECOTABELA { get; set; }

        //public Dictionary<string, object> CamposComplementares;

        /*
        public int IdMov;
        public decimal AliqOrdenacao;
        public int? Block;
        public string CampoLivre;
        public string Chapa;
        public string CodCCusto;
        public int? CodColTbOrcamento;
        public string CodCondicaoPagamento;
        public string CodDepartamento;
        public string CodEtdMunServ;
        public int? CodFilial;
        public string CodigoCodif;
        public string CodigoServico;
        public string CodLoc;
        public string CodLocalBem;
        public string CodMen;
        public string CodMunServico;
        public string CodNat;
        public int? CodPublic;
        public string CodRepresentante;
        public string CodSituacaoTributaria;
        public string CodTb1Fat;

        //CodTb1Flx
        public string CodTb1Opcional;
        public string CodTb2Fat;

        //CodTb2Flx
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
        public string CodTbGrupoOrc;
        public string CodTbOrcamento;
        public string CodTipoProduto;
        public string CodVen1;
        public decimal ComissaoRepres;
        public DateTime? DataEmissao;
        public DateTime? DataEntrega;
        public decimal? DataFatDec;
        public DateTime? DataFimFat;
        public DateTime? DataIniFat;
        public DateTime? DataOrcamento;
        public DateTime? DataRegistroExport;
        public decimal FatorConvUnd;
        public int Flag;
        public int? FlagEfeitoSaldo;
        public int? FlagReFaturamento;
        public int? FlagRepasse;
        public string HistoricoCurto;
        public string HistoricoLongo;
        public int? IdClassifEnergiaComunic;
        public int? IdContrato;
        public int? IdContratoDestino;
        public int? IdGrd;
        public string IdIntegracao;
        public int? IdNat;
        public string IdObjetoOficina;
        public int? IdProjeto;
        public int? IdTabPreco;
        public int? IdTarefa;
        public int? ImprimeMov;
        public string IndiceNCM;
        public DateTime? Inicio;
        public string NCM;
        public int NumeroSequencial;
        public int? NumeroTributos;
        public int? NumSeqItemContrato;
        public int? NumSeqItemContratoDest;
        public int? NumSeqItemContratoMedicao;
        public decimal PercentComissao;
        public decimal? PercentualDesc;
        public decimal? PercentualDesp;
        public string Prateleira;
        public bool PrecoEditado;
        public decimal PrecoTabela;
        public int? PrecoTotalEditado;
        public DateTime? PrevInicio;
        public int ProdutoSubstituto;
        public short? QtdeVolumeUnitario;
        public decimal? QtdUndPedido;
        public decimal QuantidadeAReceber;
        public decimal QuantidadeOriginal;
        public decimal QuantidadeSeparada;
        public string RegistroExportacao;
        public string Status;
        public DateTime? Termino;
        public int? TipoCodigoPrd;
        public int TipoPrecoSelecionado;
        public string TributacaoECF;
        public decimal ValorBrutoItem;
        public decimal ValorBrutoItemOrig;
        public string ValorCodigoPrd;
        public decimal? ValorDesc;
        public decimal ValorDescCondiconalItm;
        public decimal? ValorDesp;
        public decimal ValorDespCondicionalItm;
        public decimal ValorEscrituracao;
        public decimal ValorFinanceiro;
        public decimal? ValorFinancGerencial;
        public decimal ValorFinPedido;
        public decimal ValorOpFrm1;
        public decimal ValorOpFrm2;
        public decimal ValorTotalItem;
        public decimal ValorUnitario;
        public decimal? ValorUnitGerencial;
        public decimal ValorUntOrcamento;
        public decimal ValServicoNFe;
        */

        //public ObjectList<MovMovItemBemPar> ItemBem;
        //public ObjectList<MovMovItemGradePar> ItemGrade;
        //public ObjectList<MovMovItemLotePar> ItemLote;
        //public ObjectList<MovMovItemMovFiscalPar> ItemMovFiscal;
        //public ObjectList<MovMovItemNumSeriePar> ItemNumSerie;
        //public ObjectList<MovMovItemOrdemServicoPar> ItemOrdemServico;
        //public ObjectList<MovMovItemRelacExpPar> ItemRelacExportacao;
        //public ObjectList<MovMovItemRelacPar> ItemRelacionado;
        //public ObjectList<MovMovItemRelacPar> ItemRelacionadoCopiaRef;
        //public ObjectList<MovMovItemRatCCuPar> RateioCCu;
        //public ObjectList<MovMovItemRatDepPar> RateioDep;
        //public ObjectList<MovMovReservaMovPar> ReservasMov;
        //public ObjectList<MovMovTributoPar> TributosItemMov;
        //public ObjectList<MovVincMovRelacPar> VincMovRelac;

        public MovMovimentoItemPar()
        { 
        
        }
    }
}