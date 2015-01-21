using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;

namespace receive_sms
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

                return "Message Received";
            };
        }
    }
}

// Sample Output
// From : 1111111111, To : 2222222222, Text : Hello, from Plivo