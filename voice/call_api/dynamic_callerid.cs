using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

// Set the caller ID using Dial XML

namespace hangup {
  public class Program: NancyModule {
    public Program() {
      Get["dynamic_caller_id"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();

        // Generate Dial XML
        Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary < string, string > () {});
        dial.AddNumber("1111111111", new Dictionary < string, string > () {});
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
    <Dial>
        <Number>919663489033</Number>
    </Dial>
</Response>
*/

// Set the caller ID using Call API

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
          answerUrl: "http://s3.amazonaws.com/static.plivo.com/answer.xml" // The URL invoked by Plivo when the outbound call is answered
        );
        Console.WriteLine(response);
      } catch (PlivoRestException e) {
        Console.WriteLine("Exception: " + e.Message);
      }

    }
  }
}
/*
Sample Output
{
  "api_id": "eaf9a5ec-b295-11e4-b932-22000ac50fac",
  "message": "call fired",
  "request_uuid": "1b034cad-65e7-4a8c-8d93-4040d9d8809a"
}
*/