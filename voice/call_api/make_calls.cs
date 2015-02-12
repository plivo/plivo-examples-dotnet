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
             
            IRestResponse<Call> resp = plivo.make_call(new Dictionary<string, string>() 
            {
                { "from", "1111111111" }, // The phone number to which the call has to be placed
                { "to", "2222222222" }, // The phone number to be used as the caller Id
                { "answer_url", "http://dotnettest.apphb.com/speak" }, // The URL invoked by Plivo when the outbound call is answered
                { "answer_method","GET"}, // The method used to invoke the answer_url
                // Example for Asynchronous request
                // {"callback_url","http://dotnettest.apphb.com/callback"},
                // {"callback_method","GET"}
            });

            //Prints the response
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

Asynchronous Request
{
  "api_id": "5314967c-b297-11e4-b932-22000ac50fac",
  "message": "async api spawned"
}

*/

