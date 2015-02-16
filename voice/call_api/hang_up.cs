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
             
            IRestResponse<GenericResponse> resp = plivo.hangup_call(new Dictionary<string, string>() 
            {
                { "call_uuid", "defb0706-86a6-11e4-b303-498d468c930b" } // UUID of the call to be hung up
            });

            //Prints the response
            Console.Write(resp.Content);

            Console.ReadLine();
        }
    }
}

/*
Sucecssful Output
" "

Unsuccessful Output
{
  "api_id": "be4512a4-b5cc-11e4-9107-22000afaaa90",
  "error": "not found"
}
*/