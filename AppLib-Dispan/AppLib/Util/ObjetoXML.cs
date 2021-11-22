using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Util
{
    public class ObjetoXML
    {
        public String Escrever(Object Objeto)
        {
            System.IO.StringWriter Escritor = new System.IO.StringWriter();
            System.Xml.Serialization.XmlSerializer Serializador = new System.Xml.Serialization.XmlSerializer(Objeto.GetType());
            Serializador.Serialize(Escritor, Objeto);
            return Escritor.ToString();
        }

        public Object Ler(String XML, Object Objeto)
        {
            System.IO.StringReader Leitor = new System.IO.StringReader(XML);
            System.Xml.Serialization.XmlSerializer Serializador = new System.Xml.Serialization.XmlSerializer(Objeto.GetType());
            return Serializador.Deserialize(Leitor);
        }

        public Object Ler(String XML, Object[] Objeto)
        {
            System.IO.StringReader Leitor = new System.IO.StringReader(XML);
            System.Xml.Serialization.XmlSerializer Serializador = new System.Xml.Serialization.XmlSerializer(Objeto.GetType());
            return Serializador.Deserialize(Leitor);
        }
    }
}
