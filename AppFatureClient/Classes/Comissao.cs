using System;
using AppLib;
using System.Windows.Forms;
using System.Data;
using RM.Lib.Server;

namespace AppFatureClient.Classes
{
    class Comissao
    {
        public class ParametrosComissao
        {
            //Primeira tela
            public string tipo { get; set; }
            public string nPedido { get; set; }
            public string codColigada { get; set; }
            public string codCliente { get; set; }
            public string tipoDocumento { get; set; }
            public string CpfCnpj { get; set; }
            public string numDocumento { get; set; }
            public string segundoNumero { get; set; }
            public decimal? valorOriginal { get; set; }
            public string Moeda { get; set; }
            public string centroCusto { get; set; }
            public string Representante { get; set; }
            public string tipoCobranca { get; set; }
            public string Historico { get; set; }
            public string serieDocumento { get; set; }
            public DateTime? dataEmissao { get; set; }
            public DateTime? dataVencimento { get; set; }
            public DateTime? dataPrevBaixa { get; set; }

            //Segunda tela
            public string codNatOrcamentaria { get; set; }
            public string codCentroCusto { get; set; }
            public decimal? Valor { get; set; }
            public string Porcentagem { get; set; }

        }

        public static int GeraAutoInc(string _codSistema, int _codColigada, string _codAutoInc, string _nomeTabela, string _nomeColuna)
        {
            try
            {
                AppInterop.Util util = new AppInterop.Util();
                AppLib.Util.Alias alias = util.GetAlias(New.Class.EnviromentHelper.IndexAlias);
                string conn = string.Concat("Server=", alias.ServerName, ";Database=", alias.DbName, ";User Id=", alias.UserName, ";Password=", alias.Password, ";");
                RM.Lib.Data.DbServices _dbs = new RM.Lib.Data.DbServices(RM.Lib.Data.ProviderType.SqlClient, conn);

                RMSAutoInc autoInc = new RMSAutoInc(_dbs);

                return autoInc.GetNewValue(_codSistema, _codColigada, _codAutoInc, _nomeTabela, _nomeColuna);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public class MetodosComissao
        {
            public static void IncluiADC(ParametrosComissao pc)
            {
                string sql = String.Empty;
                try
                {
                    //sql = String.Format(@"UPDATE GAUTOINC SET VALAUTOINC = (SELECT VALAUTOINC + 1 FROM GAUTOINC WHERE CODAUTOINC = 'IDLAN' AND CODCOLIGADA = '1')
                    //                    WHERE CODAUTOINC = 'IDLAN'
                    //                    AND CODCOLIGADA = '1'");
                    //AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { });



                    //sql = "SELECT VALAUTOINC FROM GAUTOINC WHERE CODAUTOINC = 'IDLAN' AND CODCOLIGADA = '1'";

                    //string IDLAN = AppLib.Context.poolConnection.Get().ExecGetField(String.Empty, sql, new Object[] { }).ToString();

                    string IDLAN = GeraAutoInc("F", 1, "IDLAN", "FLAN", "IDLAN").ToString();

                    if (!String.IsNullOrWhiteSpace(IDLAN))
                    {
                        sql = String.Format(@"INSERT INTO FLAN (CODCOLIGADA, PAGREC, CODFILIAL, CODCOLCFO, CODCFO,  CODTDO, NUMERODOCUMENTO, SEGUNDONUMERO, DATAEMISSAO, DATAVENCIMENTO, DATAPREVBAIXA, 
			                            HISTORICO, SERIEDOCUMENTO, VALORORIGINAL, CODMOEVALORORIGINAL, CODCCUSTO, CODRPR, CODTB2FLX, IDLAN, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY,
			                            RECMODIFIEDON, CODCOLCXA, INSSEMOUTRAEMPRESA, REUTILIZACAO, VALORSERVICO, CAMPOALFAOP3, DATACRIACAO, DATAALTERACAO, USUARIOCRIACAO, TIPOCONTABILLAN) 
                                        VALUES (1, 1, 1, 0, '{0}', 'ADC', '{1}','{2}', CONVERT(date,'{3}', 103), CONVERT(date,'{4}', 103), CONVERT(date,'{5}', 103), '{6}','{7}', {8}, '{9}', '{10}','{11}', '{12}', 
		                                {18}, '{13}', CONVERT(date,'{14}', 103), '{15}', CONVERT(date,'{16}', 103), 1, 0.0000, 0, 0.0000, '{17}', CONVERT(date,'{19}', 103), CONVERT(date,'{20}', 103), '{21}', 2)", pc.codCliente, pc.numDocumento, pc.segundoNumero, pc.dataEmissao,
                                        pc.dataVencimento, pc.dataPrevBaixa, pc.Historico, pc.serieDocumento, pc.valorOriginal.ToString().Replace(",", "."), pc.Moeda, pc.centroCusto, pc.Representante,
                                        pc.tipoCobranca, Context.Usuario, DateTime.Now, Context.Usuario, DateTime.Now, pc.nPedido, IDLAN, DateTime.Now, DateTime.Now, Context.Usuario);

                        MetodosSQL.ExecQuery(sql);

                        //sql = @"UPDATE GAUTOINC SET VALAUTOINC = (select VALAUTOINC+1 from GAUTOINC where CODAUTOINC = 'IDRATCCU' and CODSISTEMA = 'F')
                        //        where CODAUTOINC = 'IDRATCCU'
                        //        and CODSISTEMA = 'F'";

                        //AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { });

                        int IDRATCCU = GeraAutoInc("F", 0, "IDRATCCU", "FLANRATCCU", "IDRATCCU");

                        //sql = String.Format(@"insert into FLANRATCCU (CODCOLIGADA, IDLAN, CODCCUSTO, VALOR, IDRATCCU, PERCENTUAL, CODCOLNATFINANCEIRA, CODNATFINANCEIRA, RECCREATEDBY, RECCREATEDON)
                        //                      values (1,{0},'{1}',{2},(select VALAUTOINC from GAUTOINC where CODAUTOINC = 'IDRATCCU' and CODSISTEMA = 'F'),{3},1,'{4}', '{5}', CONVERT(date,'{6}', 103))",
                        //                      IDLAN, pc.codCentroCusto, pc.Valor.ToString().Replace(",", "."), pc.Porcentagem.Replace("%", ""), pc.codNatOrcamentaria, Context.Usuario, DateTime.Now);


                        sql = String.Format(@"insert into FLANRATCCU (CODCOLIGADA, IDLAN, CODCCUSTO, VALOR, IDRATCCU, PERCENTUAL, CODCOLNATFINANCEIRA, CODNATFINANCEIRA, RECCREATEDBY, RECCREATEDON)
                                              values (1,{0},'{1}',{2},'{7}',{3},1,'{4}', '{5}', CONVERT(date,'{6}', 103))",
                                              IDLAN, pc.codCentroCusto, pc.Valor.ToString().Replace(",", "."), pc.Porcentagem.Replace("%", ""), pc.codNatOrcamentaria, Context.Usuario, DateTime.Now, IDRATCCU);

                        MetodosSQL.ExecQuery(sql);
                    }
                }
                catch
                { throw; }
            }

            public static void AtualizaADC(ParametrosComissao pc, string IDLAN)
            {
                string sql = String.Empty;

                try
                {
                    sql = String.Format(@"update FLAN
                                          set CODCFO = '{0}', 
                                          SEGUNDONUMERO = '{1}', 
	                                      DATAEMISSAO = CONVERT(date,'{2}', 103), 
	                                      DATAVENCIMENTO = CONVERT(date,'{3}', 103), 
	                                      DATAPREVBAIXA = CONVERT(date,'{4}', 103), 
	                                      HISTORICO = '{5}', 
	                                      SERIEDOCUMENTO = '{6}', 
	                                      VALORORIGINAL = {7}, 
	                                      CODCCUSTO = '{8}', 
	                                      CODRPR = '{9}', 
	                                      CODTB2FLX = '{10}',
	                                      RECMODIFIEDBY = '{11}',
	                                      RECMODIFIEDON = CONVERT(date,'{12}', 103),
                                          DATACRIACAO = CONVERT(date,'{13}', 103),
                                          USUARIOCRIACAO = '{14}'    
	                                      where IDLAN = '{15}'",
                                          pc.codCliente,
                                          pc.segundoNumero,
                                          pc.dataEmissao,
                                          pc.dataVencimento,
                                          pc.dataPrevBaixa,
                                          pc.Historico,
                                          pc.serieDocumento,
                                          pc.valorOriginal.ToString().Replace(",", "."),
                                          pc.centroCusto,
                                          pc.Representante,
                                          pc.tipoCobranca,
                                          Context.Usuario,
                                          DateTime.Now,
                                          DateTime.Now,
                                          Context.Usuario,
                                          IDLAN);

                    MetodosSQL.ExecQuery(sql);



                    sql = String.Format(@"Update FLANRATCCU
								   set CODCOLIGADA = {0}, 
							       CODCCUSTO = '{1}', 
								   VALOR = {2},  
								   PERCENTUAL = {3}, 
								   CODCOLNATFINANCEIRA = {4}, 
								   CODNATFINANCEIRA = '{5}',
								   RECMODIFIEDBY = '{6}',
								   RECMODIFIEDON = CONVERT(date,'{7}', 103)
								   where IDLAN = '{8}'",
                                   1,
                                   pc.codCentroCusto,
                                   pc.Valor.ToString().Replace(",", "."),
                                   pc.Porcentagem.ToString().Replace(",", ".").Replace("%", ""),
                                   1,
                                   pc.codNatOrcamentaria,
                                   Context.Usuario,
                                   DateTime.Now,
                                   IDLAN);

                    MetodosSQL.ExecQuery(sql);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            public static void ExcluirADC(string IDLAN)
            {
                string sql = String.Empty;

                try
                {
                    sql = String.Format(@"select COUNT(1) as 'CONT' from FLANBAIXA where CODCOLIGADA = '1' and IDLAN = {0}", IDLAN);
                    if (MetodosSQL.GetField(sql, "CONT") == "0")
                    {
                        sql = String.Format(@"DELETE FROM FLANRATCCU 
                                              WHERE CODCOLIGADA = 1 
                                              AND IDLAN = {0}
                                              AND IDLAN NOT IN 
				                                              (SELECT IDLAN FROM FLANBAIXA 
				                                               WHERE CODCOLIGADA = 1 
				                                               AND IDLAN = {0})", IDLAN);
                        
                        MetodosSQL.ExecQuery(sql);

                        sql = String.Format(@"DELETE FROM FLAN 
                                              WHERE CODCOLIGADA = 1 
                                              AND CODTDO = 'ADC' 
                                              AND PAGREC = 1 
                                              AND STATUSLAN = 0 
                                              AND IDLAN = {0}
                                              AND IDLAN NOT IN 
				                                              (SELECT IDLAN FROM FLANBAIXA 
				                                               WHERE CODCOLIGADA = 1 
				                                               AND IDLAN = {0})", IDLAN);

                        MetodosSQL.ExecQuery(sql);
                    }
                    else
                    {
                        MessageBox.Show("ADC com baixa resgistrada não pode ser excluido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }


            public static void InsereConta1(string codCliente)
            {
                string sql = string.Empty;
                try
                {
                    int AutoInc = GeraAutoInc("C", 1, "REDUZIDOC", null, null);

                    string usuario = AppLib.Context.Usuario;

                    string cliente = AppLib.Context.poolConnection.Get().ExecGetField(null, "select NOME from FCFO where CODCFO = ?", new Object[] { codCliente }).ToString();

                    #region Antigo
                    sql = String.Format(@"INSERT INTO CCONTA(CODCOLIGADA, CODCONTA, REDUZIDO, DESCRICAO, ANALITICA, RATEIO, NATUREZA, TIPOCORRECAO, TIPOCONTA,
                     BITMAP, OPCIONAL, CODHISTP, INATIVA, USUARIOINCLU, DATAINCLU, NATSPED, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY,
                     RECMODIFIEDON)

                     VALUES(1, ('1.01.03.00001.' + cast((select top 1 SUBSTRING(CODCONTA, 15, 5) + 1 from CCONTA where CODCONTA like '1.01.03.00001.%' order by CODCONTA desc) as varchar)), 
					 (select VALAUTOINC from GAUTOINC
                      where CODAUTOINC = 'REDUZIDOC'

                      AND CODCOLIGADA = 1), 
					  left('{0}',40), 1, 0, 1, 1, 0, 0, 0, 1, 0, '{1}', cast(getdate()-31 as date), '01',
					  '{1}', GETDATE(), '{1}', GETDATE())", cliente, usuario);
                    #endregion

                    sql = String.Format(@"INSERT INTO CCONTA(CODCOLIGADA, CODCONTA, REDUZIDO, DESCRICAO, ANALITICA, RATEIO, NATUREZA, TIPOCORRECAO, TIPOCONTA,
                     BITMAP, OPCIONAL, CODHISTP, INATIVA, USUARIOINCLU, DATAINCLU, NATSPED, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY,
                     RECMODIFIEDON)

                     VALUES(1, ('1.01.03.00001.' + cast((select top 1 SUBSTRING(CODCONTA, 15, 5) + 1 from CCONTA where CODCONTA like '1.01.03.00001.%' order by CODCONTA desc) as varchar)), 
					 '{2}', 
					  left('{0}',40), 1, 0, 1, 1, 0, 0, 0, 1, 0, '{1}', cast(getdate()-31 as date), '01',
					  '{1}', GETDATE(), '{1}', GETDATE())", cliente, usuario, AutoInc);

                    MetodosSQL.ExecQuery(sql);

                    //sql = @"UPDATE GAUTOINC SET VALAUTOINC = (select VALAUTOINC+1 from GAUTOINC
                    //           where CODAUTOINC = 'REDUZIDOC'
                    //           AND CODCOLIGADA = 1)
                    //           where CODAUTOINC = 'REDUZIDOC'
                    //           AND CODCOLIGADA = 1";

                    //AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { });

                    string codConta = AppLib.Context.poolConnection.Get().ExecGetField(null, "select top 1 CODCONTA from CCONTA where CODCOLIGADA = 1 and CODCONTA like '1.01.03.00001.%' order by CODCONTA desc", new Object[] { }).ToString();

                    AssociaConta1(codCliente, codConta);
                }
                catch (Exception ex)
                {
                    throw new Exception("Conta 1: " + ex.Message, ex);
                }
            }

            public static void InsereConta2(string codCliente)
            {
                string sql = string.Empty;

                try
                {
                    int AutoInc = GeraAutoInc("C", 1, "REDUZIDOC", null, null);

                    string usuario = AppLib.Context.Usuario;

                    string cliente = AppLib.Context.poolConnection.Get().ExecGetField(null, "select NOME from FCFO where CODCFO = ?", new Object[] { codCliente }).ToString();

                    #region Antigo
                    //              sql = String.Format(@"INSERT INTO CCONTA (CODCOLIGADA, CODCONTA, REDUZIDO, DESCRICAO, ANALITICA, RATEIO, NATUREZA, TIPOCORRECAO, TIPOCONTA,
                    //BITMAP, OPCIONAL, CODHISTP, INATIVA, USUARIOINCLU, DATAINCLU, NATSPED, RECCREATEDBY, RECCREATEDON,RECMODIFIEDBY,
                    //RECMODIFIEDON)
                    //VALUES (1, ('2.01.04.00001.'+ cast((select top 1 REPLACE(0, 5 - LEN((SUBSTRING(CODCONTA, 15, 5) + 1)), SUBSTRING(CODCONTA, 15, 5) + 1) from CCONTA where CODCONTA like '2.01.04.00001.%' order by CODCONTA desc)as varchar)), 
                    //(select VALAUTOINC from GAUTOINC
                    // where CODAUTOINC = 'REDUZIDOC'
                    // AND CODCOLIGADA = 1), 
                    // left('{0}',40), 1, 0, 0, 1, 0, 0, 0, 1, 0, '{1}', cast(getdate()-31 as date), '02',
                    // '{1}', GETDATE(), '{1}', GETDATE())", cliente, usuario);
                    #endregion

                    sql = String.Format(@"INSERT INTO CCONTA (CODCOLIGADA, CODCONTA, REDUZIDO, DESCRICAO, ANALITICA, RATEIO, NATUREZA, TIPOCORRECAO, TIPOCONTA,
                    BITMAP, OPCIONAL, CODHISTP, INATIVA, USUARIOINCLU, DATAINCLU, NATSPED, RECCREATEDBY, RECCREATEDON,RECMODIFIEDBY,
                    RECMODIFIEDON)
                    VALUES (1, ('2.01.04.00001.'+ cast((select top 1 REPLACE(0, 5 - LEN((SUBSTRING(CODCONTA, 15, 5) + 1)), SUBSTRING(CODCONTA, 15, 5) + 1) from CCONTA where CODCONTA like '2.01.04.00001.%' order by CODCONTA desc)as varchar)), 
                    '{2}', 
                     left('{0}',40), 1, 0, 0, 1, 0, 0, 0, 1, 0, '{1}', cast(getdate()-31 as date), '02',
                     '{1}', GETDATE(), '{1}', GETDATE())", cliente, usuario, AutoInc);

                    MetodosSQL.ExecQuery(sql);

                    //sql = @"UPDATE GAUTOINC SET VALAUTOINC = (select VALAUTOINC+1 from GAUTOINC
                    //           where CODAUTOINC = 'REDUZIDOC'
                    //           AND CODCOLIGADA = 1)
                    //           where CODAUTOINC = 'REDUZIDOC'
                    //           AND CODCOLIGADA = 1";

                    //AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { });

                    string codConta = AppLib.Context.poolConnection.Get().ExecGetField(null, "select top 1 CODCONTA from CCONTA where CODCOLIGADA = 1 and CODCONTA like '2.01.04.00001.%' order by CODCONTA desc", new Object[] { }).ToString();

                    AssociaConta2(codCliente, codConta);
                }
                catch (Exception ex)
                {
                    throw new Exception("Conta 2: " + ex.Message, ex);
                }
            }

            public static void AssociaConta1(string codCliente, string codConta)
            {
                try
                {
                    string usuario = AppLib.Context.Usuario;
                    string sql = String.Empty;

                    //string sql = @"UPDATE GAUTOINC SET VALAUTOINC = VALAUTOINC + 1 WHERE CODCOLIGADA = 0 AND CODSISTEMA = 'F' AND CODAUTOINC = 'IDCFOCONT'";
                    //MetodosSQL.ExecQuery(sql);

                    int AutoInc = GeraAutoInc("F", 0, "IDCFOCONT", "FCFOCONT", "IDCFOCONT");



                    sql = String.Format(@"INSERT INTO FCFOCONT (CODCOLIGADA, IDCFOCONT , CODCFO, TIPO, PAGREC, PERCENTUAL,CODCOLCFO, CODCOLCONTA, CODCONTA, RECCREATEDBY, RECCREATEDON,RECMODIFIEDBY,
					           RECMODIFIEDON, CLASSCONTA)
					           VALUES (1, '{3}', '{0}', 1, 1, 100, 0, 1, '{1}', '{2}', GETDATE(), '{2}', GETDATE(), 'C')", codCliente, codConta, usuario, AutoInc);

                    MetodosSQL.ExecQuery(sql);

                    //sql = @"UPDATE GAUTOINC SET VALAUTOINC = VALAUTOINC + 1 WHERE CODCOLIGADA = 0 AND CODSISTEMA = 'F' AND CODAUTOINC = 'IDCFOCONT'";

                    //MetodosSQL.ExecQuery(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            public static void AssociaConta2(string codCliente, string codConta)
            {
                try
                {
                    string usuario = AppLib.Context.Usuario;
                    string sql = String.Empty;
                    //string sql = @"UPDATE GAUTOINC SET VALAUTOINC = VALAUTOINC + 1 WHERE CODCOLIGADA = 0 AND CODSISTEMA = 'F' AND CODAUTOINC = 'IDCFOCONT'";
                    //MetodosSQL.ExecQuery(sql);

                    int AutoInc = GeraAutoInc("F", 0, "IDCFOCONT", "FCFOCONT", "IDCFOCONT");

                    sql = String.Format(@"INSERT INTO FCFOCONT (CODCOLIGADA, IDCFOCONT , CODCFO, TIPO, PAGREC, PERCENTUAL,CODCOLCFO, CODCOLCONTA, CODCONTA, RECCREATEDBY, RECCREATEDON,RECMODIFIEDBY,
					           RECMODIFIEDON, CLASSCONTA)
					           VALUES (1, '{3}', '{0}', 1, 1, 100, 0, 1, '{1}', '{2}', GETDATE(), '{2}', GETDATE(), 'ADC')", codCliente, codConta, usuario, AutoInc);

                    MetodosSQL.ExecQuery(sql);

                    //sql = @"UPDATE GAUTOINC SET VALAUTOINC = VALAUTOINC + 1 WHERE CODCOLIGADA = 0 AND CODSISTEMA = 'F' AND CODAUTOINC = 'IDCFOCONT'";

                    //MetodosSQL.ExecQuery(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            public static void ExcluiAssociacaoConta(string codCliente, string codConta)
            {
                try
                {
                    string sql = String.Format(@"delete from FCFOCONT where CODCFO = {0} and CODCONTA = '{1}'", codCliente, codConta);
                    MetodosSQL.ExecQuery(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            public static void AtualizaValores()
            {
                try
                {
                    //                    string sql = @"-- ADIANTAMENTOS

                    //SELECT ISNULL((
                    //SELECT
                    //		SUM(ISNULL(FLANBAIXA.VALORBAIXA,0))
                    //FROM FLAN

                    //LEFT JOIN FLANBAIXA 
                    //ON FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
                    //AND FLANBAIXA.IDLAN = FLAN.IDLAN

                    //WHERE FLAN.CODCOLIGADA = 1
                    //AND FLAN.CODTDO = 'ADC'
                    //AND FLAN.CAMPOALFAOP3 = (SELECT NUMEROMOV FROM TMOV WHERE IDMOV = ZTC.IDMOV AND CODCOLIGADA = 1 AND CODTMV = '2.1.10')
                    //AND FLAN.CAMPOALFAOP3 IS NOT NULL
                    //AND FLANBAIXA.STATUS = 0),0)

                    //+

                    //-- FATURAMENTO SEM VINCULO

                    //ISNULL((
                    //SELECT
                    //		SUM(ISNULL(FLANBAIXA.VALORBAIXA,0))
                    //FROM FLAN

                    //LEFT JOIN FLANBAIXA 
                    //ON FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
                    //AND FLANBAIXA.IDLAN = FLAN.IDLAN

                    //WHERE FLAN.CODCOLIGADA = 1
                    //AND FLAN.CODTDO = 'NF_e'
                    //AND FLAN.CAMPOALFAOP3 = (SELECT NUMEROMOV FROM TMOV WHERE IDMOV = ZTC.IDMOV AND CODCOLIGADA = 1 AND CODTMV = '2.1.10')
                    //AND FLAN.CAMPOALFAOP3 IS NOT NULL
                    //AND FLANBAIXA.STATUS = 0),0)

                    //+

                    //-- LANÇAMENTO FINANCEIRO

                    //ISNULL((SELECT
                    //	(SUM(ISNULL(FLANBAIXA.VALORBAIXA,0)) - SUM(ISNULL(FLANBAIXA.VALORDESCONTO,0)) - SUM(ISNULL(FLANBAIXA.VALOROP5,0)) - SUM(ISNULL(FLANBAIXA.VALORNOTACREDITOADIANTAMENTO,0)))
                    //FROM FLAN

                    //LEFT JOIN FLANBAIXA 
                    //ON FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
                    //AND FLANBAIXA.IDLAN = FLAN.IDLAN

                    //WHERE FLAN.IDMOV IN (SELECT 
                    //						IDMOVDESTINO 
                    //					FROM 
                    //						TMOVRELAC 
                    //					WHERE 
                    //						CODCOLORIGEM = 1
                    //					AND IDMOVORIGEM = ZTC.IDMOVOP

                    //					AND IDMOVDESTINO NOT IN (SELECT IDMOV FROM TMOV WHERE CODTMV IN ('2.1.40'))
                    //					)
                    //AND FLAN.CODCOLIGADA = 1
                    //AND FLAN.CODTDO <> 'ADC'
                    //AND FLANBAIXA.STATUS = 0),0) AS 'BAIXA', 
                    //ZTC.IDCOMISSAO 
                    //FROM ZTMOVCOMISSAO ZTC 
                    //WHERE ZTC.CONCLUIDO = 0";

                    string sql = @"select W.IDCOMISSAO, sum(Y.VALORBAIXADO) as 'BAIXA' from (
SELECT
	FLAN.CAMPOALFAOP3 as 'PEDIDO',
	FLAN.VALORBAIXADO+isnull(FLAN.valorop5, 0) as 'VALORBAIXADO'

FROM FLAN

LEFT JOIN FLANBAIXA 
ON FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
AND FLANBAIXA.IDLAN = FLAN.IDLAN

WHERE FLAN.CODCOLIGADA = 1
AND FLAN.CODTDO = 'ADC'
and FLAN.CAMPOALFAOP3 is not null

UNION

SELECT
	FLAN.CAMPOALFAOP3 as 'PEDIDO',
	FLAN.VALORBAIXADO+isnull(FLAN.valorop5, 0) as 'VALORBAIXADO'

FROM FLAN

LEFT JOIN FLANBAIXA 
ON FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
AND FLANBAIXA.IDLAN = FLAN.IDLAN

WHERE FLAN.CODCOLIGADA = 1
AND FLAN.CODTDO = 'NF_e'
and FLAN.CAMPOALFAOP3 is not null

UNION

SELECT
	(select top 1 NUMEROMOV from TMOV where IDMOV in 
	(select IDMOVORIGEM 
	from TMOVRELAC 
	where IDMOVDESTINO = FLAN.IDMOV
	and IDMOVORIGEM in (select IDMOV from TMOV where CODTMV = '2.1.20' AND SERIE = 'OPG' AND CODCOLIGADA = 1)
	AND CODCOLORIGEM = 1)) as 'PEDIDO',
	FLAN.VALORBAIXADO+isnull(FLAN.valorop5, 0) as 'VALORBAIXADO'

FROM FLAN

LEFT JOIN FLANBAIXA 
ON FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
AND FLANBAIXA.IDLAN = FLAN.IDLAN

WHERE FLAN.IDMOV IN (SELECT 
						IDMOVDESTINO 
					FROM 
						TMOVRELAC 
					WHERE 
						CODCOLORIGEM = 1
					AND IDMOVORIGEM in (SELECT IDMOV FROM TMOV WHERE CODCOLIGADA = 1 AND CODTMV = '2.1.20' AND SERIE = 'OPG')
					AND IDMOVDESTINO NOT IN (SELECT IDMOV FROM TMOV WHERE CODCOLIGADA = 1 AND CODTMV IN ('2.1.40')))
AND FLAN.CODCOLIGADA = 1
AND FLAN.CODTDO <> 'ADC') Y

inner join (select IDCOMISSAO, NUMEROMOV from ZTMOVCOMISSAO 

inner join TMOV
on TMOV.IDMOV = ZTMOVCOMISSAO.IDMOV

where TMOV.CODTMV = '2.1.10'
and TMOV.CODCOLIGADA = 1
and ZTMOVCOMISSAO.CONCLUIDO = 0) W
on W.NUMEROMOV = Y.PEDIDO

group by W.IDCOMISSAO";



                    DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { });

                    string valor = String.Empty;
                    string update = String.Empty;

                    MetodosSQL.CS = AppLib.Context.poolConnection.Get().ConnectionString;

                    foreach (DataRow x in dt.Rows)
                    {

                        //valor = MetodosSQL.GetField(String.Format(sql, x["NUMEROMOV"].ToString()), "ValorBaixado");
                        //valor = AppLib.Context.poolConnection.Get().ExecGetField("", ), new Object[] { }).ToString();
                        valor = x["Baixa"].ToString();

                        update = String.Format(@"update ZTMOVCOMISSAO
                                                                                                set VALORPAGO = {0}
                                                                                                where IDCOMISSAO = '{1}'", valor.Replace(",", "."), x["IDCOMISSAO"].ToString());

                        MetodosSQL.ExecQuery(update);

                    }
                    

                    MessageBox.Show("Valores atualizados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

        }
    }
}
