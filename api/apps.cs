using System;
using System.Collections.Generic;
using Plivo;

namespace apps {
  class Program {
    static void Main(string[] args) {
      var api = new PlivoApi("YOUR_AUTH_ID", "YOUR_AUTH_TOKEN");

      // Create a new appplication
      var response = api.Application.Create(
      appName: "Test Application", answerUrl: "http://answer.url");
      //Prints the response
      Console.WriteLine(response);

      /*
            Sample Output
            {
              "api_id": "9f20d25a-b5c7-11e4-9107-22000afaaa90",
              "app_id": "21154456373728579",
              "message": "created"
            }
             
            */

      // Get details of all existing applications
      var response = api.Application.List(
      limit: 5, offset: 0);

      //Prints the response
      Console.WriteLine(response);

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

      // Get details of a single application
      var response = api.Application.Get(
      appId: "15784735442685051");

      //Prints the response
      Console.Write(response);

      /*
            Sample Output 
            {
              "answer_method": "POST",
              "answer_url": "http://example.com",
              "api_id": "29c60014-b5c9-11e4-8ccf-22000afb14f7",
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
            }
  
            */

      // Modify an application
      var response = api.Application.Update(
      appId: "15784735442685051", answerUrl: "http://updated.answer.url");

      //Prints the response
      Console.WriteLine(response);

      /*
            Sample Output
            {
              "api_id": "4117403e-b5c9-11e4-8ccf-22000afb14f7",
              "message": "changed"
            }
            */

      // Delete an application
      var response = api.Application.Delete(
      appId: "15784735442685051");

      //Prints the response
      Console.Write(response);

      /*
            Successful Output
            " "

            Unsuccessful Output
            {
              "api_id": "96823a92-b5c9-11e4-ac1f-22000ac51de6",
              "error": "not found"
            }
            */
    }
  }
}