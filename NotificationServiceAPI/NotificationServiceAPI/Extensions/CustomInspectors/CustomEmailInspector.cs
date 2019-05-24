using NotificationServiceAPI.Models;
using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text.RegularExpressions;

namespace NotificationServiceAPI.Extensions
{
    public class CustomEmailInspector : IParameterInspector
    {
        public object BeforeCall(string operationName, object[] inputs)
        {
            try
            {
                var req = (SendEmailRequest)inputs[0];

                var message = req.Message;

                if (message != null)
                {
                    var destination = message.Destination;
                    CheckDestination(destination);
                    var emailMessage = message.EmailMessage;
                    CheckEmailMessage(emailMessage);
                }
                else
                {
                    throw new FaultException("Bad Request");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
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
            var pattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

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
                        throw new FaultException($"Invalid Email: {destination}");
                    }
                }
                else
                {
                    throw new FaultException("No email address entered");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckEmailMessage(string emailMessage)
        {
            try
            {
                if (emailMessage != null)
                {
                    if (!IsEmpty(emailMessage))
                    {
                        throw new FaultException("No Message Enter");
                    }
                }
                else
                {
                    throw new FaultException("Bad Request");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            return;
        }
    }
}