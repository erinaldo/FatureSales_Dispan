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
    public partial class FormLancamentoCadastro : AppLib.Windows.FormCadastroData
    {
        public int CodColigada;
        public int IdMov;

        public FormLancamentoCadastro()
        {
            InitializeComponent();
        }

        private bool campoLookupCODCFO_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCFO, "SELECT CODCFO, NOMEFANTASIA FROM FCFO WHERE CODCOLIGADA IN (0, ?)", new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookupCODCPG_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCPG, "SELECT CODTDO, DESCRICAO FROM FTDO WHERE CODCOLIGADA = ?", new Object[] { AppLib.Context.Empresa });
        }

        private void FormLancamentoCadastro_AntesSalvar(object sender, EventArgs e)
        {
            campoTexto14.Set(CodColigada.ToString());
            campoTextoIDMOV.Set(IdMov.ToString());
        }

        private void campoLookupCODCFO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM FCFO WHERE CODCOLIGADA = ? AND CODFCFO = ?";
            campoLookupCODCFO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCFO.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODCFO.Get() }).ToString();
        }

        private void campoLookupCODCPG_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM FTDO WHERE CODCOLIGADA = ? AND CODTDO = ?";
            campoLookupCODCPG.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCPG.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODCPG.Get() }).ToString();

        }

        
    }
}
