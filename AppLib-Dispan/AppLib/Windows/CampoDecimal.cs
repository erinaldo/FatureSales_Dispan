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
    public partial class CampoDecimal : UserControl
    {

        #region ATRIBUTOS

        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Valor padrão")]
        public decimal? Default { get; set; }

        [Category("_APP"), Description("Número de casas após a vírgula")]
        public int Decimais { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        #endregion

        #region CONSTRUTOR

        public CampoDecimal()
        {
            InitializeComponent();

            Query = 0;            
            Decimais = 2;
            Edita = true;
        }
        
        #endregion

        #region EVENTOS

        private void CampoDecimal_Load(object sender, EventArgs e)
        {
            textEdit1.Properties.ReadOnly = !Edita;
        }

        private void CampoDecimal_Resize(object sender, EventArgs e)
        {
            textEdit1.Height = this.Height;
            textEdit1.Width = this.Width;
        }

        private void textEdit1_Enter(object sender, EventArgs e)
        {
            textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            textEdit1.Properties.Mask.EditMask = "\\d{0,9}";

            if (Decimais > 0)
            {
                textEdit1.Properties.Mask.EditMask += ",";
            }

            for (int i = 0; i < Decimais; i++)
            {
                textEdit1.Properties.Mask.EditMask += "\\d";
            }

            textEdit1.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
        }
        
        #endregion

        #region MÉTODOS

        public decimal? Get()
        {
            if (textEdit1.Text.Equals(""))
            {
                return null;
            }
            else
            {
                String temp1 = textEdit1.Text;
                decimal temp2 = decimal.Parse(temp1); //, System.Globalization.NumberStyles.Float);

                return temp2;
            }
        }

        public void Set(decimal? valor)
        {
            if ( valor == null )
            {
                textEdit1.Text = "";
            }
            else
            {
                decimal temp1 = (decimal)valor;
                String formato = "0";

                if (Decimais > 0)
                {
                    formato += ".";
                }

                for (int i = 0; i < Decimais; i++)
                {
                    formato += "0";
                }

                textEdit1.Text = temp1.ToString(formato);
            }
        }
       
        #endregion

    }
}
