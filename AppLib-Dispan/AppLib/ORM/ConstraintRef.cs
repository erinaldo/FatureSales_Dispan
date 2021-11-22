using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.ORM
{
    public class ConstraintRef
    {
        public String NomeConstraint { get; set; }

        public String TabelaPK { get; set; }
        public String ColunaPK { get; set; }

        public String TabelaFK { get; set; }
        public String ColunaFK { get; set; }
    }
}
