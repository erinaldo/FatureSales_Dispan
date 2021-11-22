using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class RelNovoContrato : DevExpress.XtraReports.UI.XtraReport
    {
        string dir = Convert.ToString(AppDomain.CurrentDomain.BaseDirectory);
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }

        public RelNovoContrato()
        {
            InitializeComponent();
            
        }

        public void Cabecalho()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT
TMOVCOMPL.MONTAGEM,
 
CASE
WHEN TMOVCOMPL.MONTAGEM = 'NÃO' THEN ''
WHEN TMOVCOMPL.MONTAGEM = 'SIM' THEN ' E MONTAGEM '
ELSE ''
END TEXTO1,

CONVERT(VARCHAR,TMOV.PRAZOENTREGA) + ' (' + dbo.FC_EXTENSO(TMOV.PRAZOENTREGA) + ' ) dias' TEXTO2,

CASE
WHEN TMOVCOMPL.MONTAGEM = 'NÃO' THEN ''
WHEN TMOVCOMPL.MONTAGEM = 'SIM' THEN 'A montagem dos equipamentos, bem como sua adaptação aos equipamentos já existentes na propriedade, correrão por conta e risco do COMPRADOR, não constituindo nem se confundindo com o objeto deste contrato que é de compra e venda pura.'
ELSE ''
END TEXTO3,

CASE
WHEN TMOVCOMPL.FINANCIADO = 'SIM' THEN '3.1.3. Por conta da opção de financiamento firmada pelo comprador, a operação fiscal de venda que o presente contrato gerar, será enquadrada, para efeitos fiscais, como remessa para demonstração, e com a aprovação do financiamento ou o pagamento integral do preço, será emitida a Nota Fiscal de Transmissão da Propriedade. O valor pago à VENDEDORA que eventualmente exceder ao preço, será por ela restituído ao COMPRADOR no prazo máximo de 3 dias úteis, a contar da data da efetivação do crédito. De igual modo, sendo o crédito efetuado na conta corrente do COMPRADOR, este deverá repassar o valor correspondente ao saldo da venda à VENDEDORA, e esta, só após o recebimento integral de seu crédito, entregará a nota fiscal de transmissão de propriedade.'
ELSE ''
END TEXTO4,

CASE
WHEN TMOV.FRETECIFOUFOB = 1 THEN 'Responsabilizar-se pelo objeto do fornecimento, compreendendo a fabricação e a entrega na propriedade do COMPRADOR;'
WHEN TMOV.FRETECIFOUFOB = 2 THEN 'Responsabilizar-se pelo objeto do fornecimento, compreendendo a fabricação e disponibilização para retirada em sua fábrica;'
ELSE ''
END TEXTO5,

CASE
WHEN TMOVCOMPL.MONTAGEM = 'NÃO' THEN ''
WHEN TMOVCOMPL.MONTAGEM = 'SIM' THEN 'A VENDEDORA responsabilizar-se-á integralmente, por si, seus empregados, prepostos e subcontratados, perante o comprador e terceiros afetados, pelas perdas e danos a que der ensejo, seja por ação ou omissão e por todas as perdas e danos resultantes de seu inadimplemento contratual, nos termos do parágrafo único do artigo 416 do Código Civil.'
ELSE ''
END TEXTO6,

CASE
WHEN TMOV.FRETECIFOUFOB = 1 THEN ''
WHEN TMOV.FRETECIFOUFOB = 2 THEN 'Retirar e Transportar todos os equipamentos objeto deste Contrato, no prazo máximo e improrrogável de sete dias, a contar da ciência da disponibilidade para carregamento, sob pena de lhe ser cobrado serviço de armazenamento, no valor diário de R$ 150,00. A violação desse prazo dará o direito à VENDEDORA de contratar o transporte e entregar o equipamento na propriedade do COMPRADOR, cujas despesas serão cobradas deste mediante prestação de contas;'
ELSE ''
END TEXTO7,

