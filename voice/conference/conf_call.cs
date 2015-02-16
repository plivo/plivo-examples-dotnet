using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace conf_call
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");
             
            IRestResponse<Call> resp = plivo.make_call(new Dictionary<string,string>() 
            {   
                { "to", "2222222222<3333333333" }, // The phone number to which the call has to be placed
                {"from", "1111111111"}, // The phone number to be used as the caller id
                {"answer_url","http://dotnettest.apphb.com/response/conference"}, // The URL invoked by Plivo when the outbound call is answered
                {"answer_method","GET"} // The method used to call the answer_url
            });

            //Prints the message details
            Console.Write(resp.Content);

            Console.ReadLine();
        }
    }
}

// Sample output
/*
erenceanswer_method - GET{
  "api_id": "1153be1e-b5af-11e4-9107-22000afaaa90",
  "message": "calls fired",
  "request_uuid": [
    "e8c352d0-fdd5-4d74-a27e-65ff594dbe91",
    "984a0025-377e-4873-8862-f773620a258e"
  ]
}
*/