using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace hangup {
  public class Program: NancyModule {
    public Program() {
      Get["/speak_api"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();

        String getdigits_action_url = "http://dotnettest.apphb.com/speak_action";

        // Add GetDigits XML Tag
        GetDigits gd = new GetDigits("", new Dictionary < string, string > () {
          {
            "action",
            getdigits_action_url
          }, // The URL to which the digits are sent. 
          {
            "method",
            "GET"
          }, // Submit to action URL using GET or POST.
          {
            "timeout",
            "7"
          }, // Time in seconds to wait to receive the first digit. 
          {
            "retries",
            "1"
          }, // Indicates the number of retries the user is allowed to input the digits,
          {
            "redirect",
            "false"
          } // Redirect to action URL if true. If false,only request the URL and continue to next element.
        });

        // Add GetDigits Speak XML Tag
        gd.AddSpeak("Press 1 to listen to a message", new Dictionary < string, string > ());
        resp.Add(gd);

        // Add Wait XML Tag
        resp.AddWait(new Dictionary < string, string > () {
          {
            "length",
            "10"
          }
        });

        Debug.WriteLine(resp.ToString());

        var output = resp.ToString();
        var res = (Nancy.Response) output;
        res.ContentType = "text/xml";
        return res;
      };

      Get["/speak_action"] = x => {
        String digits = Request.Query["Digits"];
        String uuid = Request.Query["CallUUID"];
        Debug.WriteLine("Digit pressed : {0}, Call UUID : {1}", digits, uuid);

        // Call to Speak API
        var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
        try {
          var response = api.Call.StartSpeaking(
            callUuid: uuid,
            text: "Hello World"
          );
          Console.WriteLine(response);
        } catch (PlivoRestException e) {
          Console.WriteLine("Exception: " + e.Message);
        }

      };
    }
  }
}

/*
Sample output
<Response>
    <GetDigits action="http://dotnettest.apphb.com/speak_action" method="GET" timeout="7" retries="1" redirect="false">
        <Speak>Press 1 to listen to a message</Speak>
    </GetDigits>
    <Wait length="10"/>
</Response>

Digit pressed : 1, Call UUID : c726ca3c-b29f-11e4-89d2-31aa0d5480e9

{
    "api_id": "cdabcd44-b29f-11e4-9107-22000afaaa90",
    "message": "speak started"
}
*/