CASE
WHEN TMOVCOMPL.MONTAGEM = 'NÃO' THEN ''
WHEN TMOVCOMPL.MONTAGEM = 'SIM' THEN 'Ocorrendo atraso na entrega das obras civis, o período respectivo será acrescido ao prazo de montagem.'
ELSE ''
END TEXTO8,

CASE 
WHEN TMOVCOMPL.TIPOPAGTO = 'UNICO' THEN CAST('Como pagamento único, e vencido em '+ Convert( VarChar(10),TMOVCOMPL.VENCUNICO,103) +', a importância de ' + dbo.FC_FORMATA_MOEDA(TMOVCOMPL.VALORUNICO,'.',',') + ' (' + dbo.FC_EXTENSO(TMOVCOMPL.VALORUNICO) + ').' AS TEXT)
WHEN TMOVCOMPL.TIPOPAGTO = 'PARCELADO' THEN CAST('Como arras ou sinal de pagamento, e vencido em '+ Convert(varchar(10), TMOVCOMPL.VENCPARC,103) +' a importância de ' + dbo.FC_FORMATA_MOEDA(TMOVCOMPL.VALORPARC,'.',',') + ' (' + dbo.FC_EXTENSO(TMOVCOMPL.VALORPARC) + '), que em caso de não cumprimento deste contrato, perderá o valor em favor da VENDEDORA. '+ CAST(TMOVCOMPL.NUMPARC AS VARCHAR(70))+' parcelas mensais de ' + dbo.FC_FORMATA_MOEDA(TMOVCOMPL.VALORNUMPARC,'.',',') + ' (' + dbo.FC_EXTENSO(TMOVCOMPL.VALORNUMPARC) + '), a serem pagas a cada '+ CAST(TMOVCOMPL.PERIODICIDADE AS VARCHAR(70))+' dias, vencida a primeira em '+ Convert( VarChar(10),TMOVCOMPL.PRIMVENCPARC,103) +' e as demais sucessivamente.'AS TEXT)
ELSE CAST(''+ CAST(TMOVCOMPL.OUTROSPAGTO AS VARCHAR(500))+''AS TEXT)
END TEXTO9,

CASE
WHEN TMOVCOMPL.MONTAGEM = 'NÃO' THEN 
	CASE
	WHEN TMOV.FRETECIFOUFOB = 1 THEN 'A VENDEDORA se obriga a disponibilizar os equipamentos no prazo de ' + CONVERT(VARCHAR,TMOV.PRAZOENTREGA) + ' (' + REPLACE(REPLACE(dbo.FC_EXTENSO(TMOV.PRAZOENTREGA),'Real',''),'Reais','') + ' ) dias contados da data de depósito do sinal. A entrega será realizada no prazo de 07 dias, pela VENDEDORA, a contar da conclusão da fabricação, ou do término do seu prazo, no endereço do COMPRADOR. A montagem dos equipamentos, bem como sua adaptação aos equipamentos já existentes na propriedade, correrão por conta e risco do COMPRADOR, não constituindo nem se confundindo com o objeto deste contrato que é de compra e venda pura.'
	WHEN TMOV.FRETECIFOUFOB = 2 THEN 'A VENDEDORA se obriga a disponibilizar os equipamentos no prazo de ' + CONVERT(VARCHAR,TMOV.PRAZOENTREGA) + ' (' + REPLACE(REPLACE(dbo.FC_EXTENSO(TMOV.PRAZOENTREGA),'Real',''),'Reais','') + ' ) dias contados da data de depósito do sinal, disponibilizando-os para retirada em sua fábrica. A retirada será procedida no prazo de 07 dias pelo COMPRADOR, a contar da comunicação. A montagem dos equipamentos, bem como sua adaptação aos equipamentos já existentes na propriedade, correrão por conta e risco do COMPRADOR, não constituindo nem se confundindo com o objeto deste contrato que é de compra e venda pura.'
	ELSE ''
	END
