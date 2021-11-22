using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Data
{
    public class WebService
    {
        #region ATRIBUTOS

        //private wsLib.AppInteropServicesIClient wsClient { get; set; }
        private wsLib.Service1SoapClient wsClient { get; set; }
        private String User { get; set; }
        private String Password { get; set; }
        private String Address { get; set; }

        public int? Increment { get; set; }
        
        #endregion

        #region CONSTRUTORES

        public WebService() { }

        /*
        public WebService(String connectionString)
        {
            String[] temp = connectionString.Split(';');
            User = temp[0];
            Password = temp[1];
            Address = temp[2];

            wsClient = new wsLib.AppInteropServicesIClient("BasicHttpBinding_AppInteropServicesI", Address);

            System.ServiceModel.BasicHttpBinding binding = (System.ServiceModel.BasicHttpBinding)wsClient.Endpoint.Binding;
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxBufferPoolSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;

            binding.ReaderQuotas.MaxDepth = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;

            binding.OpenTimeout = new TimeSpan(0, 5, 0);
            binding.SendTimeout = new TimeSpan(0, 5, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
            binding.CloseTimeout = new TimeSpan(0, 5, 0);

        }
        */

        public WebService(String connectionString)
        {
            String[] temp = connectionString.Split(';');
            User = temp[0];
            Password = temp[1];
            Address = temp[2];

            wsClient = new wsLib.Service1SoapClient("Service1Soap", Address);

            System.ServiceModel.BasicHttpBinding binding = (System.ServiceModel.BasicHttpBinding)wsClient.Endpoint.Binding;
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxBufferPoolSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;

            binding.ReaderQuotas.MaxDepth = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;

            binding.OpenTimeout = new TimeSpan(0, 5, 0);
            binding.SendTimeout = new TimeSpan(0, 5, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
            binding.CloseTimeout = new TimeSpan(0, 5, 0);
        }

        #endregion

        #region MÉTODOS

        #region CONTROLE DE TRANSAÇÃO

        private Boolean Open()
        {
            return true;
        }

        private Boolean Close()
        {
            return true;
        }

        public Boolean BeginTransaction()
        {
            return true;
        }

        public Boolean Commit()
        {
            return true;
        }

        public Boolean Rollback()
        {
            return true;
        }
        
        #endregion

        #region MANIPULAÇÃO DE DADOS

        public System.Data.DataTable ExecQuery(String command)
        {
            try
            {
                wsLib.MessageGet msg = wsClient.Get(User, Password, command);

                if (msg.Mensagem.Equals(""))
                {
                    return msg.Retorno.Tables[0];
                }
                else
                {
                    throw new Exception(msg.Mensagem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ExecTransaction(String command)
        {
            try
            {
                wsLib.Message msg = wsClient.Set(User, Password, command);

                if (msg.Mensagem.Equals(""))
                {
                    return (int)msg.Retorno;
                }
                else
                {
                    throw new Exception(msg.Mensagem);
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
            
        }

        public int ExecProcedure(String command, String[] parameters, params Object[] values)
        {
            throw new Exception("implementar");
        }

        public System.Data.DataTable GetSchemaTable(String command)
        {
            try
            {
                wsLib.MessageGet msg = wsClient.Get2(User, Password, command);

                if (msg.Mensagem.Equals(""))
                {
                    return msg.Retorno.Tables[0];
                }
                else
                {
                    throw new Exception(msg.Mensagem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
        
        #endregion

        #endregion

    }
}
