using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class CampoHora : UserControl
    {
        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Valor padrão")]
        public DateTime? Default { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        public CampoHora()
        {
            InitializeComponent();
            Query = 0;
            Edita = true;
        }

        private void CampoHora_Load(object sender, EventArgs e)
        {
            timeEdit1.Properties.ReadOnly = !Edita;
        }

        private void CampoHora_Resize(object sender, EventArgs e)
        {
            timeEdit1.Width = (this.Width - 32) - 3;
        }

        public DateTime? Get()
        {
            if (timeEdit1.Text.Equals("00:00"))
            {
                return null;
            }
            else
            {
                return timeEdit1.Time;
            }
        }

        public void Set(DateTime? valor)
        {
            if (valor == null)
            {
                timeEdit1.EditValue = new DateTime(1900, 01, 01, 00, 00, 00);
            }
            else
            {
                if (valor.Equals(""))
                {
                    timeEdit1.EditValue = new DateTime(1900, 01, 01, 00, 00, 00);
                }
                else
                {
                    timeEdit1.EditValue = new DateTime(1900, 01, 01, valor.Value.Hour, valor.Value.Minute, valor.Value.Second);
                }
            }
        }

    }
}
