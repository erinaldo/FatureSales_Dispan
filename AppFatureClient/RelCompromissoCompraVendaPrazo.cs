using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelCompromissoCompraVendaPrazo : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }

        public RelCompromissoCompraVendaPrazo()
        {
            InitializeComponent();
        }

        public void Cabecalho()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"UPDATE RRELAT SET NROIMPRESSAO  = NROIMPRESSAO + 1 WHERE CODRELATORIO = '01.01' AND CODAPLIC = 'T' AND CODCOLIGADA = ? AND ID = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, 487 });
            AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction(sSql, new Object[] { });

            sSql = @"SELECT ID, NROIMPRESSAO FROM RRELAT WHERE CODRELATORIO = '01.01' AND CODAPLIC = 'T' AND CODCOLIGADA = ? AND ID = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, 487 });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel11.Text = row["NROIMPRESSAO"].ToString();
            }        
        }

        public void Detalhe()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SET LANGUAGE Português 
SELECT TMOV.idmov, 
CAST('Pelo presente instrumento particular de compromisso de venda e compra a prazo de peças e máquinas agrícolas, de um lado PALINI & ALVES LTDA, pessoa jurídica de direito privado, inscrita no CNPJ sob o nº 49.393.549/0001-82, sediada à Rodovia SP-342 KM-199, nº 225, Distrito Industrial Irmãos Del Guerra, em Espírito Santo do Pinhal - SP, doravante denominada simplesmente de COMPROMITENTE VENDEDORA, e de outro lado '+fcfo.nome+', '+ CASE WHEN FCFO.PESSOAFISOUJUR = 'J' THEN 'CNPJ:' ELSE 'CPF:' END +'  '+ ISNULL(FCFO.CGCCFO,'') +' , '+ CASE WHEN FCFO.PESSOAFISOUJUR = 'J' THEN 'I.E:' ELSE 'RG:' END +' '+ ISNULL(FCFO.INSCRESTADUAL,'')+', Domiciliado/sediada em '+ ISNULL(FCFO.CIDADEENTREGA,'')+' - '+ ISNULL(FCFO.CODETDENTREGA,'')+' , à '+ ISNULL(DTIPORUA.DESCRICAO,'')+' '+ ISNULL(FCFO.RUAENTREGA,'')+', nº '+ ISNULL(FCFO.NUMEROENTREGA,'')+', Compl.'+ISNULL(FCFO.COMPLEMENTREGA,'')+', '+ISNULL(DTIPOBAIRRO.DESCRICAO,'')+' '+ ISNULL(FCFO.BAIRROENTREGA,'') +' , doravante denominada simplesmente de COMPROMISSÁRIO(A) COMPRADOR(A), têm entre si como justo e compromissado o seguinte:'
AS TEXT) texto1, 

CAST('CLÁUSULA PRIMEIRA - A Compromitente Vendedora neste ato se compromete a vender e o Compromissário Comprador se compromete a comprar, pelo preço certo e ajustado de ' + dbo.FC_FORMATA_MOEDA(TMOV.VALORLIQUIDOORIG,'.',',') + ' (' + dbo.FC_EXTENSO(TMOV.VALORLIQUIDOORIG) + '), a(s) máquina(s) e peça(s) constantes da descrição das mercadorias  nº '+tmov.numeromov+', que segue em anexo a este presente instrumento, e deste fica fazendo parte integrante. 
Parágrafo Primeiro: Em caso de opção pelo Compromissário Comprador de financiamento do bem, este entregará à Compromitente Vendedora no ato do pedido, os cheques ou qualquer outro título que represente a dívida e comporte desconto no mercado cambial, comprometendo-se a Compromitente Vendedora, quando da liberação do valor financiado e o efetivo pagamento das mercadorias, a devolver os títulos emitidos pelo Compromissário Comprador que ainda não tiverem sido compensados, bem como o valor financiado que exceder ao preço ora contratado, em razão da liquidação dos títulos.'
AS TEXT) texto2, 

