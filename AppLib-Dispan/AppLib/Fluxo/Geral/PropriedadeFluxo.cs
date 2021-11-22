using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class PropriedadeFluxo
    {
        public String Biblioteca { get; set; }
        public String Classe { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public String TipoRetorno { get; set; }

        public List<VariaveisFluxo> Variaveis = new List<VariaveisFluxo>();

        public int Contador { get; set; }

        public void Editar()
        {
            FormPropriedadeFluxo f = new FormPropriedadeFluxo(this);
            f.ShowDialog();
        }

        public PropriedadeFluxo Copiar()
        {
            PropriedadeFluxo objDestino = new PropriedadeFluxo();

            objDestino.Biblioteca = this.Biblioteca;
            objDestino.Classe = this.Classe;
            objDestino.Nome = this.Nome;
            objDestino.Descricao = this.Descricao;
            objDestino.TipoRetorno = this.TipoRetorno;

            objDestino.Contador = this.Contador;

            objDestino.Variaveis.Clear();

            for (int i = 0; i < this.Variaveis.Count; i++)
            {
                objDestino.Variaveis.Add(null);
                objDestino.Variaveis[i] = this.Variaveis[i].Copiar();
            }

            return objDestino;
        }
    }
}
