using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace make_calls
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");

            // API ID is returned for every API request. 
            // Request UUID is request id of the call. This ID is returned as soon as the call is fired irrespective of whether the call is answered or not
             
            IRestResponse<Call> resp = plivo.make_call(new Dictionary<string, string>() 
            {
                { "from", "1111111111" }, // The phone number to which the call has to be placed
                { "to", "2222222222" }, // The phone number to be used as the caller Id
                { "answer_url", "http://dotnettest.apphb.com/speak" }, // The URL invoked by Plivo when the outbound call is answered
                { "answer_method","GET"} // The method used to invoke the answer_url
            });

            //Prints the response
            Console.Write(resp.Content);

            // Call UUID is th id of a live call. This ID is returned only after the call is answered.

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
  "api_id": "eaf9a5ec-b295-11e4-b932-22000ac50fac",
  "message": "call fired",
  "request_uuid": "1b034cad-65e7-4a8c-8d93-4040d9d8809a"
}

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

