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
    public partial class FormComissaoPorcentagemCadastro : Form
    {

        public string COD { get; set; }
        public bool EDIT { get; set; }
        string sql = String.Empty;

        public FormComissaoPorcentagemCadastro()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FormComissaoPorcentagemCadastro_Load(object sender, EventArgs e)
        {
            if(EDIT)
            {
                MetodosSQL.CS = AppLib.Context.poolConnection.Get().ConnectionString;

                sql = String.Format("select DE, ATE, PORCENTAGEM from ZPORCENTAGEMCARTA where CODDESC = '{0}'", COD);

                txtDe.textEdit1.Text = MetodosSQL.GetField(sql, "DE");
                txtAte.textEdit1.Text = MetodosSQL.GetField(sql, "ATE");
                txtPorcentagem.textEdit1.Text = MetodosSQL.GetField(sql, "PORCENTAGEM");
            }
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (EDIT)
                {
                    sql = String.Format(@"update ZPORCENTAGEMCARTA
                                      set DE = {0},
                                      ATE = {1},
                                      PORCENTAGEM = {2}
                                      where CODDESC = {3}",
                                          txtDe.textEdit1.Text.Replace(",", "."),
                                          txtAte.textEdit1.Text.Replace(",", "."),
                                          txtPorcentagem.textEdit1.Text.Replace(",", "."),
                                          COD);


                    MetodosSQL.ExecQuery(sql);
                }
                else
                {
                    sql = String.Format(@"insert into ZPORCENTAGEMCARTA values ({0},{1},{2})",
                                          txtDe.textEdit1.Text.Replace(",", "."),
                                          txtAte.textEdit1.Text.Replace(",", "."),
                                          txtPorcentagem.textEdit1.Text.Replace(",", "."));
                    MetodosSQL.ExecQuery(sql);
                }
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
