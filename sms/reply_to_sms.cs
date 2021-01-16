using Nancy;
using System;
using System.Collections.Generic;
using System.Reflection;
using Plivo.XML;

namespace Reply_To_Sms {
  public class Program: NancyModule {
    public Program() {
      Post("/reply_to_sms", parameters =>{
        String from_number = Request.Form["From"]; // Sender's phone number
        String to_number = Request.Form["To"]; // Receiver's phone number
        String text = Request.Form["Text"]; // The text which was received
        // Print the message
        Console.WriteLine("From : {0}, To : {1}, Text : {2}", from_number, to_number, text);

        // Generate the Message XML
        Plivo.XML.Response resp = new Plivo.XML.Response();
        resp.AddMessage(text, new Dictionary < string, string > () {
          {"src",to_number},// Sender's phone number
          {"dst",from_number},// receiver's phone number
          {"type","sms"},
          {"callbackUrl","http://foo.com/sms_status/"},
          {"callbackMethod","POST"}
        });

        // Print the XML
        Console.WriteLine(resp.ToString());

        // Return the XML
        var output = resp.ToString();
        var res = (Nancy.Response) output;
        res.ContentType = "application/xml";
        return res;
      };
    }
  }
}

// Sample output
// From : 2222222222, To : 1111111111, Text : Hi, from Plivo
/*
<Response>
   <Message dst="2222222222" src="1111111111">Thank you for your message</Message>
</Response>
*/
