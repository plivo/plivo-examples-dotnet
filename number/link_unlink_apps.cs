using System;
using System.Collections.Generic;
using Plivo;

namespace link_unlink_apps {
  class Program {
    static void Main(string[] args) {

      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Link an application to a phone number
      var response = api.Number.Update(
      app_id: "Test app", number: "17609915566");

      //Prints the response
      Console.WriteLine(response);

      // Unlink an application from a phone number            
      var response = api.Application.Update(
      appId: "", );

      //Prints the response
      Console.WriteLine(response);

    }
  }
}

/*
Sample Output
Link an application to a phone number 
{
  "api_id": "4b48212c-b69b-11e4-ac1f-22000ac51de6",
  "message": "changed"
}

Unlink an application from a phone number   
{
  "api_id": "19247720-b69d-11e4-b423-22000ac8a2f8",
  "message": "changed"
}
*/
