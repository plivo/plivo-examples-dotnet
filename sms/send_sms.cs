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
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");

            IRestResponse<MessageResponse> resp = plivo.send_message(new Dictionary<string, string>() 
            {
                { "src", "1111111111" }, // Sender's phone number with country code
                { "dst", "2222222222" }, // Receiver's phone number wiht country code
                { "text", "Hi, text from Plivo." } // Your SMS text message
                // To send Unicode text
                // {"text", "こんにちは、元気ですか？"} // Your SMS text message - Japanese
                // {"text", "Ce est texte généré aléatoirement"} // Your SMS text message - French
            });

            //Prints the message details
            Console.Write(resp.Content);

            // Print the message_uuid
            Console.WriteLine(resp.Data.message_uuid)

            // Print the api_id
            Console.WriteLine(resp.Data.api_id)

            Console.ReadLine();
        }
    }
}

// Sample Output
/*
{
  "api_id": "e8f04d94-a0ae-11e4-b423-22000ac8a2f8",
  "message": "message(s) queued",
  "message_uuid": [
    "e908608c-a0ae-11e4-89de-22000ae885b8"
  ]
}
Message UUID : e908608c-a0ae-11e4-89de-22000ae885b8
Api ID : e8f04d94-a0ae-11e4-b423-22000ac8a2f8
*/