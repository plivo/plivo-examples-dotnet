using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;

namespace PlivoMessage
{
    class bulk_sms
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");

            IRestResponse<MessageResponse> resp = plivo.send_message(new Dictionary<string, string>() 
            {
                { "src", "1111111111" }, // Sender's phone number with country code
                { "dst", "2222222222<3333333333" }, // Receiver's phone number wiht country code
                { "text", "Hi, text from Plivo." } // Your SMS text message
            });

            //Prints the message details
            Console.Write(resp.Content);

            // Loop through the message_uuid
            int count = resp.Data.message_uuid.Count;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Message UUID : {0}", resp.Data.message_uuid[i]);
            }
            
            // Print the api_id
            Console.WriteLine("Api ID : {0}", resp.Data.api_id);

            // When an invalid number is given as a dst parameter, an error will be thrown and the message will not be sent 

            IRestResponse<MessageResponse> response = plivo.send_message(new Dictionary<string, string>() 
            {
                { "src", "1111111111" }, // Sender's phone number with country code
                { "dst", "111111<2222222222" }, // Receiver's phone number wiht country code
                { "text", "Hi, text from Plivo." } // Your SMS text message
            });

            //Prints the message details
            Console.Write(response.Content);

            Console.ReadLine();
        }
    }
}

// Sample output
/*
{
  "api_id": "a6d93290-a0b3-11e4-96e3-22000abcb9af",
  "message": "message(s) queued",
  "message_uuid": [
    "a6f4f17e-a0b3-11e4-89de-22000ae885b8",
    "a6f4eeae-a0b3-11e4-9bd8-22000afa12b9"
  ]
}
Message UUID : a6f4f17e-a0b3-11e4-89de-22000ae885b8
Message UUID : a6f4eeae-a0b3-11e4-9bd8-22000afa12b9
Api ID : a6d93290-a0b3-11e4-96e3-22000abcb9af
*/

// Sample Output for an invalid number
/*
{
  "api_id": "e5f6c53c-a0b3-11e4-a2d1-22000ac5040c",
  "error": "111111 is not a valid phone number"
}
*/