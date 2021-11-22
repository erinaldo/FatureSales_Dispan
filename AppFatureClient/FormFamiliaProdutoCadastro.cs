using AppFatureClient.Classes;
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
    public partial class FormFamiliaProdutoCadastro : AppLib.Windows.FormCadastroData
    {
        public FormFamiliaProdutoCadastro()
        {
            InitializeComponent();
            campoTextoCODTRA.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTextoCODTRA.textEdit1.Properties.Mask.EditMask = "00.000.000";
        }

        private void FormFamiliaProdutoCadastro_AntesSalvar(object sender, EventArgs e)
        {
            campoInteiroCODCOLIGADA.Set(AppLib.Context.Empresa);

            string sql = String.Format(@"select count(1) as 'CONT' from ZPRODUTOANALISE where CODIGOPRD = '{0}'", campoTextoCODTRA.Get());

            int i = int.Parse(MetodosSQL.GetField(sql, "CONT"));

            if (ceAnaliseItemCompra.Checked)
            {
                if (i < 1)
                {
                    MetodosSQL.ExecQuery(String.Format("insert into ZPRODUTOANALISE values ('{0}','V')", campoTextoCODTRA.Get()));
                }
            }
            else
            {
                if (i >= 1)
                {
                    MetodosSQL.ExecQuery(String.Format("delete from ZPRODUTOANALISE where CODIGOPRD = '{0}'", campoTextoCODTRA.Get()));
                }
            }
        }

        private bool FormFamiliaProdutoCadastro_Validar(object sender, EventArgs e)
        {
            //valida a máscara do codigo da transportadora
            if (string.IsNullOrEmpty(campoTextoCODTRA.Get()))
            {
                MessageBox.Show("Campo Código obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {

            }

            return true;
        }

        private void carregaGridRpr()
        {
            gridControl1.DataSource = AppLib.Context.poolConnection.Get().ExecQuery("select * from ZREPREDESC where CODCOLIGADA = ? and CODTB4FAT = ?", new object[] { AppLib.Context.Empresa, campoTextoCODTRA.Get() });

            string sql = String.Format(@"select count(1) as 'CONT' from ZPRODUTOANALISE where CODIGOPRD = '{0}'", campoTextoCODTRA.Get());

            int i = int.Parse(MetodosSQL.GetField(sql, "CONT"));
            if(i >= 1)
            {
                ceAnaliseItemCompra.Checked = true;
            }
            else
            {
                ceAnaliseItemCompra.Checked = false;
            }
            //ceAnaliseItemCompra.Checked;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AppLib.Context.poolConnection.Get().ExecTransaction("INSERT INTO ZREPREDESC (CODCOLIGADA, CODREPRESENTANTE, CODTB4FAT, DESCRICAO, PERCDESCONTO) VALUES (?, ?, ?, ?, ?)", new object[] {campoInteiroCODCOLIGADA.Get(), campoLookupCODRPR.Get(), campoTextoCODTRA.Get(), campoTextoDescricaoRepr.Get(), campoDecimalPercDescontoRepr.Get() });

                carregaGridRpr();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK , MessageBoxIcon.Error);
                return;
            }
        }

        private void FormFamiliaProdutoCadastro_Load(object sender, EventArgs e)
        {
            carregaGridRpr();
            gridData1.Consulta = String.Format(@"select ZPA.CODIGOPRD, TP.NOMEFANTASIA from ZPRODUTOANALISE ZPA

                                                 left join TPRODUTO TP
                                                 on TP.CODIGOPRD = ZPA.CODIGOPRD 

                                                 where ZPA.APLICACAO = 'E'
                                                 and TP.CODCOLPRD = '1'
                                                 and ZPA.CODIGOPRD like '{0}%'", campoTextoCODTRA.Get()).Split(' ');
            gridData1.Atualizar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                AppLib.Context.poolConnection.Get().ExecTransaction(@"delete from ZREPREDESC where CODCOLIGADA = ? and CODTB4FAT = ? and CODREPRESENTANTE = ?", new object[] { campoInteiroCODCOLIGADA.Get(), campoTextoCODTRA.Get(), row1["CODREPRESENTANTE"].ToString() });
                carregaGridRpr();
                
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            try
            {
                AppLib.Windows.CampoLookup campoLookup1 = new AppLib.Windows.CampoLookup();

                campoLookup1.ColunaCodigo = "CODIGOPRD";
                campoLookup1.ColunaDescricao = "NOMEFANTASIA";
                String consulta1 = String.Format(@"SELECT * FROM (select CODCOLPRD, IDPRD, CODIGOPRD, NOMEFANTASIA from TPRODUTO where CODCOLPRD = 1 and CODIGOPRD like '{0}%') Y", campoTextoCODTRA.Get());
                AppLib.Windows.FormVisao f = new AppLib.Windows.FormVisao();
                f.grid1.NomeGrid = "Exclusão";
                f.MostrarLookup(campoLookup1, consulta1, new Object[] { });

                int i = int.Parse(MetodosSQL.GetField(String.Format(@"select COUNT(1) as 'CONT' from ZPRODUTOANALISE where CODIGOPRD = '{0}' and APLICACAO = 'E' and LEN(CODIGOPRD) > 10", campoLookup1.textBoxCODIGO.Text),"CONT"));

                if(i == 0)
                {
                    MetodosSQL.ExecQuery(String.Format("insert into ZPRODUTOANALISE values ('{0}','E')", campoLookup1.textBoxCODIGO.Text));
                }
                else
                {
                    MessageBox.Show("Esse produto já está cadastrado na lista de exclusão.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                campoLookup1.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            try
            {
                DataRow SelectRow = gridData1.GetDataRow();

                if (MessageBox.Show("Deseja excluir o item selecionado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MetodosSQL.ExecQuery(String.Format("delete from ZPRODUTOANALISE where CODIGOPRD = '{0}'", SelectRow["CODIGOPRD"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void ceAnaliseItemCompra_CheckedChanged(object sender, EventArgs e)
        {
            gridData1.Visible = ceAnaliseItemCompra.Checked;
            gridData1.Atualizar();
        }
    }
}
