using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace receive_call
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/speak"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Add Speak XML Tag
                resp.AddSpeak("Hello, Welcome to Plivo", new Dictionary<string, string>() { });
                
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
    <Speak>Hello, Welcome to Plivo.</Speak>
</Response>
*/