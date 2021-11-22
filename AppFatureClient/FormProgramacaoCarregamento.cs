using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;

namespace AppFatureClient
{
    public partial class FormProgramacaoCarregamento : Form
    {
        public DataRowCollection drc;
        public int idcarregamento;
        private List<Classes.ItensCarregamento> ListaItens = new List<Classes.ItensCarregamento>();
        private Boolean ExcluiuTudo = false;
        DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private bool altera;

        public FormProgramacaoCarregamento()
        {
            InitializeComponent();
            gridView1 = (DevExpress.XtraGrid.Views.Grid.GridView)gridObject1.gridControl1.Views[0];
            gridView1.CellValueChanged += gridView1_CellValueChanged;


        }
        public FormProgramacaoCarregamento(bool _altera)
        {
            InitializeComponent();
            gridView1 = (DevExpress.XtraGrid.Views.Grid.GridView)gridObject1.gridControl1.Views[0];
            gridView1.CellValueChanged += gridView1_CellValueChanged;
            altera = _altera;
        }
        void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Classes.ItensCarregamento item = (Classes.ItensCarregamento)gridObject1.GetObjectRow();

            if (!string.IsNullOrEmpty(item.QTD_A_PROGRAMAR.ToString()))
            {
                if (item.QTD_A_PROGRAMAR > item.QTD_SALDO)
                {
                    MessageBox.Show("Não a saldo suficiente pra pode realizar o carregamento.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ListaItens[Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0))].QTD_A_PROGRAMAR = item.QTD_SALDO;
                }
            }
        }
        public void programacaoCarregamento()
        {
            if (string.IsNullOrEmpty(txtDataIni.Get().ToString()) || string.IsNullOrEmpty(cmbCaminhao.Text))
            {
                MessageBox.Show("Favor preencher os campos corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int sim = 0;
            int nao = 0;
            List<object> listaObj = gridObject1.GetObjectRows();
            AppLib.ORM.Jit ZPROGRAMACAOCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZPROGRAMACAOCARREGAMENTO");
            DateTimeFormatInfo dfi1 = DateTimeFormatInfo.CurrentInfo;
            Calendar cal1 = dfi1.Calendar;
            DataTable dt = new DataTable();
            if (altera == true)
            {
                dt.Columns.Add("IDPROGRAMACAOCARREGAMENTO", typeof(int));
            }
            dt.Columns.Add("CODCOLIGADA", typeof(int));
            dt.Columns.Add("IDMOV", typeof(int));
            dt.Columns.Add("NSEQITMMOV", typeof(int));
            dt.Columns.Add("NUMEROMOV", typeof(string));
            dt.Columns.Add("CODIGOPRD", typeof(string));
            dt.Columns.Add("DATAENTREGA", typeof(DateTime));
            dt.Columns.Add("DESCRICAO_DO_PRODUTO", typeof(string));
            dt.Columns.Add("IDPRDCOMPOSTO", typeof(int));
            dt.Columns.Add("QTD_PEDIDO", typeof(decimal));
            dt.Columns.Add("QTD_SALDO", typeof(decimal));
            dt.Columns.Add("QTD_PROG", typeof(decimal));
            dt.Columns.Add("QTD_CARREGADA", typeof(decimal));
            string sql = string.Empty;
            if (altera == true)
            {
                sql = @"UPDATE ZPROGRAMACAOCARREGAMENTO SET QTDE = ?, DATAPROGRAMADA = ?, CAMINHAO = ?, TIPOCAMINHAO = ?, SEMANA = ?, ANO = ?, DATAALTERACAO = ?, REPROGRAMAR = NULL WHERE IDPROGRAMACAOCARREGAMENTO = ?";
                foreach (Classes.ItensCarregamento item in listaObj)
                {
                    if (item.QTD_A_PROGRAMAR > 0)
                    {
                        int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { item.QTD_A_PROGRAMAR, Convert.ToDateTime(txtDataIni.maskedTextBox1.Text), cmbCaminhao.Text, cmbTipo.Text, cal1.GetWeekOfYear(Convert.ToDateTime(txtDataIni.maskedTextBox1.Text), dfi1.CalendarWeekRule, dfi1.FirstDayOfWeek), System.DateTime.Now.Year, AppLib.Context.poolConnection.Get().GetDateTime(), item.IDPROGRAMACAOCARREGAMENTO });
                        dt.Rows.Add(item.IDPROGRAMACAOCARREGAMENTO, item.CODCOLIGADA, item.IDMOV, item.NSEQITMMOV, item.NUMEROMOV, item.CODIGOPRD, item.DATAENTREGA, item.DESCRICAO_DO_PRODUTO, item.IDPRDCOMPOSTO, item.QTD_PEDIDO, item.QTD_SALDO, item.QTD_PROG, item.QTD_CARREGADA);
                        sim++;
                    }
                    else
                    {
                        nao++;
                    }
                }
            }
            else
            {
                sql = @"INSERT INTO ZPROGRAMACAOCARREGAMENTO (IDMOV,  NSEQITMMOV, CODCOLIGADA, QTDE, DATAPROGRAMADA, CAMINHAO, TIPOCAMINHAO, SEMANA, ANO, DATAALTERACAO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                foreach (Classes.ItensCarregamento item in listaObj)
                {
                    if (item.QTD_A_PROGRAMAR > 0)
                    {
                        int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { item.IDMOV, item.NSEQITMMOV, item.CODCOLIGADA, item.QTD_A_PROGRAMAR, Convert.ToDateTime(txtDataIni.maskedTextBox1.Text), cmbCaminhao.Text, cmbTipo.Text, cal1.GetWeekOfYear(Convert.ToDateTime(txtDataIni.maskedTextBox1.Text), dfi1.CalendarWeekRule, dfi1.FirstDayOfWeek), System.DateTime.Now.Year, AppLib.Context.poolConnection.Get().GetDateTime() });
                        dt.Rows.Add(item.CODCOLIGADA, item.IDMOV, item.NSEQITMMOV, item.NUMEROMOV, item.CODIGOPRD, item.DATAENTREGA, item.DESCRICAO_DO_PRODUTO, item.IDPRDCOMPOSTO, item.QTD_PEDIDO, item.QTD_SALDO, item.QTD_PROG, item.QTD_CARREGADA);
                        sim++;
                    }
                    else
                    {
                        nao++;
                    }
                }
            }


            if (sim > 0)
            {
                DataRowCollection drc = dt.Rows;
                new ValidaEnvioEmail().validaEnvio(Convert.ToDateTime(txtDataIni.maskedTextBox1.Text), drc, "inclusão", 0);
            }

            MessageBox.Show(string.Format("Qtd. de itens programados com sucesso: {0}\n\rQtd. de itens não programados por falta de saldo: {1}", sim, nao), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListaItens.Clear();
            gridObject1_Atualizar(this, null);

            //for (int i = 0; i < grid.Count; i++)
            //{
            //    //Verificar saldo dos itens
            //    decimal saldo = Convert.ToDecimal(grid[i]["QTD_SALDO"].ToString());
            //    if (saldo <= 0)
            //    {
            //        MessageBox.Show("Não a saldo suficiente pra pode realizar o carregamento.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        if ((i + 1) == grid.Count)
            //        {
            //            return;
            //        }
            //        continue;
            //    }
            //if (checkBox1.Checked.Equals(true))
            //{
            //    ////Verificar o Status do carregamento.
            //    //string sql = @"SELECT STATUS FROM ZCARREGAMENTO WHERE IDCARREGAMENTO = ?";
            //    //object status = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new object[] { idcarregamento });
            //    //if (status.Equals("COMPLETO"))
            //    //{
            //    //    MessageBox.Show("Esse carregamento já foi baixado e não pode ser mais alterado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    //    return;
            //    //}
            //    //else
            //    //{


            //    if (string.IsNullOrEmpty(campoLookup1.Get()))
            //    {
            //        MessageBox.Show("Favor selecionar um carregamento que deseja incluir esses itens.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //    //Inserir ZCARREGAMENTOITEMS
            //    for (int l = 0; l < drc.Count; l++)
            //    {
            //        string sqla = @"INSERT INTO ZCARREGAMENTOITEMS (IDCARREGAMENTO, CODCOLIGADA, IDMOV, NSEQITMMOV, QTDE, CARREGAR, CAMINHAO, TIPOCAMINHAO, DATAPROGRAMADA) VALUES (?,?,?,?,?,?,?,?,?)";
            //        try
            //        {
            //            int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sqla, new Object[] { campoLookup1.Get(), AppLib.Context.Empresa, drc[l]["IDMOV"].ToString(), drc[l]["NSEQITMMOV"].ToString(), saldo, "PARCIAL", cmbCaminhao.Text, cmbTipo.Text, Convert.ToDateTime(txtDataIni.maskedTextBox1.Text) });
            //        }
            //        catch (Exception)
            //        {
            //        }
            //    }
            //    //}
            //}
            //else
            //{
            //Verificar aqui........... 




            //try
            //{

            //}
            //catch (Exception)
            //{
            //}
            // }
            // }

            //this.Dispose();

        }

        //       


        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            //Verifica se o campo data está preenchido corretamente.
            try
            {
                DateTime data = DateTime.Parse(txtDataIni.maskedTextBox1.Text);
                programacaoCarregamento();
            }
            catch (FormatException)
            {
                MessageBox.Show("Favor preencher o campo data corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormProgramacaoCarregamento_Load(object sender, EventArgs e)
        {
            gridObject1.GetProcessos().Add("Programar Carregamento", null, progCarregamento);
            gridData2.GetProcessos().Add("Alterar Data Carregamento", null, alterarDataProgramacao);
            gridData2.GetProcessos().Add("Alterar a quantidade do item da programação carregamento.", null, alterarQuantidadeProgramacao);
        }

        private void alterarDataProgramacao(object sender, EventArgs e)
        {
            DateTime data;
            AppLib.ORM.Jit ZPROGRAMACAOCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZPROGRAMACAOCARREGAMENTO");
            if (MessageBox.Show("Deseja alterar a data de programação nos itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                AppLib.Windows.FormMessagePrompt promt = new AppLib.Windows.FormMessagePrompt();
                promt.ShowDialog();
                if (promt.confirmacao.Equals(AppLib.Global.Types.Confirmacao.Cancelar))
                {
                    return;
                }

                try
                {
                    data = Convert.ToDateTime(promt.textBox1.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Favor digitar uma data válida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataRowCollection drc = gridData2.GetDataRows();
                for (int i = 0; i < drc.Count; i++)
                {
                    ZPROGRAMACAOCARREGAMENTO.Set("IDPROGRAMACAOCARREGAMENTO", int.Parse(drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString()));
                    ZPROGRAMACAOCARREGAMENTO.Set("DATAPROGRAMADA", data);
                    ZPROGRAMACAOCARREGAMENTO.Set("DATAALTERACAO", AppLib.Context.poolConnection.Get().GetDateTime());
                    ZPROGRAMACAOCARREGAMENTO.Save();
                }
                new ValidaEnvioEmail().validaEnvio(data, gridData2.GetDataRows(), "data", 0);
                MessageBox.Show("Alteração realizada com sucesso.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridData2.Atualizar(false);
            }
        }
        private void progCarregamento(object sender, EventArgs e)
        {
            programacaoCarregamento();
            gridObject1_Atualizar(this, null);
            gridData2.Atualizar(true);
        }
        private void alterarQuantidadeProgramacao(object sender, EventArgs e)
        {
            DataRow dr = null;
            decimal qtde = 0;
            try
            {
                dr = gridData2.GetDataRow();
            }
            catch { }
            if (dr == null)
            {
                return;
            }

            AppLib.ORM.Jit ZPROGRAMACAOCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZPROGRAMACAOCARREGAMENTO");
            if (MessageBox.Show("Deseja alterar a quantidade do item selecionado?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                AppLib.Windows.FormMessagePrompt promt = new AppLib.Windows.FormMessagePrompt();
                promt.ShowDialog();
                if (promt.confirmacao.Equals(AppLib.Global.Types.Confirmacao.Cancelar))
                {
                    return;
                }
                try
                {
                    qtde = Convert.ToDecimal(promt.textBox1.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Favor digitar uma quantidade válida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Select pra saber se a quantidade é maior do que tem cadastrado na TITMMOV
                string sql = @"-- SELECT PAI
SELECT IDMOV, NSEQITMMOV, QTD_PEDIDO, QTD_SALDO, CONVERT(DECIMAL(15,4),QTD_PROG) QTD_PROG FROM (
	--SELECT FILHO
	SELECT SUBLISTA.*,
		(QTD_PEDIDO -  ISNULL(QTD_PROG, 0)) QTD_SALDO
	FROM
	--CAMPOS SELECT FILHO
		(SELECT
		TITMMOV.IDMOV,
		TITMMOV.NSEQITMMOV,
		TITMMOV.QUANTIDADE QTD_PEDIDO,
        --SUB SELECT PRA TRAZER UM CAMPO  
		(SELECT SUM(ZPROGRAMACAOCARREGAMENTO.QTDE)
		FROM  ZPROGRAMACAOCARREGAMENTO
		WHERE
		 ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
		AND ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
		AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
		 ) QTD_PROG
		--INFORMANDO AS TABELAS DA PESQUISA
		FROM  TITMMOV
		) SUBLISTA
--FECHA SELECT PAI		
) LISTA
--WHERE SELECT PAI
WHERE LISTA.IDMOV = ? and LISTA.NSEQITMMOV = ?";

                DataTable retorno = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dr["IDMOV"].ToString(), dr["NSEQITMMOV"].ToString() });
                decimal saldo = Convert.ToDecimal(retorno.Rows[0]["QTD_SALDO"].ToString());

                if (saldo.Equals(0))
                {
                    if (qtde > Convert.ToDecimal(retorno.Rows[0]["QTD_PROG"].ToString()))
                    {
                        MessageBox.Show("Não a saldo suficiente pra realizar essa transação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (saldo > 0)
                {

                    if (qtde > (decimal.Parse(retorno.Rows[0]["QTD_PROG"].ToString()) + saldo))
                    {
                        MessageBox.Show("Não a saldo suficiente pra realizar essa transação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                ZPROGRAMACAOCARREGAMENTO.Set("IDPROGRAMACAOCARREGAMENTO", int.Parse(dr[0].ToString()));
                ZPROGRAMACAOCARREGAMENTO.Set("QTDE", qtde);
                ZPROGRAMACAOCARREGAMENTO.Set("DATAALTERACAO", AppLib.Context.poolConnection.Get().GetDateTime());
                ZPROGRAMACAOCARREGAMENTO.Save();

                new ValidaEnvioEmail().validaEnvio(Convert.ToDateTime(dr["DATAPROGRAMADA"]), gridData2.GetDataRows(), "quantidade", qtde);

                MessageBox.Show("Alteração realizada com sucesso.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridData2.Atualizar(true);
                gridObject1_Atualizar(this, null);
            }
        }
        private void txtDataIni_Validating(object sender, CancelEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int caminhao = 1;
            string sql = @"SELECT CONVERT(INT, CAMINHAO) CAMINHAO FROM ZPROGRAMACAOCARREGAMENTO WHERE DATAPROGRAMADA = ? AND CODCOLIGADA = ? GROUP BY CAMINHAO ORDER BY CAMINHAO";
            DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new object[] { txtDataIni.Get(), AppLib.Context.Empresa });
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (!string.IsNullOrEmpty(dt.Rows[i]["CAMINHAO"].ToString()))
                {
                    if (caminhao.Equals(Convert.ToInt32(dt.Rows[i]["CAMINHAO"])))
                    {
                        caminhao++;
                    }
                }


            }
            cmbCaminhao.Text = Convert.ToString(caminhao);
            gridData2.Atualizar(true);
        }

        //private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        //{
        //    //string consulta1 = @"SELECT IDCARREGAMENTO, TTRA.NOMEFANTASIA, PLACA, OBS, DATA, ZCARREGAMENTO.NUMERO, SEQUENCIAL FROM ZCARREGAMENTO inner join TTRA on ZCARREGAMENTO.CODTRA = TTRA.CODTRA WHERE STATUS = 'EM ABERTO'";
        //    //return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, consulta1, new Object[] { });
        //}

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            //String Consulta = "SELECT TTRA.NOMEFANTASIA FROM ZCARREGAMENTO inner join TTRA on ZCARREGAMENTO.CODTRA = TTRA.CODTRA WHERE STATUS = 'EM ABERTO' AND ZCARREGAMENTO.IDCARREGAMENTO = ?";
            //campoLookup1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get().ExecGetField(null, Consulta, new Object[] { campoLookup1.Get() }).ToString();
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            //if (checkBox1.Checked.Equals(true))
            //{
            //    campoLookup1.Enabled = true;
            //}
            //else
            //{
            //    campoLookup1.Enabled = false;
            //}
        }



        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            gridData2.Parametros = new object[] { txtDataIni.Get(), AppLib.Context.Empresa };
        }

        private void gridData1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridObject1_SetParametros(object sender, EventArgs e)
        {
            string valor = string.Empty;
            for (int i = 0; i < drc.Count; i++)
            {
                if (string.IsNullOrEmpty(valor))
                {
                    valor = "'" + drc[i]["IDMOV"].ToString() + "'";
                }
                else
                {
                    valor = valor + ", '" + drc[i]["IDMOV"].ToString() + "'";
                }

            }
            gridObject1.Consulta[41] = gridObject1.Consulta[41].ToString().Replace("?", valor);
            gridObject1.Parametros = new object[] { valor };
        }

        private void gridObject1_Atualizar(object sender, EventArgs e)
        {
            if (altera == true)
            {
                #region Alteração
                if (!ExcluiuTudo)
                {
                    if (ListaItens.Count == 0)
                    {

                        try
                        {
                            string comando = @"SELECT 
LISTA.IDPROGRAMACAOCARREGAMENTO,
LISTA.CODCOLIGADA, 
LISTA.IDMOV, 
LISTA.NSEQITMMOV, 
LISTA.NUMEROMOV, 
LISTA.CODIGOPRD, 
LISTA.DATAENTREGA, 
LISTA.DESCRICAO DESCRICAO_DO_PRODUTO, 
LISTA.IDPRDCOMPOSTO, 
CAST(QTD_PEDIDO AS NUMERIC(15,2)) QTD_PEDIDO, 
QTD_SALDO, CONVERT(DECIMAL(15,2),QTD_PROG) QTD_PROG, 
CONVERT(DECIMAL(15,2), QTD_CARREGADA) QTD_CARREGADA 
	FROM (
			SELECT SUBLISTA.*,
				CAST((QTD_PEDIDO -  (ISNULL(QTD_PROG, 0)+ ISNULL(QTD_CARREGADA, 0))) AS NUMERIC(15,2)) QTD_SALDO
			FROM
				(SELECT
				ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO,
				TITMMOV.IDMOV,
				TITMMOV.NSEQITMMOV,
				FCFO.NOMEFANTASIA,
				TMOV.NUMEROMOV,
				TMOV.DATAENTREGA,
				TPRODUTO.DESCRICAO,
				TITMMOV.QUANTIDADEORIGINAL QTD_PEDIDO,
				TMOV.CODTMV,
				TPRODUTO.CODIGOPRD,
				TITMMOV.IDPRDCOMPOSTO,
				TITMMOV.CODCOLIGADA,
				ZPROGRAMACAOCARREGAMENTO.REPROGRAMAR,
				(SELECT SUM(ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA)
				FROM ZPROGRAMACAOCARREGAMENTO
				WHERE
				ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
				AND ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
				AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
				) QTD_CARREGADA,
				(SELECT SUM(ZPROGRAMACAOCARREGAMENTO.QTDE)
				FROM  ZPROGRAMACAOCARREGAMENTO
				WHERE
				 ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
				AND ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
				AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
				 ) QTD_PROG
				FROM 
				TMOV, TITMMOV, TPRODUTO, FCFO, ZPROGRAMACAOCARREGAMENTO
				WHERE 
				TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA
				AND TMOV.IDMOV = TITMMOV.IDMOV
				AND TITMMOV.IDPRD = TPRODUTO.IDPRD
				AND TMOV.CODCOLCFO = FCFO.CODCOLIGADA
				AND TMOV.CODCFO = FCFO.CODCFO
				AND TITMMOV.IDMOV = ZPROGRAMACAOCARREGAMENTO.IDMOV
				AND TITMMOV.CODCOLIGADA = ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA
				AND TITMMOV.NSEQITMMOV = ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV
				)SUBLISTA	
	) LISTA
WHERE
LISTA.REPROGRAMAR = 1";
                            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(comando, new object[] { });
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Classes.ItensCarregamento item = new Classes.ItensCarregamento();
                                #region Itens
                                item.IDPROGRAMACAOCARREGAMENTO = Convert.ToInt32(dt.Rows[i]["IDPROGRAMACAOCARREGAMENTO"]);
                                item.CODCOLIGADA = Convert.ToInt32(dt.Rows[i]["CODCOLIGADA"]);
                                item.IDMOV = Convert.ToInt32(dt.Rows[i]["IDMOV"]);
                                item.NSEQITMMOV = Convert.ToInt32(dt.Rows[i]["NSEQITMMOV"]);
                                item.NUMEROMOV = dt.Rows[i]["NUMEROMOV"].ToString();
                                item.CODIGOPRD = dt.Rows[i]["CODIGOPRD"].ToString();
                                item.DESCRICAO_DO_PRODUTO = dt.Rows[i]["DESCRICAO_DO_PRODUTO"].ToString();
                                if (dt.Rows[i]["DATAENTREGA"] != DBNull.Value)
                                {
                                    item.DATAENTREGA = Convert.ToDateTime(dt.Rows[i]["DATAENTREGA"]);
                                }
                                if (dt.Rows[i]["IDPRDCOMPOSTO"] != DBNull.Value)
                                {
                                    item.IDPRDCOMPOSTO = Convert.ToInt32(dt.Rows[i]["IDPRDCOMPOSTO"]);
                                }
                                if (dt.Rows[i]["QTD_PEDIDO"] != DBNull.Value)
                                {
                                    item.QTD_PEDIDO = Convert.ToDecimal(dt.Rows[i]["QTD_PEDIDO"]);
                                }
                                if (dt.Rows[i]["QTD_SALDO"] != DBNull.Value)
                                {
                                    item.QTD_SALDO = Convert.ToDecimal(dt.Rows[i]["QTD_SALDO"]);
                                    item.QTD_A_PROGRAMAR = Convert.ToDecimal(dt.Rows[i]["QTD_SALDO"]);
                                }
                                if (dt.Rows[i]["QTD_PROG"] != DBNull.Value)
                                {
                                    item.QTD_PROG = Convert.ToDecimal(dt.Rows[i]["QTD_PROG"]);
                                }
                                if (dt.Rows[i]["QTD_CARREGADA"] != DBNull.Value)
                                {
                                    item.QTD_CARREGADA = Convert.ToDecimal(dt.Rows[i]["QTD_CARREGADA"]);
                                }
                                #endregion
                                ListaItens.Add(item);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                #endregion
             
            }
            #region Inclusão
            else
            {
                if (!ExcluiuTudo)
                {
                    
                    if (ListaItens.Count == 0)
                    {
                        String Comando = "";
                        try
                        {
                            Comando = gridObject1.GetConsulta();
                            string valor = string.Empty;
                            for (int i = 0; i < drc.Count; i++)
                            {
                                if (string.IsNullOrEmpty(valor))
                                {
                                    valor = drc[i]["IDMOV"].ToString();
                                }
                                else
                                {
                                    valor = valor + ", " + drc[i]["IDMOV"].ToString();
                                }

                            }
                            gridObject1.Consulta[41] = gridObject1.Consulta[41].ToString().Replace("?", valor);
                            gridObject1.Parametros = new object[] { valor };

                            Comando = AppLib.Context.poolConnection.Get().ParseCommand(Comando, gridObject1.Parametros);
                            Comando = Comando.Replace("'", "");
                            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(Comando, gridObject1.Parametros);

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Classes.ItensCarregamento item = new Classes.ItensCarregamento();
                                #region Itens
                                item.CODCOLIGADA = Convert.ToInt32(dt.Rows[i]["CODCOLIGADA"]);
                                item.IDMOV = Convert.ToInt32(dt.Rows[i]["IDMOV"]);
                                item.NSEQITMMOV = Convert.ToInt32(dt.Rows[i]["NSEQITMMOV"]);
                                item.NUMEROMOV = dt.Rows[i]["NUMEROMOV"].ToString();
                                item.CODIGOPRD = dt.Rows[i]["CODIGOPRD"].ToString();
                                item.DESCRICAO_DO_PRODUTO = dt.Rows[i]["DESCRICAO_DO_PRODUTO"].ToString();
                                if (dt.Rows[i]["DATAENTREGA"] != DBNull.Value)
                                {
                                    item.DATAENTREGA = Convert.ToDateTime(dt.Rows[i]["DATAENTREGA"]);
                                }
                                if (dt.Rows[i]["IDPRDCOMPOSTO"] != DBNull.Value)
                                {
                                    item.IDPRDCOMPOSTO = Convert.ToInt32(dt.Rows[i]["IDPRDCOMPOSTO"]);
                                }
                                if (dt.Rows[i]["QTD_PEDIDO"] != DBNull.Value)
                                {
                                    item.QTD_PEDIDO = Convert.ToDecimal(dt.Rows[i]["QTD_PEDIDO"]);
                                }
                                if (dt.Rows[i]["QTD_SALDO"] != DBNull.Value)
                                {
                                    item.QTD_SALDO = Convert.ToDecimal(dt.Rows[i]["QTD_SALDO"]);
                                    item.QTD_A_PROGRAMAR = Convert.ToDecimal(dt.Rows[i]["QTD_SALDO"]);
                                }
                                if (dt.Rows[i]["QTD_PROG"] != DBNull.Value)
                                {
                                    item.QTD_PROG = Convert.ToDecimal(dt.Rows[i]["QTD_PROG"]);
                                }
                                if (dt.Rows[i]["QTD_CARREGADA"] != DBNull.Value)
                                {
                                    item.QTD_CARREGADA = Convert.ToDecimal(dt.Rows[i]["QTD_CARREGADA"]);
                                }
                                #endregion
                                ListaItens.Add(item);
                            }
                        }
                        catch (Exception ex)
                        {
                            new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao atualizar a grid. Processo de carregar itens do carregamento para a grid de edição.", Comando, ex);
                        }
                    }
                }
            }
            #endregion

            gridObject1.gridControl1.DataSource = null;
            gridObject1.gridControl1.DataSource = ListaItens;

            // TRATAMENTO ESPECIAL PARA ESTE OBTER COMPONENTE HERDADO DE UM USER CONTROL
            DevExpress.XtraGrid.Views.Grid.GridView gridView1 = (DevExpress.XtraGrid.Views.Grid.GridView)gridObject1.gridControl1.Views[0];
            // FIM DO TRATAMENTO ESPECIAL

            // Tratamentos para a grid
            // Grid somente leitura
            //gridView1.OptionsBehavior.Editable = false;
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].Name == "colQTD_A_PROGRAMAR")
                {
                    gridView1.Columns[i].OptionsColumn.AllowEdit = true;
                    
                }
                else
                {
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }
            }


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

            //Desabilita Colunas PRODUTOCOMPOSTO e MATERIALINTERLIGACAO
            gridView1.Columns["CODCOLIGADA"].Visible = false;
            
            // Grid sempre mostrar filtro no rodapé
            // gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;

            // Grid zebrada
            // gridView1.OptionsView.EnableAppearanceEvenRow = true;
            // gridView1.Appearance.EvenRow.BackColor = Color.Thistle;
        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {
            DataRowCollection drc = null;
            drc = gridData2.GetDataRows();
            if (drc == null)
            {
                return;
            }
            for (int i = 0; i < drc.Count; i++)
            {
                AppLib.Context.poolConnection.Get().ExecTransaction("DELETE FROM ZPROGRAMACAOCARREGAMENTO WHERE IDPROGRAMACAOCARREGAMENTO = ?", new object[] { drc[i]["IDPROGRAMACAOCARREGAMENTO"]});    
            }
            ListaItens.Clear();
            gridObject1_Atualizar(this, null);
            gridData2.Atualizar(false);
        }
    }
}