WHEN TMOVCOMPL.MONTAGEM = 'SIM' THEN 
	CASE
	WHEN TMOV.FRETECIFOUFOB = 1 THEN 'A VENDEDORA se obriga a disponibilizar os equipamentos no prazo de ' + CONVERT(VARCHAR,TMOV.PRAZOENTREGA) + ' (' + REPLACE(REPLACE(dbo.FC_EXTENSO(TMOV.PRAZOENTREGA),'Real',''),'Reais','') + ' ) dias contados da data de depósito do sinal. A entrega será realizada no prazo de 07 dias, pela VENDEDORA, a contar da conclusão da fabricação, ou do término do seu prazo, no endereço do COMPRADOR. A montagem dos equipamentos será realizada no prazo de ' + CONVERT(varchar,TMOVCOMPL.PRAZOMONT) + ' ( ' + REPLACE(dbo.FC_EXTENSO(CONVERT(int,TMOVCOMPL.PRAZOMONT)),'Reais','') + ' ) dias a contar da data de disponibilização, pelo COMPRADOR, dos equipamentos e das obras civis na propriedade.'
	WHEN TMOV.FRETECIFOUFOB = 2 THEN 'A VENDEDORA se obriga a disponibilizar os equipamentos no prazo de ' + CONVERT(VARCHAR,TMOV.PRAZOENTREGA) + ' (' + REPLACE(REPLACE(dbo.FC_EXTENSO(TMOV.PRAZOENTREGA),'Real',''),'Reais','') + ' ) dias contados da data de depósito do sinal, disponibilizando-os para retirada em sua fábrica. A retirada será procedida no prazo de 07 dias pelo COMPRADOR, a contar da comunicação. A montagem dos equipamentos será realizada no prazo de ' + CONVERT(varchar,TMOVCOMPL.PRAZOMONT) + ' ( ' + REPLACE(dbo.FC_EXTENSO(CONVERT(int,TMOVCOMPL.PRAZOMONT)),'Reais','') + ' ) dias a contar da data de disponibilização, pelo COMPRADOR, dos equipamentos e das obras civis na propriedade.'
	ELSE ''
	END
ELSE ''
END TEXTO10,

CASE
WHEN TMOVCOMPL.MONTAGEM = 'NÃO' THEN ''
WHEN TMOVCOMPL.MONTAGEM = 'SIM' THEN ' e pelo serviço de montagem contratado'
ELSE ''
END TEXTO11,

CASE
WHEN TMOVCOMPL.MONTAGEM = 'NÃO' THEN 
	CASE
	WHEN TMOV.FRETECIFOUFOB = 1 THEN 'Responsabiliza-se pelo objeto do fornecimento, compreendendo a fabricação e a entrega do equipamento na propriedade do COMPRADOR.'
	WHEN TMOV.FRETECIFOUFOB = 2 THEN 'Responsabiliza-se pelo objeto do fornecimento, compreendendo a fabricação e disponibilização para retirada em sua fábrica.'
	ELSE ''
	END
WHEN TMOVCOMPL.MONTAGEM = 'SIM' THEN 
	CASE
	WHEN TMOV.FRETECIFOUFOB = 1 THEN 'Responsabiliza-se pelo objeto do fornecimento, compreendendo a fabricação, entrega e montagem dos equipamentos na propriedade do COMPRADOR.'
	WHEN TMOV.FRETECIFOUFOB = 2 THEN 'Responsabiliza-se pelo objeto do fornecimento, compreendendo a fabricação, disponibilização para retirada pelo COMPRADOR em sua fábrica, e montagem dos equipamentos na propriedade.'
	ELSE ''
	END
ELSE ''
END TEXTO12,

CASE
WHEN TMOV.FRETECIFOUFOB = 1 THEN ''
WHEN TMOV.FRETECIFOUFOB = 2 THEN '·	'
ELSE ''
END TEXTO13,

