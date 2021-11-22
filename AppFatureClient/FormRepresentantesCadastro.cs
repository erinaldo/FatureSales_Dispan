using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AppFatureClient
{
    public partial class FormRepresentantesCadastro : AppLib.Windows.FormCadastroData
    {
        bool edita = false;
        DataTable Dtusuarios;

        public FormRepresentantesCadastro(bool edita = false)
        {
            InitializeComponent();
            txtcodigo.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            txtcodigo.textEdit1.Properties.Mask.EditMask = "00000";

            this.edita = edita;

            if (edita == false)
            {
                LimpaDtusuarios();
            }
        }

        private void LimpaDtusuarios()
        {
            Dtusuarios = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT Top 0 CODUSUARIO as 'Usuários' FROM ztrpr", new object[] { });
            grid1.gridControl1.DataSource = Dtusuarios;
        }
        private bool lookupestado_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODETD, NOME FROM GETD";

            return new AppLib.Windows.FormVisao().MostrarLookup(lookupestado, consulta1, new Object[] { });
        }

        private bool lookupmunicipio_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODMUNICIPIO, NOMEMUNICIPIO FROM GMUNICIPIO WHERE CODETDMUNICIPIO = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(lookupmunicipio, consulta1, new Object[] { lookupestado.Get() });
        }

        private void FormRepresentantesCadastro_AntesSalvar(object sender, EventArgs e)
        {
            txtCODCOLIGADA.Set(AppLib.Context.Empresa);
        }

        private bool FormRepresentantesCadastro_ValidarSalvar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcodigo.Get()))
            {
                MessageBox.Show("Favor informar o código do representante", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(txtnome.Get()))
            {
                MessageBox.Show("Favor informar o nome do representante", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
        }

        private void FormRepresentantesCadastro_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = true;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lookupusuario.Get()))
            {
                MessageBox.Show("Favor selecionar um usuário", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (Dtusuarios == null)
                {
                    LimpaDtusuarios();
                }

                DataRow[] rows = Dtusuarios.Select("Usuários = '" + lookupusuario.Get() + "'");
                if (rows.Count() > 0)
                {
                    MessageBox.Show("Este usuário já está cadastrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (Dtusuarios == null)
                    {
                        LimpaDtusuarios();
                    }
                    DataRow dr = Dtusuarios.NewRow();

                    dr["Usuários"] = lookupusuario.Get();

                    Dtusuarios.Rows.Add(dr);

                    lookupusuario.Clear();

                    grid1.gridControl1.DataSource = Dtusuarios;
                }
               
            }
        }

        void CarregaGridUsuarios()
        {
            Dtusuarios = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODUSUARIO as 'Usuários' FROM ztrpr WHERE CODCOLIGADA = ? AND CODRPR = ?", new object[] { AppLib.Context.Empresa, txtcodigo.Get() });
            if (Dtusuarios.Rows.Count == 0)
            {
                Dtusuarios = null;
            }

            grid1.gridControl1.DataSource = Dtusuarios;
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa, txtcodigo.Get() };
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {
                if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row1 = grid1.gridView1.GetDataRow(Convert.ToInt32(grid1.gridView1.GetSelectedRows().GetValue(0).ToString()));

                    conn.ExecTransaction("delete from ztrpr where CODCOLIGADA=? AND CODRPR=? AND CODUSUARIO=?", new object[] { AppLib.Context.Empresa, txtcodigo.Get(), row1["Usuários"].ToString() });

                    conn.Commit();

                    CarregaGridUsuarios();
                }
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show("Erro ao excluir usuário, processo cancelado!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grid1_Editar(object sender, EventArgs e)
        {

        }

        private void FormRepresentantesCadastro_AntesEditar(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = true;
            LimpaDtusuarios();
        }

        private void FormRepresentantesCadastro_AposEditar(object sender, EventArgs e)
        {
            CarregaGridUsuarios();
        }

        private bool lookupusuario_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT USUARIO FROM ZUSUARIO";

            return new AppLib.Windows.FormVisao().MostrarLookup(lookupusuario, consulta1, new Object[] { });
        }

        private void FormRepresentantesCadastro_AposSalvar(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {
                if (!string.IsNullOrEmpty(txtcodigo.Get()))
                {


                    DataTable DtUsuariosGravados = conn.ExecQuery("SELECT CODUSUARIO as 'Usuários' FROM ztrpr WHERE CODCOLIGADA = ? AND CODRPR = ?", new object[] { AppLib.Context.Empresa, txtcodigo.Get() });

                    if (Dtusuarios != null)
                    {


                        for (int i = 0; i <= Dtusuarios.Rows.Count - 1; i++)
                        {
                            DataRow[] rows = DtUsuariosGravados.Select("Usuários = '" + Dtusuarios.Rows[i]["Usuários"].ToString() + "'");
                            if (rows.Count() == 0)
                            {
                                conn.ExecTransaction("insert into ztrpr (CODCOLIGADA,CODRPR,CODUSUARIO) values(?,?,?) ", new object[] { AppLib.Context.Empresa, txtcodigo.Get(), Dtusuarios.Rows[i]["Usuários"].ToString() });
                            }
                        }

                        //if (DtUsuariosGravados.Rows.Count > 0)
                        //{
                        //    for (int x = 0; x <= DtUsuariosGravados.Rows.Count -1; x++)
                        //    {
                        //        DataRow[] rows = Dtusuarios.Select("Usuários = '" + DtUsuariosGravados.Rows[x]["Usuários"].ToString() + "'");
                        //        if (rows.Count() == 0)
                        //        {
                        //            conn.ExecTransaction("delete from ztrpr where CODCOLIGADA=?,CODRPR=?,CODUSUARIO=?)", new object[] { AppLib.Context.Empresa, txtcodigo.Get(), DtUsuariosGravados.Rows[x]["Usuários"].ToString() });
                        //        }
                        //    }
                        //}
                        conn.Commit();

                        CarregaGridUsuarios();
                    }
                }
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show("Erro ao incluir usuários, processo cancelado!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbComissaoPadrao_CheckedChanged(object sender, EventArgs e)
        {
            campoDecimal1.Enabled = cbComissaoPadrao.Checked;
            if(campoDecimal1.Enabled)
            {
                campoDecimal1.textEdit1.Text = "0,00";
            }
            else
            {
                campoDecimal1.textEdit1.Text = null;
            }

        }
    }
}
