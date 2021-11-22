using System;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormPrintPreview : Form
    {
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }
        public DateTime Impressao { get; set; }
        public DevExpress.XtraReports.UI.XtraReport relatorio { get; set; }


        public FormPrintPreview()
        {
            InitializeComponent();
        }

        private void FormPrintPreview_Load(object sender, EventArgs e)
        {
            documentViewer1.DocumentSource = relatorio;

            string CodSerie = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", "select SERIE from TMOV where IDMOV = ? and CODCOLIGADA = ?", new object[] { SelectRow["IDMOV"], AppLib.Context.Empresa }).ToString();
            string NumeroMov = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", "select NUMEROMOV from TMOV where IDMOV = ? and CODCOLIGADA = ?", new object[] { SelectRow["IDMOV"], AppLib.Context.Empresa }).ToString();
            string Codcfo = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", "SELECT CODCFO FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, SelectRow["IDMOV"] }).ToString();
            string NomeCliente = "";

            if (Codcfo == "00632" || Codcfo == "00002")
            {
                NomeCliente = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", "SELECT NOMEFCFOORC FROM TMOVCOMPL WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, SelectRow["IDMOV"] }).ToString();
            }
            else
            {
                NomeCliente = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", "select NOMEFANTASIA FROM FCFO WHERE CODCFO IN (SELECT CODCFO from TMOV where IDMOV = ? and CODCOLIGADA = ?)", new object[] { SelectRow["IDMOV"], AppLib.Context.Empresa }).ToString();
            }

            if (NomeCliente.Contains("/"))
            {
                NomeCliente = NomeCliente.Replace("/", "-");
            }

            if (CodSerie == "ORC")
            {
                relatorio.Name = "Proposta Comercial Numero " + NumeroMov + " - " + NomeCliente;
            }
            else if (CodSerie == "PVP")
            {
                relatorio.Name = "Pedido de Venda Numero " + NumeroMov + " - " + NomeCliente;
            }
            else if (CodSerie == "COT")
            {
                relatorio.Name = NumeroMov + " - " + NomeCliente;
            }

            relatorio.CreateDocument(false);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //Finaliza processo caso exista (correção provisória até que a DevExpress solucione o problema) - Matheus Fenólio do Prado 2018-05-01
            //try
            //{
            //    Process[] p;
            //    p = Process.GetProcessesByName("splwow64");

            //    if ((p.Length > 0) && (p.Length < 2))
            //    {
            //        if (!p[0].Responding)
            //        {
            //            p[0].Kill();
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Erro ao tentar finalizar processo: " + ex.Message, "Erro");
            //}


            try
            {
                DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                Nullable<Boolean> retorno = printTool.PrintDialog();

                if (retorno == true)
                {
                    if (relatorio.GetType() == typeof(RelOrdemProducao))
                    {
                        int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
                        int IdMov = Convert.ToInt32(SelectRow["IDMOV"]);

                        string ConsultaFiltro = @"UPDATE TMOV SET DATAEXTRA2 = ? WHERE TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ? AND DATAEXTRA2 IS NULL";
                        ConsultaFiltro = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(ConsultaFiltro, new Object[] { Impressao, CodColigada, IdMov });
                        AppLib.Context.poolConnection.Get(Conexao).ExecTransaction(ConsultaFiltro, new Object[] { });
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao imprimir: " + Environment.NewLine + ex.Message);
            }







        }

        private void printPreviewBarItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
