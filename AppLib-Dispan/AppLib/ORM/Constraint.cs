using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.ORM
{
    public class Constraint
    {
        public String NomeConstraint { get; set; }
        public String TabelaPK { get; set; }
        public String TabelaFK { get; set; }

        public List<ConstraintRef> ConstraintRef = new List<ConstraintRef>();
    }
}
