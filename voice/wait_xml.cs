using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace wait_xml {
  public class Program: NancyModule {
    public Program() {
      Get["/basic_wait"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();

        // Add Speak XML Tag
        resp.AddSpeak("I will wait for 10 seconds", new Dictionary < string, string > () {});

        // Add Wait XML Tag
        resp.AddWait(new Dictionary < string, string > () {
          {
            "length",
            "10"
          }
        });

        // Add Speak XML Tag
        resp.AddSpeak("I just waited 10 seconds", new Dictionary < string, string > () {});

        Debug.WriteLine(resp.ToString());

        var output = resp.ToString();
        var res = (Nancy.Response) output;
        res.ContentType = "text/xml";
        return res;
      };

      Get["/delayed_wait"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();

        // Add Wait XML Tag
        resp.AddWait(new Dictionary < string, string > () {
          {
            "length","10"
          }
        });

        // Add Speak XML Tag
        resp.AddSpeak("Hello", new Dictionary < string, string > () {});

        Debug.WriteLine(resp.ToString());

        var output = resp.ToString();
        var res = (Nancy.Response) output;
        res.ContentType = "text/xml";
        return res;
      };

      Get["/beep_det"] = x => {
        Plivo.XML.Response resp = new Plivo.XML.Response();

        // Add Wait XML Tag
        resp.AddWait(new Dictionary < string, string > () {
          {
            "length","100"
          }, 
          {
            "beep","true"
          }
        });

        // Add Speak XML Tag
        resp.AddSpeak("Hello", new Dictionary < string, string > () {});

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
Basic Wait XML
<Response>
    <Speak>I will wait for 10 seconds</Speak>
    <Wait length="10" />
    <Speak>I just waited 10 seconds</Speak>
</Response>

Delayed Wait XML
<Response>
    <Wait length="10" />
    <Speak>Hello</Speak>
</Response>

Beep Detection XML
<Response>
    <Wait length="10" beep="true" />
    <Speak>Hello</Speak>
</Response>
*/    
