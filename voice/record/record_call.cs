using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace record_call
{
    public class Program : NancyModule
    {
        public Program()
        {
            // Generate a Record XML
            Get["/record"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();
                resp.AddRecord(new Dictionary<string, string>()
                {
                    {"action","http://dotnettest.apphb.com/record_action"}, // Submit the result of the record to this URL
                    {"method","GET"}, // HTTP method to submit the action URL
                    {"callbackUrl", "http://dotnettest.apphb.com/record_callback"}, // If set, this URL is fired in background when the recorded file is ready to be used.
                    {"callbackMethod","GET"} // Method used to notify the callbackUrl.
                });

                Debug.WriteLine(resp.ToString());
                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };

            // Action URL Example
            Get["/record_action"] = x =>
            {
                String record_url = Request.Query["RecordUrl"];
                String record_duration = Request.Query["RecordingDuration"];
                String record_id = Request.Query["RecordingID"];

                Debug.WriteLine("Record URL : {0}, Recording Duration : {1}, Record ID : {2}", record_url, record_duration, record_id);
                return "Done";
            };

            // Callback URL Example
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
Sample output
<Response>
    <Record action="http://dotnettest.apphb.com/record_action" method="GET" callbackUrl="http://dotnettest.apphb.com/record_callback" callbackMethod="GET"/>
</Response>

Sample output for Action URL
Record URL : https://s3.amazonaws.com/recordings_2013/03e89771-bcd3-11e4-9dc1-842b2b096c5d.mp3, Recording Duration : 23, Record ID : 03e89770-bcd2-11e4-9dc1-842b2b096c5d

Sample output for Callback URL
Record URL : https://s3.amazonaws.com/recordings_2013/03e89771-bcd3-11e4-9dc1-842b2b096c5d.mp3, Recording Duration : 23, Record ID : 03e89770-bcd2-11e4-9dc1-842b2b096c5d
*/