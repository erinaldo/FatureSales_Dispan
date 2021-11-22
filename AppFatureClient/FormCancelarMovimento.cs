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
    public partial class FormCancelarMovimento : Form
    {
        public DataRowCollection Movimentos { get; set; }

        public FormCancelarMovimento()
        {
            InitializeComponent();
        }

        private void FormCancelarMovimento_Load(object sender, EventArgs e)
        {
            campoDataDATA.Set(DateTime.Today);
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (campoDataDATA.Get() == null)
            {
                MessageBox.Show("Informe a Data do Cancelamento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (campoTextoMOTIVO.Get() == null)
            {
                MessageBox.Show("Informe o Motivo do Cancelamento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Deseja cancelar o movimento ?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            try
            {
                splashScreenManager1.ShowWaitForm();

                this.Cursor = Cursors.WaitCursor;

                for (int i = 0; i < Movimentos.Count; i++)
                {
                    string sSql = @"SELECT ID_EXERCICIO
                            FROM DEXERCICIONATUREZA
                            WHERE GETDATE() BETWEEN DATAINICIAL AND DATAFINAL
                            AND CODCOLIGADA = ?";

                    int IdExercicioFiscal = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, sSql, new Object[] { AppLib.Context.Empresa }));

                    AppInterop.MovCancelamentoPar cancelamento = new AppInterop.MovCancelamentoPar();

                    cancelamento.CodColigada = AppLib.Context.Empresa;
                    cancelamento.ApagarMovRelac = false;
                    cancelamento.CodSistemaLogado = "T";
                    cancelamento.CodUsuarioLogado = AppLib.Context.Usuario;
                    cancelamento.DataCancelamento = (DateTime)campoDataDATA.Get();
                    cancelamento.IdExercicioFiscal = IdExercicioFiscal;
                    cancelamento.IdMov = Convert.ToInt32(Movimentos[i]["IDMOV"]);
                    cancelamento.MotivoCancelamento = campoTextoMOTIVO.Get();

                    AppInterop.Message msgcancelamento;
                    if (FatureContexto.Remoto)
                    {
                        msgcancelamento = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.CancelaMovimento(AppLib.Context.Usuario, AppLib.Context.Senha, new Util().ConvertToWSMovCancelamentoPar(cancelamento)));
                    }
                    else
                    {
                        msgcancelamento = FatureContexto.ServiceClient.CancelaMovimento(AppLib.Context.Usuario, AppLib.Context.Senha, cancelamento);
                    }

                    if (int.Parse(msgcancelamento.Retorno.ToString()) > 0)
                    {
                        bool bCancelado = false;
                        while (!bCancelado)
                        {
                            sSql = @"SELECT STATUS FROM ZTMOVCANEXC WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            string sStatus = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sSql, new Object[] { Movimentos[i]["CODCOLIGADA"], Movimentos[i]["IDMOV"] }).ToString();
                            
                            if (sStatus != "A")
                            {
                                if (sStatus == "R")
                                {
                                    //System.Threading.Thread.Sleep(5000); //5s
                                }

                                if (sStatus == "S")
                                {
                                    bCancelado = true;
                                    msgcancelamento.Mensagem = "Cancelamento realizado com sucesso";
                                }

                                if (sStatus == "E")
                                {
                                    bCancelado = true;

                                    sSql = "SELECT MENSAGEM FROM ZTMOVCANEXC WHERE OPERACAO = 'C' AND CODCOLIGADA = " + Movimentos[i]["CODCOLIGADA"] + " AND IDMOV = " + Movimentos[i]["IDMOV"];
                                    msgcancelamento.Mensagem = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { }).ToString();                                                  
                                    throw new Exception(msgcancelamento.Mensagem);
                                }
                            }
                            else
                            {
                                //System.Threading.Thread.Sleep(5000); //5s
                            }
                        }

                        splashScreenManager1.CloseWaitForm();

                        MessageBox.Show(msgcancelamento.Mensagem);
                        this.Close();
                    }
                    else
                    {
                        splashScreenManager1.CloseWaitForm();

                        throw new Exception(msgcancelamento.Mensagem);
                    }
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                splashScreenManager1.CloseWaitForm();

                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
