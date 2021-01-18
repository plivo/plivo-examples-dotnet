using System;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Nancy;
using RestSharp;
using Plivo.API;
using System.Net.Mail;

namespace Send_Email
{
    public class Program : NancyModule
    {
        public Program()
        {
            Post["/email_sms/"] = x =>
            {
                String from_number = Request.Form["From"]; // Sender's phone number
                String to_number = Request.Form["To"]; // Receiver's phone number - Plivo number
                String text = Request.Form["Text"]; // The text which was received on your Plivo number

                // Print the message
                Console.WriteLine("Message received from {0}: {1}", from_number, text);

                // Call the SendEmail function
                string result = SendEmail(text, from_number);
                return result;
            };
        }

        // Send Email function
        protected string SendEmail(string text, string from_number)
        {
            string result = "Message Sent Successfully!!";
            string user_name = "Your email address";// Sender’s email ID
            const string password = "Your password"; //  password here…
            string subject = "SMS from {0}", from_number; // Subject of the mail
            string to = "To email address";
            string body = text; // Body of the mail which the text that was received
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {  return true; };

            try {
                // Initialize the smtp client
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(user_name, password),
                    Timeout = 30000,
                };

               MailMessage message = new MailMessage(user_name, to, subject, body);
               // Send the mail
               smtp.Send(message);
            } catch (Exception ex) {
               result = "Error sending email!!!";
            }
            return result;
        }
    }
}


// Sample Output
/*
Message received from  From : 1111111111, Text : Hello, from Plivo
Message Sent Successfully!!
*/