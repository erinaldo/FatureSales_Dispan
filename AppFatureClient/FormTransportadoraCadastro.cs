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
    public partial class FormTransportadoraCadastro : AppLib.Windows.FormCadastroData
    {
        public FormTransportadoraCadastro()
        {
            InitializeComponent();
            campoTextoCODTRA.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTextoCODTRA.textEdit1.Properties.Mask.EditMask = "0000";
        }

        private bool campoLookup1CODETD_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODETD, NOME FROM GETD";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODETD, consulta1, new Object[] { });
        }

        private bool campoLookup1CODMUNICIPIO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODMUNICIPIO, NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1CODMUNICIPIO, consulta1, new Object[] { campoLookup1CODETD.Get() });
        }

        private void FormTransportadoraCadastro_Preparar(object sender, EventArgs e)
        {
            campoInteiroCODCOLIGADA.Set(AppLib.Context.Empresa);
        }

        private void FormTransportadoraCadastro_Load(object sender, EventArgs e)
        {
 
        }

        private bool FormTransportadoraCadastro_Validar(object sender, EventArgs e)
        {
            //valida a máscara do codigo da transportadora
            if (string.IsNullOrEmpty(campoTextoCODTRA.Get()))
            {
                MessageBox.Show("Campo Código da Transportadora obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {

            }

            return true;
        }
    }
}
