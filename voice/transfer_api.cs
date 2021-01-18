using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace transfer_api {
  public class Program: NancyModule {
    public Program() {
      // When the call is answered, a text is played which prompts the user to press 1 to transfer the call.
      // Once the digit is pressed, the transfer API request is made and the call is transfered to another number.

      Get["call_transfer"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();
        String getdigits_action_url = "http://dotnettest.apphb.com/transfer_action";

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
          }, // Indicates the number of retries the user is allowed to input the digits
          {
            "redirect",
            "false"
          }, // Redirect to action URL if true. If false,only request the URL and continue to next element.
          {
            "numDigits",
            "1"
          }
        });

        // Add Speak XML Tag
        gd.AddSpeak("Press 1 to transfer this call", new Dictionary < string, string > ());
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

      // The Transfer API is invoked by the Get Digits action URL

      Get["/transfer_action"] = x => {
        String digits = Request.Query["Digits"];
        String uuid = Request.Query["CallUUID"];
        Debug.WriteLine("Digit pressed : {0}, Call UUID : {1}", digits, uuid);

        var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
        try {
          var response = api.Call.Transfer(
            alegUrl: "http://aleg.url/connect", // URL to transfer for aleg
            callUuid: uuid, // ID of the call
            legs: "aleg" // Leg of the call
          );
          Console.WriteLine(response);
        } catch (PlivoRestException e) {
          Console.WriteLine("Exception: " + e.Message);
        }
        return "Transfer API";
      };
    }
  }
}

/*
Sample Output
<Response>
    <GetDigits action="http://dotnettest.apphb.com/transfer_action" method="GET" timeout="7" retries="1" redirect="false" numDigits="1">
        <Speak>Press 1 to transfer this call</Speak>
    </GetDigits>
    <Wait length="10"/>
</Response>

Digit pressed : 1, Call UUID : e66aa1a0-9587-11e4-9d37-c15e0b562efe 


<Response>
    <Speak>Connecting your call..</Speak>
    <Dial>
        <Number>1111111111</Number>
    </Dial>
</Response>
*/