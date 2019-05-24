using NotificationServiceAPI.Models;
using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace NotificationServiceAPI.Extensions
{
    public class CustomHeaderInspector : IParameterInspector
    {
        private readonly string ServiceToken = ConfigurationManager.AppSettings["serviceToken"];

        public object BeforeCall(string operationName, object[] inputs)
        {
            switch (operationName)
            {
                case "Email":
                    Email(inputs);
                    break;

                case "SMS":
                    SMS(inputs);
                    break;
            }

            return null;
        }

        private void Email(object[] inputs)
        {
            try
            {
                var req = (SendEmailRequest)inputs[0];
                var header = req.ServiceToken;
                if (header != null)
                {
                    var result = IsValidHeader(header);
                    if (!result)
                    {
                        throw new FaultException("Invalid Header");
                    }
                }
                else
                {
                    throw new FaultException("Header not passed");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SMS(object[] inputs)
        {
            try
            {
                var req = (SendSMSRequest)inputs[0];
                var header = req.ServiceToken;
                if (header != null)
                {
                    var result = IsValidHeader(header);
                    if (!result)
                    {
                        throw new FaultException("Invalid Header");
                    }
                }
                else
                {
                    throw new FaultException("Header not passed");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidHeader(ServiceToken header)
        {
            var result = false;
            if (header.Token == ServiceToken)
            {
                result = true;
            }
            return result;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            return;
        }
    }
}