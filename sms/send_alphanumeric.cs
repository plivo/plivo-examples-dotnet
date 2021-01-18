using System;
using System.Collections.Generic;
using Plivo;

namespace Send_Alpha {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Send a Alpha source message
      var response = api.Message.Create(
      src: "ALPHA", // Alphanumeric sender ID
      dst: new List < String > {"2222222222"},
      // Receiver's phone number wiht country code
      text: "Hi, text from plivo", // Your SMS text message
      );

      // Prints the message details
      Console.Write(response);
    }
  }
}

// Sample output
/*{
  "api_id": "e53026ae-a0ca-11e4-a2d1-22000ac5040c",
  "message": "message(s) queued",
  "message_uuid": [
    "e5478308-a0ca-11e4-9bd8-22000afa12b9"
  ]
}
*/