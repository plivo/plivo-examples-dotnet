using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace legs_rec
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/answer_incoming"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                resp.AddRecord(new Dictionary<string, string>()
                {
                    {"action","http://dotnettest.apphb.com/record_action"}, // Submit the result of the record to this URL.
                    {"method","GET"}, // Submit to action url using GET or POST
                    {"redirect","false"}, // If false, don't redirect to action url, only request the url and continue to next element.
                    {"recordSession","true"} // Record current call session in background 
                });

                resp.AddWait(new Dictionary<string, string>()
                {
                    {"length", "10"} // Time to wait in seconds
                });

                Dial dial = new Dial(new Dictionary<string, string>()
                {
                    {"callbackUrl",""}, // URL that is notified by Plivo when one of the following events occur : 
                                        // called party is bridged with caller, called party hangs up, caller has pressed any digit
                    {"callbackMethod","GET"} // Method used to notify callbackUrl.
                });

                dial.AddNumber("1111111111", new Dictionary<string, string>() { });
                resp.Add(dial);

                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };
            
            // The Callback URL of Dial will make a request to the Record API which will record only the B Leg
            // Play API is invoked which will play a music only on the B Leg.
            Get["dial_outbound"] = x =>
            {
                string events = Request.Query["Event"];
                string call_uuid = Request.Query["CallUUID"];
                Debug.WriteLine("Event : " + events);
                Debug.WriteLine("Call UUID : " + call_uuid);

                if (events == "DialAnswer")
                {
                    string auth_id = "Your AUTH_ID";
                    string auth_token = "Your AUTH_TOKEN";

                    RestAPI plivo = new RestAPI(auth_id, auth_token);
                    IRestResponse<Plivo.API.Record> resp = plivo.record(new Dictionary<string, string>()
                    {
                        {"call_uuid",call_uuid}, // ID of the call
                        {"callback_url","http://dotnettest.apphb.com/record_callback"}, // The URL invoked by the API when the recording ends.
                        {"callback_method","GET"} // The method which is used to invoke the callback_url URL. Defaults to POST.
                    });

                    Debug.WriteLine(resp.Content);

                    RestAPI plivo1 = new RestAPI(auth_id, auth_token);
                    IRestResponse<GenericResponse> resp1 = plivo1.play(new Dictionary<string, string>()
                    {
                        {"call_uuid",call_uuid}, // ID of the call
                        {"url","https://s3.amazonaws.com/plivocloud/Trumpet.mp3"} // A single URL or a list of comma separated URLs pointing to an mp3 or wav file.
                    });

                    Debug.WriteLine(resp1.Content);
                }    
                else
                {
                    Debug.WriteLine("Invalid");                        
                }

                return "OK";
            };

            // The Callback URL of record api will return the B Leg record details.
            Get["/record_callback"] = x =>
            {
                String record_url = Request.Query["RecordUrl"];
                String record_duration = Request.Query["RecordingDuration"];
                String record_id = Request.Query["RecordingID"];

                Debug.WriteLine("Record URL : {0}, Recording Duration : {1}, Record ID : {2}", record_url, record_duration, record_id);
                return "Done";
            };
        }
    }
}

/*
Sample Output
<Response>
    <Record action="http://dotnettest.apphb.com/record_action" method="GET" redirect="false" recordSession="true"/>
    <Wait length="10"/>
    <Dial callbackUrl="" callbackMethod="GET">
        <Number>1111111111</Number>
    </Dial>
</Response>

Event : DialAnswer
Call UUID : b09d7ac6-bcd7-11e4-bcba-71a3ade89e40

Output of the Record API request
{
    "api_id": "bb8242d2-bcd7-11e4-b423-22000ac8a2f8",
    "message": "async api spawned"
}

Output of the Play XML request
{
    "api_id": "bba3d1b8-bcd7-11e4-ac1f-22000ac51de6",
    "message": "play started"
}

Output of Record API Callback URL
Record URL : http://s3.amazonaws.com/recordings_2013/11112222-4444-11e4-a4c8-782bcb5bb8af.mp3
Recording Duration : 22 
Recording ID : 693e61fd-8150-4091-a0f8-561d4a434288 

Output of Record XML Action URL
Record URL : http://s3.amazonaws.com/recordings_2013/55556666-7777-11e4-a4c8-782bcb5bb8af.mp3 
Recording Duration : 27 
Recording ID : daddbf04-9585-11e4-a4c8-782bcb5bb8af 

*/
