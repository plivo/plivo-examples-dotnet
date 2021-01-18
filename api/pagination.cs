using System;
using System.Collections.Generic;
using Plivo;

namespace apps
{
    class Program
    {
        static void Main(string[] args)
        {
          var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");
          
          // Get details of all existing applications
          var response = api.Application.List(
            limit:5,
            offset:0
            );

            //Prints the response
          Console.Write(response);

          /*
          Sample Output
           {
              "api_id": "b9125c88-b5c7-11e4-af95-22000ac54c79",
              "meta": {
                "limit": 2,
                "next": "/v1/Account/XXXXXXXXXXXXXXXXX/Application/?limit=2&offset=2",
                "offset": 0,
                "previous": null,
                "total_count": 9
              },
              "objects": [
                {
                  "answer_method": "POST",
                  "answer_url": "http://example.com",
                  "app_id": "21154456373728579",
                  "app_name": "Tesp_app",
                  "default_app": false,
                  "default_endpoint_app": false,
                  "enabled": true,
                  "fallback_answer_url": "",
                  "fallback_method": "POST",
                  "hangup_method": "POST",
                  "hangup_url": "http://example.com",
                  "message_method": "POST",
                  "message_url": "",
                  "public_uri": false,
                  "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/Application/21154456373728579/",
                  "sip_uri": "sip:21154456373728579@app.plivo.com",
                  "sub_account": null
                },
                {
                  "answer_method": "GET",
                  "answer_url": "http://dotnettest.apphb.com/response/ivr",
                  "app_id": "26469261154421101",
                  "app_name": "Receive SMS",
                  "default_app": false,
                  "default_endpoint_app": false,
                  "enabled": true,
                  "fallback_answer_url": "",
                  "fallback_method": "POST",
                  "hangup_method": "POST",
                  "hangup_url": "http://morning-ocean-4669.herokuapp.com/response/conference/",
                  "message_method": "GET",
                  "message_url": "https://dry-fortress-4047.herokuapp.com/delivery_report",
                  "public_uri": false,
                  "resource_uri": "/v1/Account/XXXXXXXXXXXXXXXXX/Application/26469261154421101/",
                  "sip_uri": "sip:26469261154421101@app.plivo.com",
                  "sub_account": null
                }
              ]
            }
            */

            // Print the link to view the next page of results
            Console.WriteLine(response.Meta.Next);

            /*
            Sample successful output
            /v1/Account/XXXXXXXXXXXX/Application/?limit=2&offset=2
            Browse https://api.plivo.com/v1/Account/XXXXXXXXXXXXXXXXX/Application/?limit=2&offset=2
            to view the next page of results. To traverse pages, browse the 'next' and 'previous' urls
            */
        }
    }
}   