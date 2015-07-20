using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Plivo.API;

namespace rent_unrent_numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");

            // Search for a new phone number
            IRestResponse<PhoneNumberList> resp = plivo.search_phone_numbers(new Dictionary<string,string>()
            {
                {"country_iso","US"}, // The ISO code A2 of the country
                {"type","local"}, // The type of number you are looking for. The possible number types are local, national and tollfree.
                {"pattern","210"}, // Represents the pattern of the number to be searched.
                {"region","Texas"} // This filter is only applicable when the number_type is local. Region based filtering can be performed.
            });

            Console.WriteLine(resp.Content);
            Debug.WriteLine(resp.Content);
            
            // Buy a new phone number
            IRestResponse<PhoneNumberResponse> res = plivo.buy_phone_number(new Dictionary<string, string>()
            {
                {"number","12109206499"} // The phone number that has to be rented
            });

            Console.WriteLine(res.Content);
            Debug.WriteLine(res.Content);

            //Modify a number
            IRestResponse<PhoneNumberResponse> res = plivo.modify_number(new Dictionary<string, string>()
            {
                {"number","12109206499"}, // The phone number that has to be rented
                {"alias","Test"}, //The textual name given to the number
                {"subaccount","Your SUB_AUTH_ID"} //The auth_id of the subaccount to which this number should be added.
            });

            Console.WriteLine(res.Content);
            Debug.WriteLine(res.Content);            
            
            // Unrent a number
            IRestResponse<GenericResponse> response = plivo.unrent_number(new Dictionary<string, string>()
            {
                {"number","12109206499"} // Number that has to be unrented
            });

            Console.WriteLine(response.Content);
            Debug.WriteLine(response.Content);
            
            Console.ReadLine();
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
      "number": "12109206499",
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