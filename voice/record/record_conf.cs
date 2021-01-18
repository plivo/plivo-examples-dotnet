using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace record_conf {
  public class Program: NancyModule {
    public Program() {
      // Generates a Conference XML
      Get["/conference"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();
        resp.AddSpeak("You will now be placed into a demo conference. This is brought to you by Plivo. To know more, visit us at Plivo.com", new Dictionary < string, string > () {});
        resp.AddConference("demo", new Dictionary < string, string > () {
          {
            "enterSound",
            "beep:1"
          }, // Used to play a sound when a member enters the conference
          {
            "callbackUrl",
            "http://dotnettest.apphb.com/conf_callback"
          }, // If specified, information is sent back to this URL
          {
            "callbackMethod",
            "GET"
          }, // Method used to notify callbackUrl
        });

        Debug.WriteLine(resp.ToString());

        var output = resp.ToString();
        var res = (Nancy.Response) output;
        res.ContentType = "text/xml";
        return res;
      };

      // Record API is called in the callback URL to record the conference
      Get["/conf_callback"] = x => {
        String events = Request.Query["Event"];
        String conf_name = Request.Query["ConferenceName"];

        // The recording starts when the user enters the conference room 
        if (events == "ConferenceEnter") {
          var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
          try {
            var response = api.Conference.StartRecording(
              "conf name"
            );
            Console.WriteLine(response);
          } catch (PlivoRestException e) {
            Console.WriteLine("Exception: " + e.Message);
          }
        } else {
          Debug.WriteLine("Invalid");
        }

        return "Done";
      };
    }
  }
}

/*
Sample output
<Response>
    <Speak>
        You will now be placed into a demo conference. This is brought to you by Plivo. To know more, visit us at Plivo.com
    </Speak>
    <Conference enterSound="beep:1" callbackUrl="http://dotnettest.apphb.com/conf_callback" callbackMethod="GET">demo</Conference>
</Response>

{
    "api_id": "349b23de-bcd1-11e4-9107-22000afaaa90",
    "message": "conference recording started",
    "recording_id": "34a8ed02-bcd1-11e4-9dc1-842b2b096c5d",
    "url": "https://s3.amazonaws.com/recordings_2013/34a8ed02-bcd1-11e4-9dc1-842b2b096c5d.mp3"
}
*/