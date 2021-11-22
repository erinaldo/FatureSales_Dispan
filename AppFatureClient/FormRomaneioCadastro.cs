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
    public partial class FormRomaneioCadastro : AppLib.Windows.FormCadastroData
    {
        private static FormRomaneioCadastro _instance = null;
        string sql;
        public static FormRomaneioCadastro GetInstance()
        {
            if (_instance == null)
                _instance = new FormRomaneioCadastro();
            return _instance;
        }

        public FormRomaneioCadastro()
        {
            InitializeComponent();
        }

        private void FormRomaneioCadastro_Load(object sender, EventArgs e)
        {
            campoData1.maskedTextBox1.Text = DateTime.Now.ToShortDateString();
            //Processos
            gridData2.GetProcessos().Add("Retirar do Romaneio", null, retiraRomaneio);
            gridData1.GetProcessos().Add("Compor Romaneio", null, comporRomaneio);
        }
        private void retiraRomaneio(object sender, EventArgs e)
        {
            string sql = string.Empty;

            if (new AppLib.Security.Access().Processo("Start", "APP8034", AppLib.Context.Perfil))
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
                    //Retira ZROMANEIOITEMS
                    for (int i = 0; i < drc.Count; i++)
                    {
                        //Altera o status do romaneio na tabela ZPROGRAMACAOROMANEIO
                        sql = @"UPDATE ZPROGRAMACAOCARREGAMENTO SET ROMANEIO = 'N' WHERE IDPROGRAMACAOCARREGAMENTO = ?";
                        AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString() });
                        //Deleta o item
                        sql = @"DELETE FROM ZROMANEIOITEMS WHERE IDROMANEIOITEMS = ?";
                        AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { drc[i]["IDROMANEIOITEMS"].ToString() });
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Atualizar as grids
                gridData2.Atualizar(false);
                gridData1.Atualizar(false);
            }
        }
        private void comporRomaneio(object sender, EventArgs e)
        {
            string sql = string.Empty;

            if (new AppLib.Security.Access().Processo("Start", "APP8034", AppLib.Context.Perfil))
            {
                DataRowCollection drc = null;
                try
                {
                    drc = gridData1.GetDataRows();
                }
                catch (Exception)
                {
                }
                if (drc == null)
                    return;
                if (txtIdRomaneio.Get().Equals(null))
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
                    //Inserir ZROMANEIOITEMS
                    for (int i = 0; i < drc.Count; i++)
                    {
                        //verfica se o romaneio já foi composto
                        sql = @"SELECT ROMANEIO FROM ZPROGRAMACAOCARREGAMENTO WHERE IDPROGRAMACAOCARREGAMENTO = ?";
                        if (Convert.ToString(AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, sql, new object[] { drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString() })).Equals("N"))
                        {
                            //Inseri o item no romaneio 
                            sql = @"INSERT INTO ZROMANEIOITEMS (IDROMANEIO, IDPROGRAMACAOCARREGAMENTO) VALUES (?, ?)";
                            AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { txtIdRomaneio.textEdit1.Text, drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString() });
                            //alterar o status do item da programação
                            sql = @"UPDATE ZPROGRAMACAOCARREGAMENTO SET ROMANEIO = 'S' WHERE IDPROGRAMACAOCARREGAMENTO = ?";
                            AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString() });
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Atualizar as grids
                gridData2.Atualizar(false);
                gridData1.Atualizar(false);
            }
        }


        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            if (txtIdRomaneio.Get().Equals(null))
            {
                gridData2.Parametros = new Object[] { 0 };
            }
            else
            {
                gridData2.Parametros = new Object[] { txtIdRomaneio.Get() };
            }
        }

        private void FormRomaneioCadastro_AntesNovo(object sender, EventArgs e)
        {
            gridData2.Atualizar(false);
            gridData1.Atualizar(false);
        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            try
            {
                DataRow dr;
                dr = gridData2.GetDataRow();
                AppLib.Windows.FormMessagePrompt prt = new AppLib.Windows.FormMessagePrompt();
                prt.ShowDialog();
                sql = @"UPDATE ZROMANEIOITEMS SET OBS = ? WHERE IDROMANEIOITEMS = ?";
                AppLib.Context.poolConnection.Get().ExecTransaction(sql, new object[] { prt.textBox1.Text.ToUpper(), dr["IDROMANEIOITEMS"] });
                MessageBox.Show("Observação Cadastrada com Sucesso.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível cadastrar a observação no item.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            gridData2.Atualizar(false);
        }

    }
}
