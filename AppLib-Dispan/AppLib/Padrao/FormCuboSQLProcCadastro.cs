using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Padrao
{
    public partial class FormCuboSQLProcCadastro : AppLib.Windows.FormCadastroData
    {
        public FormCuboSQLProcCadastro()
        {
            InitializeComponent();
        }

        private void FormCuboSQLProcCadastro_Load(object sender, EventArgs e)
        {

        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = "SELECT * FROM ZCUBOPARAM WHERE IDCUBO = ?";
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, consulta, new Object[] { campoInteiroIDCUBO.Get() });
        }

    }
}
