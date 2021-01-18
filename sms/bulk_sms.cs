using System;
using System.Collections.Generic;
using Plivo;

namespace Bulk_Sms {
  class Program {
    static void Main(string[] args) {

      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Send Bulk SMS
      var response = api.Message.Create(
      src: "1111111111", // Sender's phone number with country code
      dst: new List < String > {
        "2222222222<3333333333"
      },
      // Receiver's phone number wiht country code
      text: "Hello, this is sample text", // Your SMS text message
      url: "http://foo.com/sms_status/");

      // Prints the message details
      Console.Write(response);

      // Loop through the message_uuid
      int count = response.MessageUuid.Count;
      for (int i = 0; i < count; i++) {
        Console.WriteLine("Message UUID : {0}", response.MessageUuid[i]);
      }

      // Print the api_id
      Console.WriteLine("Api ID : {0}", response.ApiId);

      // When an invalid number is given as a dst parameter, an error will be thrown and the message will not be sent 
      var response = api.Message.Create(
      src: "1111111111", // Sender's phone number with country code
      dst: new List < String > {
        "222222<3333333333"
      },
      // Receiver's phone number wiht country code
      text: "Hello, this is sample text", // Your SMS text message
      url: "http://foo.com/sms_status/");

      // Prints the message details
      Console.Write(response);

    }
  }
}

// Sample output
/*
{
  "api_id": "a6d93290-a0b3-11e4-96e3-22000abcb9af",
  "message": "message(s) queued",
  "message_uuid": [
    "a6f4f17e-a0b3-11e4-89de-22000ae885b8",
    "a6f4eeae-a0b3-11e4-9bd8-22000afa12b9"
  ]
}
Message UUID : a6f4f17e-a0b3-11e4-89de-22000ae885b8
Message UUID : a6f4eeae-a0b3-11e4-9bd8-22000afa12b9
Api ID : a6d93290-a0b3-11e4-96e3-22000abcb9af
*/

// Sample Output for an invalid number
/*
{
  "api_id": "e5f6c53c-a0b3-11e4-a2d1-22000ac5040c",
  "error": "222222 is not a valid phone number"
}
*/