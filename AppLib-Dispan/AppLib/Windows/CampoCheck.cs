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
    public partial class CampoCheck : UserControl
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Rótulo")]
        public String Rotulo { get; set; }

        [Category("_APP"), Description("Valor padrão")]
        public String Default { get; set; }

        [Category("_APP"), Description("Valor Verdadeiro")]
        public String ValorVerdadeiro { get; set; }

        [Category("_APP"), Description("Valor Falso")]
        public String ValorFalso { get; set; }

        [Category("_APP"), Description("Tipo de campo check")]
        public Global.Types.TipoCampoCheck TipoCampoCheck { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        [Category("_APP"), Description("Situação do campo")]
        public Boolean carregando = false;
        
        #endregion

        #region CONSTRUTOR

        public CampoCheck()
        {
            InitializeComponent();
            Query = 0;
            ValorVerdadeiro = "1";
            ValorFalso = "0";
            Default = "0";
            Edita = true;
        }
        
        #endregion

        #region EVENTOS

        private void CampoCheck_Load(object sender, EventArgs e)
        {
            checkEdit1.Text = Rotulo;
            checkEdit1.Properties.ReadOnly = !Edita;
        }

        private void CampoCheck_Resize(object sender, EventArgs e)
        {
            checkEdit1.Width = this.Width;
        }
        
        #endregion

        #region MÉTODOS

        public Object Get()
        {
            if (TipoCampoCheck == Global.Types.TipoCampoCheck.Booleano)
            {
                if (checkEdit1.Checked)
                {
                    return int.Parse(ValorVerdadeiro);
                }
                else
                {
                    return int.Parse(ValorFalso);
                }
            }

            if (TipoCampoCheck == Global.Types.TipoCampoCheck.Inteiro)
            {
                if (checkEdit1.Checked)
                {
                    return int.Parse(ValorVerdadeiro);
                }
                else
                {
                    return int.Parse(ValorFalso);
                }
            }

            if (TipoCampoCheck == Global.Types.TipoCampoCheck.String)
            {
                if (checkEdit1.Checked)
                {
                    return ValorVerdadeiro;
                }
                else
                {
                    return ValorFalso;
                }
            }

            return null;
        }

        public Boolean GetCheck()
        {
            if (checkEdit1.Checked)
                return true;
            else
                return false;
        }

        public void Set(Object valor)
        {
            if (TipoCampoCheck == Global.Types.TipoCampoCheck.Booleano)
            {
                if (Convert.ToInt32(valor) == Convert.ToInt32(ValorVerdadeiro))
                {
                    checkEdit1.Checked = true;
                }
                else
                {
                    checkEdit1.Checked = false;
                }
            }

            if (TipoCampoCheck == Global.Types.TipoCampoCheck.Inteiro)
            {
                if (Convert.ToInt32(valor) == Convert.ToInt32(ValorVerdadeiro))
                {
                    checkEdit1.Checked = true;
                }
                else
                {
                    checkEdit1.Checked = false;
                }                
            }

            if (TipoCampoCheck == Global.Types.TipoCampoCheck.String)
            {
                if (valor.ToString().Equals(ValorVerdadeiro))
                {
                    checkEdit1.Checked = true;
                }
                else
                {
                    checkEdit1.Checked = false;
                }                
            }            
        }
        
        #endregion

        #region EVENTOS CUSTOMIZADOS

        // TENTATIVA 1
        //public delegate void AposMarcarHandler(object sender, EventArgs e);
        //[Category("_APP"), Description("Método executado após marcar o check"), DefaultValue(false)]
        //public event AposMarcarHandler AposMarcar;

        //public delegate void AposDesmarcarHandler(object sender, EventArgs e);
        //[Category("_APP"), Description("Método executado após desmarcar o check"), DefaultValue(false)]
        //public event AposDesmarcarHandler AposDesmarcar;

        //private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!carregando)
        //    {
        //        if (checkEdit1.CheckState == CheckState.Checked)
        //        {
        //            try
        //            {
        //                this.AposMarcar(this, null);
        //            }
        //            catch { }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                this.AposDesmarcar(this, null);
        //            }
        //            catch { }
        //        }
        //    }
        //}

        // TENTATIVA 2
        //public delegate void AposClicarHandler(object sender, EventArgs e);
        //[Category("_APP"), Description("Método executado após clicar no check"), DefaultValue(false)]
        //public event AposClicarHandler AposClicar;

        //private void checkEdit1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.AposClicar(this, null);
        //    }
        //    catch { }
        //}

        #endregion
        
    }
}
