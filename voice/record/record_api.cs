using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.API;

namespace record_api {
  class Program {
    static void Main(string[] args) {
      // To record a call
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
      try {
        var response = api.Call.StartRecording(
          callUuid: "10c94053-73b4-46fe-b74a-12159d1d3d60", // ID of the call
          timeLimit: "60", // Max recording duration in seconds
          callbackUrl: "http://dotnettest.apphb.com/record_callback", // The URL invoked by the API when the recording ends.
          callbackMethod: "GET", // The method which is used to invoke the callback_url
          transcriptionType: "auto", // The type of transcription required
          transcriptionUrl: "http://dotnettest.apphb.com/transcription", // The URL where the transcription while be sent from Plivo
          transcriptionMethod: "GET" // The method used to invoke transcriptionUrl 

        );
        Console.WriteLine(response);
      } catch (PlivoRestException e) {
        Console.WriteLine("Exception: " + e.Message);
      }

      // To stop recording a call
      try {
        var response = api.Call.StopRecording(
          callUuid: "93b35e56-5b28-47fc-95aa-8536625d3ac1" // ID of the call.
        );
        Console.WriteLine(response);
      } catch (PlivoRestException e) {
        Console.WriteLine("Exception: " + e.Message);
      }

      // To record a conference call
      try {
        var response = api.Conference.StartRecording(
          "conf name", // Name of the conference
          callbackUrl: "http://dotnettest.apphb.com/record_callback", // The URL invoked by the API when the recording ends
          callbackMethod: "GET" // The method which is used to invoke the callback_url
        );
        Console.WriteLine(response);
      } catch (PlivoRestException e) {
        Console.WriteLine("Exception: " + e.Message);
      }

      // To stop recording a conference
      try {
        var response = api.Conference.StopRecording(
          "conf name" // Name of the conference
        );
        Console.WriteLine(response);
      } catch (PlivoRestException e) {
        Console.WriteLine("Exception: " + e.Message);
      }
    }
  }
}