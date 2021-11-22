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
    public partial class FormOffOrcamentoItem : AppLib.Windows.FormCadastroData
    {
        public int? CODCOLIGADA { get; set; }
        public string IDMOV { get; set; }
        public int? NSEQITMMOV { get; set; }

        public FormOffOrcamentoItem()
        {
            InitializeComponent();
        }

        private void FormOffOrcamentoItem_Load(object sender, EventArgs e)
        {
            campoInteiroCODCOLIGADA.Set(CODCOLIGADA);
            campoInteiroIDMOV.Set(IDMOV);
            campoInteiroNSEQITMMOV.Set(NSEQITMMOV);

            if (campoInteiroNSEQITMMOV.Get() == null)
            {
                campoDecimalQUANTIDADE.Set(1);
                campoDecimalPRECOUNITARIO.Set(0);
                campoListaPRODUTOCOMPOSTO.Set(null);
            }
        }

        private bool campoLookupIDPRD_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupIDPRD, "SELECT * FROM ZTPRODUTO WHERE CODCOLPRD IN (0, ?) AND ULTIMONIVEL = 1 AND INATIVO = 0", new Object[] { campoInteiroCODCOLIGADA.Get() });
        }

        

        private void campoLookupIDPRD_AposSelecao(object sender, EventArgs e)
        {
            #region BUSCAR CODUND E O IPI

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery("SELECT CODUND, ALIQUOTAIPI FROM ZTPRODUTO WHERE IDPRD = ?", new Object[] { campoLookupIDPRD.Get() });

            if (dt.Rows.Count > 0)
            {
                campoTextoCODUND.Set(dt.Rows[0]["CODUND"].ToString());
                if (!string.IsNullOrEmpty(dt.Rows[0]["ALIQUOTAIPI"].ToString()))
                {
                    campoDecimalALIQUOTAIPI.Set(decimal.Parse(dt.Rows[0]["ALIQUOTAIPI"].ToString()));    
                }
                
            }
            else
            {
                campoTextoCODUND.Set("UN");
                campoDecimalALIQUOTAIPI.Set(0);
            }

            #endregion
        }

        private bool campoLookupIDPRDCOMPOSTO_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupIDPRDCOMPOSTO, "SELECT * FROM ZTPRODUTO WHERE CODCOLPRD IN (0, ?) AND ULTIMONIVEL = 1 AND INATIVO = 0", new Object[] { campoInteiroCODCOLIGADA.Get() });
        }

        

        private void FormOffOrcamentoItem_AposSalvar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormOffOrcamentoItem_AntesSalvar(object sender, EventArgs e)
        {
            if (campoInteiroNSEQITMMOV.Get().Equals("") || campoInteiroNSEQITMMOV.Get() == null)
            {
                String Consulta = "SELECT ISNULL(MAX(NSEQITMMOV),0) + 1 NSEQITMMOV FROM ZORCAMENTOITEM WHERE CODCOLIGADA = ? AND IDMOV = ?";
                campoInteiroNSEQITMMOV.Set( Convert.ToInt32(AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, Consulta, new Object[] { campoInteiroCODCOLIGADA.Get(), campoInteiroIDMOV.Get() }) .ToString()));
            }
        }

        private bool FormOffOrcamentoItem_Validar(object sender, EventArgs e)
        {
            if (campoLookupIDPRD.Get() == string.Empty)
            {
                MessageBox.Show("Campo Produto obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoTextoCODUND.Get() == string.Empty)
            {
                MessageBox.Show("Campo Unidade obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoDecimalQUANTIDADE.Get() <= 0)
            {
                MessageBox.Show("Campo Quantidade deve ser maior que zero.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
