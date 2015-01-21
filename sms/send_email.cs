using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;
using System.Net.Mail;

namespace Send_Email
{
    public class Program : NancyModule
    {
        public Program()
        {
            Post["/receive_sms"] = x =>
            {
                String from_number = Request.Form["From"]; // Sender's phone number
                String to_number = Request.Form["To"]; // Receiver's phone number
                String text = Request.Form["Text"]; // Te text which was received

                // Print the text
                Console.WriteLine("From : {0}, To : {1}, Text : {2}", from_number, to_number, text);

                // Call the SendEmail function
                string result = SendEmail(text);
                return result;
            };
        }

        // Send Email function
        protected string SendEmail(string text)
        {
            string result = "Message Sent Successfully!!";
            string user_name = "Your mail address";// Sender’s email ID
            const string password = "Your password"; //  password here…
            string subject = "Testing"; // Subject of the mail
            string to = "To mail address"; 
            string body = text; // Body of the mail which the text that was received

            try
            {
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
            }

            catch (Exception ex)
            {
               result = "Error sending email!!!";
            }
            
            return result;
        }
    }
}