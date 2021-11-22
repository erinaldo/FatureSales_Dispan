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
    public partial class CampoDataHora : UserControl
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

        public CampoDataHora()
        {
            InitializeComponent();
            Query = 0;
            Edita = true;
        }

        private void CampoDataHora_Load(object sender, EventArgs e)
        {
            textEdit1.Properties.ReadOnly = !Edita;
        }
        
        private Boolean Validar()
        {
            try
            {
                DateTime temp = DateTime.Parse(textEdit1.Text);
                return true;
            }
            catch { }

            return false;
        }

        public DateTime? Get()
        {
            if (textEdit1 == null)
            {
                return null;
            }
            else
            {
                if (textEdit1.Text.Equals(""))
                {
                    return null;
                }
                else
                {
                    if (this.Validar())
                    {
                        return DateTime.Parse(textEdit1.Text);
                    }
                    else
                    {
                        // incluir aqui uma mensagem de erro se for necessário
                        return null;
                    }
                }
            }
        }

        public void Set(DateTime? valor)
        {
            if (valor == null)
            {
                textEdit1.Text = "";
            }
            else
            {
                textEdit1.EditValue = valor;
            }
        }

        private void CampoDataHora_Resize(object sender, EventArgs e)
        {
            textEdit1.Height = this.Height;
            textEdit1.Width = this.Width;
        }


    }
}
