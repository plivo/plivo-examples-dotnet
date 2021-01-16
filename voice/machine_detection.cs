using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace send_sms {
  class bulk_sms {
    static void Main(string[] args) {
      // Make an outbound call
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
      try {
        var response = api.Call.Create(
          to: new List < String > {"+15671234567"}, // The phone number to which the call has to be placed
          from: "+14151234567", // The phone number to be used as the caller id
          answerMethod: "GET", // Method to invoke the answer_url
          answerUrl: "http://s3.amazonaws.com/static.plivo.com/answer.xml", // The URL invoked by Plivo when the outbound call is answered
          machineDetection: "true", // Used to detect if the call has been answered by a machine. The valid values are true and hangup.
          machineDetectionTime: "1000", // Time allotted to analyze if the call has been answered by a machine. The default value is 5000 ms.
          machineDetectionUrl: "http://dotnettest.apphb.com/machine_detection", // A URL where machine detection parameters will be sent by Plivo.
          machineDetectionMethod: "GET" // Method used to invoke machine_detection_url
        );
        Console.WriteLine(response);
      } catch (PlivoRestException e) {
        Console.WriteLine("Exception: " + e.Message);
      }

    }
  }
}

// Example for answer_url and machine_detection_url

using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace machine_detection {
  public class Program: NancyModule {
    public Program() {
      //Machine Detection URL example
      Get["/machine_detection"] = x => {
        String from_number = Request.Query["From"];
        String machine = Request.Query["Machine"];
        String to_number = Request.Query["To"];
        String call_uuid = Request.Query["CallUUID"];
        String event_ = Request.Query["Event"];
        String status = Request.Query["CallStatus"];

        Debug.WriteLine("From : {0}, Machine : {1}, To : {2}, Call UUID : {3}, Event : {4}, Call Status : {5}", from_number, machine, to_number, call_uuid, event_, status);
        return "Done";
      };

      // As soon as the voicemail finishes speaking, and there is a silence for minSilence milliseconds, 
      // the next element in the XML is processed, without waiting for the whole period of length seconds to pass

      Get["/detect"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();
        resp.AddWait(new Dictionary < string, string > () {
          {
            "length",
            "1000"
          }, {
            "silence",
            "true"
          }, {
            "minSilence",
            "3000"
          }
        });

        resp.AddSpeak("Hello Voicemail!", new Dictionary < string, string > () {});

        Debug.WriteLine(resp.ToString());

        var output = resp.ToString();
        var res = (Nancy.Response) output;
        res.ContentType = "text/xml";
        return res;
      };
    }
  }
}

/*
Sample output
{
  "api_id": "a8cd8894-b36e-11e4-ac1f-22000ac51de6",
  "message": "call fired",
  "request_uuid": "6c688abd-92aa-4645-9fc4-c65067516a20"
}

From : 1111111111, Machine : true, To : 2222222222, Call UUID : a9058528-b36e-11e4-8a65-31aa0d5480e9, Event : MachineDetection, Call Status : in-progress

<Response>
    <Wait length="1000" silence="true" minSilence="3000"/>
    <Speak>Hello Voicemail!</Speak>
</Response>
*/