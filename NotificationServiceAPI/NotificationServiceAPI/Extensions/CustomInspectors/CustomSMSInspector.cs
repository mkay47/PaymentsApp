using NotificationServiceAPI.Models;
using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text.RegularExpressions;

namespace NotificationServiceAPI.Extensions
{
    public class CustomSMSInspector : IParameterInspector
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            return;
        }

        private bool IsEmpty(string message)
        {
            var result = false;

            if (message.Length > 1)
            {
                result = true;
            }
            return result;
        }

        private bool IsValid(string destination)
        {
            var pattern = @"27[0-9]{9}$";

            var result = false;
            if (Regex.IsMatch(destination, pattern, RegexOptions.IgnoreCase))
            {
                result = true;
            }
            return result;
        }

        private void CheckDestination(string destination)
        {
            try
            {
                if (destination != null)
                {
                    if (!IsValid(destination))
                    {
                        throw new FaultException($"Invalid number: {destination}");
                    }
                }
                else
                {
                    throw new FaultException("No number entered");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckSMSMessage(string smsMessage)
        {
            if (smsMessage != null)
            {
                if (!IsEmpty(smsMessage))
                {
                    throw new FaultException("No Message Enter");
                }
            }
            else
            {
                throw new FaultException("Bad Request");
            }
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            var req = (SendSMSRequest)inputs[0];

            var sms = req.Message;

            if (sms != null)
            {
                var destination = sms.Destination;
                CheckDestination(destination);
                var smsMessage = sms.Message;
                CheckSMSMessage(smsMessage);
            }
            else
            {
                throw new FaultException("Bad Request");
            }
            return null;
        }
    }
}