using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace greetCaller {
  public class Program: NancyModule {
    public Program() {
      Get["/greet_caller"] = x => {
        var callers = new Dictionary < string,
          string > () {
            {
              "1111111111","ABCDE"
            }, {
              "2222222222","VWXYZ"
            }, {
              "3333333333","QWERTY"
            }
          };

        String fromNumber = Request.Query["From"];

        Plivo.XML.Response resp = new Plivo.XML.Response();

        if (callers.ContainsKey(fromNumber)) {
          string body = "Hello " + callers[fromNumber];
          resp.AddSpeak(body, new Dictionary < string, string > () {});
        } else {
          resp.AddSpeak("Hello Stranger!", new Dictionary < string, string > () {});
        }

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
    <Speak>Hello,ABCDEF</Speak>
</Response>
*/