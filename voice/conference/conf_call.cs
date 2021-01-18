using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace conf_call {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
      try {
        var response = api.Call.Create(
          to: new List < String > {
            "141512389112"
          }, // The phone number to which the call has to be placed
          from: "+14151234567", // The phone number to be used as the caller Id 
          answerMethod: "GET", // The method used to invoke the answer_url
          answerUrl: "https://s3.amazonaws.com/plivosamplexml/conference_url.xml", // The URL invoked by Plivo when the outbound call is answered
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
  "api_id": "1153be1e-b5af-11e4-9107-22000afaaa90",
  "message": "calls fired",
  "request_uuid": [
    "e8c352d0-fdd5-4d74-a27e-65ff594dbe91",
    "984a0025-377e-4873-8862-f773620a258e"
  ]
}
*/