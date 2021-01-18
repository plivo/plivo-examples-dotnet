using System;
using System.Collections.Generic;
using Plivo;

namespace number_detils {
  class Program {
    static void Main(string[] args) {

      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Get all numbers
      var response = api.Number.List(
      limit: 5, offset: 0);

      //Prints the response
      Console.WriteLine(response);

      // Get a particular number
      var response = api.Number.Get(
      number: "1111111111");

      //Prints the response
      Console.WriteLine(response);

    }
  }
}

/*
Sample Output
Get all numbers
{
  "api_id": "16840d6a-b692-11e4-af95-22000ac54c79",
  "meta": {
    "limit": 20,
    "next": null,
    "offset": 0,
    "previous": null,
    "total_count": 2
  },
  "objects": [
    {
      "active": true,
      "added_on": "2014-12-04",
      "alias": "",
      "application": null,
      "carrier": "Plivo",
      "monthly_rental_rate": "0.80000",
      "number": "1111111111",
      "number_type": "local",
      "region": "UNITED KINGDOM",
      "resource_uri": "/v1/Account/XXXXXXXXXXXX/Number/1111111111/",
      "sms_enabled": true,
      "sms_rate": "0.00000",
      "sub_account": null,
      "type": "local",
      "voice_enabled": true,
      "voice_rate": "0.00500"
    },
    {
      "active": true,
      "added_on": "2014-10-28",
      "alias": "",
      "application": "/v1/Account/XXXXXXXXXXXX/Application/2646926115442110
      "carrier": "Plivo",
      "monthly_rental_rate": "0.80000",
      "number": "2222222222",
      "number_type": "local",
      "region": "California, UNITED STATES",
      "resource_uri": "/v1/Account/XXXXXXXXXXXX/Number/2222222222/",
      "sms_enabled": true,
      "sms_rate": "0.00000",
      "sub_account": null,
      "type": "local",
      "voice_enabled": true,
      "voice_rate": "0.00850"
    }
  ]
}

Get a particular number
{
  "active": true,
  "added_on": "2014-12-04",
  "alias": "",
  "api_id": "c724a88c-b692-11e4-9107-22000afaaa90",
  "application": null,
  "carrier": "Plivo",
  "monthly_rental_rate": "0.80000",
  "number": "1111111111",
  "number_type": "local",
  "region": "UNITED KINGDOM",
  "resource_uri": "/v1/Account/XXXXXXXXXXXX/Number/1111111111/",
  "sms_enabled": true,
  "sms_rate": "0.00000",
  "sub_account": null,
  "type": "local",
  "voice_enabled": true,
  "voice_rate": "0.00500"
}
*/
