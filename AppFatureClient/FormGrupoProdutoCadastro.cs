using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormGrupoProdutoCadastro : AppLib.Windows.FormCadastroData
    {
        public FormGrupoProdutoCadastro()
        {
            InitializeComponent();
            campoTextoCODTRA.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTextoCODTRA.textEdit1.Properties.Mask.EditMask = "00.000";
        }

        private void FormGrupoProdutoCadastro_AntesSalvar(object sender, EventArgs e)
        {
            campoInteiroCODCOLIGADA.Set(AppLib.Context.Empresa);
        }

        private bool FormGrupoProdutoCadastro_Validar(object sender, EventArgs e)
        {
            //valida a máscara do codigo da transportadora
            if (string.IsNullOrEmpty(campoTextoCODTRA.Get()))
            {
                MessageBox.Show("Campo Código obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {

            }

            return true;
        }
    }
}
