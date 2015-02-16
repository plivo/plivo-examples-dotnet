using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace custom_tone
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
                    {"dialMusic","http://dotnettest.apphb.com/custom_tone"} // Music to be played to the caller while the call is being connected.
                });
                dial.AddNumber("1111111111", new Dictionary<string, string>() { });
                
                resp.Add(dial);
                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            Get["/custom_tone"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();
             
                // Add Play XML Tags
                resp.AddPlay("https://s3.amazonaws.com/plivocloud/music.mp3", new Dictionary<string, string>() { });

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
    <Dial dialMusic="https://morning-ocean-4669.herokuapp.com/custom_tone/">
        <Number>1111111111</Number>
    </Dial>
</Response> 

<Response>
   <Play>https://s3.amazonaws.com/plivocloud/music.mp3</Play>
</Response>
*/