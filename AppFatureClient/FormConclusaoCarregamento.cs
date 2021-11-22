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
    public partial class FormConclusaoCarregamento : AppLib.Windows.FormVisao
    {
        private static FormConclusaoCarregamento _instance = null;
        int idCarregamento;
        //private static FormCarregamentoCadastro _instance = null;
        public static FormConclusaoCarregamento GetInstance()
        {
            if (_instance == null)
                _instance = new FormConclusaoCarregamento();
            return _instance;
        }
        public FormConclusaoCarregamento()
        {
            InitializeComponent();
        }
        public  FormConclusaoCarregamento(int pIdCarregamento)
        {
            InitializeComponent();
            this.idCarregamento = pIdCarregamento;
            if (_instance == null)
                _instance = new FormConclusaoCarregamento();
            //return _instance;
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new object[] { idCarregamento };
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            DataRow dr = grid1.GetDataRow(false);

            if (dr != null)
            {
                gridData1.Parametros = new object[] { grid1.GetDataRow()["IDPRDCOMPOSTO"], grid1.GetDataRow()["IDMOV"], grid1.GetDataRow()["NSEQITMMOV"] };
            }
            else
            {
                gridData1.Parametros = new object[] { 0, 0, 0 };
            }




        }

        private void grid1_AposSelecao(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = false;
            this.gridData1.Atualizar(false);
        }

        private void FormConclusaoCarregamento_Load(object sender, EventArgs e)
        {
            gridData1.GetProcessos().Add("Concluir Carregamento", null, concluirCarregamento);


        }
        private void concluirCarregamento(object sender, EventArgs e)
        {

            if (MessageBox.Show("Tem certeza que deseja concluir o carregamento para os itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                DataRowCollection drc = gridData1.GetDataRows();
                string sql = @"UPDATE TITMMOV 
SET 
QUANTIDADE = 0,
QUANTIDADEARECEBER = 0
FROM
TITMMOV 
INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD 
WHERE 
TITMMOV.CODCOLIGADA = ?
AND TITMMOV.IDMOV = ?
AND TITMMOV.IDPRDCOMPOSTO = ?
AND TITMMOV.NSEQITMMOV = ?";
                for (int i = 0; i < drc.Count; i++)
                {
                    int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { AppLib.Context.Empresa, Convert.ToInt32(drc[i]["IDMOV"].ToString()), Convert.ToInt32(drc[i]["IDPRDCOMPOSTO"].ToString()), Convert.ToInt32(drc[i]["NSEQITMMOV"].ToString()) });
                }
                sql = @"SELECT COUNT(TITMMOV.IDMOV) RESULTADO
FROM 
TITMMOV,
TPRODUTO,
ZCARREGAMENTOITEMS
WHERE
TITMMOV.IDPRD = TPRODUTO.IDPRD
AND TITMMOV.IDMOV = ZCARREGAMENTOITEMS.IDMOV
AND TITMMOV.CODCOLIGADA = ZCARREGAMENTOITEMS.CODCOLIGADA
AND ZCARREGAMENTOITEMS.IDCARREGAMENTOITEMS = ?
AND QUANTIDADE <> 0 AND TITMMOV.NSEQITMMOV <> ?";
                object quantidade = AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new object[] { grid1.GetDataRow()["IDCARREGAMENTOITEMS"], grid1.GetDataRow()["NSEQITMMOV"] });
                sql = @"UPDATE ZCARREGAMENTO SET STATUS = ?, DATABAIXA = ? WHERE IDCARREGAMENTO = ?";
                if (quantidade.Equals(0))
                {
                    //Status = Completo
                    int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { "COMPLETO", AppLib.Context.poolConnection.Get().GetDateTime(), idCarregamento });
                }
                else
                {
                    //Status = Parcial
                    int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { "PARCIAL", AppLib.Context.poolConnection.Get().GetDateTime(), idCarregamento });
                }
                MessageBox.Show("Carregamento concluído com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

            }

        }

        private void FormConclusaoCarregamento_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            FormCarregamentoVisao f = FormCarregamentoVisao.GetInstance();
            f.grid1.filtro(true);
            f.Mostrar();
        }
    }

}

