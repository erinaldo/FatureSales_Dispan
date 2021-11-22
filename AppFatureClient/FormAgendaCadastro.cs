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
    public partial class FormAgendaCadastro : AppLib.Windows.FormCadastroData
    {
          private static FormAgendaCadastro _instance = null;

        public static FormAgendaCadastro GetInstance()
        {
            if (_instance == null)
                _instance = new FormAgendaCadastro();
            return _instance;
        }
     
        public FormAgendaCadastro()
        {
            InitializeComponent();
           
            campoMemoOBS.richTextBox1.ReadOnly = true;
            campoMemoOBS.richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;

            //Busca o ultimo valor do IDAGENDA
            string sql = @"SELECT MAX(IDAGENDA) FROM ZAGENDA";
            campoInteiroIDAGENDA.Set(Convert.ToInt32(AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new object[] { })) + 1);

        }

        private void FormAgendaCadastro_AntesNovo(object sender, EventArgs e)
        {
         
        }

        private void FormAgendaCadastro_AposNovo(object sender, EventArgs e)
        {
            string sql = @"SELECT MAX(IDAGENDA) FROM ZAGENDA";
            campoInteiroIDAGENDA.Set(Convert.ToInt32(AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new object[] { })) + 1);
        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODMUNICIPIO, NOMEMUNICIPIO, CODETDMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, consulta1, new Object[] { campoLookup2.Get() });
        }

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = "SELECT NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ? AND CODMUNICIPIO = ?";
            try
            {
                campoLookup1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, Consulta, new Object[] { campoLookup2.Get(), campoLookup1.textBoxCODIGO.Text }).ToString();
            }
            catch (Exception)
            {
                
            }
            
        }

        private void campoLookup1_AposSelecao(object sender, EventArgs e)
        {
            campoTextoCIDADE.Set(campoLookup1.textBoxDESCRICAO.Text);
        }

        private void FormAgendaCadastro_Load(object sender, EventArgs e)
        {
            campoTextoCEP.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTextoCEP.textEdit1.Properties.Mask.EditMask = "99999-999";
            campoTextoTELEFONE.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTextoTELEFONE.textEdit1.Properties.Mask.EditMask = "(99) 9999-9999";
            campoTextoTELEFONE1.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTextoTELEFONE1.textEdit1.Properties.Mask.EditMask = "(99) 9999-9999";
            campoTextoCELULAR.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTextoCELULAR.textEdit1.Properties.Mask.EditMask = "(99) 99999-9999";
            campoTextoFAX.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTextoFAX.textEdit1.Properties.Mask.EditMask = "(99) 9999-9999";
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 16);
            campoTextoTELEFONE.textEdit1.Font = f;
            campoTextoTELEFONE1.textEdit1.Font = f;
            campoTextoCELULAR.textEdit1.Font = f;
            campoTextoFAX.textEdit1.Font = f;
            campoTextoEMAIL.textEdit1.Font = f;
           
            campoTextoTELEFONE.Width = 210;
            campoTextoTELEFONE.Height = 50;
            campoTextoFAX.Width = 210;
            campoTextoFAX.Height = 50;
            campoTextoTELEFONE1.Width = 210;
            campoTextoTELEFONE1.Height = 50;
            campoTextoCELULAR.Width = 210;
            campoTextoCELULAR.Height = 50;
            campoTextoEMAIL.Height = 50;
        }

        private void FormAgendaCadastro_AntesExcluir(object sender, EventArgs e)
        {
        }

        private bool FormAgendaCadastro_ValidarExcluir(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir esse registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                return true;    
            }
            else
            {
                return false;
            }
            
        }

        private void campoLookup2_AposSelecao(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(campoLookup2.Get()))
            {
                campoLookup1.Enabled = true;
            }
            else
            {
                campoLookup1.Enabled = false;
            }
            
        }

        #region Validação da saída dos componentes LookUp

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
