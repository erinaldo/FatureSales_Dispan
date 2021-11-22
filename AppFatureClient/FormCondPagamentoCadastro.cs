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
    public partial class FormCondPagamentoCadastro : AppLib.Windows.FormCadastroData
    {
        public FormCondPagamentoCadastro()
        {
            InitializeComponent();
            campoTextoCODTRA.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTextoCODTRA.textEdit1.Properties.Mask.EditMask = "L000";
        }

        private void FormCondPagamentoCadastro_AntesSalvar(object sender, EventArgs e)
        {
            campoInteiroCODCOLIGADA.Set(AppLib.Context.Empresa);
        }

        private bool FormCondPagamentoCadastro_Validar(object sender, EventArgs e)
        {
            //valida a máscara do codigo da transportadora
            if (string.IsNullOrEmpty(campoTextoCODTRA.Get()))
            {
                MessageBox.Show("Campo Código da Condição de Pagamento obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {

            }

            return true;
        }
    }
}
