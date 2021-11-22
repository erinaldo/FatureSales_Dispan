using DevExpress.XtraEditors;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient.New.Forms.Process
{
    public partial class frmReajustarPreco : Form
    {
        #region Variáveis

        public DataRowCollection drc = null;

        private DataTable dtReajustePreco = new DataTable();
        public DataTable dtProdutos = new DataTable();
        private DataTable dtReajuste = new DataTable();

        private Class.Controller.ReajustePreco reajustePrecoController = new Class.Controller.ReajustePreco();

        #endregion

        public frmReajustarPreco()
        {
            InitializeComponent();
        }

        private void frmReajustarPreco_Load(object sender, EventArgs e)
        {
            // Criação do Schema para o datatable
            dtReajustePreco = reajustePrecoController.CriarSchemaTabelaReajuste();

            CarregaGrid();
            CustomizaGrid();         
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            calcularValorPercentual();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            salvarPrecoCalculado();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        public void CarregaGrid()
        {
            try
            {
                // Valores do Produto
                foreach (DataRow dr in drc)
                {
                    dtReajustePreco.NewRow();
                    dtReajustePreco.Rows.Add(Convert.ToInt32(dr["IDPRD"]), dr["CODIGOAUXILIAR"].ToString(), dr["NOMEFANTASIA"].ToString(), dr["PRECO_FIXO"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PRECO_FIXO"]), dr["PRECO_ACABADO"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PRECO_ACABADO"]), dr["PRECO_REVENDA"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PRECO_REVENDA"]), 0);
                }

                // Reajuste de Preço
                for (int i = 0; i < dtReajustePreco.Rows.Count; i++)
                {
                    reajustePrecoController.listReajustePreco = reajustePrecoController.PreencherPropriedades(Convert.ToInt32(dtReajustePreco.Rows[i]["IDPRD"]));

                    for (int ii = 0; ii < reajustePrecoController.listReajustePreco.Count; ii++)
                    {
                        // Preço 1
                        if (ii == 0)
                        {
                            dtReajustePreco.Rows[i]["PRECO1"] = reajustePrecoController.listReajustePreco[ii].Preco;

                            if (reajustePrecoController.listReajustePreco[ii].DataAlteracao == null)
                            {
                                dtReajustePreco.Rows[i]["DATAALTERACAO1"] = DBNull.Value;
                            }
                            else
                            {
                                dtReajustePreco.Rows[i]["DATAALTERACAO1"] = reajustePrecoController.listReajustePreco[ii].DataAlteracao;
                            }

                            dtReajustePreco.Rows[i]["USUARIOALTERACAO1"] = reajustePrecoController.listReajustePreco[ii].UsuarioAlteracao;
                        }

                        // Preço 2
                        if (ii == 1)
                        {
                            dtReajustePreco.Rows[i]["PRECO2"] = reajustePrecoController.listReajustePreco[ii].Preco;

                            if (reajustePrecoController.listReajustePreco[ii].DataAlteracao == null)
                            {
                                dtReajustePreco.Rows[i]["DATAALTERACAO2"] = DBNull.Value;
                            }
                            else
                            {
                                dtReajustePreco.Rows[i]["DATAALTERACAO2"] = reajustePrecoController.listReajustePreco[ii].DataAlteracao;
                            }

                            dtReajustePreco.Rows[i]["USUARIOALTERACAO2"] = reajustePrecoController.listReajustePreco[ii].UsuarioAlteracao;
                        }

                        // Preço 3
                        if (ii == 2)
                        {
                            dtReajustePreco.Rows[i]["PRECO3"] = reajustePrecoController.listReajustePreco[ii].Preco;

                            if (reajustePrecoController.listReajustePreco[ii].DataAlteracao == null)
                            {
                                dtReajustePreco.Rows[i]["DATAALTERACAO3"] = DBNull.Value;
                            }
                            else
                            {
                                dtReajustePreco.Rows[i]["DATAALTERACAO3"] = reajustePrecoController.listReajustePreco[ii].DataAlteracao;
                            }

                            dtReajustePreco.Rows[i]["USUARIOALTERACAO3"] = reajustePrecoController.listReajustePreco[ii].UsuarioAlteracao;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            gridControl1.DataSource = dtReajustePreco;
            gridView1.BestFitColumns();
        }

        public void CustomizaGrid()
        {
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].FieldName == "CODIGOPRD")
                {
                    gridView1.Columns[i].Caption = "Código do Produto";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "CODIGOAUXILIAR")
                {
                    gridView1.Columns[i].Caption = "Código Auxiliar";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "NOMEFANTASIA")
                {
                    gridView1.Columns[i].Caption = "Nome Fantasia";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "PRECOFIXO")
                {
                    gridView1.Columns[i].Caption = "Preço Fixo";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "PRECOACABADO")
                {
                    gridView1.Columns[i].Caption = "Preço Acabado";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "PRECOREVENDA")
                {
                    gridView1.Columns[i].Caption = "Preço Revenda";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "VALORREAJUSTADO")
                {
                    gridView1.Columns[i].Caption = "Valor Reajustado";
                }

                if (gridView1.Columns[i].FieldName == "PRECO1")
                {
                    gridView1.Columns[i].Caption = "Preço Reajustado #1";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "USUARIOALTERACAO1")
                {
                    gridView1.Columns[i].Caption = "Usuário Alteração #1";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "DATAALTERACAO1")
                {
                    gridView1.Columns[i].Caption = "Data Alteração #1";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "USUARIOALTERACAO2")
                {
                    gridView1.Columns[i].Caption = "Usuário Alteração #2";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "PRECO2")
                {
                    gridView1.Columns[i].Caption = "Preço Reajustado #2";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "DATAALTERACAO2")
                {
                    gridView1.Columns[i].Caption = "Data Alteração #2";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "PRECO3")
                {
                    gridView1.Columns[i].Caption = "Preço Reajustado #3";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "USUARIOALTERACAO3")
                {
                    gridView1.Columns[i].Caption = "Usuário Alteração #3";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }

                if (gridView1.Columns[i].FieldName == "DATAALTERACAO3")
                {
                    gridView1.Columns[i].Caption = "Data Alteração #3";
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }
            }

            ValidaTipoPreco();

            if (!VerificaVisibilidadeColunasReajuste())
            {
                XtraMessageBox.Show("Somente um tipo de produto pode ser selecionado para o processo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Dispose();
                return;
            }
        }

        private void ValidaTipoPreco()
        {
            bool usaPrecoFixo = false;
            bool calculaComoAcabado = false;
            string tipoProduto = "";

            for (int i = 0; i < dtReajustePreco.Rows.Count; i++)
            {
                usaPrecoFixo = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT USAPRECOFIXO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(dtReajustePreco.Rows[i]["IDPRD"]) }));
                calculaComoAcabado = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT CALCCOMOACABADO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(dtReajustePreco.Rows[i]["IDPRD"]) }));
                tipoProduto = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT CODTB2FAT FROM TPRODUTODEF WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(dtReajustePreco.Rows[i]["IDPRD"]) }).ToString();

                // Revenda e Calculado como Acabado = True
                if (tipoProduto == "03" && calculaComoAcabado == true)
                {
                    gridView1.Columns["PRECOFIXO"].Visible = false;
                    gridView1.Columns["PRECOREVENDA"].Visible = false;
                }

                // Revenda e Calculado como Acabado = False
                if (tipoProduto == "03" && calculaComoAcabado == false)
                {
                    gridView1.Columns["PRECOFIXO"].Visible = false;
                    gridView1.Columns["PRECOACABADO"].Visible = false;
                }

                // Acabado e Usa Preço Fixo = True
                if (tipoProduto == "02" && usaPrecoFixo == true)
                {
                    gridView1.Columns["PRECOACABADO"].Visible = false;
                    gridView1.Columns["PRECOREVENDA"].Visible = false;
                }

                // Acabado e Usa Preço Fixo = False
                if (tipoProduto == "02" && usaPrecoFixo == false)
                {
                    gridView1.Columns["PRECOFIXO"].Visible = false;
                    gridView1.Columns["PRECOREVENDA"].Visible = false;
                }
            }
        }

        private bool VerificaVisibilidadeColunasReajuste()
        {
            bool verifica = true;

            if (gridView1.Columns["PRECOFIXO"].Visible == false && gridView1.Columns["PRECOREVENDA"].Visible == false && gridView1.Columns["PRECOACABADO"].Visible == false)
            {
                verifica = false;
            }

            return verifica;
        }

        private int[] GetIDPRD()
        {
            int[] ids = new int[dtProdutos.Rows.Count];

            for (int i = 0; i < dtProdutos.Rows.Count; i++)
            {
                ids.SetValue(Convert.ToInt32(dtProdutos.Rows[i]["IDPRD"]), i);
            }

            return ids;
        }

        private string FormatarIDs()
        {
            var ids = GetIDPRD();
            string query = "";

            for (int i = 0; i < ids.Length; i++)
            {
                if (i == 0)
                {
                    query = ids[i].ToString();
                }
                query = query + ", " + ids[i].ToString();
            }

            return query;
        }

        private void calcularValorPercentual()
        {
            decimal valorPercentual = 0;
            decimal valorSelecionado = 0;
            decimal valorCalculado = 0;
            decimal valorTotal = 0;

            valorPercentual = Convert.ToDecimal(tbValorPercentual.EditValue);

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRECOREVENDA")) > 0)
                {
                    valorSelecionado = Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRECOREVENDA"));
                    valorCalculado = valorSelecionado * valorPercentual;

                    valorTotal = valorSelecionado + valorCalculado;

                    gridView1.SetRowCellValue(i, "VALORREAJUSTADO", valorTotal);
                }
                else if (Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRECOFIXO")) > 0)
                {
                    valorSelecionado = Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRECOFIXO"));
                    valorCalculado = valorSelecionado * valorPercentual;

                    valorTotal = valorSelecionado + valorCalculado;

                    gridView1.SetRowCellValue(i, "VALORREAJUSTADO", valorTotal);
                }
                else if (Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRECOACABADO")) > 0)
                {
                    valorSelecionado = Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRECOACABADO"));
                    valorCalculado = valorSelecionado * valorPercentual;

                    valorTotal = valorSelecionado + valorCalculado;

                    gridView1.SetRowCellValue(i, "VALORREAJUSTADO", valorTotal);
                }
            }

            gridView1.Columns["VALORREAJUSTADO"].Visible = true;
        }

        private void salvarPrecoCalculado()
        {
            try
            {
                int idPrd;
                decimal valorCalculado = 0;

                ConverterDataTableParaLista();

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    valorCalculado = Convert.ToDecimal(gridView1.GetRowCellValue(i, "VALORREAJUSTADO"));
                    idPrd = Convert.ToInt32(gridView1.GetRowCellValue(i, "IDPRD"));

                    // Atualiza o Preço Fixo
                    if (gridView1.Columns["PRECOFIXO"].Visible == true)
                    {
                        if (AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZTPRODUTOCOMPL SET PRECOFIXO = ? WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { valorCalculado, AppLib.Context.Empresa, idPrd }) > 0)
                        {
                            reajustePrecoController.ReajustarPreco(reajustePrecoController.listReajustePreco[i]);
                            continue;
                        }
                    }

                    // Atualiza o Preço de Revenda
                    if (gridView1.Columns["PRECOREVENDA"].Visible == true)
                    {
                        if (AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE TPRODUTODEF SET PRECO1 = ? WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { valorCalculado, AppLib.Context.Empresa, idPrd }) > 0)
                        {
                            reajustePrecoController.ReajustarPreco(reajustePrecoController.listReajustePreco[i]);
                            continue;
                        }
                    }

                    // Atualiza o Preço Acabado
                    if (gridView1.Columns["PRECOACABADO"].Visible == true)
                    {
                        if (AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZTPRODUTOCOMPL SET PRECOCALCULADO = ? WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { valorCalculado, AppLib.Context.Empresa, idPrd }) > 0)
                        {
                            reajustePrecoController.ReajustarPreco(reajustePrecoController.listReajustePreco[i]);
                            continue;
                        }
                    }
                }

                XtraMessageBox.Show("Preço(s) atualizado(s) com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
                return;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Não foi possível atualizar o(s) preço(s). Detalhes: \r\n: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void ConverterDataTableParaLista()
        {
            reajustePrecoController.listReajustePreco.Clear();

            for (int i = 0; i < dtReajustePreco.Rows.Count; i++)
            {
                reajustePrecoController.reajustePreco = new Models.ReajustePreco();

                reajustePrecoController.reajustePreco.CodColigada = AppLib.Context.Empresa;
                reajustePrecoController.reajustePreco.IDPrd = Convert.ToInt32(dtReajustePreco.Rows[i]["IDPRD"]);
                reajustePrecoController.reajustePreco.Preco = Convert.ToDecimal(dtReajustePreco.Rows[i]["VALORREAJUSTADO"]);
                reajustePrecoController.reajustePreco.DataAlteracao = DateTime.Now;
                reajustePrecoController.reajustePreco.UsuarioAlteracao = AppLib.Context.Usuario;

                reajustePrecoController.listReajustePreco.Add(reajustePrecoController.reajustePreco);
            }
        }

        #endregion
    }
}
