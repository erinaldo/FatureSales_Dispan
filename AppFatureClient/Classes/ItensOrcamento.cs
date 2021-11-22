using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.Classes
{

    public class ItemComposicao
    {
        public String CODIGOPRD { get; set; }
        public String CODIGOAUXILIAR { get; set; }
        public String NOMEFANTASIA { get; set; }
        public Decimal QUANTIDADE { get; set; }
        //public String UNIDADEMEDIDA { get; set; }
        public Decimal PRECOUNITARIO { get; set; }
        public int CODCOLIGADA { get; set; }
        public int CODFILIAL { get; set; }
        public int IDMOV { get; set; }
        public int NSEQ { get; set; }
        public int IDPRD { get; set; }
        public decimal TOTAL { get; set; }
    }

    public class SaldoItem
    {
        public Decimal SALDO_FISICO;
        public Decimal SALDO_PEDIDO;
        public Decimal SALDO_DISPONIVEL;
    }

    class ItensOrcamento
    {

        public static string getCFOP(int IDNAT)
        {
            try
            {
                string sql = $@"select CODNAT 
                                from DCFOP 
                                where CODCOLIGADA = 1 
                                and ATIVO = 1 
                                and FISCAL = 1
                                and SUBSTRING(CODNAT, 1, 1) in (5,6,7) 
                                and LEN(CODNAT) > 5
                                and IDNAT = {IDNAT}";

                return MetodosSQL.GetField(sql, "CODNAT");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static String getCFOP(String CODETD, String Aplicacao, String CODPRD, String NCONTRIB)
        {
            try
            {
                string sql = String.Empty;

                string Estado = CODETD;

                sql = String.Format(@"select isnull(NUMEROCCF, '') as 'NUMEROCCF', isnull(CODTB2FAT, '') as 'CODTB2FAT' from TPRD where CODIGOPRD = '{0}' and CODCOLIGADA = 1", CODPRD);

                string CCF = MetodosSQL.GetField(sql, "NUMEROCCF");
                string TIPOPRD = MetodosSQL.GetField(sql, "CODTB2FAT");

                string Aplic = String.Empty;
                if (String.IsNullOrWhiteSpace(Aplicacao))
                {
                    Aplic = "0";
                }
                else
                {
                    Aplic = Aplicacao;
                }


                sql = String.Format(@"select 
                                        CASE WHEN '{1}' = 1 THEN CODNATUSOCONSUMO
	                                    WHEN '{1}' = 2 THEN CODNATUSOCONSUMOSST
	                                    WHEN '{1}' = 3 THEN CODNATREVENDER
	                                    WHEN '{1}' = 4 THEN CODNATREVENDERSST
	                                    WHEN '{1}' = 5 THEN CODNATINDUSTRIALIZAR
	                                    WHEN '{1}' = 6 THEN CODNATATIVOIMOB
	                                    ELSE '' END AS 'CFOP' FROM ZREGRACFOP
                                        where CODETD = '{2}'
                                        and NUMEROCCF = case when (select COUNT(1) from ZREGRACFOP
													where CODETD = '{2}' and TIPOPRD = '{4}' 
													and NUMEROCCF = '{3}' and NAOCONTRIBUINTE = '{5}') > 0 then '{3}' else '' end
                                        and TIPOPRD = '{4}'
										and NAOCONTRIBUINTE = '{5}'", Aplic, Aplic, Estado, CCF, TIPOPRD, NCONTRIB);

                return MetodosSQL.GetField(sql, "CFOP");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static int getIDNAT(String CFOP)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(CFOP))
                {
                    string sql = String.Format(@"SELECT IDNAT FROM DCFOP WHERE CODNAT = '{0}'", CFOP);

                    return int.Parse(MetodosSQL.GetField(sql, "IDNAT"));
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("CFOP não encontrada.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public static SaldoItem getSaldo(int IDPRD)
        {
            String sql = String.Format(@"select isnull(X.SALDO_FISICO, 0) as 'SALDO_FISICO', 
	   isnull(Y.SALDO_PEDIDO, 0) as 'SALDO_PEDIDO', 
	   isnull(X.SALDO_FISICO, 0)-isnull(Y.SALDO_PEDIDO, 0) as 'SALDO_DISPONIVEL' 
												
												from TPRODUTO

												left join (SELECT IDPRD, SUM(TITMMOV.QUANTIDADE) SALDO_PEDIDO
                                                FROM TMOV
                                                inner join TITMMOV
                                                on TITMMOV.CODCOLIGADA = TMOV.CODCOLIGADA
                                                and TITMMOV.IDMOV = TMOV.IDMOV 
                                                WHERE TMOV.CODCOLIGADA = 1
                                                AND TMOV.STATUS IN ('A','G')
                                                AND TMOV.CODTMV IN ('2.1.10','2.1.15')
                                                AND TITMMOV.QUANTIDADE > 0
                                                group by IDPRD) Y
												on Y.IDPRD = TPRODUTO.IDPRD

                                                left join (SELECT IDPRD, SALDOFISICO2 SALDO_FISICO
                                                FROM TPRDLOC 
                                                WHERE CODCOLIGADA = 1) X
                                                on X.IDPRD = TPRODUTO.IDPRD

												where TPRODUTO.IDPRD = {0}", IDPRD);

            SaldoItem saldo = new SaldoItem();

            using (DataTable dt = MetodosSQL.GetDT(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    saldo.SALDO_FISICO = ((Decimal)dt.Rows[0]["SALDO_FISICO"]);
                    saldo.SALDO_PEDIDO = ((Decimal)dt.Rows[0]["SALDO_PEDIDO"]);
                    saldo.SALDO_DISPONIVEL = ((Decimal)dt.Rows[0]["SALDO_DISPONIVEL"]);
                }
                else
                {
                    saldo.SALDO_FISICO = 0;
                    saldo.SALDO_PEDIDO = 0;
                    saldo.SALDO_DISPONIVEL = 0;
                }

            }

            return saldo;
        }

        public static SaldoItem getSaldo(String CODIGOPRD)
        {
            String sql = String.Format(@"select IDPRD from TPRODUTO where CODCOLPRD = 1 and CODIGOPRD = '{0}'", CODIGOPRD);

            return getSaldo(int.Parse(MetodosSQL.GetField(sql, "IDPRD")));
        }
    }
}
