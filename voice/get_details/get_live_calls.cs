using System;
using System.Collections.Generic;
using Plivo;

namespace live_call {
  class get_detail {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Get detail of a live call 
      var response = api.Call.GetLive(liveCallUuid: "ffa23c86-87ed-4fd5-8310-59594df8ae11"); // The status of the call
      //Prints the message details
      Console.Write(resp.Content);

      Console.ReadLine();
    }
  }
}

// Sample output
/*
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