using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace conference
{
    public class Program : NancyModule
    {
        public Program()
        {
            Get["/response/conference"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();

                //  Add Speak XML Tag
                resp.AddSpeak("You will now be placed into a demo conference. This is brought to you by Plivo. To know more, visit us at Plivo.com", new Dictionary<string, string>() { });

                // Add Conference XML Tag
                resp.AddConference("demo",new Dictionary<string,string>()
                {
                    {"enterSound","beep:2"}, // Used to play a sound when a member enters the conference
                    {"record","true"}, // Option to record the call
                    {"action","http://dotnettest.apphb.com/response/conf_action"}, // URL to which the API can send back parameters
                    {"method","GET"}, // method to invoke the action Url
                    {"callbackUrl","http://dotnettest.apphb.com/response/conf_callback"}, // If specified, information is sent back to this URL
                    {"callbackMethod","GET"}, // Method used to notify callbackUrl
                });

                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            // Action URL Example
            Get["/response/conf_action"] = x =>
            {
                String conf_name = Request.Query["ConferenceName"];
                String conf_uuid = Request.Query["ConferenceUUID"];
                String conf_mem_id = Request.Query["ConferenceMemberID"];
                String record_url = Request.Query["RecordUrl"];
                String record_id = Request.Query["RecordingID"];

                Debug.WriteLine("Conference Name : {0}, Conference UUID : {1}, Conference Member ID : {2}, Record URL : {3}, Record ID : {4}", conf_name, conf_uuid, conf_mem_id, record_url, record_id);
                return "Done";
            };

            //  Callback URL Example
            Get["/response/conf_callback"] = x =>
            {
                String conf_action = Request.Query["ConferenceAction"];
                String conf_name = Request.Query["ConferenceName"];
                String conf_uuid = Request.Query["ConferenceUUID"];
                String conf_mem_id = Request.Query["ConferenceMemberID"];
                String call_uuid = Request.Query["CallUUID"];
                String record_url = Request.Query["RecordUrl"];
                String record_id = Request.Query["RecordingID"];

                Debug.WriteLine("Conference Action : {0}, Conference Name : {1}, Conference UUID : {2}, Conference Member ID : {3}, Call UUID : {4}, Record URL : {5}, Record ID : {6}", conf_action, conf_name, conf_uuid, conf_mem_id, call_uuid, record_url, record_id);
                return "Done";
            };
        }
    }
}

/*
Sample Output
<Response>
    <Speak>
        You will now be placed into a demo conference. This is brought to you by Plivo. To know more, visit us at Plivo.com
    </Speak>
    <Conference enterSound="beep:2" record="true" action="http://dotnettest.apphb.com/response/conf_action" method="GET" callbackUrl="http://dotnettest.apphb.com/response/conf_callback" callbackMethod="GET">demo</Conference>
</Response>

Conference Name : demo, Conference UUID : 23b1caa6-b5af-11e4-b696-377ffe01233f, Conference Member ID : 1486, Record URL : http://s3.amazonaws.com/recordings_2013/237254f5-b5af-11e4-a664-0026b945b52b.mp3, Record ID : 7213cd8e-b5af-11e4-a664-0026b945b52b

Conference Action : enter, Conference Name : demo, Conference UUID : 23b1caa6-b5af-11e4-b696-377ffe01233f, Conference Member ID : 1486, Call UUID : 644b143c-b5af-11e4-bc93-c73b3246dc2a, Record URL : , Record ID :
Conference Action : exit, Conference Name : demo, Conference UUID : 23b1caa6-b5af-11e4-b696-377ffe01233f, Conference Member ID : 1486, Call UUID : 644b143c-b5af-11e4-bc93-c73b3246dc2a, Record URL : , Record ID :
Conference Action : record, Conference Name : demo, Conference UUID : 23b1caa6-b5af-11e4-b696-377ffe01233f, Conference Member ID : , Call UUID : , Record URL : http://s3.amazonaws.com/recordings_2013/237254f5-b5af-11e4-a664-0026b945b52b.mp3, Record ID : 237256e6-b5af-11e4-a664-0026b945b52b
*/