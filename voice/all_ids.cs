using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace make_calls {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
      try {
        var response = api.Call.Create(
          to: new List < String > {"141512389112"}, // The phone number to which the call has to be placed
          from: "+14151234567", // The phone number to be used as the caller Id 
          answerMethod: "GET", // The method used to invoke the answer_url
          answerUrl: "http://s3.amazonaws.com/static.plivo.com/answer.xml", // The URL invoked by Plivo when the outbound call is answered
          // Example for Asynchronous request
          // callbackUrl: "http://dotnettest.apphb.com/callback",
          // callbackMethod:"GET"
        );
        Console.WriteLine(response);
      } catch (PlivoRestException e) {
        Console.WriteLine("Exception: " + e.Message);
      }

      // Call UUID is th id of a live call. This ID is returned only after the call is answered.

      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
      try {
        var response = api.Call.Get(
          callUuid: "ffa23c86-87ed-4fd5-8310-59594df8ae11" // ID of the call
        );
        Console.WriteLine(response);
      } catch (PlivoRestException e) {
        Console.WriteLine("Exception: " + e.Message);
      }
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