CASE
WHEN TMOV.FRETECIFOUFOB = 1 THEN ''
WHEN TMOV.FRETECIFOUFOB = 2 THEN 'Retirada e Transporte dos Equipamentos.'
ELSE ''
END TEXTO14,

dbo.FC_FORMATA_MOEDA(TMOV.VALORBRUTOORIG,'.',',') + ' (' + dbo.FC_EXTENSO(TMOV.VALORBRUTOORIG) + ')' VALORBRUTOORIG,
REPLACE(dbo.FC_FORMATA_MOEDA(TMOV.PERCENTUALDESC,'.',','),'R$','') PERCENTUALDESC,
dbo.FC_FORMATA_MOEDA(TMOV.VALORLIQUIDOORIG,'.',',') + ' (' + dbo.FC_EXTENSO(TMOV.VALORLIQUIDOORIG) + ')' VALORLIQUIDOORIG,

TMOVCOMPL.RESP03 DESPESAS,

TMOV.NUMEROMOV,
FCFO.NOME,
CASE
WHEN FCFO.PESSOAFISOUJUR = 'F' THEN 'CPF ' + CGCCFO
WHEN FCFO.PESSOAFISOUJUR = 'J' THEN 'CNPJ ' + CGCCFO
ELSE ''
END CGCCFO,
ISNULL(DTIPORUA.DESCRICAO,'') + ' ' +
ISNULL(FCFO.RUA,'') + 
CASE WHEN FCFO.NUMERO IS NULL THEN '' ELSE ', ' + FCFO.NUMERO END + ' ' +
CASE WHEN FCFO.COMPLEMENTO IS NULL THEN '' ELSE ', ' + FCFO.COMPLEMENTO END + ' ' +
ISNULL(DTIPOBAIRRO.DESCRICAO,'') + ' ' +
ISNULL(FCFO.BAIRRO,'') + ', em ' +
ISNULL(FCFO.CIDADE,'') + ', ' +
ISNULL(FCFO.CODETD,'') + ', ' +
ISNULL(FCFO.CEP,'') ENDERECO,

RIGHT(REPLICATE('0',2)+CAST(DAY(DATAEMISSAO) AS VARCHAR(2)),2) AS DIA,
CASE
WHEN MONTH(DATAEMISSAO) = 1 THEN 'Janeiro'
WHEN MONTH(DATAEMISSAO) = 2 THEN 'Fevereiro'
WHEN MONTH(DATAEMISSAO) = 3 THEN 'Março'
WHEN MONTH(DATAEMISSAO) = 4 THEN 'Abril'
WHEN MONTH(DATAEMISSAO) = 5 THEN 'Maio'
WHEN MONTH(DATAEMISSAO) = 6 THEN 'Junho'
WHEN MONTH(DATAEMISSAO) = 7 THEN 'Julho'
WHEN MONTH(DATAEMISSAO) = 8 THEN 'Agosto'
WHEN MONTH(DATAEMISSAO) = 9 THEN 'Setembro'
WHEN MONTH(DATAEMISSAO) = 10 THEN 'Outubro'
WHEN MONTH(DATAEMISSAO) = 11 THEN 'Novembro'
WHEN MONTH(DATAEMISSAO) = 12 THEN 'Dezembro'
ELSE ''
END MES,
YEAR(DATAEMISSAO) ANO

FROM TMOV
LEFT JOIN TMOVCOMPL ON TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA AND TMOV.IDMOV = TMOVCOMPL.IDMOV,
FCFO
LEFT JOIN DTIPORUA ON FCFO.TIPORUA = DTIPORUA.CODIGO
LEFT JOIN DTIPOBAIRRO ON FCFO.TIPOBAIRRO = DTIPOBAIRRO.CODIGO

