using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace record_using_api
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/record_api"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();
                String getdigits_action_url = "http://dotnettest.apphb.com/recording_action";
                GetDigits gd = new GetDigits("", new Dictionary<string, string>() 
                {
                    {"action",getdigits_action_url}, // The URL to which the digits are sent
                    {"method","GET"}, // Submit to action URL using GET or POST.
                    {"timeout","7"}, // Time in seconds to wait to receive the first digit.
                    {"numDigitd","1"}, // Maximum number of digits to be processed in the current operation.
                    {"retries","1"}, // Indicates the number of retries the user is allowed to input the digits
                    {"redirect","false"} // Redirect to action URL if true. If false,only request the URL and continue to next element.
                });

                gd.AddSpeak("Press 1 to record this call", new Dictionary<string, string>());
                resp.Add(gd);
                resp.AddWait(new Dictionary<string, string>() 
                {
                    {"length","10"} // Time to wait in seconds
                });

                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            Get["/recording_action"] = x =>
            {
                String digits = Request.Query["Digits"];
                String uuid = Request.Query["CallUUID"];
                Debug.WriteLine("Digit pressed : {0}, Call UUID : {1}", digits, uuid);

                if (digits == "1")
                {
                    string auth_id = "Your AUTH_ID";
                    string auth_token = "Your AUTH_TOKEN";

                    RestAPI plivo = new RestAPI(auth_id, auth_token);

                    IRestResponse<Plivo.API.Record> resp = plivo.record(new Dictionary<string, string>() 
                    {
                        { "call_uuid", uuid } // ID of the call
                    });

                    Debug.WriteLine(resp.Content);
                }
                else
                {
                    Debug.WriteLine("Invalid");
                }

                return "OK";
            };
        }
    }
}

/*
Sample Output
<Response>
    <GetDigits action="http://dotnettest.apphb.com/recording_action" method="GET" timeout="7" numDigits="1" retries="1" redirect="false">
        <Speak>Press 1 to record this call</Speak>
    </GetDigits>
    <Wait length="10"/>
</Response>

Digit pressed : 1, Call UUID : f528af7e-bccf-11e4-8ddf-25874237f7ce

{
    "api_id": "fbab15b2-bccf-11e4-9107-22000afaaa90",
    "message": "call recording started",
    "recording_id": "fbb95528-bccf-11e4-b655-842b2b4d61df",
    "url": "https://s3.amazonaws.com/recordings_2013/fbb95528-bccf-11e4-b655-842b2b4d61df.mp3"
}
*/