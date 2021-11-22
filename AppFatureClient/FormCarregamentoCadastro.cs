using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormCarregamentoCadastro : AppLib.Windows.FormCadastroData
    {
        private static FormCarregamentoCadastro _instance = null;
        string sql;

        // João Pedro Luchiari - 29//12/2017

         public DataRow row;

        public static FormCarregamentoCadastro GetInstance()
        {
            if (_instance == null)
                _instance = new FormCarregamentoCadastro();
            return _instance;
        }
        public FormCarregamentoCadastro()
        {
            InitializeComponent();
            campoTexto2.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTexto2.textEdit1.Properties.Mask.EditMask = "AAAAAAA";

            campoTexto6.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTexto6.textEdit1.Properties.Mask.EditMask = "999999";
            //campoTexto7.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            //campoTexto7.textEdit1.Properties.Mask.EditMask = "99";
        }

        private void FormCarregamentoCadastro_Load(object sender, EventArgs e)
        {
            campoTexto4.Set(AppLib.Context.Empresa.ToString());
            campoTexto8.textEdit1.Text = "EM ABERTO";
            if (this.Acao.Equals(AppLib.Global.Types.Acao.Novo))
            {
                campoHora1.Set(AppLib.Context.poolConnection.Get().GetDateTime());
                campoData2.Set(AppLib.Context.poolConnection.Get().GetDateTime());
            }

            //this.GetProcessos().Add("Compor Romaneio", null, comporRomaneio);
            gridData3.GetProcessos().Add("Compor Carregamento Completo", null, moveCarregamento);
            gridData3.GetProcessos().Add("Compor Carregamento Parcial", null, moveCarregamentoParcial);
            gridData3.GetProcessos().Add("Alterar a data de programação do carregamento.", null, alterarDataProgramacao);
            gridData3.GetProcessos().Add("Alterar a quantidade do item da programação carregamento.", null, alterarQuantidadeProgramacao);
            gridData4.GetProcessos().Add("Alterar programação do carregamento.", null, alterarDataProgramacaoReprogramar);
            gridData4.GetProcessos().Add("Compor Carregamento", null, moveCarregamentoReprogramar);
            gridData4.GetProcessos().Add("Cancelar Programação", null, CancelarProgramacao);
            gridData2.GetProcessos().Add("Mover para programação", null, moveProgramacao);
            gridData2.GetProcessos().Add("Alterar Caminhão", null, alterarCaminhao);
            gridData1.GetProcessos().Add("Compor Programação", null, programacaoCarregamento);
        }

        private void CancelarProgramacao(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DataRowCollection drc = null;
                drc = gridData4.GetDataRows();
                if (drc == null)
                {
                    return;
                }
                if (MessageBox.Show("Deseja realmente cancelar essa programação?", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    for (int i = 0; i < drc.Count; i++)
                    {
                        // cancela operação.
                        AppLib.Context.poolConnection.Get().ExecTransaction(@"DELETE FROM ZPROGRAMACAOCARREGAMENTO WHERE IDPROGRAMACAOCARREGAMENTO = ? AND CODCOLIGADA = ?", new object[] { drc[i]["IDPROGRAMACAOCARREGAMENTO"], AppLib.Context.Empresa });
                    }
                    gridData4.Atualizar(false);
                }
            }
        }

        private void alterarDataProgramacaoReprogramar(object sender, EventArgs e)
        {
            DataRowCollection drc = gridData4.GetDataRows();
            FormProgramacaoCarregamento f = new FormProgramacaoCarregamento(true);
            f.drc = drc;
            f.ShowDialog();
            gridData1.Atualizar(false);
            gridData3.Atualizar(false);
            gridData2.Atualizar(false);
            gridData4.Atualizar(false);
        }
        private void alterarCaminhao(object sender, EventArgs e)
        {

            DataRow dr = gridData2.GetDataRow();
            FormAlterarCaminhao frm = new FormAlterarCaminhao();
            frm.dataCarregamento = Convert.ToDateTime(campoData1.Get());
            frm.idProgramacaoCarregamento = Convert.ToInt32(dr["IDPROGRAMACAOCARREGAMENTO"]);
            frm.ShowDialog();
            gridData2.Atualizar(false);
        }
        //Compor Romaneio
        private void comporRomaneio(object sender, EventArgs e)
        {
            string sql = string.Empty;

            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DataRowCollection drc = null;
                try
                {
                    drc = gridData3.GetDataRows();
                }
                catch (Exception)
                {


                }
                if (drc == null)
                    return;

                try
                {
                    //Inserir ZROMANEIO
                    for (int i = 0; i < drc.Count; i++)
                    {
                        sql = @"INSERT INTO ZROMANEIO (IDPROGRAMACAOCARREGAMENTO, DATAAGENDAMENTO) VALUES (?, ?)";
                        int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString(), AppLib.Context.poolConnection.Get().GetDateTime() });
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Atualizar as grids
                //gridData1.Atualizar(false);
            }
        }
        //Programação do Carregamento
        private void programacaoCarregamento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DateTime data = System.DateTime.Now;
                DataRowCollection drc = gridData1.GetDataRows();
                FormProgramacaoCarregamento f = new FormProgramacaoCarregamento();
                f.drc = drc;
                f.idcarregamento = Convert.ToInt32(campoIdCarregamento.Get());
                f.ShowDialog();
                gridData1.Atualizar(false);
                gridData3.Atualizar(false);
                gridData2.Atualizar(false);
                gridData4.Atualizar(false);
            }
        }
        private void alterarDataProgramacao(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DataRow dr = null;
                try
                {
                    dr = gridData3.GetDataRow();
                }
                catch (Exception)
                {


                }
                if (dr == null)
                    return;
                AppLib.ORM.Jit ZPROGRAMACAOCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZPROGRAMACAOCARREGAMENTO");
                if (MessageBox.Show("Deseja alterar a data de programação nos itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    AppLib.Windows.FormMessagePrompt promt = new AppLib.Windows.FormMessagePrompt();
                    promt.ShowDialog();
                    if (promt.confirmacao.Equals(AppLib.Global.Types.Confirmacao.Cancelar))
                    {
                        return;
                    }
                    DateTime data;
                    try
                    {
                        data = Convert.ToDateTime(promt.textBox1.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor digitar uma data válida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ZPROGRAMACAOCARREGAMENTO.Set("IDPROGRAMACAOCARREGAMENTO", int.Parse(dr["IDPROGRAMACAOCARREGAMENTO"].ToString()));
                    ZPROGRAMACAOCARREGAMENTO.Set("DATAPROGRAMADA", data);
                    ZPROGRAMACAOCARREGAMENTO.Set("DATAALTERACAO", AppLib.Context.poolConnection.Get().GetDateTime());
                    ZPROGRAMACAOCARREGAMENTO.Save();
                    new ValidaEnvioEmail().validaEnvio(data, gridData3.GetDataRows(), "data", 0);
                    MessageBox.Show("Alteração realizada com sucesso.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gridData3.Atualizar(false);
                }
            }
        }
        private void alterarQuantidadeProgramacao(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DataRow dr = null;
                try
                {
                    dr = gridData3.GetDataRow();
                }
                catch (Exception)
                {


                }
                if (dr == null)
                    return;
                int qtde = 0;
                //dr = gridData3.GetDataRow();
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
		INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
		 ) QTD_PROG
		--INFORMANDO AS TABELAS DA PESQUISA
		FROM  TITMMOV
		) SUBLISTA
--FECHA SELECT PAI		
) LISTA
--WHERE SELECT PAI
WHERE LISTA.IDMOV = ? and LISTA.NSEQITMMOV = ?";
                    //string[] IDMOV = dr[0].ToString().Split(new char[] { '-' });

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

                    new ValidaEnvioEmail().validaEnvio(Convert.ToDateTime(dr["DATAPROGRAMADA"]), gridData3.GetDataRows(), "quantidade", qtde);
                    MessageBox.Show("Alteração realizada com sucesso.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gridData3.Atualizar(false);
                }
            }
        }
        private void moveCarregamento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DataRowCollection drc = null;
                try
                {
                    drc = gridData3.GetDataRows();
                }
                catch (Exception)
                {


                }
                if (drc == null)
                    return;
                if (campoIdCarregamento.Get().Equals(null))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(campoLookup1.Get()) || string.IsNullOrEmpty(campoTexto2.textEdit1.Text))
                        {
                            MessageBox.Show("Favor prencher os campos do cabeçalho corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        this.dropDownButtonSalvar_Click(sender, e);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor prencher os campos do cabeçalho corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                try
                {
                    //Verificar o Status do carregamento.
                    sql = @"SELECT STATUS FROM ZCARREGAMENTO WHERE IDCARREGAMENTO = ?";
                    object status = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new object[] { campoIdCarregamento.Get() });
                    if (status.Equals("COMPLETO"))
                    {
                        MessageBox.Show("Esse carregamento já foi baixado e não pode ser mais alterado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    else
                    {
                        for (int i = 0; i < drc.Count; i++)
                        {
                            //sql = @"UPDATE ZPROGRAMACAOCARREGAMENTO SET CARREGAMENTO = 'S', IDCARREGAMENTO = ?, CARREGAR = 'COMPLETO' WHERE IDPROGRAMACAOCARREGAMENTO = ?";
                            AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE ZPROGRAMACAOCARREGAMENTO SET CARREGAMENTO = 'S', IDCARREGAMENTO = ?, CARREGAR = ?, QTDE = 0, QTDCARREGADA = ? WHERE IDPROGRAMACAOCARREGAMENTO = ?", new Object[] { campoIdCarregamento.Get(), "COMPLETO", Convert.ToDecimal(drc[i]["QTDE"]), drc[i]["IDPROGRAMACAOCARREGAMENTO"] });

                            //int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { campoIdCarregamento.Get(), drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString() });
                        }
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Atualizar as grids
                gridData3.Atualizar(false);
                gridData2.Atualizar(false);
            }
            //if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            //{
            //    DataRowCollection drc = null;
            //    try
            //    {
            //        drc = gridData3.GetDataRows();
            //    }
            //    catch (Exception)
            //    {


            //    }
            //    if (drc == null)
            //        return;
            //    if (campoIdCarregamento.Get().Equals(null))
            //    {
            //        try
            //        {
            //            if (string.IsNullOrEmpty(campoLookup1.Get()))
            //            {
            //                MessageBox.Show("Favor prencher os campos do cabeçalho corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }
            //            this.dropDownButtonSalvar_Click(sender, e);
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show("Favor prencher os campos do cabeçalho corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }
            //    }
            //    try
            //    {
            //        //drc = gridData3.GetDataRows();
            //        //Verificar o Status do carregamento.
            //        string status = string.Empty;
            //        string aaaa = string.Empty;
            //        int qtd = 0;
            //        List<Classes.ItensCarregamento> idmov = new List<Classes.ItensCarregamento>();

            //        for (int i = 0; i < drc.Count; i++)
            //        {
            //            Classes.ItensCarregamento item = new Classes.ItensCarregamento();
            //            item.IDMOV = Convert.ToInt32(drc[i]["IDMOV"]);
            //            item.IDPROGRAMACAOCARREGAMENTO = Convert.ToInt32(drc[i]["IDPROGRAMACAOCARREGAMENTO"]);
            //            item.QTD_CARREGADA = Convert.ToDecimal(drc[i]["QTDE"]);
            //            idmov.Add(item);
            //        }
            //        //

            //        //Quantidade de itens selecionados referente ao IDMOV
            //        for (int i = 0; i < idmov.Count; i++)
            //        {
            //            aaaa = idmov[i].IDMOV.ToString();
            //            for (int ii = 0; ii < idmov.Count; ii++)
            //            {
            //                if (idmov[ii].IDMOV.ToString() == aaaa)
            //                {
            //                    qtd = qtd + 1;
            //                }

            //            }
            //            DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT * FROM ZPROGRAMACAOCARREGAMENTO WHERE IDMOV = ? AND (IDCARREGAMENTO = 0 OR IDCARREGAMENTO IS NULL)", new object[] { idmov[i].IDMOV });
            //            if (dt.Rows.Count > qtd)
            //            {
            //                idmov[i].STATUS = "PARCIAL";
            //            }
            //            else
            //            {
            //                idmov[i].STATUS = "COMPLETO";
            //            }

            //            qtd = 0;
            //        }
            //        /////////////////////////////////////////////////////
            //        var groupedCustomerList = idmov.GroupBy(u => u.IDMOV).Select(grp => grp.ToList()).ToList();
            //        for (int i = 0; i < idmov.Count; i++)
            //        {
            //            AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE ZPROGRAMACAOCARREGAMENTO SET CARREGAMENTO = 'S', IDCARREGAMENTO = ?, CARREGAR = ?, QTDE = NULL, QTDCARREGADA = ? WHERE IDPROGRAMACAOCARREGAMENTO = ?", new Object[] { campoIdCarregamento.Get(), idmov[i].STATUS, Convert.ToDecimal(idmov[i].QTD_CARREGADA), idmov[i].IDPROGRAMACAOCARREGAMENTO });
            //        }
            //        for (int i = 0; i < groupedCustomerList.Count; i++)
            //        {
            //            if (groupedCustomerList[i][0].STATUS == "COMPLETO")
            //            {
            //                AppLib.Context.poolConnection.Get().ExecTransaction("UPDATE ZPROGRAMACAOCARREGAMENTO SET CARREGAR = 'COMPLETO' WHERE IDCARREGAMENTO = ? AND IDMOV = ?", new object[] { campoIdCarregamento.Get(), groupedCustomerList[i][0].IDMOV });
            //            }
            //        }
            //    }

            //    catch (Exception)
            //    {
            //        MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    //Atualizar as grids
            //    gridData3.Atualizar(false);
            //    gridData2.Atualizar(false);
            //}
        }
        private void moveCarregamentoReprogramar(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DataRowCollection drc = null;
                drc = gridData4.GetDataRows();
                if (drc == null)
                {
                    return;
                }
                if (campoIdCarregamento.Get().Equals(null))
                {
                    try
                    {
                        this.dropDownButtonSalvar_Click(sender, e);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor prencher os campos do cabeçalho corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                try
                {
                    //Verificar o Status do carregamento.
                    sql = @"SELECT STATUS FROM ZCARREGAMENTO WHERE IDCARREGAMENTO = ?";
                    object status = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new object[] { campoIdCarregamento.Get() });
                    if (status.Equals("COMPLETO"))
                    {
                        MessageBox.Show("Esse carregamento já foi baixado e não pode ser mais alterado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < drc.Count; i++)
                        {
                            
                            sql = @"UPDATE ZPROGRAMACAOCARREGAMENTO SET CARREGAMENTO = 'S', IDCARREGAMENTO = ?, REPROGRAMAR = NULL, QTDE = 0, QTDCARREGADA = ? WHERE IDPROGRAMACAOCARREGAMENTO = ?";
                            int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { campoIdCarregamento.Get(), Convert.ToDecimal(drc[i]["QTDE"]), drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString() });
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Atualizar as grids
                gridData3.Atualizar(false);
                gridData2.Atualizar(false);
                gridData4.Atualizar(false);
            }
        }
        private void moveCarregamentoParcial(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DataRowCollection drc = null;
                try
                {
                    drc = gridData3.GetDataRows();
                }
                catch (Exception)
                {


                }
                if (drc == null)
                    return;
                if (campoIdCarregamento.Get().Equals(null))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(campoLookup1.Get()) || string.IsNullOrEmpty(campoTexto2.textEdit1.Text))
                        {
                            MessageBox.Show("Favor prencher os campos do cabeçalho corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        this.dropDownButtonSalvar_Click(sender, e);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor prencher os campos do cabeçalho corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                try
                {
                    //Verificar o Status do carregamento.
                    sql = @"SELECT STATUS FROM ZCARREGAMENTO WHERE IDCARREGAMENTO = ?";
                    object status = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new object[] { campoIdCarregamento.Get() });
                    if (status.Equals("COMPLETO"))
                    {
                        MessageBox.Show("Esse carregamento já foi baixado e não pode ser mais alterado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    else
                    {
                        ////Inserir ZCARREGAMENTOITEMS
                        //for (int i = 0; i < drc.Count; i++)
                        //{
                        //    sql = @"INSERT INTO ZCARREGAMENTOITEMS (IDCARREGAMENTO, CODCOLIGADA, IDMOV, NSEQITMMOV, QTDE, CARREGAR, CAMINHAO, TIPOCAMINHAO, DATAPROGRAMADA) VALUES (?,?,?,?,?,?,?,?,?)";
                        //    int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { campoIdCarregamento.Get(), AppLib.Context.Empresa, drc[i]["IDMOV"].ToString(), drc[i]["NSEQITMMOV"].ToString(), Convert.ToDecimal(drc[i]["QTDE"]), "PARCIAL", drc[i]["CAMINHAO"].ToString(), drc[i]["TIPOCAMINHAO"].ToString(), Convert.ToDateTime(drc[i]["DATAPROGRAMADA"].ToString()) });
                        //}

                        //Apagar ZPROGRAMACAOCARREGAMENTO
                        for (int i = 0; i < drc.Count; i++)
                        {
                            //sql = @"DELETE FROM ZPROGRAMACAOCARREGAMENTO WHERE IDPROGRAMACAOCARREGAMENTO = ?";
                            AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE ZPROGRAMACAOCARREGAMENTO SET CARREGAMENTO = 'S', IDCARREGAMENTO = ?, CARREGAR = ?, QTDE = 0, QTDCARREGADA = ? WHERE IDPROGRAMACAOCARREGAMENTO = ?", new Object[] { campoIdCarregamento.Get(), "PARCIAL", Convert.ToDecimal(drc[i]["QTDE"]), drc[i]["IDPROGRAMACAOCARREGAMENTO"] });

                        }
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Atualizar as grids
                gridData3.Atualizar(false);
                gridData2.Atualizar(false);
            }
        }
        private void moveProgramacao(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DataRowCollection drc = null;
                try
                {
                    drc = gridData2.GetDataRows();
                }
                catch (Exception)
                {


                }
                if (drc == null)
                    return;
                try
                {
                    if (campoIdCarregamento.Get().Equals(null))
                    {
                        MessageBox.Show("Favor cadastrar o cabeçalho primeiro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;

                    }
                    drc = gridData2.GetDataRows();
                    //Inserir ZCARREGAMENTOITEMS --> ZPROGRAMACAOCARREGAMENTO
                    for (int i = 0; i < drc.Count; i++)
                    {

                        //string sql = @"INSERT INTO ZPROGRAMACAOCARREGAMENTO (IDMOV, NSEQITMMOV, CODCOLIGADA, QTDE, DATAPROGRAMADA, CAMINHAO, TIPOCAMINHAO) VALUES (?,?,?,?,?,?,?)";
                        sql = @"UPDATE ZPROGRAMACAOCARREGAMENTO SET CARREGAMENTO = 'N', IDCARREGAMENTO = NULL, CARREGAR = NULL, QTDE = ?, QTDCARREGADA = 0  WHERE IDMOV = ? AND NSEQITMMOV = ?";
                        int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { Convert.ToDecimal(drc[i]["QTDCARREGADA"]), drc[i]["IDMOV"].ToString(), drc[i]["NSEQITMMOV"] });

                        // int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { drc[i]["IDMOV"].ToString(), drc[i]["NSEQITMMOV"].ToString(), AppLib.Context.Empresa, drc[i]["QTDE"].ToString(), Convert.ToDateTime(drc[i]["DATAPROGRAMADA"].ToString()), drc[i]["CAMINHAO"].ToString(), drc[i]["TIPOCAMINHAO"].ToString() });
                    }

                    //Apagar ZPROGRAMACAOCARREGAMENTO --> ZCARREGAMENTOITEMS
                    //for (int i = 0; i < drc.Count; i++)
                    //{
                    //    string sql = @"DELETE FROM ZCARREGAMENTOITEMS WHERE IDCARREGAMENTOITEMS = ?";
                    //    int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { drc[i]["IDCARREGAMENTOITEMS"].ToString() });
                    //}
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Atualizar as grids
                gridData3.Atualizar(false);
                gridData2.Atualizar(false);
            }
        }
        private void campoLookup1_AposSelecao(object sender, EventArgs e)
        {
            campoTexto3.Set(campoLookup1.Get().ToString());
        }



        //private void gridData1_Editar(object sender, EventArgs e)
        //{

        //    if (this.Acao.Equals(AppLib.Global.Types.Acao.Editar))
        //    {
        //        if (!string.IsNullOrEmpty(txtDataBaixa.Get()))
        //        {
        //            MessageBox.Show("Esse carregamento já foi baixado e não pode ser mais alterado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }

        //    if (campoIdCarregamento.Get().Equals(null))
        //    {
        //        dropDownButtonSalvar_Click(this, null);
        //    }

        //    FormCarregamentoItensCadastroVenda f = new FormCarregamentoItensCadastroVenda();
        //    f.Chave.Add(new AppLib.ORM.CampoValor("IDCARREGAMENTO", campoIdCarregamento.Get().ToString()));
        //    f.Chave.Add(new AppLib.ORM.CampoValor("CODCOLIGADA", AppLib.Context.Empresa.ToString()));
        //    f.Editar(gridData1.bs);
        //}

        //private void gridData1_Excluir(object sender, EventArgs e)
        //{
        //    if (this.Acao.Equals(AppLib.Global.Types.Acao.Editar))
        //    {
        //        if (!string.IsNullOrEmpty(txtDataBaixa.Get()))
        //        {
        //            MessageBox.Show("Esse carregamento já foi baixado e não pode ser mais alterado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }

        //    if (MessageBox.Show("Deseja realmente exluir esse item do carregamento?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
        //    {
        //        if (campoIdCarregamento.Get().Equals(null))
        //        {
        //            dropDownButtonSalvar_Click(this, null);
        //        }
        //        DataRowCollection drc = gridData1.GetDataRows();

        //        string sql = "DELETE FROM ZPROGRAMACAOCARREGAMENTO WHERE IDPROGRAMACAOCARREGAMENTO = ?";
        //        for (int i = 0; i < drc.Count; i++)
        //        {
        //            AppLib.Context.poolConnection.Get().ExecQuery(sql, drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString());
        //        }
        //    }
        //}

        private void gridData1_Novo(object sender, EventArgs e)
        {
            //if (this.Acao.Equals(AppLib.Global.Types.Acao.Editar))
            //{
            //    if (!string.IsNullOrEmpty(txtDataBaixa.Get()))
            //    {
            //        MessageBox.Show("Esse carregamento já foi baixado e não pode ser mais alterado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}

            //if (campoIdCarregamento.Get().Equals(null))
            //{
            //    campoTexto4.Set(AppLib.Context.Empresa.ToString());
            //    dropDownButtonSalvar_Click(this, null);
            //}
            //FormCarregamentoItensCadastroVenda f = new FormCarregamentoItensCadastroVenda();
            //f.Chave.Add(new AppLib.ORM.CampoValor("IDCARREGAMENTO", campoIdCarregamento.Get().ToString()));
            //f.Chave.Add(new AppLib.ORM.CampoValor("CODCOLIGADA", campoTexto4.Get().ToString()));
            //f.Novo();
        }

        //private void gridData1_SetParametros(object sender, EventArgs e)
        //{
        //    gridData1.Parametros = new Object[] { campoData1.Get() };

        //}

        private bool FormCarregamentoCadastro_ValidarExcluir(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get().ExecQuery("DELETE FROM ZCARREGAMENTOITEMS WHERE IDCARREGAMENTO = ?", campoIdCarregamento.Get().ToString());
            return true;
        }

        private void campoTexto2_Validating(object sender, CancelEventArgs e)
        {
            campoTexto2.textEdit1.Text = campoTexto2.textEdit1.Text.ToUpper();
        }

        private void campoTexto1_Validating(object sender, CancelEventArgs e)
        {
            campoTexto1.textEdit1.Text = campoTexto2.textEdit1.Text.ToUpper();
        }

        private void campoTexto2_Validating_1(object sender, CancelEventArgs e)
        {

            campoTexto2.textEdit1.Text = campoTexto2.textEdit1.Text.Replace("-", "");
            campoTexto2.textEdit1.Text = campoTexto2.textEdit1.Text.ToUpper();
            if (campoTexto2.textEdit1.Text.Length < 7)
            {
                MessageBox.Show("Favor digitar uma placa válida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string placa = campoTexto2.textEdit1.Text.Substring(0, 3);
            if (!System.Text.RegularExpressions.Regex.IsMatch(placa, "^[a-zA-Z]"))
            {
                MessageBox.Show("Favor digitar uma placa válida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void campoTexto6_Validating(object sender, CancelEventArgs e)
        {
            formataNumero();

        }
        private void formataNumero()
        {
            if (this.campoTexto6.textEdit1.Text.Length.Equals(1))
            {
                campoTexto6.textEdit1.Text = "00000" + campoTexto6.textEdit1.Text;
            }
            if (this.campoTexto6.textEdit1.Text.Length.Equals(2))
            {
                campoTexto6.textEdit1.Text = "0000" + campoTexto6.textEdit1.Text;
            }
            if (this.campoTexto6.textEdit1.Text.Length.Equals(3))
            {
                campoTexto6.textEdit1.Text = "000" + campoTexto6.textEdit1.Text;
            }
            if (this.campoTexto6.textEdit1.Text.Length.Equals(4))
            {
                campoTexto6.textEdit1.Text = "00" + campoTexto6.textEdit1.Text;
            }
            if (this.campoTexto6.textEdit1.Text.Length.Equals(5))
            {
                campoTexto6.textEdit1.Text = "0" + campoTexto6.textEdit1.Text;
            }
        }
        private bool FormCarregamentoCadastroVenda_ValidarSalvar(object sender, EventArgs e)
        {
            if (campoData1.Get() < campoData2.Get())
            {
                MessageBox.Show("Data Agendamento não pode ser menor que a data de autorização.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.Acao.Equals(AppLib.Global.Types.Acao.Editar))
            {
                sql = @"SELECT STATUS FROM ZCARREGAMENTO WHERE IDCARREGAMENTO = ?";
                object status = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new object[] { campoIdCarregamento.Get() });
                if (status.Equals("COMPLETO"))
                {
                    MessageBox.Show("Esse carregamento já foi baixado e não pode ser mais alterado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                sql = @"SELECT MAX(NUMERO) FROM ZCARREGAMENTO";
                int numero = Convert.ToInt32(AppLib.Context.poolConnection.Get().ExecGetField(0, sql));
                campoTexto6.textEdit1.Text = Convert.ToString(numero + 1);
                formataNumero();
            }

            if (row["STATUSOPERACAO"].ToString() == "CONCLUÍDO")
            {
                MessageBox.Show("Não é permitido editar carregamento 'concluído'.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //this.BotaoSalvar = false;
                //this.BotaoOK = false;
                return false;
            }
            return true;
        }

        private void campoTexto7_Validating(object sender, CancelEventArgs e)
        {
            //if (this.campoTexto7.textEdit1.Text.Length.Equals(1))
            //{
            //    campoTexto7.textEdit1.Text = "0" + campoTexto7.textEdit1.Text;
            //}
        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            FormTransportadoraVisao f = new FormTransportadoraVisao();
            f.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            //            return f.MostrarLookup(campoLookupCODCFO, "  AND PAGREC IN (1, 3) AND ATIVO = 1");
            return f.MostrarLookup(campoLookup1, "SELECT * FROM TTRA WHERE CODCOLIGADA = ? AND INATIVO = 0", new Object[] { AppLib.Context.Empresa });
        }

        private void campoData1_Validating(object sender, CancelEventArgs e)
        {
            gridData3.Atualizar(false);
        }

        private void gridData3_SetParametros(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(campoData2.Get().ToString()))
            //{
            //    gridData3.Parametros = new Object[] { campoData1.Get() };
            //}
            //else
            //{
            //    gridData3.Parametros = new Object[] { campoData2.Get() };
            //}
        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            if (campoIdCarregamento.Get().Equals(null))
            {
                gridData2.Parametros = new Object[] { null };
            }
            else
            {
                gridData2.Parametros = new Object[] { campoIdCarregamento.Get() };
            }


        }

        private void gridData3_Editar(object sender, EventArgs e)
        {
            moveCarregamento(sender, e);
        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            moveProgramacao(sender, e);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(campoData2.Get().ToString()))
            //{
            //    gridData3.Parametros = new Object[] { campoData2.Get() };
            //    gridData3.Atualizar(false);
            //}
        }

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TTRA WHERE CODCOLIGADA = ? AND CODTRA = ?";
            campoLookup1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup1.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup1.Get() }).ToString();
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void FormCarregamentoCadastro_AntesSalvar(object sender, EventArgs e)
        {

        }




    }
}
