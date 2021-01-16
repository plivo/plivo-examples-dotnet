using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace make_calls {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
      try {
        var response = api.Call.Delete(
          callUuid: "10c94053-73b4-46fe-b74a-12159d1d3d60" //UUID of the call
        );
        Console.WriteLine(response);
      } catch (PlivoRestException e) {
        Console.WriteLine("Exception: " + e.Message);
      }
    }
  }
}

/*
Sucecssful Output
" "

Unsuccessful Output
{
  "api_id": "be4512a4-b5cc-11e4-9107-22000afaaa90",
  "error": "not found"
}
*/