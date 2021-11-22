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
    public partial class CampoTexto : UserControl
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Valor padrão")]
        public String Default { get; set; }

        [Category("_APP"), Description("Número Máximo de Caracteres Permitido")]
        public int? MaximoCaracteres { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }
        
        #endregion

        #region CONSTRUTOR

        public CampoTexto()
        {
            InitializeComponent();

            Query = 0;

            if (MaximoCaracteres != null)
            {
                textEdit1.Properties.MaxLength = (int)MaximoCaracteres;
            }

            Edita = true;
        }
        
        #endregion

        #region EVENTOS

        private void CampoTexto_Load(object sender, EventArgs e)
        {
            textEdit1.Properties.ReadOnly = !Edita;
        }

        private void CampoTexto_Resize(object sender, EventArgs e)
        {
            textEdit1.Height = this.Height;
            textEdit1.Width = this.Width;
        }
        
        #endregion

        #region MÉTODOS

        public String Get()
        {
            if (textEdit1.Text.Equals(""))
            {
                // ANTIGO -> return String.Empty; (DESATIVADO PARA PADRONIZAR PARA NULL)
                return null;
            }
            else
            {
                return textEdit1.Text;
            }
        }

        public void Set(String valor)
        {
            textEdit1.Text = valor;
        }
        
        #endregion

    }
}
