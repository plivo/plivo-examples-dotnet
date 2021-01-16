using System;
using System.Collections.Generic;
using Plivo;

namespace get_details {
  class get_all_call_details {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Get all call details
      var response = api.Call.List(
        limit: 5, 
        offset: 0
        );

      // Prints the call details
      Console.Write(response);

      // Filtering the response
      var response = api.Call.List(
        limit: 5, 
        offset: 0, 
        fromNumber: "1111111111", 
        toNumber: "2222222222", 
        callDirection: "outbound"
      );
      // Prints the call details
      Console.Write(Response);
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