using System;
using System.Collections.Generic;
using RestSharp;
using Plivo.API;

namespace apps
{
    class Program
    {
        static void Main(string[] args)
        {
            RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");
            
            // Create a new appplication
            IRestResponse<GenericResponse> resp = plivo.create_application(new Dictionary<string,string>()
            {
              {"answer_url", "http://example.com"},
              {"app_name", "Tesp_app"}
            });

            //Prints the response
            Console.Write(resp.Content);

            /*
            Sample Output
            {
              "api_id": "9f20d25a-b5c7-11e4-9107-22000afaaa90",
              "app_id": "21154456373728579",
              "message": "created"
            }
             
            */
            
            // Get details of all existing applications
            IRestResponse<ApplicationList> res = plivo.get_applications(new Dictionary<string, string>(){ });

            //Prints the response
            Console.Write(res.Content);

            // Prints the total number of apps
            Console.WriteLine("Total count : " + res.Data.meta.total_count);

            // Prints the public_uri, default_app
            int count = res.Data.meta.total_count;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("public_uri : {0}, default_app : {1}",res.Data.objects[i].public_uri, res.Data.objects[i].default_app);
            }

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
            Total count : 9
             
            public_uri : False, default_app : False
            public_uri : False, default_app : False
            public_uri : False, default_app : False
            public_uri : True, default_app : True
            public_uri : False, default_app : False
            public_uri : True, default_app : False
            public_uri : False, default_app : False
            public_uri : False, default_app : False
            public_uri : False, default_app : False

            */
                       
            // Get details of a single application
            IRestResponse<Application> res1 = plivo.get_application(new Dictionary<string, string>()
            {
                {"app_id","21154456373728579"}
            });

            //Prints the response
            Console.Write(res1.Content);

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
            IRestResponse<GenericResponse> res2 = plivo.modify_application(new Dictionary<string, string>()
            {
                {"app_id","21154456373728579"},
                {"answer_url","http://exampletest.com"}
            });

            //Prints the response
            Console.Write(res2.Content);

            /*
            Sample Output
            {
              "api_id": "4117403e-b5c9-11e4-8ccf-22000afb14f7",
              "message": "changed"
            }
            */
            
            // Delete an application
            IRestResponse<GenericResponse> res3 = plivo.delete_application(new Dictionary<string,string>()
            {
              {"app_id","21154456373728579"}  
            });

            //Prints the response
            Console.Write(res3.Content);

            /*
            Sample Output
            {
              "api_id": "96823a92-b5c9-11e4-ac1f-22000ac51de6",
              "error": "not found"
            }
            */
        }
    }
}
            