WHERE TMOV.CODCOLIGADA = ? AND TMOV.IDMOV = ?
AND TMOV.CODCOLCFO = FCFO.CODCOLIGADA
AND TMOV.CODCFO = FCFO.CODCFO
";
            
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { CodColigada, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DataSource = dt;

            foreach (System.Data.DataRow row in dt.Rows)
            {
                if (row["MONTAGEM"].ToString() == "SIM")
                {
                    this.DetailReport.Visible = true;
                    this.Detail1.Visible = true;
                }
                else
                {
                    this.DetailReport.Visible = false;
                    this.Detail1.Visible = false;
                }


                System.IO.MemoryStream data = new System.IO.MemoryStream();
                System.IO.Stream str = System.IO.File.OpenRead(string.Concat(dir, "template_contrato.htm"));

                

                str.CopyTo(data);
                byte[] buf = new byte[data.Length];
                data.Read(buf, 0, buf.Length);

                string text;
                data.Position = 0;
                using (System.IO.TextReader reader = new System.IO.StreamReader(data, System.Text.Encoding.Default))
                {
                    text = reader.ReadToEnd();
                }

                System.Windows.Forms.WebBrowser document = new System.Windows.Forms.WebBrowser();

                text = text.Replace("[NOME1]", row["NOME"].ToString());
                text = text.Replace("[CGCCFO1]", row["CGCCFO"].ToString());
                text = text.Replace("[ENDERECO1]", row["ENDERECO"].ToString());
                text = text.Replace("[NUMEROMOV1]", row["NUMEROMOV"].ToString());
                text = text.Replace("[TEXTO101]", row["TEXTO10"].ToString());
                text = text.Replace("[TEXTO112]", row["TEXTO11"].ToString());
                text = text.Replace("[VALORBRUTOORIG1]", row["VALORBRUTOORIG"].ToString());
                text = text.Replace("[PERCENTUALDESC1]", row["PERCENTUALDESC"].ToString());
                text = text.Replace("[VALORLIQUIDOORIG1]", row["VALORLIQUIDOORIG"].ToString());
                text = text.Replace("[TEXTO91]", row["TEXTO9"].ToString());
                text = text.Replace("[TEXTO41]", row["TEXTO4"].ToString());
                text = text.Replace("[TEXTO122]", row["TEXTO12"].ToString());
                text = text.Replace("[TEXTO61]", row["TEXTO6"].ToString());
                text = text.Replace("[TEXTO131]", row["TEXTO13"].ToString());
                text = text.Replace("[TEXTO141]", row["TEXTO14"].ToString());
                text = text.Replace("[TEXTO71]", row["TEXTO7"].ToString());
                text = text.Replace("[TEXTO81]", row["TEXTO8"].ToString());
                text = text.Replace("[TEXTO11]", row["TEXTO1"].ToString());
                text = text.Replace("[DIAEMI]", row["DIA"].ToString());
                text = text.Replace("[MESEMI]", row["MES"].ToString());
                text = text.Replace("[ANOEMI]", row["ANO"].ToString());



                xrRichText1.Html = text;








                data = new System.IO.MemoryStream();

                if (row["DESPESAS"].ToString() == "NÃO")
                {
                    str = System.IO.File.OpenRead(string.Concat(dir, "template_anexo_despesa.htm"));
                }
                else
                {
                    str = System.IO.File.OpenRead(string.Concat(dir, "template_anexo.htm"));                
                }

                str.CopyTo(data);
                buf = new byte[data.Length];
                data.Read(buf, 0, buf.Length);

                data.Position = 0;
                using (System.IO.TextReader reader = new System.IO.StreamReader(data, System.Text.Encoding.Default))
                {
                    text = reader.ReadToEnd();
                }

                document = new System.Windows.Forms.WebBrowser();
                text = text.Replace("[NUMEROMOV1]", row["NUMEROMOV"].ToString());
                text = text.Replace("[DIAEMI]", row["DIA"].ToString());
                text = text.Replace("[MESEMI]", row["MES"].ToString());
                text = text.Replace("[ANOEMI]", row["ANO"].ToString());
                xrRichText2.Html = text;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Cabecalho();
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
    }
}
