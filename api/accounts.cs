using System;
using System.Collections.Generic;
using Plivo;

namespace accounts {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Get Account details 
      var response = api.Account.Get();

      //Prints the response
      Console.WriteLine(response);

      /*
            Sample Output
            {
              "account_type": "standard",
              "address": "Sample address",
              "api_id": "56aed210-b5c2-11e4-b932-22000ac50fac",
              "auth_id": "XXXXXXXXXXXXXXXXX",
              "auto_recharge": false,
              "billing_mode": "prepaid",
              "cash_credits": "77.39415",
              "city": "Test City",
              "name": "Test",
              "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/",
              "state": "",
              "timezone": "Asia/Kolkata"
            } 
            */

      // Modify Account 
      var response = api.Account.Update(
      city: "Test city", name: "Test Account", address: "Test Address");

      //Prints the response
      Console.Write(response);

      /*
            Sample Output
            {
              "api_id": "a450e166-b5c2-11e4-b423-22000ac8a2f8",
              "message": "changed"
            }
            */

      // Create a sub account
      var response = api.Subaccount.Create(
      enabled: true, name: "Test Subaccount dotNet");

      //Prints the response
      Console.Write(response);

      /*
            Sample Output 
            {
              "api_id": "e4868222-b5c2-11e4-ac1f-22000ac51de6",
              "auth_id": "SAOWQ0NJFKMTRKMTRMZT",
              "auth_token": "M2M4YzllOGNlOTJkOWY0YmI2MWFmMTI1YzAzYTY0",
              "message": "created"
            }  
            */

      // Modify a sub account
      var response = api.Subaccount.Update(
      id: "SAXXXXXXXXXXXXXXXXXX", // Subaccount_auth_id
      name: "Updated Subaccount Name");

      //Prints the response
      Console.Write(response);

      /*
            Sample Output
            {
              "api_id": "0fa7a6e8-b5c3-11e4-8ccf-22000afb14f7",
              "message": "changed"
            }
            */

      // Get details of all sub accounts
      var response = api.Subaccount.List(
      limit: 5, offset: 0);

      //Prints the response
      Console.Write(response);

      /*
            Sample Output
            {
              "api_id": "3f1fad62-b5c3-11e4-ac1f-22000ac51de6",
              "meta": {
                "limit": 20,
                "next": null,
                "offset": 0,
                "previous": null,
                "total_count": 3
              },
              "objects": [
                {
                  "account": "/v1/Account/XXXXXXXXXXXXXXXXX/",
                  "auth_id": "SAOWQ0NJFKMTRKMTRMZT",
                  "auth_token": "M2M4YzllOGNlOTJkOWY0YmI2MWFmMTI1YzAzYTY0",
                  "created": "2015-02-16",
                  "enabled": true,
                  "modified": "2015-02-16",
                  "name": "Testing_subaccount",
                  "new_auth_token": "M2M4YzllOGNlOTJkOWY0YmI2MWFmMTI1YzAzYTY0",
                  "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/Subaccount/SAOWQ0NJFKMTRKMTRMZT/"
                },
                {
                  "account": "/v1/Account/XXXXXXXXXXXXXXXXX/",
                  "auth_id": "SAYWJLYWI1MZU1MWY4YT",
                  "auth_token": "YzRiZTBmZjRlMjkxZGNhZWM2M2YyNWRlOTQ4YmZh",
                  "created": "2015-02-10",
                  "enabled": true,
                  "modified": null,
                  "name": "Testing2",
                  "new_auth_token": "YzRiZTBmZjRlMjkxZGNhZWM2M2YyNWRlOTQ4YmZh",
                  "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/Subaccount/SAYWJLYWI1MZU1MWY4YT/"
                },
                {
                  "account": "/v1/Account/XXXXXXXXXXXXXXXXX/",
                  "auth_id": "SAMWJKYJFHZTM2YWE4OW",
                  "auth_token": "MjI4YzBiMDQ4MWFjODkyYWNkMDY3NDViMDZjZGUz",
                  "created": "2014-12-04",
                  "enabled": true,
                  "modified": null,
                  "name": "Ramya",
                  "new_auth_token": "MjI4YzBiMDQ4MWFjODkyYWNkMDY3NDViMDZjZGUz",
                  "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/Subaccount/SAMWJKYJFHZTM2YWE4OW/"
                }
               ]
             }   
             Total count ; 3 
            
            */

      // Get details of a single sub account
      var response = api.Subaccount.Get(
      id: "SAXXXXXXXXXXXXXXXXXX" // Subaccount_auth_id
      );

      //Prints the response
      Console.WriteLine(response);

      /*
            Sample Output 
            {
              "account": "/v1/Account/XXXXXXXXXXXXXXXXX/",
              "api_id": "a0d00ff2-b5c3-11e4-af95-22000ac54c79",
              "auth_id": "SAOWQ0NJFKMTRKMTRMZT",
              "auth_token": "M2M4YzllOGNlOTJkOWY0YmI2MWFmMTI1YzAzYTY0",
              "created": "2015-02-16",
              "enabled": true,
              "modified": "2015-02-16",
              "name": "Testing_subaccount",
              "new_auth_token": "M2M4YzllOGNlOTJkOWY0YmI2MWFmMTI1YzAzYTY0",
              "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/Subaccount/SAOWQ0NJFKMTRKMTRMZT/"
            } 
            */

      // Delete a sub account
      var response = api.Subaccount.Delete(
      id: "SAXXXXXXXXXXXXXXXXXX", // Subaccount_auth_id
      cascade: true);
      Console.WriteLine(response);

      /* 
            Successful Output 
            " "
            Unsuccessful Output
            {
              "api_id": "0006e040-b5c4-11e4-af95-22000ac54c79",
              "error": "not found"
            } 
            */
    }
  }
}