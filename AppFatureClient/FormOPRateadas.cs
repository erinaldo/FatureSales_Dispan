using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFatureClient.Classes;
using AppLib.Windows;

namespace AppFatureClient
{
    public partial class FormOPRateadas : AppLib.Windows.FormVisao
    {
        private static FormOPRateadas _instance = null;

        public static FormOPRateadas GetInstance()
        {
            if (_instance == null)
                _instance = new FormOPRateadas();
            return _instance;
        }

        public FormOPRateadas()
        {
            InitializeComponent();

            // Processos
            grid1.GetProcessos().Add("Reverter Rateio", null, ReverterRateio);
        }

        private void ReverterRateio(object sender, EventArgs e)
        {
            try
            {
                New.Class.Controller.RateioNFOP r = new New.Class.Controller.RateioNFOP();

                DataRow row = grid1.GetDataRow();

                r.ReverterRateio(Convert.ToInt32(row["IDMOV"]), row["NUMEROMOV"].ToString());

                grid1.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            try
            {
                DialogResult r = MessageBox.Show("Todos os registros da NF selecionada serão excluídos!", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if(r == DialogResult.OK)
                {
                    DataRow dr = grid1.GetDataRow();
                    RateioNFOP.DeletarRateio((int)dr["IDMOV"]);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            try
            {
                FormSelecionaNFParaRateio rateio = new FormSelecionaNFParaRateio();
                rateio.ShowDialog();

                if (!String.IsNullOrWhiteSpace(rateio.cod))
                {
                    splashScreenManager1.ShowWaitForm();
                    RateioNFOP.Rateia(rateio.cod);
                    MessageBox.Show("Processo concluído com sucesso!", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    grid1.Atualizar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (splashScreenManager1.IsSplashFormVisible)
                {
                    splashScreenManager1.CloseWaitForm();
                }    
            }
        }


        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void FormOPRateadas_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            
        }

        private void grid1_AposAtualizar(object sender, EventArgs e)
        {
            
        }

        private void FormOPRateadas_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Alterar Data Movimentação", null, AlterarDataMovimentacao);
        }

        private void AlterarDataMovimentacao(object sender, EventArgs e)
        {
            try
            {

                DataRowCollection drc = grid1.GetDataRows();
                bool movimentoBaixa = true;
                foreach (DataRow dr in drc)
                {
                    if (dr.IsNull("IDMOVBAIXA"))
                    {
                        movimentoBaixa = false;
                    }
                }

                if(movimentoBaixa)
                {
                    splashScreenManager1.ShowWaitForm();
                    RateioNFOP.AlterarDataMovimentacao(grid1.GetDataRows());
                    grid1.Atualizar();
                }
                else
                {
                    MessageBox.Show("Você selecionou um movimento sem baixa!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                splashScreenManager1.CloseWaitForm();
            }

        }
    }
}
