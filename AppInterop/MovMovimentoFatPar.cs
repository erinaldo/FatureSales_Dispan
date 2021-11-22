using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInterop
{
    [Serializable]
    public class MovMovimentoFatPar
    {
        public int CodColigada;
        public string CodSistema;
        public string CodTmvDestino;
        public string CodTmvOrigem;
        public string CodUsuario;
        public DateTime dataBase;
        public DateTime? dataEmissao;
        public int efeitoPedidoFatAutomatico;
        public string GrupoFaturamento;
        public int IdExercicioFiscal;
        public List<int> IdMov;
        public string numeroMov;
        public bool realizaBaixaPedido;
        public int TipoFaturamento;
        public string NumeroOrdem;

        public List<MovMovimentoItemFatPar> listaMovItemFatAutomatico;

        public MovMovimentoFatPar()
        { 
        
        }
    }
}