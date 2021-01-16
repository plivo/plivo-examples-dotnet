using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace forward_calls {
  public class Program: NancyModule {
    public Program() {
      Get["/forward"] = x => {
        // The phone number of the person calling your Plivo number,
        // we'll use this as the Caller ID when we forward the call.
        String from_number = Request.Query["From"];

        // The number you would like to forward the call to.
        String forwarding_number = "2222222222";

        Plivo.XML.Response resp = new Plivo.XML.Response();
        Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary < string, string > () {
          {
            "callerId",
            from_numbers
          } // The phone number to be used as the caller id. It can be set to the from_number or any custom number.
        });
        dial.AddNumber(forwarding_number, new Dictionary < string, string > () {});

        resp.Add(dial);
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
   <Dial callerId="1111111111">
       <Number>2222222222</Number>
   </Dial>
</Response>
*/
