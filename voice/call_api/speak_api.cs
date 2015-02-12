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
            Get["/speak_api"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();
                
                String getdigits_action_url = "http://dotnettest.apphb.com/speak_action";
                GetDigits gd = new GetDigits("",new Dictionary<string, string>() 
                {
                    {"action",getdigits_action_url}, // The URL to which the digits are sent. 
                    {"method","GET"}, // Submit to action URL using GET or POST.
                    {"timeout","7"}, // Time in seconds to wait to receive the first digit. 
                    {"retries","1"}, // Indicates the number of retries the user is allowed to input the digits,
                    {"redirect","false"} // Redirect to action URL if true. If false,only request the URL and continue to next element.
                });

                gd.AddSpeak("Press 1 to listen to a message",new Dictionary<string,string>());
                resp.Add(gd);
                resp.AddWait(new Dictionary<string, string>() 
                {
                    {"length","10"}
                });

                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            Get["/speak_action"] = x =>
            {
                String digits = Request.Query["Digits"];
                String uuid = Request.Query["CallUUID"];
                Debug.WriteLine("Digit pressed : {0}, Call UUID : {1}",digits, uuid );

                RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN";

                IRestResponse<GenericResponse> resp = plivo.speak(new Dictionary<string, string>() 
                {
                    { "call_uuid", uuid }, // ID of the call
                    { "text", "Hello, from Speak API" }, // Text to be played.
                    { "voice", "WOMAN" }, // The voice to be used, can be MAN,WOMAN. Defaults to WOMAN.
                    {"language","en-GB"} // The language to be used
                });

                Debug.WriteLine(resp.Content);
                return "Speak API";
            };
        }
    }
}

/*
Sample output
<Response>
    <GetDigits action="http://dotnettest.apphb.com/speak_action" method="GET" timeout="7" retries="1" redirect="false">
        <Speak>Press 1 to listen to a message</Speak>
    </GetDigits>
    <Wait length="10"/>
</Response>

Digit pressed : 1, Call UUID : c726ca3c-b29f-11e4-89d2-31aa0d5480e9

{
    "api_id": "cdabcd44-b29f-11e4-9107-22000afaaa90",
    "message": "speak started"
}
*/