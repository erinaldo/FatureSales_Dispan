using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppInterop
{
    [Serializable]
    public class MovCancelamentoPar
    {
        public int CodColigada;
        public string CodSistemaLogado;
        public string CodUsuarioLogado;
        public int? IdExercicioFiscal;
        public int IdMov;

        public bool ApagarMovRelac;
        public DateTime DataCancelamento;
        public string MotivoCancelamento;

        public MovCancelamentoPar()
        {
        
        }
    }
}
