using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.API;

namespace record_api
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");

            // To record a call
            IRestResponse<Plivo.API.Record> resp = plivo.record(new Dictionary<string, string>() 
            {
                {"call_uuid", "xxxxxxxxxxx" } // ID of the call
                {"time_limit","40"}, // Max recording duration in seconds
                {"callback_url","http://dotnettest.apphb.com/record_callback"}, // The URL invoked by the API when the recording ends
                {"callback_method","GET"}, // The method which is used to invoke the callback_url
                {"transcriptionType","auto"}, // The type of transcription required
                {"transcriptionUrl","http://dotnettest.apphb.com/transcription"}, // The URL where the transcription while be sent from Plivo
                {"transcriptionMethod","GET"}, // The method used to invoke transcriptionUrl 

            });

            Debug.WriteLine(resp.Content);

            // To stop recording a call
            IRestResponse<Plivo.API.Record> resp = plivo.stop_record(new Dictionary<string, string>() 
            {
                { "call_uuid", "xxxxxxxxxxx" } // ID of the call
            });

            Debug.WriteLine(resp.Content);

            // To record a conference call
            IRestResponse<Plivo.API.Record> resp = plivo.record_conference(new Dictionary<string, string>() 
            {
                {"conference_name", "demo" } // The conference name
                {"callback_url","http://dotnettest.apphb.com/record_callback"}, // The URL invoked by the API when the recording ends
                {"callback_method","GET"}, // The method which is used to invoke the callback_url
            });

            Debug.WriteLine(resp.Content);

            // To stop recording a conference
            IRestResponse<Plivo.API.Record> resp = plivo.stop_record_conference(new Dictionary<string, string>() 
            {
                {"conference_name", "demo" } // The conference name
            });

            Debug.WriteLine(resp.Content);

            Console.ReadLine();
        }
    }
}