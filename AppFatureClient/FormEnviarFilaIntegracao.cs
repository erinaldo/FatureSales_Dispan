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
    public partial class FormEnviarFilaIntegracao : Form
    {
        public DataRowCollection Movimentos { get; set; }

        public FormEnviarFilaIntegracao()
        {
            InitializeComponent();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;

                for (int i = 0; i < Movimentos.Count; i++)
                {
                    if (Movimentos[i]["TABELA"].ToString() == "ZORCAMENTO")
                    {

                        int CodColigada = Convert.ToInt32(Movimentos[i]["CODCOLIGADA"]);
                        string IdMov = Movimentos[i]["VALORCAMPO"].ToString();
                        int IdLog = Convert.ToInt32(Movimentos[i]["IDLOG"]);

                        string sSql = "SELECT ISNULL(IDINTEGRACAO,0) FROM ZORCAMENTO WHERE CODCOLIGADA = ? AND IDMOV = ?";
                        int retorno = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { CodColigada, IdMov }).ToString());
                        if (retorno == 0)
                        {
                            sSql = @"UPDATE ZORCAMENTO SET ENVIAR = 0 WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            AppLib.Context.poolConnection.Get("Conn2").ExecQuery(sSql, new object[] { CodColigada, IdMov });

                            sSql = @"UPDATE ZORCAMENTO SET ENVIAR = 1 WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada, IdMov });

                            if (checkBox1.Checked)
                            {
                                sSql = @"DELETE FROM ZLOGINTEGRACAO WHERE CODCOLIGADA = ? AND IDLOG = ?";
                                AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada, IdLog });
                            }
                        }
                        else
                        {
                            throw new Exception("Operação Cancelada. Registro já integrado.");
                        }
                    }

                    if (Movimentos[i]["TABELA"].ToString() == "ZFCFOX")
                    {

                        int CodColigada = Convert.ToInt32(Movimentos[i]["CODCOLIGADA"]);
                        string IdOff = Movimentos[i]["VALORCAMPO"].ToString();
                        int IdLog = Convert.ToInt32(Movimentos[i]["IDLOG"]);

                        string sSql = "SELECT ISNULL(CODCFO,'ERRO') FROM ZFCFOX WHERE CODCOLIGADA = ? AND IDOFF = ?";
                        string retorno = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { CodColigada, IdOff }).ToString();
                        if (retorno == "ERRO")
                        {
                            sSql = @"UPDATE ZFCFOX SET ENVIAR = 1 WHERE CODCOLIGADA = ? AND IDOFF = ?";
                            AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada, IdOff });

                            if (checkBox1.Checked)
                            {
                                sSql = @"DELETE FROM ZLOGINTEGRACAO WHERE CODCOLIGADA = ? AND IDLOG = ?";
                                AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada, IdLog });
                            }
                        }
                        else
                        {
                            throw new Exception("Operação Cancelada. Registro já integrado.");
                        }
                    }
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Dispose();
        }
    }
}
