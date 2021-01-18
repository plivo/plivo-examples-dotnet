using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace custom_sip_header {
  class Program {
    static void Main(string[] args) {

      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
      try {
        var response = api.Call.Create(
          to: new List < String > {"141512389112"}, // The phone number to which the call has to be placed
          from: "+14151234567", // The phone number to be used as the caller Id 
          answerMethod: "GET", // The method used to invoke the answer_url
          answerUrl: "http://s3.amazonaws.com/static.plivo.com/answer.xml", // The URL invoked by Plivo when the outbound call is answered
          sipHeaders: "Test=Sample" // List of SIP Headers in the form of 'key=value' pairs, separated by commas 
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
  "api_id": "fa813ea6-b297-11e4-af95-22000ac54c79",
  "message": "call fired",
  "request_uuid": "93ef02ab-8a10-4eef-9127-2fcf0fe487ae"
}

The SIP header can be seen as a query parameter in the answer_url
/answer_xml?Direction=outbound&From=14151234567&ALegUUID=fad58e02-b297-11e4-96d7-377ffe01233f&BillRate=0.03570&To=919663489033&
X-PH-Test=Sample&CallUUID=fad58e02-b297-11e4-96d7-377ffe01233f&ALegRequestUUID=93ef02ab-8a10-4eef-9127-2fcf0fe487ae&
RequestUUID=93ef02ab-8a10-4eef-9127-2fcf0fe487ae&CallStatus=in-progress&Event=StartApp HTTP/1.1" 200 59 "-" 
"Mozilla/5.0 (X11; Linux i686) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.35 Safari/535.1" 
"source=nginx measure#http.response_time=0.088s measure#http.bytes.sent=217"
*/
