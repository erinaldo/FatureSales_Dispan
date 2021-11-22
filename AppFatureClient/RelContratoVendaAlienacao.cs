using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelContratoVendaAlienacao : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }

        public RelContratoVendaAlienacao()
        {
            InitializeComponent();
        }

        public void Cabecalho()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"UPDATE RRELAT SET NROIMPRESSAO  = NROIMPRESSAO + 1 WHERE CODRELATORIO = '01.02' AND CODAPLIC = 'T' AND CODCOLIGADA = ? AND ID = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, 488 });
            AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction(sSql, new Object[] { });

            sSql = @"SELECT ID, NROIMPRESSAO FROM RRELAT WHERE CODRELATORIO = '01.02' AND CODAPLIC = 'T' AND CODCOLIGADA = ? AND ID = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, 488 });
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
SELECT TMOV.IDMOV, 
CAST('Pelo presente instrumento particular de contrato de venda e compra a prazo de peças e máquinas agrícolas com reserva de domínio, de um lado :'AS TEXT) texto1,  
CAST('PALINI & ALVES LTDA, pessoa jurídica de direito privado, inscrita no CNPJ sob o nº 49.393.549/0001-82, sediada à Rodovia SP-342 KM-199, nº 225, Distrito Industrial Irmãos Del Guerra, em Espírito Santo do Pinhal - SP, doravante denominada simplesmente de VENDEDORA, e de outro lado '+ ISNULL(FCFO.NOME,'') +', '+ CASE WHEN FCFO.PESSOAFISOUJUR = 'J' THEN 'CNPJ:' ELSE 'CPF:' END +' '+ ISNULL(FCFO.CGCCFO,'') +' , '+ CASE WHEN FCFO.PESSOAFISOUJUR = 'J' THEN 'I.E:' ELSE 'RG:' END +' '+ ISNULL(FCFO.INSCRESTADUAL,'')+', Domiciliado/sediada em '+ ISNULL(FCFO.CIDADEENTREGA,'')+', - '+ ISNULL(FCFO.CODETDENTREGA,'')+' à  '+ ISNULL(DTIPORUA.DESCRICAO,'')+' '+ ISNULL(FCFO.RUAENTREGA,'')+', nº '+ ISNULL(FCFO.NUMEROENTREGA,'')+', Compl.'+ISNULL(FCFO.COMPLEMENTREGA,'')+', '+ ISNULL(DTIPOBAIRRO.DESCRICAO,'')+'  '+ ISNULL(FCFO.BAIRROENTREGA,'') +' , doravante denominada simplesmente de COMPRADOR(A), têm entre si como justo e contratado o seguinte:'AS TEXT) texto2,
CAST('CLÁUSULA PRIMEIRA - O VENDEDOR vende neste ato ao COMPRADOR, pelo preço certo e ajustado de ' + dbo.FC_FORMATA_MOEDA(TMOV.VALORLIQUIDOORIG,'.',',') + ' (' + dbo.FC_EXTENSO(TMOV.VALORLIQUIDOORIG) + '), a(s) máquina(s) e peça(s) constantes da descrição das mercadorias alienadas com reserva de domínio  nº '+tmov.numeromov+', que segue em anexo a este presente instrumento, e deste fica fazendo parte integrante.'AS TEXT) texto3,
CAST('CLÁUSULA SEGUNDA - Por conta do preço referido na cláusula primeira, o VENDEDOR recebe :'AS TEXT) texto4, 

