using System;
using System.Collections.Generic;
using System.Text;

namespace AppInterop
{
    [Serializable]
    public class FinCliForPar
    {
        //
        public int? Ativo;
        public string Bairro;
        public string BairroEntrega;
        public string BairroPgto;
        public bool? Bloqueado;
        public string CaixaPostal;
        public string CaixaPostalEntrega;
        public string CaixaPostalPagamento;
        public int? CalculaAVP;
        public string CampoAlfaOp1;
        public string CampoAlfaOp2;
        public string CampoAlfaOp3;
        public string CampoLivre;
        //public Dictionary<string, object> CamposComplementares;
        public int? CategoriaAutonomo;
        public string CBOAutonomo;
        public string CEP;
        public string CEPCaixaPostal;
        public string CEPEntrega;
        public string CEPPgto;
        public string CGCCFO;
        public string CI_Orgao;
        public string CI_UF;
        public string CIAutonomo;
        public string Cidade;
        public string CidadeEntrega;
        public string CidadePgto;
        public string CIdentidade;
        public string CodCfo;
        public string CodCfo_Old;
        public int? CodColCfoFiscal;
        public int? CodColContaGer;
        public int? CodColigada;
        public int? CodCOLTCF;
        public string CodContaGer;
        public string CODETD;
        public string CODETDEntrega;
        public string CODETDPgto;
        public string CodMunicipio;
        public string CodMunicipioEntrega;
        public string CodMunicipioPgto;
        public string CodPagtoGPS;
        public string CodReceita;
        public string CODTCF;
        public string CodUsuarioAcesso;
        public int ColigadaCorrente;
        public string Complemento;
        public string ComplementoPgto;
        public string ComplemEntrega;
        public string Contato;
        public string ContatoEntrega;
        public string ContatoPgto;
        public string ContEventoContab;
        public bool? Contribuinte;
        public bool? ContribuinteISS;
        public DateTime DataAlteracao;
        public DateTime DataCriacao;
        public DateTime? DataOp1;
        public DateTime? DataOp2;
        public DateTime? DataOp3;
        public DateTime? DtInicAtividades;
        public DateTime? DTNascimento;
        public string Email;
        public string EmailEntrega;
        public string EmailFiscal;
        public string EmailPgto;
        public string EstadoCivil;
        public string FAX;
        public string FAXEntrega;
        public string FAXPgto;        
        public int IdCfo;
        public int? IdCfoFiscal;
        public string IdIntegracao;
        public int? IDPais;
        public int? IDPaisEntrega;
        public int? IDPaisPgto;
        public string InscrEstadual;
        public string InscrMunicipal;
        public decimal? LimiteCredito;
        public int LinhaTXT;
        public string NIF;
        public string NIT;
        public string Nome;
        public string NomeFantasia;
        public int? NumDependentes;
        public int? NumDiasAtraso;
        public string Numero;
        public string NumeroEntrega;
        public string NumeroPgto;
        public int? NumFuncionarios;
        public bool? OptantePeloSimples;
        public decimal? Patrimonio;
        public string Rua;
        public string RuaEntrega;
        public string RuaPgto;
        public string Suframa;
        public string Telefone;
        public string TelefoneComercial;
        public string TelefoneEntrega;
        public string TelefonePgto;
        public string Telex;
        public int? TipoBairro;
        public int? TipoBairroEntrega;
        public int? TipoBairroPgto;
        public int? TipoRua;
        public int? TipoRuaEntrega;
        public int? TipoRuaPgto;
        public string UsuarioAlteracao;
        public string UsuarioCriacao;
        public decimal? ValFrete;
        public decimal? ValorOp1;
        public decimal? ValorOp2;
        public decimal? ValorOp3;
        public decimal? VROutrasDeducoesIRRF;

        public int? RamoAtiv;

        //Para cada campo complementar
        public object HISTORICOATUAL;
        public object HISTORICOCFO;

        //public FinCFOFormaTributacaoEnum? FormaTributacao;
        //public FinCFONacionalidadeEnum? Nacionalidade;
        //public FinCFOOrgaoPublicoEnum? OrgaoPublico;
        //public FinCFOCliForEnum? PagRec;
        //public FinCFOPessoaFisJurEnum? PessoaFisOuJur;
        //public FinCFOPorteEnum? Porte;
        //public FinCFORamoAtivEnum? RamoAtiv;
        //public FinCFORegimeISSEnum? RegimeISS;
        //public FinCFORetencaoISSEnum? RetencaoISS;
        //public FinCFOSituacaoNifEnum? SituacaoNIF;
        //public FinCFOTipoOPCombustivelEnum? TipoOpCombustivel;        
        //public FinCFOTipoRendimentoEnum? TipoRendimento;
        //public FinCFOTipoTomadorEnum? TPTomador;

        //Opções:
        //"1", "Cliente"
        //"2", "Fornecedor"
        //"3", "Ambos"
        public int PagRec;

        //Opções:
        //"F","PessoaFisica"
        //"J","PessoaJurídica"
        public string PessoaFisOuJur;

        public string CodRpr;
        public string ContribuinteICMS;
        public string Nacionalidade;
        public string DocumentosEstrangeiro;

        public FinCliForPar()
        {
        
        }
    }
}
