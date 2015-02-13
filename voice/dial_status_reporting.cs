using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace dial_status_reporting
{
    public class Program : NancyModule
    {
        public Program()
        {

            Get["/dial"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Generate Dial XML
                Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>() 
                {
                    {"action","http://dotnettest.apphb.com/dial_status"}, // Redirect to this URL after leaving Dial.
                    {"method","GET"} // Submit to action URL using GET or POST.
                });
                dial.AddNumber("1111111111", new Dictionary<string, string>() { });
                
                resp.Add(dial);
                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            Get["/dial_status"] = x =>
            {
                // After completion of the call, Plivo will report back the status to the action URL in the Dial XML.
                String status = Request.Query["DialStatus"];
                String aleg = Request.Form["DialALegUUID"];
                String bleg = Request.Form["DialBLegUUID"];

                Debug.WriteLine("Status : {0}, ALeg UUID : {1}, BLeg UUID : {2}", status, aleg, bleg);
                return "OK";
            };
        }
    }
}

/*
Sample Output
<Response>
    <Dial action="http://dotnettest.apphb.com/dial_status" method="GET">
        <Number>1111111111</Number>
    </Dial>
</Response>
Status : completed, ALeg Uuid : 52bb0058-902d-11e4-9681-2d7d49a323a0, BLeg Uuid : 54f84290-902d-11e4-96df-2d7d49a323a0
*/
