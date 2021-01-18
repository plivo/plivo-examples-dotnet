using System;
using System.Collections.Generic;
using Plivo;

namespace rent_unrent_numbers {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Search for a new phone number
      var response = api.Phonenumber.List(
      countryIso: "GB", // The ISO code A2 of the country
      type: "local", // The type of number you are looking for. The possible number types are local, national and tollfree.
      pattern: "210", // Represents the pattern of the number to be searched.
      region: "Texas" // This filter is only applicable when the number_type is local. Region based filtering can be performed.
      );

      // Prints the response
      Console.WriteLine(response);

      // Buy a new phone number
      var response = api.Phonenumber.Buy(
      number: "10123456789");

      // Prints the response
      Console.WriteLine(response);

      // Modify a number
      var response = api.Number.Update(
      alias: "Updated Alias", // The textual name given to the number
      number: "12109206499" // The phone number that has to be rented
      );

      //Prints the response
      Console.WriteLine(response);

      // Unrent a number
      var response = api.Number.Delete(
      number: "17609915566" // Number that has to be unrented
      );

      // Prints the response
      Console.WriteLine(response);
    }
  }
}

/*
Sample Output
Search for a new phone number
{
  "api_id": "75f20322-b694-11e4-8ccf-22000afb14f7",
  "meta": {
    "limit": 20,
    "next": "/v1/Account/XXXXXXXXXXXX/PhoneNumber/?limit=20&country_iso=US&pattern=210&region=Texas&offset=20&type=local",
    "offset": 0,
    "previous": null,
    "total_count": 98
  },
  "objects": [
    {
      "country": "UNITED STATES",
      "lata": 566,
      "monthly_rental_rate": "0.80000",
      "number": "12109206500",
      "prefix": "210",
      "rate_center": "SANANTONIO",
      "region": "Texas, UNITED STATES",
      "resource_uri": "/v1/Account/XXXXXXXXXXXX/PhoneNumber/12109206500/",
      "restriction": null,
      "restriction_text": null,
      "setup_rate": "0.00000",
      "sms_enabled": true,
      "sms_rate": "0.00000",
      "type": "fixed",
      "voice_enabled": true,
      "voice_rate": "0.00850"
    },
    {
      "country": "UNITED STATES",
      "lata": 566,
      "monthly_rental_rate": "0.80000",
      "number": "12109206501",
      "prefix": "210",
      "rate_center": "SANANTONIO",
      "region": "Texas, UNITED STATES",
      "resource_uri": "/v1/Account/XXXXXXXXXXXX/PhoneNumber/12109206501/",
      "restriction": null,
      "restriction_text": null,
      "setup_rate": "0.00000",
      "sms_enabled": true,
      "sms_rate": "0.00000",
      "type": "fixed",
      "voice_enabled": true,
      "voice_rate": "0.00850"
    }
  ]
}

Buy a new phone number
{
  "api_id": "a81fb812-b694-11e4-b932-22000ac50fac",
  "message": "created",
  "numbers": [
    {
      "number": "10123456789",
      "status": "Success"
    }
  ],
  "status": "fulfilled"
}

Modify a Number
{
  "api_id": "46dcfbca-2ee7-11e5-a151-feff819cdc9f",
  "message": "changed"
} 

Unrent a number
{
  "api_id": "e7cf5c74-b699-11e4-b423-22000ac8a2f8",
  "error": "not found"
}  
 
*/