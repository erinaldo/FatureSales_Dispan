using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient.Seguranca
{
    public partial class FormProcessoCadastro : AppLib.Windows.FormCadastroData
    {
        public FormProcessoCadastro()
        {
            InitializeComponent();
        }

        private void FormProcessoCadastro_Load(object sender, EventArgs e)
        {

        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            return new FormMenuVisao().MostrarLookup(campoLookup1);
        }

        

        private void FormProcessoCadastro_AntesSalvar(object sender, EventArgs e)
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

        #endregion
    }
}