DATENAME(WEEKDAY, tmov.dataemissao) + ', ' + CONVERT(VARCHAR(02),DATEPART(DAY,tmov.dataemissao)) + ' DE ' + DATENAME(MONTH,tmov.dataemissao) + ' DE ' + CONVERT(VARCHAR(04),DATEPART(YEAR,tmov.dataemissao)) +'.' AS EMISSAO
FROM TMOV, FCFO, DTIPORUA, DTIPOBAIRRO WHERE TMOV.CODCOLCFO = FCFO.CODCOLIGADA AND TMOV.CODCFO = FCFO.CODCFO AND FCFO.TIPORUAENTREGA = DTIPORUA.CODIGO AND FCFO.TIPOBAIRROENTREGA = DTIPOBAIRRO.CODIGO AND TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel12.Text = row["texto1"].ToString();
                xrLabel23.Text = row["texto2"].ToString();
                xrLabel24.Text = row["texto3"].ToString();
                xrLabel25.Text = row["texto4"].ToString();
                txtDATAEMISSAO.Text = row["emissao"].ToString();
            }
        }

        public void Detalhe1()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT 
  TMOV.IDMOV,
  CAST(
   CASE 
   WHEN TMOVCOMPL.TIPOPAGTO = 'UNICO'     THEN 'Como pagamento único, e vencido em '+ Convert( varchar, TMOVCOMPL.VENCUNICO,103) + ', a importância de ' + dbo.FC_FORMATA_MOEDA(TMOVCOMPL.VALORUNICO,'.',',') + ' (' + dbo.FC_EXTENSO(TMOVCOMPL.VALORUNICO) + ').'
   WHEN TMOVCOMPL.TIPOPAGTO = 'PARCELADO' THEN 'Como arras ou sinal de pagamento, e vencido em ' + Convert( VarChar, TMOVCOMPL.VENCPARC,103) +' a importância de ' + dbo.FC_FORMATA_MOEDA(TMOVCOMPL.VALORPARC,'.',',') + ' (' + dbo.FC_EXTENSO(TMOVCOMPL.VALORPARC) + '), que em caso de não cumprimento deste contrato, perderá o valor em favor da VENDEDORA.'+ CAST(TMOVCOMPL.NUMPARC AS VARCHAR(70))+' parcelas mensais de ' + dbo.FC_FORMATA_MOEDA(TMOVCOMPL.VALORNUMPARC,'.',',') + ' (' + dbo.FC_EXTENSO(TMOVCOMPL.VALORNUMPARC) + '), a serem pagas a cada '+ CAST(TMOVCOMPL.PERIODICIDADE AS VARCHAR(70))+' dias, vencida a primeira em '+ Convert( VarChar,TMOVCOMPL.PRIMVENCPARC,103) +' e as demais sucessivamente.'
   ELSE LTrim(RTrim(CAST(TMOVCOMPL.OUTROSPAGTO AS VARCHAR(500))))
   END
  AS TEXT) texto
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
                xrLabel13.Text = row["texto"].ToString();
            }
        }

        public void Detalhe2()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('CLÁUSULA TERCEIRA - Por força deste contrato de compra e venda, aqui expressamente instituído e aceito pelas partes, fica reservada à Vendedora, a propriedade do(s) objeto(s) descrito(s) na cláusula primeira (constante da referida descrição), até que sejam liquidados os valores aqui contratados.'AS TEXT) texto1, 
CAST('CLÁUSULA QUARTA - Fica o COMPRADOR ou terceira pessoa abaixo qualificada e desde que aceite expressamente, constituído no encargo de fiel depositário do bem, obrigando-se por sua guarda e conservação, e desde já advertido que caso não seja efetuado o pagamento de qualquer das prestações, ficará, desde logo, constituído em mora conforme parágrafo primeiro da cláusula sexta, e obrigado sob as penas da lei, a restituir incontinenti o(s) objeto(s) condicionalmente adquirido(s), nos termos do disposto no artigo 627 e seguintes do Código Civil.'AS TEXT) texto2
FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel14.Text = row["texto1"].ToString();
                xrLabel26.Text = row["texto2"].ToString();
            }
        }

        public void Detalhe3()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, CAST('Parágrafo Único - Recusado o encargo de fiel depositário pelo Compromissário Comprador, o Sr.(a): '+ ISNULL(TMOVCOMPL.NOME_COMP1,'') +',  '+ CASE WHEN ISNULL(TMOVCOMPL.PESSOAFISOUJUR1,'') = 'J' THEN 'CNPJ:' ELSE 'CPF:' END +' '+ ISNULL(TMOVCOMPL.CGCCFO_COMP1,'') +', '+ CASE WHEN ISNULL(TMOVCOMPL.PESSOAFISOUJUR1,'') = 'J' THEN 'I.E:' ELSE 'RG:' END +' '+ ISNULL(TMOVCOMPL.RGIE_COMP1,'') +', domiciliado/sediada em: '+ ISNULL(TMOVCOMPL.MUN_UF_COMP1,'') +' , à '+ ISNULL(TMOVCOMPL.ENDERECO_COMP1,'') +', voluntariamente assume tal compromisso, obrigando-se na forma do caput desta cláusula, e desde já ciente das conseqüências deste encargo.'AS TEXT) texto 
