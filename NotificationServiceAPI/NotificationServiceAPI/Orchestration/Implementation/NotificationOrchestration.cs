using NotificationServiceAPI.Admin;
using NotificationServiceAPI.Models;
using RestSharp;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace NotificationServiceAPI.Orchestration
{
    public class NotificationOrchestration : INotificationOrchestration
    {
        private readonly string _ApiSecretKey;
        private readonly AdminModel _Admin;

        public NotificationOrchestration()
        {
            _ApiSecretKey = ConfigurationManager.AppSettings["api_secret"];
            _Admin.Email = ConfigurationManager.AppSettings["admin_email"];
            _Admin.Password = ConfigurationManager.AppSettings["admin_paswsord"];
            _Admin.MailServer = ConfigurationManager.AppSettings["mail_host"];
        }

        public SendEmailResponse SendEmail(SendEmailRequest request)
        {
            var destination = request.Message.Destination;

            var message = request.Message.EmailMessage;

            try
            {
                // Credentials
                var credentials = new NetworkCredential(_Admin.Email,_Admin.Password);

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(credentials.UserName),
                    Subject = "Message from SOAP service",
                    Body = message
                };

                mail.To.Add(new MailAddress(destination));

                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = _Admin.MailServer,
                    EnableSsl = true,
                    Credentials = credentials
                };

                // Send it...
                client.Send(mail);

                return new SendEmailResponse { Response = request.Message + " was sent" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SendSMSResponse SendSMS(SendSMSRequest request)
        {
            try
            {
                var destination = request.Message.Destination;
                var message = request.Message.Message;

                var req = new RestRequest("https://api4.apidaze.io/f0b4155d/sms/send")
                {
                    RequestFormat = DataFormat.Json
                };

                req.AddParameter("api_secret", _ApiSecretKey);
                req.AddParameter("number", $"00{destination}");
                req.AddParameter("subject", "A Message From SOAP Service");
                req.AddParameter("body", message);

                var client = new RestClient();

                var response = client.Post(req);

                if (!response.IsSuccessful)
                {
                    throw new Exception("Request unsuccessful");
                }

                return new SendSMSResponse { Response = $"message sent to {destination}" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}