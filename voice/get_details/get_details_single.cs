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
             
            IRestResponse<CDR> resp = plivo.get_cdr(new Dictionary<string, string>() 
            {
                { "record_id", "064c0e98-b1e2-11e4-989e-c73b3246dc2a" } // The ID of the call
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
  "api_id": "fa69df46-b2a5-11e4-af95-22000ac54c79",
  "bill_duration": 0,
  "billed_duration": 0,
  "call_direction": "outbound",
  "call_duration": 0,
  "call_uuid": "064c0e98-b1e2-11e4-989e-c73b3246dc2a",
  "end_time": "2015-02-11 17:05:28+05:30",
  "from_number": "+1111111111",
  "parent_call_uuid": null,
  "resource_uri": "/v1/Account/XXXXXXXXXXXXXXX/Call/064c0e98-b1e2-11e4-989e-c73b3246dc2a/",
  "to_number": "2222222222",
  "total_amount": "0.00000",
  "total_rate": "0.03570"
}

*/