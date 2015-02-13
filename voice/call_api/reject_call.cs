using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace hangup
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/hangup"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Add Speak XML Tag
                resp.AddSpeak("This call will be hung up in 1 minute", new Dictionary<string, string>(){});

                // Add Hangup XML Tag
                resp.AddHangup(new Dictionary<string, string>() 
                {
                    {"reason","busy"}, // Specify the reason for hangup
                    {"schedule","60"} // Schedule the hangup
                });    

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
    <Speak>This call will be hung up in 1 minute</Speak>
    <Hangup reason="busy" schedule="60"/>
</Response>
*/