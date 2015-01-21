using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;

namespace long_sms
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
                { "text", "This randomly generated text can be used in your layout (webdesign , websites, books, posters ... ) for free. This text is entirely free of law. Feel free to link to this site by using the image below or by making a simple text link" } // Your SMS text message
            });

            //Prints the message details
            Console.Write(resp.Content);
        }
    }
}

// Sample output
/*
(202, {
   u'message': u'message(s) queued', 
   u'message_uuid': [u'dcfc1510-9260-11e4-b1a4-22000ac693b1'], 
   u'api_id': u'dce8fb42-9260-11e4-b932-22000ac50fac'
   }
)
*/