using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace play_recorded
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/play"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                // Add Play tag
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
   <Play>https://s3.amazonaws.com/plivocloud/Trumpet.mp3</Play>
</Response>
*/