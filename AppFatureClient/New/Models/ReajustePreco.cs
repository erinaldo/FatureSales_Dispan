using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFatureClient.New.Models
{
    public class ReajustePreco
    {
        public int CodColigada { get; set; }

        public int IDPrd { get; set; }

        public decimal Preco { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string UsuarioAlteracao { get; set; }
    }
}
