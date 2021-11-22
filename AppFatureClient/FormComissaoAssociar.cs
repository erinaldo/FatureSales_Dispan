using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFatureClient.Classes;

namespace AppFatureClient
{
    public partial class FormComissaoAssociar : Form
    {
        public string codCliente { get; set; }
        public string nomeCliente { get; set; }
        public bool retorno { get; private set; }

        public FormComissaoAssociar()
        {
            InitializeComponent();
        }


        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(clConta1.textBoxCODIGO.Text) && String.IsNullOrEmpty(clConta2.textBoxCODIGO.Text))
            {
                MessageBox.Show("Os dois campos precisam estar preenchidos");
            }
            else
            {
                string sql = String.Empty;
                string ncontas = String.Empty;
                string point = String.Empty;

                try
                {
                    point = "[Quantifica numero de contas 1]";
                    sql = String.Format("select count(1) from FCFOCONT where CODCFO = {0} and CODCONTA like '1.01.03.00001.%'", clCliente.textBoxCODIGO.Text);
                    ncontas = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new Object[] { }).ToString();

                    if (int.Parse(ncontas) == 0)
                    {
                        point = "[Associa conta 1]";
                        Comissao.MetodosComissao.AssociaConta1(codCliente, clConta1.textBoxCODIGO.Text);
                    }

                    point = "[Quantifica numero de contas 2]";
                    sql = String.Format("select count(1) from FCFOCONT where CODCFO = {0} and CODCONTA like '2.01.04.00001.%'", clCliente.textBoxCODIGO.Text);
                    ncontas = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new Object[] { }).ToString();

                    if (int.Parse(ncontas) == 0)
                    {
                        point = "[Associa conta 2]";
                        Comissao.MetodosComissao.AssociaConta2(codCliente, clConta2.textBoxCODIGO.Text);
                    }

                    AtualizaData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ": " + point);
                }
                finally
                {
                    this.Close();
                }
            }
        }

        private void FormComissaoAssociar_Load(object sender, EventArgs e)
        {
            clCliente.textBoxCODIGO.Text = codCliente;
            clCliente.textBoxDESCRICAO.Text = nomeCliente;

            string sql = String.Empty;
            string ncontas = String.Empty;
            string point = String.Empty;

            try
            {
                point = "[Quantifica numero de contas 1]";
                sql = String.Format("select count(1) from FCFOCONT where CODCFO = {0} and CODCONTA like '1.01.03.00001.%'", clCliente.textBoxCODIGO.Text);
                ncontas = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new Object[] { }).ToString();

                if (int.Parse(ncontas) == 1)
                {
                    point = "[Código conta 1]";
                    sql = String.Format("select CCONTA.CODCONTA from FCFOCONT inner join CCONTA on CCONTA.CODCONTA = FCFOCONT.CODCONTA where CODCFO = {0} and FCFOCONT.CODCONTA like '1.01.03.00001.%'", clCliente.textBoxCODIGO.Text);
                    clConta1.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new Object[] { }).ToString(); ;

                    point = "[Descrição conta 1]";
                    sql = String.Format("select DESCRICAO from FCFOCONT inner join CCONTA on CCONTA.CODCONTA = FCFOCONT.CODCONTA where CODCFO = {0} and FCFOCONT.CODCONTA like '1.01.03.00001.%'", clCliente.textBoxCODIGO.Text);
                    clConta1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new Object[] { }).ToString(); ;
                }

                point = "[Quantifica numero de contas 2]";
                sql = String.Format("select count(1) from FCFOCONT where CODCFO = {0} and CODCONTA like '2.01.04.00001.%'", clCliente.textBoxCODIGO.Text);
                ncontas = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new Object[] { }).ToString();

                if (int.Parse(ncontas) == 1)
                {
                    point = "[Código conta 2]";
                    sql = String.Format("select CCONTA.CODCONTA from FCFOCONT inner join CCONTA on CCONTA.CODCONTA = FCFOCONT.CODCONTA where CODCFO = {0} and FCFOCONT.CODCONTA like '2.01.04.00001.%'", clCliente.textBoxCODIGO.Text);
                    clConta2.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new Object[] { }).ToString(); ;

                    point = "[Descrição conta 2]";
                    sql = String.Format("select DESCRICAO from FCFOCONT inner join CCONTA on CCONTA.CODCONTA = FCFOCONT.CODCONTA where CODCFO = {0} and FCFOCONT.CODCONTA like '2.01.04.00001.%'", clCliente.textBoxCODIGO.Text);
                    clConta2.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new Object[] { }).ToString(); ;

                }

                CarregaData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ": " + point);
            }




        }

        private void clConta1_AposSelecao(object sender, EventArgs e)
        {
            CarregaData();
        }

        private void clConta2_AposSelecao(object sender, EventArgs e)
        {
            CarregaData();
        }

        public void CarregaData()
        {
            string point = String.Empty;
            try
            {
                
                string sql = String.Empty;

                if(!String.IsNullOrWhiteSpace(clConta1.textBoxCODIGO.Text))
                {
                    point = "[Data conta 1]";
                    sql = String.Format(@"select C.DATAINCLU from CCONTA C

                                          inner join FCFOCONT F
                                          on F.CODCONTA = C.CODCONTA

                                          where F.CODCONTA = '{0}'", clConta1.textBoxCODIGO.Text);
                    dthConta1.Set(Convert.ToDateTime(MetodosSQL.GetField(sql, "DATAINCLU")));
                }

                if (!String.IsNullOrWhiteSpace(clConta2.textBoxCODIGO.Text))
                {
                    point = "[Data conta 2]";
                    sql = String.Format(@"select C.DATAINCLU from CCONTA C

                                          inner join FCFOCONT F
                                          on F.CODCONTA = C.CODCONTA

                                          where F.CODCONTA = '{0}'", clConta2.textBoxCODIGO.Text);

                    dthConta2.Set(Convert.ToDateTime(MetodosSQL.GetField(sql, "DATAINCLU")));
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ": " + point);
            }
            
        }

        public void AtualizaData()
        {
            string point = String.Empty;
            try
            {

                string sql = String.Empty;

                if (!String.IsNullOrWhiteSpace(clConta1.textBoxCODIGO.Text))
                {
                    point = "[Data conta 1]";
                    sql = String.Format(@"update CCONTA
                                          set DATAINCLU = CONVERT(date,'{0}', 103)
                                          where CODCONTA = '{1}'", dthConta1.Get(), clConta1.textBoxCODIGO.Text);

                    MetodosSQL.ExecQuery(sql);
                }

                if (!String.IsNullOrWhiteSpace(clConta2.textBoxCODIGO.Text))
                {
                    point = "[Data conta 2]";
                    sql = String.Format(@"update CCONTA
                                          set DATAINCLU = CONVERT(date,'{0}', 103)
                                          where CODCONTA = '{1}'", dthConta2.Get(), clConta2.textBoxCODIGO.Text);

                    MetodosSQL.ExecQuery(sql);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ": " + point);
            }

        }

        private void clConta1_SetDescricao(object sender, EventArgs e)
        {
            
        }
    }
}
