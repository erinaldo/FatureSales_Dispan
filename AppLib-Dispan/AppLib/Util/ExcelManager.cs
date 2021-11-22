using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.Interop.Excel;

namespace AppLib.Util
{
    public class ExcelManager
    {
        public String arquivo { get; set; }
        private Boolean aberto { get; set; }

        private Microsoft.Office.Interop.Excel.Application excel { get; set; }
        private Microsoft.Office.Interop.Excel.Workbook workbook { get; set; }
        private Microsoft.Office.Interop.Excel.Worksheet worksheet { get; set; }
        private Microsoft.Office.Interop.Excel.Range range { get; set; }

        public ExcelManager(String _arquivo)
        {
            arquivo = _arquivo;
            excel = new Microsoft.Office.Interop.Excel.Application();
        }

        public Microsoft.Office.Interop.Excel.Workbook Abrir()
        {
            workbook = excel.Workbooks.Open(arquivo, Type.Missing, Type.Missing,
                                                       Type.Missing, Type.Missing,
                                                       Type.Missing, Type.Missing,
                                                       Type.Missing, Type.Missing,
                                                       Type.Missing, Type.Missing,
                                                       Type.Missing, Type.Missing,
                                                       Type.Missing, Type.Missing);

            aberto = true;
            return workbook;
        }

        public void Fechar(Boolean salvar)
        {
            if (aberto)
            {
                workbook.Close(salvar);
            }
        }

        public List<String> GetAbas()
        {
            List<String> abas = new List<String>();

            foreach (Worksheet worksheet in workbook.Worksheets)
            {
                abas.Add(worksheet.Name);
            }

            return abas;
        }

        public Microsoft.Office.Interop.Excel.Worksheet GetAba(String aba)
        {
            List<String> abas = new List<String>();

            foreach (Worksheet worksheetCurrent in workbook.Worksheets)
            {
                if (aba.ToUpper().Equals(worksheetCurrent.Name.ToUpper()))
                {
                    worksheet = worksheetCurrent;
                    return worksheet;
                }
            }

            return null;
        }

        public Microsoft.Office.Interop.Excel.Worksheet GetAbaAtiva()
        {
            worksheet = workbook.ActiveSheet;
            return worksheet;
        }

        public Object GetValor(String coluna, int linha)
        {
            Object valor = worksheet.Cells[linha, coluna].Value;
            return valor;
        }

        public Object GetValor(Object valorSeNulo, String coluna, int linha)
        {
            Object result = this.GetValor(coluna, linha);

            if (result == null)
            {
                return valorSeNulo;
            }
            else
            {
                return result;
            }
        }

        public Object GetValor(Object valorSeNulo, String aba, String coluna, int linha)
        {
            this.GetAba(aba);
            return this.GetValor(valorSeNulo, coluna, linha);
        }

        public void SetValor(int coluna, int linha, Object valor)
        {
            worksheet.Cells[linha, coluna] = valor;
        }

        public void SetValor(String coluna, int linha, Object valor)
        {
            worksheet.Cells[linha, coluna] = valor;
        }

        public void SetValor(String aba, String coluna, int linha, Object valor)
        {
            this.GetAba(aba);
            this.SetValor(coluna, linha, valor);
        }

        public void SetDados(String aba, String coluna, int linha, System.Data.DataTable dt)
        {
            this.GetAba(aba);

            range = worksheet.Cells[linha, coluna];

            int colunaCorrente = range.Column;
            int linhaCorrente = linha;

            for (int iLinha = 0; iLinha < dt.Rows.Count; iLinha++)
            {
                for (int iColuna = 0; iColuna < dt.Columns.Count; iColuna++)
                {
                    this.SetValor(colunaCorrente, linhaCorrente, dt.Rows[iLinha][iColuna]);
                    colunaCorrente++;
                }

                colunaCorrente = range.Column;
                linhaCorrente++;
            }
        }

        public void SetDados2(String aba, String coluna, int linha, System.Data.DataTable dt)
        {
            this.GetAba(aba);

            Microsoft.Office.Interop.Excel.Range pontoA = worksheet.Cells[linha, coluna];

            int linhaFinal = pontoA.Row + dt.Rows.Count - 1;
            int colunaFinal = pontoA.Column + dt.Columns.Count - 1;
            Microsoft.Office.Interop.Excel.Range pontoB = worksheet.Cells[linhaFinal, colunaFinal];

            range = worksheet.Range[pontoA, pontoB];

            range.Value2 = Util.Conversor.DataTableToArray(dt);
        }

    }
}
