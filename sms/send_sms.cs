using System;
using System.Collections.Generic;
using Plivo;

namespace Send_Sms {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Send a message
      var response = api.Message.Create(
      src: "1111111111", // Sender's phone number with country code
      dst: new List < String > {"2222222222"}, // Receiver's phone number wiht country code
      text: "Hi, text from plivo", // Your SMS text message
      );

      // Prints the message details
      Console.Write(response);

      // Print the message_uuid
      Console.WriteLine(response.MessageUuid[0]);

      // Print the api_id
      Console.WriteLine(response.ApiId);
    }
  }
}

// Sample Output
/*
{
  "api_id": "e8f04d94-a0ae-11e4-b423-22000ac8a2f8",
  "message": "message(s) queued",
  "message_uuid": [
    "e908608c-a0ae-11e4-89de-22000ae885b8"
  ]
}
Message UUID : e908608c-a0ae-11e4-89de-22000ae885b8
Api ID : e8f04d94-a0ae-11e4-b423-22000ac8a2f8
*/