DATENAME(WEEKDAY, tmov.dataemissao) + ', ' + CONVERT(VARCHAR(02),DATEPART(DAY,tmov.dataemissao)) + ' DE ' + DATENAME(MONTH,tmov.dataemissao) + ' DE ' + CONVERT(VARCHAR(04),DATEPART(YEAR,tmov.dataemissao)) +'.' AS EMISSAO
FROM TMOV, TMOVCOMPL, FCFO, DTIPORUA, DTIPOBAIRRO WHERE TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA AND TMOV.IDMOV = TMOVCOMPL.IDMOV AND tmov.codcolcfo = fcfo.codcoligada and tmov.codcfo = fcfo.codcfo AND FCFO.TIPORUAENTREGA = DTIPORUA.CODIGO AND FCFO.TIPOBAIRROENTREGA = DTIPOBAIRRO.CODIGO AND TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel13.Text = row["texto1"].ToString();
                xrLabel25.Text = row["texto2"].ToString();

                txtDATAEMISSAO.Text = row["emissao"].ToString();
            }
        }

        public void Detalhe1()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, CAST('Parágrafo Segundo: Não caberá à Compromitente Vendedora a responsabilidade de providenciar junto às instituições financeiras a liberação do financiamento, valendo-se dos títulos emitidos pelo Compromissário Comprador para o recebimento de seu crédito. 'AS TEXT) texto
FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel12.Text = row["texto"].ToString();
            }
        }

        public void Detalhe2()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, CAST('CLÁUSULA SEGUNDA - Por conta do preço referido na cláusula primeira, a Compromitente Vendedora receberá:'AS TEXT) texto
FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel14.Text = row["texto"].ToString();
            }
        }

        public void Detalhe3()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT
  TMOV.IDMOV,
  CASE WHEN TMOVCOMPL.TIPOPAGTO = 'UNICO' THEN CAST('Como pagamento único, e vencido em '+ Convert( VarChar(10),TMOVCOMPL.VENCUNICO,103) +', a importância de ' + dbo.FC_FORMATA_MOEDA(TMOVCOMPL.VALORUNICO,'.',',') + ' (' + dbo.FC_EXTENSO(TMOVCOMPL.VALORUNICO) + ').' AS TEXT)
       WHEN TMOVCOMPL.TIPOPAGTO = 'PARCELADO' THEN CAST('Como arras ou sinal de pagamento, e vencido em '+ Convert(varchar(10), TMOVCOMPL.VENCPARC,103) +' a importância de ' + dbo.FC_FORMATA_MOEDA(TMOVCOMPL.VALORPARC,'.',',') + ' (' + dbo.FC_EXTENSO(TMOVCOMPL.VALORPARC) + '), que em caso de não cumprimento deste contrato, perderá o valor em favor da COMPROMITENTE VENDEDORA.
'+ CAST(TMOVCOMPL.NUMPARC AS VARCHAR(70))+' parcelas mensais de ' + dbo.FC_FORMATA_MOEDA(TMOVCOMPL.VALORNUMPARC,'.',',') + ' (' + dbo.FC_EXTENSO(TMOVCOMPL.VALORNUMPARC) + '), a serem pagas a cada '+ CAST(TMOVCOMPL.PERIODICIDADE AS VARCHAR(70))+' dias, vencida a primeira em '+ Convert( VarChar(10),TMOVCOMPL.PRIMVENCPARC,103) +' e as demais sucessivamente.'AS TEXT)
       ELSE CAST(''+ CAST(TMOVCOMPL.OUTROSPAGTO AS VARCHAR(500))+''AS TEXT)
       END texto
FROM
  TMOV,
  TMOVCOMPL
WHERE     TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA
      AND TMOV.IDMOV       = TMOVCOMPL.IDMOV
      AND TMOV.CODCOLIGADA = ?
      AND TMOV.IDMOV       = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel15.Text = row["texto"].ToString();
            }
        }

        public void Detalhe4()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('CLÁUSULA TERCEIRA - Por força deste Compromisso de compra e venda, aqui expressamente instituído e aceito pelas partes, fica reservada à Compromitente Vendedora, a propriedade do(s) objeto(s) descrito(s) na cláusula primeira (constante da referida descrição), até que sejam liquidados os valores aqui contratados, ou até que seja liberado pela Instituição Financeira o financiamento pretendido pelo Compromissário Comprador, quando então será emitida a nota fiscal de transmissão de propriedade.'AS TEXT) texto1,
