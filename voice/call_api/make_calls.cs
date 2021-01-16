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

Asynchronous Request
{
  "api_id": "5314967c-b297-11e4-b932-22000ac50fac",
  "message": "async api spawned"
}
*/

