using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace bulk_calls
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");
             
            IRestResponse<Call> resp = plivo.make_call(new Dictionary<string, string>() 
            {
                { "from", "1111111111" }, // The phone number to which the call has to be placed
                { "to", "2222222222<3333333333" }, // The phone number to be used as the caller Id
                { "answer_url", "http://dotnettest.apphb.com/speak" }, // The URL invoked by Plivo when the outbound call is answered
                {"answer_method","GET"}, // The method used to invoke the answer_url
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
  "api_id": "8ea08aca-b297-11e4-9107-22000afaaa90",
  "message": "calls fired",
  "request_uuid": [
    "0f694769-dddd-41dd-8df5-13733615de89",
    "2951fb6c-2882-4556-9171-4bb8f380b7c8"
  ]
}
*/