CAST('CLÁUSULA QUARTA - O Compromissário Comprador, por conta da posse direta que exercerá durante o cumprimento deste compromisso, recebendo os equipamentos através de nota fiscal de demonstração, assume neste ato o encargo de fiel depositário dos bens, obrigando-se por sua guarda e conservação, e desde já advertido que caso não seja efetuado o pagamento de qualquer das prestações, ficará, desde logo, constituído em mora conforme parágrafo primeiro da cláusula sexta, e obrigado sob as penas da lei, a restituir incontinenti o(s) objeto(s) de cuja posse se vale neste ato.'AS TEXT) texto2
FROM TMOV WHERE TMOV.CODCOLIGADA = ? and IDMOV= ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel16.Text = row["texto"].ToString();
            }
        }

        public void Detalhe5()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('CLÁUSULA TERCEIRA - Por força deste Compromisso de compra e venda, aqui expressamente instituído e aceito pelas partes, fica reservada à Compromitente Vendedora, a propriedade do(s) objeto(s) descrito(s) na cláusula primeira (constante da referida descrição), até que sejam liquidados os valores aqui contratados, ou até que seja liberado pela Instituição Financeira o financiamento pretendido pelo Compromissário Comprador, quando então será emitida a nota fiscal de transmissão de propriedade.'AS TEXT) texto1,
CAST('CLÁUSULA QUARTA - O Compromissário Comprador, por conta da posse direta que exercerá durante o cumprimento deste compromisso, recebendo os equipamentos através de nota fiscal de demonstração, assume neste ato o encargo de fiel depositário dos bens, obrigando-se por sua guarda e conservação, e desde já advertido que caso não seja efetuado o pagamento de qualquer das prestações, ficará, desde logo, constituído em mora conforme parágrafo primeiro da cláusula sexta, e obrigado sob as penas da lei, a restituir incontinenti o(s) objeto(s) de cuja posse se vale neste ato.'AS TEXT) texto2 
FROM TMOV WHERE TMOV.CODCOLIGADA = ? and tmov.idmov = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel18.Text = row["texto1"].ToString();
                xrLabel26.Text = row["texto2"].ToString();
            }
        }

        public void Detalhe6()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('Parágrafo Único - Recusado o encargo de fiel depositário pelo Compromissário Comprador, o Sr.(a): '+ ISNULL(TMOVCOMPL.NOME_COMP1,'') +',  '+ CASE WHEN ISNULL(TMOVCOMPL.PESSOAFISOUJUR1,'') = 'J' THEN 'CNPJ:' ELSE 'CPF:' END +' '+ ISNULL(TMOVCOMPL.CGCCFO_COMP1,'') +', '+ CASE WHEN ISNULL(TMOVCOMPL.PESSOAFISOUJUR1,'') = 'J' THEN 'I.E:' ELSE 'RG:' END +' '+ ISNULL(TMOVCOMPL.RGIE_COMP1,'') +', domiciliado/sediada em: '+ ISNULL(TMOVCOMPL.MUN_UF_COMP1,'') +' , à '+ ISNULL(TMOVCOMPL.ENDERECO_COMP1,'') +', voluntariamente assume tal compromisso, obrigando-se na forma do caput desta cláusula, e desde já ciente das conseqüências deste encargo.'AS TEXT) texto1,
CAST('CLÁUSULA QUINTA - Enquanto não houver pagamento integral do preço, obriga-se o Compromissário Comprador, bem como o fiel depositário (caso seja terceiro), a defender os equipamentos da turbação de terceiros e permitindo à Compromitente Vendedora a inspeção.'AS TEXT) texto2
FROM TMOV, TMOVCOMPL WHERE TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA AND TMOV.IDMOV = TMOVCOMPL.IDMOV AND TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel19.Text = row["texto1"].ToString();
                xrLabel27.Text = row["texto2"].ToString();
            }
        }

        public void Detalhe7()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('CLÁUSULA SEXTA - Em caso de não pagamento das parcelas ora contratadas, o presente compromisso fica rescindido de pleno direito, e o Compromissário Comprador arcará com todas as despesas de desmontagem e transporte dos equipamentos constantes da descrição das mercadorias (pedido), valendo o presente como título executivo extrajudicial, mediante comprovação de despesas efetuadas pela Compromissária Vendedora, sem prejuízo da retenção das arras.'AS TEXT) texto1,
