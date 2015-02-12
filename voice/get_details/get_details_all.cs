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

            // Without Filters
             
            IRestResponse<CDRList> resp = plivo.get_cdrs(new Dictionary<string, string>() {});

            //Prints the message details
            Console.Write(resp.Content);

            // Filtering the response

            IRestResponse<CDRList> resp = plivo.get_cdrs(new Dictionary<string, string>() 
            {
                { "end_time_gt", "2015-02-10 11:47" }, // Filter out calls according to the time of completion. gte stands for greater than or equal.
                { "call_direction", "outbound" }, // Filter the results by call direction. The valid inputs are inbound and outbound
                { "from_number","1111111111"}, // Filter the results by the number from where the call originated
                { "to_number","2222222222"}, // Filter the results by the number to which the call was made
                { "limit","2"}, // The number of results per page
                { "offset","0"} // The number of value items by which the results should be offset
            });

            //Prints the message details
            Console.Write(resp.Content);

            Console.ReadLine();
        }
    }
}

// Sample output
/*
Without Filter
{
  "api_id": "24e4ec9e-b2a5-11e4-b932-22000ac50fac",
  "meta": {
    "limit": 2,
    "next": "/v1/Account/XXXXXXXXXXXXXXX/Call/?limit=20&offset=0",
    "offset": 0,
    "previous": null,
    "total_count": 107
  },
  "objects": [
    {
      "bill_duration": 15,
      "billed_duration": 60,
      "call_direction": "inbound",
      "call_duration": 15,
      "call_uuid": "44c56e60-b1e4-11e4-87e0-2b70f7e6a9a7",
      "end_time": "2015-02-11 17:21:24+05:30",
      "from_number": "2222222222",
      "parent_call_uuid": null,
      "resource_uri": "/v1/Account/XXXXXXXXXXXXXXX/Call/44c56e60-b1e4-11e4-87e0-2b70f7e6a9a7/",

      "to_number": "1111111111",
      "total_amount": "0.00850",
      "total_rate": "0.00850"
    },
    {
      "bill_duration": 0,
      "billed_duration": 0,
      "call_direction": "outbound",
      "call_duration": 0,
      "call_uuid": "f9f61e54-b1e1-11e4-84ac-377ffe01233f",
      "end_time": "2015-02-11 17:05:32+05:30",
      "from_number": "+1111111111",
      "parent_call_uuid": null,
      "resource_uri": "/v1/Account/XXXXXXXXXXXXXXX/Call/f9f61e54-b1e1-11e4-84ac-377ffe01233f/",

      "to_number": "3333333333",
      "total_amount": "0.00000",
      "total_rate": "0.03570"
    }
  ]
}

With Filters
{
  "api_id": "83bd23ee-b2a5-11e4-b932-22000ac50fac",
  "meta": {
    "limit": 2,
    "next": "/v1/Account/XXXXXXXXXXXXXXX/Call/?end_time_gt=2015-02-10+11%3A47&call_direction=outbound&to_number=919663489033&from_number=1111111111&limit=2&offset=2",
    "offset": 0,
    "previous": null,
    "total_count": 14
  },
  "objects": [
    {
      "bill_duration": 0,
      "billed_duration": 0,
      "call_direction": "outbound",
      "call_duration": 0,
      "call_uuid": "f9f61e54-b1e1-11e4-84ac-377ffe01233f",
      "end_time": "2015-02-11 17:05:32+05:30",
      "from_number": "+1111111111",
      "parent_call_uuid": null,
      "resource_uri": "/v1/Account/XXXXXXXXXXXXXXX/Call/f9f61e54-b1e1-11e4-84ac-377ffe01233f/",

      "to_number": "2222222222",
      "total_amount": "0.00000",
      "total_rate": "0.03570"
    },
    {
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
*/
