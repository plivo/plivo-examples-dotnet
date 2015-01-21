using System;
using System.Collections.Generic;
using System.Reflection;
using RestSharp;
using Plivo.API;

namespace get_details
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");

            IRestResponse<Message> resp = plivo.get_message(new Dictionary<string, string>() 
            {
                { "record_id", "1aead330-8ff9-11e4-9bd8-22000afa12b9" } // Message UUID
            });

            //Prints the message details
            Console.Write(resp.Content);

            Console.ReadLine();
        }
    }
}

// Sample Output
/* 
{
  "api_id": "8df2725a-a0cc-11e4-ac1f-22000ac51de6",
  "from_number": "1111111111",
  "message_direction": "outbound",
  "message_state": "sent",
  "message_time": "2014-12-30 11:54:38+04:00",
  "message_type": "sms",
  "message_uuid": "1aead330-8ff9-11e4-9bd8-22000afa12b9",
  "resource_uri": "/v1/Account/XXXXXXXXXXXX/Message/1aead330-8ff9-11e4-9bd8-22000afa12b9/",
  "to_number": "2222222222",
  "total_amount": "0.00650",
  "total_rate": "0.00650",
  "units": 1
}
*/