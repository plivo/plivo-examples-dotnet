using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace send_sms
{
    class bulk_sms
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");
             
            IRestResponse<LiveCall> resp = plivo.get_live_call(new Dictionary<string,string>() 
            {   
                { "call_uuid", "cd8fb3a0-b2a6-11e4-9a04-f5504e456438" } // The status of the call
            });

            //Prints the message details
            Console.Write(resp.Content);

            Console.ReadLine();
        }
    }
}

// Sample output
/*
{
  "api_id": "f972956e-b2a6-11e4-8ccf-22000afb14f7",
  "call_status": "in-progress",
  "call_uuid": "cd8fb3a0-b2a6-11e4-9a04-f5504e456438",
  "caller_name": "+2222222222",
  "direction": "inbound",
  "from": "2222222222",
  "session_start": "2015-02-12 11:03:41.617073",
  "to": "1111111111"
}
*/