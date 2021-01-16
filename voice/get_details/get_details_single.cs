using System;
using System.Collections.Generic;
using Plivo;

namespace get_details {
  class get_single_call_detail {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Get single call details
      var response = api.Call.Get(callUuid: "ffa23c86-87ed-4fd5-8310-59594df8ae11");

      //Prints the call details
      Console.Write(response);
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