CAST('Parágrafo Primeiro: Em caso de mora, inadimplemento ou rescisão, o Compromissário Comprador se sujeitará ainda ao pagamento de multa de 2% (Dois por cento) sobre o valor remanescente do compromisso (quando rescindido) ou da parcela em atraso, e juros de 1% (Hum por cento) ao mês pro rata die e pagamento de honorários advocatícios de 5% (cinco por cento), independente dos honorários de sucumbência eventualmente fixados.'AS TEXT) texto2,
CAST('Parágrafo Segundo: Em caso de rescisão do presente compromisso vencerá antecipadamente todas as parcelas vincendas, reservando a Compromitente Vendedora no direito de optar pela reintegração de posse dos equipamentos, o que desde logo é autorizado pelo compromissário comprador, ou promover a execução dos créditos deste decorrente, hipótese em que será emitida a nota fiscal de transmissão de propriedade.'AS TEXT) texto3
FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel20.Text = row["texto1"].ToString();
                xrLabel28.Text = row["texto2"].ToString();
                xrLabel29.Text = row["texto3"].ToString();
            }
        }

        public void Detalhe8()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('CLÁUSULA SÉTIMA - Incluem-se no preço deste compromisso, sendo de responsabilidade da Compromitente Vendedora, os itens marcados com a palavra SIM no inicio da frase:'AS TEXT) texto
FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel21.Text = row["texto"].ToString();
            }
        }

        public void Detalhe9()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
ISNULL(TMOVCOMPL.RESP01,'NÃO') +' - As despesas de frete e seguro dos equipamentos.' texto1,
ISNULL(TMOVCOMPL.RESP02,'NÃO') +' - As despesas com montagem do equipamento, compreendidas pela diária do mecânico, viagens de ida e volta ao local da montagem.' texto2, 
ISNULL(TMOVCOMPL.RESP03,'NÃO') +' - Despesas de refeição e estadias dos mecânicos.' texto3,
ISNULL(TMOVCOMPL.RESP04,'NÃO') +' - Fornecimento de ajudantes braçais necessários à montagem dos equipamentos.' texto4,
ISNULL(TMOVCOMPL.RESP05,'NÃO') +' - Materiais necessário para a execução da montagem como: Caminhão Munk, compressor, disco de corte e desbaste.' texto5,
ISNULL(TMOVCOMPL.RESP06,'NÃO') +' - O valor da complementação do ICMS devido por ocasião de entrada do bem no Estado de destino.' texto6,
ISNULL(TMOVCOMPL.RESP07,'NÃO') +' - Tubulações hidráulicas e ligações que poderão ser de PVC.' texto7,
ISNULL(TMOVCOMPL.RESP08,'NÃO') +' - Embalagem do produto quando exigida por transportadora contratada por qualquer das partes.' texto8,
ISNULL(TMOVCOMPL.RESP09,'NÃO') +' - As ligações elétricas, quadros de comandos, chaves elétricas, etc.'texto9,
ISNULL(TMOVCOMPL.RESP10,'NÃO') +' - Serviços de alvenarias para instalação dos equipamentos (bases para chumbações, moegas e poços dos elevadores)' texto10,
ISNULL(TMOVCOMPL.RESP12,'NÃO') +' - '+ CONVERT(VARCHAR, ISNULL(TMOVCOMPL.RESP11,'')) texto11
FROM TMOV, TMOVCOMPL WHERE TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA AND TMOV.IDMOV = TMOVCOMPL.IDMOV AND TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel22.Text = row["texto1"].ToString();
                xrLabel30.Text = row["texto2"].ToString();
                xrLabel31.Text = row["texto3"].ToString();
                xrLabel32.Text = row["texto4"].ToString();
                xrLabel33.Text = row["texto5"].ToString();
                xrLabel34.Text = row["texto6"].ToString();
                xrLabel35.Text = row["texto7"].ToString();
                xrLabel36.Text = row["texto8"].ToString();
                xrLabel37.Text = row["texto9"].ToString();
                xrLabel38.Text = row["texto10"].ToString();
                xrLabel39.Text = row["texto11"].ToString();
            }
        }

        public void Detalhe10()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('Parágrafo Único - Caso queira, e seja expressamente autorizada pelo Compromissário Comprador, a Compromitente Vendedora, poderá assumir despesas complementares não inclusas no preço, reservando-se no direito de cobrar os valores efetivamente despendidos.'AS TEXT) texto1,
