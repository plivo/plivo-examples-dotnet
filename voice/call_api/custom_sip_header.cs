using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace custom_sip_header
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
                {"answer_method","GET"}, // The method used to invoke the answer_url
                {"sip_headers","Test=Sample"} // List of SIP Headers in the form of 'key=value' pairs, separated by commas 
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
  "api_id": "fa813ea6-b297-11e4-af95-22000ac54c79",
  "message": "call fired",
  "request_uuid": "93ef02ab-8a10-4eef-9127-2fcf0fe487ae"
}
*/
