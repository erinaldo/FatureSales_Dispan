using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFatureClient
{
    public class ProdutoNivel
    {
        public object ValueMember { get; set; }
        public object DisplayMember { get; set; }

        public ProdutoNivel()
        {

        }

        public ProdutoNivel(object value, object display)
        {
            this.ValueMember = value;
            this.DisplayMember = display;
        }
    }
}
