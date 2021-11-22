using DevExpress.XtraEditors;
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
    public partial class FormMenuPerfilCadastro : AppLib.Windows.FormCadastroData
    {
        public FormMenuPerfilCadastro()
        {
            InitializeComponent();
        }

        private void FormMenuPerfilCadastro_Load(object sender, EventArgs e)
        {

        }        

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            return new FormMenuVisao().MostrarLookup(campoLookup1);
        }

        

        private bool campoLookup2_SetFormConsulta(object sender, EventArgs e)
        {
            return new FormPerfilVisao().MostrarLookup(campoLookup2);
        }

        

        private void FormMenuPerfilCadastro_AntesSalvar(object sender, EventArgs e)
        {
            if (campoDATACRIACAO.Get() == null)
            {
                campoDATACRIACAO.Set(DateTime.Now);
                campoUSUARIOCRIACAO.Set(AppLib.Context.Usuario);
            }

            campoDATAALTERACAO.Set(DateTime.Now);
            campoUSUARIOALTERACAO.Set(AppLib.Context.Usuario);
        }

        #region Validação da saída dos componentes LookUp

        private void campoLookup1_Leave(object sender, EventArgs e)
        {
            if (campoLookup1.textBoxCODIGO.EditValue != null)
            {
                AppFatureClient.New.Class.Utilities util = new AppFatureClient.New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup1))
                {
                    XtraMessageBox.Show("Informe o valor correto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void campoLookup2_Leave(object sender, EventArgs e)
        {
            if (campoLookup2.textBoxCODIGO.EditValue != null)
            {
                AppFatureClient.New.Class.Utilities util = new AppFatureClient.New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup2))
                {
                    XtraMessageBox.Show("Informe o valor correto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion
    }
}
