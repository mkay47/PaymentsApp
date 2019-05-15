using CentralServiceAPI.web.Notification.Mappings;
using CentralServiceAPI.web.Notification.Models;
using Microsoft.Extensions.Configuration;
using ServiceReference1;
using System;
using System.ServiceModel;
using System.Xml;

namespace CentralServiceAPI.web.Notification.Orchestrations
{
    public class NotificationOrchestration : INotificationOrchestration
    {
        private readonly INotificationMapping _NotificationMapping;
        private readonly string _Token;

        public NotificationOrchestration(INotificationMapping notificationMapping, IConfiguration configuration)
        {
            _NotificationMapping = notificationMapping;
            _Token = configuration.GetSection("NotificationServiceKey").Value;
        }

        public MessageOutput SendEmail(MessageInput messageInput)
        {
            var client = new NotificationServiceClient();

            client.OpenAsync();

            var obj = new SendEmailRequest()
            {
                ServiceToken = new ServiceToken
                {
                    Token = _Token
                },
                Message = new Email()
                {
                    Destination = messageInput.Destination,
                    EmailMessage = messageInput.Message
                }
            };

            var reply = client.EmailAsync(obj.ServiceToken, obj.Message).Result;

            client.CloseAsync();

            return _NotificationMapping.MapNotificationOutput(reply);
        }

        public MessageOutput SendSMS(MessageInput messageInput)
        {
            //var basicHttpbinding = new BasicHttpBinding(BasicHttpSecurityMode.None)
            //{
            //    Name = "BasicHttpBinding_IBlogService",
            //    MaxReceivedMessageSize = 2147483646,
            //    MaxBufferSize = 2147483646,
            //    MaxBufferPoolSize = 2147483646,
            //    ReaderQuotas = new XmlDictionaryReaderQuotas()
            //    {
            //        MaxArrayLength = 2147483646,
            //        MaxStringContentLength = 5242880
            //    },
            //    SendTimeout = new TimeSpan(0, 5, 0),
            //    CloseTimeout = new TimeSpan(0, 5, 0),
            //    Security = new BasicHttpSecurity
            //    {
            //        Mode = BasicHttpSecurityMode.None,
            //        Transport = new HttpTransportSecurity
            //        {
            //            ClientCredentialType = HttpClientCredentialType.None
            //        }
            //    }
            //};

            //var endpointAddress = new EndpointAddress("http://localhost:8000");

            var client = new NotificationServiceClient();

            client.OpenAsync();

            var obj = new SendSMSRequest()
            {
                ServiceToken = new ServiceToken
                {
                    Token = _Token
                },
                Message = new SMS()
                {
                    Destination = messageInput.Destination,
                    Message = messageInput.Message
                }
            };

            var reply = client.SMSAsync(obj.ServiceToken, obj.Message).Result;

            client.CloseAsync();

            return _NotificationMapping.MapNotificationOutput(reply);
        }
    }
}