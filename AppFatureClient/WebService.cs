using System;
using System.Collections.Generic;
using System.Text;

namespace AppFatureClient
{
    public class WebService
    {
        public WebService()
        { 
        
        }

        /*
        public wsLib.AppInteropServicesIClient GetWebService(String connectionString)
        {
            String[] temp = connectionString.Split(';');

            wsLib.AppInteropServicesIClient wsClient = new wsLib.AppInteropServicesIClient("BasicHttpBinding_AppInteropServicesI", temp[2]);

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

            return wsClient;
        }
        */

        public wsLib.Service1SoapClient GetWebService(String connectionString)
        {
            String[] temp = connectionString.Split(';');

            wsLib.Service1SoapClient wsClient = new wsLib.Service1SoapClient("Service1Soap", temp[2]);

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

            return wsClient;
        }
    }
}
