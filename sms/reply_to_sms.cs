using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;

namespace reply_to_sms
{
    class Program
    {
        static void Main(string[] args)
        {
            Post["/receive_sms"] = x =>
            {
                String from_number = Request.Form["From"]; // Sender's phone number
                String to_number = Request.Form["To"]; // Receiver's phone number
                String text = Request.Form["Text"]; // The text which was received

                // Print the message
                Console.WriteLine("From : {0}, To : {1}, Text : {2}", from_number, to_number, text);

                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Generate Message XML
                resp.AddMessage("Thank you for your message", new Dictionary<string, string>() {
                { "src", to_number }, // Sender's phone number
                { "dst", from_number } // receiver's phone number
                });

                Console.WriteLine(resp.ToString());

                // Return the XML
                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };
        }
    }
}

// Sample output
// From : 2222222222, To : 1111111111, Text : Hi, from Plivo
/*
<Response>
   <Message dst="2222222222" src="1111111111">Thank you for your message</Message>
</Response>
*/