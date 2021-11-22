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
    public partial class FormProcessoAplicarRepresentante : Form
    {
        DataRowCollection drc;
        public FormProcessoAplicarRepresentante()
        {
            InitializeComponent();
        }
        public FormProcessoAplicarRepresentante(DataRowCollection _drc)
        {
            InitializeComponent();
            drc = _drc;

        }

        private bool AplicaRepresentante(DataRowCollection drc)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get();
            conn.BeginTransaction();
            AppLib.ORM.Jit ZREPREDESC = new AppLib.ORM.Jit(conn, "ZREPREDESC");
            try
            {

                for (int i = 0; i < drc.Count; i++)
                {
                    ZREPREDESC.Set("CODCOLIGADA", AppLib.Context.Empresa);
                    ZREPREDESC.Set("CODREPRESENTANTE", campoLookupCODRPR.Get());
                    ZREPREDESC.Set("CODTB4FAT", drc[i]["CODTB4FAT"]);
                    ZREPREDESC.Set("DESCRICAO", campoLookupCODRPR.textBoxDESCRICAO.Text);
                    ZREPREDESC.Set("PERCDESCONTO", campoDecimalPercDescontoRepr.Get());
                    ZREPREDESC.Save();
                }
                conn.Commit();
                return true;
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (AplicaRepresentante(drc) == true)
            {
                MessageBox.Show("Aplicação realizada com sucesso.", "Informação de Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
        }
    }
}
