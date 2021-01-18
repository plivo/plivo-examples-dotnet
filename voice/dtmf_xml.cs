using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace custom_tone {
  public class Program: NancyModule {
    public Program() {
      Get["/dtmf"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();

        // Add Speak XML Tag
        resp.AddSpeak("Sending digits", new Dictionary < string, string > () {});

        // Add DTMF XML Tag
        resp.AddDTMF("12345", new Dictionary < string, string > () {});

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
Sample Output
<Response>
    <Speak>Sending Digits</Speak>
    <DTMF>12345</DTMF>
</Response>
*/