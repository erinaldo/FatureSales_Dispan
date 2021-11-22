using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Padrao
{
    public partial class FormExcelSQLCadastro : AppLib.Windows.FormCadastroData
    {
        public FormExcelSQLCadastro()
        {
            InitializeComponent();
        }

        private void FormExcelSQLCadastro_Load(object sender, EventArgs e)
        {

        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new Object[] { campoInteiroIDPLANILHASQL.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            FormExcelSQLParCadastro f = new FormExcelSQLParCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHASQL", campoInteiroIDPLANILHASQL.Get()));
            f.Novo();
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormExcelSQLParCadastro f = new FormExcelSQLParCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHASQL", campoInteiroIDPLANILHASQL.Get()));
            f.Editar(gridData1);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            FormExcelSQLParCadastro f = new FormExcelSQLParCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHASQL", campoInteiroIDPLANILHASQL.Get()));
            f.Excluir(gridData1);
        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            gridData2.Parametros = new Object[] { campoInteiroIDPLANILHASQL.Get() };
        }

        private void gridData2_Novo(object sender, EventArgs e)
        {
            FormExcelSQLProcCadastro f = new FormExcelSQLProcCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHASQL", campoInteiroIDPLANILHASQL.Get()));
            f.Novo();
        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            FormExcelSQLProcCadastro f = new FormExcelSQLProcCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHASQL", campoInteiroIDPLANILHASQL.Get()));
            f.Editar(gridData2);
        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {
            FormExcelSQLProcCadastro f = new FormExcelSQLProcCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHASQL", campoInteiroIDPLANILHASQL.Get()));
            f.Excluir(gridData2);
        }
    }
}
