using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace seq_dial
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/seq_dial"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Generate Dial XML
                Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>() 
                {
                    {"timeout", "20"}, // The duration (in seconds) for which the called party has to be given a ring.
                    {"action", "http://dotnettest.apphb.com/dial_status"} // Redirect to this URL after leaving Dial. 
                });
                dial.AddNumber("1111111111", new Dictionary<string, string>() { });
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
Sample output
<Response>
    <Dial action="http://dotnettest.apphb.com/dial_status" timeout="20">
       <Number>1111111111</Number>
    </Dial>
    <Dial>
       <Number>2222222222</Number>
    </Dial>
</Response>
*/