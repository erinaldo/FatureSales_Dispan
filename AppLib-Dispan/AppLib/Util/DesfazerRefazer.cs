using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Util
{
    public class DesfazerRefazer
    {
        public List<String> AcaoDesfazer = new List<String>();
        public List<Object> ListaDesfazer = new List<Object>();

        public List<String> AcaoRefazer = new List<String>();
        public List<Object> ListaRefazer = new List<Object>();

        public void NovaAcao(String NomeAcao, Object ItemLista)
        {
            AcaoDesfazer.Add(NomeAcao);
            ListaDesfazer.Add(ItemLista);

            AcaoRefazer.Clear();
            ListaRefazer.Clear();
        }

        public Object Desfazer()
        {
            String Acao = AcaoDesfazer[AcaoDesfazer.Count - 1];
            Object Item = ListaDesfazer[ListaDesfazer.Count - 1];

            AcaoDesfazer.RemoveAt(AcaoDesfazer.Count - 1);
            ListaDesfazer.RemoveAt(ListaDesfazer.Count - 1);

            AcaoRefazer.Add(Acao);
            ListaRefazer.Add(Item);

            return Item;
        }

        public Object Refazer()
        {
            String Acao = AcaoRefazer[AcaoRefazer.Count - 1];
            Object Item = ListaRefazer[ListaRefazer.Count - 1];

            AcaoRefazer.RemoveAt(AcaoRefazer.Count - 1);
            ListaRefazer.RemoveAt(ListaRefazer.Count - 1);

            AcaoDesfazer.Add(Acao);
            ListaDesfazer.Add(Item);

            return Item;
        }
    }
}
