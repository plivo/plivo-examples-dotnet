using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace call_whisper
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/call_whisper"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Generate DIal XML
                Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>() 
                {
                    {"confirmSound","http://dotnettest.apphb.com/confirm_sound"}, // A remote URL fetched with POST HTTP request which must return an 
                                                                                  // XML response with Play, Wait and/or Speak elements only.
                    {"confirmKey","5"} // The digit to be pressed by the called party to accept the call.
                });
                dial.AddNumber("1111111111", new Dictionary<string, string>() { });
                dial.AddNumber("2222222222", new Dictionary<string, string>() { });
                dial.AddNumber("3333333333", new Dictionary<string, string>() { });
                resp.Add(dial);
                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            Get["/confirm_sound"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                //Generate Speak XML
                resp.AddSpeak("Press 5 to answer the call", new Dictionary<string, string>() { });

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
    <Dial confirmKey="5" confirmSound="http://dotnettest.apphb.com/confirm_sound">
        <Number>1111111111</Number>
        <Number>2222222222</Number>
        <Number>3333333333</Number>
    </Dial>
</Response>

<Response>
    <Speak>Press 5 to answer the call</Speak>
</Response>
*/