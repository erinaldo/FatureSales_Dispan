using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class FormImportarDados : DevExpress.XtraEditors.XtraForm
    {
        private System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
        public String Conexao { get; set; }

        public FormImportarDados()
        {
            InitializeComponent();
            Conexao = "Start";
        }

        private void FormProgresso_Load(object sender, EventArgs e)
        {
            ofd.Filter = "Arquivo Excel (*.xlsx)|*.xlsx";
            ofd.Multiselect = true;
            ofd.Title = "Selecione a(s) planilha(s)";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                this.Close();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int iArquivo = 0; iArquivo < ofd.FileNames.Length; iArquivo++)
                {
                    String arquivo = ofd.FileNames[iArquivo].ToString();
                    String strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + arquivo + ";Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;\"";
                    // String strConn = "Provider=Microsoft.ACE.OLEDB.14.0;Data Source=" + arquivo + ";Extended Properties=Excel 14.0 Xml";
                    // String strConn = "Provider=Microsoft.ACE.OLEDB.15.0;Data Source=" + arquivo + ";Extended Properties=Excel 15.0 Xml";

                    AppLib.Data.OleDb ole = new AppLib.Data.OleDb(strConn);

                    ole.conn.Open();
                    DataTable dtSheet = ole.conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                    List<string> listSheet = new List<string>();
                    foreach (DataRow drSheet in dtSheet.Rows)
                    {
                        if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                        {
                            listSheet.Add(drSheet["TABLE_NAME"].ToString());
                        }
                    }
                    ole.conn.Close();

                    String tabela = listSheet[0];
                    System.Data.DataTable dt = ole.ExecQuery("SELECT * FROM [" + tabela + "]");

                    int contSave = 0;

                    for (int iLinha = 1; iLinha < dt.Rows.Count; iLinha++)
                    {
                        backgroundWorker1.ReportProgress(this.CalcularPercentual(dt.Rows.Count, iLinha));

                        AppLib.ORM.Jit reg = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), tabela.Replace("$", ""));

                        for (int iColuna = 0; iColuna < dt.Columns.Count; iColuna++)
                        {
                            String coluna = dt.Rows[0][iColuna].ToString();
                            Object valor = dt.Rows[iLinha][iColuna];

                            reg.Set(coluna, valor);

                            if (backgroundWorker1.CancellationPending)
                            {
                                iColuna = dt.Columns.Count;
                            }
                        }

                        try
                        {
                            contSave += reg.Save();
                        }
                        catch (Exception ex)
                        {
                            AppLib.Windows.FormMessageDefault.ShowError("Erro ao salvar registro " + iLinha + ".\r\nDetalhe técnico: " + ex.Message);
                        }

                        if (backgroundWorker1.CancellationPending)
                        {
                            iLinha = dt.Rows.Count;
                        }
                    }

                    if (backgroundWorker1.CancellationPending)
                    {
                        iArquivo = ofd.FileNames.Length;
                    }
                }

            }
            catch (Exception) { }
        }

        public int CalcularPercentual(int total, int atual)
        {
            int result = (atual * 100) / total;
            return result;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            this.Close();
        }

        private void FormImportarDados_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

    }
}
