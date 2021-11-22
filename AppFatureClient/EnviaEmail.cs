using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFatureClient.Classes;

namespace AppFatureClient
{
    public partial class EnviaEmail : Form
    {

        

        public EnviaEmail(String _CODCFO, String _Assunto)
        {
            InitializeComponent();
            CODCFO = _CODCFO;
            Assunto = _Assunto;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            
        }

        private void EnviaEmail_Load(object sender, EventArgs e)
        {
            try
            {
                sql = String.Format(@"select EMAIL from FCFO
                                        where CODCFO = '{0}'", CODCFO);

                txtEmail.Set(MetodosSQL.GetField(sql, "EMAIL"));

                txtAssunto.Set(Assunto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
