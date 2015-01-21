using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;

namespace send_alpha
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");
           
            IRestResponse<MessageResponse> resp = plivo.send_message(new Dictionary<string, string>() 
            {
                { "src", "ALPHA" }, // Alphanumeric sender ID
                { "dst", "1111111111" }, // Receiver's phone number wiht country code
                { "text", "Hi, text from plivo" } // Your SMS text message
            });

            //Prints the message details
            Console.Write(resp.Content);
            Console.ReadLine();
        }
    }
}

// Sample output
/*{
  "api_id": "e53026ae-a0ca-11e4-a2d1-22000ac5040c",
  "message": "message(s) queued",
  "message_uuid": [
    "e5478308-a0ca-11e4-9bd8-22000afa12b9"
  ]
}
*/