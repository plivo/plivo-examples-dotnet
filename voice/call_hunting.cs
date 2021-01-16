using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace call_hunting {
  public class Program: NancyModule {
    public Program() {
      Get["/call_hunting"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();

        // Generate Dial XML
        Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary < string, string > () {});
        dial.AddUser("sip:abcd1234@phone.plivo.com", new Dictionary < string, string > () {});
        dial.AddNumber("2222222222", new Dictionary < string, string > () {});
        dial.AddNumber("3333333333", new Dictionary < string, string > () {});
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
    <Speak>Dialing</Speak>
    <Dial>
       <User>sip:abcd1234@phone.plivo.com</User>
        <Number>2222222222</Number>
        <Number>3333333333</Number>
    </Dial>
</Response>
*/