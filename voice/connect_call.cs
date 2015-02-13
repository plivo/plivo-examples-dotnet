using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace connect_call
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/connect"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Add Speak XML Tag
                resp.AddSpeak("Connecting your call..", new Dictionary<string, string>() { });

                // Add Dial XML Tag
                Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>() { });
                dial.AddNumber("2222222222", new Dictionary<string, string>() { });
                
                resp.Add(dial);
                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };
        }
    }
}

/*
Sample Output
<Response>
   <Speak>Connecting your call..</Speak>
   <Dial>
       <Number>2222222222</Number>
   </Dial>
</Response>
*/