CAST('CLÁUSULA OITAVA - A Compromitente Vendedora garante os equipamentos objeto deste instrumento, bem como o seu perfeito funcionamento pelo período de 18 meses contados da data de seu faturamento, ou 12 meses contados da data de sua instalação, valendo o prazo que primeiro expirar, desde que o Compromissário Comprador:'AS TEXT) texto2,
CAST('I.      Contrate o serviço de montagem autorizado pela Compromitente Vendedora.'AS TEXT) texto3,
CAST('II.     Utilize e proceda a manutenção no equipamento de acordo com as recomendações técnicas constantes do manual de instruções.'AS TEXT) texto4
FROM TMOV WHERE TMOV.CODCOLIGADA = ? and idmov = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel23.Text = row["texto1"].ToString();
                xrLabel40.Text = row["texto2"].ToString();
                xrLabel41.Text = row["texto3"].ToString();
                xrLabel42.Text = row["texto4"].ToString();
            }
        }

        public void Detalhe11()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('Parágrafo Primeiro - A garantia contra defeitos decorrentes do uso normal do equipamento será prestada na oficina da Compromitente Vendedora, localizada em Espírito Santo do Pinhal - SP, cabendo ao Compromissário Comprador as despesas com desmontagem, transporte e remontagem do equipamento.'AS TEXT) texto1,
CAST('Parágrafo Segundo - A garantia de peças eventualmente substituídas encerra-se com a garantia das peças originais.'AS TEXT) texto2,
CAST('Parágrafo Terceiro - A garantia de que trata esta cláusula, não engloba danos ao equipamento causados por acidentes, desgaste decorrente de uso impróprio, instalação inadequada, manobra e usos indevidos, armazenagem inadequada, montagem executada fora dos padrões pré-estabelecidos ou pessoas não autorizadas pela Compromitente Vendedora, transporte negligente, ou em caso de o Compromissário Comprador haver empreendido reparos por conta própria, ou por empresas não autorizadas.'AS TEXT) texto3,
CAST('Parágrafo Quarto - Em se tratando de acessórios e/ou equipamentos que não sejam de fabricação própria, a garantia dada pela Compromitente Vendedora restringir-se-á às normas de garantia estipuladas pelos fabricantes de cada acessório, inclusive quanto ao prazo de vigência.'AS TEXT) texto4,
CAST('CLÁUSULA NONA - O uso inadequado do equipamento e a ocorrência de qualquer espécie de dano oriunda deste evento, não obrigarão a Compromitente Vendedora a qualquer tipo de indenização, já que todas as engrenagens e transmissões, bem como partes que tenham potencialidade de risco, contam com as respectivas proteções.'AS TEXT) texto5,
CAST('CLÁUSULA DÉCIMA - As partes elegem esta comarca de Espírito Santo do Pinhal/SP para dirimir qualquer controvérsia advinda do presente compromisso, renunciando a qualquer outro por mais privilegiado que seja.'AS TEXT) texto6
FROM TMOV WHERE TMOV.CODCOLIGADA = ? and tmov.idmov = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel24.Text = row["texto1"].ToString();
                xrLabel43.Text = row["texto2"].ToString();
                xrLabel44.Text = row["texto3"].ToString();
                xrLabel45.Text = row["texto4"].ToString();
                xrLabel46.Text = row["texto5"].ToString();
                xrLabel47.Text = row["texto6"].ToString();
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe();
        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe1();
        }

        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe2();
        }

        private void Detail3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe3();
        }

        private void Detail4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe4();
        }

        private void Detail5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe5();
        }

        private void Detail6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe6();
        }

        private void Detail7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe7();
        }

        private void Detail8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe8();
        }

        private void Detail9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe9();
        }

        private void Detail10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe10();
        }

        private void Detail11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe11();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Cabecalho();
        }
    }
}