FROM TMOV, TMOVCOMPL WHERE TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA AND TMOV.IDMOV = TMOVCOMPL.IDMOV AND TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
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
CAST('CLÁUSULA QUINTA - Enquanto não houver pagamento integral do preço, obriga-se o COMPRADOR, bem como o fiel depositário (caso seja terceiro), a defender os equipamentos da turbação de terceiros e permitindo ao VENDEDOR a inspeção. 'AS TEXT) texto1,
CAST('CLÁUSULA SEXTA - Em caso de não pagamento das parcelas ora contratadas, o contrato fica rescindido de pleno direito, e o COMPRADOR arcará com todas as despesas de desmontagem e transporte das mercadorias alienadas com reserva de domínio, valendo o presente como título executivo extrajudicial, mediante comprovação de despesas efetuadas pela VENDEDORA, sem prejuízo da retenção das arras. 'AS TEXT) texto2,
CAST('Parágrafo Primeiro :- Em caso de mora, inadimplemento ou rescisão, o comprador se sujeitará ainda ao pagamento de multa de 2% (dois por cento) sobre o valor remanescente do contrato (quando rescindido) ou da parcela em atraso, e juros de 1% (Hum por cento) ao mês pro rata die e pagamento de honorários advocatícios de 5%, independente dos honorários de sucumbência eventualmente fixados.'AS TEXT) texto3,
CAST('Parágrafo Segundo :- Em caso de rescisão do presente contrato, vencerão antecipadamente todas as parcelas vincendas, reservando a VENDEDORA no direito de optar em propor ação judicial objetivando a busca e apreensão dos equipamentos objeto do presente contrato, ou promover a execução dos créditos nos termos do que dispõe o artigo 1070 e seguintes do CPC.'AS TEXT) texto4,
CAST('Parágrafo Terceiro :- A VENDEDORA, em caso de rescisão do presente contrato, poderá vender o bem diretamente a terceira pessoa, cujo valor será abatido na dívida, e o saldo remanescente, positivo ou negativo, será devolvido ou cobrado do COMPRADOR. Neste caso, fica assegurado ao COMPRADOR o direito de preferência sobre o bem, desde que efetue o pagamento do saldo devedor.'AS TEXT) texto5
FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel16.Text = row["texto1"].ToString();
                xrLabel27.Text = row["texto2"].ToString();
                xrLabel28.Text = row["texto3"].ToString();
                xrLabel29.Text = row["texto4"].ToString();
                xrLabel30.Text = row["texto5"].ToString();
            }
        }

        public void Detalhe5()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('Parágrafo Quarto :- Na hipótese do parágrafo terceiro, a VENDEDORA cientificará o Comprador, informando o preço da venda bem como o valor do débito para exercício do direito de preferência.'AS TEXT) texto1,
CAST('CLÁUSULA SÉTIMA - Incluem-se no preço deste contrato, sendo de responsabilidade da VENDEDORA os itens marcados com a palavra SIM no início da frase :'AS TEXT) texto2
FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel18.Text = row["texto1"].ToString();
                xrLabel31.Text = row["texto2"].ToString();
            }
        }

        public void Detalhe6()
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
                xrLabel19.Text = row["texto1"].ToString();
                xrLabel32.Text = row["texto2"].ToString();
                xrLabel33.Text = row["texto3"].ToString();
                xrLabel34.Text = row["texto4"].ToString();
                xrLabel35.Text = row["texto5"].ToString();
                xrLabel36.Text = row["texto6"].ToString();
                xrLabel37.Text = row["texto7"].ToString();
                xrLabel38.Text = row["texto8"].ToString();
                xrLabel39.Text = row["texto9"].ToString();
                xrLabel40.Text = row["texto10"].ToString();
                xrLabel41.Text = row["texto11"].ToString();
            }
        }

        public void Detalhe7()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('Parágrafo Único :- Caso queira, e seja expressamente autorizado pelo Comprador, a VENDEDORA poderá assumir despesas complementares não inclusas no preço, reservando-se no direito de cobrar os valores efetivamente despendidos. 'AS TEXT) texto1, 
