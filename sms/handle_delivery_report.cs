using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;

namespace PlivoMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            Post["/delivery_report"] = x =>
            {
                String from_number = Request.Form["From"]; // Sender's phone number
                String to_number = Request.Form["To"]; // Receiver's phone number
                String status = Request.Form["Status"]; // Status of the message
                String uuid = Request.Form["MessageUUID"]; // Message UUID

                Console.WriteLine("From : {0}, To : {1}, Status : {2}, UUID : {3}", from_number, to_number, status, uuid);

                return "Delivery Reported";
            };
        }
    }
}

// Sample Output
// From : 2222222222 To : 1111111111 Status : queued MessageUUID : 53e6526a-8a7a-11e4-a77d-22000ae383ea
// From : 2222222222 To : 1111111111 Status : sent MessageUUID : 53e6526a-8a7a-11e4-a77d-22000ae383ea
// From : 2222222222 To : 1111111111 Status : delivered MessageUUID : 53e6526a-8a7a-11e4-a77d-22000ae383ea

// Possible values for message status - "queued", "sent", "failed", "delivered", "undelivered" or "rejected"