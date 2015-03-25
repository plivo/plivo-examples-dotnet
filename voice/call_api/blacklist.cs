using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace blacklist
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/reject_caller"] = x =>
            {
                string[] blacklist = {"1111111111","2222222222"};
 
                String fromNumber = Request.Query["From"];
 
                Plivo.XML.Response resp = new Plivo.XML.Response();
 
                if (blacklist.Equals(fromNumber))
                {
                    resp.AddHangup(new Dictionary<string, string>()
                {
                    {"reason", "rejected"}
                });
                }
                else
                {
                    resp.AddSpeak("Hello, from Plivo!", new Dictionary<string, string>() { });
                }
 
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
Sample output when From number is in blacklist
<Response>
    <Hangup reason="rejected"/>
</Response>

Sample Output when From number is not in blacklist
<Response>
    <Speak>Hello from Plivo</Speak>
</Response>
*/