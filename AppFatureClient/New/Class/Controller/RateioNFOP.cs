using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.New.Class.Controller
{
    public class RateioNFOP
    {
        #region Construtor

        public RateioNFOP() { }

        #endregion

        #region Métodos

        public void ReverterRateio(int _idMOV, string _numeroMOV)
        {
            if (XtraMessageBox.Show("Todos os registros da Nota Fiscal "+ _numeroMOV +" serão excluídos, deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (validaOPBaixada(_idMOV))
                {
                    try
                    {
                        DataTable dtRateio = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM ZOPRATEADAS WHERE CODCOLIGADA = 1 AND CODFILIAL = 1 AND IDMOV = ?", new object[] { _idMOV });

                        if (dtRateio.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtRateio.Rows.Count; i++)
                            {
                                if (!deleteKATVORDEMMP(Convert.ToInt32(dtRateio.Rows[i]["IDKAO"])))
                                {
                                    XtraMessageBox.Show("Não foi possível excluir os registros referentes à NF "+ _numeroMOV +".", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                if (!deleteKESTQDETALHE(Convert.ToInt32(dtRateio.Rows[i]["IDKED"])))
                                {
                                    XtraMessageBox.Show("Não foi possível excluir os registros referentes à NF " + _numeroMOV + ".", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                if (!deleteKESTQDETALHEQTDE(Convert.ToInt32(dtRateio.Rows[i]["IDKEDQ"])))
                                {
                                    XtraMessageBox.Show("Não foi possível excluir os registros referentes à NF " + _numeroMOV + ".", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                if (!deleteZOPRATEADAS(Convert.ToInt32(dtRateio.Rows[i]["IDRATEIO"])))
                                {
                                    XtraMessageBox.Show("Não foi possível excluir os registros referentes à NF " + _numeroMOV + ".", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            XtraMessageBox.Show("Reversão de Rateio realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Não foi possível efetuar o rateio para a Nota Fiscal "+ _numeroMOV +". Detalhes: \r\n: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Não é permitido excluir registros de NFs com OPs baixadas!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_idMOV"></param>
        /// <returns></returns>
        private bool validaOPBaixada(int _idMOV)
        {
            bool valida = true;

            if (Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT COUNT(1) AS 'TOTAL'
                                                                                               FROM ZOPRATEADAS ZOR
                                                                                               LEFT
                                                                                               JOIN (SELECT CODCOLIGADA, CODORDEM, IDMOV, DATAHORA FROM KMOVESTOQUE WHERE IDPRODUTO IN (81725,82181,83805,83950,83916)) KME
                                                                                                     ON KME.CODCOLIGADA = ZOR.CODCOLIGADA
                                                                                               AND KME.CODORDEM = ZOR.CODORDEM
                                                                                               WHERE KME.IDMOV IS NOT NULL
                                                                                               AND ZOR.IDMOV = ?", new object[] { _idMOV })) > 0)
            {
                valida = false;
            }

            return valida;
        }

        private bool deleteKATVORDEMMP(int _idAtvOrdemMateriaPrima)
        {
            bool valida = false;

            if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM KATVORDEMMP WHERE IDATVORDEMMATERIAPRIMA = ?", new object[] { _idAtvOrdemMateriaPrima }) > 0)
            {
                valida = true;
            }

            return valida;
        }

        private bool deleteKESTQDETALHE(int _idAutoInc)
        {
            bool valida = false;

            if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM KESTQDETALHE WHERE IDAUTOINC = ?", new object[] { _idAutoInc }) > 0)
            {
                valida = true;
            }

            return valida;
        }

        private bool deleteKESTQDETALHEQTDE(int _idAutoInc)
        {
            bool valida = false;

            if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM KESTQDETALHEQTDE WHERE IDAUTOINC = ?", new object[] { _idAutoInc }) > 0)
            {
                valida = true;
            }

            return valida;
        }

        private bool deleteZOPRATEADAS(int _idRateio)
        {
            bool valida = false;

            if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM ZOPRATEADAS WHERE CODCOLIGADA = 1 AND CODFILIAL = 1 AND IDRATEIO = ?", new object[] { _idRateio }) > 0)
            {
                valida = true;
            }

            return valida;
        }

        #endregion
    }
}
