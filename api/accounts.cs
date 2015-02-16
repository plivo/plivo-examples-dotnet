using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace send_sms
{
    class bulk_sms
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");
            /*
            // Get Account details 
            IRestResponse<Account> resp = plivo.get_account();

            //Prints the response
            Console.Write(resp.Content);

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
            IRestResponse<GenericResponse> res = plivo.modify_account(new Dictionary<string, string>()
            {
                {"name","Testing"},
                {"city","City Test"},
                {"address","City address"},
                {"timezone","Asia/Kolkata"}
            });

            //Prints the response
            Console.Write(res.Content);

            /*
            Sample Output
            {
              "api_id": "a450e166-b5c2-11e4-b423-22000ac8a2f8",
              "message": "changed"
            }
            */

            // Create a sub account
            IRestResponse<GenericResponse> res1 = plivo.create_subaccount(new Dictionary<string, string>()
            {
                {"name","Testing_subaccount"},
                {"enabled","True"}
            });

            //Prints the response
            Console.Write(res1.Content);

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
            IRestResponse<GenericResponse> res2 = plivo.modify_subaccount(new Dictionary<string, string>()
            {
                {"subauth_id","SAOWQ0NJFKMTRKMTRMZT"},
                {"name","Testing_subaccount"}
            });

            //Prints the response
            Console.Write(res2.Content);

            /*
            Sample Output
            {
              "api_id": "0fa7a6e8-b5c3-11e4-8ccf-22000afb14f7",
              "message": "changed"
            }
            */
            
            // Get details of all sub accounts
            IRestResponse<SubAccountList> res3 = plivo.get_subaccounts();

            //Prints the response
            Console.Write(res3.Content);

            // Print the total number of sub accounts
            Console.Write("Total count : " + res3.Data.meta.total_count);

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
            IRestResponse<SubAccount> res4 = plivo.get_subaccount(new Dictionary<string, string>()
            {
                {"subauth_id","SAOWQ0NJFKMTRKMTRMZT"}
            });

            //Prints the response
            Console.Write(res4.Content);

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
            IRestResponse<GenericResponse> res5 = plivo.delete_subaccount(new Dictionary<string, string>()
            {
                {"subauth_id","SAOWQ0NJFKMTRKMTRMZT"}
            });

            //Prints the response
            Console.Write(res5.Content);

            Console.ReadLine();
            
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