CAST('CLÁUSULA OITAVA - A VENDEDORA garante os produtos alienados, bem como seu perfeito funcionamento pelo período de 18 meses contados da data de seu faturamento, ou 12 meses contados da data de sua instalação, valendo o prazo que primeiro expirar, desde que o COMPRADOR :'AS TEXT) texto2, 
CAST('I - Contrate o serviço de montagem autorizado pela VENDEDORA.'AS TEXT) texto3,
CAST('II - Utilize e proceda a manutenção no equipamento de acordo com as recomendações técnicas constantes do manual de instruções.'AS TEXT) texto4, 
CAST('Parágrafo Primeiro :- A garantia contra defeitos decorrentes do uso normal do equipamento será prestada na oficina da VENDEDORA, localizada em Esp. Santo do Pinhal - SP, cabendo ao COMPRADOR as despesas com desmontagem, transporte e remontagem do equipamento.'AS TEXT) texto5 
FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel20.Text = row["texto1"].ToString();
                xrLabel42.Text = row["texto2"].ToString();
                xrLabel43.Text = row["texto3"].ToString();
                xrLabel44.Text = row["texto4"].ToString();
                xrLabel45.Text = row["texto5"].ToString();
            }
        }

        public void Detalhe8()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, 
CAST('Parágrafo Segundo :- A garantia de peças eventualmente substituídas encerra-se com a garantia das peças originais.'AS TEXT) texto1, 
CAST('Parágrafo Terceiro :- A garantia de que trata esta cláusula, não engloba danos ao equipamento causados por acidentes, desgaste decorrente de uso impróprio, instalação inadequada , manobra e usos indevidos, armazenagem inadequada, montagem executada fora dos padrões pré - estabelecidos ou pessoas não autorizadas pela VENDEDORA, transporte negligente, ou em caso de o COMPRADOR haver empreendido reparos por conta própria, ou por empresas não autorizadas.'AS TEXT) texto2,
CAST('Parágrafo Quarto :- Em se tratando de acessórios e / ou equipamentos que não sejam de fabricação própria, a garantia dada pela VENDEDORA restringir - se -á às normas de garantia estipuladas pelos fabricantes de cada acessório, inclusive quanto ao prazo de vigência.'AS TEXT) texto3,
CAST('CLÁUSULA NONA - O uso inadequado do equipamento e a ocorrência de qualquer espécie de dano oriunda deste evento, não obrigarão a VENDEDORA a qualquer tipo de indenização, já que todas as engrenagens e transmissões, bem como partes que tenham potencialidade de risco, contam com as respectivas proteções.'AS TEXT) texto4,
CAST('CLÁUSULA DÉCIMA - As partes elegem esta comarca de Espírito Santo do Pinhal/SP, para dirimir qualquer controvérsia advinda do presente contrato, renunciando a qualquer outro por mais privilegiado que seja.'AS TEXT) texto5
FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel21.Text = row["texto1"].ToString();
                xrLabel46.Text = row["texto2"].ToString();
                xrLabel47.Text = row["texto3"].ToString();
                xrLabel48.Text = row["texto4"].ToString();
                xrLabel49.Text = row["texto5"].ToString();
            }
        }

        public void Detalhe9()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"
SELECT TMOV.IDMOV, CAST('E assim, por encontrarem-se justos e contratados, lavraram o presente em duas vias de igual teor e forma, assinado em todas as folhas, juntamente com a discriminação de mercadorias alienadas com reserva de domínio.
'AS TEXT) texto FROM TMOV WHERE TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                xrLabel22.Text = row["texto"].ToString();
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

        }

        private void Detail11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Cabecalho();
        }
    }
}
