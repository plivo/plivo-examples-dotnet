using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.Utilities;
using Nancy;

namespace validateSignature {
  public class Program: NancyModule {
    public Program() {
      // Fetch headers
      Get["/receive_sms/"] = x =>{
        IEnumerable < string > signature = Request.Headers["X-Plivo-Signature-V2"];
        String[] sign = (String[]) signature;
        String actualsignature = sign[0];

        IEnumerable < string > nonce = Request.Headers["X-Plivo-Signature-V2-Nonce"];
        String[] key = (String[]) nonce;
        String actualnonce = key[0];

        String auth_token = "YOUR_AUTH_TOKEN";
        String url = Request.Url.SiteBase + Request.Url.Path;

        // Validate the Signatures received from the headers
        bool valid = Plivo.Utilities.XPlivoSignatureV2.VerifySignature(url, actualnonce, actualsignature, auth_token);
        Debug.WriteLine("Valid : " + valid);

        String from_number = Request.Query["From"];
        String to_number = Request.Query["To"];
        String text = Request.Query["Text"];

        // Prints message details
        Console.WriteLine("From : {0}, To : {1}, Text : {2}", from_number, to_number, text);
        return "OK";
      };
    }
  }
}