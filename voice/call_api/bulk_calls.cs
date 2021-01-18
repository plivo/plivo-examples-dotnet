using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace bulk_calls {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
      try {
        var response = api.Call.Create(
          to: new List < String > {"141512389112<14151231213"}, // The phone number to which the call has to be placed
          from: "+14151234567", // The phone number to be used as the caller Id 
          answerMethod: "GET", // The method used to invoke the answer_url
          answerUrl: "http://s3.amazonaws.com/static.plivo.com/answer.xml" // The URL invoked by Plivo when the outbound call is answered
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
  "api_id": "8ea08aca-b297-11e4-9107-22000afaaa90",
  "message": "calls fired",
  "request_uuid": [
    "0f694769-dddd-41dd-8df5-13733615de89",
    "2951fb6c-2882-4556-9171-4bb8f380b7c8"
  ]
}
*/