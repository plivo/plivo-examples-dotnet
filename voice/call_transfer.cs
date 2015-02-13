using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace call_transfer
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/call_transfer"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Add speak XML tag
                resp.AddSpeak("Please wait while your call is being transferred", new Dictionary<string, string>() { });

                // Add Redirect XML tag
                resp.AddRedirect("http://dotnettest.apphb.com/connect", new Dictionary<string, string>() { });

                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            Get["/connect"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Generate Dial XML
                Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>() 
                {
                    {"action","http://dotnettest.apphb.com/dial_status"}, // Redirect to this URL after leaving Dial. 
                    {"method","GET"}, // Submit to action URL using GET or POST.
                    {"redirect", "true"} // If set to false, do not redirect to action URL. We expect an XML from the action URL if this parameter is set to true. 
                });
                dial.AddNumber("1111111111", new Dictionary<string, string>() { });
                
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
Sample output for Redirect XML
<Response>
    <Speak>Please wait while you call is being transferred</Speak>
    <Redirect>https://morning-ocean-4669.herokuapp.com/connect/</Redirect>
</Response>

Sample output for Dial XML
<Response>
    <Speak>Connecting your call..</Speak>
    <Dial action="https://morning-ocean-4669.herokuapp.com/dial_status/" method="GET" redirect="true">
        <Number>1111111111</Number>
    </Dial>
</Response>
*/