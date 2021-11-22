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
    public partial class FormOffOrcamentoVisao : AppLib.Windows.FormVisao
    {
        private static FormOffOrcamentoVisao _instance = null;

        public static FormOffOrcamentoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormOffOrcamentoVisao();
            return _instance;
        }

        private bool AnexoItemMovimento;

        public FormOffOrcamentoVisao()
        {
            InitializeComponent();

            AnexoItemMovimento = false;

            grid1.GetAnexos().Add("Item do Movimento", null, ItemMovimento);
            grid1.GetAnexos().Add("Fechar Anexos", null, FecharAnexos);

            this.grid1.GetProcessos().Add("Imprimir Orçamento", null, ImprimirOrcamento);
            this.grid1.GetProcessos().Add("Imprimir Orçamento Liquido", null, ImprimirOrcamentoLiquido);
            this.grid1.GetProcessos().Add("Imprimir Orçamento Desconto", null, ImprimirOrcamentoDesconto);
            this.grid1.GetProcessos().Add("Imprimir Pedido", null, ImprimirPedido);

            this.grid1.GetProcessos().Add("-", null, null);

            this.grid1.GetProcessos().Add("Enviar Orçamentos Selecionados", null, EnviarOrcamentosSelecionados);
        }

        public void FecharAnexos(object sender, EventArgs e)
        {
            try
            {
                AnexoItemMovimento = false;
                splitContainer1.Panel2Collapsed = true;
            }
            catch { }
        }

        public void ItemMovimento(object sender, EventArgs e)
        {
            DataRow dr = null;

            try
            {
                dr = grid1.GetDataRow();
            }
            catch { }

            if (dr == null)
                return;

            AnexoItemMovimento = true;
            string ConsultaFiltro = string.Empty;

            try
            {
                splitContainer1.Panel2Collapsed = false;

                ConsultaFiltro = @"SELECT *
FROM ZVWOFFORCAMENTOITEM
WHERE CODCOLIGADA = ?
  AND IDMOV = ?";
                ConsultaFiltro = AppLib.Context.poolConnection.Get(grid1.Conexao).ParseCommand(ConsultaFiltro, new Object[] { dr["CODCOLIGADA"], dr["IDMOV"] });
                gridView1.GridControl.DataSource = AppLib.Context.poolConnection.Get(grid1.Conexao).ExecQuery(ConsultaFiltro, new Object[] { });

                // formata as colunas
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    String TempColuna = gridView1.Columns[i].FieldName;
                    String TempTipo = gridView1.Columns[i].ColumnType.ToString();
                    String TempFormat = gridView1.Columns[i].DisplayFormat.ToString();

                    if (gridView1.Columns[i].ColumnType == typeof(DateTime))
                    {
                        gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridView1.Columns[i].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss.fff";
                    }
                }

                // Tratamentos para a grid
                // Grid somente leitura
                gridView1.OptionsBehavior.Editable = false;

                // Grid com seleção de multiplos registros
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

                // Grid com scroll horizontal
                gridView1.OptionsView.ColumnAutoWidth = false;

                // Grid com auto ajuste do melhor tamanho das colunas
                gridView1.BestFitMaxRowCount = 800;
                gridView1.BestFitColumns();

                // Grid mostrar coluna de filtro
                gridView1.OptionsView.ShowAutoFilterRow = true;

                // Grid sempre mostrar filtro no rodapé
                // gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;

                // Grid zebrada
                // gridView1.OptionsView.EnableAppearanceEvenRow = true;
                // gridView1.Appearance.EvenRow.BackColor = Color.Thistle;   
            }
            catch (Exception ex)
            {
                new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao executar consulta.", ConsultaFiltro, ex);            
            }
        }

        public void ImprimirOrcamento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8010", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    RelOrcamentoOff relatorio = new RelOrcamentoOff();
                    relatorio.SelectRow = grid1.GetDataRow();
                    relatorio.Conexao = grid1.Conexao;
                    DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                    tool.ShowRibbonPreviewDialog();
                }
            }
        }

        public void ImprimirOrcamentoLiquido(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8010", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    RelOrcamentoLiquidoOff relatorio = new RelOrcamentoLiquidoOff();
                    relatorio.SelectRow = grid1.GetDataRow();
                    relatorio.Conexao = grid1.Conexao;
                    DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                    tool.ShowRibbonPreviewDialog();
                }
            }
        }

        public void ImprimirOrcamentoDesconto(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8010", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    RelOrcamentoDescontoOff relatorio = new RelOrcamentoDescontoOff();
                    relatorio.SelectRow = grid1.GetDataRow();
                    relatorio.Conexao = grid1.Conexao;
                    DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                    tool.ShowRibbonPreviewDialog();
                }
            }
        }

        public void ImprimirPedido(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8011", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    RelPedidoOff relatorio = new RelPedidoOff();
                    relatorio.SelectRow = grid1.GetDataRow();
                    relatorio.Conexao = grid1.Conexao;
                    DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                    tool.ShowRibbonPreviewDialog();
                }
            }
        }

        public Boolean pedidoContemItens(int CODCOLIGADA, string IDMOV)
        {
            String Comando = "SELECT COUNT(*) FROM ZORCAMENTOITEM WHERE CODCOLIGADA = ? AND IDMOV = ?";

            int contador = int.Parse(AppLib.Context.poolConnection.Get("Conn2").ExecGetField(0, Comando, new Object[] { CODCOLIGADA, IDMOV }).ToString());

            if (contador > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean MovimentosEnviadosParaIntegracao()
        {
            return false;
            /*
            System.Data.DataRowCollection drc = grid1.GetDataRows();

            for (int i = 0; i < drc.Count; i++)
            {
                if (drc[i]["ENVIAR"].Equals(1))
                {
                    MessageBox.Show("Não é permitido editar/excluir movimentos enviados para integração.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }
            }
           
            return false;
            */
        }

        public void EnviarOrcamentosSelecionados(object sender, EventArgs e)
        {
            System.Data.DataRowCollection drc = grid1.GetDataRows();

            if (drc != null)
            {
                MessageBox.Show("ATENÇÃO: Os orçamentos selecionados serão liberados para envio, mas o envio ocorrerá somente se estiver conectado á internet.\r\n\nCaso não esteja conectado á internet:\r\n- Feche este programa de digitação de orçamentos.\r\n- Conecte á internet.\r\n- Abra este programa novamente.\r\n- Faça o login para que os orçamentos sejam enviados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (MessageBox.Show("Deseja prosseguir com a operação?", "Mensagem", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                for (int i = 0; i < drc.Count; i++)
                {
                    int CODCOLIGADA = int.Parse(drc[i]["CODCOLIGADA"].ToString());
                    string IDMOV = drc[i]["IDMOV"].ToString();


                    String Comando = "SELECT IDOFF FROM ZORCAMENTO WHERE CODCOLIGADA = ? AND IDMOV = ?";
                    string IDOFF = AppLib.Context.poolConnection.Get("Conn2").ExecGetField(string.Empty, Comando, new Object[] { CODCOLIGADA, IDMOV }).ToString();

                    if (pedidoContemItens(CODCOLIGADA, IDMOV))
                    {
                        if (IDOFF != string.Empty)
                        {
                            Comando = "SELECT CODCOLIGADA FROM ZFCFOX WHERE IDOFF = ?";
                            int CODCOLCFO = int.Parse(AppLib.Context.poolConnection.Get("Conn2").ExecGetField(0, Comando, new Object[] { IDOFF }).ToString());

                            String Comando3 = "UPDATE ZFCFOX SET ENVIAR = 1, RECCREATEDBY = ?, RECCREATEDON = ?, RECMODIFIEDBY = ?, RECMODIFIEDON = ? WHERE CODCOLIGADA = ? AND IDOFF = ?";
                            int temp3 = AppLib.Context.poolConnection.Get("Conn2").ExecTransaction(Comando3, new Object[] { AppLib.Context.Usuario, DateTime.Now, AppLib.Context.Usuario, DateTime.Now, CODCOLCFO, IDOFF });
                        }
                        string sql = @"SELECT COUNT(NSEQITMMOV) valor FROM ZORCAMENTOITEM WHERE CODCOLIGADA = ? AND IDMOV = ?";
                        int retorno = Convert.ToInt32(AppLib.Context.poolConnection.Get("Conn2").ExecGetField(0, sql, new object[] { AppLib.Context.Empresa, IDMOV }));

                        String Comando1 = "UPDATE ZORCAMENTO SET ENVIAR = 1, RECCREATEDBY = ?, RECCREATEDON = ?, RECMODIFIEDBY = ?, RECMODIFIEDON = ?, CHECKITENS = ? WHERE CODCOLIGADA = ? AND IDMOV = ?";
                        String Comando2 = "UPDATE ZORCAMENTOITEM SET RECCREATEDBY = ?, RECCREATEDON = ?, RECMODIFIEDBY = ?, RECMODIFIEDON = ? WHERE CODCOLIGADA = ? AND IDMOV = ?";

                        int temp1 = AppLib.Context.poolConnection.Get("Conn2").ExecTransaction(Comando1, new Object[] { AppLib.Context.Usuario, DateTime.Now, AppLib.Context.Usuario, DateTime.Now, retorno, CODCOLIGADA, IDMOV });
                        int temp2 = AppLib.Context.poolConnection.Get("Conn2").ExecTransaction(Comando2, new Object[] { AppLib.Context.Usuario, DateTime.Now, AppLib.Context.Usuario, DateTime.Now, CODCOLIGADA, IDMOV });
                    }
                    else
                    {
                        MessageBox.Show("Orçamento CODCOLIGADA: " + CODCOLIGADA + " IDMOV: " + IDMOV + " - Não pode ser enviado pois não contém itens.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                grid1.toolStripButtonATUALIZAR_Click(this, null);
            }
        }

        private void FormOffOrcamentoVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormOffOrcamentoCadastro().Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            if (this.MovimentosEnviadosParaIntegracao())
            {
                // NÃO FAZER NADA
            }
            else
            {
                new FormOffOrcamentoCadastro().Editar(grid1.bs);
            }
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            if (this.MovimentosEnviadosParaIntegracao())
            {
                // NÃO FAZER NADA
            }
            else
            {
                new FormOffOrcamentoCadastro().Excluir(grid1.GetDataRows());
            }
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_EventoClick(object sender, EventArgs e)
        {
            if (AnexoItemMovimento)
                ItemMovimento(this, null);
        }

        private void FormOffOrcamentoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}
