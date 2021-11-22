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
    public partial class FormDashboardViewer : Form
    {
        public String Conexao { get; set; }
        public String ArquivoXML { get; set; }

        public FormDashboardViewer()
        {
            InitializeComponent();
        }

        private void FormDashboardView_Load(object sender, EventArgs e)
        {
            dashboardViewer1.LoadDashboard(ArquivoXML);
        }

        private void dashboardViewer1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get(Conexao);

            if (conn.Database == Global.Types.Database.SqlClient)
            {
                System.Data.SqlClient.SqlConnectionStringBuilder builder = AppLib.Data.AssistantDecode.SqlClient(conn.ConnectionString);

                DevExpress.DataAccess.ConnectionParameters.SqlServerConnectionParametersBase parameters = e.ConnectionParameters as DevExpress.DataAccess.ConnectionParameters.SqlServerConnectionParametersBase;

                if (parameters != null)
                {
                    parameters.ServerName = builder.DataSource;
                    parameters.DatabaseName = builder.InitialCatalog;
                    parameters.UserName = builder.UserID;
                    parameters.Password = builder.Password;
                }
            }

            if (conn.Database == Global.Types.Database.OracleClient)
            {
                System.Data.OracleClient.OracleConnectionStringBuilder builder = AppLib.Data.AssistantDecode.OracleClient(conn.ConnectionString);

                DevExpress.DataAccess.ConnectionParameters.OracleConnectionParameters parameters = e.ConnectionParameters as DevExpress.DataAccess.ConnectionParameters.OracleConnectionParameters;

                if (parameters != null)
                {
                    parameters.ServerName = builder.DataSource;
                    parameters.UserName = builder.UserID;
                    parameters.Password = builder.Password;
                }
            }
        }
    }
}
