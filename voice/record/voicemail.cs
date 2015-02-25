using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.XML;
using Nancy;

namespace voicemail
{
    public class Program : NancyModule
    {
        public Program()
        {
            // Generate a Record XML and ask the caller to leave a message.
            // The recorded file will be sent to the 'action' URL.
            Get["/voicemail"] = x =>
            {
                Plivo.XML.Response resp = new Plivo.XML.Response();
                resp.AddSpeak("Please leave your message after the beep", new Dictionary<string, string>() { });
                resp.AddRecord(new Dictionary<string, string>()
                {
                    {"action","http://dotnettest.apphb.com/save_record_url"}, // Submit the result of the record to this URL
                    {"method","GET"}, // HTTP method to submit the action URL
                    {"maxLength","30"}, // Maximum number of seconds to record 
                    {"transcriptionType","auto"}, // The type of transcription required
                    {"transcriptionUrl", "http://dotnettest.apphb.com/transcription"}, // The URL where the transcription while be sent from Plivo
                    {"transcriptionMethod","GET"} // The method used to invoke transcriptionUrl
                });

                Debug.WriteLine(resp.ToString());

                var output = resp.ToString();
                var res = (Nancy.Response)output;
                res.ContentType = "text/xml";
                return res;
            };
            
            // Action URL Example
            Get["/save_record_url"] = x =>
            {
                String record_url = Request.Query["RecordUrl"];
                Debug.WriteLine("Recording URL : {0}", record_url);
                return "OK";
            };
            
            // Transcription URL Example
            Get["/transcription"] = x =>
            {
                String transcription = Request.Query["transcription"];
                Debug.WriteLine("Transcription : {0}", transcription);
                return "OK";
            };
        }
    }
}

/*
Sample Output
<Response>
    <Speak>Please leave your message after the beep</Speak>
    <Record action="http://dotnettest.apphb.com/save_record_url" method="GET" maxLength="30" transcriptionType="auto" 
        transcriptionUrl="http://dotnettest.apphb.com/transcription" transcriptionMethod="GET"/>
</Response>

Recording URL : https://s3.amazonaws.com/recordings_2013/a2150b54-bccd-11e4-b666-842b2b4d61df.mp3

Transcription is : Hello
*/