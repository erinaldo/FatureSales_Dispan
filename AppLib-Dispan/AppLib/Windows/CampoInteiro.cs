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
    public partial class CampoInteiro : UserControl
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Valor padrão")]
        public int? Default { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }
        
        #endregion

        #region CONSTRUTOR

        public CampoInteiro()
        {
            InitializeComponent();

            textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textEdit1.Properties.Mask.EditMask = "d";
            Query = 0;
            Edita = true;
        }
        
        #endregion

        #region EVENTOS

        private void CampoInteiro_Load(object sender, EventArgs e)
        {
            textEdit1.Properties.ReadOnly = !Edita;
        }

        private void CampoInteiro_Resize(object sender, EventArgs e)
        {
            textEdit1.Height = this.Height;
            textEdit1.Width = this.Width;
        }        
        
        #endregion

        #region MÉTODOS

        public int? Get()
        {
            if (textEdit1.Text.Equals(""))
            {
                return null;
            }
            else
            {
                return int.Parse(textEdit1.Text);
            }
        }

        public void Set(int? valor)
        {
            textEdit1.Text = valor.ToString();
        }
        
        #endregion
        
    }
}
