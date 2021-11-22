using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class FormRequisicaoPecasVisao : AppLib.Windows.FormVisao
    {
        private static FormRequisicaoPecasVisao _instance = null;
        DateTime data;
        int qtde = 0;
        public static FormRequisicaoPecasVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormRequisicaoPecasVisao();
            return _instance;
        }

        //private bool AnexoItemMovimento = true;

        public FormRequisicaoPecasVisao()
        {
            InitializeComponent();
        }
        private void alterarDataProgramacao(object sender, EventArgs e)
        {
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
        //        private void enviaEmail(DataRowCollection _drc, int i)
        //        {
        //            //Configuração do e-mail.
        //            string consulta = @"SELECT ENDERECOEMAIL, PORTAEMAIL, USUARIOEMAIL, SENHAEMAIL, ENVIARCOMO FROM ZPARAMFATURE WHERE CODCOLIGADA = ?";
        //            System.Data.DataTable dtEmail = AppLib.Context.poolConnection.Get().ExecQuery(consulta, new object[] { AppLib.Context.Empresa });
        //            AppLib.Util.Email email = new AppLib.Util.Email();
        //            email.Host = dtEmail.Rows[0]["ENDERECOEMAIL"].ToString();
        //            email.Porta = Convert.ToInt32(dtEmail.Rows[0]["PORTAEMAIL"]);
        //            email.Usuario = dtEmail.Rows[0]["USUARIOEMAIL"].ToString();
        //            email.De = dtEmail.Rows[0]["ENVIARCOMO"].ToString();
        //            email.Senha = dtEmail.Rows[0]["SENHAEMAIL"].ToString();
        //            //
        //            //Enviar email
        //            DataRow dr = grid1.GetDataRow();
        //            //for (int i = 0; i < _drc.Count; i++)
        //            //{
        //                string sql = @"SELECT EMAIL FROM ZENVIAEMAIL WHERE EVENTO = ? AND CODCOLIGADA = ?";
        //                System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new object[] { "ALTERAÇÃO CARREGAMENTO", AppLib.Context.Empresa });
        //                for (int ii = 0; ii < dt.Rows.Count; ii++)
        //                {
        //                    email.Assunto = "Alteração do carregamento de número " + _drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString() + " do Pedido de número " + dr["NUMEROMOV"].ToString();
        //                    email.Para = dt.Rows[ii]["EMAIL"].ToString();
        //                    //Busca as informações da mensagem
        //                    sql = @"SELECT 
        //ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO, 
        //ZPROGRAMACAOCARREGAMENTO.IDMOV, 
        //TMOV.NUMEROMOV, 
        //FCFO.CODCFO, 
        //FCFO.NOMEFANTASIA,
        //TPRODUTO.CODIGOPRD,
        //TPRODUTO.NOMEFANTASIA PRODUTO
        //FROM 
        //ZPROGRAMACAOCARREGAMENTO 
        //INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
        //INNER JOIN TMOV ON TITMMOV.IDMOV = TMOV.IDMOV
        //INNER JOIN FCFO ON TMOV.CODCFO = FCFO.CODCFO 
        //INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD
        //WHERE ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO = ?";
        //                    DataTable dt1 = AppLib.Context.poolConnection.Get().ExecQuery(sql, new object[] { _drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString() });
        //                    //email.Para = @"fabio.campos@itinit.com.br";
        //                    email.Mensagem = @"
        //<!DOCTYPE html>
        //    <html>
        //        <head>
        //            <title>Alerta sobre alteração da programação do carregamento - PALINI & ALVES</title>
        //        <style type=""text/css"">  
        //            body    {
        //                font-family: monospace, serif, arial, times;
        //                color: black;background-color: #FFFFFF;
        //                    }  
        //            table   {
        //                border-color:#F9F9F9;
        //                    }
        //        </style>
        //        </head>
        //        <body>
        //            <table align=""center"" border=""1"" style=""width:90%"" cellspacing=""0"" cellpadding=""12"">
        //                <tr>
        //                <td bgcolor=""#F1F1F1"">
        //                    <font face=""Verdana"" size=""4"">
        //                   <b>Alteração do carregamento de número " + dt1.Rows[0]["IDPROGRAMACAOCARREGAMENTO"].ToString() + " do Pedido de número " + dt1.Rows[0]["NUMEROMOV"].ToString() + "</b></font></td></tr><tr><td>";
        //                    email.Mensagem += @"
        //                    <table>
        //                        <tr>
        //                       <td>
        //                           <b>Alteração da data da programação do carregamento -- IDMOV = " + dt1.Rows[0]["IDMOV"].ToString() + "  CLIENTE = " + dt1.Rows[0]["CODCFO"].ToString() + " - " + dt1.Rows[0]["NOMEFANTASIA"].ToString() + " - " + dt1.Rows[0]["CODIGOPRD"].ToString() + " - " + dt1.Rows[0]["PRODUTO"].ToString() + "</b></td>                       <td></td></tr></table></td></tr>";
        //                    email.Mensagem += @"
        //            <tr>
        //            <td>
        //                <table>
        //                  <tr>
        //                  <td>
        //<b>Data antiga: " + Convert.ToDateTime(_drc[i]["DATAPROGRAMADA"]).ToShortDateString() + " -- Nova data: " + data.ToShortDateString() + " </b></td></tr></table></td></tr><br><br></body></html>";
        //                }
        //                bool enviou = email.Enviar();
        //          //  }
        //        }
        //        private void enviaEmail(DataRow _dr)
        //        {
        //            //Configuração do e-mail.
        //            string consulta = @"SELECT ENDERECOEMAIL, PORTAEMAIL, USUARIOEMAIL, SENHAEMAIL, ENVIARCOMO FROM ZPARAMFATURE WHERE CODCOLIGADA = ?";
        //            System.Data.DataTable dtEmail = AppLib.Context.poolConnection.Get().ExecQuery(consulta, new object[] { AppLib.Context.Empresa });
        //            AppLib.Util.Email email = new AppLib.Util.Email();
        //            email.Host = dtEmail.Rows[0]["ENDERECOEMAIL"].ToString();
        //            email.Porta = Convert.ToInt32(dtEmail.Rows[0]["PORTAEMAIL"]);
        //            email.Usuario = dtEmail.Rows[0]["USUARIOEMAIL"].ToString();
        //            email.De = dtEmail.Rows[0]["ENVIARCOMO"].ToString();
        //            email.Senha = dtEmail.Rows[0]["SENHAEMAIL"].ToString();
        //            //
        //            //Enviar email
        //            DataRow dr = grid1.GetDataRow();
        //            string sql = @"SELECT EMAIL FROM ZENVIAEMAIL WHERE EVENTO = ? AND CODCOLIGADA = ?";
        //            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new object[] { "ALTERAÇÃO CARREGAMENTO", AppLib.Context.Empresa });
        //            for (int ii = 0; ii < dt.Rows.Count; ii++)
        //            {
        //                email.Assunto = "Alteração do carregamento de número " + _dr["IDPROGRAMACAOCARREGAMENTO"].ToString() + " do Pedido de número " + _dr["NUMEROMOV"].ToString();
        //                email.Para = dt.Rows[ii]["EMAIL"].ToString();
        //                //Busca as informações da mensagem
        //                sql = @"SELECT 
        //ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO, 
        //ZPROGRAMACAOCARREGAMENTO.IDMOV, 
        //TMOV.NUMEROMOV, 
        //FCFO.CODCFO, 
        //FCFO.NOMEFANTASIA,
        //TPRODUTO.CODIGOPRD,
        //TPRODUTO.NOMEFANTASIA PRODUTO
        //FROM 
        //ZPROGRAMACAOCARREGAMENTO 
        //INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
        //INNER JOIN TMOV ON TITMMOV.IDMOV = TMOV.IDMOV
        //INNER JOIN FCFO ON TMOV.CODCFO = FCFO.CODCFO 
        //INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD
        //WHERE ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO = ?";
        //                DataTable dt1 = AppLib.Context.poolConnection.Get().ExecQuery(sql, new object[] { _dr["IDPROGRAMACAOCARREGAMENTO"].ToString() });
        //               //email.Para = @"fabio.campos@itinit.com.br";
        //                email.Mensagem = @"
        //<!DOCTYPE html>
        //    <html>
        //        <head>
        //            <title>Alerta sobre alteração da programação do carregamento - PALINI & ALVES</title>
        //        <style type=""text/css"">  
        //            body    {
        //                font-family: monospace, serif, arial, times;
        //                color: black;background-color: #FFFFFF;
        //                    }  
        //            table   {
        //                border-color:#F9F9F9;
        //                    }
        //        </style>
        //        </head>
        //        <body>
        //            <table align=""center"" border=""1"" style=""width:90%"" cellspacing=""0"" cellpadding=""12"">
        //                <tr>
        //                <td bgcolor=""#F1F1F1"">
        //                    <font face=""Verdana"" size=""4"">
        //                  <b>Alteração do carregamento de número " + dt1.Rows[0]["IDPROGRAMACAOCARREGAMENTO"].ToString() + " do Pedido de número " + dr["NUMEROMOV"].ToString() + "</b></font></td></tr><tr><td>";
        //                email.Mensagem += @"
        //                    <table>
        //                        <tr>
        //                       <td>
        //                           <b>Alteração da Quantidade do item da programação do carregamento -- IDMOV = " + dt1.Rows[0]["IDMOV"].ToString() + "  CLIENTE = " + dt1.Rows[0]["CODCFO"].ToString() + " - " + dt1.Rows[0]["NOMEFANTASIA"].ToString() + " - " + dt1.Rows[0]["CODIGOPRD"].ToString() + " - " + dt1.Rows[0]["PRODUTO"].ToString() + "</b></td>                       <td></td></tr></table></td></tr>";
        //                email.Mensagem += @"
        //            <tr>
        //            <td>
        //                <table>
        //                  <tr>
        //                  <td>
        //<b>Quantidade antiga: " + _dr["QTDE"].ToString() + " -- Quantidade Nova: " + qtde.ToString() + " </b></td></tr></table></td></tr><br><br></body></html>";
        //            }
        //            bool enviou = email.Enviar();
        //        }

        private void alterarQuantidadeProgramacao(object sender, EventArgs e)
        {

            DataRow dr = null;
            try
            {
                dr = gridData2.GetDataRow();
            }
            catch { }

            if (dr == null)
                return;
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
                    qtde = int.Parse(promt.textBox1.Text);
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
                gridData2.Atualizar(false);
            }
        }

        public void consultaProgramacao(object sender, EventArgs e)
        {

            DataRow dr = null;
            try
            {
                dr = grid1.GetDataRow();
            }
            catch { }

            if (dr == null)
                return;
            //selecao = 3;
            //AnexoItemMovimento = false;
            hideTabPage hide = new hideTabPage();
            tabPage3.Text = "Consultar Programação Carregamento";
            hide.ShowTabPage(tabPage3, tabControl1);
            if (splitContainer1.Panel2Collapsed.Equals(true))
            {
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                this.gridData2.Atualizar(false);
            }
        }
        public void agendarProgramacao(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8033", AppLib.Context.Perfil))
            {
                DateTime data = System.DateTime.Now;
                DataRowCollection drc = grid1.GetDataRows();
                FormProgramacaoCarregamento f = new FormProgramacaoCarregamento();
                f.drc = drc;
                f.ShowDialog();
            }
        }
        //Programação do Carregamento
        private void programacaoCarregamento(object sender, EventArgs e)
        {
            DateTime data = System.DateTime.Now;
            DataRowCollection drc = gridData1.GetDataRows();
            FormProgramacaoCarregamento f = new FormProgramacaoCarregamento();
            f.drc = drc;
            f.ShowDialog();
            gridData1.Atualizar(false);
        }

        private void FormRequisicaoPecasVisao_Load(object sender, EventArgs e)
        {
            //AnexoItemMovimento = false;

            grid1.GetAnexos().Add("Item do Movimento", null, ItemMovimento);
            grid1.GetProcessos().Add("Programar Carregamento", null, agendarProgramacao);
            grid1.GetAnexos().Add("Consultar Programação Carregamento", null, consultaProgramacao);
            grid1.GetAnexos().Add("Fechar Anexo", null, FecharAnexo);
            grid1.GetAnexos().Add("Fechar todos Anexos", null, FecharAnexos);

            grid1.GetProcessos().Add("Imprimir Requisição de Peças", null, ImprimirRequisicaoPecas);
            grid1.GetProcessos().Add("Imprimir Peças Pendentes", null, ImprimirPecasPendentes);
            //grid1.GetProcessos().Add("Faturar Movimento", null, FaturarMovimento);

            grid1.GetProcessos().Add("-", null, null);

            grid1.GetProcessos().Add("Cancelar Movimento", null, CancelarMovimento);
            grid1.GetProcessos().Add("Cópia de Movimento", null, CopiaDeMovimento);
            grid1.GetProcessos().Add("Rastreamento de Movimentos", null, Rastreabilidade);
            //Criação dos processos na grid de programação do carregamento.
            gridData1.GetProcessos().Add("Programar Carregamento", null, programacaoCarregamento);
            //Criação dos processos na grid de consulta da programação do carregamento.
            gridData2.GetProcessos().Add("Alterar data da programação.", null, alterarDataProgramacao);
            gridData2.GetProcessos().Add("Alterar quantidade da programação.", null, alterarQuantidadeProgramacao);
            hideTabPage hide = new hideTabPage();
            hide.HideTabPage(tabPage1, tabControl1);
            hide.HideTabPage(tabPage2, tabControl1);
            hide.HideTabPage(tabPage3, tabControl1);
        }

        public void FecharAnexo(object sender, EventArgs e)
        {
            hideTabPage hide = new hideTabPage();
            try
            {
                hide.HideTabPage(tabControl1.SelectedTab, tabControl1);
            }
            catch (Exception)
            {
            }
            if (tabControl1.TabPages.Count.Equals(0))
            {
                // AnexoItemMovimento = false;
                // selecao = 0;
                splitContainer1.Panel2Collapsed = true;
            }
        }
        public void FecharAnexos(object sender, EventArgs e)
        {
            try
            {
                //   AnexoItemMovimento = false;
                hideTabPage hide = new hideTabPage();
                hide.HideTabPage(tabPage1, tabControl1);
                hide.HideTabPage(tabPage2, tabControl1);
                hide.HideTabPage(tabPage3, tabControl1);
                splitContainer1.Panel2Collapsed = true;
                //selecao = 0;
            }
            catch { }
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormRequisicaoPecasCadastro().Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormRequisicaoPecasCadastro frm = new FormRequisicaoPecasCadastro();
            frm.row = grid1.GetDataRow();
            frm.Editar(grid1.bs);
            //new FormRequisicaoPecasCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8043", AppLib.Context.Perfil))
            {
                if (MessageBox.Show("Deseja excluir o movimento ?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                //Verifciar se existe carregamento para o IDMOV
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataRowCollection Movimentos = grid1.GetDataRows();
                    if (Movimentos != null)
                    {
                        for (int i = 0; i < Movimentos.Count; i++)
                        {
                            string sql = @"SELECT DISTINCT IDCARREGAMENTO FROM ZCARREGAMENTOITEMS WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            string retorno = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sql, new Object[] { Movimentos[i]["CODCOLIGADA"], Movimentos[i]["IDMOV"] }).ToString();
                            if (!string.IsNullOrEmpty(retorno))
                            {
                                MessageBox.Show("Não é permitido a exclusão de movimentos que já que possuem carregamento(s).", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }
                            excluirItem(Convert.ToInt32(Movimentos[i]["CODCOLIGADA"]), Convert.ToInt32(Movimentos[i]["IDMOV"]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void excluirItem(int codcoligada, int idmov)
        {
            AppInterop.MovExclusaoPar exclusao = new AppInterop.MovExclusaoPar();
            exclusao.CodColigada = AppLib.Context.Empresa;
            exclusao.CodSistemaLogado = "T";
            exclusao.CodUsuarioLogado = AppLib.Context.Usuario;
            exclusao.IdMov = Convert.ToInt32(idmov);

            AppInterop.Message msgexclusao;
            if (FatureContexto.Remoto)
            {
                msgexclusao = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.ExcluiMovimento(AppLib.Context.Usuario, AppLib.Context.Senha, new Util().ConvertToWSMovExclusaoPar(exclusao)));
            }
            else
            {
                msgexclusao = FatureContexto.ServiceClient.ExcluiMovimento(AppLib.Context.Usuario, AppLib.Context.Senha, exclusao);
            }

            this.Cursor = Cursors.Default;

            if (int.Parse(msgexclusao.Retorno.ToString()) > 0)
            {
                string sMensagem = string.Empty;
                bool bExclusao = false;
                while (!bExclusao)
                {
                    string sSql = @"SELECT STATUS FROM ZTMOVCANEXC WHERE CODCOLIGADA = ? AND IDMOV = ?";
                    string sStatus = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sSql, new Object[] { codcoligada, idmov }).ToString();

                    if (sStatus != "A")
                    {
                        if (sStatus == "R")
                        {
                            //System.Threading.Thread.Sleep(5000); //5s                                    
                        }

                        if (sStatus == "S")
                        {
                            bExclusao = true;
                            msgexclusao.Mensagem = "Exclusão realizada com sucesso";
                        }

                        if (sStatus == "E")
                        {
                            bExclusao = true;

                            sSql = "SELECT MENSAGEM FROM ZTMOVCANEXC WHERE OPERACAO = 'E' AND CODCOLIGADA = " + codcoligada + " AND IDMOV = " + idmov;
                            msgexclusao.Mensagem = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { }).ToString();
                            throw new Exception(msgexclusao.Mensagem);
                        }
                    }
                    else
                    {
                        //System.Threading.Thread.Sleep(5000); //5s
                    }
                }

                MessageBox.Show(msgexclusao.Mensagem);
            }
            else
            {
                throw new Exception(msgexclusao.Mensagem);
            }
        }
        public void ItemMovimento(object sender, EventArgs e)
        {
            hideTabPage hide = new hideTabPage();
            tabPage1.Text = "Item de Movimento";
            hide.ShowTabPage(tabPage1, tabControl1);
            DataRow dr = null;
            //selecao = 0;
            try
            {
                dr = grid1.GetDataRow();
            }
            catch { }

            if (dr == null)
                return;

            // AnexoItemMovimento = true;
            string ConsultaFiltro = string.Empty;

            try
            {
                splitContainer1.Panel2Collapsed = false;

                ConsultaFiltro = @"
SELECT
TITMMOV.NSEQITMMOV,
TITMMOV.IDPRD,
TPRD.CODIGOPRD,
ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
TITMMOV.CODUND,
CAST(TITMMOV.QUANTIDADEORIGINAL AS NUMERIC(15,2)) QUANTIDADE,
CAST(TITMMOV.PRECOUNITARIO AS NUMERIC(15,2)) PRECOUNITARIO,
TPRD.NUMEROCCF,

CASE 
WHEN TITMMOVCOMPL.PRODPRICIPAL = 'NÃO' THEN 0
WHEN TITMMOVCOMPL.PRODPRICIPAL = 'SIM' THEN 1
ELSE NULL
END PRODPRICIPAL,

TITMMOVCOMPL.PRODPRICIPAL PRODUTO_COMPOSTO,

CASE 
WHEN TITMMOVCOMPL.TIPOMAT = 0 THEN 'SIM'
WHEN TITMMOVCOMPL.TIPOMAT = 1 THEN 'NÃO'
ELSE NULL
END MATERIAL_INTERLIGACAO,

TITMMOVCOMPL.SEQUENCIAL

FROM TITMMOV
LEFT JOIN TITMMOVCOMPL ON TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA
  AND TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV
  AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
LEFT JOIN TITMMOVHISTORICO ON TITMMOV.CODCOLIGADA = TITMMOVHISTORICO.CODCOLIGADA
  AND TITMMOV.IDMOV = TITMMOVHISTORICO.IDMOV
  AND TITMMOV.NSEQITMMOV = TITMMOVHISTORICO.NSEQITMMOV,
  TPRD

WHERE TITMMOV.CODCOLIGADA = TPRD.CODCOLIGADA
  AND TITMMOV.IDPRD = TPRD.IDPRD
  AND TITMMOV.CODCOLIGADA = ?
  AND TITMMOV.IDMOV = ?";
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

        public void ImprimirPecasPendentes(object sender, EventArgs e)
        {

            FormRequisicaoDevolucaoFiltro f = new FormRequisicaoDevolucaoFiltro();
            f.ShowDialog();


            RelRequisicaoPecasPendentes report = new RelRequisicaoPecasPendentes();


            report.DtaInicio = f.DtaInicio;
            report.DtaFim = f.DtaFim;
            report.Representante = f.Representante;
            report.Status = f.Status;
            report.Cliente = f.Cliente;


            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.ShowPreviewDialog();
            }
        }

        public void ImprimirRequisicaoPecas(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8023", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    System.Data.DataRow dr = grid1.GetDataRow();

                    string Consulta = @"UPDATE TITMMOV SET TITMMOV.QUANTIDADEORIGINAL = TITMMOV.QUANTIDADETOTAL 
                                    FROM  TITMMOV, TMOV 
                                    WHERE TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA
                                    AND TMOV.IDMOV = TITMMOV.IDMOV
                                    AND TMOV.CODTMV IN ('2.1.03')
                                    AND TITMMOV.CODCOLIGADA = ?
                                    AND TITMMOV.IDMOV = ?";

                    Consulta = AppLib.Context.poolConnection.Get(grid1.Conexao).ParseCommand(Consulta, new Object[] { dr["CODCOLIGADA"], dr["IDMOV"] });
                    AppLib.Context.poolConnection.Get(grid1.Conexao).ExecTransaction(Consulta, new Object[] { });

                    RelRequisicaoPecas relatorio = new RelRequisicaoPecas();
                    relatorio.SelectRow = grid1.GetDataRow();
                    relatorio.Conexao = grid1.Conexao;

                    FormPrintPreview f = new FormPrintPreview();
                    f.SelectRow = grid1.GetDataRow();
                    f.Conexao = grid1.Conexao;
                    f.relatorio = relatorio;
                    f.ShowDialog();
                }
            }
        }

        public void CancelarMovimento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8025", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FormCancelarMovimento f = new FormCancelarMovimento();
                    f.Movimentos = grid1.GetDataRows();
                    f.ShowDialog();
                    grid1.toolStripButtonATUALIZAR_Click(this, null);
                }
            }
        }

        public void FaturarMovimento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8024", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    //if (grid1.GetDataRows().Count > 1)
                    //{
                    //    MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    FormFaturar f = new FormFaturar();
                    f.CodTmvOrigem = "2.1.05";
                    f.Movimentos = grid1.GetDataRows();
                    f.ShowDialog();
                    grid1.toolStripButtonATUALIZAR_Click(this, null);
                }
            }
        }

        public void CopiaDeMovimento(object sender, EventArgs e)
        {
            System.Data.DataRowCollection drc = grid1.GetDataRows();

            if (drc != null)
            {
                if (grid1.GetDataRows().Count > 1)
                {
                    MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FormRequisicaoPecasCadastro f = new FormRequisicaoPecasCadastro();
                f.CopiaDeMovimento = true;
                f.Editar(grid1.bs);

                grid1.toolStripButtonATUALIZAR_Click(this, null);
            }
        }

        public void Rastreabilidade(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FormRastreabilidade f = new FormRastreabilidade();
                    f.Movimento = grid1.GetDataRow();
                    f.ShowDialog();
                }
            }
            catch
            { }
        }

        private void grid1_EventoClick(object sender, EventArgs e)
        {
            if (tabControl1.TabCount.Equals(0))
            {
                return;
            }
            if (tabControl1.SelectedTab.Text.Equals("Item de Movimento"))
            {
                ItemMovimento(this, null);
                return;
            }

            if (tabControl1.SelectedTab.Text.Equals("Programar Carregamento"))
            {
                agendarProgramacao(this, null);
                return;
            }
            if (tabControl1.SelectedTab.Text.Equals("Consultar Programação Carregamento"))
            {
                consultaProgramacao(this, null);
                return;
            }
        }

        private void FormRequisicaoPecasVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {

            try
            {
                gridData1.Parametros = new Object[] { grid1.GetDataRow()["IDMOV"] };
            }
            catch (Exception)
            {
                gridData1.Parametros = new object[] { 0 };
            }
        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {

            if (new AppLib.Security.Access().Processo("Start", "APP8043", AppLib.Context.Perfil))
            {
                if (MessageBox.Show("Para efetuar essa exclusão, todos os itens do romaneio serão excluído.\nDeseja realmente exluir esse item?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    DataRowCollection drc = gridData2.GetDataRows();
                    for (int i = 0; i < drc.Count; i++)
                    {
                        AppLib.Context.poolConnection.Get().ExecQuery("delete ZPROGRAMACAOCARREGAMENTO where IDPROGRAMACAOCARREGAMENTO = ?", drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString());
                    }
                }

            }


            gridData2.Atualizar(false);

        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            try
            {
                gridData2.Parametros = new Object[] { grid1.GetDataRow()["IDMOV"] };
            }
            catch (Exception)
            {
                gridData2.Parametros = new object[] { 0 };
            }
        }


    }
}
